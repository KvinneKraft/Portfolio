// Author: Dashie
// Version: 1.0
// Dash Shell

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Networking;
using DashFramework.Runnables;
using DashFramework.Erroring;
using DashFramework.Dialog;


namespace GateHey
{
    public class Dialog2
    {
	public readonly static DashControls Controls = new DashControls();
	public readonly static DashTools Tools = new DashTools();


	class InitiateTop
	{
	    public void Initiate(DashWindow Parent, DashWindow Inst, Dialog2 This)
	    {
		try
		{
		    Color barBCol = Inst.values.getBarColor();
		    Size diagSize = new Size(425, 285);
		    Color diagBCol = Inst.BackColor;

		    string diagTitle = ("Dash - Shell");

		    Parent.InitializeWindow(diagSize, diagTitle, diagBCol, barBCol, roundRadius: 0);

		    Parent.values.onControlClick(1, () => 
		    {
			This.StopScan();
			This.Hide();
		    });

		    Parent.values.setTitleLocation(new Point(10, -2));
		    Parent.values.HideIcons();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	class InitiateBottom
	{
	    /*
	     Release 2.0 Update Notes for Future Dashie:

		- Make sure to replace this interactive shell with the one you put into 
		  DashFramework.cs (unless you changed the name in the meanwhile-).

		- Create a separate file for the hook registration, or simply make use of 
		  the planned AddCommand integration.

		- Add some event manipulation.
		 */

	    Dictionary<string, CommandHandler> RunnableCache = new Dictionary<string, CommandHandler>();
	    delegate void CommandHandler();

	    void TextBoxHook(Dialog2 This, KeyEventArgs e)
	    {
		if (e.KeyCode == Keys.Enter)
		{
		    string[] cmd = TxtBox.Text.ToLower().Split(' ');

		    if (RunnableCache.ContainsKey(cmd[0]))
		    {
			try
			{
			    RunnableCache[cmd[0]]();
			}

			catch (Exception E)
			{
			    This.SendMessage("An exception has occurred internally making me unable to fullfill your needs.");
			    This.SendMessage($"Debugger Information: ErrorCode[{E.Message}]");
			}

			return;
		    }

		    This.SendMessage("That command cannot be found.  Perhaps try typing help.");
		}
	    }

	    
	    void InvokeSafely(CommandHandler cmd, string type)
	    {
		try
		{
		    cmd();
		}

		catch (Exception E)
		{
		    throw new Exception($"{type}");
		}
	    }


	    void RegisterCommandHooks()
	    {
		try
		{
		    Tools.SortCode(("SET Commands"), () =>
		    {
			RunnableCache.Add("fcol", () => 
			{
			    InvokeSafely(() =>
			    {

			    }, "fcol");
			});

			RunnableCache.Add("bcol", () =>
			{
			    InvokeSafely(() =>
			    {

			    }, "fcol");
			});

			// fcol, bcol
		    });

		    Tools.SortCode(("My Commands"), () =>
		    {
			RunnableCache.Add("website", () => 
			    InvokeSafely(() => Tools.
				OpenUrl("https://pugpawz.com"), 
				    "website"));

			RunnableCache.Add("youtube", () =>
			    InvokeSafely(() => Tools.OpenUrl("https://www.youtube.com/channel/UCODilr1GUANP7i1TvEkjsAQ"),
				    "youtube"));

			RunnableCache.Add("website", () => 
			    InvokeSafely(() => Tools.
				OpenUrl("https://pugpawz.com"), 
				    "website"));
		    });

		    Tools.SortCode(("Util Commands"), () =>
		    {
			// start, stop, reboot (application), close
			// (application), clear, help, savelog
		    });
		}

		catch (Exception E)
		{
		    throw new Exception($"{E.Message}");
		}
	    }


	    readonly public TextBox TxtBox = new TextBox();
	    readonly DashPanel Panel = new DashPanel();
	    readonly Label Lbl = new Label();

	    public void Initiate(DashWindow Parent, DashWindow Inst, Dialog2 This)
	    {
		try
		{
		    Tools.SortCode(("Control Adding"), () =>
		    {
			var PanelLoca = new Point(0, Parent.Height - 28);
			var PanelBCol = Parent.values.getBarColor();
			var PanelSize = new Size(Parent.Width, 26);

			var LblSize = Tools.GetFontSize("$:", Id: 0);
			var LblLoca = new Point(0, 7);
			var LblFCol = Color.White;
			var LblBCol = PanelBCol;

			var TxtSize = new Size(PanelSize.Width - LblSize.Width - 3, 24);
			var TxtLoca = new Point(LblSize.Width, 2);
			var TxtFCol = Color.White;
			var TxtBCol = PanelBCol;

			Controls.Label(Panel, Lbl, LblSize, LblLoca, LblBCol, LblFCol, ("$:"), 0, 10);
			Controls.TextBox(Panel, TxtBox, TxtSize, TxtLoca, TxtBCol, TxtFCol, 1, 9);
			Controls.Panel(Parent, Panel, PanelSize, PanelLoca, PanelBCol);
		    });

		    Tools.SortCode(("Last Touches"), () =>
		    {
			TxtBox.KeyDown += (s, e) => TextBoxHook(This, e);
			TxtBox.Text = ("help");
		    });

		    RegisterCommandHooks();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	public class DashShell
	{
	    public TextBox TerminalLog = new TextBox();
	    
	    public void SetDefaultText()
	    {
		try
		{
		    TerminalLog.Text = string.Format(
			$">>> Hey there {Environment.UserName} fellow creature )o(\r\n" +
			">>> Type 'help' for help, thank you for using this project.\r\n\r\n"
		    );
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void OutputText(string msg, bool nl = true) =>
		TerminalLog.AppendText($"{msg}{(nl ? "\r\n" : "")}");

	    public TextBox GetTerminal() => TerminalLog;


	    public void SetTerminalColor(Color BCol, Color FCol)
	    {
		TerminalLog.BackColor = BCol;
		TerminalLog.ForeColor = FCol;
	    }
	}


	class InitiateMiddle
	{ 
	    readonly CustomScrollBar DashBar = new CustomScrollBar();
	    readonly public DashShell Shell = new DashShell();
	    readonly DashPanel Panel = new DashPanel();

	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    var PanelSize = new Size(Parent.Width - 4, Parent.Height - 54);
		    var PanelLoca = new Point(2, 26);
		    var PanelBCol = Color.FromArgb(22, 29, 36);

		    var TextBoxSize = new Size(PanelSize.Width - 10, PanelSize.Height - 10);
		    var TextBoxLoca = new Point(5, 5);
		    var TextBoxFCol = Color.White;
		    var TextBoxBCol = PanelBCol;

		    Controls.TextBox(Panel, Shell.TerminalLog, TextBoxSize, TextBoxLoca, TextBoxBCol, TextBoxFCol, 0, 9, true, true, true, false);
		    Controls.Panel(Parent, Panel, PanelSize, PanelLoca, PanelBCol);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	readonly InitiateBottom InitiateB = new InitiateBottom();
	readonly InitiateMiddle InitiateM = new InitiateMiddle();
	readonly InitiateTop InitiateT = new InitiateTop();

	readonly public DashWindow Parent = new DashWindow();

	public void Initiator(DashWindow Inst)
	{
	    try
	    {
		InitiateT.Initiate(Parent, Inst, this);
		InitiateB.Initiate(Parent, Inst, this);
		InitiateM.Initiate(Parent, Inst);

		this.Inst = Inst;
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}

	
	readonly DashNet DashNet = new DashNet();
	
	bool AttemptConnect(string host, int port, SocketType socketType, ProtocolType protocol, string packetData, int timeout)
	{
	    try
	    {
		return DashNet.IsHostReachable(host, port, socketType,
		    protocol, packetData, timeout);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	ProtocolType GetProtocol(string protocol)
	{
	    switch (protocol)
	    {
		case "ICP": return ProtocolType.Icmp;
		case "UDP": return ProtocolType.Udp;
		case "TCP": return ProtocolType.Tcp;
	    }

	    return ProtocolType.Tcp;
	}


	SocketType GetSocketType(string protocol)
	{
	    switch (GetProtocol(protocol))
	    {
		case ProtocolType.Tcp: return SocketType.Stream;
		case ProtocolType.Icmp: return SocketType.Raw;
		case ProtocolType.Udp: return SocketType.Raw;
	    }

	    return SocketType.Stream;
	}


	void SendMessage(string msg, bool nl = true, bool cv = false) =>
	    InitiateM.Shell.OutputText($"{(cv ? (Universal.DoVerbose ? msg : string.Empty) : msg)}", nl);


	readonly public List<int> SuccessfulConnections = new List<int>();
	readonly public List<int> FailedConnections = new List<int>();

	void ClearPortStatuses()
	{
	    SuccessfulConnections.Clear();
	    FailedConnections.Clear();
	}


	int lineCounter = 0;

	void AddPortStatus(int port, bool open = true, bool uline = false)
	{
	    if (open) SuccessfulConnections.Add(port);
	    else FailedConnections.Add(port);

	    SendMessage($"| {port} => {(open ? "open" : "closed")} ",
		nl: (!uline ? lineCounter == 3 : true), cv: !open);

	    lineCounter = (uline ? (lineCounter >= 3 ? lineCounter = 0 
		: lineCounter += 1) : lineCounter);
	}


	readonly Runnable Runnables = new Runnable();

	public void RunScan(MainGUI.Initiator2 MainSettings)
	{
	    try
	    {
		Tools.SortCode(("Window Visibility Section"), () =>
		{
		    InitiateM.Shell.SetDefaultText();

		    MainSettings.Dialog2.Parent.Show();
		    MainSettings.Dialog2.Parent.BringToFront();
		    MainSettings.Dialog2.Parent.Focus();

		    ClearPortStatuses();
		});
		
		Runnables.RunTaskAsynchronously(null, () => 
		{
		    Tools.SortCode(("Scan Section"), () =>
		    {
			SendMessage($"> GateHey scan session started at: {Tools.GetCurrentTime()}");

			// For threading; only allow this when either a range or a big selection of ports has been selected.
			// The amount of selected ports should be at least equal to the amount of threads, if not disable feature.
			string host = DashNet.GetIP(MainSettings.GetComponentValues()["host"]);
			string packetData = MainSettings.GetComponentValues()["packdata"];
			string protocol = MainSettings.GetComponentValues()["protocol"];

			bool sendPacketData = (packetData.Length < 1 || packetData.Equals("none"));

			int threads = int.Parse(MainSettings.GetComponentValues()["threads"]);
			int timeout = int.Parse(MainSettings.GetComponentValues()["timeout"]);

			Tools.SortCode(("Port Scanning"), () =>
			{
			    SendMessage($"> Scanning host: {host} using {protocol} with {threads} threads and a " +
				$"timeout of {timeout} in miliseconds ...");

			    int ScanType = Universal.ScanType;

			    Universal.ToggleScanner();
			    Thread.Sleep(1000);

			    if (ScanType == 1) //Single
			    {
				int port = Universal.Ports[0];

				AddPortStatus(port, AttemptConnect(host, port, GetSocketType(protocol), 
				    GetProtocol(protocol), packetData, timeout));
			    }

			    else if (ScanType == 2) // Multi
			    {
				foreach (int port in Universal.Ports)
				{
				    if (!Universal.IsScanning())
				    {
					break;
				    }

				    AddPortStatus(port, AttemptConnect(host, port, GetSocketType(protocol),
					GetProtocol(protocol), packetData, timeout));
				}
			    }

			    else // Ranged
			    {
				for (int port = Universal.Ports[0]; port <= Universal.Ports[1]; port += 1)
				{
				    if (!Universal.IsScanning())
				    {
					break;
				    }

				    AddPortStatus(port, AttemptConnect(host, port, GetSocketType(protocol),
					GetProtocol(protocol), packetData, timeout));
				}
			    }

			    SendMessage($"\r\n> GateHey scan session has been finished successfully at: " +
				$"{Tools.GetCurrentTime()}.");
			});
		    });
		});
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	
	public void StopScan()
	{
	    try
	    {
		SendMessage($"> GateHey scan session has been canceled successfully telling " + 
		    "all workers (if any-) to stop working ...");

		Runnables.RunTaskAsynchronously(null, () =>
		{
		    Universal.ToggleScanner();
		    Thread.Sleep(3500);

		    SendMessage("> You are now free to launch another scan.");
		});
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly DashLink Linker = new DashLink();
	public DashWindow Inst = null;

	public void Show()
	{
	    Parent.Show();
	    Linker.CenterDialog(Parent, Inst);
	}


	public void Hide() => Parent.Hide();
    }
}