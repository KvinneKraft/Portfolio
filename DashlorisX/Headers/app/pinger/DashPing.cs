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

	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X   Pinger", minim:false);
	readonly DashTools Tools = new DashTools();

	private void InitializeMenuBar()
	{
	    try
	    {
		var BAR_COLA = Color.FromArgb(8, 8, 8);
		MenuBar.Add(this, 26, BAR_COLA, BAR_COLA);
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
		var STAT_TEXT = string.Format("Status: Offline");
		var STAT_SIZE = new Size(Width - 8, 24);
		var STAT_LOCA = new Point(4, 10 + MenuBar.Bar.Height);
		var STAT_BCOL = BackColor;
		var STAT_FCOL = Color.White;

		Controls.Label(this, StatusLabel, STAT_SIZE, STAT_LOCA, STAT_BCOL, STAT_FCOL, 1, 12, STAT_TEXT);
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
	    try
	    {
		var CONT_SIZE = new Size(Width - 22, 40);
		var CONT_LOCA = new Point(11, 8 + StatusLabel.Height + StatusLabel.Top);
		var CONT_BCOL = Color.FromArgb(9, 39, 66); //ControlContainer.BackColor;

		Controls.Image(this, HostContainer, CONT_SIZE, CONT_LOCA, null, CONT_BCOL);
		Tools.Round(HostContainer, 6);

		var LABEL_BCOL = HostContainer.BackColor;
		var LABEL_FCOL = Color.White;

		var HOSLA_TEXT = string.Format("Host:");
		var HOSLA_SIZE = Tools.GetFontSize(HOSLA_TEXT, 10);
		var HOSLA_LOCA = new Point(8, (HostContainer.Height - HOSLA_SIZE.Height) / 2);

		Controls.Label(HostContainer, HostLabel, HOSLA_SIZE, HOSLA_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, HOSLA_TEXT);

		Control GetDeepToll() =>
		    HostContainer.Controls[HostContainer.Controls.Count - 1];

		var TBOX_BCOL = Color.FromArgb(10, 10, 10);
		var TBOX_FCOL = Color.White;

		var HTBOX_SIZE = new Size(150, 19);
		var HTBOX_LOCA = new Point(HOSLA_SIZE.Width + HOSLA_LOCA.X, (HostContainer.Height - HTBOX_SIZE.Height) / 2);

		Controls.TextBox(HostContainer, HostTextBox, HTBOX_SIZE, HTBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 8, Color.Empty);
		Tools.Round(GetDeepToll(), 6);

		var POLAB_TEXT = string.Format("Port:");
		var POLAB_SIZE = Tools.GetFontSize(POLAB_TEXT, 10);
		var POLAB_LOCA = new Point(HTBOX_LOCA.X + HTBOX_SIZE.Width + 5, (HostContainer.Height - POLAB_SIZE.Height) / 2);

		Controls.Label(HostContainer, PortLabel, POLAB_SIZE, POLAB_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, POLAB_TEXT);

		var POBOX_SIZE = new Size(HostContainer.Width - PortLabel.Left - PortLabel.Width - 10, HTBOX_SIZE.Height);
		var POBOX_LOCA = new Point(PortLabel.Left + PortLabel.Width, HTBOX_LOCA.Y);

		Controls.TextBox(HostContainer, PortTextBox, POBOX_SIZE, POBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 10, Color.Empty);
		Tools.Round(GetDeepToll(), 6);

		var RECT_SIZE = new Size(HostContainer.Width - 2, HostContainer.Height - 2);
		var RECT_LOCA = new Point(1, 1);
		var RECT_BCOL = Color.FromArgb(8, 8, 8);

		Tools.PaintRectangle(HostContainer, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
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

	private string GetHost()
	{
	    var r_host = HostTextBox.Text.ToLower();
	    
	    if (!IPAddress.TryParse(r_host, out IPAddress ham))
	    {
		if (!r_host.Contains("http://") && !r_host.Contains("https://"))
		{
		    r_host = "https://" + r_host;
		}

		if (!Uri.TryCreate(r_host, UriKind.RelativeOrAbsolute, out Uri bacon))
		{
		    MessageBox.Show("Invalid URL specified.  Retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		    return string.Empty;
		};

		try
		{
		    r_host = Dns.GetHostAddresses(bacon.Host)[0].ToString();
		}

		catch
		{
		    MessageBox.Show("Invalid host specified.  Retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		    return string.Empty;
		}
	    }

	    else
	    {
		r_host = ham.ToString();

		if (ham.AddressFamily != AddressFamily.InterNetwork)
		{
		    MessageBox.Show("Invalid IPv4 address specified.  Retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		    return string.Empty;
		}
	    }

	    if (r_host.Length < 7 || r_host == string.Empty)
	    {
		MessageBox.Show("No host was specified.  Retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		return string.Empty;
	    }

	    return r_host;
	}

	private int GetPort()
	{
	    bool isInteger = int.TryParse(PortTextBox.Text, out int result);

	    if (result < 1 || result > 65535 || !isInteger)
	    {
		MessageBox.Show("No or invalid integer was specified.  Retry!", "Port Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		result = -1;
	    };

	    return result;
	}

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

		string host = GetHost();
		int port = GetPort();

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
	    try
	    {
		var OCON_SIZE = new Size(Width - 22, 46);
		var OCON_LOCA = new Point(11, HostContainer.Top + HostContainer.Height + 10);
		var OCON_BCOL = HostContainer.BackColor;

		Controls.Image(this, OptionContainer, OCON_SIZE, OCON_LOCA, null, OCON_BCOL);
		Tools.Round(OptionContainer, 6);

		var BUTT_SIZE = new Size(90, 26);
		var BUTT_LOCA = new Point(10, 10);
		var BUTT_BCOL = Color.FromArgb(10, 10, 10);//23, 33, 51);
		var BUTT_FCOL = Color.White;

		Controls.Button(OptionContainer, Check, BUTT_SIZE, BUTT_LOCA, BUTT_BCOL, BUTT_FCOL, 1, 9, "Check", Color.Empty);
		Tools.Round(Check, 8);

		Check.Click += (s, e) =>
		{
		    int isOnline = IsOnline();

		    if (isOnline == 1)
		    {
			MessageBox.Show("The server is online!  Click OK to exit this dialog.", "Ping Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
			StatusLabel.Text = "Status: Online!";
		    }

		    else if (isOnline == -1)
		    {
			MessageBox.Show("The server, unfortunately, is offline!  You may want to specify a different port (80 or so).", "Ping Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
			StatusLabel.Text = "Status: Offline!";
		    }

		    else
		    {
			StatusLabel.Text = "Status: Unknown!";
		    }
		};

		var LABEL_BCOL = OptionContainer.BackColor;
		var LABEL_FCOL = Color.White;

		var PICTU_SIZE = new Size(18, 18);
		var PICTU_BCOL = ToggleOf;
		var PICTU_LOCA = (OptionContainer.Height - PICTU_SIZE.Height) / 2 - 1;

		var ICMPL_TEXT = string.Format("ICMP:");
		var ICMPL_SIZE = Tools.GetFontSize(ICMPL_TEXT, 10);
		var ICMPL_LOCA = new Point(Check.Left + Check.Width + 25, (OptionContainer.Height - ICMPL_SIZE.Height) / 2);

		Controls.Label(OptionContainer, ICMPTitle, ICMPL_SIZE, ICMPL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, ICMPL_TEXT);

		var ICMPB_LOCA = new Point(ICMPTitle.Left + ICMPTitle.Width + 2, PICTU_LOCA);

		Controls.Image(OptionContainer, ICMPBox, PICTU_SIZE, ICMPB_LOCA, null, PICTU_BCOL);

		ICMPBox.Click += (s, e) =>
		{
		    if (ICMPBox.BackColor != ToggleOn)
		    {
			ICMPBox.BackColor = ToggleOn;
			TCPBox.BackColor = ToggleOf;
		    };
		};

		var TCPL_TEXT = string.Format("TCP:");
		var TCPL_SIZE = Tools.GetFontSize(TCPL_TEXT, 10);
		var TCPL_LOCA = new Point(ICMPBox.Left + ICMPBox.Width + 10, (OptionContainer.Height - TCPL_SIZE.Height) / 2);

		Controls.Label(OptionContainer, TCPTitle, TCPL_SIZE, TCPL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, TCPL_TEXT);

		var TCPB_LOCA = new Point(TCPTitle.Left + TCPTitle.Width + 2, PICTU_LOCA);

		Controls.Image(OptionContainer, TCPBox, PICTU_SIZE, TCPB_LOCA, null, ToggleOn/*PICTU_BCOL*/);

		TCPBox.Click += (s, e) =>
		{
		    if (TCPBox.BackColor != ToggleOn)
		    {
			ICMPBox.BackColor = ToggleOf;
			TCPBox.BackColor = ToggleOn;
		    };
		};

		var RECT_SIZE = new Size(OptionContainer.Width - 2, OptionContainer.Height - 2);
		var RECT_LOCA = new Point(1, 1);
		var RECT_BCOL = Color.FromArgb(8, 8, 8);

		Tools.PaintRectangle(OptionContainer, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);

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
