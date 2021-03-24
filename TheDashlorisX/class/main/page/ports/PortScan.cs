
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
    public class PortScan
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();
	private readonly PictureBox S1Container2 = new PictureBox();
	private readonly PictureBox S1Container3 = new PictureBox();

	private void Init1(DashWindow DashWindow, PictureBox Capsule)
	{
	    try
	    {
		var Container1Size = Capsule.Size;
		var Container1Loca = new Point(0, 0);

		Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Capsule.BackColor);

		var Container2Size = new Size(Capsule.Width, 98);
		var Container2Loca = new Point(0, 0);

		var Container3Size = new Size(Capsule.Width, 135);
		var Container3Loca = new Point(0, 118);

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
	private readonly PictureBox S2Container2 = new PictureBox();
	private readonly PictureBox S2Container3 = new PictureBox();

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

	private readonly Label S2Label1 = new Label();
	private readonly Label S2Label2 = new Label();
	private readonly Label S2Label3 = new Label();
	private readonly Label S2Label4 = new Label();
	private readonly Label S2Label5 = new Label();
	private readonly Label S2Label6 = new Label();

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

	private Size S2TextBoxSize(Size Size, Point Loca, int Height = 20)
	{
	    try
	    {
		int Width = (S2Container1.Width - Loca.X - Size.Width);
		return new Size(Width, Height);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private Point S2ControlX(Size Size, Point Loca, int Y = -1, int Extra = 10)
	{//Use S2ControlY as well. | for class, initialize with controls preferred.
	    try
	    {
		int X = (Size.Width + Loca.X + Extra);

		if (Y == -1)
		    Y = Loca.Y;

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
		var DropMenuLoca = new Point(S1Container2.Left + S2Label6.Left + 10, S1Container2.Top + S2Label6.Top + S2Label6.Height + 10);
		var DropMenuBCol = S2Label6.BackColor;

		S2DropMenu.SetupMenu(S1Container1, DropMenuLoca, DropMenuBCol, DropMenuBCol);

		int ItemWidth = S2Label6.Width - 4;
		int ItemHeight = 18;

		var ItemBCol = S2Container1.BackColor;
		
		S2DropMenu.AddItem(new Label(), ("(T.C.P)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);
		S2DropMenu.AddItem(new Label(), ("(U.D.P)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);

		S2Label6.MouseEnter += (s, e) =>
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

	private void Init2(PictureBox Capsule)
	{
	    try
	    {
		var Container1Size = new Size(S1Container2.Width - 20, S1Container2.Height);
		var Container1Loca = new Point(10, 10);
		var Container1BCol = S1Container2.BackColor;

		Control.Image(S1Container2, S2Container1, Container1Size, Container1Loca, Container1BCol);

		var Label1Loca = new Point(0, 0);
		var Label1Size = GetFontSize("Host:");

		var TextBox1Loca = new Point(Label1Size.Width, 0);
		var TextBox1Size = new Size(165, 20);

		var Label2Loca = S2ControlX(TextBox1Size, TextBox1Loca);
		var Label2Size = GetFontSize("Ports:");

		var TextBox2Loca = S2ControlX(Label2Size, Label2Loca, Extra:0);
		var TextBox2Size = S2TextBoxSize(Label2Size, Label2Loca);

		var Label3Loca = new Point(0, TextBox1Loca.Y + 30);
		var Label3Size = GetFontSize("Timeout:");
		
		var TextBox3Loca = new Point(Label3Size.Width, Label3Loca.Y);
		var TextBox3Size = new Size(125, 20);

		var Label4Loca = S2ControlX(TextBox3Size, TextBox3Loca);
		var Label4Size = GetFontSize("Protocol:");

		var Label5Loca = new Point(0, Label3Loca.Y + 30);
		var Label5Size = GetFontSize("Keep-Alive:");

		var Label6Loca = S2ControlX(Label4Size, Label4Loca, Extra:0);
		var Label6Size = S2TextBoxSize(Label4Size, Label4Loca);

		S2AddTextBox(S2TextBox1, TextBox1Size, TextBox1Loca, ("8.8.8.8"));
		S2AddTextBox(S2TextBox2, TextBox2Size, TextBox2Loca, ("80,443,22,21"));
		S2AddTextBox(S2TextBox3, TextBox3Size, TextBox3Loca, ("350"));

		foreach (Control Control in S2Container1.Controls)
		{
		    if (Control is PictureBox)
		    {
			Tool.Round(Control, 6);
		    }
		}

		S2AddLabel(S2Label1, Label1Size, Label1Loca, ("Host:"));
		S2AddLabel(S2Label2, Label2Size, Label2Loca, ("Ports:"));
		S2AddLabel(S2Label3, Label3Size, Label3Loca, ("Timeout:"));
		S2AddLabel(S2Label4, Label4Size, Label4Loca, ("Protocol:"));
		S2AddLabel(S2Label5, Label5Size, Label5Loca, ("Keep-Alive:"));
		S2AddLabel(S2Label6, Label6Size, Label6Loca, ("-- (TCP) --"), 8);

		S2Label6.TextAlign = ContentAlignment.MiddleCenter;
		S2Label6.BackColor = (S2TextBox1.BackColor);

		S2SetupDropDownMenu();

		var CheckBoxSize = new Size(16, 16);
		var CheckBoxLoca = new Point(Label5Size.Width, Label5Loca.Y + 2);

		Control.CheckBox(S2Container1, S2Container2, S2Container3, CheckBoxSize, CheckBoxLoca, Capsule.BackColor, Color.DarkMagenta, true);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly TextBox S3TextBox1 = new TextBox();

	private readonly Button S3Button1 = new Button();

	private readonly Label S3Label1 = new Label();

	private void Init3()
	{
	    try
	    {
		var Container1Size = new Size(S1Container3.Width - 20, S1Container3.Height - 20);
		var Container1Loca = new Point(10, 10);
		var Container1BCol = S1Container2.BackColor;

		Control.Image(S1Container3, S3Container1, Container1Size, Container1Loca, Container1BCol);

		var LabelSize = GetFontSize(("Scan Log"), 12);
		var LabelLoca = new Point(0, 0);

		Control.Label(S3Container1, S3Label1, LabelSize, LabelLoca, Container1BCol, Color.White, 1, 12, ("Scan Log"));

		var ButtonSize = new Size(95, 20);
		var ButtonLoca = new Point(Container1Size.Width - 95, 0);
		var ButtonBCol = S2TextBox1.BackColor;

		Control.Button(S3Container1, S3Button1, ButtonSize, ButtonLoca, ButtonBCol, Color.White, 1, 8, ("Start Scan"));
		Tool.Round(S3Button1, 6);

		var Container2Size = new Size(Container1Size.Width, Container1Size.Height - 30);
		var Container2Loca = new Point(0, 30);

		Control.Image(S3Container1, S3Container2, Container2Size, Container2Loca, ButtonBCol);

		var TextBoxLoca = new Point(0, 0);

		Control.TextBox(S3Container2, S3TextBox1, Container2Size, TextBoxLoca, ButtonBCol, Color.White, 1, 7, 
		    ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

		Tool.Round(S3Container1, 6);
		Tool.Round(S3Container2, 6);
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

		    Init1(DashWindow, Capsule);
		    Init2(Capsule);
		    Init3();

		    isInitialized = true;
		}

		Show();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
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

	//Move this, obviously:
	public class ControlHelper
	{
	    private readonly DashControls Control = new DashControls();

	    //TextBoxBCol
	    public Control TextBoxParent = new Control();

	    public Color TextBoxBCol = Color.FromArgb(28, 28, 28);
	    public Color TextBoxFCol = Color.White;

	    private void S2AddTextBox(TextBox TextBox, Size Size, Point Loca, string Text, int FontHeight = 8)
	    {
		try
		{
		    TextBox.TextAlign = HorizontalAlignment.Center;
		    TextBox.Text = (Text);

		    Control.TextBox(TextBoxParent, TextBox, Size, Loca, TextBoxBCol, TextBoxFCol, 1, FontHeight);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private Size TextBoxSize(Size Size, Point Loca, int Height = 20)
	    {
		try
		{
		    int Width = (TextBoxParent.Width - Loca.X - Size.Width);
		    return new Size(Width, Height);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public Control LabelParent = new Control();

	    public Color LabelBCol = Color.FromArgb(28, 28, 28);
	    public Color LabelFCol = Color.White;

	    private void S2AddLabel(Label Label, Size Size, Point Loca, string Text, int FontHeight = 10)
	    {
		try
		{
		    Control.Label(LabelParent, Label, Size, Loca, LabelBCol, LabelFCol, 1, FontHeight, Text);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private readonly DashTools Tool = new DashTools();

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

	    private Point ControlX(Size Size, Point Loca, int Y = -1, int Extra = 10)
	    {//Use S2ControlY as well. | for class, initialize with controls preferred.
		try
		{
		    int X = (Size.Width + Loca.X + Extra);

		    if (Y == -1)
			Y = Loca.Y;

		    return new Point(X, Y);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}
    }
}