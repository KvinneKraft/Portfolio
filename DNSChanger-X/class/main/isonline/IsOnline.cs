
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DNSChangerX
{
    public class IsOnline
    {
	private readonly DashNet DashNet = new DashNet();
	private readonly DashBox DashBox = new DashBox();

	public void HandleIsOnline(string ip1, string ip2, DashDialog DashDialog, PictureBox BottomContainer1)
	{
	    try
	    {
		if (ip1.Length > 6)
		{
		    DashBox.Show(GetReachabilityReport(ip1, ip2), "DNS Connectivity Check", DashDialog.BackColor, DashDialog.MenuBar.Bar.BackColor, BottomContainer1.BackColor, Color.White);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private string GetReachabilityReport(string ip1, string ip2)
	{
	    try
	    {
		string report = ("The following results were returned after trying to connect to the given DNS server(s).\r\n\r\nPrimary DNS (d1:53) Status: r1");

		string primary = ip1;
		string replace = string.Empty;

		if (DashNet.ConfirmIP(primary))
		{
		    if (DashNet.IsOnline(primary, port: 53, timeout: 350))
		    {
			replace = ("Online!");
		    }

		    else
		    {
			replace = ("Offline!");
		    }
		}

		else
		{
		    replace = ("unknown address specified");
		}

		report = report.Replace("r1", replace).Replace("d1", primary);

		string secondary = ip2;

		if (secondary.Length > 6)
		{
		    report += "\r\nSecondary DNS (d2:53) Status: r2";

		    if (DashNet.ConfirmIP(secondary))
		    {
			if (DashNet.IsOnline(secondary, port: 53, timeout: 350))
			{
			    replace = ("Online!");
			}

			else
			{
			    replace = ("Offline!");
			}
		    }

		    else
		    {
			replace = ("unknown address specified");
		    }

		    report = report.Replace("r2", replace).Replace("d2", secondary);
		}

		return report;
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
		return string.Empty;
	    }
	}
    }
}
