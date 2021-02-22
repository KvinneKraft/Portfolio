
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

		Control.Label(TopContainer, Label, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, FontHeight, LabelText);

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

		DropDownMenu.SetupMenu(TopContainer, MenuLoca, Color.FromArgb(8, 8, 8), Color.FromArgb(8, 8, 8));

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

		Control.TextBox(TopContainer, TextBox, TextBoxSize, TextBoxLoca, TextBoxBCol, TextBoxFCol, 1, 8, ReadOnly: true);

		TextBox.TextAlign = HorizontalAlignment.Center;
		TextBox.Text = TextBoxText;

		Tool.Round(TextBox.Parent, 6);

		var LabelBCol = TopContainer.BackColor;
		var LabelFCol = Color.White;

		Control.Label(TopContainer, Label, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, 10, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private Size GetFontSize(string text, int height = 10) =>
	    Tool.GetFontSize(text, height);

	private readonly PictureBox TopContainer = new PictureBox();

	private void InitContainer1(DashDialog DashDialog)
	{
	    try
	    {
		var ContainerSize = new Size(MainContainer.Width - 16, 54);
		var ContainerLoca = new Point(8, 8);
		var ContainerBCol = MainContainer.BackColor;

		Control.Image(MainContainer, TopContainer, ContainerSize, ContainerLoca, ContainerBCol);

		var Label4Size = GetFontSize("Packet Size:");
		var Label1Size = GetFontSize("Timeout:");
		var Label3Size = GetFontSize("Method:");
		var Label2Size = GetFontSize("Port:");

		var Label1Loca = new Point(0, 1);
		var TextBox1Size = new Size(65, 20);
		var TextBox1Loca = new Point(Label1Size.Width, 0);

		var Label2Loca = new Point(TextBox1Size.Width + TextBox1Loca.X + 8, 1);
		var TextBox2Size = new Size(TopContainer.Width - (Label2Loca.X + Label2Size.Width), 20);
		var TextBox2Loca = new Point(Label2Loca.X + Label2Size.Width, 0);

		var Label3Loca = new Point(0, Label1Loca.Y + Label1Size.Height + 8);

		SetupDropDownMenu(Label3Loca, Label3Size, DashDialog);

		var Label4Loca = new Point(TopLabel4.Width + TopLabel4.Left + 8, Label3Loca.Y);
		var TextBox3Size = new Size(TopContainer.Width - (Label4Loca.X + Label4Size.Width), 20);
		var TextBox3Loca = new Point(Label4Loca.X + Label4Size.Width, Label4Loca.Y - 1);

		AddInputBox(TopLabel1, Label1Size, Label1Loca, ("Timeout:"), TextBox1, TextBox1Size, TextBox1Loca, ("0"), DashDialog);
		AddInputBox(TopLabel2, Label2Size, Label2Loca, ("Port:"), TextBox2, TextBox2Size, TextBox2Loca, ("0"), DashDialog);
		AddInputBox(TopLabel3, Label3Size, Label3Loca, ("Method:"), TextBox3, TextBox3Size, TextBox3Loca, ("0"), DashDialog);

		Control.Label(TopContainer, TopLabel5, Label4Size, Label4Loca, TopContainer.BackColor, Color.White, 1, 10, ("Packet Size:"));
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
		var ContainerSize = new Size(MainContainer.Width - 16, 20);
		var ContainerLoca = new Point(8, TopContainer.Top + TopContainer.Height + 10);
		var ContainerBCol = MainContainer.BackColor;

		Control.Image(MainContainer, BottomContainer, ContainerSize, ContainerLoca, ContainerBCol);

		

		MessageBox.Show($"{BottomContainer.Top + BottomContainer.Height}");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox MainContainer = new PictureBox();

	public void Initialize(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width - 20, 104);
		var ContainerLoca = new Point(10, (Capsule.Height - 104) / 2);
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		Control.Image(Capsule, MainContainer, ContainerSize, ContainerLoca, ContainerBCol);

		InitContainer1(DashDialog);
		InitContainer2();

		Tool.Round(MainContainer, 6);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}