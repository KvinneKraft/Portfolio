
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
    public class HTTP
    {
	readonly private static DashControls CONTROL = new DashControls();
	readonly private static DashTools TOOL = new DashTools();

	public static class DASHLORIS4
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox CONNECTION_BURST_DELAY = new TextBox() { Text = "5000" };
	    public static TextBox CYCLE_CONNECTION_COUNT = new TextBox() { Text = "32" };
	    public static TextBox CONNECTION_LIVE_TIME = new TextBox() { Text = "5000" };
	    public static TextBox RANDOM_USER_AGENT = new TextBox() { Text = "True" };
	    public static TextBox BYTES = new TextBox() { Text = "1000" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "C.B.D > (Connection Burst Delay)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "C.C.C > (Cycle Connection Count)" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "C.L.T > (Connection Live Time)" };
	    public static TextBox LABEL_4 = new TextBox() { Text = "R.U.A > (Random User-Agent[true/false])" };
	    public static TextBox LABEL_5 = new TextBox() { Text = "B.P.C > (Header Size Basically)" };
	}

	public static class SLOWLORIS2
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox CONNECTION_BURST_DELAY = new TextBox() { Text = "3500" };
	    public static TextBox CONNECTION_LIVE_TIME = new TextBox() { Text = "3500" };
	    public static TextBox RANDOM_USER_AGENT = new TextBox() { Text = "True" };
	    public static TextBox HEADER = new TextBox() { Text = "Click Me To Change" };
	    public static TextBox BYTES = new TextBox() { Text = "1000" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "C.B.D > (Connection Burst Delay)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "C.L.T > (Connection Live Time)" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "R.U.A > (Random User-Agent[true/false])" };
	    public static TextBox LABEL_4 = new TextBox() { Text = "H.T.S > (Header To Send)" };
	    public static TextBox LABEL_5 = new TextBox() { Text = "B.P.C > (Header Size Basically)" };
	}

	public static class PUTPOSTGET
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox CONNECTION_TIMEOUT = new TextBox() { Text = "500" };
	    public static TextBox RANDOM_USER_AGENT = new TextBox() { Text = "True" };
	    public static TextBox CORE_WORKERS = new TextBox() { Text = "4" };
	    public static TextBox WORKERS = new TextBox() { Text = "16" };
	    public static TextBox HEADER = new TextBox() { Text = "Click Me To Change" };
	    public static TextBox BYTES = new TextBox() { Text = "1000" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "C.T.. > (Connection Timeout)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "R.U.A > (Random User-Agent[true/false])" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "C.C.W > (Core Connection Workers)" };
	    public static TextBox LABEL_4 = new TextBox() { Text = "W.... > (Workers)" };
	    public static TextBox LABEL_5 = new TextBox() { Text = "H.T.S > (Header To Send)" };
	    public static TextBox LABEL_6 = new TextBox() { Text = "B.P.C > (Header Size Basically)" };
	}

	public static class HFLOOD
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox CONNECTIONS_PER_THREAD = new TextBox() { Text = "32" };
	    public static TextBox RANDOM_USER_AGENT = new TextBox() { Text = "True" };
	    public static TextBox WORKERS = new TextBox() { Text = "32" };
	    public static TextBox HEADER = new TextBox() { Text = "Click Me To Change" };
	    public static TextBox BYTES = new TextBox() { Text = "1000" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "C.P.T > (Connections Per Thread)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "R.U.A > (Random User-Agent[true/false])" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "W.... > (Workers)" };
	    public static TextBox LABEL_4 = new TextBox() { Text = "H.T.S > (Header To Send)" };
	    public static TextBox LABEL_5 = new TextBox() { Text = "B.P.C > (Header Size Basically)" };
	}

	public void InitializeHTTPConfiguration(Form CONTAINER)
	{
	    //
	    // Add HTTP Options to Containers. Start with Dashloris
	    // 
	    //
	    // Dictionary<string[TYPE_METHOD], List<string[VALUES]>>
	    // Since we know the method, we also know what values to extract.
	    //

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
	    
	    /*Dashloris 4.0*/
	    try
	    {
		CONTROL.Image(CONTAINER, DASHLORIS4.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.CONNECTION_BURST_DELAY, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(DASHLORIS4.CONTAINER);

		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.CYCLE_CONNECTION_COUNT, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(DASHLORIS4.CONTAINER);

		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.CONNECTION_LIVE_TIME, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(DASHLORIS4.CONTAINER);

		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.LABEL_4, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.RANDOM_USER_AGENT, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(DASHLORIS4.CONTAINER);

		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.LABEL_5, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(DASHLORIS4.CONTAINER, DASHLORIS4.BYTES, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("DASHLORIS4");
	    };

	    ResetY();

	    /*Slowloris 2.0*/
	    try
	    {
		CONTROL.Image(CONTAINER, SLOWLORIS2.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.CONNECTION_BURST_DELAY, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(SLOWLORIS2.CONTAINER);

		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.CONNECTION_LIVE_TIME, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(SLOWLORIS2.CONTAINER);

		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.RANDOM_USER_AGENT, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(SLOWLORIS2.CONTAINER);

		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.LABEL_4, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.HEADER, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(SLOWLORIS2.CONTAINER);

		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.LABEL_5, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(SLOWLORIS2.CONTAINER, SLOWLORIS2.BYTES, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("SLOWLORIS2");
	    }

	    ResetY();

	    /*PUT POST AND GET*/
	    try
	    {
		CONTROL.Image(CONTAINER, PUTPOSTGET.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.CONNECTION_TIMEOUT, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(PUTPOSTGET.CONTAINER);

		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.RANDOM_USER_AGENT, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(PUTPOSTGET.CONTAINER);

		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.CORE_WORKERS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(PUTPOSTGET.CONTAINER);

		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.LABEL_4, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.WORKERS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(PUTPOSTGET.CONTAINER);

		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.LABEL_5, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.HEADER, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(PUTPOSTGET.CONTAINER);

		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.LABEL_6, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(PUTPOSTGET.CONTAINER, PUTPOSTGET.BYTES, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("PUT, POST AND GET");
	    }

	    ResetY();

	    /*Header Flood*/
	    try
	    {
		CONTROL.Image(CONTAINER, HFLOOD.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.CONNECTIONS_PER_THREAD, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(HFLOOD.CONTAINER);

		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.RANDOM_USER_AGENT, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(HFLOOD.CONTAINER);

		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.WORKERS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(HFLOOD.CONTAINER);

		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.LABEL_4, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.HEADER, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(HFLOOD.CONTAINER);

		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.LABEL_5, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(HFLOOD.CONTAINER, HFLOOD.BYTES, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("H-FLOOD");
	    }
	}
    }
}
