Instructions

1. Ensure that you have installed the latest version of Mono for Linux (Fedora 3)
   and that you have a search path to the Mono binaries - this should be
   automatic, if you used the installer.

2. Ensure that the source code and the build* batch files are in the same
   directory and that the build* files have execute permissions

3. Execute the following files in order

   ./build_rssnetwork.bat
   ./build_rssdata.bat
   ./build_rssaggregator.bat

   This will build the dependant assemblies and the final executable binary
   called RSSAggregator.exe

4. Ensure that you have MySQL installed and you have created a database using
   the database script provided. You'll also need ODBC for Linux installed but
   this is an option in package manager for most Linux distributions.

5. Ensure that you have an ODBC Data Source called 'rssaggregator'and using 
   the MySQL ODBC Database driver. This should use a username and password
   as chosen by yourself. In my case i left it at 'sa' with a password of
   'password' - imaginative i know

   NOTE: If you don't do this, the application will still run but will not
   store the feeds list to the database for later use

6. Execute the application using the 'mono' runtime, followed by the
   application name
