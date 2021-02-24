
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


	private readonly PictureBox S2Container1 = new PictureBox();//Sector 2 Container 1
	private readonly PictureBox S2Container2 = new PictureBox();
	private readonly PictureBox S2Container3 = new PictureBox();

	private void Init2()
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