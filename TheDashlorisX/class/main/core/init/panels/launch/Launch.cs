
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
    public class Launch
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tools = new DashTools();


	private readonly Button TopButton1 = new Button();
	private readonly Label TopLabel1 = new Label();

	private void TopInit1()
	{

	}

	private readonly PictureBox TopContainer1 = new PictureBox();

	private void InitContainer1(PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width - 20, 90);
		var ContainerLoca = new Point(10, 10);
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		Control.Image(Capsule, TopContainer1, ContainerSize, ContainerLoca, ContainerBCol);

		var LabelText = ("Statistics:");
		var LabelLoca = new Point(8, 8);
		var LabelFCol = Color.White;

		Control.Label(TopContainer1, TopLabel1, Size.Empty, LabelLoca, ContainerBCol, LabelFCol, 1, 12, LabelText);

		Tools.Round(TopContainer1, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}


	private readonly PictureBox BottomContainer1 = new PictureBox();
	private readonly PictureBox BottomContainer2 = new PictureBox();

	private void InitContainer2()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}


	public void Initialize(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		InitContainer1(Capsule);
		InitContainer2();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}