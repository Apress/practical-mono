using System;
using System.Data;
using System.Data.Odbc;
using System.Collections;

namespace RSSAggreagatorData
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
        private Exception error;

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

        // Create – Create a database record
        //
        public void Create()
        {
            // TODO - Implementation
        }
/*
        // Read – Read one or more database records
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
*/

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
            // Close database connection
            //
//            Close();
            // Return the data
            //
            return rdr;

        }

        // Update – Update an existing database record
        //
        public void Update()
        {
            // TODO - Implementation
        }

        // Delete – Delete an existing database record
        //
        public void Delete()
        {
            // TODO - Implementation
        }
    }


    //////////////////////////////////////////////////////////
    //
    // Data Containers
    //
    //////////////////////////////////////////////////////////
    public struct RSSFeed
    {
        public int feedID;
        public string feedName;
        public string feedURL;

        public void Initialize()
        {
            feedID = -1;
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
        private DataSet data;
        protected DataAccessLayer dal;

        // Constructor
        //
        public DataEntity()
        {
             // Read the connection string from the Applications Central configuration file
             // but for the moment we'll hard code it below
             string conn = "DSN=rssaggregator;UID=root;PWD=mypassword";
             dal = new DataAccessLayer(conn);
        }
    }

    public class RSSFeedDataEntity : DataEntity
    {

        public RSSFeed ReadSingle(int ID)
        {
            RSSFeed feed;
	    OdbcDataReader dr = dal.ReadAsDataReader("SELECT * FROM feed WHERE FeedID=1");
            feed.feedID = dr.GetInt32(0);
            feed.feedName = dr.GetString(1);
            feed.feedURL = dr.GetString(2);
            return feed;
        }

        public ArrayList ReadAll()
        {
            ArrayList list = new ArrayList();            
            OdbcDataReader dr = dal.ReadAsDataReader("SELECT * FROM feed");
            do
            {
                RSSFeed feed = new RSSFeed();
                feed.feedID = dr.GetInt32(0);
                feed.feedName = dr.GetString(1);
                feed.feedURL = dr.GetString(2);
                list.Add(feed);
            } while (dr.Read());
            return list;
        }
    }

}


