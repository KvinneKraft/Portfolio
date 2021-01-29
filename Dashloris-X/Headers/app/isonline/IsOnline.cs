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
    public class IsOnline
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	public readonly DashDialog DashDialog = new DashDialog();

	private void InitializeComponent()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		var AppBColor = Color.FromArgb(6, 17, 33);
		var AppTitle = string.Format("Dashloris-X   Dash Ping");
		var AppSize = new Size(350, 225);

		DashDialog.JustInitialize(AppSize, AppTitle, AppBColor, MenuBarBColor);

		DashDialog.VisibleChanged += (s, e) =>
		{
		    if (DashDialog.Visible)
		    {
			HostTextBox.Text = DashlorisX.HostTextBox.Text;
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly Label StatusLabel = new Label() { TextAlign = ContentAlignment.MiddleCenter };

	private void InitializeStatusContainer()
	{
	    try
	    {
		var StatusText = string.Format("Status: Offline");
		var StatusSize = new Size(DashDialog.Width - 8, 24);
		var StatusLocation = new Point(4, 10 + DashDialog.MenuBar.Bar.Height);
		var StatusBColor = DashDialog.BackColor;
		var StatusFColor = Color.White;

		Control.Label(DashDialog, StatusLabel, StatusSize, StatusLocation, StatusBColor, StatusFColor, 1, 12, StatusText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox HostContainer = new PictureBox();

	private readonly TextBox HostTextBox = new TextBox() { TextAlign = HorizontalAlignment.Center, Text = "https://pugpawz.com/" };
	private readonly TextBox PortTextBox = new TextBox() { TextAlign = HorizontalAlignment.Center, Text = "65535" };

	private readonly Label HostLabel = new Label();
	private readonly Label PortLabel = new Label();

	private void InitializeHostContainer()
	{
	    var ContainerSize = new Size(DashDialog.Width - 22, 40);
	    var ContainerLocation = new Point(11, 8 + StatusLabel.Height + StatusLabel.Top);
	    var ContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Control.Image(DashDialog, HostContainer, ContainerSize, ContainerLocation, ContainerBColor);
		Tool.Round(HostContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var LabelBColor = HostContainer.BackColor;
	    var LabelFColor = Color.White;

	    var TextBoxBColor = Color.FromArgb(10, 10, 10);
	    var TextBoxFColor = Color.White;

	    var HostLabelText = string.Format("Host:");
	    var HostLabelSize = Tool.GetFontSize(HostLabelText, 10);
	    var HostLabelLocation = new Point(8, (HostContainer.Height - HostLabelSize.Height) / 2);

	    var HostBoxSize = new Size(150, 19);
	    var HostBoxLocation = new Point(HostLabelSize.Width + HostLabelLocation.X, (HostContainer.Height - HostBoxSize.Height) / 2);

	    var PortLabelText = string.Format("Port:");
	    var PortLabelSize = Tool.GetFontSize(PortLabelText, 10);
	    var PortLabelLocation = new Point(HostBoxLocation.X + HostBoxSize.Width + 5, (HostContainer.Height - PortLabelSize.Height) / 2);

	    var PortBoxSize = new Size(HostContainer.Width - PortLabelLocation.X - PortLabelSize.Width - 10, HostBoxSize.Height);
	    var PortBoxLocation = new Point(PortLabelLocation.X + PortLabelSize.Width, HostBoxLocation.Y);

	    Control GetDeepToll() =>
		HostContainer.Controls[HostContainer.Controls.Count - 1];

	    try
	    {
		Control.TextBox(HostContainer, PortTextBox, PortBoxSize, PortBoxLocation, TextBoxBColor, TextBoxFColor, 1, 9);
		Tool.Round(GetDeepToll(), 6);

		Control.TextBox(HostContainer, HostTextBox, HostBoxSize, HostBoxLocation, TextBoxBColor, TextBoxFColor, 1, 8);
		Tool.Round(GetDeepToll(), 6);

		Control.Label(HostContainer, HostLabel, HostLabelSize, HostLabelLocation, LabelBColor, LabelFColor, 1, 10, HostLabelText);
		Control.Label(HostContainer, PortLabel, PortLabelSize, PortLabelLocation, LabelBColor, LabelFColor, 1, 10, PortLabelText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox InnerOptionContainer = new PictureBox();
	private readonly PictureBox OptionContainer = new PictureBox();
	private readonly PictureBox ICMPBox = new PictureBox();
	private readonly PictureBox TCPBox = new PictureBox();

	private readonly Label ICMPTitle = new Label();
	private readonly Label TCPTitle = new Label();

	private readonly DashNet DashNet = new DashNet();

	private int GetStatus()
	{
	    try
	    {
		Thread.Sleep(1000);

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

	private readonly Button Check = new Button();

	private readonly Color ToggleOn = Color.Green;
	private readonly Color ToggleOf = Color.FromArgb(8, 8, 8);

	private void InitializeOptionContainer()
	{
	    var OContainerSize = new Size(DashDialog.Width - 22, 46);
	    var OContainerLocation = new Point(11, HostContainer.Top + HostContainer.Height + 10);
	    var OContainerBColor = HostContainer.BackColor;

	    try
	    {
		Control.Image(DashDialog, OptionContainer, OContainerSize, OContainerLocation, OContainerBColor);
		Tool.Round(OptionContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var ButtonSize = new Size(90, 26);
	    var ButtonLocation = new Point(10, 10);
	    var ButtonBColor = Color.FromArgb(3, 18, 26);//10, 10, 10);
	    var ButtonFColor = Color.White;

	    try
	    {
		Control.Button(OptionContainer, Check, ButtonSize, ButtonLocation, ButtonBColor, ButtonFColor, 1, 9, "Check");
		Tool.Round(Check, 8);

		Check.Click += (s, e) =>
		{
		    if (StatusLabel.Text != "Status: Checking ....")
		    {
			StatusLabel.Text = "Status: Checking ....";

			new Thread(() =>
			{
			    int isOnline = GetStatus();

			    if (isOnline == 1)
			    {
				StatusLabel.Text = "Status: Online!";
			    }

			    else if (isOnline == -1)
			    {
				StatusLabel.Text = "Status: Offline!";
			    }

			    else
			    {
				StatusLabel.Text = "Status: Unknown!";
			    }
			})

			{ IsBackground = true }.Start();
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var LabelBColor = OptionContainer.BackColor;
	    var LabelFColor = Color.White;

	    var ToggleBoxSize = new Size(18, 18);
	    var ToggleBoxBColor = ToggleOf;

	    var ICMPLabelText = string.Format("ICMP:");
	    var ICMPLabelSize = Tool.GetFontSize(ICMPLabelText, 10);
	    var ICMPLabelLocation = new Point(Check.Left + Check.Width + 25, (OptionContainer.Height - ICMPLabelSize.Height) / 2);

	    var ICMPBoxLocation = new Point(ICMPLabelLocation.X + ICMPLabelSize.Width + 2, (OptionContainer.Height - ToggleBoxSize.Height) / 2 - 1);

	    var TCPLabelText = string.Format("TCP:");
	    var TCPLabelSize = Tool.GetFontSize(TCPLabelText, 10);
	    var TCPLabelLocation = new Point(ICMPBoxLocation.X + ToggleBoxSize.Width + 10, (OptionContainer.Height - TCPLabelSize.Height) / 2);

	    var TCPBoxLocation = new Point(TCPLabelLocation.X + TCPLabelSize.Width + 2, (OptionContainer.Height - ToggleBoxSize.Height) / 2 - 1);

	    try
	    {
		Control.Label(OptionContainer, ICMPTitle, ICMPLabelSize, ICMPLabelLocation, LabelBColor, LabelFColor, 1, 10, ICMPLabelText);
		Control.Label(OptionContainer, TCPTitle, TCPLabelSize, TCPLabelLocation, LabelBColor, LabelFColor, 1, 10, TCPLabelText);

		Control.Image(OptionContainer, ICMPBox, ToggleBoxSize, ICMPBoxLocation, ToggleBoxBColor);

		ICMPBox.Click += (s, e) =>
		{
		    if (ICMPBox.BackColor != ToggleOn)
		    {
			ICMPBox.BackColor = ToggleOn;
			TCPBox.BackColor = ToggleOf;
		    };
		};

		Control.Image(OptionContainer, TCPBox, ToggleBoxSize, TCPBoxLocation, ToggleOn);

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
	    
	    try
	    {
		foreach (Control control in OptionContainer.Controls)
		{
		    if (control is PictureBox)
		    {
			Tool.Round(control, 6);
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private bool DoInitialize = true;

	public void Show()
	{
	    try
	    {
		if (DoInitialize)
		{
		    InitializeComponent();
		    InitializeStatusContainer();
		    InitializeHostContainer();
		    InitializeOptionContainer();

		    Tool.Resize(DashDialog, new Size(DashDialog.Width, OptionContainer.Top + OptionContainer.Height + 14));
		    Tool.PaintLine(DashDialog, DashDialog.MenuBar.Bar.BackColor, 2, new Point(0, DashDialog.Height - 2), new Point(DashDialog.Width, DashDialog.Height - 2));

		    DoInitialize = false;
		}
		
		DashDialog.ShowAsIs(ShowDialog: false);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
