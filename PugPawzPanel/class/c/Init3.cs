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
    public class Init3
    {
	private readonly string[] configFormat = new string[]
	{
	    @"sql-host=1.1.1.1", @"sql-port=1433", @"sql-username=sqluser",  @"sql-password=sqlpassword",
	    @"insertion-database=mydatabase", @"downloads-table=downloads", @"blog-table=blog",  @"website-control-url=https://mywebsite.com:2021",
	    @"filezilla-location=C:\Program Files\FileZilla FTP Client\filezilla.exe",
	};

	public void CreateDefaultConfigFile()
	{
	    try
	    {
		if (!File.Exists("config.txt"))
		{
		    using (File.Create("config.txt"))
		    {
			File.WriteAllLines("config.txt", configFormat);
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly Dictionary<Entry, string> Entries = new Dictionary<Entry, string>()
	{
	    { Entry.WebsiteControlUrl, "website-control-url" },
	    { Entry.InsertionDatabase, "insertion-database" },
	    { Entry.FilezillaLocation, "filezilla-location" },
	    { Entry.DownloadsTable, "downloads-table" },
	    { Entry.SqlUsername, "sql-username" },
	    { Entry.SqlPassword, "sql-password" },
	    { Entry.BlogTable, "blog-table" },
	    { Entry.SqlHost, "sql-host" },
	    { Entry.SqlPort, "sql-port" },
	};

	public enum Entry
	{
	    FilezillaLocation = 8, WebsiteControlUrl = 7, InsertionDatabase = 4,
	    DownloadsTable = 5, SqlUsername = 2, SqlPassword = 3, BlogTable = 6,
	    SqlPort = 1, SqlHost = 0,
	};


	readonly List<string> Settings = new List<string>();

	public void ReadConfig()
	{
	    try
	    {
		var entries = File.ReadAllLines("config.txt").ToList();

		if (entries.Count != configFormat.Length)
		{
		    // Come up with a way to handle these errors.
		    return;
		}

		for (int k = 0; k < Entries.Count; k += 1)
		{
		    for (int s = 0; s < entries.Count; k += 1)
		    {
			string[] arr = entries[s].Split('=');

			if (!Entries.ContainsValue(arr[0]) || arr.Length < 1)
			{
			    // Error cuz entries does not contain entry we have here.
			    return;
			}
		    }
		}

		//--(1)
		// Check if every entry exists. [Done!]
		//
		//--(2)
		// Check each entry, use a universal parser for validation of 
		//  datatypes and what not.  getValue(Type type, string Data) 
		//  returns object as object.
		//
		//--(3)
		// Make sure you can loop through the list and do it all automatically
		//  in an efficient type of way.
		//
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
