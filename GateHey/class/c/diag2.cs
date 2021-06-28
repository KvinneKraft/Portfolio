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

using DashFramework.DashInteract;
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
			    This.SendMessage("An exception has occurred internally making me unable to fullfill your needs.");
			    This.SendMessage($"Debugger Information: ErrorCode[{E.Message}]");
			}

			return;
		    }

		    This.SendMessage("That command cannot be found.  Perhaps try typing help.");
		}
	    }
	    

	    Color RighteousColors(Dialog2 This, string[] cmds)
	    {
		try
		{
		    void SendErrorMessage()
		    {
			This.SendMessage(">> Usage: set (f/b)col [r,g,b]");
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


	    readonly DashApp TouchApp = new DashApp();

	    void RegisterCommandHooks(Dialog2 This, InitiateMiddle OptSet, MainGUI Main)
	    {
		try
		{
		    void AddAliases(string cmd, params string[] aliases)
		    {
			if (RunnableCache.ContainsKey(cmd))
			    foreach (string alias in aliases)
				RunnableCache.Add(alias, (cmds) =>
			    RunnableCache[cmd]
			(cmds));
		    }

		    Tools.SortCode(("SET Commands"), () =>
		    {
			RunnableCache.Add("set", (string[] cmds) =>
			{
			    if (cmds.Length < 2)
			    {
				This.SendMessage(">> Usage: set [bcol | fcol] r,g,b");
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
				    Tools.RGBString(ColorCode) + ".");
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
				    .RGBString(ColorCode) + ".");
			    }
			});
		    });

		    Tools.SortCode(("My Commands"), () =>
		    {
			void OpenUrl(string url)
			{
			    try
			    {
				This.SendMessage($"> Opening the corresponding URL in your browser (if any) ...");
				Tools.OpenUrl(url);
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
				This.SendMessage("> Scan is already running.  Try typing stop instead.");
				return;
			    }

			    This.RunScan(Main);
			});

			RunnableCache.Add("reboot", (cmds) =>
			{
			    if (IsScanning())
			    {
				This.StopScan(Main.InitiateBottom);
				Thread.Sleep(500);
			    }

			    TouchApp.RestartMe();
			});

			RunnableCache.Add("close", (cmds) =>
			{
			    if (IsScanning())
			    {
				This.StopScan(Main.InitiateBottom);
				Thread.Sleep(500);
			    }

			    TouchApp.CloseMe();
			});

			RunnableCache.Add("clear", (cmds) =>
			    OptSet.Shell.ClearText());

			RunnableCache.Add("help", (cmds) =>
			{
			    Runnables.RunTaskAsynchronously(null, () =>
			    {
				This.SendMessage($">> Available dashie shell commands:");

				for (int k = 0, s = 1; k < RunnableCache.Count; k += 1, s += 1)
				{
				    This.SendMessage($": {RunnableCache.ElementAt(k).Key} ", 
					nl: s == 4);

				    if (s == 4) s = 1;
				}

				This.SendMessage($">> Please note that support for more commands " +
				    "will be available in the near future.  This release is the first.");
			    });
			});

			RunnableCache.Add("verbose", (cmds) => 
			{
			    Universal.DoVerbose = (Universal.DoVerbose == true 
				? false : true);

			    This.SendMessage($@"> You have turned verbose " +
				(Universal.DoVerbose ? "on" : "off") + ".");
			});

			RunnableCache.Add("save", (cmds) =>
			{
			    string filename = $@"latest-log.txt";

			    try
			    {
				This.SendMessage($"> Saving log file under the name {filename} ...");
			
				File.WriteAllText($"{filename}", OptSet.Shell.TerminalLog.Text);

				if (!File.Exists(filename))
				{
				    throw new Exception("filename");
				}

				This.SendMessage("> Done.");
			    }

			    catch
			    {
				This.SendMessage($"> Unable to save file as {filename} !");
			    }
			});

			AddAliases("verbose", "verb", "showmeall", "descriptive", "moreinfo");
			AddAliases("clear", "cls", "clr", "clearlog", "clrlog", "clslog");
			AddAliases("save", "savelog", "savlog", "export", "exportlog");
			AddAliases("reboot", "restart", "rebt", "rest", "reload");
			AddAliases("close", "exit", "terminate", "end");
			AddAliases("help", "hlp", "info", "ineedhelp");
			AddAliases("stop", "quit", "cancel", "halt");
			AddAliases("start", "go", "run", "scan");
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

	    public void ClearText() => TerminalLog.Clear();
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


	void SendMessage(string msg, bool nl = true, bool cv = false) => Runnables
	    .RunTaskAsynchronously(InitiateM.Shell.TerminalLog.Parent, () => InitiateM
		.Shell.OutputText($@"{(cv ? (Universal.DoVerbose ? msg : "") : msg)}", nl));


	readonly public List<int> SuccessfulConnections = new List<int>();
	readonly public List<int> FailedConnections = new List<int>();

	void ClearPortStatuses()
	{
	    SuccessfulConnections.Clear();
	    FailedConnections.Clear();
	}

	
	void AddPortStatus(int port, bool open = true, bool uline = false)
	{
	    if (open)
		if (!SuccessfulConnections.Contains(port))
		    SuccessfulConnections.Add(port);
	    else
		if (!FailedConnections.Contains(port))
		    FailedConnections.Add(port);

	    if (Universal.IsScanning())
		SendMessage($"> {port}: {(open ? "open" : "closed")}", nl: 
		    (!open ? (!Universal.DoVerbose ? false : true) : true), cv: !open);
	}


	readonly Runnable Runnables = new Runnable();

	public void RunScan(MainGUI Main)
	{
	    try
	    {
		if (!Universal.SettingsValidation(Main.InitiateMiddle, false))
		{
		    Tools.MsgBox($"{Universal.GetLastError()}", 
			icon: MessageBoxIcon.Warning);

		    return;
		}

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

			    var ListOThreads = new List<Thread>();
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
				if (Universal.Ports.Count < threads)
				{
				    SendMessage("> You have selected more threads than ports.");
				    SendMessage("> You must at least have as many ports selected as threads.");
				    StopScan(Init1);
				    return;
				}

				// multi-thread dis:
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
				if (Universal.Ports[1] - Universal.Ports[0] < threads)
				{
				    SendMessage("> You have selected more threads than ports.");
				    SendMessage("> You must at least have as many ports selected as threads.");
				    StopScan(Init1);
				    return;
				}

				int last = -1;

				void ScanThese(int min, int max)
				{
				    for (int port = min; port <= max; port += 1)
				    {
					if (Universal.IsScanning())
					    AddPortStatus(port, AttemptConnect(host, port, GetSocketType(protocol),
						GetProtocol(protocol), packetData, timeout));
					else
					    break;
				    }
				}
				
				void AddThread(int min, int max)
				{
				    ListOThreads.Add(new Thread
					(() => ScanThese(min, max)));

				    last = max;
				}

				int range = (Universal.Ports[1] - Universal.Ports[0]) / threads;
				int minim = Universal.Ports[0];
				int maxim = Universal.Ports[1];

				for (int thread = 0, min = minim, max = range + 1; thread < threads; thread += 1, 
				    min += range, max = range * (thread + 1) + 1)
					AddThread(min, max);
				
				if (last < maxim)
				    AddThread(last, maxim);
			    }

			    if (ListOThreads.Count > 0)
				SendMessage($"> Starting {ListOThreads.Count} scan workers ...");

			    foreach (Thread thread in ListOThreads)
				thread.Start();

			    foreach (Thread thread in ListOThreads)
				thread.Join();
			    
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

			SendMessage($"> Telling all workers (if any-) to stop working ...");

			UpdateButtonText("Stopping ...", BottomSect);

			Universal.ToggleScanner();
			Thread.Sleep(3500);
			
			SendMessage($"> GateHey scan session has been finished successfully at: {Tools.GetCurrentTime()}");
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