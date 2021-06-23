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

		    string diagTitle = ("Dashie )O( Shell");

		    Parent.InitializeWindow(diagSize, diagTitle, diagBCol, 
			barBCol, roundRadius: 0, barClose: false);

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

	    readonly Dictionary<string, CommandHandler> RunnableCache = new Dictionary<string, CommandHandler>();
	    readonly Runnable Runnables = new Runnable();

	    delegate void CommandHandler(string[] cmds);
	    bool IsScanning() => Universal.IsScanning();

	    void TextBoxHook(Dialog2 This, KeyEventArgs e)
	    {
		if (e.KeyCode == Keys.Enter)
		{
		    string[] cmd = TxtBox.Text.ToLower().Split(' ');

		    if (RunnableCache.ContainsKey(cmd[0]))
		    {
			try
			{
			    TxtBox.Clear();
			    RunnableCache[cmd[0]](cmd);
			}

			catch (Exception E)
			{
			    This.SendMessage("An exception has occurred internally making me unable to fullfill your needs.", pnl: IsScanning());
			    This.SendMessage($"Debugger Information: ErrorCode[{E.Message}]");
			}

			return;
		    }

		    This.SendMessage("That command cannot be found.  Perhaps try typing help.", pnl: IsScanning());
		}
	    }
	    

	    Color RighteousColors(Dialog2 This, string[] cmds)
	    {
		try
		{
		    void SendErrorMessage()
		    {
			This.SendMessage(">> Usage: set (f/b)col [r,g,b]", pnl: IsScanning());
			This.SendMessage(">> Example: set bcol 1,1,1");
		    }

		    if (cmds.Length < 3)
		    {
			SendErrorMessage();
			return Color.Empty;
		    }

		    string[] colors = cmds[2].Split(',');

		    if (colors.Length != 3)
		    {
			SendErrorMessage();
			return Color.Empty;
		    }

		    int[] rgb = new int[3];

		    for (int k = 0; k < 3; k += 1)
		    {
			if (!int.TryParse(colors[k], out int r) || r > 255 || r < 0)
			{
			    SendErrorMessage();
			    return Color.Empty;
			}

			rgb[k] = r;
		    }

		    return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
		}

		catch
		{
		    throw new Exception("color codes");
		}
	    }


	    void RegisterCommandHooks(Dialog2 This, InitiateMiddle OptSet, MainGUI Main)
	    {
		try
		{
		    Tools.SortCode(("SET Commands"), () =>
		    {
			RunnableCache.Add("set", (string[] cmds) =>
			{
			    if (cmds.Length < 2)
			    {
				This.SendMessage(">> Usage: set [bcol | fcol] r,g,b", pnl: IsScanning());
				This.SendMessage(">> Example: set bcol 1,1,1");
				return;
			    }

			    else if (cmds[1].Equals("fcol"))
			    {
				Color ColorCode = RighteousColors(This, cmds);

				if (ColorCode == Color.Empty)
				    return;

				OptSet.Shell.TerminalLog.ForeColor = ColorCode;
				TxtBox.ForeColor = ColorCode;
				Lbl.ForeColor = ColorCode;

				This.SendMessage($@"> Set fore color to: " +
				    Tools.RGBString(ColorCode) + ".", pnl: IsScanning());
			    }

			    else if (cmds[1].Equals("bcol"))
			    {
				Color ColorCode = RighteousColors(This, cmds);

				if (ColorCode == Color.Empty)
				    return;

				OptSet.Shell.TerminalLog.BackColor = ColorCode;
				TxtBox.Parent.BackColor = ColorCode;
				Lbl.Parent.BackColor = ColorCode;
				TxtBox.BackColor = ColorCode;
				Lbl.BackColor = ColorCode;

				This.SendMessage($@"> Set back color to: " + Tools
				    .RGBString(ColorCode) + ".", pnl: IsScanning());
			    }
			});
		    });

		    Tools.SortCode(("My Commands"), () =>
		    {
			void OpenUrl(string url)
			{
			    try
			    {
				This.SendMessage($"> Opening the corresponding URL in your browser (if any) ...", 
				    pnl: IsScanning()); Tools.OpenUrl(url);
			    }

			    catch
			    {
				throw new Exception("urls");
			    }
			}

			RunnableCache.Add("youtube", (cmds) =>
			    OpenUrl("https://www.youtube.com/channel/UCODilr1GUANP7i1TvEkjsAQ"));

			RunnableCache.Add("github", (cmds) =>
			    OpenUrl("https://github.com/KvinneKraft"));

			RunnableCache.Add("website", (cmds) =>
			    OpenUrl("https://pugpawz.com"));
		    });

		    Tools.SortCode(("Util Commands"), () =>
		    {
			RunnableCache.Add("stop", (cmds) => 
			{
			    if (!IsScanning())
			    {
				This.SendMessage("> You are currently not scanning.  Try typing start instead.");
				return;
			    }

			    This.StopScan(Main.InitiateBottom);
			});

			RunnableCache.Add("start", (cmds) =>
			{
			    if (IsScanning())
			    {
				This.SendMessage("> Scan is already running.  Try typing stop instead.", pnl: true);
				return;
			    }

			    This.RunScan(Main);
			});

			// left off at reboot <----
			// start, stop, reboot (application), close
			// (application), clear, help, savelog, verbose
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

	    public void Initiate(DashWindow Parent, DashWindow Inst, InitiateMiddle OptSet, Dialog2 This, MainGUI Main)
	    {
		try
		{
		    Tools.SortCode(("Control Adding"), () =>
		    {
			var PanelLoca = new Point(2, Parent.Height - 28);
			var PanelBCol = Parent.values.getBarColor();
			var PanelSize = new Size(Parent.Width - 4, 26);

			var LblSize = Tools.GetFontSize("$:", Id: 0);
			var LblLoca = new Point(4, 7);
			var LblFCol = Color.White;
			var LblBCol = PanelBCol;

			var TxtSize = new Size(PanelSize.Width - LblSize.Width - 7, 24);
			var TxtLoca = new Point(LblSize.Width + 4, 2);
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

		    RegisterCommandHooks(This, OptSet, Main);
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
	    readonly public DashPanel Panel = new DashPanel();

	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    Tools.SortCode(("Panel"), () =>
		    {
			var PanelSize = new Size(Parent.Width - 4, Parent.Height - 53);
			var PanelLoca = new Point(2, 26);
			var PanelBCol = Color.FromArgb(22, 29, 36);

			Controls.Panel(Parent, Panel, PanelSize, PanelLoca, PanelBCol);
		    });

		    Tools.SortCode(("TextBox"), () =>
		    {
			var TextBoxSize = new Size(Panel.Width - 10, Panel.Height - 10);
			var TextBoxLoca = new Point(5, 5);
			var TextBoxFCol = Color.White;
			var TextBoxBCol = Panel.BackColor;

			Controls.TextBox(Panel, Shell.TerminalLog, TextBoxSize, TextBoxLoca,
			    TextBoxBCol, TextBoxFCol, 1, 9, true, true, true, false);
		    });
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

	public void Initiator(DashWindow Inst, MainGUI Main)
	{
	    try
	    {
		InitiateT.Initiate(Parent, Inst, this);
		InitiateB.Initiate(Parent, Inst, InitiateM, this, Main);
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


	void SendMessage(string msg, bool nl = true, bool cv = false, bool pnl = false) =>
	    InitiateM.Shell.OutputText($@"" + (pnl ? "\r\n" : "") + 
		(cv ? (Universal.DoVerbose ? msg : "") : msg), nl);


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
		nl: (!uline ? lineCounter == 4 : true), cv: !open);

	    lineCounter = (uline ? (lineCounter >= 4 ? lineCounter = 0 
		: lineCounter += 1) : lineCounter);
	}


	readonly Runnable Runnables = new Runnable();

	public void RunScan(MainGUI Main)
	{
	    try
	    {
		MainGUI.Initiator1 Init1 = Main.InitiateBottom;
		MainGUI.Initiator2 Init2 = Main.InitiateMiddle;

		Dialog2 Dialog2 = Main.InitiateMiddle.Dialog2;

		Tools.SortCode(("Window Visibility Section"), () =>
		{
		    UpdateButtonText("Scanning ...", Init1);

		    InitiateM.Shell.SetDefaultText();

		    Dialog2.Parent.Show();
		    Dialog2.Parent.BringToFront();
		    Dialog2.Parent.Focus();

		    ClearPortStatuses();
		});
		
		Runnables.RunTaskAsynchronously(null, () => 
		{
		    Tools.SortCode(("Scan Section"), () =>
		    {
			SendMessage($"> GateHey scan session started at: {Tools.GetCurrentTime()}");

			// For threading; only allow this when either a range or a big selection of ports has been selected.
			// The amount of selected ports should be at least equal to the amount of threads, if not disable feature.
			string host = DashNet.GetIP(Init2.GetComponentValues()["host"]);
			string packetData = Init2.GetComponentValues()["packdata"];
			string protocol = Init2.GetComponentValues()["protocol"];

			bool sendPacketData = (packetData.Length < 1 || packetData.Equals("none"));

			int threads = int.Parse(Init2.GetComponentValues()["threads"]);
			int timeout = int.Parse(Init2.GetComponentValues()["timeout"]);

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
				    if (Universal.IsScanning())
					AddPortStatus(port, AttemptConnect(host, port, GetSocketType(protocol),
					    GetProtocol(protocol), packetData, timeout));
				    else
					break;
				}
			    }

			    else // Ranged
			    {
				for (int port = Universal.Ports[0]; port <= Universal.Ports[1]; port += 1)
				{
				    if (Universal.IsScanning())
					AddPortStatus(port, AttemptConnect(host, port, GetSocketType(protocol),
					    GetProtocol(protocol), packetData, timeout));
				    else
					break;
				}
			    }

			    StopScan(Init1, false);
			});
		    });
		});
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	void UpdateButtonText(string text, MainGUI.Initiator1 BottomSect) => Runnables
	    .RunTaskAsynchronously(BottomSect.Bttn1.Parent, () => BottomSect.Bttn1.Text = $"{text}");


	public void StopScan(MainGUI.Initiator1 BottomSect, bool forceful = true)
	{
	    try
	    {
		Runnables.RunTaskAsynchronously(null, () =>
		{
		    try
		    {
			if (!BottomSect.Bttn1.Text.Equals("Scanning ..."))
			    return;
			
			SendMessage($"> Telling all workers (if any-) to stop working ...", 
			    pnl: Universal.IsScanning());

			UpdateButtonText("Stopping ...", BottomSect);

			Universal.ToggleScanner();
			Thread.Sleep(3500);
			
			SendMessage($"> GateHey scan session has been finished successfully at: " +
			    $"{Tools.GetCurrentTime()}.");

			SendMessage("> You are now free to launch another scan.");
			UpdateButtonText("Start Scanning", BottomSect);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
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