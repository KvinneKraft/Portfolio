
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


	private readonly Label TopLabel1 = new Label();

	private void TopInit1(Color LabelBCol)
	{
	    try
	    {
		var LabelText = ("Statistics:");
		var LabelLoca = new Point(8, 8);
		var LabelFCol = Color.White;

		Control.Label(TopContainer1, TopLabel1, Size.Empty, LabelLoca, LabelBCol, LabelFCol, 1, 12, LabelText);

		var ContainerSize = new Size(TopContainer1.Width - 13, TopContainer1.Height - (TopLabel1.Height + TopLabel1.Top + 15));
		var ContainerLoca = new Point(5, TopContainer1.Height - ContainerSize.Height - 5);
		var ContainerBCol = TopContainer1.BackColor;

		Control.Image(TopContainer1, TopContainer2, ContainerSize, ContainerLoca, ContainerBCol);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}



	private readonly Button TopButton1 = new Button();

	private void TopInit2(DashDialog DashDialog)
	{
	    try
	    {
		var ButtonSize = new Size(85, 20);
		var ButtonLoca = new Point(TopContainer1.Width - 93, 8);
		var ButtonBCol = DashDialog.MenuBar.Bar.BackColor;
		var ButtonFCol = Color.White;

		Control.Button(TopContainer1, TopButton1, ButtonSize, ButtonLoca, ButtonBCol, ButtonFCol, 1, 7, "Reset");

		Tool.Round(TopButton1, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}



	private readonly PictureBox TopContainer2 = new PictureBox();

	public readonly TextBox TopTextBox1 = new TextBox();
	public readonly TextBox TopTextBox2 = new TextBox();
	public readonly TextBox TopTextBox3 = new TextBox();
	public readonly TextBox TopTextBox4 = new TextBox();

	private readonly Label TopLabel2 = new Label();
	private readonly Label TopLabel3 = new Label();
	private readonly Label TopLabel4 = new Label();
	private readonly Label TopLabel5 = new Label();

	private void TopInit3(DashDialog DashDialog)
	{
	    try
	    {
		var TextBoxBCol = DashDialog.MenuBar.Bar.BackColor;
		var TextBoxFCol = Color.White;

		var LabelBCol = TopContainer2.BackColor;
		var LabelFCol = Color.White;

		/*Think about a way to make this look better, this is horrific, what the actual fuck!*/

		var Label1Text = ("Headers Sent:");
		var Label1Size = Tool.GetFontSize(Label1Text, 9);
		var Label1Loca = new Point(0, 0);

		var TextBox1Size = new Size(65, 20);
		var TextBox1Loca = new Point(Label1Loca.X + Label1Size.Width, Label1Loca.Y - 2);

		var Label2Text = ("Connections:");
		var Label2Size = Tool.GetFontSize(Label2Text, 9);
		var Label2Loca = new Point(TextBox1Size.Width + TextBox1Loca.X + 8, 0);

		var TextBox2Size = new Size(TopContainer2.Width - Label2Loca.X - Label2Size.Width, 20);
		var TextBox2Loca = new Point(Label2Loca.X + Label2Size.Width, Label2Loca.Y - 2);

		var Label3Text = ("Bytes Sent:");
		var Label3Size = Tool.GetFontSize(Label3Text, 9);
		var Label3Loca = new Point(0, Label2Size.Height + Label2Loca.Y + 8);

		var TextBox3Size = new Size(65, 20);
		var TextBox3Loca = new Point(Label3Loca.X + Label3Size.Width, Label3Loca.Y - 2);

		var Label4Text = ("Time Left:");
		var Label4Size = Tool.GetFontSize(Label4Text, 9);
		var Label4Loca = new Point(TextBox3Size.Width + TextBox3Loca.X + 8, Label3Loca.Y);

		var TextBox4Size = new Size(TopContainer2.Width - Label4Loca.X - Label4Size.Width, 20);
		var TextBox4Loca = new Point(Label4Loca.X + Label4Size.Width, Label4Loca.Y - 2);

		Control.Label(TopContainer2, TopLabel5, Label4Size, Label4Loca, LabelBCol, LabelFCol, 1, 9, Label4Text);
		Control.Label(TopContainer2, TopLabel4, Label3Size, Label3Loca, LabelBCol, LabelFCol, 1, 9, Label3Text);
		Control.Label(TopContainer2, TopLabel3, Label2Size, Label2Loca, LabelBCol, LabelFCol, 1, 9, Label2Text);
		Control.Label(TopContainer2, TopLabel2, Label1Size, Label1Loca, LabelBCol, LabelFCol, 1, 9, Label1Text);

		Control.TextBox(TopContainer2, TopTextBox4, TextBox4Size, TextBox4Loca, TextBoxBCol, TextBoxFCol, 1, 8, ReadOnly: true);
		Control.TextBox(TopContainer2, TopTextBox3, TextBox3Size, TextBox3Loca, TextBoxBCol, TextBoxFCol, 1, 8, ReadOnly: true);
		Control.TextBox(TopContainer2, TopTextBox2, TextBox2Size, TextBox2Loca, TextBoxBCol, TextBoxFCol, 1, 8, ReadOnly: true);
		Control.TextBox(TopContainer2, TopTextBox1, TextBox1Size, TextBox1Loca, TextBoxBCol, TextBoxFCol, 1, 8, ReadOnly: true);

		TopTextBox1.TextAlign = HorizontalAlignment.Center;
		TopTextBox2.TextAlign = HorizontalAlignment.Center;
		TopTextBox3.TextAlign = HorizontalAlignment.Center;
		TopTextBox4.TextAlign = HorizontalAlignment.Center;

		TopTextBox1.Text = ("0");
		TopTextBox2.Text = ("0");
		TopTextBox3.Text = ("0");
		TopTextBox4.Text = ("0");

		Tool.Round(TopTextBox1.Parent, 6);
		Tool.Round(TopTextBox2.Parent, 6);
		Tool.Round(TopTextBox3.Parent, 6);
		Tool.Round(TopTextBox4.Parent, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
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
		throw (E);
	    }
	}



	private readonly PictureBox BottomContainer2 = new PictureBox();

	private readonly TextBox BottomTextBox1 = new TextBox();

	private readonly Button BottomButton1 = new Button();

	private readonly Label BottomLabel1 = new Label();

	private void BottomInit1(DashDialog DashDialog)
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (E);
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