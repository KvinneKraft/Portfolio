// Author: Dashie
// Version: 1.0
// Dash Shell

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Networking;
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
	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    Color barBCol = Inst.values.getBarColor();
		    Size diagSize = new Size(425, 285);
		    Color diagBCol = Inst.BackColor;

		    string diagTitle = ("Dash - Shell");

		    Parent.InitializeWindow(diagSize, diagTitle, diagBCol, barBCol, roundRadius: 0);
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
	    readonly DashPanel Panel = new DashPanel();
	    readonly TextBox TxtBox = new TextBox();
	    readonly Label Lbl = new Label();

	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    var PanelLoca = new Point(0, Parent.Height - 28);
		    var PanelBCol = Parent.values.getBarColor();
		    var PanelSize = new Size(Parent.Width, 26);

		    Controls.Panel(Parent, Panel, PanelSize, PanelLoca, PanelBCol);

		    var LblSize = Tools.GetFontSize("$:", Id: 0);
		    var LblLoca = new Point(0, 7);
		    var LblFCol = Color.White;
		    var LblBCol = PanelBCol;

		    var TxtSize = new Size(PanelSize.Width - LblSize.Width - 3, 24);
		    var TxtLoca = new Point(LblSize.Width, 2);
		    var TxtFCol = Color.White;
		    var TxtBCol = PanelBCol;

		    Controls.TextBox(Panel, TxtBox, TxtSize, TxtLoca, TxtBCol, TxtFCol, 1, 9);
		    Controls.Label(Panel, Lbl, LblSize, LblLoca, LblBCol, LblFCol, ("$:"), 0, 10);

		    TxtBox.Text = ("help");
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	class InitiateMiddle
	{
	    public class DashShell
	    {
		public readonly TextBox TerminalLog = new TextBox();

		public void setDefaultText()
		{
		    try
		    {
			TerminalLog.Text = string.Format(
			    $">>> Hey there {Environment.UserName} !\r\n" +
			    ">>> Type 'help' for help, thank you for using this."
			);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}
	    }


	    readonly CustomScrollBar DashBar = new CustomScrollBar();
	    readonly DashShell Shell = new DashShell();
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

		    Controls.TextBox(Panel, Shell.TerminalLog, TextBoxSize, TextBoxLoca, TextBoxBCol, TextBoxFCol, 0, 9, true, true, FixedSize: false);
		    Controls.Panel(Parent, Panel, PanelSize, PanelLoca, PanelBCol);

		    Shell.setDefaultText();
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

	readonly DashWindow Parent = new DashWindow();

	public void Initiator(DashWindow Inst)
	{
	    try
	    {
		InitiateT.Initiate(Parent, Inst);
		InitiateB.Initiate(Parent, Inst);
		InitiateM.Initiate(Parent, Inst);

		this.Inst = Inst;
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}


	// connect to port method, with protocol type, packetData, timeout and port
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


	public void RunScan(MainGUI.Initiator2 MainSettings)
	{
	    try
	    {
		int threads = int.Parse(MainSettings.GetComponentValues()["threads"]);
		int timeout = int.Parse(MainSettings.GetComponentValues()["timeout"]);

		string packetData = MainSettings.GetComponentValues()["packdata"];
		string protocol = MainSettings.GetComponentValues()["protocol"];
		string host = MainSettings.GetComponentValues()["host"];

		bool sendPacketData = (packetData.Length < 1 || packetData.Equals("none"));

		Tools.SortCode(("Port Scanning"), () =>
		{
		    List<int> SuccessfulConnections = new List<int>();
		    List<int> FailedConnections = new List<int>();

		    int ScanType = Universal.ScanType;
		    
		    if (ScanType == 1) //Single
		    {
			int port = Universal.Ports[0];

			if (AttemptConnect(host, port, GetSocketType(protocol), GetProtocol(protocol), packetData, timeout))
			{
			    SuccessfulConnections.Add(port);
			}
			
			else
			{
			    FailedConnections.Add(port);
			}
		    }

		    else if (ScanType == 2) // Multi
		    {
			foreach (int port in Universal.Ports)
			{
			    if (!Universal.DoScanning)
			    {
				break;
			    }

			    else if (AttemptConnect(host, port, GetSocketType(protocol), GetProtocol(protocol), packetData, timeout))
			    {
				SuccessfulConnections.Add(port);
			    }

			    else
			    {
				FailedConnections.Add(port);
			    }
			}
		    }

		    else // Ranged
		    {
			for (int port = Universal.Ports[0]; port <= Universal.Ports[1]; port += 1)
			{
			    if (!Universal.DoScanning)
			    {
				break;
			    }

			    else if (AttemptConnect(host, port, GetSocketType(protocol), GetProtocol(protocol), packetData, timeout))
			    {
				SuccessfulConnections.Add(port);
			    }

			    else
			    {
				FailedConnections.Add(port);
			    }
			}
		    }

		    MessageBox.Show($"{SuccessfulConnections.Count} : {FailedConnections.Count}");
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