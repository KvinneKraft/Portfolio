﻿// Author: Dashie
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
	static readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X");
	new readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();

	private void InitializeMenuBar()
	{
	    try
	    {
		Color MenuBarBColor = Color.FromArgb(19, 36, 64);
		MenuBar.Add(this, 26, MenuBarBColor, MenuBarBColor);
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
		BackColor = Color.FromArgb(6, 17, 33);//MidnightBlue;
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

	readonly List<Label> LabelObjects = new List<Label>()
	{
	    Host_L, Bytes_L,
	    Port_L, Duration_L
	};
	
	readonly static Label Duration_L = new Label();
	readonly static Label Bytes_L = new Label();
	readonly static Label Host_L = new Label();
	readonly static Label Port_L = new Label();

	readonly List<TextBox> TextBoxObjects = new List<TextBox>()
	{
	    Host_T, Bytes_T,
	    Port_T, Duration_T
	};

	readonly static TextBox Duration_T = new TextBox() { Text = "4500", TextAlign = HorizontalAlignment.Center };
	readonly static TextBox Bytes_T = new TextBox() { Text = "1024", TextAlign = HorizontalAlignment.Center };
	readonly static TextBox Host_T = new TextBox() { Text = "https://www.google.co.uk", TextAlign = HorizontalAlignment.Center };
	readonly static TextBox Port_T = new TextBox() { Text = "65535", TextAlign = HorizontalAlignment.Center };

	public enum MainContainerObject
	{
	    Host = 0, Bytes = 1,
	    Port = 2, Duration = 3
	};

	private void InitializeMainField()
	{
	    var MContainerSize = new Size(Width - 22, 64);
	    var MContainerLocation = new Point(11, MenuBar.Bar.Height + 10);
	    var MContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Controls.Image(this, MainContainer, MContainerSize, MContainerLocation, null, MContainerBColor);
		Tools.Round(MainContainer, 8);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var IContainerSize = new Size(MContainerSize.Width - 22, MContainerSize.Height - 21/*Measure it*/);
	    var IContainerLocation = new Point(11, 11);
	    var IContainerBColor = MContainerBColor;

	    try
	    {
		Controls.Image(MainContainer, InnerMainContainer, IContainerSize, IContainerLocation, null, IContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    try
	    {
		var LabelTexts = new List<string>() { "Host:", "Bytes:", "Port:", "Duration:" };
		var TextBoxWidths = new List<int>() { 145, 95 };

		for (int k1 = 0, tid = 0, h1 = 19; k1 < 2; k1 += 1)
		{
		    int x1 = 0;
		    int y1 = 0;

		    int w3 = 0;
		    int x3 = 0;

		    if (k1 >= 1)
		    {
			y1 += LabelObjects[tid - 1].Height + LabelObjects[tid - 1].Top + 5;
		    }

		    for (int k2 = 0; k2 < 2; k2 += 1)
		    {
			var LText = LabelTexts[tid];
			var LSize = Tools.GetFontSize(LText, 11);

			if (k2 > 0) x1 += (w3 + x3 + 10);

			var LLoca = new Point(x1, y1);

			Controls.Label(InnerMainContainer, LabelObjects[tid], LSize, LLoca, InnerMainContainer.BackColor, Color.White, 1, 11, LText);

			int w2 = TextBoxWidths[k1];

			if (k2 > 0) w2 = (InnerMainContainer.Width - LLoca.X - LSize.Width);

			var TLoca = new Point(LLoca.X + LSize.Width, y1);
			var TSize = new Size(w2, h1);

			x3 = TLoca.X;
			w3 = w2;

			Controls.TextBox(InnerMainContainer, TextBoxObjects[tid], TSize, TLoca, Color.FromArgb(10, 10, 10), Color.White, 1, 9, Color.Empty);
			Tools.Round(InnerMainContainer.Controls[InnerMainContainer.Controls.Count - 1], 4);

			tid += 1;
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var MRectangleSize = new Size(MContainerSize.Width - 2, MContainerSize.Height - 2);
	    var MRectangleLocation = new Point(1, 1);
	    var MRectangleBColor = Color.FromArgb(8, 8, 8);

	    try
	    {
		Tools.PaintRectangle(MainContainer, 2, MRectangleSize, MRectangleLocation, MRectangleBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox OptionContainer = new PictureBox();
	readonly PictureBox InnerOptionContainer = new PictureBox();

	readonly List<Button> ButtonObjects = new List<Button>()
	{
	    Launch, Properties,
	    Online, About
	};

	readonly static Button Properties = new Button();
	readonly static Button Online = new Button();
	readonly static Button Launch = new Button();
	readonly static Button About = new Button();

	public enum OptionContainerObject
	{
	    Launch = 0, Properties = 1,
	    Online = 2, About = 3
	}

	private void InitializeOptionField()
	{
	    var OContainerSize = new Size(Width - 20, 81);
	    var OContainerLocation = new Point(10, MainContainer.Top + MainContainer.Height + 10);
	    var OContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Controls.Image(this, OptionContainer, OContainerSize, OContainerLocation, null, OContainerBColor);
		Tools.Round(OptionContainer, 8);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var IOptionContainerSize = new Size(OContainerSize.Width - 28, OContainerSize.Height - 28);
	    var IOptionContainerLocation = new Point(14, 14);
	    var IOptionContainerBColor = OContainerBColor;

	    try
	    {
		Controls.Image(OptionContainer, InnerOptionContainer, IOptionContainerSize, IOptionContainerLocation, null, IOptionContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var ButtonSize = new Size((InnerOptionContainer.Width - 8) / 2, 26);
	    var ButtonBColor = Color.FromArgb(3, 18, 26);//Kulakov Created These Colours!
	    var ButtonFColor = Color.White;

	    var ButtonTexts = new List<string>() { "Launch", "Settings", "Online", "About" };

	    try
	    {
		var y = 0;

		for (int k = 0, p = 0; k < ButtonTexts.Count / 2; k += 1)
		{
		    var x = 0;

		    for (int s = 0; s < 2; s += 1, p += 1)
		    {
			var BUTTO_LOCA = new Point(x, y);

			Controls.Button(InnerOptionContainer, ButtonObjects[p], ButtonSize, BUTTO_LOCA, ButtonBColor, ButtonFColor, 1, 10, ButtonTexts[p], Color.Empty);
			Tools.Round(ButtonObjects[p], 8);

			x = ButtonSize.Width + ButtonObjects[p].Left + 8;
		    }

		    y += ButtonSize.Height + ButtonObjects[p - 1].Top + 8;
		}

		Tools.Resize(InnerOptionContainer, new Size(InnerOptionContainer.Width, ButtonObjects[ButtonObjects.Count - 1].Top + ButtonObjects[ButtonObjects.Count - 1].Height));
		Tools.Resize(OptionContainer, new Size(OptionContainer.Width, ButtonObjects[ButtonObjects.Count - 1].Top + ButtonObjects[ButtonObjects.Count - 1].Height + 28));
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var MRectangleSize = new Size(OptionContainer.Width - 2, OptionContainer.Height - 2);
	    var MRectangleLocation = new Point(1, 1);
	    var MRectangleBColor = Color.FromArgb(8, 8, 8);

	    try
	    {
		Tools.PaintRectangle(OptionContainer, 2, MRectangleSize, MRectangleLocation, MRectangleBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void InitializeInterface()
	{
	    try
	    {
		ReinitializeComponent();

		InitializeMainField();
		InitializeOptionField();

		Tools.Resize(this, new Size(Width, OptionContainer.Top + OptionContainer.Height + 11));
		Tools.PaintLine(this, MenuBar.Bar.BackColor, 2, new Point(0, Height - 1), new Point(Width, Height - 1));
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public DashlorisX()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeInterface();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }

    public static class Program
    {
	private static void RunDashlorisX() =>
	    Application.Run(new DashlorisX());

	private static void ShowToS() =>
	   new TOS().ShowDialog();

	[STAThread]
	public static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    new Settings().ShowDialog();
	    Environment.Exit(-1);

	    ShowToS();
	    RunDashlorisX();
	}
    }
}
