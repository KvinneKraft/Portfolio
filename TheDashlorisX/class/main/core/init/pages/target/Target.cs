
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
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();
	
	private readonly PictureBox TopContainer1 = new PictureBox();
	private readonly PictureBox TopContainer2 = new PictureBox();

	private void Init1(PictureBox Capsule)
	{
	    try
	    {
		var Container1BCol = Capsule.BackColor;
		var Container1Loca = new Point(0, 0);
		var Container1Size = Capsule.Size;

		var Container2Size = new Size(Container1Size.Width - 10, 100);
		var Container2Loca = new Point(5, (Container1Size.Height - Container2Size.Height) / 2);
		var Container2BCol = Color.FromArgb(2, 55, 110);

		Control.Image(TopContainer1, TopContainer2, Container2Size, Container2Loca, Container2BCol);
		Control.Image(Capsule, TopContainer1, Container1Size, Container1Loca, Container1BCol);

		Tool.Round(TopContainer2, 6);
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
	
	private readonly Label TopLabel1 = new Label();
	private readonly Label TopLabel2 = new Label();
	private readonly Label TopLabel3 = new Label();
	private readonly Label TopLabel4 = new Label();

	private Size GetFontSize(string text, int height = 10) =>
	    Tool.GetFontSize(text, height);

	private void AddInputBox(Label Label, Size LabelSize, Point LabelLoca, string LabelText, TextBox TextBox, Size TextBoxSize, Point TextBoxLoca, string TextBoxText, DashDialog DashDialog)
	{
	    try
	    {
		var TextBoxBCol = DashDialog.MenuBar.Bar.BackColor;
		var TextBoxFCol = Color.White;

		Control.TextBox(TopContainer2, TextBox, TextBoxSize, TextBoxLoca, TextBoxBCol, TextBoxFCol, 1, 9, ReadOnly: true);

		TextBox.TextAlign = HorizontalAlignment.Center;
		TextBox.Text = TextBoxText;

		Tool.Round(TextBox.Parent, 6);

		var LabelBCol = TopContainer2.BackColor;
		var LabelFCol = Color.White;

		Control.Label(TopContainer2, Label, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, 10, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void Init2(DashDialog DashDialog)
	{
	    try
	    {
		var Label4Size = GetFontSize("Duration:");
		var Label3Size = GetFontSize("Bytes:");
		var Label1Size = GetFontSize("Host:");
		var Label2Size = GetFontSize("Port:");

		var TextBox1Size = new Size(135, 20);
		var TextBox3Size = new Size(85, 20);

		var TextBox4Size = new Size(TopContainer2.Width - (TextBox3Size.Width + Label3Size.Width + 22 + Label4Size.Width), 20);
		var TextBox2Size = new Size(TopContainer2.Width - (TextBox1Size.Width + Label1Size.Width + 22 + Label2Size.Width), 20);

		var Label1Loca = new Point(5, 8);
		var TextBox1Loca = new Point(Label1Size.Width + 5, 6);

		var Label2Loca = new Point(TextBox1Size.Width + TextBox1Loca.X + 10, 8);
		var TextBox2Loca = new Point(Label2Loca.X + Label2Size.Width, 6);

		int Y2 = (TextBox2Loca.Y + TextBox2Size.Height + 10);

		var Label3Loca = new Point(5, TextBox2Loca.Y + TextBox2Size.Height + 10);
		var TextBox3Loca = new Point(Label3Loca.X + Label3Size.Width, Y2);

		var Label4Loca = new Point(TextBox3Loca.X + TextBox3Size.Width + 10, Y2);
		var TextBox4Loca = new Point(Label4Loca.X + Label4Size.Width, Y2);
		
		AddInputBox(TopLabel1, Label1Size, Label1Loca, ("Host:"), TopTextBox1, TextBox1Size, TextBox1Loca, ("https://pugpawz.com"), DashDialog);
		AddInputBox(TopLabel4, Label4Size, Label4Loca, ("Duration:"), TopTextBox4, TextBox4Size, TextBox4Loca, ("5250"), DashDialog);
		AddInputBox(TopLabel3, Label3Size, Label3Loca, ("Bytes:"), TopTextBox3, TextBox3Size, TextBox3Loca, ("75250"), DashDialog);
		AddInputBox(TopLabel2, Label2Size, Label2Loca, ("Port:"), TopTextBox2, TextBox2Size, TextBox2Loca, ("65535"), DashDialog);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	
	private readonly Button TopButton1 = new Button();

	private void Init3()
	{
	    try
	    {
		var ButtonSize = new Size(135, 24);
		var ButtonLoca = new Point(8, TopContainer2.Height - 32);
		var ButtonBCol = TopTextBox1.BackColor;
		var ButtonFCol = Color.White;

		Control.Button(TopContainer2, TopButton1, ButtonSize, ButtonLoca, ButtonBCol, ButtonFCol, 1, 8, "Test Connection");

		Tool.Round(TopButton1, 6);
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
		Init3();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
