using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class InfoDialog : Form
    {
	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	public class GUICONTAINER
	{
	    public static void InitializeGUI(Form TOP)
	    {
		try
		{
		    TOP.StartPosition = FormStartPosition.CenterParent;
		    TOP.FormBorderStyle = FormBorderStyle.None;

		    var GUI_BCOL = Color.FromArgb(24, 24, 24);

		    TOP.BackColor = GUI_BCOL;

		    var GUI_SIZE = new Size(200, 200);

		    TOOL.Resize(TOP, GUI_SIZE);
		    TOOL.Round(TOP, 6);
		}

		catch
		{
		    throw new Exception("Help Dialog Initialize GUI.");
		};
	    }
	}

	public class MENUBARCONTAINER
	{
	    readonly static public PictureBox BAR = new PictureBox();

	    readonly static public Label TITLE = new Label();

	    readonly static public Button CLOSE = new Button();

	    public static void InitializeMEB(Form TOP)
	    {
		var BAR_SIZE = new Size(TOP.Width, 24);
		var BAR_LOCA = new Point(0, 0);
		var BAR_COLA = Color.FromArgb(12, 12, 12);

		CONTROL.Image(TOP, BAR, BAR_SIZE, BAR_LOCA, null, BAR_COLA);

		var TITLE_TEXT = "Dash Information";
		var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 10);
		var TITLE_LOCA = new Point(10, (BAR.Height - TITLE_SIZE.Height) / 2);
		var TITLE_BCOL = BAR_COLA;
		var TITLE_FCOL = Color.White;

		CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 10, TITLE_TEXT);

		var CLOSE_SIZE = new Size(50, BAR.Height);
		var CLOSE_LOCA = new Point(BAR.Width - CLOSE_SIZE.Width, 0);
		var CLOSE_BCOL = BAR_COLA;
		var CLOSE_FCOL = TITLE_FCOL;

		CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 12, "X", Color.Empty);

		CLOSE.Click += (s, e) =>
		    TOP.Close();

		var BORDER_SIZE = new Size(TOP.Width - 2, TOP.Height - BAR.Height - 1);
		var BORDER_LOCA = new Point(1, BAR.Height);
		var BORDER_COLA = BAR_COLA;

		TOOL.PaintRectangle(TOP, 2, BORDER_SIZE, BORDER_LOCA, BORDER_COLA);
		TOOL.Interactive(BAR, TOP);

		foreach (Control CON in BAR.Controls)
		{
		    TOOL.Interactive(CON, TOP);
		};
	    }
	}

	public class MESSAGECONTAINER
	{
	    readonly static public TextBox INFOCONTAINER = new TextBox() { TextAlign = HorizontalAlignment.Center };

	    public static void InitializeMEG(Form TOP)
	    {
		var INFO_SIZE = new Size(TOP.Width - 4, TOP.Height - MENUBARCONTAINER.BAR.Height - 2);
		var INFO_LOCA = new Point(2, MENUBARCONTAINER.BAR.Height);
		var INFO_BCOL = Color.MidnightBlue;
		var INFO_FCOL = Color.White;

		CONTROL.TextBox(TOP, INFOCONTAINER, INFO_SIZE, INFO_LOCA, INFO_BCOL, INFO_FCOL, 1, 9, Color.Empty, FIXEDSIZE: false, READONLY: true, SCROLLBAR: true, MULTILINE: true);

		INFOCONTAINER.Text = (
		    "Hey there, I am Dashie also known as the Developer of this application.\r\n\r\nI am very high right now but I am still coding.\r\n\r\nNice!" +
		    "\r\n\r\n" +
		    "The intention of this application was to allow people like yourself to test the bandwidth of your server legally.\r\n\r\nThis tool will offer you the ability to pick any method from the list of available methods with just a click.\r\n\r\nThe configuration is not too complicated either, I have taken user-friendly interactivity into mind while coding this." +
		    "\r\n\r\n\r\n" +
		    "This all sounds nice and all but the application is not done yet.\r\n\r\nThis is just a preview of that what awaits you people, mwahaha!!\r\n\r\nExpect a lot of updates in the future." +
		    "\r\n\r\n" +
		    "If you do find any issues during the use of this application then please let me know at KvinneKraft@protonmail.com.\r\n\r\nAs I have said, I am still working on developing this application to the fullest extent.  You can also reach out to me if you have any suggestions." +
		    "\r\n\r\n" +
		    "That is me.....DASHIE" +
		    "\r\n\r\n" +
		    "Ssshhh, I am still here!\r\n\r\nI am actually going to put more information in the actual utility section. I am soo high.....bye."
		);
	    }
	}

	public InfoDialog()
	{
	    GUICONTAINER.InitializeGUI(this);
	    MENUBARCONTAINER.InitializeMEB(this);
	    MESSAGECONTAINER.InitializeMEG(this);
	}
    }
}