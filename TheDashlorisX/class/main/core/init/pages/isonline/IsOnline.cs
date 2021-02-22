
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
    public class IsOnline
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	private readonly PictureBox Container2 = new PictureBox();
	private readonly PictureBox Container3 = new PictureBox();
	private readonly PictureBox PictureBox = new PictureBox();

	private readonly TextBox TextBox1 = new TextBox();

	private readonly Label Label1 = new Label();
	private readonly Label Label2 = new Label();

	private readonly DropDownMenu DropDownMenu = new DropDownMenu();

	private Size GetFontSize(string Text, int FontHeight = 9) =>
	    Tool.GetFontSize(Text, FontHeight);

	private void InitSection1(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {//preferred height + 16 
		var Container1Size = new Size(Capsule.Width - 20, 80);
		var Container1Loca = new Point(10, 0);
		var Container1BCol = Color.FromArgb(2, 55, 110);

		var Container2Size = new Size(Container1Size.Width - 16, Container1Size.Height - 16);
		var Container2Loca = new Point(8, 8);
		var Container2BCol = Container1BCol;

		Control.Image(Container1, Container2, Container1Size, Container1Loca, Container1BCol);
		Control.Image(Container2, Container3, Container2Size, Container2Loca, Container2BCol);

		Tool.Round(Container2, 6);

		var Label1Text = ("Connection Timeout:");
		var Label1Size = GetFontSize(Label1Text);
		var Label1Loca = new Point(0, 0);

		var Label2Text = ("Protocol:");
		var Label2Size = GetFontSize(Label2Text);
		var Label2Loca = new Point(Label1Loca.X, Label1Size.Height + 8);

		var LabelBCol = Container2BCol;
		var LabelFCol = Color.White;

		Control.Label(Container2, Label1, Label1Size, Label1Loca, LabelBCol, LabelFCol, 1, 9, Label1Text);
		Control.Label(Container2, Label2, Label2Size, Label2Loca, LabelBCol, LabelFCol, 1, 9, Label2Text);

		var TextBoxSize = new Size(Container2.Width - Label1.Width - 40, 20);
		var TextBoxLoca = new Point(Label1.Width, 0);
		var TextBoxBCol = DashDialog.MenuBar.Bar.BackColor;
		var TextBoxFCol = Color.White;

		Control.TextBox(Container2, TextBox1, TextBoxSize, TextBoxLoca, TextBoxBCol, TextBoxFCol, 1, 8);


	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox Container5 = new PictureBox();
	private readonly PictureBox Container4 = new PictureBox();

	private readonly Button Button1 = new Button();
	private readonly Label Label3 = new Label();

	private void InitSection2()
	{
	    try
	    {
		// Bottom Section
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox Container1 = new PictureBox();

	private void InitSection3()
	{
	    try
	    {
		// Encapsulating Section
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Initialize(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		InitSection1(DashDialog, Capsule);
		InitSection2();
		InitSection3();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}