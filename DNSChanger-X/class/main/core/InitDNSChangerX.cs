
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
		var Container1Size = new Size(DashDialog.Width - 20, 150);
		var Container1Loca = new Point(10, DashDialog.MenuBar.Bar.Height + 10);
		var Container1BCol = Color.Purple;

		Control.Image(DashDialog, TopContainer1, Container1Size, Container1Loca, Container1BCol);

		var Label1Loca = new Point(5, 5);
		var Label2Loca = new Point(Label1Loca.X, Label1Loca.Y + TextRenderer.MeasureText("IP-2:", Tool.GetFont(1, 10)).Height + 10);

		var LabelBCol = Container1BCol;
		var LabelFCol = Color.White;

		Control.Label(TopContainer1, TopLabel1, Size.Empty, Label1Loca, LabelBCol, LabelFCol, 1, 10, "IP-1:");
		Control.Label(TopContainer1, TopLabel2, Size.Empty, Label2Loca, LabelBCol, LabelFCol, 1, 10, "IP-2:");
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


