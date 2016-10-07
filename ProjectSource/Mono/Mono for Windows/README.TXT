Instructions

1. Ensure that you have installed the latest version of Mono for Windows
   and that your PATH environment variable has a pointed to the \bin
   directory for the Mono binaries

2. Ensure that the source code and the build.bat batch file are in the
   same directory

3. Execute build.bat, this will compile and build the RSSAggregator project

4. Ensure that you have MySQL installed and you have created a database using
   the database script provided

5. Ensure that you have an ODBC Data Source called 'rssaggregator'and using 
   the MySQL ODBC Database driver. This should use a username and password
   as chosen by yourself. In my case i left it at 'sa' with a password of
   'password' - imaginative i know

   NOTE: If you don't do this, the application will still run but will not
   store the feeds list to the database for later use

6. Execute the application using the 'mono' runtime, followed by the
   application name
