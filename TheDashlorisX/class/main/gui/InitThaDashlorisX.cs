
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
    public class InitThaDashlorisX : DashGlobe
    {
	public readonly DashDialog DashDialog = new DashDialog();

	private void Init1()
	{
	    try
	    {
		var DialogMCol = Color.FromArgb(17, 38, 94);
		var DialogBCol = Color.FromArgb(6, 14, 36);
		var DialogSize = new Size(400, 350);

		DashDialog.JustInitialize(DialogSize, ("Tha Dashloris-X  -  3.0"), DialogBCol, DialogMCol);

		DashDialog.MenuBar.Close.Click += (s, e) =>
		{
		    ExitApplication();
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S2Container1 = new PictureBox();//Bottom Bar : S2Container1 = Sector 2 Container 1
	private readonly PictureBox S2Container2 = new PictureBox();//Icon Container
	private readonly PictureBox S2Container3 = new PictureBox();//Icon Itself

	private readonly Button S2Button1 = new Button();

	private void Init2()
	{
	    try
	    {
		var Container1Size = new Size(DashDialog.Width, 34);
		var Container1Loca = new Point(0, DashDialog.Height - 34);
		var Container1BCol = DashDialog.MenuBar.Bar.BackColor;

		Control.Image(DashDialog, S2Container1, Container1Size, Container1Loca, Container1BCol);

		var Container2Size = new Size(26, 26);
		var Container2Loca = new Point(5, 4);

		Control.Image(S2Container1, S2Container2, Container2Size, Container2Loca, Color.Black);
		Tool.Round(S2Container2, 6);

		var Container3Loca = new Point(1, 1);
		var Container3Size = new Size(24, 24);

		Control.Image(S2Container2, S2Container3, Container3Size, Container3Loca, Container1BCol, Properties.Resources.MenuBarIcon);

		var Button1Size = new Size(100, 24);
		var Button1Loca = new Point(Container1Size.Width - 105, 5);

		Control.Button(S2Container1, S2Button1, Button1Size, Button1Loca, Container1BCol, Color.White, 1, 9, ("Launch Attack"));
		Tool.Round(S2Button1, 6);

		// Status Text in the middle of bar?
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly Button S3Button1 = new Button();
	private readonly Button S3Button2 = new Button();
	private readonly Button S3Button3 = new Button();
	private readonly Button S3Button4 = new Button();
	private readonly Button S3Button5 = new Button();

	private readonly Label S3Label1 = new Label();

	private void Init3()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void InitializeApp()
	{
	    try
	    {
		Init1();//GUI
		Init2();//BottomBar
		Init3();//SideMenu
		//Init4();//Capsule
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}