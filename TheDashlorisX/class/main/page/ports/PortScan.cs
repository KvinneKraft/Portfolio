
// Author: Dashie
// Version: 3.0

//#groningen#1505

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
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
    public class PortScan
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
		var Container1Size = new Size(Capsule.Width, Capsule.Height - 3);
		var Container1Loca = new Point(0, 3);

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
	private readonly PictureBox S2Container2 = new PictureBox();
	private readonly PictureBox S2Container3 = new PictureBox();

	private readonly TextBox S2TextBox1 = new TextBox();
	private readonly TextBox S2TextBox2 = new TextBox();
	private readonly TextBox S2TextBox3 = new TextBox();
	
	private readonly Label S2Label1 = new Label();
	private readonly Label S2Label2 = new Label();
	private readonly Label S2Label3 = new Label();
	private readonly Label S2Label4 = new Label();
	private readonly Label S2Label5 = new Label();
	private readonly Label S2Label6 = new Label();

	private readonly ControlHelper CHelper = new ControlHelper();
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
		var Container1Size = new Size(S1Container2.Width - 20, S1Container2.Height);
		var Container1Loca = new Point(10, 10);
		var Container1BCol = S1Container2.BackColor;

		Control.Image(S1Container2, S2Container1, Container1Size, Container1Loca, Container1BCol);

		CHelper.TextBoxParent = S2Container1;
		CHelper.LabelParent = S2Container1;

		var Label1Loca = new Point(0, 0);
		var Label1Size = CHelper.GetFontSize("Host:");

		var TextBox1Loca = new Point(Label1Size.Width, 0);
		var TextBox1Size = new Size(165, 20);

		var Label2Loca = CHelper.ControlX(TextBox1Size, TextBox1Loca);
		var Label2Size = CHelper.GetFontSize("Ports:");

		var TextBox2Loca = CHelper.ControlX(Label2Size, Label2Loca, Extra:0);
		var TextBox2Size = CHelper.TextBoxSize(Label2Size, Label2Loca);

		var Label3Loca = new Point(0, TextBox1Loca.Y + 30);
		var Label3Size = CHelper.GetFontSize("Timeout:");
		
		var TextBox3Loca = new Point(Label3Size.Width, Label3Loca.Y);
		var TextBox3Size = new Size(125, 20);

		var Label4Loca = CHelper.ControlX(TextBox3Size, TextBox3Loca);
		var Label4Size = CHelper.GetFontSize("Protocol:");

		var Label5Loca = new Point(0, Label3Loca.Y + 30);
		var Label5Size = CHelper.GetFontSize("Keep-Alive:");

		var Label6Loca = CHelper.ControlX(Label4Size, Label4Loca, Extra:0);
		var Label6Size = CHelper.TextBoxSize(Label4Size, Label4Loca);

		CHelper.TextBoxBCol = S1Container1.Parent.BackColor;
		CHelper.TextBoxFCol = Color.White;

		CHelper.AddTextBox(S2TextBox1, TextBox1Size, TextBox1Loca, ("8.8.8.8"));
		CHelper.AddTextBox(S2TextBox2, TextBox2Size, TextBox2Loca, ("80,443,22,21"));
		CHelper.AddTextBox(S2TextBox3, TextBox3Size, TextBox3Loca, ("350"));
		
		foreach (Control Control in S2Container1.Controls)
		{
		    if (Control is PictureBox)
		    {
			Tool.Round(Control, 6);
		    }
		}

		CHelper.LabelBCol = S2Container1.BackColor;
		CHelper.LabelFCol = Color.White;

		CHelper.AddLabel(S2Label1, Label1Size, Label1Loca, ("Host:"));
		CHelper.AddLabel(S2Label2, Label2Size, Label2Loca, ("Ports:"));
		CHelper.AddLabel(S2Label3, Label3Size, Label3Loca, ("Timeout:"));
		CHelper.AddLabel(S2Label4, Label4Size, Label4Loca, ("Protocol:"));
		CHelper.AddLabel(S2Label5, Label5Size, Label5Loca, ("Keep-Alive:"));
		CHelper.AddLabel(S2Label6, Label6Size, Label6Loca, ("-- (TCP) --"), 8);
		
		S2Label6.TextAlign = ContentAlignment.MiddleCenter;
		S2Label6.BackColor = (S2TextBox1.BackColor);

		S2SetupDropDownMenu();

		var CheckBoxSize = new Size(16, 16);
		var CheckBoxLoca = new Point(Label5Size.Width, Label5Loca.Y + 2);

		Control.CheckBox(S2Container1, S2Container2, S2Container3, CheckBoxSize, CheckBoxLoca, Capsule.BackColor, Color.DarkMagenta, true);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public static readonly TextBox S3TextBox1 = new TextBox();
	private readonly Button S3Button1 = new Button();

	private void S3Button1Event()
	{
	    try
	    {
		new PortScanner().ScanEvent(S2TextBox1.Text, S2TextBox2.Text, 
		    S2TextBox3.Text, S2Label6.Text, S2Container3);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}

	public class PortScanner
	{
	    private readonly DashNet DashNet = new DashNet();


	    private void Print(string Data, bool NewLine = true)
	    {
		S3TextBox1.AppendText($@"{Data}{(NewLine ? "\r\n" : string.Empty)}");
	    }


	    private readonly List<Socket> Sockets = new List<Socket>();

	    private bool ScanPort(ProtocolType ProtocolType, SocketType SocketType, string Address, int Port, int Timeout, bool KeepAlive)
	    {
		try
		{
		    AddressFamily AddrFamily = DashNet.GetAddressFamily(Address);

		    using (Socket Socket = new Socket(AddrFamily, SocketType, ProtocolType))
		    {
			Socket.LingerState = new LingerOption(true, 0);
			Socket.NoDelay = true;

			Socket.ReceiveTimeout = 0;
			Socket.SendTimeout = 0;

			IAsyncResult Async = Socket.BeginConnect(Address, Port, null, null);
			bool Result = Async.AsyncWaitHandle.WaitOne(Timeout, true);

			if (KeepAlive)
			{
			    Socket.Ttl = 255;
			    Sockets.Add(Socket);
			}

			return Socket.Connected;
		    }
		}

		catch
		{
		    return false;
		}
	    }

	    public void ScanEvent(string Address, string Port, string Timeout, string Method, PictureBox CheckBox)
	    {
		try
		{
		    Print("Validating given data ....");

		    if (!DashNet.CanIP(Address))
		    {
			Print("The host given could not be resolved.");
			return;
		    }

		    bool GoveDomains = PortScan.GoveDomains;
		    bool VerboseMode = PortScan.VerboseMode;

		    Print($@"Goverment Domains: {(GoveDomains ? "Allowed" : "Prohibited")}");
		    Print($@"Verbose Mode: {(GoveDomains ? "Enabled" : "Disabled")}");

		    if (!DashNet.AllowedDomain(Address))
		    {
			if (!GoveDomains)
			{
			    Print("The given domain is a government domain. These are by default on the blacklist. Press 'F1' for a potential fix.");
			    return;
			}
		    }

		    Address = DashNet.GetIP(Address);

		    var Splitter = '~';

		    if (!Port.Contains('~'))
		    {
			Splitter = Port.Contains('-') ? '-' : ',';
		    }

		    List<int> Ports = new List<int>();

		    foreach (var Port_ in Port.Split(Splitter))
		    {
			if (!DashNet.CanPort(Port_))
			{
			    Print("The port value specified can not be used.");
			    return;
			}
			Ports.Add(int.Parse(Port_));
		    }

		    if (!DashNet.CanInteger(Timeout))
		    {
			Print("The timeout value is invalid.");
			return;
		    }

		    int TT = int.Parse(Timeout);

		    ProtocolType ProtocolType = ProtocolType.Tcp;
		    SocketType SocketType = SocketType.Stream;

		    if (Method.Contains("UDP"))
		    {
			ProtocolType = ProtocolType.Udp;
			SocketType = SocketType.Dgram;
		    }

		    bool KA = (CheckBox.BackColor == Color.DarkMagenta);

		    Print($"Date and Time: [{DateTime.Today}]");
		    Print("Started scanning ....");

		    void ProgressMade(int _Port)
		    {
			try
			{
			    int MP = Ports.Count / 4;

			    var PortDB = new List<int>()
			    {
				Ports[MP * 1],
				Ports[MP * 2],
				Ports[MP * 3]
			    };

			    if (PortDB.Contains(_Port))
			    {
				Print($@"We are getting there. Stage: {PortDB.IndexOf(_Port) + 1}/3 !");
			    }
			}

			catch (Exception E)
			{
			    throw (ErrorHandler.GetException(E));
			}
		    }

		    switch (Splitter)
		    {
			case '~':
			{
			    Print("Mode: Single Port");

			    if (ScanPort(ProtocolType, SocketType, Address, Ports[0], TT, KA))
			    {
				Print($"({Ports[0]}) -= Open");
			    }

			    else if (VerboseMode)
			    {
				Print($"({Ports[0]}) -= Closed");
				ProgressMade(Ports[0]);
			    }

			    break;
			}
			case '-':
			{
			    /*List<List<int>> PortDB = new List<List<int>>()
			    {
				new List<int>()
				{
				    80, 443
				},

				new List<int>()
				{
				    21, 22
				},

	    			new List<int>()
				{
				    2222, 8080
				},
			    };
			    
			    Only if the amount of ports is > 8 and if the amount
			    of ports is dividable by 4; A thread for each list.
			    
			    Timer is two parted code, should work nicely.  Add to log.*/

			    Print("Mode: Range Ports");
			    for (int k = Ports[0]; k <= Ports[1]; k += 1)
			    {
				if (!DoScanning)
				{
				    Print("Scan has been cancelled.");
				    break;
				}

				else if (ScanPort(ProtocolType, SocketType, Address, k, TT, KA))
				{
				    Print($"({k}) -= Open");
				}

				else if (VerboseMode)
				{
				    Print($"({k}) -= Closed");
				    ProgressMade(k);
				}

				ProgressMade(k);
			    }
			    break;
			}
			case ',':
			{
			    Print("Mode: Selective Ports");
			    foreach (var _Port in Ports)
			    {
				if (!DoScanning)
				{
				    Print("Scan has been cancelled.");
				    break;
				}

				else if (ScanPort(ProtocolType, SocketType, Address, _Port, TT, KA))
				{
				    Print($"({_Port}) -= Open");
				}

				else if (VerboseMode)
				{
				    Print($"({_Port}) -= Closed");
				}

				ProgressMade(_Port);
			    }
			    break;
			}
		    }

		    if (KA)
		    {
			Print("Done scanning, flushing connections!");

			foreach (var Socket in Sockets)
			{
			    Socket.Close();
			}

			Sockets.Clear();
		    }

		    Print("Finished scanning!");//Add Timer?
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly Label S3Label1 = new Label();

	private void Init3()
	{
	    try
	    {
		var Container1Size = new Size(S1Container3.Width - 20, S1Container3.Height - 20);
		var Container1Loca = new Point(10, 10);
		var Container1BCol = S1Container2.BackColor;

		Control.Image(S1Container3, S3Container1, Container1Size, Container1Loca, Container1BCol);

		var LabelSize = CHelper.GetFontSize(("Scan Log"), 12);
		var LabelLoca = new Point(0, 0);

		Control.Label(S3Container1, S3Label1, LabelSize, LabelLoca, Container1BCol, Color.White, 1, 12, ("Scan Log"));

		var ButtonSize = new Size(100, 21);
		var ButtonLoca = new Point(Container1Size.Width - 100, 0);
		var ButtonBCol = S2TextBox1.BackColor;

		Control.Button(S3Container1, S3Button1, ButtonSize, ButtonLoca, ButtonBCol, Color.White, 1, 8, ("Start Scan"));

		S3Button1.Click += (s, e) =>
		{
		    if (S3Button1.Text == ("Start Scan"))
		    {
			new Thread(() 
			=> 
			    {
				S3Button1.Text = ("Stop Scan");

				S3Button1Event();

				S3Button1.Text = ("Start Scan");

				DoScanning = true;
			    }
			)

			{ IsBackground = true }.Start();
		    }

		    else
		    {
			DoScanning = false;
		    }

		    return;
		};

		Tool.Round(S3Button1, 6);

		var Container2Size = new Size(Container1Size.Width, Container1Size.Height - 30);
		var Container2Loca = new Point(0, 30);

		Control.Image(S3Container1, S3Container2, Container2Size, Container2Loca, ButtonBCol);

		var TextBoxLoca = new Point(5, 5);
		var TextBoxSize = new Size(Container2Size.Width - 10, Container2Size.Height - 10);

		Control.TextBox(S3Container2, S3TextBox1, TextBoxSize, TextBoxLoca, ButtonBCol, Color.White, 1, 7, 
		    ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

		S3TextBox1.Text = string.Format
		(
		    "Press 'F1' for optional shortcut-key functionality.\r\n" +
		    "Waiting for action....\r\n"
		);

		Tool.Round(S3Container1, 6);
		Tool.Round(S3Container2, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	

	private void Print(string data)
	{
	    S3TextBox1.AppendText($"{data}\r\n");
	}

	public static bool VerboseMode = false;
	public static bool GoveDomains = false;
	public static bool DoScanning = true;

	private void SaveLogToDevice()
	{
	    try
	    {
		using (SaveFileDialog saveFileDialog = new SaveFileDialog())
		{
		    saveFileDialog.Filter = ("Text File (*.txt)|*.txt");
		    saveFileDialog.DefaultExt = ("txt");
		    saveFileDialog.Title = ($"{DateTime.Today}");

		    saveFileDialog.CheckFileExists = false;
		    saveFileDialog.CheckPathExists = true;

		    DialogResult Result = saveFileDialog.ShowDialog();

		    if (Result != DialogResult.OK)
		    {
			Print("Process has been aborted!");
			return;
		    }
		    
		    Print("Saving file to device ....");

		    File.WriteAllText(saveFileDialog.FileName, S3TextBox1.Text);

		    Print("Done!");
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void Init4()
	{
	    try
	    {
		void AddEventHandler(Control.ControlCollection Collection, Control Control)
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
					{
					    Print(":Help Message:");
					    Print("F1 = this message,");
					    Print("F2 = scan usage help,");
					    Print("F3 = allow disbanned domains,");
					    Print("F4 = toggle verbose mode,");
					    Print("F5 = clear this log,");
					    Print("F6 = save this log to your device;");
					    //add scan help
					    break;
					}
					case Keys.F2:
					{
					    Print(":Settings Rundown:");
					    Print("Host=target specification,");
					    Print("Ports=port(s) to be scanned. supports ranges using '-' and supports specification using the ',',");
					    Print("Timeout=the amount of miliseconds to wait before returning false,");
					    Print("Protocol=the protocol to use. TCP is recommended due to its compatibility,");
					    Print("Keep-Alive=whether the connections made should be kept until the scan finishes;");
					    break;
					}
					case Keys.F3:
					{
					    GoveDomains = !GoveDomains;
					    Print($@"Government domain specification allowance has been {(GoveDomains ? "enabled" : "disabled")}!");
					    break;
					}
					case Keys.F4:
					{
					    VerboseMode = !VerboseMode;
					    Print($@"Verbose mode has been {(VerboseMode ? "enabled" : "disabled")}!");
					    break;
					}
					case Keys.F5:
					{
					    S3TextBox1.Clear();
					    break;
					}
					case Keys.F6:
					{ 
					    SaveLogToDevice();
					    break;
					}
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
		AddEventHandler(S2Container2.Controls, null);
		AddEventHandler(S2Container3.Controls, null);

		foreach (Control Control in S2Container1.Controls)
		{
		    if (Control is PictureBox && Control.Controls.Count > 0)
		    {
			AddEventHandler(null, Control.Controls[0]);
		    }
		}

		AddEventHandler(S3Container1.Controls, null);
		AddEventHandler(S3Container2.Controls, null);

		AddEventHandler(null, S1Container1);
		AddEventHandler(null, S1Container2);
		AddEventHandler(null, S1Container3);
		AddEventHandler(null, S2Container1);
		AddEventHandler(null, S2Container2);
		AddEventHandler(null, S2Container3);
		AddEventHandler(null, S3Container1);
		AddEventHandler(null, S3Container2);

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
		    Init3();
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