
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Networking;
using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class ServerPing
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();
	private readonly PictureBox S1Container2 = new PictureBox();
	private readonly PictureBox S1Container3 = new PictureBox();

	private void Init1(DashWindow DashWindow, PictureBox Capsule)
	{
	    try
	    {
		var Container1Size = new Size(Capsule.Width, 255);
		var Container1Loca = new Point(0, (Capsule.Height - 255) / 2);

		Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Capsule.BackColor);

		var Container2Size = new Size(Capsule.Width, 98);
		var Container2Loca = new Point(0, 0);

		var Container3Size = new Size(Capsule.Width, 135);
		var Container3Loca = new Point(0, 118);

		var ContainerBCol = DashWindow.MenuBar.MenuBar.BackColor;

		Control.Image(S1Container1, S1Container2, Container2Size, Container2Loca, ContainerBCol);
		Control.Image(S1Container1, S1Container3, Container3Size, Container3Loca, ContainerBCol);

		Tool.Round(S1Container2, 6);
		Tool.Round(S1Container3, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S2Container1 = new PictureBox();

	private readonly TextBox S2TextBox1 = new TextBox();
	private readonly TextBox S2TextBox2 = new TextBox();
	private readonly TextBox S2TextBox3 = new TextBox();
	private readonly TextBox S2TextBox4 = new TextBox();

	private readonly Label S2Label1 = new Label();
	private readonly Label S2Label2 = new Label();
	private readonly Label S2Label3 = new Label();
	private readonly Label S2Label4 = new Label();
	private readonly Label S2Label5 = new Label();
	private readonly Label S2Label6 = new Label();

	private readonly DropMenu S2DropMenu = new DropMenu();

	private void S2SetupDropDownMenu()
	{
	    try
	    {
		var DropMenuLoca = new Point(S1Container2.Left + S2Label6.Left + 10, S1Container2.Top + S2Label6.Top + S2Label6.Height + 10);
		var DropMenuBCol = S2Label6.BackColor;

		S2DropMenu.SetupMenu(S1Container1, DropMenuLoca, DropMenuBCol, DropMenuBCol);

		int ItemWidth = S2Label6.Width - 4;
		int ItemHeight = 18;

		var ItemBCol = S2Container1.BackColor;

		S2DropMenu.AddItem(new Label(), ("(T.C.P)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);
		S2DropMenu.AddItem(new Label(), ("(U.D.P)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);
		S2DropMenu.AddItem(new Label(), ("(ICMPv)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);

		foreach (Control Item in S2DropMenu.ContentContainer.Controls)
		{
		    try
		    {
			Item.Click += (s, e) =>
			{
			    S2Label6.Text = $"--{Item.Text.Replace(".", string.Empty)}--";
			};
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		S2Label6.MouseEnter += (s, e) =>
		{
		    try
		    {
			S2DropMenu.Show();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void Init2(PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(S1Container2.Width - 20, S1Container2.Height - 20);
		var ContainerLoca = new Point(10, 10);
		var ContainerBCol = S1Container2.BackColor;

		Control.Image(S1Container2, S2Container1, ContainerSize, ContainerLoca, ContainerBCol);

		ControlHelper CHelper = new ControlHelper()
		{
		    TextBoxBCol = Capsule.BackColor,
		    TextBoxParent = S2Container1,
		    TextBoxFCol = Color.White,

		    LabelParent = S2Container1,
		    LabelBCol = ContainerBCol,
		    LabelFCol = Color.White
		};

		var Label1Size = CHelper.GetFontSize("Host:");
		var Label1Loca = new Point(0, 0);

		var TextBox1Size = new Size(165, 20);
		var TextBox1Loca = new Point(Label1Size.Width, 0);

		var Label2Size = CHelper.GetFontSize("Port:");
		var Label2Loca = CHelper.ControlX(TextBox1Size, TextBox1Loca);

		var TextBox2Size = CHelper.TextBoxSize(Label2Size, Label2Loca);
		var TextBox2Loca = CHelper.ControlX(Label2Size, Label2Loca, Extra: 0);

		var Label3Size = CHelper.GetFontSize("Packet Size:");
		var Label3Loca = new Point(0, TextBox2Loca.Y + TextBox2Size.Height + 10);

		var TextBox3Size = new Size(100, 20);
		var TextBox3Loca = CHelper.ControlX(Label3Size, Label3Loca, Extra: 0);

		var Label4Size = CHelper.GetFontSize("T.T.L:");
		var Label4Loca = CHelper.ControlX(TextBox3Size, TextBox3Loca);

		var TextBox4Size = CHelper.TextBoxSize(Label4Size, Label4Loca);
		var TextBox4Loca = CHelper.ControlX(Label4Size, Label4Loca, Extra: 0);

		var Label5Size = CHelper.GetFontSize("Protocol:");
		var Label5Loca = new Point(0, TextBox4Loca.Y + TextBox2Size.Height + 10);

		var Label6Size = new Size(100, Label5Size.Height);
		var Label6Loca = CHelper.ControlX(Label5Size, Label5Loca, Extra: 0);

		CHelper.AddTextBox(S2TextBox1, TextBox1Size, TextBox1Loca, ("https://pugpawz.com"));
		CHelper.AddTextBox(S2TextBox2, TextBox2Size, TextBox2Loca, ("80"));
		CHelper.AddTextBox(S2TextBox3, TextBox3Size, TextBox3Loca, ("2021"));
		CHelper.AddTextBox(S2TextBox4, TextBox4Size, TextBox4Loca, ("75"));

		foreach (Control Control in S2Container1.Controls)
		{
		    if (Control is PictureBox)
		    {
			Tool.Round(Control, 6);
		    }
		}

		CHelper.AddLabel(S2Label3, Label3Size, Label3Loca, ("Packet Size:"));
		CHelper.AddLabel(S2Label5, Label5Size, Label5Loca, ("Protocol:"));
		CHelper.AddLabel(S2Label4, Label4Size, Label4Loca, ("T.T.L:"));
		CHelper.AddLabel(S2Label1, Label1Size, Label1Loca, ("Host:"));
		CHelper.AddLabel(S2Label2, Label2Size, Label2Loca, ("Port:"));
		CHelper.AddLabel(S2Label6, Label6Size, Label6Loca, ("--- (TCP) ---"), 8);

		S2Label6.TextAlign = ContentAlignment.MiddleCenter;
		S2Label6.BackColor = Capsule.BackColor;

		S2SetupDropDownMenu();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private void S3Button1Event()
	{
	    try
	    {
		new ServerPinger().PingEvent(S2TextBox1.Text, S2TextBox2.Text,
		    S2TextBox3.Text, S2TextBox4.Text, S2Label6.Text);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}

	public class ServerPinger
	{
	    private readonly DashNet DashNet = new DashNet();

	    public bool PingHost(ProtocolType ProtocolType, SocketType SocketType, string Address, int Port, int PacketSize, int TTL, int Timeout = 5000)
	    {
		try
		{
		    AddressFamily AddrFamily = DashNet.GetAddressFamily(Address);

		    using (Socket Socket = new Socket(AddrFamily, SocketType, ProtocolType))
		    {
			Socket.ReceiveTimeout = Timeout / 2;
			Socket.SendTimeout = Timeout / 2;

			Socket.SendBufferSize = PacketSize;
			Socket.Ttl = (short) TTL;

			IAsyncResult Async = Socket.BeginConnect(Address, Port, null, null);
			bool Result = Async.AsyncWaitHandle.WaitOne(Timeout, true);

			if (Socket.Connected)
			{
			    byte[] Bytes = Encoding.ASCII.GetBytes
			    (
				new string('+', PacketSize)
			    );

			    Socket.Send(Bytes);

			    Thread.Sleep(1000); // Do not want to flood the fucking server.
			}

			return Socket.Connected;
		    }
		}

		catch
		{
		    Print("The selected protocol is not supported by your operating system.  Cancelling ....");
		    DoPinging = false;

		    return false;
		}
	    }

	    private void Print(string Data, bool NewLine = true)
	    {
		S3TextBox1.AppendText($@"{Data}{(NewLine ? "\r\n" : string.Empty)}");
	    }

	    public void PingEvent(string Address, string Port, string PacketSize, string TTL, string Protocol)
	    {
		try
		{
		    Print("Validating given data ....");

		    if (!DashNet.CanIP(Address))
		    {
			Print("The host given could not be resolved.");
			return;
		    }

		    else if (!DashNet.CanPort(Port))
		    {
			Print("The port value specified can not be used.");
			return;
		    }
		    
		    else if (!DashNet.CanInteger(PacketSize))
		    {
			Print("The packet size value specified can not be used.");
			return;
		    }

		    else if (!DashNet.CanInteger(TTL))
		    {
			Print("The T.T.L value specified can not be used.");
			return;
		    }

		    Address = DashNet.GetIP(Address);

		    int _PacketSize = int.Parse(PacketSize);
		    int _Port = int.Parse(Port);
		    int _TTL = int.Parse(TTL);

		    ProtocolType ProtocolType = ProtocolType.Tcp;
		    SocketType SocketType = SocketType.Stream;

		    if (Protocol.ToLower().Contains("icmpv"))
		    {
			ProtocolType = ProtocolType.IcmpV6;
			//SocketType = SocketType.Seqpacket;
		    }

		    else if (Protocol.Contains("UDP"))
		    {
			ProtocolType = ProtocolType.Udp;
			SocketType = SocketType.Dgram;
		    }

		    Print($"Started pinging {Address}:{Port} ....");
		    
		    for (int r = 1; ; r += 1)
		    {
			if (!DoPinging)
			{
			    break;
			}

			var StopWatch = new Stopwatch();
			
			StopWatch.Start();

			if (PingHost(ProtocolType, SocketType, Address, _Port, _PacketSize, _TTL))
			{
			    int time = (int) StopWatch.ElapsedMilliseconds - (StopWatch.ElapsedMilliseconds > 1000 ? 1001 : 1000);
			    Print($"Received reply from {Address}:{Port} -req={r} -bytes={PacketSize} -ttl={TTL} in {time}ms");
			}

			else
			{
			    Print($"Request {r} timed out!");
			}

			StopWatch.Stop();
		    }

		    Print($"Finished pinging {Address}:{Port}.");
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	public static readonly TextBox S3TextBox1 = new TextBox();

	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly Button S3Button1 = new Button();
	private readonly Label S3Label1 = new Label();

	public static bool DoPinging = true;

	private void Init3(PictureBox Capsule)
	{
	    try
	    {
		var Container1Size = new Size(S1Container3.Width - 20, S1Container3.Height - 20);
		var Container1Loca = new Point(10, 10);
		var Container1BCol = S1Container3.BackColor;

		Control.Image(S1Container3, S3Container1, Container1Size, Container1Loca, Container1BCol);

		var LabelSize = Tool.GetFontSize("Ping Log", 12);
		var LabelLoca = new Point(0, 0);

		Control.Label(S3Container1, S3Label1, LabelSize, LabelLoca, Container1BCol, Color.White, 1, 12, ("Ping Log"));

		var ButtonSize = new Size(100, 21);
		var ButtonLoca = new Point(Container1Size.Width - 100, 0);
		var ButtonBCol = Capsule.BackColor;

		Control.Button(S3Container1, S3Button1, ButtonSize, ButtonLoca, ButtonBCol, Color.White, 1, 8, ("Start Pinging"));

		S3Button1.Click += (s, e) =>
		{
		    if (S3Button1.Text == ("Start Pinging"))
		    {
			new Thread(()
			=>
			    {
				S3Button1.Text = ("Stop Pinging");

				S3Button1Event();

				S3Button1.Text = ("Start Pinging");

				DoPinging = true;
			    }
			)

			{ IsBackground = true }.Start();
		    }

		    else
		    {
			DoPinging = false;
		    }

		    return;
		};
		
		var TextBoxSize = new Size(Container1Size.Width, Container1Size.Height - (10 + LabelSize.Height));
		var TextBoxLoca = new Point(0, LabelSize.Height + 10);
		var TextBoxBCol = Capsule.BackColor;

		Control.TextBox(S3Container1, S3TextBox1, TextBoxSize, TextBoxLoca, TextBoxBCol, Color.White, 1, 7,
		    ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

		S3TextBox1.Text = string.Format
		(
		    "I am waiting for action. Learn how to use the pinger by experimenting with it. Or be lazy and press F1.\r\n" 
		);

		Tool.Round(S3Container1, 6);
		Tool.Round(S3Button1, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private void Print(string Data, bool NewLine = true) =>
	    S3TextBox1.AppendText($@"{Data}{(NewLine ? "\r\n" : string.Empty)}");

	private void Init4()
	{
	    try
	    {
		void AddEventHandler(Control.ControlCollection Collection = null, Control Control = null)
		{
		    try
		    {
			void SetItemEvent(Control Item)
			{
			    try
			    {
				Item.KeyDown += (s, e) =>
				{
				    switch (e.KeyData)
				    {
					case Keys.F1:
					    Print(":Help Message:");
					    Print("F1 = this message,");
					    Print("F2 = ping usage help,");
					    Print("F3 = clear this log;");
					    break;
					case Keys.F2:
					    Print(":Settings Rundown:");
					    Print("Host=target specification,");
					    Print("Port=port to be used to ping,");
					    Print("Packet Size=size of packet to send,");
					    Print("T.T.L=time to live for each connection,");
					    Print("Protocol=the protocol to use. TCP is recommended due to is compatibility;");
					    break;
					case Keys.F3:
					    S3TextBox1.Clear();
					    break;
				    }
				};
			    }

			    catch (Exception E)
			    {
				throw (ErrorHandler.GetException(E));
			    }
			}

			if (Collection != null)
			{
			    foreach (Control Item in Collection)
			    {
				SetItemEvent(Item);
			    }
			}

			if (Control != null)
			{
			    SetItemEvent(Control);
			}
		    }

		    catch (Exception E)
		    {
			ErrorHandler.JustDoIt(E);
		    }
		}

		AddEventHandler(S1Container1.Controls, null);
		AddEventHandler(S1Container2.Controls, null);
		AddEventHandler(S1Container3.Controls, null);
		
		AddEventHandler(S2Container1.Controls, null);
		AddEventHandler(S3Container1.Controls, null);

		foreach (Control Control in S2Container1.Controls)
		{
		    if (Control is PictureBox && Control.Controls.Count > 0)
		    {
			AddEventHandler(null, Control.Controls[0]);
		    }
		}

		AddEventHandler(null, S1Container1);
		AddEventHandler(null, S1Container2);
		AddEventHandler(null, S1Container3);
		AddEventHandler(null, S2Container1);
		AddEventHandler(null, S3Container1);

		S1Container1.VisibleChanged += (s, e) =>
		{
		    if (S1Container1.Visible)
		    {
			S1Container1.Select();
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private bool isInitialized = false;

	public void InitializePage(DashWindow DashWindow, PictureBox Capsule, InitThaDashlorisX Parent)
	{
	    try
	    {
		if (!isInitialized)
		{
		    Init1(DashWindow, Capsule);
		    Init2(Capsule);
		    Init3(Capsule);
		    Init4();

		    isInitialized = true;
		}
		
		Show();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}

	public void Show()
	{
	    try
	    {
		S1Container1.Show();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Hide()
	{
	    try
	    {
		S1Container1.Hide();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
