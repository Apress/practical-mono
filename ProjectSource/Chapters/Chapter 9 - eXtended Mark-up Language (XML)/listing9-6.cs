// 
// Channel demo
//
Channel ch =  new Channel();
ch.InitialiseFromFeed("rss.xml");

ArrayList l = ch.InitialiseChannelItemCollection();
foreach (Object o in l)
{
  ChannelItemEntity e = (ChannelItemEntity)o;
  Console.Out.WriteLine("**********************************");
  Console.Out.WriteLine("<title> = " +e.title);
  Console.Out.WriteLine("<link> = " +e.link);
  Console.Out.WriteLine("<description> = " +e.description);
  Console.Out.WriteLine("<author> = " +e.author);
  Console.Out.WriteLine("<category> = " +e.category);
  Console.Out.WriteLine("<publishedDate> = " +e.publishedDate);
  Console.Out.WriteLine("<guid> = " +e.guid);
  Console.Out.WriteLine("**********************************");
}
