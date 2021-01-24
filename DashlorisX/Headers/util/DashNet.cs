// Author: Dashie
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
	public bool IsAllowedDomain(string url)
	{
	    bool IsValid = (!url.Contains(".gov") && !url.Contains(".edu") && !url.Contains(".govt"));

	    if (!IsValid)
	    {
		MessageBox.Show("The domain specified is blacklisted!", "Domain Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
			MessageBox.Show("The host specified is not an ipv4 and neither a valid http/https/www url.  Please correct this and then retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return string.Empty;
		    };

		    try
		    {
			r_host = Dns.GetHostAddresses(bacon.Host)[0].ToString();
		    }

		    catch
		    {
			MessageBox.Show("The domain specified does not resolve to a valid ipv4 address.  Please correct this and then retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return string.Empty;
		    }
		}

		else
		{
		    r_host = ham.ToString();

		    if (ham.AddressFamily != AddressFamily.InterNetwork)
		    {
			MessageBox.Show("The host specified resolved to an invalid ipv4 address.  Please correct this and then retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return string.Empty;
		    }
		}

		if (r_host.Length < 7 || r_host == string.Empty)
		{
		    MessageBox.Show("The host specified is invalid.  Please correct this and then retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		    return string.Empty;
		}

		return r_host;
	    }

	    catch (Exception E)
	    {
		throw (E);
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

		if (result < 1 || result > 65535 || !isInteger)
		{
		    MessageBox.Show("The port specified was found to be invalid.\r\n\r\nNote: A valid port ranges from 1 to 65535.  If you did not know this then you probably should not be using this right now.\r\n\r\nPlease retry!", "Port Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		    result = -1;
		};

		return result;
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}
    }
}
