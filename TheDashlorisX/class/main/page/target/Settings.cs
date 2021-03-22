
// Author: Dashie
// Version: 3.0
// Coded while high....

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


	private readonly PictureBox S1Container1 = new PictureBox();
	private readonly PictureBox S1Container2 = new PictureBox();
	private readonly PictureBox S1Container3 = new PictureBox();

	private void InitS1(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		var Container1Size = new Size(Capsule.Width, 201);
		var Container1Loca = new Point(-2, -2);
		var Container1BCol = Capsule.BackColor;

		Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Container1BCol);

		var Container2Size = new Size(Capsule.Width, 95);
		var Container3Size = new Size(Capsule.Width, 96);

		var ContainerBCol = DashWindow.MenuBar.MenuBar.BackColor;

		var Container3Loca = new Point(0, 105);
		var Container2Loca = new Point(0, 0);

		Control.Image(S1Container1, S1Container3, Container3Size, Container3Loca, ContainerBCol);
		Control.Image(S1Container1, S1Container2, Container2Size, Container2Loca, ContainerBCol);
 
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

	private readonly TextBox S2Textbox1 = new TextBox();
	private readonly TextBox S2Textbox2 = new TextBox();
	private readonly TextBox S2Textbox3 = new TextBox();
	private readonly TextBox S2Textbox4 = new TextBox();
	private readonly TextBox S2Textbox5 = new TextBox();

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

	private void InitS2(PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(S1Container2.Width - 20, S1Container2.Height - 20);
		var ContainerLoca = new Point(10, 10);

		Control.Image(S1Container2, S2Container1, ContainerSize, ContainerLoca, S1Container2.BackColor);

		var Label1Loca = new Point(0, 0);
		var Label1Size = GetFontSize("Send Delay:");

		var TextBox1Loca = new Point(Label1Size.Width, 0);
		var TextBox1Size = new Size(70, 20);

		var Label2Loca = S2ControlLoca(TextBox1Size, TextBox1Loca);
		var Label2Size = GetFontSize("Timeout:");

		var TextBox2Loca = S2ControlLoca(Label2Size, Label2Loca, Extra: 0);
		var TextBox2Size = S2TextBoxSize(Label2Size, Label2Loca);

		var Label3Loca = new Point(0, Label1Size.Height + Label1Loca.Y + 10);
		var Label3Size = GetFontSize("Dash Workers:");

		var TextBox3Loca = S2ControlLoca(Label3Size, Label3Loca, Label3Loca.Y, 0);
		var TextBox3Size = new Size(55, 20);

		var Label4Loca = S2ControlLoca(TextBox3Size, TextBox3Loca, Label3Loca.Y);
		var Label4Size = GetFontSize("Max Connections:");

		var TextBox4Loca = S2ControlLoca(Label4Size, Label4Loca, Label3Loca.Y, 0);
		var TextBox4Size = S2TextBoxSize(Label4Size, Label4Loca);

		var Label5Loca = new Point(0, Label3Size.Height + Label3Loca.Y + 10);
		var Label5Size = GetFontSize("Content-Length:");

		var TextBox5Loca = S2ControlLoca(Label5Size, Label5Loca, Label5Loca.Y, 0);
		var TextBox5Size = new Size(70, 20);

		S2AddTextBox(S2Textbox1, TextBox1Size, TextBox1Loca, ("500"));
		S2AddTextBox(S2Textbox2, TextBox2Size, TextBox2Loca, ("500"));
		S2AddTextBox(S2Textbox3, TextBox3Size, TextBox3Loca, ("8"));
		S2AddTextBox(S2Textbox4, TextBox4Size, TextBox4Loca, ("500"));
		S2AddTextBox(S2Textbox5, TextBox5Size, TextBox5Loca, ("4012"));

		S2AddLabel(S2Label1, Label1Size, Label1Loca, ("Send Delay:"));
		S2AddLabel(S2Label2, Label2Size, Label2Loca, ("Timeout:"));
		S2AddLabel(S2Label3, Label3Size, Label3Loca, ("Dash Workers:"));
		S2AddLabel(S2Label4, Label4Size, Label4Loca, ("Max Connections:"));
		S2AddLabel(S2Label5, Label5Size, Label5Loca, ("Content-Length:"));

		foreach (Control Control in S2Container1.Controls)
		{ 
		    if (Control is PictureBox)
		    {
			Tool.Round(Control, 6);
		    }
		}

		var Label6Loca = S2ControlLoca(TextBox5Size, TextBox5Loca, TextBox5Loca.Y);
		var Label6Size = GetFontSize("Random-UA:");

		S2AddLabel(S2Label6, Label6Size, Label6Loca, ("Random-UA:"));

		var CheckBoxSize = new Size(16, 16);
		var CheckBoxLoca = S2ControlLoca(Label6Size, Label6Loca, Label6Loca.Y + 2, 0);
		var CheckBoxCol2 = Capsule.BackColor;

		Control.CheckBox(S2Container1, S2Container2, S2Container3, CheckBoxSize, CheckBoxLoca, CheckBoxCol2, Color.DarkMagenta, true);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly TextBox S3TextBox1 = new TextBox();
	private readonly TextBox S3TextBox2 = new TextBox();

	private readonly List<PictureBox> S3ContainerCollection = new List<PictureBox>();

	private readonly Button S3Selector1 = new Button();
	private readonly Button S3Selector2 = new Button();

	private void S3AddTextBox(TextBox TextBox, Button Button, Size Size, Point Loca, string Text, int FontHeight = 8)
	{
	    try
	    {
		PictureBox Container = new PictureBox();

		var ContainerSize = new Size(Size.Width + 20, Size.Height);
		var BCol = S1Container1.Parent.BackColor;

		Control.Image(S3Container1, Container, ContainerSize, Loca, Color.White);

		TextBox.TextAlign = HorizontalAlignment.Center;
		TextBox.Text = (Text);

		Control.TextBox(Container, TextBox, Size, new Point(), BCol, Color.White, 1, FontHeight);

		var ButtonLoca = new Point(ContainerSize.Width - 20, 0);
		var ButtonSize = new Size(20, 20);

		Control.Button(Container, Button, ButtonSize, ButtonLoca, Color.DarkMagenta, Color.White, 1, FontHeight, ("^"));

		S3ContainerCollection.Add(Container);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly Label S3Label1 = new Label();
	private readonly Label S3Label2 = new Label();

	private void S3AddLabel(Label Label, Size Size, Point Loca, string Text, int FontHeight = 10)
	{
	    try
	    {
		var LabelBCol = S3Container1.BackColor;
		var LabelFCol = Color.White;

		Control.Label(S3Container1, Label, Size, Loca, LabelBCol, LabelFCol, 1, FontHeight, Text);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private Size S3TextBoxSize(Size Size, Point Loca, int Height = 20)
	{
	    try
	    {
		int Width = (S3Container1.Width - (Size.Width + Loca.X) - 20);
		return new Size(Width, Height);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly Button S3Button1 = new Button();
	private readonly Button S3Button2 = new Button();
	private readonly Button S3Button3 = new Button();

	private void InitS3()
	{
	    try
	    {
		var Container1Size = new Size(S1Container3.Width - 20, S1Container3.Height - 20);
		var Container1Loca = new Point(10, 10);

		Control.Image(S1Container3, S3Container1, Container1Size, Container1Loca, S1Container3.BackColor);

		var Label1Size = GetFontSize("Proxy List:");
		var Label1Loca = new Point(0, 0);

		var TextBox1Loca = S2ControlLoca(Label1Size, Label1Loca, 0, 0);
		var TextBox1Size = S3TextBoxSize(Label1Size, Label1Loca);

		var Label2Size = GetFontSize("Credential List:");
		var Label2Loca = new Point(0, Label1Size.Height + 10);

		var TextBox2Loca = S2ControlLoca(Label2Size, Label2Loca, Label2Loca.Y, 0);
		var TextBox2Size = S3TextBoxSize(Label2Size, Label2Loca);

		S3AddTextBox(S3TextBox1, S3Selector1, TextBox1Size, TextBox1Loca, ("<Select Text File>"));
		S3AddLabel(S3Label1, Label1Size, Label1Loca, ("Proxy List:"));

		S3AddTextBox(S3TextBox2, S3Selector2, TextBox2Size, TextBox2Loca, ("<Select Text File>"));
		S3AddLabel(S3Label2, Label2Size, Label2Loca, ("Credential List:"));

		var Container2Size = new Size(335, 20);
		var Container2Loca = new Point(-2, Label2Loca.Y + Label2Size.Height + 10);

		Control.Image(S3Container1, S3Container2, Container2Size, Container2Loca, S3Container1.BackColor);

		var ButtonSize = new Size(105, 20);
		var ButtonBCol = S3TextBox1.BackColor;

		var Button1Loca = new Point(0, 0);
		var Button2Loca = new Point(115, 0);
		var Button3Loca = new Point(230, 0);

		Control.Button(S3Container2, S3Button1, ButtonSize, Button1Loca, ButtonBCol, Color.White, 1, 8, "Test Servers");
		Control.Button(S3Container2, S3Button2, ButtonSize, Button2Loca, ButtonBCol, Color.White, 1, 8, "Modify Header");
		Control.Button(S3Container2, S3Button3, ButtonSize, Button3Loca, ButtonBCol, Color.White, 1, 8, "Go Back");
		
		foreach (Control Control in S3Container2.Controls)
		{
		    if (Control is Button)
		    {
			Tool.Round(Control, 6);
		    }
		}

		S3Button3.Click += (s, e) =>
		{
		    InitThaDashlorisX.S3Class1.Show();
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private bool isInitialized = false;

	public void Initialize(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		if (!isInitialized)
		{
		    InitS1(Capsule, DashWindow);
		    InitS2(Capsule);
		    InitS3();
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