using System;
using System.Windows.Forms;

public class MyMenu : System.Windows.Forms.MainMenu	
{
  // Member Variables
  private System.Windows.Forms.MenuItem miFile;	
  private System.Windows.Forms.MenuItem miFileOpen;
  private System.Windows.Forms.MenuItem miFileClose;
  private System.Windows.Forms.MenuItem miFileExit;

  //
  // Default constructor
  //
  public MyMenu()
  {        
    // Create our Main Menu item
    miFile = new MenuItem("&File");
    this.MenuItems.Add(miFile);

    // Create the FILE menu items
    miFileOpen = new MenuItem("&Open");
    miFileClose = new MenuItem("&Close");        
    miFileExit = new MenuItem("E&xit");

    // ...and add them to the FILE menu item
    miFile.MenuItems.Add(miFileOpen);
    miFile.MenuItems.Add(miFileClose);
    miFile.MenuItems.Add(new MenuItem("-"));
    miFile.MenuItems.Add(miFileExit);
  }
}

public class MyForm : Form
{
  public MyMenu mainMenu;

  public MyForm()
  {
    this.Text = "RSS Aggregator";
    this.Width = 800;
    this.Height = 600;

    mainMenu = new MyMenu();
    this.Menu = mainMenu;
  }
}


public class MyTestApplication
{

  public static void Main(string[] args)
  {

    Application.Run(new MyForm());

  }
}