
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

	
	private readonly Label TopLabel1 = new Label();

	private void TopInit1(Color LabelBCol)
	{
	    try
	    {
		var LabelText = ("Statistics:");
		var LabelLoca = new Point(8, 8);
		var LabelFCol = Color.White;

		Control.Label(TopContainer1, TopLabel1, Size.Empty, LabelLoca, LabelBCol, LabelFCol, 1, 12, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}


	private readonly Button TopButton1 = new Button();

	private void TopInit2(DashDialog DashDialog)
	{
	    try
	    {
		var ButtonText = ("Reset");
		var ButtonSize = new Size(85, 20);
		var ButtonLoca = new Point(TopContainer1.Width - 93, 8);
		var ButtonBCol = DashDialog.MenuBar.Bar.BackColor;
		var ButtonFCol = Color.White;

		Control.Button(TopContainer1, TopButton1, ButtonSize, ButtonLoca, ButtonBCol, ButtonFCol, 1, 7, ButtonText);

		Tools.Round(TopButton1, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}


	private readonly PictureBox TopContainer1 = new PictureBox();

	private void InitContainer1(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width - 20, 90);
		var ContainerLoca = new Point(10, 10);
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		Control.Image(Capsule, TopContainer1, ContainerSize, ContainerLoca, ContainerBCol);

		TopInit1(ContainerBCol);
		TopInit2(DashDialog);

		Tools.Round(TopContainer1, 6);
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
		InitContainer1(DashDialog, Capsule);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}