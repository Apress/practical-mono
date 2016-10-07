using System;
using System.Windows.Forms;
using System.Data;
using RSSAggreagatorData;
using System.Collections;

class MyDialog: Form
{

  Label label1;
  public TextBox txtRssURL;
  Button btnOpen;
  Button btnClose;

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
    txtRssURL.Text = "http://news.bbc.co.uk/rss/newsonline_uk_edition/front_page/rss.xml";

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
  // Member variable declaration
  public MyMenu mainMenu;

  private Panel pnlSubscriptions;
  private Panel pnlThreads;
  private Panel pnlContents;

  private Label lblSubscriptions;
  private TextBox txtContents;
  private DataGrid dgThreads;
  private ListBox lstSubscriptions;
//  private DataTable t;

  // Data Entity Items assocaited with form
  // Ties together CHAPTER 8 ADO.NET and CHAPTER 6 - WINDOWS FORMS)
  RSSFeedDataEntity feedEntity = null;
  ArrayList feeds = null;
  ArrayList threads = null;
  
  // Define a placeholder for our Channel Class, this represents the current
  // Channel (or feed in the database)
  Channel currentChannel = null;

  public MyMainForm()
  {

    // Data Entity Items assocaited with form
    // Ties together CHAPTER 8 ADO.NET and CHAPTER 6 - WINDOWS FORMS)
    RSSFeedDataEntity feedEntity = new RSSFeedDataEntity(); 
    mainMenu = new MyMenu();
    this.Menu = mainMenu;

    // Assign our event handler
    mainMenu.miFileOpen.Click += new System.EventHandler(this.miFileOpen_Click);
    mainMenu.miFileClose.Click += new System.EventHandler(this.miFileClose_Click);
    mainMenu.miFileExit.Click += new System.EventHandler(this.miFileExit_Click);

    // Construct our GUI controls
    lblSubscriptions = new System.Windows.Forms.Label();
    lstSubscriptions = new System.Windows.Forms.ListBox();
    dgThreads = new System.Windows.Forms.DataGrid();
//    t = new System.Data.DataTable();
    txtContents = new System.Windows.Forms.TextBox();
    pnlSubscriptions = new System.Windows.Forms.Panel();
    pnlThreads = new System.Windows.Forms.Panel();
    pnlContents = new System.Windows.Forms.Panel();

    // Initialize pnlSubscriptions
    pnlSubscriptions.AutoScroll = true;
    pnlSubscriptions.Controls.Add(lblSubscriptions);
    pnlSubscriptions.Controls.Add(lstSubscriptions);
    pnlSubscriptions.Location = new System.Drawing.Point(8, 8);
    pnlSubscriptions.Name = "pnlSubscriptions";
    pnlSubscriptions.Size = new System.Drawing.Size(208, 432);
    pnlSubscriptions.TabIndex = 0;

    // Initialise lblSubscriptions
    lblSubscriptions.Location = new System.Drawing.Point(8, 8);
    lblSubscriptions.Name = "lblSubscriptions";
    lblSubscriptions.Size = new System.Drawing.Size(192, 23);
    lblSubscriptions.TabIndex = 1;
    lblSubscriptions.Text = "Subscribed Feeds:";

    // Subscriptions Listbox

    lstSubscriptions.Location = new System.Drawing.Point(8, 32);
    lstSubscriptions.Name = "lstSubscriptions";
    lstSubscriptions.Size = new System.Drawing.Size(192, 394);
    lstSubscriptions.TabIndex = 0;

    // Initialize pnlThreads
    pnlThreads.Controls.Add(this.dgThreads);
    pnlThreads.Location = new System.Drawing.Point(224, 8);
    pnlThreads.Name = "pnlThreads";
    pnlThreads.Size = new System.Drawing.Size(472, 208);
    pnlThreads.TabIndex = 1;

    // Initialize dgThreads
    dgThreads.DataMember = "";
    dgThreads.HeaderForeColor = System.Drawing.SystemColors.ControlText;
    dgThreads.Location = new System.Drawing.Point(8, 32);
    dgThreads.Name = "dgThreads";
    dgThreads.Size = new System.Drawing.Size(456, 168);
    dgThreads.TabIndex = 0;

    // Initialize pnlContents
    pnlContents.Controls.Add(txtContents);
    pnlContents.Location = new System.Drawing.Point(224, 224);
    pnlContents.Name = "pnlContents";
    pnlContents.Size = new System.Drawing.Size(472, 216);
    pnlContents.TabIndex = 2;

    // Contents Textbox  
    txtContents.AutoSize = false;
    txtContents.Location = new System.Drawing.Point(8, 8);
    txtContents.Name = "textBox1";
    txtContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
    txtContents.Size = new System.Drawing.Size(456, 200);
    txtContents.TabIndex = 0;
    txtContents.Text = "";

    // Initialise the form
    this.Text = "Main Form";
    this.ClientSize = new System.Drawing.Size(704, 449);
    this.Controls.Add(this.pnlContents);
    this.Controls.Add(this.pnlThreads);
    this.Controls.Add(this.pnlSubscriptions);

    // Initialise Data
    feeds = feedEntity.ReadAll();
    if (feeds.Count > 0)
    {
      // Initialise User-Inteface control (lstSubscriptions) with feeds
      RefreshSubscriptions();
    }
      
      
/*    
    t.Columns.Add("Title");
    t.Columns.Add("Date");
    t.Columns.Add("Author");
    t.Columns.Add("Subject");
    dgThreads.DataSource = t;
    pnlThreads.Controls.Add(dgThreads);
*/
  }

  // Refresh the Subscriptions control from our list
  //
  public void RefreshSubscriptions()
  {
    //TODO
    Console.WriteLine("->RefreshSubscriptions() called");
  }

  // Refresh the Threads control from our list
  //
  public void RefreshThreads(Channel channel)
  {
    //TODO
    if (channel != null)
    {
      if (channel.channelItemEntityList != null)
      {
        dgThreads.DataSource = feeds;      
      }
    }
    else
      Console.WriteLine("CHannel is null");
    Console.WriteLine("->RefreshThreads() called");
  }

  public void miFileOpen_Click(object sender, System.EventArgs e)
  {
    // Implementation goes here
    MyDialog dlgFileOpen = new MyDialog("File Open");
    dlgFileOpen.ShowDialog();
    if (dlgFileOpen.ChoseOpen)
    {
      // If the user has asked to OPEN a Feed, we'll add this to the list
      // Create RSSFeed container
      RSSFeed feed = new RSSFeed();
      feed.feedName = dlgFileOpen.txtRssURL.Text;
      feed.feedURL = dlgFileOpen.txtRssURL.Text;
      // Add it to our list
      feeds.Add(feed);
      Console.WriteLine("->Added 'feed' to 'feeds' list");
      // Refresh our Subscription control
      RefreshSubscriptions();
      // Set the current channel
      if (currentChannel == null)
        currentChannel = new Channel();
      currentChannel.InitialiseFromURL(feed.feedURL);      
      // Read the Threads associated with the current channel
      RefreshThreads(currentChannel);
      // Debug
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