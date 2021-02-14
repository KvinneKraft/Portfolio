
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

	private bool IsIPAddressFamily(AddressFamily AddressFamily, string dns)
	{
	    try
	    {
		return
		(
		    IPAddress.Parse(dns).AddressFamily == AddressFamily
		);
	    }

	    catch
	    {
		return false;
	    }
	}

	private void ShowIPvError()
	{
	    try
	    {
		ShowDialog("One of the given DNS addresses is incompatible with the set IP version.\r\n\r\nPlease make sure you only use IPv4 or IPv6 whenever you are sure about the version your DNS address is.\r\n\r\nFuture functionality promises automatic detection.", "Invalid IP Version");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void ShowInterfaceLackError()
	{
	    try
	    {
		ShowDialog("It appears no usable interfaces were found.\r\n\r\nPerhaps try switching between ip versions to see which works best.\r\n\r\nPerhaps your system does not support the set ip version.", "Insufficient Interfaces Found");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void ShowSuccessMessage()
	{
	    try
	    {
		ShowDialog("The specified DNS has been set.\r\n\r\nYou may have to restart your system depending on your setup.\r\n\r\nIf it did not get set after that though, please reach out to me at KvinneKraft@protonmail.com.", "Execution Success");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly List<NetworkInterfaceType> NetworkInterfaceTypes = new List<NetworkInterfaceType>()
	{
   	    NetworkInterfaceType.GigabitEthernet, NetworkInterfaceType.Wireless80211,
	    NetworkInterfaceType.GenericModem, NetworkInterfaceType.Ethernet
	};

	private List<NetworkInterface> GetNetworkInterfaces(AddressFamily AddressFamily)
	{
	    try
	    {
		var NetworkInterfaces = new List<NetworkInterface>();

		foreach (var NetworkInterface in NetworkInterface.GetAllNetworkInterfaces())
		{
		    if (NetworkInterface.OperationalStatus.Equals(OperationalStatus.Up))
		    {
			if (NetworkInterfaceTypes.Contains(NetworkInterface.NetworkInterfaceType))
			{
			    if (NetworkInterface.GetIPProperties().GatewayAddresses.Any
			    (
				b =>
				(
				    b.Address.AddressFamily.Equals(AddressFamily)
				)
			    ))

			    {
				NetworkInterfaces.Add(NetworkInterface);
			    }
			}
		    }
		}

		return NetworkInterfaces;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void ChangeDNSToIPv4(List<string> dns)
	{
	    try
	    {
		List<NetworkInterface> NetworkInterfaceCollection = GetNetworkInterfaces(AddressFamily.InterNetwork);

		if (NetworkInterfaceCollection.Count < 1)
		{
		    ShowInterfaceLackError();
		    return;
		}

		ManagementObjectCollection MObjectCollection = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();

		int ModifiedInterfaces = 0;

		foreach (NetworkInterface NetworkInterface in NetworkInterfaceCollection)
		{
		    foreach (ManagementObject MObject in MObjectCollection)
		    {
			if (MObject["Description"].ToString() == NetworkInterface.Description)
			{
			    if ((bool) MObject["IPEnabled"])
			    {
				ManagementBaseObject MBaseObject = MObject.GetMethodParameters("SetDNSServerSearchOrder");

				if (MBaseObject != null)
				{
				    MBaseObject["DNSServerSearchOrder"] = (dns.ToArray());
				    MObject.InvokeMethod("SetDNSServerSearchOrder", MBaseObject, null);

				    ModifiedInterfaces += 1;
				}
			    }
			}
		    }
		}

		if (ModifiedInterfaces == 0)
		{
		    ShowDialog("No applicable network adapters were found.\r\n\r\nThis has caused the application to be unable to set the specified secondary and or primary DNS.\r\n\r\nPlease make sure that atleast one network adapter is compatible with either IPv4 or IPv6 depending on your desired needs.", "No DNS Set!");
		}

		else
		{
		    ShowSuccessMessage();
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void ChangeDNSToIPv6(List<string> dns)
	{
	    try
	    {
		List<NetworkInterface> NetworkInterfaceCollection = GetNetworkInterfaces(AddressFamily.InterNetwork);

		if (NetworkInterfaceCollection.Count < 1)
		{
		    ShowInterfaceLackError();
		    return;
		}

		string[] WindowsCommands = new string[2];

		WindowsCommands[0] = ("interface ipv6 set dnsservers \"%name%\" static %ip% primary");

		if (dns.Count > 0)
		{
		    WindowsCommands[1] = ("interface ipv6 add dnsservers \"%name%\" %ip% index=2");
		}

		string FilePath = ($@"{Environment.SystemDirectory}\netsh.exe");

		for (int k = 0; k < NetworkInterfaceCollection.Count; k += 1)
		{
		    string InterfaceName = NetworkInterfaceCollection[k].Description; //Name;

		    foreach (string Argument in WindowsCommands)
		    {
			using (Process Process = new Process())
			{
			    Process.StartInfo.Arguments = ($"{Argument.Replace("%name%", InterfaceName).Replace("%ip%", dns[WindowsCommands.ToList().IndexOf(Argument)])}");
			    Process.StartInfo.FileName = (FilePath);

			    MessageBox.Show(Process.StartInfo.Arguments);

			    Process.StartInfo.UseShellExecute = true;
			    Process.StartInfo.CreateNoWindow = true;

			    Process.Start();
			}
		    }
		}
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

		foreach (string dns in DomainNameServers)
		{
		    if (!checkbox.BackColor.Equals(Initialize.CheckEnable))
		    {
			if (!IsIPAddressFamily(AddressFamily.InterNetworkV6, dns))
			{
			    ShowIPvError();
			    return;
			}
		    }

		    else
		    {
			if (!IsIPAddressFamily(AddressFamily.InterNetwork, dns))
			{
			    ShowIPvError();
			    return;
			}
		    }
		}

		if (checkbox.BackColor.Equals(Initialize.CheckEnable))
		{
		    ChangeDNSToIPv4(DomainNameServers);
		}

		else
		{
		    ChangeDNSToIPv6(DomainNameServers);
		}
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
