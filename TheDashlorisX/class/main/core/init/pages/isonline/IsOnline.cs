
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

	private readonly PictureBox TopContainer1 = new PictureBox();
	private readonly PictureBox TopContainer2 = new PictureBox();

	private readonly Label TopLabel1 = new Label();
	private readonly Label TopLabel2 = new Label();
	private readonly Label TopLabel3 = new Label();
	private readonly Label TopLabel4 = new Label();
	private readonly Label TopLabel5 = new Label();

	private readonly TextBox TextBox1 = new TextBox();
	private readonly TextBox TextBox2 = new TextBox();
	private readonly TextBox TextBox3 = new TextBox();

	private readonly DropDownMenu DropDownMenu = new DropDownMenu();

	private void AddLabel(Label Label, Size LabelSize, Point LabelLoca, string LabelText, DashDialog DashDialog, int FontHeight)
	{
	    try
	    {
		var LabelBCol = DashDialog.MenuBar.Bar.BackColor;
		var LabelFCol = Color.White;

		Control.Label(TopContainer2, Label, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, FontHeight, LabelText);

		Label.TextAlign = ContentAlignment.MiddleCenter;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void SetupDropDownMenu(Point Loca, Size Size, DashDialog DashDialog)
	{
	    try
	    {
		var MenuLoca = new Point(Loca.X + Size.Width, Loca.Y + 20);

		DropDownMenu.SetupMenu(TopContainer2, MenuLoca, Color.FromArgb(8, 8, 8), Color.FromArgb(8, 8, 8));

		var Label4Size = new Size(75, 20);
		var Label4Loca = new Point(Size.Width, Loca.Y);

		AddLabel(TopLabel4, Label4Size, Label4Loca, "TCP", DashDialog, 8);

		TopLabel4.MouseEnter += (s, e) =>
		{
		    DropDownMenu.Show();
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

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

		Control.Label(TopContainer2, Label, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, 10, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private Size GetFontSize(string text, int height = 10) =>
	    Tool.GetFontSize(text, height);

	private void InitContainer1(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var Container1Size = new Size(Capsule.Width - 20, 104);
		var Container1Loca = new Point(10, (Capsule.Height - 104) / 2);
		var Container1BCol = Color.FromArgb(2, 55, 110);

		var Container2Size = new Size(TopContainer1.Width - 16, 54);
		var Container2Loca = new Point(8, 8);
		var Container2BCol = TopContainer1.BackColor;

		Control.Image(TopContainer1, TopContainer2, Container2Size, Container2Loca, Container2BCol);
		Control.Image(Capsule, TopContainer1, Container1Size, Container1Loca, Container1BCol);

		var Label4Size = GetFontSize("Packet Size:");
		var Label1Size = GetFontSize("Timeout:");
		var Label3Size = GetFontSize("Method:");
		var Label2Size = GetFontSize("Port:");

		var Label1Loca = new Point(0, 1);
		var TextBox1Size = new Size(65, 20);
		var TextBox1Loca = new Point(Label1Size.Width, 0);

		var Label2Loca = new Point(TextBox1Size.Width + TextBox1Loca.X + 8, 1);
		var TextBox2Size = new Size(TopContainer2.Width - (Label2Loca.X + Label2Size.Width), 20);
		var TextBox2Loca = new Point(Label2Loca.X + Label2Size.Width, 0);

		var Label3Loca = new Point(0, Label1Loca.Y + Label1Size.Height + 8);

		SetupDropDownMenu(Label3Loca, Label3Size, DashDialog);

		var Label4Loca = new Point(TopLabel4.Width + TopLabel4.Left + 8, Label3Loca.Y);
		var TextBox3Size = new Size(TopContainer2.Width - (Label4Loca.X + Label4Size.Width), 20);
		var TextBox3Loca = new Point(Label4Loca.X + Label4Size.Width, Label4Loca.Y - 1);

		AddInputBox(TopLabel1, Label1Size, Label1Loca, ("Timeout:"), TextBox1, TextBox1Size, TextBox1Loca, ("0"), DashDialog);
		AddInputBox(TopLabel2, Label2Size, Label2Loca, ("Port:"), TextBox2, TextBox2Size, TextBox2Loca, ("0"), DashDialog);
		AddInputBox(TopLabel3, Label3Size, Label3Loca, ("Method:"), TextBox3, TextBox3Size, TextBox3Loca, ("0"), DashDialog);

		Control.Label(TopContainer2, TopLabel5, Label4Size, Label4Loca, TopContainer2.BackColor, Color.White, 1, 10, ("Packet Size:"));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox BottomContainer = new PictureBox();
	private readonly Button BottomButton = new Button();
	private readonly Label BottomLabel = new Label();

	private void InitContainer2()
	{
	    try
	    {
		var ContainerSize = new Size(MainContainer.Width - 16, 32);
		var ContainerLoca = new Point(8, TopContainer2.Top + TopContainer2.Height + 10);
		var ContainerBCol = MainContainer.BackColor;

		Control.Image(MainContainer, BottomContainer, ContainerSize, ContainerLoca, ContainerBCol);

		var ButtonSize = new Size(95, 24);
		var ButtonLoca = new Point(0, 4);
		var ButtonBCol = ContainerBCol;
		var ButtonFCol = Color.White;

		Control.Button(BottomContainer, BottomButton, ButtonSize, ButtonLoca, ButtonBCol, ButtonFCol, 1, 9, "Check Settings");

		var LabelText = ("Status: Unknown");
		var LabelSize = GetFontSize(LabelText, 11);
		var LabelLoca = new Point(BottomContainer.Width - LabelSize.Width);
		var LabelBCol = ContainerBCol;
		var LabelFCol = Color.White;

		Control.Label(BottomContainer, BottomLabel, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, 12, LabelText);

		MessageBox.Show($"{BottomContainer.Top + BottomContainer.Height}|{LabelSize.Height}");
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
		InitContainer1(DashDialog, Capsule);
		InitContainer2(Capsule);

		Tool.Round(MainContainer, 6);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}