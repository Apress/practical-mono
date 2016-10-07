using System;
using Glade;
using Gtk;

// Main Window class
public class MainWindow 
{   
    // Member variables
    public Gtk.Window win;
    private Glade.XML xml;

    // Constructor
    public MainWindow () 
    { 
        /// Load our XML Glade file
        xml = new Glade.XML ("./RSSAggregator.glade", "frmMain", ""); 


	Gtk.MenuItem mnuExit = (xml.GetWidget("exit1") as Gtk.MenuItem);
	mnuExit.Activated += new EventHandler(on_exit1_activate);

        // Create our Gtk.Window reference
	win = (xml.GetWidget("frmMain") as Gtk.Window);
    } 

    // OPEN menu item signal
    public void on_open1_activate(System.Object o, EventArgs args)
    {
        Console.Out.WriteLine("File OPEN signal");
    }

    // CLOSE menu item signal
    public void on_close1_activate(System.Object o, EventArgs args)
    {
        Console.Out.WriteLine("File CLOSE signal");
    }

    // QUIT menu item signal
    public void on_quit1_activate(System.Object o, EventArgs args)
    {
        Console.Out.WriteLine("File QUIT signal");
        Application.Quit();
    }

    // About ABOUT item signal
    public void on_about1_activate(System.Object o, EventArgs args)
    {
        Console.Out.WriteLine("ABOUT signal");
    }


} 

// Main applications
public class MainApp
{

    public static void Main(string[] args)
    {
        // Initialise Gtk
        Gtk.Application.Init();

        // Create our window
        MainWindow wndMain = new MainWindow();

        // Show the window
        wndMain.win.ShowAll();

        // Run our applications
        Application.Run();
    }
}
