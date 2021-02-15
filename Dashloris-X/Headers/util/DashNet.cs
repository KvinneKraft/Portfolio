﻿// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace DashlorisX
{
    public class DashNet
    {
	private readonly DashBox DashBox = new DashBox();

	private void ShowError(string message, string title)
	{
	    var ContainerBColor = Color.FromArgb(9, 39, 66);
	    var MenuBarBColor = Color.FromArgb(19, 36, 64);
	    var AppBColor = Color.FromArgb(6, 17, 33);

	    DashBox.Show(message, title, AppBColor, MenuBarBColor, ContainerBColor, Color.White);
	}

	public bool ConfirmDuration(string Value)
	{
	    int V = GetInteger(Value);

	    if (V == -1 || V < 10)
	    {
		ShowError("The duration specified was found to be invalid.\r\n\r\nThe value must be above 10 atleast!\r\n\r\nPlease correct this and then retry.", "Duration Error");
		return false;
	    }

	    return true;
	}

	public bool ConfirmBytes(string Value)
	{
	    int V = GetInteger(Value);

	    if (V == -1 || V < 10)
	    {
		ShowError("The bytes specified was found to be invalid.\r\n\r\nThe value must be above 10 atleast!\r\n\r\nPlease correct this and then retry.", "Byte Error");
		return false;
	    }

	    return true;
	}

	public AddressFamily GetAddressFamily(string host)
	{
	    try
	    {
		return (IPAddress.Parse(host).AddressFamily);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public bool IsOnline(string host, int port = 80, int timeout = 500)
	{
	    try
	    {
		var stocking = new Socket(GetAddressFamily(host), SocketType.Stream, ProtocolType.Tcp);

		var resu = stocking.BeginConnect(host, port, null, null);
		var succ = resu.AsyncWaitHandle.WaitOne(timeout, true);

		return stocking.Connected;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public bool IsAllowedDomain(string url)
	{
	    bool IsValid = (!url.Contains(".gov") && !url.Contains(".edu") && !url.Contains(".govt"));

	    if (!IsValid)
	    {
		ShowError("The domain specified is blacklisted!\r\n\r\nPlease specify a domain that is not blacklisted!", "Domain Restriction");
	    }

	    return IsValid;
	}

	public bool ConfirmInteger(string value)
	{
	    return (GetInteger(value) != -1);
	}

	public int GetInteger(string value)
	{
	    try
	    {
		return int.Parse(value);
	    }

	    catch
	    {
		return -1;
	    }
	}

	public bool ConfirmIP(string host)
	{
	    return (GetIP(host) != string.Empty);
	}

	public string GetIP(string host)
	{
	    try
	    {
		var r_host = host.ToLower();

		if (!IPAddress.TryParse(r_host, out IPAddress ham))
		{
		    if (!r_host.Contains("http://") && !r_host.Contains("https://"))
		    {
			r_host = "https://" + r_host;
		    }

		    if (!Uri.TryCreate(r_host, UriKind.RelativeOrAbsolute, out Uri bacon))
		    {
			ShowError("The host specified is not an ipv4 and neither a valid http/https/www url.\r\n\r\nPlease correct this and then retry!", "Host Address Parse Error");
			return string.Empty;
		    };

		    try
		    {
			r_host = Dns.GetHostAddresses(bacon.Host)[0].ToString();
		    }

		    catch
		    {
			ShowError("The domain specified does not resolve to a valid ipv4 address.\r\n\r\nPlease correct this and then retry!", "Host Address Parse Error");
			return string.Empty;
		    }
		}

		else
		{
		    r_host = ham.ToString();

		    if (ham.AddressFamily != AddressFamily.InterNetwork)
		    {
			ShowError("The host specified resolved to an invalid ipv4 address.\r\n\r\nPlease correct this and then retry!", "Host Address Parse Error");
			return string.Empty;
		    }
		}

		if (r_host.Length < 7 || r_host == string.Empty)
		{
		    ShowError("The host specified is invalid.\r\n\r\nPlease correct this and then retry!", "Host Address Parse Error");
		    return string.Empty;
		}

		return r_host;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public bool ConfirmPort(string port)
	{
	    return (GetPort(port) != -1);
	}

	public int GetPort(string port)
	{
	    try
	    {
		bool isInteger = int.TryParse(port, out int result);

		if (!isInteger || result < 1 || result > 65535)
		{
		    ShowError("The port specified was found to be invalid.\r\n\r\nNote: A valid port ranges from 1 to 65535.\r\n\r\nIf you did not know this then you probably should not be using this right now.\r\n\r\nPlease retry!", "Port Parse Error");
		    result = -1;
		};

		return result;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
