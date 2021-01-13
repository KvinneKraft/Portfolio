
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
    public class UDP
    {
	readonly private static DashControls CONTROL = new DashControls();
	readonly private static DashTools TOOL = new DashTools();

	public static class OVERLOAD
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox CONNECTION_LIVE_TIME = new TextBox() { Text = "100" };
	    public static TextBox MAX_CONNECTIONS = new TextBox() { Text = "50000" };
	    public static TextBox BYTES_PER_CONNECTION = new TextBox() { Text = "5000" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "C.L.T > (Connection Live Time)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "M.C.. > (Max Connections)" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "B.P.C > (Bytes Per Connection)" };
	}

	public static class WAVYBABY
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox WAVE_DELAY = new TextBox() { Text = "1500" };
	    public static TextBox CONNECTION_DELAY = new TextBox() { Text = "100" };
	    public static TextBox BYTES_PER_PACKET = new TextBox() { Text = "5000" };
	    public static TextBox CORE_WORKERS = new TextBox() { Text = "5" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "W.D.. > (Wave Delay)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "C.D.. > (Connection Delay)" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "B.P.P > (Bytes Per Packet)" };
	    public static TextBox LABEL_4 = new TextBox() { Text = "C.W.. > (Core Workers)" };
	}

	public static class GOHAM
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox BYTES_PER_PACKET = new TextBox() { Text = "5000" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "B.P.P > (Bytes Per Packet)" };
	}

	public static class INSTAFLOOD
	{
	    public static PictureBox CONTAINER = new PictureBox() { Visible = false };

	    public static TextBox BYTES_PER_PACKET = new TextBox() { Text = "2375" };
	    public static TextBox CORE_WORKERS = new TextBox() { Text = "16" };
	    public static TextBox WORKERS = new TextBox() { Text = "8" };

	    public static TextBox LABEL_1 = new TextBox() { Text = "B.P.P > (Bytes Per Packet)" };
	    public static TextBox LABEL_2 = new TextBox() { Text = "C.W.. > (Core Workers)" };
	    public static TextBox LABEL_3 = new TextBox() { Text = "W.... > (Workers)" };
	}

	public void InitializeUDPConfiguration(Form CONTAINER)
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

	    /*Overload*/
	    try
	    {
		CONTROL.Image(CONTAINER, OVERLOAD.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(OVERLOAD.CONTAINER, OVERLOAD.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(OVERLOAD.CONTAINER, OVERLOAD.CONNECTION_LIVE_TIME, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(OVERLOAD.CONTAINER);

		CONTROL.TextBox(OVERLOAD.CONTAINER, OVERLOAD.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(OVERLOAD.CONTAINER, OVERLOAD.MAX_CONNECTIONS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(OVERLOAD.CONTAINER);

		CONTROL.TextBox(OVERLOAD.CONTAINER, OVERLOAD.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(OVERLOAD.CONTAINER, OVERLOAD.BYTES_PER_CONNECTION, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("OVERLOAD");
	    }

	    ResetY();

	    /*Wavy Baby*/
	    try
	    {
		CONTROL.Image(CONTAINER, WAVYBABY.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(WAVYBABY.CONTAINER, WAVYBABY.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(WAVYBABY.CONTAINER, WAVYBABY.WAVE_DELAY, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(WAVYBABY.CONTAINER);

		CONTROL.TextBox(WAVYBABY.CONTAINER, WAVYBABY.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(WAVYBABY.CONTAINER, WAVYBABY.CONNECTION_DELAY, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(WAVYBABY.CONTAINER);

		CONTROL.TextBox(WAVYBABY.CONTAINER, WAVYBABY.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(WAVYBABY.CONTAINER, WAVYBABY.BYTES_PER_PACKET, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(WAVYBABY.CONTAINER);

		CONTROL.TextBox(WAVYBABY.CONTAINER, WAVYBABY.LABEL_4, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(WAVYBABY.CONTAINER, WAVYBABY.CORE_WORKERS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("WAVYBABY");
	    }

	    ResetY();

	    /*Go Ham*/
	    try
	    {
		CONTROL.Image(CONTAINER, GOHAM.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(GOHAM.CONTAINER, GOHAM.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(GOHAM.CONTAINER, GOHAM.BYTES_PER_PACKET, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("GOHAM");
	    }

	    ResetY();

	    /*Insta Flood*/
	    try
	    {
		CONTROL.Image(CONTAINER, INSTAFLOOD.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		CONTROL.TextBox(INSTAFLOOD.CONTAINER, INSTAFLOOD.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(INSTAFLOOD.CONTAINER, INSTAFLOOD.BYTES_PER_PACKET, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(INSTAFLOOD.CONTAINER);

		CONTROL.TextBox(INSTAFLOOD.CONTAINER, INSTAFLOOD.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(INSTAFLOOD.CONTAINER, INSTAFLOOD.CORE_WORKERS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		UpdateY(INSTAFLOOD.CONTAINER);

		CONTROL.TextBox(INSTAFLOOD.CONTAINER, INSTAFLOOD.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		CONTROL.TextBox(INSTAFLOOD.CONTAINER, INSTAFLOOD.WORKERS, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);
	    }

	    catch
	    {
		throw new Exception("INSTAFLOOD");
	    }
	}
    }
}