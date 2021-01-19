// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DashlorisX
{
    public class DashMenuBar
    {
	readonly public DashControls CONTROL = new DashControls();
	readonly public DashTools TOOL = new DashTools();

	bool minim = true, close = true, hide = true;

	public DashMenuBar(string title, bool minim = true, bool close = true, bool hide = true)
	{
	    this.minim = minim;
	    this.close = close;

	    Title.Text = (title);
	}

	readonly public PictureBox SLogo = new PictureBox();
	readonly public PictureBox Logo = new PictureBox();
	readonly public PictureBox Bar = new PictureBox();

	readonly public Label Title = new Label();

	readonly public Button Close = new Button();
	readonly public Button Minim = new Button();

	public void Add(Form Top, int Height, Color BarCola, Color BorCola)
	{
	    var BAR_SIZE = new Size(Top.Width, Height);
	    var BAR_LOCA = new Point(0, 0);
	    var BAR_COLA = BarCola;

	    try
	    {
		CONTROL.Image(Top, Bar, BAR_SIZE, BAR_LOCA, null, BAR_COLA);
		TOOL.Interactive(Bar, Top);
	    }

	    catch
	    {
		throw new Exception("Menu Bar");
	    }

	    var LOGO_SIZE = new Size(38, 32);
	    var LOGO_LOCA = new Point(5, 5);

	    try
	    {
		CONTROL.Image(Top, SLogo, LOGO_SIZE, LOGO_LOCA, Properties.Resources.LOGO, Color.FromArgb(6, 17, 33));
		TOOL.Interactive(SLogo, Top);

		CONTROL.Image(Bar, Logo, LOGO_SIZE, LOGO_LOCA, Properties.Resources.LOGO, BarCola);
		TOOL.Interactive(Logo, Top);
	    }

	    catch
	    {
		throw new Exception("Logo");
	    }

	    var TITLE_TEXT = (Title.Text);
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 8);
	    var TITLE_LOCA = new Point(Logo.Width + Logo.Left + 5, (Bar.Height - TITLE_SIZE.Height) / 2);

	    try
	    {
		CONTROL.Label(Bar, Title, TITLE_SIZE, TITLE_LOCA, BarCola, Color.White, 1, 8, TITLE_TEXT);
		TOOL.Interactive(Title, Top);
	    }

	    catch
	    {
		throw new Exception("Title");
	    }

	    var BUTTO_SIZE = new Size(65, Height);
	    var BUTTO_LOCA = new Point(Bar.Width - BUTTO_SIZE.Width, 0);

	    try
	    {

		if (close)
		{
		    CONTROL.Button(Bar, Close, BUTTO_SIZE, BUTTO_LOCA, BarCola, Color.White, 1, 10, ("X"), Color.Empty);
		    TOOL.Interactive(Close, Top);

		    Close.Click += (s, e) =>
		    {
			if (!hide)
			{
			    Environment.Exit(-1);
			}

			else
			{
			    Top.Close();
			}
		    };
		}

		if (close && minim)
		{
		    BUTTO_LOCA.X -= BUTTO_SIZE.Width;
		}

		if (minim)
		{
		    CONTROL.Button(Bar, Minim, BUTTO_SIZE, BUTTO_LOCA, BarCola, Color.White, 1, 10, ("-"), Color.Empty);
		    TOOL.Interactive(Minim, Top);

		    Minim.Click += (s, e) => Top.SendToBack();
		}
	    }

	    catch
	    {
		throw new Exception("Buttons");
	    }

	    var RECT_SIZE = new Size(Bar.Width - 2, Top.Height - Bar.Height + 1);
	    var RECT_LOCA = new Point(1, Bar.Height + Bar.Top - 2);

	    try
	    {
		TOOL.PaintRectangle(Top, 2, RECT_SIZE, RECT_LOCA, BorCola);
	    }

	    catch
	    {
		throw new Exception("Rectangle");
	    }
	}
    }
}
