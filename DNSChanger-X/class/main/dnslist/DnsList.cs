
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
    public class DnsList
    {
	public void HandleDnsList()
	{
	    try
	    {
		using (Process Process = new Process())
		{
		    Process.StartInfo = new ProcessStartInfo()
		    {
			FileName = "https://public-dns.info/nameservers.txt",
			UseShellExecute = true,
		    };

		    Process.Start();
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
