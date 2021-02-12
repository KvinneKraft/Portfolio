
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Management;
using System.Net.Sockets;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DNSChangerX
{
    public class SpoofDns
    {
	private void ChangeIPv4(string ip1, string ip2)
	{
	    try
	    {
		// Change to Ipv4
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void ChangeIPv6(string ip1, string ip2)
	{
	    try
	    {
		// Change to IPv6
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void ChangeDns(PictureBox Checkbox1, PictureBox Checkbox2, string ip1, string ip2)
	{
	    try
	    {
		if (Checkbox2.BackColor == Initialize.CheckEnable)
		{
		    ChangeIPv6(ip1, ip2);
		}

		else
		{
		    ChangeIPv4(ip1, ip2);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
