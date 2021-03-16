
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
    public class Settings
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();//Main Capsule Container
	private readonly PictureBox S1Container2 = new PictureBox();//Main Configuration Container
	private readonly PictureBox S1Container3 = new PictureBox();//Main Proxy Container

	private void InitS1(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		var Container1Loca = new Point(0, 0);
		var Container1BCol = Capsule.BackColor;

		Control.Image(Capsule, S1Container1, Capsule.Size, Container1Loca, Container1BCol);

		var ContainerSize = new Size(Capsule.Width, 95);//102 original calculation + 20 for y:10
		var ContainerBCol = DashWindow.MenuBar.MenuBar.BackColor;

		var Container3Loca = new Point(0, 105);
		var Container2Loca = new Point(0, 0);

		Control.Image(S1Container1, S1Container3, ContainerSize, Container3Loca, ContainerBCol);
		Control.Image(S1Container1, S1Container2, ContainerSize, Container2Loca, ContainerBCol);

		Tool.Round(S1Container1, 6); 
		Tool.Round(S1Container2, 6);
		Tool.Round(S1Container3, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S2Container1 = new PictureBox();//Configuration Container;
	private readonly PictureBox S2Container2 = new PictureBox();
	private readonly PictureBox S2Container3 = new PictureBox();

	private readonly TextBox S2Textbox1 = new TextBox();
	private readonly TextBox S2Textbox2 = new TextBox();
	private readonly TextBox S2Textbox3 = new TextBox();
	private readonly TextBox S2Textbox4 = new TextBox();
	private readonly TextBox S2Textbox5 = new TextBox();

	private void S2AddTextBox(TextBox TextBox, Size Size, Point Loca, string Text, int FontHeight = 8)
	{
	    try
	    {
		var TextBoxBCol = Color.MidnightBlue;//S2Container1.BackColor;
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

	private Size S2GetFontSize(string Text, int FontHeight = 10)
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

	private Point S2ControlLoca(Size Size, Point Loca, int Y = 0, int Extra = 10)
	{
	    try
	    {
		int X = (Size.Width + Loca.X + Extra);
		return new Point(X, Y);
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

		Control.Image(S1Container2, S2Container1, ContainerSize, ContainerLoca, S1Container2.BackColor);

		var Label1Loca = new Point(0, 0);
		var Label1Size = S2GetFontSize("Send Delay:");

		var TextBox1Loca = new Point(Label1Size.Width, 0);
		var TextBox1Size = new Size(75, 20);

		S2AddTextBox(S2Textbox1, TextBox1Size, TextBox1Loca, ("500"));
		S2AddLabel(S2Label1, Label1Size, Label1Loca, ("Send Delay:"));

		var Label2Loca = S2ControlLoca(TextBox1Size, TextBox1Loca);
		var Label2Size = S2GetFontSize("Timeout:");

		var TextBox2Loca = S2ControlLoca(Label2Size, Label2Loca, Extra: 0);
		var TextBox2Size = S2TextBoxSize(Label2Size, Label2Loca);

		S2AddTextBox(S2Textbox2, TextBox2Size, TextBox2Loca, ("500"));
		S2AddLabel(S2Label2, Label2Size, Label2Loca, ("Timeout:"));

		var Label3Loca = new Point(0, Label1Size.Height + Label1Loca.Y + 10);
		var Label3Size = S2GetFontSize("Dash Workers:");

		var TextBox3Loca = S2ControlLoca(Label3Size, Label3Loca, Label3Loca.Y, 0);
		var TextBox3Size = new Size(65, 20);

		S2AddTextBox(S2Textbox3, TextBox3Size, TextBox3Loca, ("8"));
		S2AddLabel(S2Label3, Label3Size, Label3Loca, ("Dash Workers:"));

		var Label4Loca = S2ControlLoca(TextBox3Size, TextBox3Loca, Label3Loca.Y);
		var Label4Size = S2GetFontSize("Max Connections:");

		var TextBox4Loca = S2ControlLoca(Label4Size, Label4Loca, Label3Loca.Y, 0);
		var TextBox4Size = S2TextBoxSize(Label4Size, Label4Loca);

		S2AddTextBox(S2Textbox4, TextBox4Size, TextBox4Loca, ("500"));
		S2AddLabel(S2Label4, Label4Size, Label4Loca, ("Max Connections:"));

		var Label5Loca = new Point(0, Label3Size.Height + Label3Loca.Y + 10);
		var Label5Size = S2GetFontSize("Content-Length:");

		var TextBox5Loca = S2ControlLoca(Label5Size, Label5Loca, Label5Loca.Y, 0);//new Point(0, Label5Loca.Y);
		var TextBox5Size = new Size(70, 20);

		S2AddTextBox(S2Textbox5, TextBox5Size, TextBox5Loca, ("4012"));
		S2AddLabel(S2Label5, Label5Size, Label5Loca, ("Content-Length:"));

		foreach (Control Control in S2Container1.Controls)
		{ 
		    if (Control is PictureBox)
		    {
			Tool.Round(Control, 6);
		    }
		}

		var Label6Loca = S2ControlLoca(TextBox5Size, TextBox5Loca, TextBox5Loca.Y);
		var Label6Size = S2GetFontSize("Random-UA:");

		S2AddLabel(S2Label6, Label6Size, Label6Loca, ("Random-UA:"));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();//Proxy List Container
	private readonly PictureBox S3Container2 = new PictureBox();//Proxy Option Container

	private readonly TextBox S3TextBox1 = new TextBox();
	private readonly TextBox S3TextBox2 = new TextBox();

	private readonly Button S3Selector1 = new Button();
	private readonly Button S3Selector2 = new Button();

	private readonly Label S3Label1 = new Label();
	private readonly Label S3Label2 = new Label();

	private void InitS3()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void Initialize(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		InitS1(Capsule, DashWindow);
		InitS2();
		InitS3();
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