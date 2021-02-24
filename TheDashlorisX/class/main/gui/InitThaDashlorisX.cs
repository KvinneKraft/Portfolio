
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
	private readonly PictureBox S2Container4 = new PictureBox();//Launch Container

	private readonly Button S2Button1 = new Button();

	private void Init2()
	{
	    try
	    {
		var Container1Size = new Size(DashDialog.Width, 30);
		var Container1Loca = new Point(0, DashDialog.Height - 30);
		var Container1BCol = DashDialog.MenuBar.Bar.BackColor;

		Control.Image(DashDialog, S2Container1, Container1Size, Container1Loca, Container1BCol);

		var Container2Size = new Size(26, 24);
		var Container2Loca = new Point(5, 3);

		Control.Image(S2Container1, S2Container2, Container2Size, Container2Loca, Container1BCol);
		
		var Container3Loca = new Point(0, 0);

		Control.Image(S2Container2, S2Container3, Container2Size, Container3Loca, Container1BCol, Properties.Resources.MenuBarIcon);
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
		//Init3();//SideMenu
		//Init4();//Capsule
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}