using System;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;

public class ChannelEntity
{
  public string title = "";
  public string link = "";
  public string description = "";
}

public class ChannelImageEntity
{
  public string url = "";
  public string title = "";
  public string link = "";
}

public class ChannelItemEntity
{
  public string title = "";
  public string link = "";
  public string description = "";
  public string author = "";
  public string category = "";
  public string publishedDate = "";
  public string guid = "";
}


public class ChannelImage
{
  // Member Variables
  public ChannelImageEntity channelImageEntity;

  // Default constructor - Initialise out ChannelImageEntity instance
  public ChannelImage()
  {
    channelImageEntity = new ChannelImageEntity();
  }

  // Methods
  // ..Implementation goes here

}


public class Channel
{
  // Member Variables
  public ChannelEntity channelEntity = null;
  public ChannelImage channelImage = null;
  public ArrayList channelItemEntityList;

  XPathDocument doc = null;
  XPathNavigator nav = null;

  // Methods

  // LoadFromURL(uri) - Load 
  // 
  public void LoadFromURL(string url)
  {
    // Establish a URI (of type URL) and create a HttpWebRequest instance
    XmlTextReader tr = new XmlTextReader(url);

  }

  // .. Default Constructor
  public void LoadFromXml(string xmlFilename)
  {
    try
    {
      doc = new XPathDocument(xmlFilename);
    }
    catch(Exception msg)
    {
      // Handle exception here
      Console.Out.WriteLine("Error: " + msg.Message);
    }
  }

  // CreateXPathNavigator - Creates an XPathNavigator from the XPathDocument
  //
  public void CreateXPathNavigator()
  {
    if ((nav == null))
      nav = doc.CreateNavigator();
  }
 
  // InitialiseChannelEntity - Initialise the ChannelEntity passed with values from the RSS Feed
  //
  public void InitialiseChannelEntity()
  {
    // Create our navigator from the channel element
    CreateXPathNavigator();
    if (nav != null)
    {
      if (channelEntity == null)
        channelEntity = new ChannelEntity();
      channelEntity.title = nav.Evaluate("string(//channel/title)").ToString();
      channelEntity.link = nav.Evaluate("string(//channel/link)").ToString();
      channelEntity.description = nav.Evaluate("string(//channel/description)").ToString();
    }
  }

  // InitialiseChannelImage - Initialise the ChannelImage from the RSS feed if possible
  // 
  public void InitialiseChannelImage()
  {
    CreateXPathNavigator();
    if (nav != null)
    {
      XPathNodeIterator nodeItr = nav.Select("//channel/image");
      if (nodeItr.MoveNext())
      {
        if (channelImage == null)
          channelImage = new ChannelImage();
        channelImage.channelImageEntity.url = nav.Evaluate("string(//channel/image/url)").ToString();
        channelImage.channelImageEntity.title = nav.Evaluate("string(//channel/image/title)").ToString();
        channelImage.channelImageEntity.link = nav.Evaluate("string(//channel/image/link)").ToString();
	/*
        Console.Out.WriteLine("<image> found");
        Console.Out.WriteLine(channelImage.channelImageEntity.url);
        Console.Out.WriteLine(channelImage.channelImageEntity.title);
        Console.Out.WriteLine(channelImage.channelImageEntity.link);
        */
      }
    }
  }

  // InitialiseChannelItemCollection - Return a collection of ChannelItemEntities as read from
  //                                   the RSS Feed.
  //
  public ArrayList InitialiseChannelItemCollection()
  {
    ArrayList list = null;
    CreateXPathNavigator();
    if (nav != null)
    {
      XPathNodeIterator nodeItr = nav.Select("//channel/item");
      while (nodeItr.MoveNext())
      {
        // If an item has been found, initialise our ArrayList class type if necessary
        if (list == null)
          list = new ArrayList();

        // Create our ChannelItemEntity instance and initialise it
        ChannelItemEntity item = new ChannelItemEntity();
        item.title = nodeItr.Current.Evaluate("string(./title)").ToString();
        item.link = nodeItr.Current.Evaluate("string(./link)").ToString();
        item.description = nodeItr.Current.Evaluate("string(./description)").ToString();
        item.author = nodeItr.Current.Evaluate("string(./author)").ToString();
        item.category = nodeItr.Current.Evaluate("string(./category)").ToString();
        item.publishedDate = nodeItr.Current.Evaluate("string(./pubdate)").ToString();
        item.guid = nodeItr.Current.Evaluate("string(./guid)").ToString();

        // Add it to our collection
        list.Add(item);
      }
    }       
    return list;
  }

  // InitialiseFromFeed( filename ) - Initialises the Channel class & Entities from the RSS Feed
  //                                  which is passed as an XML file
  //
  public void InitialiseFromFeed(string RSSFilename)
  {
    LoadFromXml(RSSFilename);
    InitialiseChannelEntity();
    InitialiseChannelImage();    
    channelItemEntityList = InitialiseChannelItemCollection();
  }


  // InitialiseFromURL( url ) - Initialises the Channel class & Entities from the RSS Feed
  //                            which is passed as a URL
  //
  public void InitialiseFromURL(string url)
  {
    LoadFromURL(url);
    InitialiseChannelEntity();
    InitialiseChannelImage();    
    channelItemEntityList = InitialiseChannelItemCollection();
  }

  // ReadChannelElement() - returns the XMLNode relating to the <channel> element
  //
  /*
  public XmlNode ReadChannelElement()
  {    
    XmlNode channelNode = doc.SelectSingleNode("//channel");
    return channelNode;
  }
  */
}


public class RSSConfiguration
{

  // Member variables
  public string filename = "";
  public XmlDocument doc = null;

  public void CreateDefaultConfiguration()
  {
    XmlTextWriter xml = new XmlTextWriter("configuration.xml",null);
    xml.WriteStartElement("configuration");
    
    xml.WriteElementString("defaultfeed", "");
    xml.WriteElementString("dbconnection", "");
    xml.WriteElementString("maxchannelitems", "30");

    xml.WriteEndElement();
    xml.Close();
  }

  // UpdateElementValue - Updates the value of the specified element to a new value
  //
  public void UpdateElementValue(string elementName, string newValue)
  {
    if (doc != null)
    {
      XmlNode node = doc.SelectSingleNode(@"//configuration/"+elementName);
      if (node != null)
      {
        node.InnerText=newValue;
      }
    }
  }

  // Load - Loads the XML document from disk into memory 
  //
  public void Load(string filename)
  {
    this.filename = filename;
    doc = new XmlDocument();
    doc.Load(filename);
  }

  // Save - Saves the XML document to disk using the same filename as that used to open it
  //
  public void Save()
  {
    if (doc != null)
      doc.Save(this.filename);
  }
}

