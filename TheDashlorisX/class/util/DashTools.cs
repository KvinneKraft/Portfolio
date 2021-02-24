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
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TheDashlorisX.Properties;

namespace TheDashlorisX
{
    public class DashTools
    {
	public Size GetFontSize(string Text, int Size)
	{
	    return TextRenderer.MeasureText(Text, GetFont(1, Size));
	}

	public void Resize(Control Object, Size Size)
	{
	    Object.MaximumSize = Size;
	    Object.MinimumSize = Size;
	}

	public void PaintRectangle(Control Object, int Thickness, Size Size, Point Location, Color Color)
	{
	    Object.Paint += (s, e) =>
	    {
		var graphics = e.Graphics;

		graphics.SmoothingMode = SmoothingMode.HighQuality;

		using (Pen pen = new Pen(Color, Thickness))
		{
		    graphics.DrawRectangle(pen, new Rectangle(Location, Size));
		};
	    };
	}

	public void PaintLine(Control Object, Color Color, int Thickness, Point Location1, Point Location2)
	{
	    Object.Paint += (s, e) =>
	    {
		var graphics = e.Graphics;

		graphics.SmoothingMode = SmoothingMode.HighQuality;

		using (Pen pen = new Pen(Color, Thickness))
		{
		    graphics.DrawLine(pen, Location1, Location2);
		};
	    };
	}

	public void PaintCircle(Control Object, Color Color, int Thickness, Point Location, Size Size)
	{
	    Object.Paint += (s, e) =>
	    {
		var graphics = e.Graphics;

		graphics.SmoothingMode = SmoothingMode.HighQuality;

		using (Pen pen = new Pen(Color, Thickness))
		{
		    graphics.DrawEllipse(pen, new RectangleF(Location, Size));
		};
	    };
	}

	[DllImport("User32.dll")] static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);
	public void UseResourceCursor(Control Object, byte[] BYTES)
	{
	    var curse = new Cursor(CreateIconFromResource(BYTES, (uint)BYTES.Length, false, 0x00030000));

	    Object.Cursor = curse;
	    Object.Update();
	}

	public void Interactive(Control Object, Control Target)
	{
	    var Location = Point.Empty;

	    Object.MouseMove += (s, e) =>
	    {
		if (Location.IsEmpty)
		{
		    return;
		}

		Target.Location = new Point(Target.Location.X + (e.X - Location.X), Target.Location.Y + (e.Y - Location.Y));
	    };

	    Object.MouseDown += (s, e) =>
	    {
		Location = new Point(e.X, e.Y);
	    };

	    Object.MouseUp += (s, e) =>
	    {
		Location = Point.Empty;
	    };
	}

	public class ReadOnlyForm : Form
	{
	    public void PaintOwner(PaintEventArgs e)
	    {
		e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
		base.OnPaint(e);
	    }
	}

	public readonly ReadOnlyForm ReadForm = new ReadOnlyForm();

	public void Round(Control Object, int Radius)
	{
	    Object.Paint += (s, e) =>
	    {
		try
		{
		    ReadForm.PaintOwner(e);

		    GraphicsPath GraphicsPath = new GraphicsPath();
		    
		    var Rectangle = new Rectangle(0, 0, Object.Width, Object.Height);

		    int R = Radius * 3;

		    int H = Rectangle.Height;
		    int W = Rectangle.Width;

		    int X = Rectangle.X;
		    int Y = Rectangle.X;

		    GraphicsPath.AddArc(X, Y, R, R, 170, 90);
		    GraphicsPath.AddArc(X + W - R, Y, R, R, 270, 90);
		    GraphicsPath.AddArc(X + W - R, Y + H - R, R, R, 0, 90);
		    GraphicsPath.AddArc(X, Y + H - R, R, R, 80, 90);

		    Object.Region = new Region(GraphicsPath);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    };
	}

	[DllImport("gdi32.dll")] private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
	readonly PrivateFontCollection FontCollection = new PrivateFontCollection();

	public Font GetFont(int FontId, int Height)
	{
	    try
	    {
		if (FontCollection.Families.Length < 2)
		{
		    var RawDataCollection = new List<byte[]>()
		{
		    Resources.main,
		    Resources.cute,
		};

		    for (int k = 0; k < RawDataCollection.Count; k += 1)
		    {
			byte[] RawData = RawDataCollection[k];

			var Pointer = Marshal.AllocCoTaskMem(RawData.Length);

			Marshal.Copy(RawData, 0, Pointer, RawData.Length);

			uint Reference = 0;

			AddFontMemResourceEx(Pointer, (uint)RawData.Length, IntPtr.Zero, ref Reference);
			FontCollection.AddMemoryFont(Pointer, RawData.Length);
		    };
		};

		return new Font(FontCollection.Families[FontId], Height, FontStyle.Regular);
	    }

	    catch { /*Silenced for now.*/ return new Font("Modern", Height, FontStyle.Regular); };
	}
    }
}