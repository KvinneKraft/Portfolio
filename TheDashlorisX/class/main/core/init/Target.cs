
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
    public class Target
    {
	private readonly DashControls Controls = new DashControls();
	private readonly DashTools Tools = new DashTools();


	private readonly PictureBox TopContainer1 = new PictureBox();
	private readonly PictureBox TopContainer2 = new PictureBox();

	private void Init1(PictureBox Capsule)
	{
	    try
	    {
		var Container1BCol = Capsule.BackColor;
		var Container1Loca = new Point(0, 0);
		var Container1Size = Capsule.Size;

		var Container2Size = new Size(Container1Size.Width - 10, 90);
		var Container2Loca = new Point(5, (Container1Size.Height - Container2Size.Height) / 2);
		var Container2BCol = Color.FromArgb(2, 55, 110);

		Controls.Image(TopContainer1, TopContainer2, Container2Size, Container2Loca, Container2BCol);
		Controls.Image(Capsule, TopContainer1, Container1Size, Container1Loca, Container1BCol);

		Tools.Round(TopContainer2, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox TopContainer3 = new PictureBox();

	public readonly TextBox TopTextBox1 = new TextBox();
	public readonly TextBox TopTextBox2 = new TextBox();
	public readonly TextBox TopTextBox3 = new TextBox();
	public readonly TextBox TopTextBox4 = new TextBox();

	private readonly Button TopButton1 = new Button();

	private readonly Label TopLabel1 = new Label();
	private readonly Label TopLabel2 = new Label();
	private readonly Label TopLabel3 = new Label();
	private readonly Label TopLabel4 = new Label();

	private Size GetFontSize(string text, int height = 10) =>
	    Tools.GetFontSize(text, height);

	private void Init2(DashDialog DashDialog)
	{
	    try
	    {
		var LabelBCol = TopContainer2.BackColor;
		var LabelFCol = Color.White;

		var TextBoxBCol = DashDialog.MenuBar.Bar.BackColor;
		var TextBoxFCol = Color.White;

		var Label3Text = ("Duration:");
		var Label4Text = ("Bytes:");
		var Label2Text = ("Port:");
		var Label1Text = ("Host:");

		var Label1Size = GetFontSize(Label1Text);
		var Label2Size = GetFontSize(Label2Text);
		var Label3Size = GetFontSize(Label3Text);
		var Label4Size = GetFontSize(Label4Text);

		int Y1 = (8);

		var Label1Loca = new Point(5, Y1);
		var TextBox1Size = new Size(135, 20);
		var TextBox1Loca = new Point(Label1Size.Width + 5, Y1);

		var Label2Loca = new Point(TextBox1Size.Width + TextBox1Loca.X + 10, Y1);
		var TextBox2Loca = new Point(Label2Loca.X + Label2Size.Width, Y1);
		var TextBox2Size = new Size(TopContainer2.Width - TextBox2Loca.X - 8, 20);

		int Y2 = (TextBox2Loca.Y + TextBox2Size.Height + 10);

		var Label3Loca = new Point(5, TextBox2Loca.Y + TextBox2Size.Height + 10);
		var TextBox3Loca = new Point(Label3Loca.X + Label3Size.Width, Y2);
		var TextBox3Size = new Size(85, 20);

		var Label4Loca = new Point(TextBox3Loca.X + TextBox3Size.Width + 10, Y2);
		var TextBox4Loca = new Point(Label4Loca.X + Label4Size.Width, Y2);
		var TextBox4Size = new Size(TopContainer2.Width - TextBox4Loca.X - 8, 20);

		Controls.Label(TopContainer2, TopLabel1, Label1Size, Label1Loca, LabelBCol, LabelFCol, 1, 10, Label1Text);
		Controls.Label(TopContainer2, TopLabel2, Label2Size, Label2Loca, LabelBCol, LabelFCol, 1, 10, Label2Text);
		Controls.Label(TopContainer2, TopLabel3, Label3Size, Label3Loca, LabelBCol, LabelFCol, 1, 10, Label3Text);
		Controls.Label(TopContainer2, TopLabel4, Label4Size, Label4Loca, LabelBCol, LabelFCol, 1, 10, Label4Text);

		Controls.TextBox(TopContainer2, TopTextBox1, TextBox1Size, TextBox1Loca, TextBoxBCol, TextBoxFCol, 1, 9);
		Controls.TextBox(TopContainer2, TopTextBox2, TextBox2Size, TextBox2Loca, TextBoxBCol, TextBoxFCol, 1, 9);
		Controls.TextBox(TopContainer2, TopTextBox3, TextBox3Size, TextBox3Loca, TextBoxBCol, TextBoxFCol, 1, 9);
		Controls.TextBox(TopContainer2, TopTextBox4, TextBox4Size, TextBox4Loca, TextBoxBCol, TextBoxFCol, 1, 9);

		Tools.Round(TopTextBox1.Parent, 6);
		Tools.Round(TopTextBox2.Parent, 6);
		Tools.Round(TopTextBox3.Parent, 6);
		Tools.Round(TopTextBox4.Parent, 6);
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
		Init1(Capsule);
		Init2(DashDialog);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
