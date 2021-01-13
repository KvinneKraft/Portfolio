using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class TargetContainer
    {
	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	readonly public static PictureBox CONTAINER = new PictureBox();

	readonly public static TextBox IP_BOX = new TextBox() { Text = "https://pugpawz.com/", TextAlign = HorizontalAlignment.Center };
	readonly public static TextBox PO_BOX = new TextBox() { Text = "8080", TextAlign = HorizontalAlignment.Center };
	readonly public static TextBox DR_BOX = new TextBox() { Text = "360", TextAlign = HorizontalAlignment.Center };

	readonly static Label IP_LAB = new Label();
	readonly static Label PO_LAB = new Label();
	readonly static Label DR_LAB = new Label();

	public static void InitializeHCon(Form TOP)
	{
	    try
	    {
		var CONTAINER_SIZE = new Size(200, 83);
		var CONTAINER_LOCA = new Point(8, MENUBAR.BAR.Height + 7);
		var CONTAINER_COLA = Color.FromArgb(28, 28, 28);

		CONTROL.Image(TOP, CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);
		TOOL.Round(CONTAINER, 6);

		void RoundTextBox() =>
		    TOOL.Round(CONTAINER.Controls[CONTAINER.Controls.Count - 1], 6);

		Size GetFontSize(string TEXT) =>
		    TOOL.GetFontSize(TEXT, 12);

		int GetAvailableWidth(Size BASE) =>
		    CONTAINER.Width - BASE.Width - 15;

		var IPLAB_TEXT = "IP:";
		var IPLAB_SIZE = GetFontSize(IPLAB_TEXT);
		var IPLAB_LOCA = new Point(6, 5);
		var IPLAB_BCOL = Color.Transparent;
		var IPLAB_FCOL = Color.White;

		CONTROL.Label(CONTAINER, IP_LAB, IPLAB_SIZE, IPLAB_LOCA, IPLAB_BCOL, IPLAB_FCOL, 1, 12, IPLAB_TEXT);

		var IPBOX_SIZE = new Size(GetAvailableWidth(IPLAB_SIZE), 20);
		var IPBOX_LOCA = new Point(IPLAB_LOCA.X + IPLAB_SIZE.Width, 6);
		var IPBOX_BCOL = Color.FromArgb(16, 16, 16);
		var IPBOX_FCOL = Color.White;

		CONTROL.TextBox(CONTAINER, IP_BOX, IPBOX_SIZE, IPBOX_LOCA, IPBOX_BCOL, IPBOX_FCOL, 1, 10, Color.Empty);
		RoundTextBox();

		var POLAB_TEXT = "Port:";
		var POLAB_SIZE = GetFontSize(POLAB_TEXT);
		var POLAB_LOCA = new Point(IPLAB_LOCA.X, IPLAB_LOCA.Y + IPLAB_SIZE.Height + 5);
		var POLAB_BCOL = Color.Transparent;
		var POLAB_FCOL = Color.White;

		CONTROL.Label(CONTAINER, PO_LAB, POLAB_SIZE, POLAB_LOCA, POLAB_BCOL, POLAB_FCOL, 1, 12, POLAB_TEXT);

		var POBOX_SIZE = new Size(GetAvailableWidth(POLAB_SIZE), IPBOX_SIZE.Height);
		var POBOX_LOCA = new Point(POLAB_LOCA.X + POLAB_SIZE.Width, POLAB_LOCA.Y + 2);
		var POBOX_BCOL = IPBOX_BCOL;
		var POBOX_FCOL = IPBOX_FCOL;

		CONTROL.TextBox(CONTAINER, PO_BOX, POBOX_SIZE, POBOX_LOCA, POBOX_BCOL, POBOX_FCOL, 1, 10, Color.Empty);
		RoundTextBox();

		var DULAB_TEXT = "Duration:";
		var DULAB_SIZE = GetFontSize(DULAB_TEXT);
		var DULAB_LOCA = new Point(IPLAB_LOCA.X, POLAB_LOCA.Y + POLAB_SIZE.Height + 5);
		var DULAB_BCOL = Color.Transparent;
		var DULAB_FCOL = Color.White;

		CONTROL.Label(CONTAINER, DR_LAB, DULAB_SIZE, DULAB_LOCA, DULAB_BCOL, DULAB_FCOL, 1, 12, DULAB_TEXT);

		var DUBOX_SIZE = new Size(GetAvailableWidth(DULAB_SIZE), IPBOX_SIZE.Height);
		var DUBOX_LOCA = new Point(DULAB_LOCA.X + DULAB_SIZE.Width, DULAB_LOCA.Y + 2);
		var DUBOX_BCOL = IPBOX_BCOL;
		var DUBOX_FCOL = IPBOX_FCOL;

		CONTROL.TextBox(CONTAINER, DR_BOX, DUBOX_SIZE, DUBOX_LOCA, DUBOX_BCOL, DUBOX_FCOL, 1, 10, Color.Empty);
		RoundTextBox();

		var RECTANGLE_SIZE = CONTAINER_SIZE;
		var RECTANGLE_LOCA = new Point(0, 0);
		var RECTANGLE_COLA = Color.FromArgb(CONTAINER_COLA.R - 8, CONTAINER_COLA.G - 8, CONTAINER_COLA.B - 8);

		TOOL.PaintRectangle(CONTAINER, 4, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
	    }

	    catch
	    {
		throw new Exception("IP/Port Section.");
	    };
	}
    };
}