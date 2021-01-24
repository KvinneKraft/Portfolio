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

// To-Do:
// - Add MENU BAR utility
// - Add Save File Dialog replacement
// - Add Open File Dialog replacement
// - Add Message Box Dialog replacement
// - FIX TEXTBOX BORDER STYLE

namespace DashlorisX
{
    public class DashControls
    {
	readonly DashTools TOOL = new DashTools();

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