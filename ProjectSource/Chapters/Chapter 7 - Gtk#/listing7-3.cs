using System;
using Glade;
using Gtk;

public class MainWindow 
{   
    public Gtk.Window win;
    Glade.XML xml;

    public MainWindow () 
    { 
        xml = new Glade.XML ("./RSSAggregator.glade", "frmMain", ""); 
	win = (xml.GetWidget("frmMain") as Gtk.Window);
   } 
} 

public class MainApp
{

	public static void Main(string[] args)
	{
		Gtk.Application.Init();
		MainWindow wndMain = new MainWindow();
		wndMain.win.ShowAll();
		Application.Run();
	}
}
