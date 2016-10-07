using System;
using System.Windows.Forms;

public class MyMenu : System.Windows.Forms.MainMenu	
{
  // Member Variables
  private System.Windows.Forms.MenuItem miFile;	
  public System.Windows.Forms.MenuItem miFileOpen;
  public System.Windows.Forms.MenuItem miFileClose;
  public  System.Windows.Forms.MenuItem miFileExit;

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

    // Assign our event handler
    mainMenu.miFileOpen.Click += new System.EventHandler(this.miFileOpen_Click);
    mainMenu.miFileClose.Click += new System.EventHandler(this.miFileClose_Click);
    mainMenu.miFileExit.Click += new System.EventHandler(this.miFileExit_Click);
  }

  public void miFileOpen_Click(object sender, System.EventArgs e)
  {
    // Implementation goes here
  }

  public void miFileClose_Click(object sender, System.EventArgs e)
  {
    // Implementation goes here
  }

  public void miFileExit_Click(object sender, System.EventArgs e)
  {
    // Implementation goes here
    Console.Out.WriteLine("File Exit clicked");
    this.Close();
  }

}


public class MyTest
{
  public static void Main(string[] args)
  {
    Application.Run(new MyForm());
  }
}