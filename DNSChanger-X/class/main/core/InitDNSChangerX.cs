﻿
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
		var MenuBarBCol = Color.Purple;
		var AppBCol = Color.HotPink;
		var AppSize = new Size(334, 250);

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
	private readonly Label TopLabel3 = new Label(); //version
	private readonly Label TopLabel4 = new Label(); //4
	private readonly Label TopLabel5 = new Label(); //6

	public readonly TextBox TopTextBox1 = new TextBox();
	public readonly TextBox TopTextBox2 = new TextBox();

	private readonly PictureBox TopContainer1 = new PictureBox();
	private readonly PictureBox TopContainer2 = new PictureBox();
	private readonly PictureBox TopContainer3 = new PictureBox();

	private readonly PictureBox TopCheckBox1 = new PictureBox();
	private readonly PictureBox TopCheckBox2 = new PictureBox();

	private readonly Button TopButton1 = new Button(); //spoof

	private Size GetLabelSize(string text, int size = 9) =>
	    TextRenderer.MeasureText(text, Tool.GetFont(1, size));

	private void InitTop1()
	{
	    try
	    {
		var ContainerBCol = Color.Purple;

		var Container1Size = new Size(DashDialog.Width - 20, 150);
		var Container1Loca = new Point(10, DashDialog.MenuBar.Bar.Height + 10);

		Control.Image(DashDialog, TopContainer1, Container1Size, Container1Loca, ContainerBCol);

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
		var TextBoxBCol = Color.FromArgb(16, 16, 16);
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

	private void InitTop2()
	{
	    try
	    {
		var ContainerBCol = Color.Purple;

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

		var CheckBoxBCol = Color.FromArgb(16, 16, 16);

		Control.Image(TopContainer3, TopCheckBox1, CheckBoxSize, CheckBox1Loca, CheckBoxBCol);
		Control.Image(TopContainer3, TopCheckBox2, CheckBoxSize, CheckBox2Loca, CheckBoxBCol);

		Tool.Round(TopCheckBox1, 6);
		Tool.Round(TopCheckBox2, 6);

		var Button1Size = new Size(TopTextBox1.Parent.Width, 20);
		var Button1Loca = new Point(TopContainer3.Width - Button1Size.Width, (TopContainer3.Height - Button1Size.Height) / 2);
		var Button1BCol = Color.FromArgb(16, 16, 16);
		var Button1FCol = Color.White;

		Control.Button(TopContainer3, TopButton1, Button1Size, Button1Loca, Button1BCol, Button1FCol, 1, 7, "Change DNS");
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
	private readonly PictureBox BottomContainer3 = new PictureBox();

	private readonly Button BottomButton1 = new Button(); //is online
	private readonly Button BottomButton2 = new Button(); //dns list
	private readonly Button BottomButton3 = new Button(); //help
	private readonly Button BottomButton4 = new Button(); //about

	public void BottomComponent()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}


