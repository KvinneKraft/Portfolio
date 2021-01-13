//
// Finally some decent code :smh:
//
// Author: Dashie
// Version: 1.0
//

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public partial class ToS : Form
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	private void LoadGUILayout()
	{
	    Text = "Terms of Service Wall";

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterScreen;

	    ShowInTaskbar = false;

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
		MessageBox.Show("You must accept the terms of service in order to use my software.\r\n\r\nClick OK to exit the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		Application.Exit();
	    };

	    var TITLE_TEXT = "DASH ToS";
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 8);
	    var TITLE_LOCA = new Point(10, (B.Height - TITLE_SIZE.Height) / 2);
	    var TITLE_BCOL = BAR_BCOL;
	    var TITLE_FCOL = Color.White;

	    CONTROL.Label(B, T, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, TITLE_TEXT);

	    TOOL.Interactive(B, this);

	    var RECT_SIZE = new Size(Width - 1, Height - B.Height - 1);
	    var RECT_LOCA = new Point(1, B.Height);
	    var RECT_BCOL = B.BackColor;

	    TOOL.PaintRectangle(this, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	}

	readonly private TextBox L = new TextBox();
	readonly private Button A = new Button();
	
	private string TermsOfService()
	{
	    return string.Format
	    (
		"(1) When using this application you automatically agree with the Terms of Services.\r\n\r\n" +
		"(2) When using this application you automatically confirm you claim responsibility for any use if any.\r\n\r\n" +
		"(3) When using this application you automatically confirm you are aware of the impact this application can have when used wrongly.\r\n\r\n" +
		"(4) When using this application you automatically confirm you are aware of the laws corresponding to DDoS/DoS/Flood attacks in your country.\r\n\r\n" +
		"(5) When using this application you automatically confirm I Dashie am not responsible for any harm inflicted upon any using this application or any of its sub-components.\r\n\r\n"
	    );
	}

	private void LoadToS()
	{
	    var LOG_SIZE = new Size(Width - 4, Height - B.Height - 2);
	    var LOG_LOCA = new Point(2, B.Height + 1);
	    var LOG_BCOL = Color.MidnightBlue;
	    var LOG_FCOL = Color.White;

	    CONTROL.TextBox(this, L, LOG_SIZE, LOG_LOCA, LOG_BCOL, LOG_FCOL, 1, 9, Color.Empty, READONLY: true, FIXEDSIZE:false, SCROLLBAR:true, MULTILINE:true);

	    L.Text = TermsOfService();

	    var AGREE_SIZE = new Size(100, 26);
	    var AGREE_LOCA = new Point((L.Width - AGREE_SIZE.Width) / 2, L.Height - AGREE_SIZE.Height - 8);
	    var AGREE_BCOL = Color.FromArgb(16, 16, 16);
	    var AGREE_FCOL = Color.White;

	    CONTROL.Button(L, A, AGREE_SIZE, AGREE_LOCA, AGREE_BCOL, AGREE_FCOL, 1, 10, "Agree", Color.Empty);

	    A.Click += (s, e) =>
	    {
		Close();
	    };

	    TOOL.Round(A, 6);
	}

	public ToS()
	{
	    try
	    {
		LoadGUILayout();
		LoadMenuBar();
		LoadToS();
	    }

	    catch
	    {
		Environment.Exit(-1);
	    }
	}
    }
}
