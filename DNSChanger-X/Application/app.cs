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

	private List<string> GetDomainNameServers(string ip1, string ip2)
	{
	    try
	    {
		var DnsNs = new List<string>() { ip1 };

		if (ip2.Length > 6)
		{
		    DnsNs.Add(ip2);
		}

		return DnsNs;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private bool ChangeIPv4(string ip1, string ip2)
	{
	    try
	    {
		List<NetworkInterface> NetworkInterfaces = GetCurrentNetworkInterface();
		List<string> DomainNameServer = GetDomainNameServers(ip1, ip2);

		if (NetworkInterfaces.Count < 1)
		{
		    ShowDialog("There was no active usable interface found to change the DNS server(s) of.\r\n\r\nI need an interface to change the DNS of else this functionality can not be used.", "Interface Detection Error");
		    return false;
		}

		ManagementObjectCollection ObjectMCollection = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();

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
					BaseObject["DNSServerSearchOrder"] = DomainNameServer.ToArray();
					Object.InvokeMethod("SetDNSServerSearchOrder", BaseObject, null);
					Servers = Servers + 1;
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
		List<NetworkInterface> NetworkInterfaces = GetCurrentNetworkInterface();
		List<string> DomainNameServer = GetDomainNameServers(ip1, ip2);

		if (NetworkInterfaces.Count < 1)
		{
		    ShowDialog("There was no active usable interface found to change the DNS server(s) of.\r\n\r\nI need an interface to change the DNS of else this functionality can not be used.", "Interface Detection Error");
		    return false;
		}

		List<string> Commands = new List<string>();

		Commands.Add("netsh interface ipv6 set dnsservers \"%name%\" static %ip% primary");

		if (DomainNameServer.Count > 1)
		{
		    Commands.Add("netsh interface ipv6 add dnsservers \"%name%\" %ip% index=2");
		}

		foreach (var NetworkInterface in NetworkInterfaces)
		{
		    if (IsIPv6(NetworkInterface))
		    {
			var InterfaceName = NetworkInterface.Name;

			foreach (var Command in Commands)
			{
			    using (Process Process = new Process())
			    {
				Process.StartInfo = new ProcessStartInfo()
				{
				    Arguments = $"{Command.Replace("%name%", InterfaceName).Replace("%ip%", DomainNameServer[Commands.IndexOf(Command)])}",
				    FileName = $@"{Environment.SystemDirectory}\netsh.exe",

				    UseShellExecute = true,
				    CreateNoWindow = true,
				};

				Process.Start();
			    }
			}
		    }
		}

		return true;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly DashNet DashNet = new DashNet();

	private bool IPIsCompatible(string ip1, string ip2, AddressFamily AddressFamily)
	{
	    return ();
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
			Success = ChangeIPv6(ip1, ip2);
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
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}