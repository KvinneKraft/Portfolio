using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class LoginDialog
    {
	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	readonly static PictureBox BAR_CONTAINER = new PictureBox();

	readonly static Button CLOSE = new Button();
	readonly static Button MINIM = new Button();

	readonly static Label TITLE = new Label();

	private static void InitializeMEU(Form TOP)
	{
	    var BAR_SIZE = new Size(TOP.Width, 28);
	    var BAR_LOCA = new Point(0, 0);
	    var BAR_COLA = Color.FromArgb(8, 8, 8);

	    CONTROL.Image(TOP, BAR_CONTAINER, BAR_SIZE, BAR_LOCA, null, BAR_COLA);

	    var BUTTON_SIZE = new Size(48, BAR_SIZE.Height);
	    var BUTTON_LOCA = new Point(BAR_SIZE.Width - BUTTON_SIZE.Width, 0);
	    var BUTTON_BCOL = BAR_COLA;
	    var BUTTON_FCOL = Color.White;

	    CONTROL.Button(BAR_CONTAINER, CLOSE, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "X", Color.Empty);

	    BUTTON_LOCA.X -= BUTTON_SIZE.Width;

	    CONTROL.Button(BAR_CONTAINER, MINIM, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "-", Color.Empty);

	    CLOSE.Click += (s, e) => Application.Exit();
	    MINIM.Click += (s, e) => TOP.SendToBack();

	    var TITLE_TEXT = "Dash Authenticator";
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 9);
	    var TITLE_LOCA = new Point((BAR_CONTAINER.Width - TITLE_SIZE.Width - (BUTTON_SIZE.Width * 2)) / 2, (BAR_CONTAINER.Height - TITLE_SIZE.Height) / 2);
	    var TITLE_BCOL = BAR_CONTAINER.BackColor;
	    var TITLE_FCOL = Color.White;

	    CONTROL.Label(BAR_CONTAINER, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 9, TITLE_TEXT);

	    TOOL.Interactive(BAR_CONTAINER, TOP);

	    foreach (Control CON in BAR_CONTAINER.Controls)
	    {
		if (!(CON is Button))
		{
		    TOOL.Interactive(CON, TOP);
		}
	    }
	}

	public static void UndoChanges(Form TOP)
	{
	    foreach (Control Control in TOP.Controls)
	    {
		Control.Hide();
	    }

	    TOOL.Resize(TOP, new Size(450, 258));
	}

	private static void InitializeGUI(Form TOP)
	{
	    TOOL.Resize(TOP, new Size(225, 122));
	    TOOL.Round(TOP, 6);

	    TOP.BackgroundImage = null;
	    TOP.BackColor = Color.MidnightBlue; //Color.FromArgb(18, 18, 18);

	    TOP.FormBorderStyle = FormBorderStyle.None;
	    TOP.StartPosition = FormStartPosition.CenterScreen;

	    TOP.Icon = Properties.Resources.ICON_ICO;

	    var RECTANGLE_SIZE = new Size(TOP.Width - 1, TOP.Height - 1);
	    var RECTANGLE_LOCA = new Point(0, 0);
	    var RECTANGLE_COLA = Color.FromArgb(8, 8, 8);

	    TOOL.PaintRectangle(TOP, 2, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
	}

	readonly static TextBox IUSERNAME = new TextBox() { TextAlign = HorizontalAlignment.Center };
	readonly static TextBox IPASSWORD = new TextBox() { TextAlign = HorizontalAlignment.Center, PasswordChar = ' ' };

	readonly static Label USERNAME = new Label();
	readonly static Label PASSWORD = new Label();

	private static void InitializeCRE(Form TOP)
	{
	    var LABEL_SIZE = TOOL.GetFontSize("Username:", 12);//GetPreferredSize("Username:", 12);
	    var LABEL_LOCA = new Point(5, 31);
	    var LABEL_BCOL = TOP.BackColor;
	    var LABEL_FCOL = Color.White;

	    int GetLabelWidth() => TOP.Width - LABEL_SIZE.Width - 15;
	    int GetLabelX() => LABEL_SIZE.Width + LABEL_LOCA.X;

	    CONTROL.Label(TOP, USERNAME, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 12, "Username:");

	    var BOX_SIZE = new Size(GetLabelWidth(), 20);
	    var BOX_LOCA = new Point(GetLabelX(), LABEL_LOCA.Y + 1);
	    var BOX_BCOL = Color.FromArgb(8, 8, 8);
	    var BOX_FCOL = Color.White;

	    Control GetAnonControl() => TOP.Controls[TOP.Controls.Count - 1];

	    CONTROL.TextBox(TOP, IUSERNAME, BOX_SIZE, BOX_LOCA, BOX_BCOL, BOX_FCOL, 1, 10, Color.Empty);
	    TOOL.Round(GetAnonControl(), 6);

	    LABEL_SIZE = TOOL.GetFontSize("Password:", 12); //GetPreferredSize("Password:", 12);
	    LABEL_LOCA.Y += LABEL_SIZE.Height + 5;

	    CONTROL.Label(TOP, PASSWORD, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 12, "Password:");

	    BOX_SIZE = new Size(GetLabelWidth(), BOX_SIZE.Height);
	    BOX_LOCA = new Point(GetLabelX(), LABEL_LOCA.Y + 1);

	    CONTROL.TextBox(TOP, IPASSWORD, BOX_SIZE, BOX_LOCA, BOX_BCOL, BOX_FCOL, 1, 10, Color.Empty);
	    TOOL.Round(GetAnonControl(), 6);
	}

	readonly static PictureBox BUTTON_CONTAINER = new PictureBox();

	readonly public static Button LOGIN = new Button();
	readonly public static Button HELP = new Button();

	private static void InitializeAUT(Form TOP)
	{
	    var CONTAINER_SIZE = new Size(210, 26);
	    var CONTAINER_LOCA = new Point((TOP.Width - CONTAINER_SIZE.Width) / 2, TOP.Height - CONTAINER_SIZE.Height - 7);
	    var CONTAINER_COLA = TOP.BackColor;

	    CONTROL.Image(TOP, BUTTON_CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

	    var BUTTON_SIZE = new Size(CONTAINER_SIZE.Width / 2 - 5, CONTAINER_SIZE.Height);
	    var BUTTON_LOCA = new Point(0, 0);
	    var BUTTON_BCOL = Color.MidnightBlue;
	    var BUTTON_FCOL = Color.White;

	    CONTROL.Button(BUTTON_CONTAINER, LOGIN, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 11, "Login", Color.Empty);
	    TOOL.Round(LOGIN, 6);

	    BUTTON_LOCA.X += BUTTON_SIZE.Width + 10;

	    CONTROL.Button(BUTTON_CONTAINER, HELP, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 11, "Info", Color.Empty);
	    TOOL.Round(HELP, 6);

	    HELP.Click += (s, e) =>
	    {
		if (!Interface.HelpDialog.Visible)
		{
		    Interface.HelpDialog.ShowDialog();
		};
	    };
	}

	public static void AuthenticatorInterf(Form TOP)
	{
	    try
	    {
		InitializeGUI(TOP);
		InitializeMEU(TOP);
		InitializeCRE(TOP);
		InitializeAUT(TOP);
	    }

	    catch
	    {
		throw new Exception("AuthenticatorInterf()");
	    }
	}
    }
}