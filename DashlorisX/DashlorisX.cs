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

		Tools.Round(this, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox MainContainer = new PictureBox();
	readonly PictureBox InnerMainContainer = new PictureBox();

	readonly static Label Duration_L = new Label();
	readonly static Label Bytes_L = new Label();
	readonly static Label Host_L = new Label();
	readonly static Label Port_L = new Label();

	readonly List<Label> LabelObjects = new List<Label>() { Host_L, Bytes_L, Port_L, Duration_L };

	readonly static TextBox Duration_T = new TextBox() { Text = "4500", TextAlign = HorizontalAlignment.Center };
	readonly static TextBox Bytes_T = new TextBox() { Text = "1024", TextAlign = HorizontalAlignment.Center };
	readonly static TextBox Host_T = new TextBox() { Text = "https://www.google.co.uk", TextAlign = HorizontalAlignment.Center };
	readonly static TextBox Port_T = new TextBox() { Text = "65535", TextAlign = HorizontalAlignment.Center };

	readonly List<TextBox> TextBoxObjects = new List<TextBox>() { Host_T, Bytes_T, Port_T, Duration_T };

	private void InitializeMainField()
	{
	    try//Clean this up:
	    {
		var MCONTA_SIZE = new Size(Width - 22, 65);
		var MCONTA_LOCA = new Point(11, MenuBar.Bar.Height + 10);
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

		var LabelTexts = new List<string>() { "Host", "Bytes", "Port", "Duration" };
		var TextBoxWidths = new List<int>() { 145, 95 };

		for (int k1 = 0, tid = 0, h1 = 20; k1 < 2; k1 += 1)
		{
		    try
		    {
			int x1 = 0;
			int y1 = 0;

			int w3 = 0;
			int x3 = 0;

			if (k1 >= 1)//When layer 1 is done
			{
			    y1 += LabelObjects[tid - 1].Height + LabelObjects[tid - 1].Top + 5;
			}

			for (int k2 = 0; k2 < 2; k2 += 1)
			{
			    var LText = LabelTexts[tid];
			    var LSize = Tools.GetFontSize(LText, 11);
			    
			    if (k2 > 0)
			    {
				x1 += w3 + x3 + 10;
			    }

			    var LLoca = new Point(x1, y1);

			    Controls.Label(InnerMainContainer, LabelObjects[tid], LSize, LLoca, InnerMainContainer.BackColor, Color.White, 1, 11, LText);

			    var w2 = TextBoxWidths[k1];

			    if (k2 > 0)
			    {
				w2 = InnerMainContainer.Width - LLoca.X - LSize.Width;
			    }
			    
			    var TLoca = new Point(LLoca.X + LSize.Width, y1);
			    var TSize = new Size(w2, h1);

			    x3 = TLoca.X;
			    w3 = w2;

			    Controls.TextBox(InnerMainContainer, TextBoxObjects[tid], TSize, TLoca, Color.FromArgb(8, 8, 8), Color.White, 1, 9, Color.Empty);

			    tid += 1;
			}
		    }

		    catch (Exception E)
		    {
			MessageBox.Show($"{E.Message}\r\n|\r\n{E.StackTrace}");
		    }
		}
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
		ErrorHandler.Utilize($"----------------------\r\n{E.StackTrace}\r\n----------------------\r\n{E.Message}\r\n----------------------\r\n{E.Source}\r\n----------------------", "Error Handler");
	    }
	}
    }
}
