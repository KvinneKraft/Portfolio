
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
using System.Net.NetworkInformation;

namespace DNSChangerX
{
    public class SpoofDns
    {
	private readonly DashNet DashNet = new DashNet();
	private readonly DashBox DashBox = new DashBox();

	private void ShowDialog(string Message, string Title)
	{
	    try
	    {
		var ContainerBCol = Color.FromArgb(2, 55, 110);
		var MenuBarBCol = Color.FromArgb(14, 0, 57);
		var AppBCol = Color.FromArgb(36, 1, 112);

		DashBox.Show(Message, Title, AppBCol, MenuBarBCol, ContainerBCol, Color.White);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void ChangeDns(PictureBox checkbox, string dns1, string dns2)
	{
	    try
	    {
		List<string> DomainNameServers = new List<string>();

		DomainNameServers.Add(dns1);
		DomainNameServers.Add(dns2);

		for (int k = 0; k < DomainNameServers.Count; k += 1)
		{
		    if (k == 0)
		    {
			if (!DashNet.ConfirmIP(DomainNameServers[k]))
			{
			    ShowDialog("I was unable to resolve the given primary domain name server address.\r\n\r\nPlease make sure you have specified the right IP address.\r\n\r\nIt can be an IPv4 or IPv6 address, that is up to you, just make sure it is reachable and therefore, again, the right IP address.", "IP Resolve Error");
			    return;
			}

			continue;
		    }

		    if (DomainNameServers[k].Length > 0)
		    {
			if (!DashNet.ConfirmIP(DomainNameServers[k]))
			{
			    ShowDialog("I was unable to resolve the given secondary domain name server address.\r\n\r\nPlease make sure you have specified the right IP address.\r\n\r\nIt can be an IPv4 or IPv6 address, that is up to you, just make sure it is reachable and therefore, again, the right IP address.", "IP Resolve Error");
			    return;
			}

			continue;
		    }

		    DomainNameServers.RemoveAt(k);
		}
		
		if (!checkbox.BackColor.Equals(Initialize.CheckEnable))
		{
		    if (IsIPAddressFamily(AddressFamily.InterNetworkV6))
		    {

			return;
		    }
		}

		else
		{
		    if (IsIPAddressFamily(AddressFamily.InterNetwork))
		    {

		    }
		}
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
