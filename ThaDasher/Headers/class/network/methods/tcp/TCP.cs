
//
// Author: Dashie
// Version: 1.0
//

using System;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class TCP
    {
	readonly private static DashControls CONTROL = new DashControls();
	readonly private static DashTools TOOL = new DashTools();

	public static class LONGSOCKS
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox CONNECTION_LIVE_TIME = new TextBox() { Text = "3500" };
	    public static TextBox MAX_CONNECTIONS = new TextBox() { Text = "650" };
	    public static TextBox BYTES_PER_CONNECTION = new TextBox() { Text = "3500" };
	
	    public static TextBox LABEL_1 = new TextBox() { Text = "C.L.T > (Connection Live Time)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "M.C.. > (Max Connections)" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "B.P.C > (Bytes Per Connection)" };
	}

	public static class MULTIFLOOD
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox CONNECTION_DELAY = new TextBox() { Text = "200" };
	    public static TextBox MAX_CONNECTIONS = new TextBox() { Text = "1500" };
	    public static TextBox BYTES_PER_CONNECTION = new TextBox() { Text = "5000" };
	    public static TextBox RECYCLE_WORKERS = new TextBox() { Text = "8" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "C.D.. > (Connection Delay)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "M.C.. > (Max Connections)" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "B.P.C > (Bytes Per Connection)" };
	    public static TextBox LABEL_4 = new TextBox() { Text = "R.W.. > (Re-Cycle Workers)" };
	}

	public static class MULTISOCKS
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox CONNECTION_DELAY = new TextBox() { Text = "350" };
	    public static TextBox CONNECTIONS_PER_TURN = new TextBox() { Text = "36" };
	    public static TextBox BYTES_PER_CONNECTION = new TextBox() { Text = "5000" };
	    public static TextBox RECYCLE_WORKERS = new TextBox() { Text = "8" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "C.D.. > (Connection Delay)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "C.P.T > (Connections Per Turn)" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "B.P.C > (Bytes Per Connection)" };
	    public static TextBox LABEL_4 = new TextBox() { Text = "R.W.. > (Re-Cycle Workers)" };
	}

	public static class WAVESSS
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox WAVE_DELAY = new TextBox() { Text = "5000" };
	    public static TextBox CONNECTIONS_PER_WAVE = new TextBox() { Text = "500" };
	    public static TextBox BYTES_PER_CONNECTION = new TextBox() { Text = "8500" };
	    public static TextBox RECYCLE_WORKERS = new TextBox() { Text = "16" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "W.D.. > (Wave Delay)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "C.P.W > (Connections Per Wave)" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "B.P.C > (Bytes Per Connection)" };
	    public static TextBox LABEL_4 = new TextBox() { Text = "R.W.. > (Re-Cycle Workers)" };
	}

	public void InitializeTCPConfiguration(Form CONTAINER)
	{
	    var CONTAINER_SIZE = new Size(CONTAINER.Width, CONTAINER.Height);
	    var CONTAINER_LOCA = new Point(0, 0);
	    var CONTAINER_COLA = Color.FromArgb(64, 25, 112);

	    var OPTION_SIZE = new Size(CONTAINER_SIZE.Width - 165, 26);
	    var OPTION_LOCA = new Point(165, 0);
	    var OPTION_BCOL = Color.FromArgb(64, 13, 53);//112, 25, 45);
	    var OPTION_FCOL = Color.White;

	    var LABEL_SIZE = new Size(OPTION_LOCA.X, OPTION_SIZE.Height);
	    var LABEL_LOCA = new Point(0, 0);
	    var LABEL_BCOL = Color.FromArgb(40, 13, 64);//64, 13, 25);
	    var LABEL_FCOL = Color.White;

	    void UpdateY(PictureBox container)
	    {
		OPTION_LOCA.Y = container.Controls[container.Controls.Count - 1].Height + container.Controls[container.Controls.Count - 1].Top;
		LABEL_LOCA.Y = OPTION_LOCA.Y;
	    }

	    void ResetY()
	    {
		OPTION_LOCA.Y = 0;
		LABEL_LOCA.Y = OPTION_LOCA.Y;
	    }

	    /*Long Socks*/
	    try
	    {
		CONTROL.Image(CONTAINER, LONGSOCKS.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(LONGSOCKS.CONTAINER, LONGSOCKS.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(LONGSOCKS.CONTAINER, LONGSOCKS.CONNECTION_LIVE_TIME, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(LONGSOCKS.CONTAINER);

		CONTROL.TextBox(LONGSOCKS.CONTAINER, LONGSOCKS.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(LONGSOCKS.CONTAINER, LONGSOCKS.MAX_CONNECTIONS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(LONGSOCKS.CONTAINER);

		CONTROL.TextBox(LONGSOCKS.CONTAINER, LONGSOCKS.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(LONGSOCKS.CONTAINER, LONGSOCKS.BYTES_PER_CONNECTION, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("LONGSOCKS");
	    }

	    ResetY();

	    /*Multi Flood*/
	    try
	    {
		CONTROL.Image(CONTAINER, MULTIFLOOD.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(MULTIFLOOD.CONTAINER, MULTIFLOOD.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(MULTIFLOOD.CONTAINER, MULTIFLOOD.CONNECTION_DELAY, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(MULTIFLOOD.CONTAINER);

		CONTROL.TextBox(MULTIFLOOD.CONTAINER, MULTIFLOOD.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(MULTIFLOOD.CONTAINER, MULTIFLOOD.MAX_CONNECTIONS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(MULTIFLOOD.CONTAINER);

		CONTROL.TextBox(MULTIFLOOD.CONTAINER, MULTIFLOOD.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(MULTIFLOOD.CONTAINER, MULTIFLOOD.BYTES_PER_CONNECTION, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(MULTIFLOOD.CONTAINER);

		CONTROL.TextBox(MULTIFLOOD.CONTAINER, MULTIFLOOD.LABEL_4, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(MULTIFLOOD.CONTAINER, MULTIFLOOD.RECYCLE_WORKERS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("MULTIFLOOD");
	    }

	    ResetY();

	    /*Multi Socks*/
	    try
	    {
		CONTROL.Image(CONTAINER, MULTISOCKS.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(MULTISOCKS.CONTAINER, MULTISOCKS.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(MULTISOCKS.CONTAINER, MULTISOCKS.CONNECTION_DELAY, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(MULTISOCKS.CONTAINER);

		CONTROL.TextBox(MULTISOCKS.CONTAINER, MULTISOCKS.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(MULTISOCKS.CONTAINER, MULTISOCKS.CONNECTIONS_PER_TURN, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(MULTISOCKS.CONTAINER);

		CONTROL.TextBox(MULTISOCKS.CONTAINER, MULTISOCKS.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(MULTISOCKS.CONTAINER, MULTISOCKS.BYTES_PER_CONNECTION, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(MULTISOCKS.CONTAINER);

		CONTROL.TextBox(MULTISOCKS.CONTAINER, MULTISOCKS.LABEL_4, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(MULTISOCKS.CONTAINER, MULTISOCKS.RECYCLE_WORKERS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("MULTISOCKS");
	    }

	    ResetY();

	    /*Wavesss*/
	    try
	    {
		CONTROL.Image(CONTAINER, WAVESSS.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(WAVESSS.CONTAINER, WAVESSS.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(WAVESSS.CONTAINER, WAVESSS.WAVE_DELAY, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(WAVESSS.CONTAINER);

		CONTROL.TextBox(WAVESSS.CONTAINER, WAVESSS.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(WAVESSS.CONTAINER, WAVESSS.CONNECTIONS_PER_WAVE, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(WAVESSS.CONTAINER);

		CONTROL.TextBox(WAVESSS.CONTAINER, WAVESSS.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(WAVESSS.CONTAINER, WAVESSS.BYTES_PER_CONNECTION, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(WAVESSS.CONTAINER);

		CONTROL.TextBox(WAVESSS.CONTAINER, WAVESSS.LABEL_4, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(WAVESSS.CONTAINER, WAVESSS.RECYCLE_WORKERS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("WAVESSS");
	    }
	}
    }
}