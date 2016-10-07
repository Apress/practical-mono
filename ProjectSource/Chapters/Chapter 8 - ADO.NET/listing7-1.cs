using System;
using Glade;
using Gtk;

public class MainWindow 
{   
  public MainWindow () 
  { 
    Glade.XML gui = new Glade.XML ("./RSSAggregator.glade", "frmMain", ""); 
  } 
} 

