
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

		Tools.Round(TopButton1, 6);
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
		var ContainerSize = new Size(TopContainer1.Width - 10, TopContainer1.Height - (TopLabel1.Height + TopLabel1.Top + 10));
		var ContainerLoca = new Point(5, TopContainer1.Top - 5);
		var ContainerBCol = TopContainer1.BackColor;

		Control.Image(TopContainer1, TopContainer2, ContainerSize, ContainerLoca, ContainerBCol);

		var LabelBCol = ContainerBCol;
		var LabelFCol = Color.White;

		var TextBoxBCol = DashDialog.MenuBar.Bar.BackColor;
		var TextBoxFCol = Color.White;

		var Label1Text = ("");
		var Label1Size = Tool.GetFontSize(Label1Text, 9);
		var Label1Loca = new Point(0, 0);

		var TextBox1Size = new Size(85, 20);
		var TextBox1Loca = new Point(Label1Loca.X + Label1Size.Width, Label1Loca.Y - 2);

		Control.Label();
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

		Tools.Round(TopContainer1, 6);
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
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}