
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class AboutDialog : Form
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();


	private void LoadGLayout()
	{
	    Text = "Information";

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterParent;

	    ShowInTaskbar = false;

	    BackColor = Color.MidnightBlue;

	    MinimizeBox = false;
	    MaximizeBox = false;

	    TOOL.Resize(this, new Size(250, 250));
	    TOOL.Round(this, 6);
	}

	readonly private PictureBox B = new PictureBox();
	readonly private Button X = new Button();
	readonly private Label T = new Label();

	private void LoadMenuBar()
	{
	    var BAR_SIZE = new Size(Width - 1, 26);
	    var BAR_LOCA = new Point(1, 0);
	    var BAR_BCOL = Color.FromArgb(8, 8, 8);

	    CONTROL.Image(this, B, BAR_SIZE, BAR_LOCA, null, BAR_BCOL);

	    var CLOSE_SIZE = new Size(55, BAR_SIZE.Height);
	    var CLOSE_LOCA = new Point(BAR_SIZE.Width - CLOSE_SIZE.Width, 0);
	    var CLOSE_BCOL = BAR_BCOL;
	    var CLOSE_FCOL = Color.White;

	    CONTROL.Button(B, X, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 10, "X", Color.Empty);

	    X.Click += (s, e) =>
	    {
		Hide();
	    };

	    var TITLE_TEXT = "DASH Information";
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 8);
	    var TITLE_LOCA = new Point(10, (B.Height - TITLE_SIZE.Height) / 2);
	    var TITLE_BCOL = BAR_BCOL;
	    var TITLE_FCOL = Color.White;

	    CONTROL.Label(B, T, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, TITLE_TEXT);

	    var RECT_SIZE = new Size(Width - 2, Height - B.Height - 1);
	    var RECT_LOCA = new Point(1, B.Height - 1);
	    var RECT_BCOL = B.BackColor;

	    TOOL.PaintRectangle(this, 3, RECT_SIZE, RECT_LOCA, RECT_BCOL);

	    TOOL.Interactive(B, this);
	}

	readonly private Label D_T = new Label(); // Description Title
	readonly private TextBox D_D = new TextBox(); // Description Description
	readonly private PictureBox D_C = new PictureBox(); // Description Container

	private void LoadInforma()
	{
	    var TITLE_TEXT = "About Tha Dasher";
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 14);
	    var TITLE_LOCA = new Point(-1, B.Height + 10);
	    var TITLE_BCOL = BackColor;
	    var TITLE_FCOL = Color.White;

	    CONTROL.Label(this, D_T, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 14, TITLE_TEXT);

	    var CONT_SIZE = new Size(Width - 26, 162);
	    var CONT_LOCA = new Point(13, D_T.Top + D_T.Height + 10);
	    var CONT_BCOL = BackColor;

	    CONTROL.Image(this, D_C, CONT_SIZE, CONT_LOCA, null, CONT_BCOL);

	    var DESC_TEXT = "Hey there, thank you for using Tha Dasher!\r\n\r\nIt was a long time coming because of all of the work that went into this majestic piece of craft but I hope you find the right use for it because that would be nice.\r\n\r\nFor those who were too lazy to read any of the other dialogs....this tool will allow you to stress-test the bandwidth of your network or someone else their network (Assuming you have got their permission.).\r\n\r\nYou may make use of an enormous list of available and effective methods and modules that will probably get the job done.\r\n\r\nIf you have any suggestions or simply want to contact me, you can find me at KvinneKraft@protonmail.com.\r\n\r\nFor those curious rats out there, this entire project is open source, you can find the open source version at my github here: https://github.com.\r\n\r\nPlease know that wrong use can cause your network to slightly die out because of the power it can generate.\r\n\r\n-Dashie";
	    var DESC_SIZE = new Size(Width - 30, 159);
	    var DESC_LOCA = new Point(2, 2);
	    var DESC_BCOL = BackColor;
	    var DESC_FCOL = Color.White;
	    
	    D_D.Text = DESC_TEXT;

	    CONTROL.TextBox(D_C, D_D, DESC_SIZE, DESC_LOCA, DESC_BCOL, DESC_FCOL, 1, 8, Color.Empty, READONLY:true, MULTILINE:true, SCROLLBAR:true, FIXEDSIZE:false);

	    var RECT_SIZE = new Size(D_D.Width + 3, D_D.Height + 3);
	    var RECT_LOCA = new Point(0, 0);
	    var RECT_BCOL = B.BackColor;

	    TOOL.PaintRectangle(D_C, 4, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	    TOOL.Round(D_C, 6);
	}

	public AboutDialog()
	{
	    try
	    {
		LoadGLayout();
		LoadMenuBar();
		LoadInforma();
	    }

	    catch
	    {
		Application.Exit();
	    }
	}
    }
}
