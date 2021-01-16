// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

using DashlorisX.Properties;

namespace DashlorisX
{
    public partial class DashlorisX : Form
    {
	new public readonly DashControls Controls = new DashControls();
	public readonly DashTools Tools = new DashTools();

	public readonly DashMenuBar MenuBar = new DashMenuBar();

	private void InitializeMenuBar()
	{
	    try
	    {
		Color BAR_COLA = Color.FromArgb(8, 8, 8);
		MenuBar.Add(this, 26, BAR_COLA, BAR_COLA);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void ReinitializeComponent()
	{
	    try
	    {
		BackColor = Color.MidnightBlue;
		Icon = Resources.ICON;

		Tools.Round(this, 4);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox MainContainer = new PictureBox();
	readonly PictureBox InnerMainContainer = new PictureBox();

	readonly Label Duration_L = new Label();
	readonly Label Bytes_L = new Label();
	readonly Label Host_L = new Label();
	readonly Label Port_L = new Label();

	readonly TextBox Duration_T = new TextBox() { Text = "4500", TextAlign = HorizontalAlignment.Center };
	readonly TextBox Bytes_T = new TextBox() { Text = "1024", TextAlign = HorizontalAlignment.Center };
	readonly TextBox Host_T = new TextBox() { Text = "https://www.google.co.uk", TextAlign = HorizontalAlignment.Center };
	readonly TextBox Port_T = new TextBox() { Text = "65535", TextAlign = HorizontalAlignment.Center };

	private void InitializeMainField()
	{
	    try//Clean this up:
	    {
		var MCONTA_SIZE = new Size(Width - 20, 65);
		var MCONTA_LOCA = new Point(10, MenuBar.Bar.Height + 10);
		var MCONTA_COLA = Color.FromArgb(16, 16, 16);

		Controls.Image(this, MainContainer, MCONTA_SIZE, MCONTA_LOCA, null, MCONTA_COLA);
		Tools.Round(MainContainer, 6);

		var MRECT_SIZE = new Size(MCONTA_SIZE.Width - 2, MCONTA_SIZE.Height - 2);
		var MRECT_LOCA = new Point(1, 1);
		var MRECT_COLA = Color.FromArgb(8, 8, 8);

		Tools.PaintRectangle(MainContainer, 2, MRECT_SIZE, MRECT_LOCA, MRECT_COLA);

		var ICONTA_SIZE = new Size(MCONTA_SIZE.Width - 22, MCONTA_SIZE.Height - 20/*Measure it*/);
		var ICONTA_LOCA = new Point(10, 10);
		var ICONTA_COLA = MCONTA_COLA;

		Controls.Image(MainContainer, InnerMainContainer, ICONTA_SIZE, ICONTA_LOCA, null, ICONTA_COLA);

		var LABEL_TEXT = "Host";
		var LABEL_SIZE = Tools.GetFontSize(LABEL_TEXT, 11);
		var LABEL_LOCA = new Point(0, 0);

		Controls.Label(InnerMainContainer, Host_L, LABEL_SIZE, LABEL_LOCA, InnerMainContainer.BackColor, Color.White, 1, 11, LABEL_TEXT);
		
		var TEXTBOX_SIZE = new Size(145, 20);
		var TEXTBOX_LOCA = new Point(Host_L.Width + Host_L.Left, Host_L.Top);
		var TEXTBOX_BCOL = Color.FromArgb(8, 8, 8);

		Controls.TextBox(InnerMainContainer, Host_T, TEXTBOX_SIZE, TEXTBOX_LOCA, TEXTBOX_BCOL, Color.White, 1, 9, Color.Empty);

		LABEL_TEXT = "Bytes";
		LABEL_SIZE = Tools.GetFontSize(LABEL_TEXT, 11);
		LABEL_LOCA = new Point(TEXTBOX_SIZE.Width + TEXTBOX_LOCA.X + 10, 0);

		Controls.Label(InnerMainContainer, Bytes_L, LABEL_SIZE, LABEL_LOCA, InnerMainContainer.BackColor, Color.White, 1, 11, LABEL_TEXT);

		TEXTBOX_SIZE = new Size(InnerMainContainer.Width - Bytes_L.Left - Bytes_L.Width, TEXTBOX_SIZE.Height);
		TEXTBOX_LOCA = new Point(Bytes_L.Width + Bytes_L.Left, Bytes_L.Top);

		Controls.TextBox(InnerMainContainer, Bytes_T, TEXTBOX_SIZE, TEXTBOX_LOCA, TEXTBOX_BCOL, Color.White, 1, 9, Color.Empty);

		LABEL_TEXT = "Port";
		LABEL_SIZE = Tools.GetFontSize(LABEL_TEXT, 11);
		LABEL_LOCA = new Point(0, TEXTBOX_SIZE.Height + TEXTBOX_LOCA.Y + 5);

		Controls.Label(InnerMainContainer, Port_L, LABEL_SIZE, LABEL_LOCA, InnerMainContainer.BackColor, Color.White, 1, 11, LABEL_TEXT);

		TEXTBOX_SIZE = new Size(95, TEXTBOX_SIZE.Height);
		TEXTBOX_LOCA = new Point(Port_L.Width + Port_L.Left, Port_L.Top);

		Controls.TextBox(InnerMainContainer, Port_T, TEXTBOX_SIZE, TEXTBOX_LOCA, TEXTBOX_BCOL, Color.White, 1, 9, Color.Empty);

		LABEL_TEXT = "Duration";
		LABEL_SIZE = Tools.GetFontSize(LABEL_TEXT, 11);
		LABEL_LOCA = new Point(TEXTBOX_SIZE.Width + TEXTBOX_LOCA.X + 10, Port_L.Top);

		Controls.Label(InnerMainContainer, Duration_L, LABEL_SIZE, LABEL_LOCA, InnerMainContainer.BackColor, Color.White, 1, 11, LABEL_TEXT);

		TEXTBOX_SIZE = new Size(InnerMainContainer.Width - Duration_L.Left - Duration_L.Width, TEXTBOX_SIZE.Height);
		TEXTBOX_LOCA = new Point(Duration_L.Width + Duration_L.Left, Duration_L.Top);

		Controls.TextBox(InnerMainContainer, Duration_T, TEXTBOX_SIZE, TEXTBOX_LOCA, TEXTBOX_BCOL, Color.White, 1, 9, Color.Empty);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox OptionContainer = new PictureBox();
	readonly PictureBox InnerOptionContainer = new PictureBox();

	readonly Button Properties = new Button();
	readonly Button Online = new Button();
	readonly Button Launch = new Button();
	readonly Button About = new Button();

	private void InitializeOptionField()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void InitializeInterfa()
	{
	    ReinitializeComponent();
	    InitializeMainField(); 
	    InitializeOptionField();
	}

	public DashlorisX()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeInterfa();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize($"{E.StackTrace}\r\n----------------------\r\n{E.Message}", "Error Handler");
	    }
	}
    }
}
