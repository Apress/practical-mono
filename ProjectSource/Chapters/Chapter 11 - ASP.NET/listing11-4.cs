using System;
using System.Runtime.Remoting.Messaging;

class Test
{
  // Indicator as to whether the Async call is finished
  public static bool bFinished = false;

  // Callback method
  //
  public static void GetFeed_Callback(IAsyncResult result)
  {
    RSSFeed rs = (RSSFeed)result.AsyncState;
    Console.Out.WriteLine(rs.EndGetFeed(result));
    bFinished = true;
  }

  public static void Main(string[] args)
  {
    RSSFeed rs = new RSSFeed();
    AsyncCallback callback = new AsyncCallback(Test.GetFeed_Callback);

    rs.BeginGetFeed("http://newsrss.bbc.co.uk/rss/newsonline_uk_edition/~CCC
front_page/rss.xml", callback, rs);

    Console.Out.WriteLine("Waiting for web service to finish");
    while (!bFinished);
    Console.Out.WriteLine("Web service has finished!");

  }
}
