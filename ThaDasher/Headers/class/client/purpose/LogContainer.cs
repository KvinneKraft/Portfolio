using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class LogContainer
    {
	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	readonly public static RichTextBox LOG = new RichTextBox();
	readonly public static PictureBox CONTAINER = new PictureBox();

	readonly public static Button CLEAR = new Button();

	public static void SetupLogContainer()
	{
	    LOG.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
	}

	public static void InitializeLCon(Form TOP)
	{
	    try
	    {
		var CONTAINER_SIZE = new Size(TOP.Width - 2 - TargetContainer.CONTAINER.Left - 14 - TargetContainer.CONTAINER.Width, TargetContainer.CONTAINER.Height + 5 + SettingsContainer.CONTAINER.Height);
		var CONTAINER_LOCA = new Point(TOP.Width - 1 - CONTAINER_SIZE.Width - 8, TargetContainer.CONTAINER.Top);
		var CONTAINER_COLA = TargetContainer.CONTAINER.BackColor;

		CONTROL.Image(TOP, CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		TOOL.Round(CONTAINER, 6);

		var LOG_SIZE = new Size(CONTAINER_SIZE.Width - 10, CONTAINER_SIZE.Height - 16);
		var LOG_LOCA = new Point(5, 8);
		var LOG_COLA = CONTAINER_COLA;

		CONTROL.RichTextBox(CONTAINER, LOG, LOG_SIZE, LOG_LOCA, LOG_COLA, Color.White, 1, 9, "Yes man.");

		SetupLogContainer();

		var CLEAR_SIZE = new Size(100, 26);
		var CLEAR_LOCA = new Point((LOG_SIZE.Width - CLEAR_SIZE.Width) / 2, LOG_SIZE.Height - CLEAR_SIZE.Height);
		var CLEAR_BCOL = Color.FromArgb(160, TOP.BackColor.R, TOP.BackColor.G, TOP.BackColor.B);
		var CLEAR_FCOL = Color.White;

		CONTROL.Button(LOG, CLEAR, CLEAR_SIZE, CLEAR_LOCA, CLEAR_BCOL, CLEAR_FCOL, 1, 10, "Clear", Color.Empty);

		string GetLogText()
		{
		    var TEXT =
		    (
			"-= Press F1 for shortcut keys =-\r\n"
		    );

		    return TEXT;
		}

		LOG.Text = GetLogText();

		CLEAR.Click += (s, e) =>
		    LOG.Clear();

		TOOL.Round(CLEAR, 6);

		var RECTANGLE_SIZE = CONTAINER_SIZE;
		var RECTANGLE_LOCA = new Point(0, 0);
		var RECTANGLE_COLA = Color.FromArgb(CONTAINER_COLA.R - 8, CONTAINER_COLA.G - 8, CONTAINER_COLA.B - 8);

		TOOL.PaintRectangle(CONTAINER, 4, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
	    }

	    catch (Exception e)
	    {
		throw new Exception(e.Message);
	    };
	}
    }
}