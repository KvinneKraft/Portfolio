using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class SettingsContainer
    {
	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	readonly static Form DIALOG = new Form();

	private static void InitializeDialogy()
	{
	    try
	    {
		DIALOG.FormBorderStyle = FormBorderStyle.None;
		DIALOG.BackColor = Color.FromArgb(24, 24, 24);

		DIALOG.AutoScroll = true;

		DIALOG.HorizontalScroll.Enabled = false;
		DIALOG.HorizontalScroll.Visible = false;

		DIALOG.VerticalScroll.Enabled = true;
		DIALOG.VerticalScroll.Visible = true;

		TOOL.Resize(DIALOG, new Size(200, 200));
		TOOL.Round(DIALOG, 6);

		DIALOG.StartPosition = FormStartPosition.CenterParent;

		TOOL.PaintRectangle(DIALOG, 2, DIALOG.Size, new Point(0, 0), Color.FromArgb(8, 8, 8));
	    }

	    catch
	    {
		throw new Exception("Selector Container Initialization.");
	    }
	}

	readonly static PictureBox BAR = new PictureBox();

	readonly static Button CLOSE = new Button();

	readonly static Label TITLE = new Label();

	private static void InitializeMenuBar()
	{
	    try
	    {
		var BAR_SIZE = new Size(DIALOG.Width - 18, 26);
		var BAR_LOCA = new Point(1, 1);
		var BAR_COLA = Color.FromArgb(12, 12, 12);

		CONTROL.Image(DIALOG, BAR, BAR_SIZE, BAR_LOCA, null, BAR_COLA);
		TOOL.Interactive(BAR, DIALOG);

		var TITLE_SIZE = Size.Empty;
		var TITLE_LOCA = new Point(5, -1);
		var TITLE_BCOL = BAR_COLA;
		var TITLE_FCOL = Color.White;

		CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, "Method Selector");
		TOOL.Interactive(TITLE, DIALOG);

		var CLOSE_SIZE = new Size(50, BAR_SIZE.Height);
		var CLOSE_LOCA = new Point(BAR_SIZE.Width - CLOSE_SIZE.Width, 0);
		var CLOSE_BCOL = BAR_COLA;
		var CLOSE_FCOL = Color.White;

		CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 12, "X", Color.Empty);

		CLOSE.Click += (s, e) =>
		    DIALOG.Hide();
	    }

	    catch
	    {
		throw new Exception("Selector Container Menu Bar Initialization.");
	    }
	}

	readonly static Label HTTP_TITLE = new Label() { Text = "HTTP Methods", TextAlign = ContentAlignment.MiddleCenter };

	readonly static Button HTTP_DASHLORIS = new Button();
	readonly static Button HTTP_SLOWLORIS = new Button();
	readonly static Button HTTP_FLOOD = new Button();
	readonly static Button HTTP_POST = new Button();
	readonly static Button HTTP_PUT = new Button();
	readonly static Button HTTP_GET = new Button();

	readonly static int httpcount = 6;

	readonly static Label TCP_TITLE = new Label() { Text = "TCP Methods", TextAlign = ContentAlignment.MiddleCenter };

	readonly static Button TCP_LONGSOCKS = new Button();
	readonly static Button TCP_SOCKS = new Button();
	readonly static Button TCP_FLOOD = new Button();
	readonly static Button TCP_WAVES = new Button();

	readonly static int tcpcount = 4;

	readonly static Label UDP_TITLE = new Label() { Text = "UDP Methods", TextAlign = ContentAlignment.MiddleCenter };

	readonly static Button UDP_OVERLOAD = new Button();
	readonly static Button UDP_FLOOD = new Button();
	readonly static Button UDP_WAVES = new Button();
	readonly static Button UDP_HAM = new Button();

	static public string CURRENT_METHOD = "Dashloris 4.0"; 

	private static void UpdateMethodsButton(string m)
	{
	    string p = "NONE";

	    switch (m.ToLower())
	    {
		case "dashloris 4.0":
		case "slowloris 2.0":
		case "put head":
		case "post head":
		case "get head":
		case "h-flood":
		    p = "HTTP";
		    break;

		case "long socks":
		case "multi flood":
		case "multi socks":
		case "wavesss":
		    p = "TCP";
		    break;

		case "overload":
		case "wavy baby":
		case "go ham":
		case "insta flood":
		    p = "UDP";
		    break;
	    };

	    METHODS.Text = $"{p} -- {m}";

	    DIALOG.Close();
	}

	private static void InitializeOptions()
	{
	    try
	    {
		var OPTIO_CONTR = new List<Button>()
		{
		    HTTP_DASHLORIS, HTTP_SLOWLORIS, HTTP_PUT, HTTP_POST, HTTP_GET, HTTP_FLOOD,
		    TCP_LONGSOCKS, TCP_FLOOD, TCP_SOCKS, TCP_WAVES,
		    UDP_OVERLOAD, UDP_WAVES, UDP_HAM, UDP_FLOOD
		};

		var OPTION_TEXT = new List<string>()
		{
		    "Dashloris 4.0", "Slowloris 2.0", "PUT Head", "POST Head", "GET Head", "H-Flood",
		    "Long Socks", "Multi Flood", "Multi Socks", "Wavesss",
		    "Overload", "Wavy Baby", "Go Ham", "Insta Flood",
		};

		var BUTTON_SIZE = new Size(DIALOG.Width - 19, 24);
		var BUTTON_LOCA = new Point(2, BAR.Height + 1);
		var BUTTON_BCOL = Color.FromArgb(64, 25, 112);//150, Color.MidnightBlue.R, Color.MidnightBlue.G, Color.MidnightBlue.B);
		var BUTTON_FCOL = Color.White;

		int GetTitleHeight(string param) => TOOL.GetFontSize(param, 24).Height;//TextRenderer.MeasureText(param, TOOL.GetFont(1, 24)).Height;

		var TITLE_CONTR = new List<Label>()
		{
		    HTTP_TITLE, TCP_TITLE, UDP_TITLE
		};

		var TITLE_SIZE = new Size(DIALOG.Width - 18, GetTitleHeight(TITLE_CONTR[0].Text));
		var TITLE_BCOL = BAR.BackColor;
		var TITLE_FCOL = Color.White;

		for (int i = 0, k = 0; i < OPTION_TEXT.Count; i += 1)
		{
		    if (i == 0 || i == httpcount || i == httpcount + tcpcount)
		    {
			var TITLE_LOCA = new Point(1, BUTTON_LOCA.Y);

			CONTROL.Label(DIALOG, TITLE_CONTR[k], TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 12, TITLE_CONTR[k].Text);
			BUTTON_LOCA.Y += TITLE_SIZE.Height; k += 1;
		    }

		    CONTROL.Button(DIALOG, OPTIO_CONTR[i], BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, OPTION_TEXT[i], Color.Empty);
		    BUTTON_LOCA.Y += BUTTON_SIZE.Height;

		    var r = i;

		    OPTIO_CONTR[i].Click += (s, e) =>
		    {
			if (CURRENT_METHOD != OPTION_TEXT[r])
			{
			    CURRENT_METHOD = OPTION_TEXT[r];

			    UpdateMethodsButton(OPTION_TEXT[r]);
			};
		    };
		};
	    }

	    catch
	    {
		throw new Exception("Selector Container Method Dialog Initialization.");
	    }
	}

	public static void InitializeSelectorContainer(Form TOP)
	{
	    try
	    {
		InitializeDialogy();
		InitializeMenuBar();
		InitializeOptions();
	    }

	    catch (Exception e)
	    {
		throw new Exception(e.Message);
	    }
	}

	readonly public static PictureBox CONTAINER = new PictureBox();
	readonly static PictureBox PONY1 = new PictureBox();
	readonly static PictureBox PONY2 = new PictureBox();

	readonly public static Button METHODS = new Button();

	readonly static Label METHOD_TITLE = new Label() { TextAlign = ContentAlignment.MiddleCenter };

	public static void InitializeMCon(Form TOP)
	{
	    try
	    {
		var CONTAINER_SIZE = new Size(TargetContainer.CONTAINER.Width, 100);
		var CONTAINER_LOCA = new Point(TargetContainer.CONTAINER.Left, TargetContainer.CONTAINER.Top + TargetContainer.CONTAINER.Height + 5);
		var CONTAINER_BCOL = TargetContainer.CONTAINER.BackColor;

		CONTROL.Image(TOP, CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_BCOL);

		var RECTANGLE_SIZE = CONTAINER_SIZE;
		var RECTANGLE_LOCA = new Point(0, 0);
		var RECTANGLE_COLA = Color.FromArgb(CONTAINER_BCOL.R - 8, CONTAINER_BCOL.G - 8, CONTAINER_BCOL.B - 8);

		TOOL.PaintRectangle(CONTAINER, 4, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
		TOOL.Round(CONTAINER, 6);

		var TITLE_SIZE = Size.Empty;
		var TITLE_LOCA = new Point(0, 5);
		var TITLE_BCOL = Color.Transparent;
		var TITLE_FCOL = Color.White;

		CONTROL.Label(CONTAINER, METHOD_TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 16, "Methods");

		METHOD_TITLE.Left = (CONTAINER_SIZE.Width - METHOD_TITLE.Width) / 2;

		var METHOD_SIZE = new Size(175, 28);
		var METHOD_LOCA = new Point((CONTAINER_SIZE.Width - METHOD_SIZE.Width) / 2, TITLE_LOCA.Y + METHOD_TITLE.Height + 5);
		var METHOD_BCOL = Color.FromArgb(16, 16, 16);
		var METHOD_FCOL = Color.White;

		CONTROL.Button(CONTAINER, METHODS, METHOD_SIZE, METHOD_LOCA, METHOD_BCOL, METHOD_FCOL, 1, 10, "HTTP -- Dashloris 4.0", Color.Empty);
		TOOL.Round(METHODS, 4);

		METHODS.Click += (s, e) => DIALOG.ShowDialog();

		var IMAGE1_IMAG = Properties.Resources.PONY_GIF1;
		var IMAGE1_SIZE = IMAGE1_IMAG.Size;
		var IMAGE1_LOCA = new Point(5, CONTAINER_SIZE.Height - IMAGE1_SIZE.Height);
		var IMAGE1_COLA = Color.Transparent;

		CONTROL.Image(CONTAINER, PONY1, IMAGE1_SIZE, IMAGE1_LOCA, IMAGE1_IMAG, IMAGE1_COLA);

		var IMAGE2_IMAG = Properties.Resources.PONY_GIF2;
		var IMAGE2_SIZE = IMAGE2_IMAG.Size;
		var IMAGE2_LOCA = new Point(CONTAINER_SIZE.Width - IMAGE2_SIZE.Width - 4, CONTAINER_SIZE.Height - IMAGE1_SIZE.Height);
		var IMAGE2_COLA = Color.Transparent;

		CONTROL.Image(CONTAINER, PONY2, IMAGE2_SIZE, IMAGE2_LOCA, IMAGE2_IMAG, IMAGE2_COLA);

		InitializeSelectorContainer(TOP);
	    }

	    catch (Exception e)
	    {
		throw new Exception(e.Message);
	    };
	}
    };
}