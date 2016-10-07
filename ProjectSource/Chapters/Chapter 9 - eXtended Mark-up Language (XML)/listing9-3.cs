using System;
using System.Drawing;
using System.Xml;
using System.Collections;

public class ChannelImageEntity
{
  public string url = "";
  public string title = "";
  public string link = "";
}

public class ChannelImage
{
  // Member variables
  public ChannelImageEntity channelImageEntity;

  // Default constructor - Initialize out ChannelImageEntity instance
  public ChannelImage()
  {
    channelImageEntity = new ChannelImageEntity();
  }

  // Member Methods
  public bool DownloadImage(string url)
  {
    // Todo
    return true;
  }

  public Image GetImage()
  {
    // Todo
    return null;
  }

}
