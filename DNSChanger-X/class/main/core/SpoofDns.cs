
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
	private NetworkInterface GetCurrentNetworkInterface()
	{
	    try
	    {
		// Long ass line of code...
		return NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault
		(
		    a => a.OperationalStatus == OperationalStatus.Up && 
		    (
			a.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
			a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || 
			a.NetworkInterfaceType == NetworkInterfaceType.Ethernet
		    )
		    
		    && 
		    
		    a.GetIPProperties().GatewayAddresses.Any
		    (
			b => b.Address.AddressFamily.ToString() == "InterNetwork"
		    )
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private List<string> GetDnsNs(string ip1, string ip2)
	{
	    var DnsNs = new List<string>() { ip1 };

	    if (ip2.Length > 6)
	    {
		DnsNs.Add(ip2);
	    }

	    return DnsNs;
	}

	/*
    string[] Dns = { DnsString };
    var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
    if (CurrentInterface == null) return;

    ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
    ManagementObjectCollection objMOC = objMC.GetInstances();
    foreach (ManagementObject objMO in objMOC)
    {
        if ((bool)objMO["IPEnabled"])
        {
            if (objMO["Description"].ToString().Equals(CurrentInterface.Description))
            {
                ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                if (objdns != null)
                {
                    objdns["DNSServerSearchOrder"] = Dns;
                    objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                }
            }
        }
    }	
	*/

	private void ChangeDnsNs(List<string> DnsNs)
	{
	    try
	    {
		string[] Dns = DnsNs.ToArray();


	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void ChangeIPv4(string ip1, string ip2)
	{
	    try
	    {
		ChangeDnsNs(GetDnsNs(ip1, ip2));
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


	private readonly DashBox DashBox = new DashBox();

	private void ErrorDialog(string message)
	{
	    try
	    {
		var ContainerBCol = Color.FromArgb(2, 55, 110);
		var MenuBarBCol = Color.FromArgb(14, 0, 57);
		var AppBCol = Color.FromArgb(36, 1, 112);
		
		DashBox.Show(message, "IP Specification Error", AppBCol, MenuBarBCol, ContainerBCol, Color.White);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly DashNet DashNet = new DashNet();

	public void ChangeDns(PictureBox Checkbox1, PictureBox Checkbox2, string ip1, string ip2)
	{
	    try
	    {
		if (DashNet.ConfirmIP(ip1))
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

		ErrorDialog("The server requested as your potentially new primary DNS server could not be validated as correct.\r\n\r\nI need you to make sure that the IP given is valid and can actually be used.");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
