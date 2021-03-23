
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class LockOn
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();
	private readonly PictureBox S1Container2 = new PictureBox();
	private readonly PictureBox S1Container3 = new PictureBox();

	private void InitS1(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		var Container1Size = new Size(Capsule.Width, 180);
		var Container1Loca = new Point(0, -2);

		Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Capsule.BackColor);

		var Container2Size = new Size(Container1Size.Width, 98);
		var Container2Loca = new Point(0, 0);

		var Container3Size = new Size(Container1Size.Width, 70);
		var Container3Loca = new Point(0, 110);

		var ContainerBCol = DashWindow.MenuBar.MenuBar.BackColor;

		Control.Image(S1Container1, S1Container2, Container2Size, Container2Loca, ContainerBCol);
		Control.Image(S1Container1, S1Container3, Container3Size, Container3Loca, ContainerBCol);

		Tool.Round(S1Container2, 6);
		Tool.Round(S1Container3, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S2Container1 = new PictureBox();

	private readonly TextBox S2TextBox1 = new TextBox();
	private readonly TextBox S2TextBox2 = new TextBox();
	private readonly TextBox S2TextBox3 = new TextBox();

	private void S2AddTextBox(TextBox TextBox, Size Size, Point Loca, string Text, int FontHeight = 8)
	{
	    try
	    {
		var TextBoxBCol = S1Container1.Parent.BackColor;
		var TextBoxFCol = Color.White;

		TextBox.TextAlign = HorizontalAlignment.Center;
		TextBox.Text = (Text);

		Control.TextBox(S2Container1, TextBox, Size, Loca, TextBoxBCol, TextBoxFCol, 1, FontHeight);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly Button S2Button1 = new Button();

	private readonly Label S2Label1 = new Label();
	private readonly Label S2Label2 = new Label();
	private readonly Label S2Label3 = new Label();
	private readonly Label S2Label4 = new Label();
	private readonly Label S2Label5 = new Label();

	private void S2AddLabel(Label Label, Size Size, Point Loca, string Text, int FontHeight = 10)
	{
	    try
	    {
		var LabelBCol = S2Container1.BackColor;
		var LabelFCol = Color.White;

		Control.Label(S2Container1, Label, Size, Loca, LabelBCol, LabelFCol, 1, FontHeight, Text);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private Size S2GetLeftOver(Size Size, Point Loca, int Height = 20)
	{
	    try
	    {
		int Width = (S2Container1.Width - (Size.Width + Loca.X));
		return new Size(Width, Height);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private Size GetFontSize(string Text, int FontHeight = 10)
	{
	    try
	    {
		Font Font = Tool.GetFont(1, FontHeight);
		return TextRenderer.MeasureText(Text, Font);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private Point S2TextBoxLoca(Size Size, Point Loca, int Y = 0, int Extra = 0)
	{
	    try
	    {
		int X = (Loca.X + Size.Width) + Extra;
		return new Point(X, Y);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly DropMenu S2DropMenu = new DropMenu();

	private void S2SetupDropDownMenu()
	{
	    try
	    {
		var DropMenuLoca = new Point(S1Container2.Left + S2Label5.Left + 10, S1Container2.Top + S1Container2.Height - 10);
		var DropMenuBCol = S2Label5.BackColor;

		S2DropMenu.SetupMenu(S1Container1, DropMenuLoca, DropMenuBCol, DropMenuBCol);

		int ItemWidth = S2Label5.Width - 4;
		int ItemHeight = 18;

		var ItemBCol = S2Container1.BackColor;

		S2DropMenu.AddItem(new Label(), ("(1991 - 0.9)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);
		S2DropMenu.AddItem(new Label(), ("(1996 - 1.0)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);
		S2DropMenu.AddItem(new Label(), ("(1997 - 1.1)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);
		S2DropMenu.AddItem(new Label(), ("(2015 - 2.0)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);
		S2DropMenu.AddItem(new Label(), ("(2020 - 3.0)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);

		S2Label5.MouseEnter += (s, e) =>
		{
		    try
		    {
			S2DropMenu.Show();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void InitS2()
	{
	    try
	    {
		var ContainerSize = new Size(S1Container2.Width - 20, S1Container2.Height - 20);
		var ContainerLoca = new Point(10, 10);
		var ContainerBCol = S1Container2.BackColor;

		Control.Image(S1Container2, S2Container1, ContainerSize, ContainerLoca, ContainerBCol);

		var Label1Size = GetFontSize(("Host:"));
		var Label1Loca = new Point(0, 0);

		var TextBox1Size = new Size(165, 20);
		var TextBox1Loca = S2TextBoxLoca(Label1Size, Label1Loca);

		var Label2Size = GetFontSize("Port:");
		var Label2Loca = S2TextBoxLoca(TextBox1Size, TextBox1Loca, Extra: 10);

		var TextBox2Size = S2GetLeftOver(Label2Size, Label2Loca);
		var TextBox2Loca = S2TextBoxLoca(Label2Size, Label2Loca);

		int GetBellow(Size Size, Point Loca)
		{
		    try
		    {
			return (Size.Height + Loca.Y + 10);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		var Label3Size = GetFontSize("Duration:");
		var Label3Loca = new Point(0, GetBellow(TextBox1Size, TextBox1Loca));

		var TextBox3Size = new Size(135, 20);
		var TextBox3Loca = S2TextBoxLoca(Label3Size, Label3Loca, Label3Loca.Y);

		var Label4Size = GetFontSize("HTTP Version:");
		var Label4Loca = S2TextBoxLoca(TextBox3Size, TextBox3Loca, Label3Loca.Y, 10);

		var Label5Size = new Size(Label4Size.Width, 20);
		var Label5Loca = new Point(Label4Loca.X, Label4Loca.Y + Label4Size.Height + 10);

		S2AddTextBox(S2TextBox1, TextBox1Size, TextBox1Loca, ("https://pugpawz.com"));
		S2AddTextBox(S2TextBox2, TextBox2Size, TextBox2Loca, ("65535"));
		S2AddTextBox(S2TextBox3, TextBox3Size, TextBox3Loca, ("45000"));

		S2AddLabel(S2Label1, Label1Size, Label1Loca, ("Host:"));
		S2AddLabel(S2Label2, Label2Size, Label2Loca, ("Port:"));
		S2AddLabel(S2Label3, Label3Size, Label3Loca, ("Duration:"));
		S2AddLabel(S2Label4, Label4Size, Label4Loca, ("HTTP Version:"));
		S2AddLabel(S2Label5, Label5Size, Label5Loca, ("--- (1.0) ---"), 8);

		S2Label5.TextAlign = ContentAlignment.MiddleCenter;
		S2Label5.BackColor = (S2TextBox1.BackColor);

		var ButtonSize = new Size(110, 20);
		var ButtonLoca = new Point(0, Label3Loca.Y + Label3Size.Height + 10);
		var ButtonBCol = S2TextBox1.BackColor;

		Control.Button(S2Container1, S2Button1, ButtonSize, ButtonLoca, ButtonBCol, Color.White, 1, 8, ("Is It There?"));

		foreach (Control Control in S2Container1.Controls)
		{
		    if (Control is PictureBox || Control is Button)
		    {
			Tool.Round(Control, 6);
		    }
		}

		S2SetupDropDownMenu();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();

	private readonly Button S3Button1 = new Button();
	private readonly Button S3Button2 = new Button();
	private readonly Button S3Button3 = new Button();
	private readonly Button S3Button4 = new Button();

	private void HideCapsulePages(PictureBox Capsule)
	{
	    try
	    {
		foreach (Control Control in Capsule.Controls)
		{
		    if (Control.Visible)
		    {
			Control.Hide();
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public readonly Settings S3Settings = new Settings();

	private void InitS3(PictureBox Capsule, DashWindow DashWindow, InitThaDashlorisX Parent)
	{
	    try
	    {
		var ContainerSize = new Size(S1Container3.Width - 20, S1Container3.Height - 20);
		var ContainerLoca = new Point(10, 10);
		var ContainerBCol = S1Container3.BackColor;

		Control.Image(S1Container3, S3Container1, ContainerSize, ContainerLoca, ContainerBCol);

		Size CalculateSize()
		{
		    try
		    {
			int Width = (ContainerSize.Width / 2) - 5;
			return (new Size(Width, 20));
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}
		
		var ButtonSize = CalculateSize();

		var Button4Loca = new Point(ButtonSize.Width + 10, ButtonSize.Height + 10);
		var Button3Loca = new Point(0, ButtonSize.Height + 10);
		var Button2Loca = new Point(ButtonSize.Width + 10, 0);
		var Button1Loca = new Point(0, 0);

		var ButtonBCol = S2TextBox1.BackColor;

		Control.Button(S3Container1, S3Button1, ButtonSize, Button1Loca, ButtonBCol, Color.White, 1, 8, ("Fancy Settings Man"));
		Control.Button(S3Container1, S3Button2, ButtonSize, Button2Loca, ButtonBCol, Color.White, 1, 8, ("Whoosh IP"));
		Control.Button(S3Container1, S3Button3, ButtonSize, Button3Loca, ButtonBCol, Color.White, 1, 8, ("Geo IP"));
		Control.Button(S3Container1, S3Button4, ButtonSize, Button4Loca, ButtonBCol, Color.White, 1, 8, ("I am Dumb!"));

		foreach (Button Button in S3Container1.Controls)
		{
		    Tool.Round(Button, 6);
		}

		S3Button1.Click += (s, e) =>
		{
		    try
		    {
			HideCapsulePages(Capsule);
			S3Settings.Initialize(Capsule, DashWindow, Parent);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private bool isInitialized = false;

	public void InitializePage(DashWindow DashWindow, PictureBox Capsule, InitThaDashlorisX Parent)
	{
	    try
	    {
		if (!isInitialized)
		{
		    InitS1(Capsule, DashWindow);
		    InitS2();
		    InitS3(Capsule, DashWindow, Parent);

		    isInitialized = (true);
		}

		Show();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Show()
	{
	    try
	    {
		S1Container1.Show();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Hide()
	{
	    try
	    {
		S1Container1.Hide();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}