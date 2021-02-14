﻿
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
	private readonly DashBox DashBox = new DashBox();

	private void ShowDialog(string message, string title)
	{
	    try
	    {
		var ContainerBCol = Color.FromArgb(2, 55, 110);
		var MenuBarBCol = Color.FromArgb(14, 0, 57);
		var AppBCol = Color.FromArgb(36, 1, 112);

		DashBox.Show(message, title, AppBCol, MenuBarBCol, ContainerBCol, Color.White);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly DashNet DashNet = new DashNet();

	private readonly List<NetworkInterfaceType> NetworkInterfaceTypes = new List<NetworkInterfaceType>()
	{
   	    NetworkInterfaceType.GigabitEthernet,
	    NetworkInterfaceType.Wireless80211,
	    NetworkInterfaceType.GenericModem,
	    NetworkInterfaceType.Ethernet
	};

	private bool ValidateNetworkInterface(NetworkInterface NetworkInterface)
	{
	    return NetworkInterface.GetIPProperties().GatewayAddresses.Any
	    (
		b =>
		(
		    b.Address.AddressFamily == AddressFamily.InterNetworkV6 ||
		    b.Address.AddressFamily == AddressFamily.InterNetwork
		)
	    );
	}

	private List<NetworkInterface> GetCurrentNetworkInterface()
	{
	    try
	    {
		var NetworkInterfaceList = new List<NetworkInterface>();

		foreach (NetworkInterface NetworkInterface in NetworkInterface.GetAllNetworkInterfaces())
		{
		    if (NetworkInterface.OperationalStatus == OperationalStatus.Up)
		    {
			if (NetworkInterfaceTypes.Contains(NetworkInterface.NetworkInterfaceType))
			{
			    if (ValidateNetworkInterface(NetworkInterface))
			    {
				NetworkInterfaceList.Add(NetworkInterface);
			    }
			}
		    }
		}
		
		return NetworkInterfaceList;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	
	private bool IsIPv6(NetworkInterface NetworkInterface)
	{
	    try
	    {
		return NetworkInterface.GetIPProperties().GatewayAddresses.Any
		(
		    b => b.Address.AddressFamily == AddressFamily.InterNetworkV6
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

	private bool ChangeIPv4(string ip1, string ip2)
	{
	    try
	    {
		var NetworkInterfaces = GetCurrentNetworkInterface();

		if (NetworkInterfaces.Count < 1)
		{
		    ShowDialog("There was no active usable interface found to change the DNS server(s) of.\r\n\r\nI need an interface to change the DNS of else this functionality can not be used.", "Interface Detection Error");
		    return false;
		}

		ManagementObjectCollection ObjectMCollection = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();

		string[] Dns = GetDnsNs(ip1, ip2).ToArray();
		int Servers = 0;

		foreach (var NetworkInterface in NetworkInterfaces)
		{
		    if (!IsIPv6(NetworkInterface))
		    {
			foreach (ManagementObject Object in ObjectMCollection)
			{
			    if (Object["Description"].ToString() == NetworkInterface.Description)
			    {
				if ((bool)Object["IPEnabled"])
				{
				    ManagementBaseObject BaseObject = Object.GetMethodParameters("SetDNSServerSearchOrder");

				    if (BaseObject != null)
				    {
					BaseObject["DNSServerSearchOrder"] = Dns;
					Object.InvokeMethod("SetDNSServerSearchOrder", BaseObject, null);

					Servers += 1;
				    }
				}
			    }
			}
		    }
		}

		if (Servers == 0)
		{
		    ShowDialog("No applicable network adapters were found.\r\n\r\nThis has caused the application to be unable to set the valid DNS server(s).", "Error While Setting DNS");
		    return false;
		}

		return true;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private bool ChangeIPv6(string ip1, string ip2)
	{
	    try
	    {
		return true;
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
		if (DashNet.ConfirmIP(ip1))
		{
		    bool Success = false;

		    if (Checkbox2.BackColor == Initialize.CheckEnable)
		    {
			Success = ChangeIPv6(ip1, ip2);
		    }

		    else
		    {
			Success = ChangeIPv4(ip1, ip2);
		    }

		    if (Success)
		    {
			ShowDialog("You have successfully set your new DNS servers!", "Success!");
		    }
		}

		else
		{
		    ShowDialog("The server requested as your potentially new primary DNS server could not be validated as correct.\r\n\r\nI need you to make sure that the IP given is valid and can actually be used.", "IP Specification Error");
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
