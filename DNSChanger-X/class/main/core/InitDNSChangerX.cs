
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DNSChangerX
{
    public class Initialize
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	public readonly DashDialog DashDialog = new DashDialog();

	public void CoreComponent()
	{
	    try
	    {
		var MenuBarBCol = Color.FromArgb(14, 0, 57);
		var AppBCol = Color.FromArgb(36, 1, 112);
		var AppSize = new Size(334, 180);

		DashDialog.JustInitialize(AppSize, ("DNS Changer-X  -  1.0"), AppBCol, MenuBarBCol);

		DashDialog.MenuBar.Close.Click += (s, e) =>
		{
		    Application.Exit();
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly Label TopLabel1 = new Label(); //ip1 
	private readonly Label TopLabel2 = new Label(); //ip2

	public readonly TextBox TopTextBox1 = new TextBox();
	public readonly TextBox TopTextBox2 = new TextBox();

	private readonly PictureBox TopContainer1 = new PictureBox();
	private readonly PictureBox TopContainer2 = new PictureBox();
	private readonly PictureBox TopContainer3 = new PictureBox();

	private readonly Button TopButton1 = new Button(); //spoof

	private Size GetLabelSize(string text, int size = 9) =>
	    TextRenderer.MeasureText(text, Tool.GetFont(1, size));

	private void InitTop1()
	{
	    try
	    {
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		var Container1Size = new Size(DashDialog.Width - 20, 58);
		var Container1Loca = new Point(10, DashDialog.MenuBar.Bar.Height + 10);

		Control.Image(DashDialog, TopContainer1, Container1Size, Container1Loca, ContainerBCol);

		Tool.Round(TopContainer1, 6);

		var LabelSize = GetLabelSize("IP-2:", 10);
		var LabelBCol = ContainerBCol;
		var LabelFCol = Color.White;

		int Container2Width = (Container1Size.Width - 10);//10 => 5 from top and sides of container1.
		int TextBoxWidth = ((Container2Width - ((LabelSize.Width * 2) + 10)) / 2);//10 => separation width

		var Label2Loca = new Point(LabelSize.Width + TextBoxWidth + 10, 0);
		var Label1Loca = new Point(0, 0);

		Control.Label(TopContainer2, TopLabel1, Size.Empty, Label1Loca, LabelBCol, LabelFCol, 1, 10, "IP-1:");
		Control.Label(TopContainer2, TopLabel2, Size.Empty, Label2Loca, LabelBCol, LabelFCol, 1, 10, "IP-2:");

		var TextBoxSize = new Size(TextBoxWidth, 23);
		var TextBoxBCol = DashDialog.MenuBar.Bar.BackColor;
		var TextBoxFCol = Color.White;

		var TextBox1Loca = new Point(TopLabel1.Width + TopLabel1.Left, Label1Loca.Y - 2);
		var TextBox2Loca = new Point(TopLabel2.Width + TopLabel2.Left, Label2Loca.Y - 2);

		Control.TextBox(TopContainer2, TopTextBox1, TextBoxSize, TextBox1Loca, TextBoxBCol, TextBoxFCol, 1, 9);
		Control.TextBox(TopContainer2, TopTextBox2, TextBoxSize, TextBox2Loca, TextBoxBCol, TextBoxFCol, 1, 9);

		TopTextBox1.Text = ("208.67.222.222");
		TopTextBox2.Text = ("208.67.220.220");

		TopTextBox1.TextAlign = HorizontalAlignment.Center;
		TopTextBox2.TextAlign = HorizontalAlignment.Center;

		Tool.Round(TopTextBox1.Parent, 6);
		Tool.Round(TopTextBox2.Parent, 6);

		var Container2Size = new Size(Container2Width, TopLabel2.Top + TopLabel2.Height + 2);
		var Container2Loca = new Point(3, 5);

		Control.Image(TopContainer1, TopContainer2, Container2Size, Container2Loca, ContainerBCol);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void HandleCheckBox(PictureBox TheBox, PictureBox NotTheBox)
	{
	    if (TheBox.BackColor == CheckEnable)
	    {
		return;
	    }

	    NotTheBox.BackColor = CheckDisable;
	    TheBox.BackColor = CheckEnable;
	}
	
	private void InitTop2()
	{
	    try
	    {
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		var Container3Size = new Size(TopContainer2.Width, 20);
		var Container3Loca = new Point(3, TopContainer2.Top + TopContainer2.Height + 8);

		Control.Image(TopContainer1, TopContainer3, Container3Size, Container3Loca, ContainerBCol);

		var LabelBCol = ContainerBCol;
		var LabelFCol = Color.White;

		var Label3Size = GetLabelSize("Version", 10);
		var Label3Loca = new Point(0, 0);

		var Label4Size = GetLabelSize("4", 10);
		var Label4Loca = new Point(Label3Size.Width, Label3Loca.Y);

		var CheckBoxSize = new Size(16, 16);

		var Label5Size = GetLabelSize("or 6", 10);
		var Label5Loca = new Point(Label4Size.Width + Label4Loca.X + CheckBoxSize.Width + 2, Label3Loca.Y);

		Control.Label(TopContainer3, TopLabel3, Label3Size, Label3Loca, LabelBCol, LabelFCol, 1, 10, "Version");
		Control.Label(TopContainer3, TopLabel4, Label4Size, Label4Loca, LabelBCol, LabelFCol, 1, 10, "4");
		Control.Label(TopContainer3, TopLabel5, Label5Size, Label5Loca, LabelBCol, LabelFCol, 1, 10, "or 6");

		var CheckBox1Loca = new Point(Label4Loca.X + Label4Size.Width, ((Label4Size.Height - CheckBoxSize.Height) + Label4Loca.Y) / 2);
		var CheckBox2Loca = new Point(Label5Loca.X + Label5Size.Width, ((Label5Size.Height - CheckBoxSize.Height) + Label5Loca.Y) / 2);

		var CheckBoxBCol = DashDialog.MenuBar.Bar.BackColor;

		Control.Image(TopContainer3, TopCheckBox1, CheckBoxSize, CheckBox1Loca, CheckBoxBCol);
		Control.Image(TopContainer3, TopCheckBox2, CheckBoxSize, CheckBox2Loca, CheckBoxBCol);

		TopCheckBox1.BackColor = CheckEnable;

		TopCheckBox1.Click += (s, e) => HandleCheckBox(TopCheckBox1, TopCheckBox2);
		TopCheckBox2.Click += (s, e) => HandleCheckBox(TopCheckBox2, TopCheckBox1);

		Tool.Round(TopCheckBox1, 6);
		Tool.Round(TopCheckBox2, 6);

		var Button1Size = new Size(TopTextBox1.Parent.Width, 20);
		var Button1Loca = new Point(TopContainer3.Width - Button1Size.Width, (TopContainer3.Height - Button1Size.Height) / 2);
		var Button1BCol = DashDialog.MenuBar.Bar.BackColor;
		var Button1FCol = Color.White;

		Control.Button(TopContainer3, TopButton1, Button1Size, Button1Loca, Button1BCol, Button1FCol, 1, 7, "Change DNS");

		Tool.Round(TopButton1, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void TopComponent()
	{
	    try
	    {
		InitTop1();
		InitTop2();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox BottomContainer1 = new PictureBox();
	private readonly PictureBox BottomContainer2 = new PictureBox();

	private readonly Button BottomButton1 = new Button(); //is online
	private readonly Button BottomButton2 = new Button(); //dns list
	private readonly Button BottomButton3 = new Button(); //help
	private readonly Button BottomButton4 = new Button(); //info

	private readonly AppHelp AppHelp = new AppHelp();
	private readonly AppInfo AppInfo = new AppInfo();
	private readonly DnsList DnsList = new DnsList();

	public void BottomComponent()
	{
	    try
	    {
		var Container1Size = new Size(TopContainer1.Width, 63);
		var Container1Loca = new Point(10, TopContainer1.Height + TopContainer1.Top + 10);

		var Container2Size = new Size(Container1Size.Width - 10, Container1Size.Height - 10);
		var Container2Loca = new Point(5, 5);

		var ContainerBCol = Color.FromArgb(2, 55, 110);

		Control.Image(BottomContainer1, BottomContainer2, Container2Size, Container2Loca, ContainerBCol);
		Control.Image(DashDialog, BottomContainer1, Container1Size, Container1Loca, ContainerBCol);

		Tool.Round(BottomContainer1, 6);

		var ButtonSize = new Size((BottomContainer2.Width - 5) / 2, 24);
		var ButtonBCol = DashDialog.MenuBar.Bar.BackColor;
		var ButtonFCol = Color.White;

		var Button4Loca = new Point(ButtonSize.Width + 5, ButtonSize.Height + 5);
		var Button3Loca = new Point(0, ButtonSize.Height + 5);
		var Button2Loca = new Point(ButtonSize.Width + 5, 0);
		var Button1Loca = new Point(0, 0);

		Control.Button(BottomContainer2, BottomButton1, ButtonSize, Button1Loca, ButtonBCol, ButtonFCol, 1, 9, "Is Online");
		Control.Button(BottomContainer2, BottomButton2, ButtonSize, Button2Loca, ButtonBCol, ButtonFCol, 1, 9, "DNS List");
		Control.Button(BottomContainer2, BottomButton3, ButtonSize, Button3Loca, ButtonBCol, ButtonFCol, 1, 9, "App Help");
		Control.Button(BottomContainer2, BottomButton4, ButtonSize, Button4Loca, ButtonBCol, ButtonFCol, 1, 9, "App Info");

		foreach (Button Button in BottomContainer2.Controls)
		{
		    Tool.Round(Button, 6);
		}

		BottomButton2.Click += (s, e) =>
		{
		    DnsList.Show();
		};

		BottomButton3.Click += (s, e) =>
		{
		    AppHelp.Show(DashDialog, BottomContainer1);
		};

		BottomButton4.Click += (s, e) =>
		{
		    AppInfo.Show(DashDialog, BottomContainer1);
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}


