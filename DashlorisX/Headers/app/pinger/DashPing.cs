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

		var LABEL_TEXT = string.Format("Host:");
		var LABEL_SIZE = Tools.GetFontSize(LABEL_TEXT, 10);
		var LABEL_LOCA = new Point(8, (HostContainer.Height - LABEL_SIZE.Height) / 2);
		var LABEL_BCOL = HostContainer.BackColor;
		var LABEL_FCOL = Color.White;

		Controls.Label(HostContainer, HostLabel, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, LABEL_TEXT);

		Control GetDeepToll() =>
		    HostContainer.Controls[HostContainer.Controls.Count - 1];

		var TBOX_SIZE = new Size(150, 19);
		var TBOX_LOCA = new Point(LABEL_SIZE.Width + LABEL_LOCA.X, (HostContainer.Height - TBOX_SIZE.Height) / 2);
		var TBOX_BCOL = Color.FromArgb(10, 10, 10);
		var TBOX_FCOL = Color.White;

		Controls.TextBox(HostContainer, HostTextBox, TBOX_SIZE, TBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 8, Color.Empty);
		Tools.Round(GetDeepToll(), 6);

		LABEL_TEXT = string.Format("Port:");
		LABEL_SIZE = Tools.GetFontSize(LABEL_TEXT, 10);
		LABEL_LOCA = new Point(TBOX_LOCA.X + TBOX_SIZE.Width + 5, (HostContainer.Height - LABEL_SIZE.Height) / 2);

		Controls.Label(HostContainer, PortLabel, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, LABEL_TEXT);

		TBOX_SIZE = new Size(HostContainer.Width - PortLabel.Left - PortLabel.Width - 10, TBOX_SIZE.Height);
		TBOX_LOCA = new Point(PortLabel.Left + PortLabel.Width, TBOX_LOCA.Y);

		Controls.TextBox(HostContainer, PortTextBox, TBOX_SIZE, TBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 10, Color.Empty);
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

	private bool IsOnline()
	{
	    try
	    {

		return true;
	    }

	    catch
	    {
		return false;
	    }
	}

	readonly Button Check = new Button();

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
		    if (!IsOnline())
		    {
			StatusLabel.Text = "Status: Online!";
		    }

		    else
		    {
			StatusLabel.Text = "Status: Offline!";
		    }
		};

		var LABEL_BCOL = OptionContainer.BackColor;
		var LABEL_FCOL = Color.White;

		var ICMPL_TEXT = string.Format("ICMP:");
		var ICMPL_SIZE = Tools.GetFontSize(ICMPL_TEXT, 10);
		var ICMPL_LOCA = new Point(Check.Left + Check.Width + 10, (OptionContainer.Height - ICMPL_SIZE.Height) / 2);

		Controls.Label(OptionContainer, ICMPTitle, ICMPL_SIZE, ICMPL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, ICMPL_TEXT);

		var RECT_SIZE = new Size(OptionContainer.Width - 2, OptionContainer.Height - 2);
		var RECT_LOCA = new Point(1, 1);
		var RECT_BCOL = Color.FromArgb(8, 8, 8);

		Tools.PaintRectangle(OptionContainer, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
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
