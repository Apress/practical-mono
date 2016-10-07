using System;
using System.Windows.Forms;
using System.Data;
using RSSAggregatorData;
using System.Collections;
using System.Drawing;
using System.Net;
using System.IO;

// MyDialog - RSS File Open dialog box
//
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
    btnOpen.Text = "&Open";
    btnOpen.Location = new System.Drawing.Point(192, 56);
    btnOpen.Click += new System.EventHandler(btnOpen_Click);

    // CANCEL button
    btnClose = new Button();
    btnClose.Text="&Close";
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

// MyMenu - Main Menu for the application that is attached to the main form
//
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
    miFileOpen = new MenuItem("&Open Feed");
    miFileClose = new MenuItem("&Close Feed items");        
    miFileExit = new MenuItem("E&xit");

    // ...and add them to the FILE menu item
    miFile.MenuItems.Add(miFileOpen);
    miFile.MenuItems.Add(miFileClose);
    miFile.MenuItems.Add(new MenuItem("-"));
    miFile.MenuItems.Add(miFileExit);
  }
}

// MyMainForm - Main form for the whole application
//
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
  private PictureBox pctImage;
  private Label lblImage;


  public bool bViewOffline = false;

  // Data Entity Items assocaited with form
  // Ties together CHAPTER 8 ADO.NET and CHAPTER 6 - WINDOWS FORMS)
  public RSSFeedDataEntity feedEntity = null;
  ArrayList feeds = null;  
  
  // Define a reference to the current Channel aka Feed  
  Channel currentChannel = null;

  public MyMainForm()
  {

    // Data Entity Items assocaited with form
    // Ties together CHAPTER 8 ADO.NET and CHAPTER 6 - WINDOWS FORMS)
    feedEntity = new RSSFeedDataEntity(); 
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
    txtContents = new System.Windows.Forms.TextBox();
    pnlSubscriptions = new System.Windows.Forms.Panel();
    pnlThreads = new System.Windows.Forms.Panel();
    pnlContents = new System.Windows.Forms.Panel();
    pctImage = new System.Windows.Forms.PictureBox();
    lblImage = new System.Windows.Forms.Label();


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

    // DataGrid events
    lstSubscriptions.Click += new System.EventHandler(this.lstSubscriptions_Click);

    // Initialize pnlThreads
    pnlThreads.Controls.Add(this.lblImage);
    pnlThreads.Controls.Add(this.pctImage);
    pnlThreads.Controls.Add(this.dgThreads);          
    pnlThreads.Location = new System.Drawing.Point(224, 8);
    pnlThreads.Name = "pnlThreads";
    pnlThreads.Size = new System.Drawing.Size(472,210);
    pnlThreads.TabIndex = 1;

    pnlSubscriptions.BorderStyle = BorderStyle.FixedSingle;
    pnlThreads.BorderStyle = BorderStyle.FixedSingle;
    pnlContents.BorderStyle = BorderStyle.FixedSingle;
    pctImage.BorderStyle= BorderStyle.FixedSingle;
    // Initialize dgThreads
    dgThreads.HeaderForeColor = System.Drawing.SystemColors.ControlText;
    dgThreads.Location = new System.Drawing.Point(8, 32);
    dgThreads.Name = "dgThreads";
    dgThreads.Size = new System.Drawing.Size(472, 208);
    dgThreads.TabIndex = 0;

    // dgThreads TABLE STYLE
    DataGridTableStyle tableStyle = new DataGridTableStyle();
    tableStyle.MappingName = "ArrayList";

    // Column 1    
    DataGridTextBoxColumn pd = new DataGridTextBoxColumn();
    pd.MappingName = "PublishedDate";
    pd.HeaderText = "Date";
    pd.ReadOnly = true;
    pd.Width = 150;
    tableStyle.GridColumnStyles.Add(pd);

    // Column 2
    DataGridTextBoxColumn hd = new DataGridTextBoxColumn();
    hd.MappingName = "Title";
    hd.HeaderText = "Headline";
    hd.ReadOnly = true;
    hd.Width = 300;
    tableStyle.GridColumnStyles.Add(hd);

    // DataGrid style
    dgThreads.TableStyles.Clear();
    dgThreads.TableStyles.Add(tableStyle);

    // DataGrid events
    dgThreads.Click += new System.EventHandler(this.dgThread_Click);

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
    txtContents.Multiline = true;
    txtContents.WordWrap = true;
    // 
    // pctImage
    // 
    pctImage.Location = new System.Drawing.Point(8, 8);
    pctImage.Name = "pctImage";
    pctImage.TabIndex = 1;
    pctImage.Visible = false;
    // 
    // lblImage
    //     
    lblImage.Name = "lblImage";
    lblImage.Size = new System.Drawing.Size(344, 24);
    lblImage.TabIndex = 2;
    lblImage.Text = "label3";
    lblImage.Visible = false;
    
    // Initialise the forms core properties
    this.Text = "RSS Aggreagator";
    this.ClientSize = new System.Drawing.Size(704, 449);
    this.Controls.Add(this.pnlContents);
    this.Controls.Add(this.pnlThreads);
    this.Controls.Add(this.pnlSubscriptions);

    // Initialise the list of default
    // feeds from the 'feed' table
    feeds = feedEntity.ReadAll();
    if (feeds.Count > 0)
    {
      // Initialise User-Inteface control (lstSubscriptions) with feeds
      RefreshSubscriptions();
    }

  }

  // Refresh the Subscriptions control from our list
  //
  public void RefreshSubscriptions()
  {
    lstSubscriptions.BeginUpdate();
    lstSubscriptions.Items.Clear();
    foreach (RSSFeed feed in feeds)
    {
      // Simply add the name of the feed to the list
      lstSubscriptions.Items.Add(feed.feedName);
    }
    lstSubscriptions.EndUpdate();
  }


  // GetImageFromURL - Returns an image based on its URL
  //
  private Bitmap GetImageFromURL(string url)
  {
    WebClient wc = new WebClient();
    Stream s = wc.OpenRead(url);
    Bitmap img = new Bitmap(s);
    s.Close();
    return img;
  }

  // Refresh the Threads control from our list
  //
  public void RefreshThreads(Channel channel)
  {
    // Refresh the channel items
    if (channel != null)
    {
      if (channel.channelItemEntityList != null)
      {
        dgThreads.DataSource = channel.channelItemEntityList;      
      }
    }
  }

  // ClearSubscriptions - Clears Feed list and FeedItem text as no Channel is selected
  public void ClearSubscriptions()
  {
    // Channels associated with the Feed subscription first ...
    dgThreads.DataSource = null;    
    // ... Any Channel text next ...
    txtContents.Text = "";    
    // .. Finally the currently selected Feed
    currentChannel = null;
  }

  // FindFeed(URL) - Look for a feed within the list by its URL and return its index
  //
  public int FindFeed(string URL)
  {
    // Don't check if no feed data is present
    if (feeds == null)
      return -1;
    // Check that the feed hasn't already been added, no point in having two
    for (int i=0; i<feeds.Count; i++)    
    {
      RSSFeed f = (RSSFeed)feeds[i];
      if (f.feedURL.CompareTo(URL) == 0)
        return i;
    }
    return -1;
  }

  // miFileOpen_Click - Event for the user requesting that a feed is opened
  //
  public void miFileOpen_Click(object sender, System.EventArgs e)
  {
    // Show the Dialog box to get the URL for the RSS Feed
    MyDialog dlgFileOpen = new MyDialog("Open Feed");
    dlgFileOpen.ShowDialog();
    if (dlgFileOpen.ChoseOpen)
    {
      // Whether we found the feed subscription or not, clear the list of channel subscriptions
      ClearSubscriptions();

      // Check that the feed hasn't already been added, no point in having two
      int feedIndex = FindFeed(dlgFileOpen.txtRssURL.Text);
      RSSFeed feed = new RSSFeed();
      // Feed found, display a message saying as much
      if (feedIndex != -1)
      {
        // Show a message dialog box ...
        MessageBox.Show("You have already opened this feed and so it will not be opened twice");
        // ...and point the current feed at the feed found
        feed = (RSSFeed)feeds[feedIndex];
      }
      // Feed not found, add it
      if (feedIndex == -1)
      {
        // If the user has asked to OPEN a Feed, we'll add this to the list
        // Create RSSFeed container        
        feed.feedName = dlgFileOpen.txtRssURL.Text;
        feed.feedURL = dlgFileOpen.txtRssURL.Text;
        // Add it to our list
        feedIndex = feeds.Add(feed);
        // Refresh our Subscription control
        RefreshSubscriptions();
      }
      // Initialise the current channel
      InitialiseCurrentChannel(feed.feedURL);
      // Read the Threads associated with the current channel
      RefreshThreads(currentChannel);      
      lstSubscriptions.SelectedIndex = feedIndex;  // Ensure the last opened or selected is highlighted
    }
  }

  // miFileClose_Click - Event for the user requesting that a feed is opened
  //
  public void miFileClose_Click(object sender, System.EventArgs e)
  {
    // Clear the subscription contents relating to current open feed
    ClearSubscriptions();
  }

  // miFileExit_Click - Event for the user wanting to close the application
  //
  public void miFileExit_Click(object sender, System.EventArgs e)
  {
    // Save feed subscriptions to the database
    PersistFeedSubscriptions();
    // Close down the dialog box
    this.Close();
  }

  // dgThread_Click = Event for when a users chooses to view the news item
  //
  public void dgThread_Click(object sender, System.EventArgs e)
  {
    // Get the current row (after the click event) and display the relevant items text
    int rowNumber = dgThreads.CurrentCell.RowNumber;
    ChannelItemEntity item = (ChannelItemEntity)currentChannel.channelItemEntityList[rowNumber];
    txtContents.Text = item.Description;
  }

  // lstSubscriptions_Click = Event for when a users chooses an open subscription from the list
  //
  public void lstSubscriptions_Click(object sender, System.EventArgs e)
  {
    // Get the index
    int index = lstSubscriptions.SelectedIndex;
    if (lstSubscriptions.SelectedIndex == -1)
      if (lstSubscriptions.Items.Count == 1)
        index = 0;   // Only a single item in the list
    // Return if nothing selected
    if (index ==-1)
      return;
    // Get feed name
    string feedName = lstSubscriptions.Items[index].ToString();
    // Find the feed in the list
    int feedIndex = FindFeed(feedName);
    if (feedIndex != -1)
    {
      // Clear the list of channel subscriptions
      ClearSubscriptions();
      // Initialise the channel
      InitialiseCurrentChannel(feedName);
      // Read the Threads associated with the current channel
      RefreshThreads(currentChannel);
    }
  }

  // PersistFeedSubscriptions - Store the list of opened feeds to the database
  //
  public void PersistFeedSubscriptions()
  {
    // Traverse the list of subscriptions and archive(save) these to
    // the database
    try
    {
      foreach (RSSFeed feed in feeds)
      {
        // Clear existing data by deleting the Feed and letting the cascade delete
        // function within the database, delete its dependant items
        if (feed.ID != 0)
          ClearFeedFromDatabase(feed);

        // Load the feed items from the Internet
        InitialiseCurrentChannel(feed.feedURL);      

        // Write the feed
        feedEntity.WriteSingleFeed(feed);
      }    
    }
    catch
    {
      // Ignore errors, it simply means the database will not be up to date
    }
  }

  // InitialiseCurrentChannel - Initialise the current visible channel
  //
  private void InitialiseCurrentChannel(string feedName)
  {  
    try
    {
      // Set the current channel
      if (currentChannel == null)
        currentChannel = new Channel();
      // Initialise from the appropriate source
      currentChannel.InitialiseFromURL(feedName);      

      // Set the Feeds Image properties, both the Image itself and the Title,
      // also, Scale image to that of the original
      try
      {
        // Load the Image (stream it into a bitmap using tge GetImageFromURL() function)
        pctImage.Image = GetImageFromURL(currentChannel.channelImage.channelImageEntity.url);
        // Size the Image picture
        pctImage.Size = new Size(pctImage.Image.Width, pctImage.Image.Height);
        // Set and Size the Image title
        lblImage.Text = currentChannel.channelImage.channelImageEntity.title;
        lblImage.Left = pctImage.Left + pctImage.Width + 8;
        lblImage.Top = pctImage.Top;
        // Set so that you can see them
        pctImage.Visible = true;
        lblImage.Visible = true;
      }
      catch
      {
        // Error loading the Feed image, hide it
        pctImage.Visible = false;
        lblImage.Visible = false;
      }
      
      // Scale the height of the feeds    
      ScaleThreadsHeight();
    }
    catch(Exception e)
    {
      MessageBox.Show("Error [" + e.Message + "] trying to initialise the Feed", "Error");
    }
  }

  // ScaleThreadsHeight - Scale the height of the Threads DataGrid so that the Feed Image and its
  //                      title can clearly be seen.
  //
  private void ScaleThreadsHeight()
  {
    // If its visible, scale around it
    if (pctImage.Visible == true)
    {
      dgThreads.Height = pnlThreads.Height - pctImage.Height - 8*3;
      dgThreads.Top = pctImage.Top + pctImage.Height + 8;
      dgThreads.Width = pnlThreads.Width-16;
    }
    else
    {
      // Invisible means we can use the full space
      dgThreads.Height = pnlThreads.Height - 8*2;
      dgThreads.Top = 8;
      dgThreads.Width = pnlThreads.Width-16;
    }
  }

  // ClearFeedFromDatabase - Remove the feed from the database, ready for a refresh (this is faster
  //                          than trying to consolidate the data - and easier!)
  //
  private void ClearFeedFromDatabase(RSSFeed feed)
  {
    // Delete the Feed item, ensuring that the cascade delete exists in the database
    bool bDeleted = feedEntity.DeleteFeed(feed.ID);
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