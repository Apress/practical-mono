Welcome,

I hope you have or are still enjoying the book, this README addresses
some of the improvements that are possible once you have grasped your 
Mono fundamentals and are keen to experiment. The RSS Aggregator in its
current form is a basic RSS Reader that demonstrates the fundamental
concepts and features of the .NET framework as implemented by the Open Source
community. However, improvements can be made and i've listed some below.

Possible Improvements
=====================

1. You could replace the TextBox control used hold the contents of an
individuals channel items value with a Web Browser control. This will enable
you to view the contents of the RSS item as intended, with hyperlinks 
activated etc.

At the time of writing the RSS Aggregator, the .NET Framework 1.1 does not
include such a control (although Gtk# does), but the .NET Framework 2.x 
does and so you could use this for your new and improved RSS Aggregator

2. You could also modify the code to store ALL of the RSS Channels and their
associated items in the database using the 'channel' and 'channelitems' tables