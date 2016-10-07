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

public class MainApp
{
  public static void Main(string[] args)
  {
    Gtk.Application.Init();
    MainWindow wndMain = new MainWindow();
    Gtk.Application.Run();
  }
}
