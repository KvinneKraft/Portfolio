using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

// To-Do:
// - Add MENU BAR utility
// - Add Save File Dialog replacement
// - Add Open File Dialog replacement
// - Add Message Box Dialog replacement
// - FIX TEXTBOX BORDER STYLE

namespace ThaDasher
{
    public static class MENUBAR
    {
	readonly public static PictureBox BAR = new PictureBox();
	readonly public static PictureBox LOGO = new PictureBox();

	readonly public static Label TITLE = new Label();

	readonly public static Button CLOSE = new Button();
	readonly public static Button MINIM = new Button();

	public enum STYLE
	{
	    THIN = 26,
	    THIC = 28,
	    GIAT = 36,
	}
    }

    public class DashControls
    {
	readonly DashTools TOOL = new DashTools();

	public void MenuBar(Form TOP, int STYLE, bool hasBorder, Color BAR_COLA, Color BOR_COLA)
	{
	    if (BAR_COLA.IsEmpty) BAR_COLA = Color.FromArgb(8, 8, 8);
	    
	    if (hasBorder)
	    {
		if (BOR_COLA.IsEmpty) BOR_COLA = Color.FromArgb(8, 8, 8);

		var BOR_SIZE = new Size(TOP.Width - 2, TOP.Height - 2);
		var BOR_LOCA = new Point(1, 1);

		TOOL.PaintRectangle(TOP, 2, BOR_SIZE, BOR_LOCA, BOR_COLA);
	    };

	    var BAR_SIZE = new Size(TOP.Width - 2, STYLE);
	    var BAR_LOCA = new Point(1, 1);

	    Image(TOP, MENUBAR.BAR, BAR_SIZE, BAR_LOCA, null, BAR_COLA);

	    var LOGO_IMAG = Properties.Resources.ICON_PNG;
	    var LOGO_SIZE = LOGO_IMAG.Size;
	    var LOGO_LOCA = new Point(4, (BAR_SIZE.Height - LOGO_IMAG.Height) / 2);

	    Image(MENUBAR.BAR, MENUBAR.LOGO, LOGO_SIZE, LOGO_LOCA, LOGO_IMAG, BAR_COLA);

	    var TITLE_TEXT = TOP.Text;
	    var TITLE_FONT = TOOL.GetFont(1, 8);
	    var TITLE_SIZE = TextRenderer.MeasureText(TITLE_TEXT, TITLE_FONT);
	    var TITLE_LOCA = new Point(LOGO_SIZE.Width + LOGO_LOCA.X + 25, (BAR_SIZE.Height - TITLE_SIZE.Height) / 2);
	    var TITLE_FCOL = Color.White;

	    Label(MENUBAR.BAR, MENUBAR.TITLE, TITLE_SIZE, TITLE_LOCA, BAR_COLA, TITLE_FCOL, 1, 8, TITLE_TEXT);

	    var BUTTON_SIZE = new Size(75, BAR_SIZE.Height);
	    var BUTTON_LOCA = new Point(BAR_SIZE.Width - BUTTON_SIZE.Width, 0);
	    var BUTTON_FCOL = Color.White;
	    var BUTTON_TEXT = "X";

	    Button(MENUBAR.BAR, MENUBAR.CLOSE, BUTTON_SIZE, BUTTON_LOCA, BAR_COLA, BUTTON_FCOL, 1, 10, BUTTON_TEXT, Color.Empty);

	    BUTTON_LOCA.X -= BUTTON_SIZE.Width;
	    BUTTON_TEXT = "-";

	    MENUBAR.CLOSE.Click += (s, e) =>
		TOP.Close();

	    Button(MENUBAR.BAR, MENUBAR.MINIM, BUTTON_SIZE, BUTTON_LOCA, BAR_COLA, BUTTON_FCOL, 1, 10, BUTTON_TEXT, Color.Empty);

	    MENUBAR.MINIM.Click += (s, e) =>
		TOP.SendToBack();

	    TOOL.Interactive(MENUBAR.BAR, TOP);

	    foreach (Control c in MENUBAR.BAR.Controls)
		if (!(c is Button)) TOOL.Interactive(c, TOP);
	}

	public void TextBox(Control CON, TextBox OBJECT, Size TSIZE, Point TLOCATION, Color BCOLOR, Color FCOLOR, int FTYPE, int FSIZE, Color BORDERCOLOR, bool HASBORDER = false, int BORDERSIZE = 0, bool READONLY = false, bool MULTILINE = false, bool SCROLLBAR = false, bool FIXEDSIZE = true)
	{
	    TOOL.Resize(OBJECT, TSIZE);

	    OBJECT.Location = TOOL.GetCenter(CON, OBJECT, TLOCATION);

	    if (HASBORDER)
		TOOL.PaintRectangle(CON, BORDERSIZE, TSIZE, TLOCATION, BORDERCOLOR);

	    OBJECT.BackColor = BCOLOR;
	    OBJECT.ForeColor = FCOLOR;

	    OBJECT.BorderStyle = BorderStyle.None;
	    OBJECT.Font = TOOL.GetFont(FTYPE, FSIZE);

	    OBJECT.ReadOnly = READONLY;
	    OBJECT.Multiline = MULTILINE;

	    OBJECT.TabStop = false;

	    if (FIXEDSIZE)
	    {
		var FIXEDBOX = new PictureBox();

		Image(CON, FIXEDBOX, TSIZE, TLOCATION, null, BCOLOR);

		TOOL.Resize(OBJECT, new Size(TSIZE.Width - 6, OBJECT.PreferredHeight));
		OBJECT.Location = TOOL.GetCenter(FIXEDBOX, OBJECT, new Point(3, 3));

		FIXEDBOX.Click += (s, e) => OBJECT.Select();

		CON = FIXEDBOX;
	    };

	    if (SCROLLBAR)
		OBJECT.ScrollBars = ScrollBars.Vertical;

	    CON.Controls.Add(OBJECT);
	}

