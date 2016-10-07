call mcs RSSNetwork.cs /t:library
call mcs RSSData.cs /t:library /r:System.Data /r:RSSNetwork
call mcs RSSUI.cs /t:exe /out:RSSAggregator.exe /r:System.Windows.Forms /r:System.Data /r:System.Drawing /r:RSSData /r:RSSNetwork