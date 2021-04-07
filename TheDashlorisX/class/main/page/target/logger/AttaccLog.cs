
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class AttaccLog
    { 
	public readonly SpruceLog SprucyLog = new SpruceLog();

	public void CommenceLaunch(LockOn S3Class1, Settings S3Class2, DashWindow DashWindow, PictureBox Capsule)
	{
	    try
	    {
		new Thread(() =>
		{
		    if (SprucyLog.RequiresInit)
		    {
			void Invoker()
			{
			    try
			    {
				SprucyLog.InitializePage(DashWindow, Capsule);
			    }

			    catch (Exception E)
			    {
				throw (ErrorHandler.GetException(E));
			    }
			}

			Capsule.Invoke(new MethodInvoker(Invoker));
		    }

		    foreach (Control Control in Capsule.Controls)
		    {
			if (Control.Visible)
			{
			    Control.Hide();
			}
		    }

		    SprucyLog.Show();
		})

		{ IsBackground = true }.Start();
		
		(string, int, int, string) ParseSect1()
		{
		    return ("8.8.8.8", 80, 2000, "1.0");
		}
		
		(int, int, int, int, int, bool) ParseSect2()
		{
		    return (200, 500, 8, 16, 5012, true);
		}

		(List<string>, List<string>) ParseSect3()
		{
		    return (new List<string>() { "172.0.0.1" }, new List<string>() { "somepass" });
		}

		/*
		 * Sources: S3Class1, S3Class2
		 * 
		 * Method 1:
		 * Returns a tuple with parsed Host, Port, Duration and HTTPv.
		 * 
		 * Method 2:
		 * Returns a tuple with parsed SendDelay, Timeout, DashWorkers,
		 * Max Connections, Content Length and UAR.
		 * 
		 * Method 3:
		 * Returns a tuple with a list of proxies and credentials for these
		 * potential proxies.
		 */
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