	public void Button(Control CON, Button OBJECT, Size BSIZE, Point BLOCATION, Color BCOLOR, Color FCOLOR, int FTYPE, int FSIZE, string TEXT, Color BORDERCOLOR, bool HASBORDER = false, int BORDERSIZE = 0)
	{
	    TOOL.Resize(OBJECT, BSIZE);

	    OBJECT.Location = TOOL.GetCenter(CON, OBJECT, BLOCATION);

	    if (HASBORDER)
	    {
		TOOL.PaintRectangle(CON, BORDERSIZE, BSIZE, BLOCATION, BORDERCOLOR);
	    };

	    OBJECT.FlatAppearance.BorderColor = BCOLOR;
	    OBJECT.FlatAppearance.BorderSize = 0;

	    OBJECT.FlatStyle = FlatStyle.Flat;
	    OBJECT.TabStop = false;

	    OBJECT.BackColor = BCOLOR;
	    OBJECT.ForeColor = FCOLOR;

	    OBJECT.Font = TOOL.GetFont(FTYPE, FSIZE);
	    OBJECT.Text = TEXT;

	    CON.Controls.Add(OBJECT);
	}

	public void Image(Control CON, PictureBox OBJECT, Size ISIZE, Point ILOCATION, Image IMAGE, Color BCOLOR)
	{
	    TOOL.Resize(OBJECT, ISIZE);

	    OBJECT.Location = TOOL.GetCenter(CON, OBJECT, ILOCATION);

	    OBJECT.BorderStyle = BorderStyle.None;
	    OBJECT.TabStop = false;

	    if (BCOLOR == Color.Empty)
		BCOLOR = Color.Transparent;

	    OBJECT.BackColor = BCOLOR;
	    OBJECT.Image = IMAGE;

	    CON.Controls.Add(OBJECT);
	}

	public void Label(Control CON, Label OBJECT, Size LSIZE, Point LLOCATION, Color BCOLOR, Color FCOLOR, int FTYPE, int FSIZE, string TEXT)
	{
	    var FONT = TOOL.GetFont(FTYPE, FSIZE);

	    if (LSIZE == Size.Empty)
		LSIZE = TextRenderer.MeasureText(TEXT, FONT);

	    TOOL.Resize(OBJECT, LSIZE);

	    OBJECT.Location = TOOL.GetCenter(CON, OBJECT, LLOCATION);

	    OBJECT.BorderStyle = BorderStyle.None;
	    OBJECT.FlatStyle = FlatStyle.Flat;

	    OBJECT.TabStop = false;

	    OBJECT.ForeColor = FCOLOR;
	    OBJECT.BackColor = BCOLOR;

	    OBJECT.Font = FONT;
	    OBJECT.Text = TEXT;

	    CON.Controls.Add(OBJECT);
	}

	public void RichTextBox(Control CON, RichTextBox OBJECT, Size RSIZE, Point RLOCATION, Color BCOLOR, Color FCOLOR, int FTYPE, int FSIZE, string TEXT)
	{
	    var FONT = TOOL.GetFont(FTYPE, FSIZE);

	    if (RSIZE == Size.Empty)
		RSIZE = TextRenderer.MeasureText(TEXT, FONT);

	    TOOL.Resize(OBJECT, RSIZE);

	    OBJECT.BorderStyle = BorderStyle.None;
	    OBJECT.TabStop = false;

	    OBJECT.Location = TOOL.GetCenter(CON, OBJECT, RLOCATION);

	    OBJECT.BackColor = BCOLOR;
	    OBJECT.ForeColor = FCOLOR;

	    OBJECT.Font = FONT;
	    OBJECT.Text = TEXT;

	    CON.Controls.Add(OBJECT);
	}

	public void ScrollBar(Control CON, PictureBox CONT1, Color COLA1, PictureBox CONT2, Color COLA2, Size SIZE, Point LOCA)
	{
	    Image(CON, CONT1, SIZE, LOCA, null, COLA1);

	    var BAR_SIZE = new Size(SIZE.Width - 10, SIZE.Height - 10);
	    var BAR_LOCA = new Point(5, 5);

	    Image(CONT1, CONT2, BAR_SIZE, BAR_LOCA, null, COLA2);
	}
    }
}