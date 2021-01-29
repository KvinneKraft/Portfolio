// Author: Dashie
// Version: 1.0
//
// You know it is Dash Code if it is Dash Code man, whatchu on about?
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;
using System.Collections.Generic;

using DashlorisX.Properties;

namespace DashlorisX
{
    public partial class DashlorisX : Form
    {
	private readonly DashControls Control = new DashControls();
	private readonly static DashTools Tool = new DashTools();

	public static class Program
	{
	    private static void DashlorisX()
	    {
		new DashlorisX().ShowDialog();
	    }

	    private static void ShowToS()
	    {
		new TOS().Show();
	    }
	    
	    [STAThread] public static void Main()
	    {
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);



		ShowToS();
		DashlorisX();
		
		Application.Exit();
	    }
	}

	private static readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X", hide: false);

	private void InitializeMenuBar()
	{
	    try
	    {
		Color MenuBarBColor = Color.FromArgb(19, 36, 64);
		MenuBar.Add(this, 26, MenuBarBColor, MenuBarBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox InnerMainContainer = new PictureBox();
	private readonly PictureBox MainContainer = new PictureBox();

	public readonly static TextBox DurationTextBox = new TextBox() { Text = "4500" };
	public readonly static TextBox BytesTextBox = new TextBox() { Text = "1024" };
	public readonly static TextBox HostTextBox = new TextBox() { Text = "https://pugpawz.com" };
	public readonly static TextBox PortTextBox = new TextBox() { Text = "80" };

	private readonly List<TextBox> TextBoxObjects = new List<TextBox>()
	{
	    HostTextBox, BytesTextBox,
	    PortTextBox, DurationTextBox
	};

	private readonly static Label DurationLabel = new Label();
	private readonly static Label BytesLabel = new Label();
	private readonly static Label HostLabel = new Label();
	private readonly static Label PortLabel = new Label();

	private readonly List<Label> LabelObjects = new List<Label>()
	{
	    HostLabel, BytesLabel,
	    PortLabel, DurationLabel
	};

	public static bool BlockDomains = true;

	private void SetupKeyEvents()
	{
	    try
	    {
		HostTextBox.KeyDown += (s, e) =>
		{
		    if (e.KeyCode == Keys.F4)
		    {
			if (BlockDomains)
			{
			    MessageBox.Show("You have turned the special setting on, this means you are now able to target blacklisted domains such as *.gov and *.edu.  Not recommended, but yea, freedom!", "Special Setting", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			else
			{
			    MessageBox.Show("You have turned the special setting off, this means you are now no longer able to target blacklisted domains.  I recommend this.", "Special Setting", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			BlockDomains = !BlockDomains;
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void InitializeMainField()
	{
	    var MContainerSize = new Size(Width - 22, 64);
	    var MContainerLocation = new Point(11, MenuBar.Bar.Height + 10);
	    var MContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Control.Image(this, MainContainer, MContainerSize, MContainerLocation, MContainerBColor);
		Tool.Round(MainContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var IContainerSize = new Size(MContainerSize.Width - 22, MContainerSize.Height - 21/*Measure it*/);
	    var IContainerLocation = new Point(11, 11);
	    var IContainerBColor = MContainerBColor;

	    try
	    {
		Control.Image(MainContainer, InnerMainContainer, IContainerSize, IContainerLocation, IContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
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
			var LSize = Tool.GetFontSize(LText, 11);

			if (k2 > 0) x1 += (w3 + x3 + 10);

			var LLoca = new Point(x1, y1);

			Control.Label(InnerMainContainer, LabelObjects[tid], LSize, LLoca, InnerMainContainer.BackColor, Color.White, 1, 11, LText);

			int w2 = TextBoxWidths[k1];

			if (k2 > 0) w2 = (InnerMainContainer.Width - LLoca.X - LSize.Width);

			var TLoca = new Point(LLoca.X + LSize.Width, y1);
			var TSize = new Size(w2, h1);

			x3 = TLoca.X;
			w3 = w2;

			Control.TextBox(InnerMainContainer, TextBoxObjects[tid], TSize, TLoca, Color.FromArgb(10, 10, 10), Color.White, 1, 9);
			Tool.Round(InnerMainContainer.Controls[InnerMainContainer.Controls.Count - 1], 6);

			tid += 1;
		    }
		}

		foreach (Control c1 in InnerMainContainer.Controls)
		{
		    if (c1.Controls.Count > 0 && c1 is PictureBox)
		    {
			foreach (TextBox c2 in c1.Controls)
			{
			    c2.TextAlign = HorizontalAlignment.Center;
			}
		    }
		}

		SetupKeyEvents();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox InnerOptionContainer = new PictureBox();
	private readonly PictureBox OptionContainer = new PictureBox();

	public readonly static Button Launch = new Button();

	private readonly static Button Settings = new Button();
	private readonly static Button Online = new Button();
	private readonly static Button About = new Button();

	public readonly static List<Button> ButtonObjects = new List<Button>()
	{
	    Launch, Settings,
	    Online, About
	};

	private readonly DashlorisXInfo DashlorisXInfoDialog = new DashlorisXInfo();
	private readonly Confirmation ConfirmationDialog = new Confirmation();
	private readonly Settings SettingsDialog = new Settings();
	private readonly IsOnline IsOnlineDialog = new IsOnline();
	private readonly DashNet DashNet = new DashNet();

	private void SetupClickEvents()
	{
	    try
	    {
		ButtonObjects[0].Click += (s, e) =>
		{
		    var ButtonObject = ButtonObjects[0];

		    if (ButtonObject.Text == "Launch")
		    {
			if (!ConfirmationDialog.DashDialog.Visible)
			{
			    ConfirmationDialog.Show();
			}
		    }

		    else
		    {
			Confirmation.PowPow.StopAttack();
		    }
		};

		ButtonObjects[1].Click += (s, e) =>
		{
		    if (!SettingsDialog.DashDialog.Visible)
		    {
			SettingsDialog.Show();
		    }
		};

		ButtonObjects[2].Click += (s, e) =>
		{
		    if (!IsOnlineDialog.DashDialog.Visible)
		    {
			IsOnlineDialog.Show();
		    }
		};

		ButtonObjects[3].Click += (s, e) =>
		{
		    if (DashlorisXInfoDialog.InfoContainer == null || !DashlorisXInfoDialog.InfoContainer.Visible)
		    {
			DashlorisXInfoDialog.Show();
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void InitializeOptionField()
	{
	    var OContainerSize = new Size(Width - 20, 81);
	    var OContainerLocation = new Point(10, MainContainer.Top + MainContainer.Height + 10);
	    var OContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Control.Image(this, OptionContainer, OContainerSize, OContainerLocation, OContainerBColor);
		Tool.Round(OptionContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var IOptionContainerSize = new Size(OContainerSize.Width - 28, OContainerSize.Height - 28);
	    var IOptionContainerLocation = new Point(14, 14);
	    var IOptionContainerBColor = OContainerBColor;

	    try
	    {
		Control.Image(OptionContainer, InnerOptionContainer, IOptionContainerSize, IOptionContainerLocation, IOptionContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
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

			Control.Button(InnerOptionContainer, ButtonObjects[p], ButtonSize, BUTTO_LOCA, ButtonBColor, ButtonFColor, 1, 10, ButtonTexts[p]);
			Tool.Round(ButtonObjects[p], 6);

			x = ButtonSize.Width + ButtonObjects[p].Left + 8;
		    }

		    y += ButtonSize.Height + ButtonObjects[p - 1].Top + 8;
		}

		SetupClickEvents();

		Tool.Resize(InnerOptionContainer, new Size(InnerOptionContainer.Width, ButtonObjects[ButtonObjects.Count - 1].Top + ButtonObjects[ButtonObjects.Count - 1].Height));
		Tool.Resize(OptionContainer, new Size(OptionContainer.Width, ButtonObjects[ButtonObjects.Count - 1].Top + ButtonObjects[ButtonObjects.Count - 1].Height + 28));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void InitializeInterface()
	{
	    try
	    {
		InitializeMainField();
		InitializeOptionField();

		Tool.Resize(this, new Size(Width, OptionContainer.Top + OptionContainer.Height + 11));
		Tool.PaintLine(this, MenuBar.Bar.BackColor, 2, new Point(0, Height - 1), new Point(Width, Height - 1));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
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
}
