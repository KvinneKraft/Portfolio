
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
	private readonly Label Label3 = new Label();

	private readonly DropDownMenu DropDownMenu = new DropDownMenu();

	private readonly Label Item1 = new Label();
	private readonly Label Item2 = new Label();

	private Size GetFontSize(string Text, int FontHeight = 9) =>
	    Tool.GetFontSize(Text, FontHeight);

	private void SetupDropDownMenu(Point Label3Loca, Color TextBoxBCol)
	{
	    try
	    {
		var MenuLocation = new Point(Label3Loca.X + 8, Container3.Height + Container3.Top);
		var MenuBCol = TextBoxBCol;

		DropDownMenu.SetupMenu(Container1, MenuLocation, MenuBCol, MenuBCol);
		
		DropDownMenu.AddItem(Item1, "[Tcp]", MenuBCol, Color.White, ItemTextSize: 8, ItemWidth: 106, ItemHeight: 20);
		DropDownMenu.AddItem(Item2, "[Udp]", MenuBCol, Color.White, ItemTextSize: 8, ItemWidth: 106, ItemHeight: 20);

		Label3.MouseEnter += (s, e) =>
		{
		    if (!DropDownMenu.Container.Visible)
		    {
			DropDownMenu.Show();
		    }
		};

		Container2.MouseEnter += (s, e) =>
		{
		    if (DropDownMenu.Container.Visible)
		    {
			DropDownMenu.Hide();
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void InitSection1(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {//preferred height + 16 
		var Container1Size = new Size(Capsule.Width - 40, 61);
		var Container1Loca = new Point(0, 0);
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
		var Label2Loca = new Point(Label1Loca.X, Label1Size.Height + 12);

		var Label3Text = ("--[Protocol: Tcp]--");
		var Label3Size = new Size(110, 18);
		var Label3Loca = new Point(Label2Size.Width + 4, Label2Loca.Y);

		var LabelBCol = Container2BCol;
		var LabelFCol = Color.White;

		Control.Label(Container3, Label3, Label3Size, Label3Loca, DashDialog.MenuBar.Bar.BackColor, LabelFCol, 1, 8, Label3Text);
		Control.Label(Container3, Label1, Label1Size, Label1Loca, LabelBCol, LabelFCol, 1, 9, Label1Text);
		Control.Label(Container3, Label2, Label2Size, Label2Loca, LabelBCol, LabelFCol, 1, 9, Label2Text);

		Label3.TextAlign = ContentAlignment.MiddleCenter;

		var TextBoxSize = new Size(Container3.Width - Label1.Width - 40, 19);
		var TextBoxLoca = new Point(Label1.Width + 4, 0);
		var TextBoxBCol = DashDialog.MenuBar.Bar.BackColor;
		var TextBoxFCol = Color.White;

		Control.TextBox(Container3, TextBox1, TextBoxSize, TextBoxLoca, TextBoxBCol, TextBoxFCol, 1, 8);

		TextBox1.TextAlign = HorizontalAlignment.Center;
		TextBox1.Text = ("5000");

		Tool.Round(TextBox1.Parent, 6);

		SetupDropDownMenu(Label3Loca, TextBoxBCol);

		var PictureBoxImage = Properties.Resources.isonline1;
		var PictureBoxSize = PictureBoxImage.Size;
		var PictureBoxLoca = new Point(Container3.Width - PictureBoxSize.Width, Container3.Height - PictureBoxSize.Height);
		var PictureBoxBCol = Container2BCol;
		
		Control.Image(Container3, PictureBox, PictureBoxSize, PictureBoxLoca, PictureBoxBCol, PictureBoxImage);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox Container5 = new PictureBox();
	private readonly PictureBox Container4 = new PictureBox();

	private readonly Button Button1 = new Button();
	private readonly Label Label4 = new Label();

	private void InitSection2(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var Container1Size = new Size(Capsule.Width - 40, 36);
		var Container1Loca = new Point(0, Container2.Height + 10);
		var Container1BCol = Container2.BackColor;

		var Container2Size = new Size(Container1Size.Width - 16, Container1Size.Height - 16);
		var Container2Loca = new Point(8, 8);
		var Container2BCol = Container1BCol;

		Control.Image(Container1, Container4, Container1Size, Container1Loca, Container1BCol);
		Control.Image(Container4, Container5, Container2Size, Container2Loca, Container2BCol);

		Tool.Round(Container4, 6);

		var ButtonSize = new Size(100, 20);
		var ButtonLoca = new Point(0, 0);
		var ButtonBCol = DashDialog.MenuBar.Bar.BackColor;
		var ButtonFCol = Color.White;

		Control.Button(Container5, Button1, ButtonSize, ButtonLoca, ButtonBCol, ButtonFCol, 1, 8, "Check");

		Tool.Round(Button1, 6);

		var LabelText = ("Status: Unknown");
		var LabelSize = GetFontSize(LabelText, 12);
		var LabelLoca = new Point(Container5.Width - LabelSize.Width, 0);
		var LabelBCol = Container2BCol;
		var LabelFCol = Color.White;

		Control.Label(Container5, Label4, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, 12, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox Container1 = new PictureBox();

	private void InitSection3(PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width - 20, Container4.Height + Container4.Top);
		var ContainerLoca = new Point(20, (Capsule.Height - ContainerSize.Height) / 2);
		var ContainerBCol = Capsule.BackColor;

		Control.Image(Capsule, Container1, ContainerSize, ContainerLoca, ContainerBCol);
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
		InitSection2(DashDialog, Capsule);
		InitSection3(Capsule);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}