
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using ThaDasher.Properties;

namespace ThaDasher
{
    public class DashTools
    {
	public string GetErrorFormat(Exception e) => $"Hey there, I am unfortunate to say but DashBooks has stopped working!\r\n\r\nIf you want to help me out then please send the following to me at KvinneKraft@protonmail.com. \r\n\r\nSTACK TRACE:\r\n{e.StackTrace}\r\n\r\nRAW MESSAGE:\r\n{e.Message}\r\n\r\nThank you, regardless!\r\n-Dashie";

	public Size GetFontSize(string TEXT, int SIZE) =>
	    TextRenderer.MeasureText(TEXT, GetFont(1, SIZE));

	public void Resize(Control OBJECT, Size SIZE)
	{
	    OBJECT.MaximumSize = SIZE;
	    OBJECT.MinimumSize = SIZE;
	    OBJECT.Size = SIZE;
	}

	public void PaintRectangle(Control CON, int THICKNESS, Size SIZE, Point LOCATION, Color COLOR)
	{
	    LOCATION = GetCenter(CON, LOCATION, SIZE);

	    CON.Paint += (s, e) =>
	    {
		var graphics = e.Graphics;

		graphics.SmoothingMode = SmoothingMode.AntiAlias;

		using (Pen pen = new Pen(COLOR, THICKNESS))
		{
		    graphics.DrawRectangle(pen, new Rectangle(LOCATION, SIZE));
		};
	    };
	}

	public void PaintLine(Control CON, Color COLOR, int THICKNESS, Point LOCATION1, Point LOCATION2)
	{
	    CON.Paint += (s, e) =>
	    {
		var graphics = e.Graphics;

		graphics.SmoothingMode = SmoothingMode.AntiAlias;

		using (Pen pen = new Pen(COLOR, THICKNESS))
		{
		    graphics.DrawLine(pen, LOCATION1, LOCATION2);
		};
	    };
	}

	public void PaintCircle(Control CON, Color COLOR, int THICKNESS, Point LOCATION, Size SIZE)
	{
	    LOCATION = GetCenter(CON, LOCATION, SIZE);

	    CON.Paint += (s, e) =>
	    {
		var graphics = e.Graphics;

		graphics.SmoothingMode = SmoothingMode.AntiAlias;

		using (Pen pen = new Pen(COLOR, THICKNESS))
		{
		    graphics.DrawEllipse(pen, new RectangleF(LOCATION, SIZE));
		};
	    };
	}

	public Point GetCenter(Control CON, Point LOCATION, Size SIZE)
	{
	    if (LOCATION.Y < 0)
		LOCATION.Y = (CON.Height - SIZE.Height) / 2;

	    if (LOCATION.X < 0)
		LOCATION.X = (CON.Width - SIZE.Width) / 2;

	    return LOCATION;
	}

	public Point GetCenter(Control CON, Control OBJECT, Point LOCATION)
	{
	    if (LOCATION.Y < 0)
		LOCATION.Y = (CON.Height - OBJECT.Height) / 2;

	    if (LOCATION.X < 0)
		LOCATION.X = (CON.Width - OBJECT.Width) / 2;

	    return LOCATION;
	}

	[DllImport("User32.dll")] static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);
	public void UseResourceCursor(Control CON, byte[] BYTES)
	{
	    var curse = new Cursor(CreateIconFromResource(BYTES, (uint)BYTES.Length, false, 0x00030000));

	    CON.Cursor = curse;
	    CON.Update();
	}

	public void Interactive(Control CON, Control TAR)
	{
	    var LOC = Point.Empty;

	    CON.MouseMove += (s, e) =>
	    {
		if (LOC.IsEmpty)
		    return;

		var SOC = Point.Empty;

		SOC.X = TAR.Location.X + (e.X - LOC.X);
		SOC.Y = TAR.Location.Y + (e.Y - LOC.Y);

		TAR.Location = SOC;
	    };

	    CON.MouseDown += (s, e) =>
		LOC = new Point(e.X, e.Y);

	    CON.MouseUp += (s, e) =>
		LOC = Point.Empty;
	}

	public class ReadOnlyForm : Form
	{
	    public void PaintOwner(PaintEventArgs e)
	    {
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		base.OnPaint(e);
	    }
	}

	readonly public ReadOnlyForm ROF = new ReadOnlyForm();

	public void Round(Control CON, int RAD)
	{
	    CON.Paint += (s, e) =>
	    {
		ROF.PaintOwner(e);

		using (GraphicsPath GRAP = new GraphicsPath())
		{
		    var RECT = new Rectangle(0, 0, CON.Width, CON.Height);

		    int R = RAD * 3;

		    int H = RECT.Height;
		    int W = RECT.Width;

		    int X = RECT.X; // Perhaps use 0 instead?
		    int Y = RECT.X; // Perhaps use 0 instead?

		    GRAP.AddArc(X, Y, R, R, 170, 90);
		    GRAP.AddArc(X + W - R, Y, R, R, 270, 90);
		    GRAP.AddArc(X + W - R, Y + H - R, R, R, 0, 90);
		    GRAP.AddArc(X, Y + H - R, R, R, 80, 90);

		    CON.Region = new Region(GRAP);
		};
	    };
	}

	[DllImport("gdi32.dll")] private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
	readonly PrivateFontCollection FONT_COLLECTION = new PrivateFontCollection();

	public Font GetFont(int TYPE, int SIZE)
	{
	    if (FONT_COLLECTION.Families.Length < 2)
	    {
		var FDAT = new List<byte[]>();

		FDAT.Add(Resources.main);
		FDAT.Add(Resources.cute);

		for (int k = 0; k < FDAT.Count; k += 1)
		{
		    byte[] DAT = FDAT[k];

		    var PTR = Marshal.AllocCoTaskMem(DAT.Length);
		    Marshal.Copy(DAT, 0, PTR, DAT.Length);

		    uint REF = 0;

		    AddFontMemResourceEx(PTR, (uint)DAT.Length, IntPtr.Zero, ref REF);
		    FONT_COLLECTION.AddMemoryFont(PTR, DAT.Length);
		};
	    };

	    return new Font(FONT_COLLECTION.Families[TYPE]/*+1 ?*/, SIZE, FontStyle.Regular);
	}
    }
}