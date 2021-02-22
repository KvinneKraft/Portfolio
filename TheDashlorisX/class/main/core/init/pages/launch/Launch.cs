
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

namespace TheDashlorisX
{
    public class Launch
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	private readonly PictureBox TopContainer2 = new PictureBox();
	private readonly Label TopLabel1 = new Label();

	private void TopInit1(Color LabelBCol)
	{
	    try
	    {
		var LabelText = ("Dashloris-X Statistics:");
		var LabelLoca = new Point(5, 5);
		var LabelFCol = Color.White;

		Control.Label(TopContainer1, TopLabel1, Size.Empty, LabelLoca, LabelBCol, LabelFCol, 1, 12, LabelText);

		var ContainerSize = new Size(TopContainer1.Width - 13, TopContainer1.Height - (TopLabel1.Height + TopLabel1.Top + 15));
		var ContainerLoca = new Point(5, TopContainer1.Height - ContainerSize.Height - 5);
		var ContainerBCol = TopContainer1.BackColor;

		Control.Image(TopContainer1, TopContainer2, ContainerSize, ContainerLoca, ContainerBCol);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly Button TopButton1 = new Button();

	private void TopInit2(DashDialog DashDialog)
	{
	    try
	    {
		var ButtonLoca = new Point(TopContainer1.Width - 93, 8);
		var ButtonBCol = DashDialog.MenuBar.Bar.BackColor;
		var ButtonSize = new Size(85, 20);
		var ButtonFCol = Color.White;

		Control.Button(TopContainer1, TopButton1, ButtonSize, ButtonLoca, ButtonBCol, ButtonFCol, 1, 7, "Reset");

		Tool.Round(TopButton1, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public readonly TextBox TopTextBox1 = new TextBox();
	public readonly TextBox TopTextBox2 = new TextBox();
	public readonly TextBox TopTextBox3 = new TextBox();
	public readonly TextBox TopTextBox4 = new TextBox();

	private readonly Label TopLabel2 = new Label();
	private readonly Label TopLabel3 = new Label();
	private readonly Label TopLabel4 = new Label();
	private readonly Label TopLabel5 = new Label();

	private void AddInputBox(Label Label, Size LabelSize, Point LabelLoca, string LabelText, TextBox TextBox, Size TextBoxSize, Point TextBoxLoca, string TextBoxText, DashDialog DashDialog)
	{
	    try
	    {
		var TextBoxBCol = DashDialog.MenuBar.Bar.BackColor;
		var TextBoxFCol = Color.White;

		Control.TextBox(TopContainer2, TextBox, TextBoxSize, TextBoxLoca, TextBoxBCol, TextBoxFCol, 1, 8, ReadOnly: true);

		TextBox.TextAlign = HorizontalAlignment.Center;
		TextBox.Text = TextBoxText;

		Tool.Round(TextBox.Parent, 6);

		var LabelBCol = TopContainer2.BackColor;
		var LabelFCol = Color.White;

		Control.Label(TopContainer2, Label, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, 9, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private Size GetFontSize(string text, int height = 9) =>
	    Tool.GetFontSize(text, height);

	private void TopInit3(DashDialog DashDialog)
	{
	    try
	    {
		var Label1Size = GetFontSize(("Headers Sent:"));
		var Label2Size = GetFontSize(("Connections:"));
		var Label3Size = GetFontSize(("Bytes Sent:"));
		var Label4Size = GetFontSize(("Time Left:"));

		var TextBox1Size = new Size(65, 20);
		var TextBox3Size = new Size(65, 20);

		var TextBox2Size = new Size(TopContainer2.Width - (TextBox1Size.Width + 8 + Label1Size.Width) - Label2Size.Width, 20);
		var TextBox4Size = new Size(TopContainer2.Width - (TextBox3Size.Width + 8 + Label3Size.Width) - Label4Size.Width, 20);

		var Label1Loca = new Point(0, 0);
		var TextBox1Loca = new Point(Label1Size.Width, Label1Loca.Y - 2);

		var Label2Loca = new Point(TextBox1Size.Width + 8 + TextBox1Loca.X, 0);
		var TextBox2Loca = new Point(Label2Loca.X + Label2Size.Width, Label2Loca.Y - 2);
		
		var Label3Loca = new Point(0, Label2Size.Height + Label2Loca.Y + 8);
		var TextBox3Loca = new Point(Label3Size.Width, Label3Loca.Y - 2);

		var Label4Loca = new Point(TextBox3Size.Width + 8 + TextBox3Loca.X, Label3Loca.Y);
		var TextBox4Loca = new Point(Label4Loca.X + Label4Size.Width, Label4Loca.Y - 2);

		AddInputBox(TopLabel2, Label1Size, Label1Loca, ("Headers Sent:"), TopTextBox1, TextBox1Size, TextBox1Loca, ("0"), DashDialog);
		AddInputBox(TopLabel3, Label2Size, Label2Loca, ("Connections:"), TopTextBox2, TextBox2Size, TextBox2Loca, ("0"), DashDialog);
		AddInputBox(TopLabel4, Label3Size, Label3Loca, ("Bytes Sent:"), TopTextBox3, TextBox3Size, TextBox3Loca, ("0"), DashDialog);
		AddInputBox(TopLabel5, Label4Size, Label4Loca, ("Time Left:"), TopTextBox4, TextBox4Size, TextBox4Loca, ("0"), DashDialog);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox TopContainer1 = new PictureBox();

	private void InitContainer1(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width - 20, 90);
		var ContainerLoca = new Point(10, 10);
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		Control.Image(Capsule, TopContainer1, ContainerSize, ContainerLoca, ContainerBCol);

		TopInit1(ContainerBCol);
		TopInit2(DashDialog);
		TopInit3(DashDialog);

		Tool.Round(TopContainer1, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox BottomContainer3 = new PictureBox();
	private readonly PictureBox BottomContainer2 = new PictureBox();

	private readonly TextBox BottomTextBox1 = new TextBox();
	private readonly Button BottomButton1 = new Button();
	private readonly Label BottomLabel1 = new Label();

	private void BottomInit1(DashDialog DashDialog)
	{
	    try
	    {
		var LabelSize = Tool.GetFontSize("Dash Log:", 12);
		var LabelBCol = TopContainer1.BackColor;
		var LabelLoca = new Point(8, 8);

		Control.Label(BottomContainer1, BottomLabel1, LabelSize, LabelLoca, LabelBCol, Color.White, 1, 12, "Dash Log:");

		var Container1Size = new Size(BottomContainer1.Width - 14, BottomContainer1.Height - 49);
		var Container1BCol = Color.FromArgb(3, 36, 71);
		var Container1Loca = new Point(7, 42);

		Control.Image(BottomContainer1, BottomContainer2, Container1Size, Container1Loca, Container1BCol);
		Tool.Round(BottomContainer2, 6);

		var ButtonSize = new Size(85, 20);
		var ButtonLoca = new Point(Container1Size.Width - 78, 8);
		var ButtonBCol = DashDialog.MenuBar.Bar.BackColor;
		
		Control.Button(BottomContainer1, BottomButton1, ButtonSize, ButtonLoca, ButtonBCol, Color.White, 1, 7, "Reset");
		Tool.Round(BottomButton1, 6);
		
		var Container2Size = new Size(Container1Size.Width - 10, Container1Size.Height - 10);
		var Container2Loca = new Point(5, 5);

		Control.Image(BottomContainer2, BottomContainer3, Container2Size, Container2Loca, Container1BCol);
		Tool.Round(BottomContainer3, 6);

		var TextBoxLoca = new Point(0, 0);

		Control.TextBox(BottomContainer3, BottomTextBox1, Container2Size, TextBoxLoca, Container1BCol, Color.White, 1, 8, ReadOnly: true, FixedSize: false, ScrollBar: true, Multiline: true);

		BottomTextBox1.Text = string.Format
		(
		    "(?) use me with caution, you can crash your own network because of misuse."
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox BottomContainer1 = new PictureBox();

	private void InitContainer2(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width - 20, Capsule.Height - TopContainer1.Height - 32);
		var ContainerLoca = new Point(10, TopContainer1.Height + TopContainer1.Top + 10);
		var ContainerBCol = TopContainer1.BackColor;

		Control.Image(Capsule, BottomContainer1, ContainerSize, ContainerLoca, ContainerBCol);

		BottomInit1(DashDialog);

		Tool.Round(BottomContainer1, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public void Initialize(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		InitContainer1(DashDialog, Capsule);
		InitContainer2(DashDialog, Capsule);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}