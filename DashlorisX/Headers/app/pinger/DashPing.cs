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
		var BAR_COLA = Color.FromArgb(19, 36, 64);
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

		try
		{
		    Controls.Image(this, HostContainer, CONT_SIZE, CONT_LOCA, null, CONT_BCOL);
		    Tools.Round(HostContainer, 6);
		}

		catch
		{
		    throw new Exception("Host Container");
		}

		var LABEL_BCOL = HostContainer.BackColor;
		var LABEL_FCOL = Color.White;

		var TEXTBOX_BCOL = Color.FromArgb(10, 10, 10);
		var TEXTBOX_FCOL = Color.White;

		var HOSLA_TEXT = string.Format("Host:");
		var HOSLA_SIZE = Tools.GetFontSize(HOSLA_TEXT, 10);
		var HOSLA_LOCA = new Point(8, (HostContainer.Height - HOSLA_SIZE.Height) / 2);

		var HTBOX_SIZE = new Size(150, 19);
		var HTBOX_LOCA = new Point(HOSLA_SIZE.Width + HOSLA_LOCA.X, (HostContainer.Height - HTBOX_SIZE.Height) / 2);

		var POLAB_TEXT = string.Format("Port:");
		var POLAB_SIZE = Tools.GetFontSize(POLAB_TEXT, 10);
		var POLAB_LOCA = new Point(HTBOX_LOCA.X + HTBOX_SIZE.Width + 5, (HostContainer.Height - POLAB_SIZE.Height) / 2);

		var POBOX_SIZE = new Size(HostContainer.Width - PortLabel.Left - PortLabel.Width - 10, HTBOX_SIZE.Height);
		var POBOX_LOCA = new Point(PortLabel.Left + PortLabel.Width, HTBOX_LOCA.Y);

		Control GetDeepToll() =>
		    HostContainer.Controls[HostContainer.Controls.Count - 1];

		try
		{
		    Controls.TextBox(HostContainer, PortTextBox, POBOX_SIZE, POBOX_LOCA, TEXTBOX_BCOL, TEXTBOX_FCOL, 1, 10, Color.Empty);
		    Tools.Round(GetDeepToll(), 6);

		    Controls.TextBox(HostContainer, HostTextBox, HTBOX_SIZE, HTBOX_LOCA, TEXTBOX_BCOL, TEXTBOX_FCOL, 1, 8, Color.Empty);
		    Tools.Round(GetDeepToll(), 6);

		    Controls.Label(HostContainer, HostLabel, HOSLA_SIZE, HOSLA_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, HOSLA_TEXT);
		    Controls.Label(HostContainer, PortLabel, POLAB_SIZE, POLAB_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, POLAB_TEXT);
		}

		catch
		{
		    throw new Exception("Host Container Items");
		}

		var RECT_SIZE = new Size(HostContainer.Width - 2, HostContainer.Height - 2);
		var RECT_LOCA = new Point(1, 1);
		var RECT_BCOL = Color.FromArgb(8, 8, 8);

		try
		{
		    Tools.PaintRectangle(HostContainer, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
		}

		catch
		{
		    throw new Exception("Host Container Rectangle");
		}
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
	    try
	    {
		var OCON_SIZE = new Size(Width - 22, 46);
		var OCON_LOCA = new Point(11, HostContainer.Top + HostContainer.Height + 10);
		var OCON_BCOL = HostContainer.BackColor;

		try
		{
		    Controls.Image(this, OptionContainer, OCON_SIZE, OCON_LOCA, null, OCON_BCOL);
		    Tools.Round(OptionContainer, 6);
		}

		catch
		{
		    throw new Exception("Option Container");
		}

		var BUTT_SIZE = new Size(90, 26);
		var BUTT_LOCA = new Point(10, 10);
		var BUTT_BCOL = Color.FromArgb(10, 10, 10);
		var BUTT_FCOL = Color.White;

		try
		{
		    Controls.Button(OptionContainer, Check, BUTT_SIZE, BUTT_LOCA, BUTT_BCOL, BUTT_FCOL, 1, 9, "Check", Color.Empty);
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

		catch
		{
		    throw new Exception("Check Event");
		}

		var LABEL_BCOL = OptionContainer.BackColor;
		var LABEL_FCOL = Color.White;

		var PICTU_SIZE = new Size(18, 18);
		var PICTU_BCOL = ToggleOf;
		var PICTU_LOCA = (OptionContainer.Height - PICTU_SIZE.Height) / 2 - 1;

		var ICMPL_TEXT = string.Format("ICMP:");
		var ICMPL_SIZE = Tools.GetFontSize(ICMPL_TEXT, 10);
		var ICMPL_LOCA = new Point(Check.Left + Check.Width + 25, (OptionContainer.Height - ICMPL_SIZE.Height) / 2);

		var TCPL_TEXT = string.Format("TCP:");
		var TCPL_SIZE = Tools.GetFontSize(TCPL_TEXT, 10);
		var TCPL_LOCA = new Point(ICMPBox.Left + ICMPBox.Width + 10, (OptionContainer.Height - TCPL_SIZE.Height) / 2);

		var ICMPB_LOCA = new Point(ICMPTitle.Left + ICMPTitle.Width + 2, PICTU_LOCA);
		var TCPB_LOCA = new Point(TCPTitle.Left + TCPTitle.Width + 2, PICTU_LOCA);

		try
		{
		    Controls.Label(OptionContainer, ICMPTitle, ICMPL_SIZE, ICMPL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, ICMPL_TEXT);
		    Controls.Label(OptionContainer, TCPTitle, TCPL_SIZE, TCPL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, TCPL_TEXT);

		    Controls.Image(OptionContainer, ICMPBox, PICTU_SIZE, ICMPB_LOCA, null, PICTU_BCOL);

		    ICMPBox.Click += (s, e) =>
		    {
			if (ICMPBox.BackColor != ToggleOn)
			{
			    ICMPBox.BackColor = ToggleOn;
			    TCPBox.BackColor = ToggleOf;
			};
		    };
		    
		    Controls.Image(OptionContainer, TCPBox, PICTU_SIZE, TCPB_LOCA, null, ToggleOn);

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

		var RECT_SIZE = new Size(OptionContainer.Width - 2, OptionContainer.Height - 2);
		var RECT_LOCA = new Point(1, 1);
		var RECT_BCOL = Color.FromArgb(8, 8, 8);

		try
		{
		    Tools.PaintRectangle(OptionContainer, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);

		    foreach (Control control in OptionContainer.Controls)
		    {
			if (control is PictureBox)
			{
			    Tools.Round(control, 6);
			}
		    }
		}

		catch
		{
		    throw new Exception("Option Container Rectangle");
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
