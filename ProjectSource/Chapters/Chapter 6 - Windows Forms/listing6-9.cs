using System;
using System.Windows.Forms;

class MyDialog: Form
{

  Label label1;
  public TextBox txtRssURL;  // Public scope for access
  Button btnOpen;
  Button btnClose;

  // Define our new state variable
  public bool ChoseOpen = false;

  // Default constructor
  public MyDialog(string title)
  {
    this.Text = title;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.ClientSize = new System.Drawing.Size(362, 96);
    this.MinimizeBox= false;
    this.MaximizeBox = false;

    // Label
    label1 = new Label();
    label1.Text = "Enter RSS URL:";
    label1.Size = new System.Drawing.Size(100, 16);
    label1.Location = new System.Drawing.Point(8, 19);

    // TextBox for URL
    txtRssURL = new TextBox();
    txtRssURL.Location = new System.Drawing.Point(112, 16);
    txtRssURL.Size = new System.Drawing.Size(240, 20);

    // OPEN button
    btnOpen = new Button();
    btnOpen.Text = "Open";
    btnOpen.Location = new System.Drawing.Point(192, 56);
    btnOpen.Click += new System.EventHandler(btnOpen_Click);

    // CANCEL button
    btnClose = new Button();
    btnClose.Text="Close";
    btnClose.Location = new System.Drawing.Point(275, 56);
    btnClose.Click += new System.EventHandler(btnClose_Click);

    // Add the controls to the form
    this.Controls.Add(label1);
    this.Controls.Add(txtRssURL);
    this.Controls.Add(btnOpen);
    this.Controls.Add(btnClose);

  }

  // Close button event
  //
  public void btnClose_Click(object sender, System.EventArgs e)
  {
    ChoseOpen = false;
    using (sender as Form)
    {
      Close();
    }
  }

  // Open button event
  //
  public void btnOpen_Click(object sender, System.EventArgs e)
  {
    ChoseOpen = true;
    this.Close();
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
    if (dlgFileOpen.ChoseOpen)
    {
      Console.Out.WriteLine(dlgFileOpen.txtRssURL.Text);
    }

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