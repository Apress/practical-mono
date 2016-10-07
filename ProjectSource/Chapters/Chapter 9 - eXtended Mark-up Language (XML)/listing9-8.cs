public class RSSConfiguration
{

  // Member variables
  public string filename = "";
  public XmlDocument doc = null;

  // CreateDefaultConfiguration – Create our configuration~CCC
xml file with default values
  //
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

  // UpdateElementValue - Updates the value of the ~CCC
specified element to a new value
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

  // Save - Saves the XML document to disk using the same filename~CCC
 as that used to open it
  //
  public void Save()
  {
    if (doc != null)
      doc.Save(this.filename);
  }
