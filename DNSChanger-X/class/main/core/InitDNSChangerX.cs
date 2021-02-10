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
		var AppSize = new Size(325, 250);

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

	private readonly PictureBox TopSelectBox1 = new PictureBox();
	private readonly PictureBox TopSelectBox2 = new PictureBox();

	private readonly Button TopButton1 = new Button(); //spoof

	public void TopComponent()
	{
	    try
	    {
		var ContainerBCol = Color.Purple;

		var Container1Size = new Size(DashDialog.Width - 20, 150);
		var Container1Loca = new Point(10, DashDialog.MenuBar.Bar.Height + 10);

		Control.Image(DashDialog, TopContainer1, Container1Size, Container1Loca, ContainerBCol);

		var LabelSize = TextRenderer.MeasureText("IP-2:", Tool.GetFont(1, 10));
		var LabelBCol = ContainerBCol;
		var LabelFCol = Color.White;

		var Label2Loca = new Point(0, LabelSize.Height + 10);
		var Label1Loca = new Point(0, 0);

		Control.Label(TopContainer2, TopLabel1, Size.Empty, Label1Loca, LabelBCol, LabelFCol, 1, 10, "IP-1:");
		Control.Label(TopContainer2, TopLabel2, Size.Empty, Label2Loca, LabelBCol, LabelFCol, 1, 10, "IP-2:");

		int Container2Width = Container1Size.Width - 10;

		var TextBoxSize = new Size(Container2Width - TopLabel2.Left - TopLabel2.Width - 5, 21);
		var TextBoxBCol = Color.FromArgb(16, 16, 16);
		var TextBoxFCol = Color.White;

		var TextBox1Loca = new Point(TopLabel1.Width + TopLabel1.Left, Label1Loca.Y - 2);
		var TextBox2Loca = new Point(TopLabel1.Width + TopLabel1.Left, Label2Loca.Y - 2);

		Control.TextBox(TopContainer2, TopTextBox1, TextBoxSize, TextBox1Loca, TextBoxBCol, TextBoxFCol, 1, 9);
		Control.TextBox(TopContainer2, TopTextBox2, TextBoxSize, TextBox2Loca, TextBoxBCol, TextBoxFCol, 1, 9);

		var Container2Size = new Size(Container2Width, TopLabel2.Top + TopLabel2.Height + 2);
		var Container2Loca = new Point(5, 5);

		Control.Image(TopContainer1, TopContainer2, Container2Size, Container2Loca, ContainerBCol);
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


