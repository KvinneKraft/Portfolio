using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Runnables;
using DashFramework.Erroring;
using DashFramework.Dialog;

namespace DashApplication
{
    public class Init3Constants
    {
	public enum Entry
	{
	    FilezillaLocation = 0, WebsiteControlUrl, InsertionDatabase,
	    DownloadsTable, SqlUsername, SqlPassword, BlogTable, SqlHost,
	    SqlPort,
	};

	readonly Dictionary<Entry, string> Entries = new Dictionary<Entry, string>()
	{
	    { Entry.WebsiteControlUrl, "website-control-url=" },
	    { Entry.InsertionDatabase, "insertion-database=" },
	    { Entry.FilezillaLocation, "filezilla-location=" },
	    { Entry.DownloadsTable, "downloads-table=" },
	    { Entry.SqlUsername, "sql-username=" },
	    { Entry.SqlPassword, "sql-password=" },
	    { Entry.BlogTable, "blog-table=" },
	    { Entry.SqlHost, "sql-host=" },
	    { Entry.SqlPort, "sql-port=" },
	};

	readonly string[] defaultSettingsFormat = new string[]
	{
	    @"sql-host=1.1.1.1",
	    @"sql-port=1433",
	    @"sql-username=sqluser",
	    @"sql-password=sqlpassword",
	    @"insertion-database=mydatabase",
	    @"downloads-table=downloads",
	    @"blog-table=blog",
	    @"filezilla-location=C:\Program Files\FileZilla FTP Client\filezilla.exe",
	    @"website-control-url=https://mywebsite.com:2021",
	};
    }
    

    public class Init3 : Init3Constants
    {
	readonly List<string> Settings = new List<string>();

	public void CreateDefaultConfigFile()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void SecondaryInitiate(DashWindow app, PictureBox frame)
	{
	    try
	    {
		CreateDefaultConfigFile();

		Runnable runnable = new Runnable();

		runnable.RunTaskLater
		(
		    app, 
		    
		    () => 
		    {
			MessageBox.Show("Test");
			//Load Configuration
			//Set values to temp variables
			//Update main values with temp variables every 500ms
		    }, 
		    
		    8000, true
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
