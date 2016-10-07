using System;
using System.Windows.Forms;

class MyDialog: Form
{
  // Default constructor
  public MyDialog(string title)
  {
    this.Text = title;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.ClientSize = new System.Drawing.Size(362, 96);
    this.MinimizeBox= false;
    this.MaximizeBox = false;
  }
}

public class MyMenu : System.Windows.Forms.MainMenu	
{
  // Member Variables
  public  System.Windows.Forms.MenuItem miFile;	
  public  System.Windows.Forms.MenuItem miFileOpen;
  public  System.Windows.Forms.MenuItem miFileClose;
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


class MyMainForm : Form
{

  public MyMenu mainMenu;

  public MyMainForm()
  {
    this.Text = "Main Form";
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
    MyDialog dlgFileOpen = new MyDialog("File Open");
    dlgFileOpen.ShowDialog();
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


class Test
{

	static public void Main(string[] args)
	{

		// Fixed Dialog
		Application.Run(new MyMainForm());
	}
}