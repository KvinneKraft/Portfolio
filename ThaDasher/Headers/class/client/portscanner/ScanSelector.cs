using System;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class ScanSelector : Form
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	readonly PictureBox B = new PictureBox();
	readonly Button X = new Button();
	readonly Label T = new Label();

	private void LoadMenuBar()
	{
	    var BAR_SIZE = new Size(Width, 26);
	    var BAR_LOCA = new Point(0, 0);
	    var BAR_BCOL = Color.FromArgb(8,8,8);

	    CONTROL.Image(this, B, BAR_SIZE, BAR_LOCA, null, BAR_BCOL);

	    var CLOSE_SIZE = new Size(65, B.Height);
	    var CLOSE_LOCA = new Point(B.Width - CLOSE_SIZE.Width, 0);
	    var CLOSE_BCOL = B.BackColor;
	    var CLOSE_FCOL = Color.White;

	    CONTROL.Button(B, X, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 10, "X", Color.Empty);

	    X.Click += (s, e) =>
	    {
		Hide();
	    };

	    var TITLE_TEXT = "Port Scan Selector";
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 8);
	    var TITLE_LOCA = new Point(10, (B.Height - TITLE_SIZE.Height) / 2);
	    var TITLE_BCOL = BAR_BCOL;
	    var TITLE_FCOL = Color.White;

	    CONTROL.Label(B, T, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, TITLE_TEXT);

	    TOOL.Interactive(T, this);
	    TOOL.Interactive(B, this);

	    var RECT_SIZE = new Size(Width - 2, Height - B.Height - 1);
	    var RECT_LOCA = new Point(1, B.Height - 1);
	    var RECT_BCOL = B.BackColor;

	    TOOL.PaintRectangle(this, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	}

	private void LoadGLayout()
	{
	    Text = "Port Scan Selector";

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterParent;

	    Icon = Properties.Resources.ICON_ICO;
	    BackColor = Color.MidnightBlue;

	    MinimizeBox = false;
	    MaximizeBox = false;

	    TOOL.Resize(this, new Size(200, 120));
	    TOOL.Round(this, 6);
	}

	readonly PictureBox VERSION_CONTAINER = new PictureBox();

	readonly Button VERSION_1 = new Button();
	readonly Button VERSION_2 = new Button();

	readonly PortScanner2 PORTSCANNER2 = new PortScanner2();
	readonly PortScanner PORTSCANNER1 = new PortScanner();

	private void LoadOptions()
	{
	    var CONT_SIZE = new Size(150, 64);
	    var CONT_LOCA = new Point((Width - CONT_SIZE.Width) / 2, (Height + (B.Height - 2) - CONT_SIZE.Height) / 2);
	    var CONT_BCOL = BackColor;

	    CONTROL.Image(this, VERSION_CONTAINER, CONT_SIZE, CONT_LOCA, null, CONT_BCOL);

	    var BUTT_SIZE = new Size(150, 27);
	    var BUTT_LOCA = new Point(0, 0);
	    var BUTT_BCOL = Color.FromArgb(8, 8, 8);
	    var BUTT_FCOL = Color.White;

	    CONTROL.Button(VERSION_CONTAINER, VERSION_1, BUTT_SIZE, BUTT_LOCA, BUTT_BCOL, BUTT_FCOL, 1, 10, "Version 1", Color.Empty);

	    VERSION_1.Click += (s, e) =>
	    {
		Hide();

		PORTSCANNER1.Show();
		PORTSCANNER2.BringToFront();
	    };

	    BUTT_LOCA.Y += BUTT_SIZE.Height + 10;

	    CONTROL.Button(VERSION_CONTAINER, VERSION_2, BUTT_SIZE, BUTT_LOCA, BUTT_BCOL, BUTT_FCOL, 1, 10, "Version 2", Color.Empty);

	    VERSION_2.Click += (s, e) =>
	    {
		Hide();

		PORTSCANNER2.Show();
		PORTSCANNER2.BringToFront();
	    };

	    foreach (Button OBJECT in VERSION_CONTAINER.Controls)
	    {
		TOOL.Round(OBJECT, 4);
	    }
	}

	public ScanSelector()
	{
	    try
	    {
		LoadGLayout();
		LoadMenuBar();
		LoadOptions();
	    }

	    catch
	    {
		Environment.Exit(-1);
	    }
	}
    }
}