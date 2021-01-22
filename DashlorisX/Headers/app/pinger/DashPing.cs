// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

using DashlorisX.Properties;

namespace DashlorisX
{
    public class DashPing : Form
    {
	new readonly DashControls Controls = new DashControls();

	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X   Pinger", minim: false);
	readonly DashTools Tools = new DashTools();

	private void InitializeMenuBar()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		MenuBar.Add(this, 26, MenuBarBColor, MenuBarBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void InitializeLayout()
	{
	    try
	    {
		BackColor = Color.FromArgb(6, 17, 33);
		Tools.Round(this, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly Label StatusLabel = new Label() { TextAlign = ContentAlignment.MiddleCenter };

	private void InitializeStatusContainer()
	{
	    try
	    {
		var StatusText = string.Format("Status: Offline");
		var StatusSize = new Size(Width - 8, 24);
		var StatusLocation = new Point(4, 10 + MenuBar.Bar.Height);
		var StatusBColor = BackColor;
		var StatusFColor = Color.White;

		Controls.Label(this, StatusLabel, StatusSize, StatusLocation, StatusBColor, StatusFColor, 1, 12, StatusText);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox HostContainer = new PictureBox();

	readonly TextBox HostTextBox = new TextBox() { TextAlign = HorizontalAlignment.Center, Text = "https://pugpawz.com/" };
	readonly TextBox PortTextBox = new TextBox() { TextAlign = HorizontalAlignment.Center, Text = "65535" };

	readonly Label HostLabel = new Label();
	readonly Label PortLabel = new Label();

	private void InitializeHostContainer()
	{
	    var ContainerSize = new Size(Width - 22, 40);
	    var ContainerLocation = new Point(11, 8 + StatusLabel.Height + StatusLabel.Top);
	    var ContainerBColor = Color.FromArgb(9, 39, 66); //ControlContainer.BackColor;

	    try
	    {
		Controls.Image(this, HostContainer, ContainerSize, ContainerLocation, null, ContainerBColor);
		Tools.Round(HostContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var LabelBColor = HostContainer.BackColor;
	    var LabelFColor = Color.White;

	    var TextBoxBColor = Color.FromArgb(10, 10, 10);
	    var TextBoxFColor = Color.White;

	    var HostLabelText = string.Format("Host:");
	    var HostLabelSize = Tools.GetFontSize(HostLabelText, 10);
	    var HostLabelLocation = new Point(8, (HostContainer.Height - HostLabelSize.Height) / 2);

	    var HostBoxSize = new Size(150, 19);
	    var HostBoxLocation = new Point(HostLabelSize.Width + HostLabelLocation.X, (HostContainer.Height - HostBoxSize.Height) / 2);

	    var PortLabelText = string.Format("Port:");
	    var PortLabelSize = Tools.GetFontSize(PortLabelText, 10);
	    var PortLabelLocation = new Point(HostBoxLocation.X + HostBoxSize.Width + 5, (HostContainer.Height - PortLabelSize.Height) / 2);

	    var PortBoxSize = new Size(HostContainer.Width - PortLabel.Left - PortLabel.Width - 10, HostBoxSize.Height);
	    var PortBoxLocation = new Point(PortLabel.Left + PortLabel.Width, HostBoxLocation.Y);

	    Control GetDeepToll() =>
		HostContainer.Controls[HostContainer.Controls.Count - 1];

	    try
	    {
		Controls.TextBox(HostContainer, PortTextBox, PortBoxSize, PortBoxLocation, TextBoxBColor, TextBoxFColor, 1, 10, Color.Empty);
		Tools.Round(GetDeepToll(), 6);

		Controls.TextBox(HostContainer, HostTextBox, HostBoxSize, HostBoxLocation, TextBoxBColor, TextBoxFColor, 1, 8, Color.Empty);
		Tools.Round(GetDeepToll(), 6);

		Controls.Label(HostContainer, HostLabel, HostLabelSize, HostLabelLocation, LabelBColor, LabelFColor, 1, 10, HostLabelText);
		Controls.Label(HostContainer, PortLabel, PortLabelSize, PortLabelLocation, LabelBColor, LabelFColor, 1, 10, PortLabelText);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var RectangleSize = new Size(HostContainer.Width - 2, HostContainer.Height - 2);
	    var RectangleLocation = new Point(1, 1);
	    var RectangleBColor = Color.FromArgb(8, 8, 8);

	    try
	    {
		Tools.PaintRectangle(HostContainer, 2, RectangleSize, RectangleLocation, RectangleBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox InnerOptionContainer = new PictureBox();
	readonly PictureBox OptionContainer = new PictureBox();
	readonly PictureBox ICMPBox = new PictureBox();
	readonly PictureBox TCPBox = new PictureBox();

	readonly Label ICMPTitle = new Label();
	readonly Label TCPTitle = new Label();

	readonly DashNet DashNet = new DashNet();

	private int IsOnline()
	{
	    try
	    {
		var ProtType = ProtocolType.Tcp;

		if (ICMPBox.BackColor == ToggleOn)
		{
		    ProtType = ProtocolType.Icmp;
		}

		var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtType);

		string host = DashNet.GetIP(HostTextBox.Text);
		int port = DashNet.GetPort(PortTextBox.Text);

		if (host == string.Empty || port == -1)
		{
		    return -2;
		}

		var resu = sock.BeginConnect(host, port, null, null);
		var succ = resu.AsyncWaitHandle.WaitOne(500, true);
		
		if (sock.Connected)
		{
		    return 1;
		}

		else
		{
		    return -1;
		}
	    }

	    catch
	    {
		return -1;
	    }
	}

	readonly Button Check = new Button();

	readonly Color ToggleOn = Color.Green;
	readonly Color ToggleOf = Color.FromArgb(8, 8, 8);

	private void InitializeOptionContainer()
	{
	    var OContainerSize = new Size(Width - 22, 46);
	    var OContainerLocation = new Point(11, HostContainer.Top + HostContainer.Height + 10);
	    var OContainerBColor = HostContainer.BackColor;

	    try
	    {
		Controls.Image(this, OptionContainer, OContainerSize, OContainerLocation, null, OContainerBColor);
		Tools.Round(OptionContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var ButtonSize = new Size(90, 26);
	    var ButtonLocation = new Point(10, 10);
	    var ButtonBColor = Color.FromArgb(10, 10, 10);
	    var ButtonFColor = Color.White;

	    try
	    {
		Controls.Button(OptionContainer, Check, ButtonSize, ButtonLocation, ButtonBColor, ButtonFColor, 1, 9, "Check", Color.Empty);
		Tools.Round(Check, 8);

		Check.Click += (s, e) =>
		{
		    StatusLabel.Text = "Status: Checking ....";

		    int isOnline = IsOnline();

		    if (isOnline == 1)
		    {
			StatusLabel.Text = "Status: Online!";
			MessageBox.Show("The server is online!  Click OK to exit this dialog.", "Ping Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
		    }

		    else if (isOnline == -1)
		    {
			StatusLabel.Text = "Status: Offline!";
			MessageBox.Show("The server, unfortunately, is offline!  You may want to specify a different port (80 or so).", "Ping Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
		    }

		    else
		    {
			StatusLabel.Text = "Status: Unknown!";
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var LabelBColor = OptionContainer.BackColor;
	    var LabelFColor = Color.White;

	    var ToggleBoxSize = new Size(18, 18);
	    var ToggleBoxLocation = (OptionContainer.Height - ToggleBoxSize.Height) / 2 - 1;
	    var ToggleBoxBColor = ToggleOf;

	    var ICMPLabelText = string.Format("ICMP:");
	    var ICMPLabelSize = Tools.GetFontSize(ICMPLabelText, 10);
	    var ICMPLabelLocation = new Point(Check.Left + Check.Width + 25, (OptionContainer.Height - ICMPLabelSize.Height) / 2);

	    var TCPLabelText = string.Format("TCP:");
	    var TCPLabelSize = Tools.GetFontSize(TCPLabelText, 10);
	    var TCPLabelLocation = new Point(ICMPBox.Left + ICMPBox.Width + 10, (OptionContainer.Height - TCPLabelSize.Height) / 2);

	    var ICMPB_LOCA = new Point(ICMPTitle.Left + ICMPTitle.Width + 2, ToggleBoxLocation);
	    var TCPB_LOCA = new Point(TCPTitle.Left + TCPTitle.Width + 2, ToggleBoxLocation);

	    try
	    {
		Controls.Label(OptionContainer, ICMPTitle, ICMPLabelSize, ICMPLabelLocation, LabelBColor, LabelFColor, 1, 10, ICMPLabelText);
		Controls.Label(OptionContainer, TCPTitle, TCPLabelSize, TCPLabelLocation, LabelBColor, LabelFColor, 1, 10, TCPLabelText);

		Controls.Image(OptionContainer, ICMPBox, ToggleBoxSize, ICMPB_LOCA, null, ToggleBoxBColor);

		ICMPBox.Click += (s, e) =>
		{
		    if (ICMPBox.BackColor != ToggleOn)
		    {
			ICMPBox.BackColor = ToggleOn;
			TCPBox.BackColor = ToggleOf;
		    };
		};

		Controls.Image(OptionContainer, TCPBox, ToggleBoxSize, TCPB_LOCA, null, ToggleOn);

		TCPBox.Click += (s, e) =>
		{
		    if (TCPBox.BackColor != ToggleOn)
		    {
			ICMPBox.BackColor = ToggleOf;
			TCPBox.BackColor = ToggleOn;
		    };
		};
	    }

	    catch
	    {
		throw new Exception("Option Container Controls");
	    }

	    var RectangleSize = new Size(OptionContainer.Width - 2, OptionContainer.Height - 2);
	    var RectangleLocation = new Point(1, 1);
	    var RectangleBColor = Color.FromArgb(8, 8, 8);

	    try
	    {
		Tools.PaintRectangle(OptionContainer, 2, RectangleSize, RectangleLocation, RectangleBColor);

		foreach (Control control in OptionContainer.Controls)
		{
		    if (control is PictureBox)
		    {
			Tools.Round(control, 6);
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public DashPing()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeLayout();
		InitializeStatusContainer();
		InitializeHostContainer();
		InitializeOptionContainer();

		Tools.Resize(this, new Size(Width, OptionContainer.Top + OptionContainer.Height + 14));
		Tools.PaintLine(this, MenuBar.Bar.BackColor, 2, new Point(0, Height - 2), new Point(Width, Height -2));
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(350, 225);
	    MinimumSize = new Size(350, 225);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Dash Ping";
	    Tag = "DashlorisX Dash Ping";
	    Name = "Dash Ping";

	    Icon = Resources.ICON;

	    ResumeLayout(false);
	}
    }
}
