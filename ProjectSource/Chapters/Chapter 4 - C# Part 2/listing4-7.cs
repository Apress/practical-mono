using System;


// Delegate for our News Channel
public delegate void NewsChangeEventHandler(string news);


// Class which Records the Latest News Headlinme
public class NewsChannel
{

  // Last News Item (stops repeats)
  private string newsHeadline;

  // Event Fired on a change of news
  public event NewsChangeEventHandler LatestNewsChange;

  // News Property
  public string LatestNewsHeadline 
  {
    // Set the news headline
    set 
    {
      if (newsHeadline != value)
      {
        LatestNewsChange(value);
        newsHeadline= value;
      }
    }
  }
}


// A class representing somebody watching the channel
public class NewsChannel_Watcher
{
  private string _name;

  public NewsChannel_Watcher(NewsChannel  nh, string name)
  {
    _name = name;
    nh.LatestNewsChange += new NewsChangeEventHandler(NewsChanged_Event);
  }

  // Event Fires, show latest news
  private void NewsChanged_Event(string news)
  {
    Console.Out.WriteLine(_name + " is informed of " + news);
  }
}


// Testbed 
class Test
{
  static void Main(string[] args)
  { 
    // A Single news channgel
    NewsChannel n = new NewsChannel();

    // Setup all of the reporters 
    NewsChannel_Watcher nick = new NewsChannel_Watcher(n, "nick");
    NewsChannel_Watcher ashley = new NewsChannel_Watcher(n, "ashley");
    NewsChannel_Watcher emma = new NewsChannel_Watcher(n, "emma");
    NewsChannel_Watcher megan = new NewsChannel_Watcher(n, "megan");

    // Pulic a news item
    n.LatestNewsHeadline = "mark buys a drink";    

  }

}