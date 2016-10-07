// InitialiseChannelItemCollection - Return a collection of ~CCC
ChannelItemEntities as read from
// the RSS Feed.
//
public ArrayList  InitialiseChannelItemCollection()
{
  ArrayList list = null;
  CreateXPathNavigator();
  if (nav != null)
  {
    XPathNodeIterator nodeItr = nav.Select("//channel/item");
    while (nodeItr.MoveNext())
    {
      // If an item has been found, initialize our ArrayList class type if necessary
      if (list == null)
        list = new ArrayList();

      // Create our ChannelItemEntity instance and initialize it
      ChannelItemEntity item = new ChannelItemEntity();
      item.title = nodeItr.Current.Evaluate("string(./title)").ToString();
      item.link = nodeItr.Current.Evaluate("string(./link)").ToString();
      item.description = nodeItr.Current.Evaluate("string(./description)").~CCC
ToString();
      item.author = nodeItr.Current.Evaluate("string(./author)").ToString();
      item.category = nodeItr.Current.Evaluate("string(./category)").ToString();
      item.publishedDate = nodeItr.Current.Evaluate("string(./pubDate)").ToString();
      item.guid = nodeItr.Current.Evaluate("string(./guid)").ToString();

      // Add it to our collection
      list.Add(item);
    }
  }       
  return list;
}
