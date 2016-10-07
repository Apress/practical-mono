using System;
using System.Data;
using System.Data.Odbc;
using System.Collections;

namespace RSSAggregatorData
{
  //////////////////////////////////////////////////////////
  //
  // DataAccessLayer
  //
  //////////////////////////////////////////////////////////
  public class DataAccessLayer
  {
    // Members
    //
    private string connectionString;
    private OdbcConnection connection = null;

    // Constructor
    //
    public DataAccessLayer(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Open - Establish a database connection
    //
    public void Open()
    {
        connection = new OdbcConnection(this.connectionString);
        connection.Open();
    }

    // Close – Close a database connection
    //
    public void Close()
    {
        if ((connection != null) && (connection.State != ConnectionState.Closed))

            connection.Close();
    }

    public OdbcDataReader ReadAsDataReader(string sql)
    {
        OdbcDataReader rdr = null;

        // Establish a connection
        //
        Open();
        // Read the data
        //
        OdbcCommand cmd = new OdbcCommand(sql, this.connection);
        rdr = cmd.ExecuteReader();
        rdr.Read();
        // Return the data
        //
        return rdr;

    }

    // CreateDirectly – Create a database record
    //
    public int CreateDirectly(string sql)
    {
      OdbcCommand cmd = new OdbcCommand(sql, this.connection);
      return cmd.ExecuteNonQuery();
    }

    // UpdateDirectly – Update an existing database record
    //
    public int UpdateDirectly(string sql)
    {
      OdbcCommand cmd = new OdbcCommand(sql, this.connection);
      return cmd.ExecuteNonQuery();
    }

    // DeleteDirectly – Delete an existing database record
    //
    public int DeleteDirectly(string sql)
    {
      OdbcCommand cmd = new OdbcCommand(sql, this.connection);
      return cmd.ExecuteNonQuery();
    }

    // ReadAsDataSet – Read one or more database records and return the resulting dataset
    //
    public DataSet ReadAsDataSet(string sql)
    {
      // Establish a connection
      //
      Open();
      // Read the data
      //
      DataSet dataset = new DataSet();
      OdbcDataAdapter adapter = new OdbcDataAdapter();
      adapter.SelectCommand = new OdbcCommand(sql, this.connection);
      adapter.Fill(dataset);
      // Close database connection
      //
      Close();
      return dataset;
    }
  }


  //////////////////////////////////////////////////////////
  //
  // Data Containers
  //
  //////////////////////////////////////////////////////////
  public struct RSSFeed
  {
      public int ID;
      public string feedName;
      public string feedURL;

      public void Initialize()
      {
          ID = -1;
          feedName="";
          feedURL="";
      }
  }

  //////////////////////////////////////////////////////////
  //
  // Data Entity
  //
  //////////////////////////////////////////////////////////
  public abstract class DataEntity
  {
      // Members
      //
      protected DataAccessLayer dal;

      // Constructor
      //
      public DataEntity()
      {
            // Read the connection string from the Applications Central configuration file
            // but in our instance we'll hard code the value so you'll need to make sure
            // that this exists
            string conn = "DSN=rssaggregator;UID=root;PWD=password";
            dal = new DataAccessLayer(conn);
      }
  }

  public class RSSFeedDataEntity : DataEntity
  {
    // ReadSingle - Reads a single Feed item, identified by its ID
    //
    public RSSFeed ReadSingle(int ID)
    {
      RSSFeed feed;
	    OdbcDataReader dr = dal.ReadAsDataReader("SELECT FeedID, FeedURL, FeedName FROM feed WHERE FeedID="+ID.ToString());
      if (dr.HasRows)
      {
        feed.ID = dr.GetInt32(0);
        feed.feedName = dr.GetString(1);
        feed.feedURL = dr.GetString(2);        
      }
      else
      {
        feed.ID = 0;
        feed.feedName = "";
        feed.feedURL = "";
      }
      dr.Close();
      return feed;
    }

    // ReadAll - Reads all Feed Items
    //
    public ArrayList ReadAll()
    {
      ArrayList list = new ArrayList();            
      try
      {
        OdbcDataReader dr = dal.ReadAsDataReader("SELECT FeedID, FeedURL, FeedName FROM feed");
        if (dr.HasRows) // Must have returned some data
        {
          do
          {
            RSSFeed feed = new RSSFeed();
            feed.ID = dr.GetInt32(0);
            feed.feedName = dr.GetString(1);
            feed.feedURL = dr.GetString(2);
            list.Add(feed);
          } while (dr.Read());
        }
        dr.Close();
      }
      catch
      {
        // We'll just leave the array empty, but trap the exception
      }
      return list;
    }

    // CreateDirectly() - Creates a database row using the Direct SQL method
    //
    public bool CreateDirectly(int ID, string name, string url)
    {
      string sqlINSERT = "INSERT INTO Feed(FeedID, FeedName, FeedURL) VALUES({0},'{1}','{2}')";
      sqlINSERT = String.Format(sqlINSERT,ID,name,url);

      // Execute the command and return true if a row was inserted
      return (dal.CreateDirectly(sqlINSERT) == 1);
    }

    // UpdateDirectly() - Updates a database row using the Direct SQL method
    //
    public bool UpdateDirectly(int ID, string name, string url)
    {
      string sqlUPDATE = "UPDATE Feed SET FeedName='{0}', FeedURL='{1}' WHERE FeedID={2}";            
      sqlUPDATE = String.Format(sqlUPDATE,name,url,ID);

      // Execute the command and return true if a row was updated
      return (dal.UpdateDirectly(sqlUPDATE) == 1);
    }

    // DeleteFeed - Delete the information relating to a feed
    //
    public bool DeleteFeed(int feedID)
    {
      // Execute the command and return true if more than row was deleted
      return (dal.DeleteDirectly("DELETE FROM Feed WHERE FeedID="+feedID.ToString()) == 1);
    }

    // WriteSingleFeed - Write a single RSSFeed item to the database
    //
    public void WriteSingleFeed(RSSFeed feed)
    {
      // Look to see if the feed exists, if it does, UPDATE it otherwise INSERT it
      RSSFeed rs = ReadSingle(feed.ID);
      if (rs.ID == 0)
        CreateDirectly(feed.ID, feed.feedName, feed.feedURL);
      else
        UpdateDirectly(feed.ID, feed.feedName, feed.feedURL);
    }

    // WriteChannelItems - Write collection of 'ChannelItemEntity' to th database
    //
    public void WriteChannelItems(ArrayList list)
    {
      for (int idx=0; idx<list.Count;idx++)
      {
        // Cast to its intended type
        ChannelItemEntity ci = (ChannelItemEntity)list[idx];
        // Write to the database
        // READER: An Exercise for you to do

      }
    }

    // ReadSingleToDataSet() - Read a single record from the database 
    //                                           into the embedded DataSet
    //
    public bool ReadSingleToDataSet(int ID)
    {
      DataSet data = dal.ReadAsDataSet("SELECT FeedID, FeedName, FeedURL FROM Feed WHERE FeedID="+ID.ToString());
      return (data != null);
    }

    // ReadAllToDataSet() - Read all  records from the database into the embedded DataSet
    //
    public bool ReadAllToDataSet()
    {
      DataSet data = dal.ReadAsDataSet("SELECT FeedID, FeedName, FeedURL FROM feed");
      return (data != null);
    }

  }
}


