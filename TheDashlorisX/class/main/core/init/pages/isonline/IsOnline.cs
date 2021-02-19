
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



	private readonly Label TopLabel1 = new Label();
	private readonly Label TopLabel2 = new Label();
	private readonly Label TopLabel3 = new Label();
	private readonly Label TopLabel4 = new Label();

	private readonly TextBox TextBox1 = new TextBox();
	private readonly TextBox TextBox2 = new TextBox();
	private readonly TextBox TextBox3 = new TextBox();

	private readonly DropDownMenu DropDownMenu = new DropDownMenu();

	private void TopInit1()
	{
	    try
	    {
		
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}



	private readonly PictureBox TopContainer = new PictureBox();

	private void InitContainer1()
	{
	    try
	    {
		var ContainerSize = new Size(MainContainer.Width - 16, 54);
		var ContainerLoca = new Point(8, 8);
		var ContainerBCol = MainContainer.BackColor;

		Control.Image(MainContainer, TopContainer, ContainerSize, ContainerLoca, ContainerBCol);

		TopInit1();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}



	private readonly PictureBox BottomContainer = new PictureBox();
	private readonly Button BottomButton = new Button();
	private readonly Label BottomLabel = new Label();

	private void InitContainer2()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}



	private readonly PictureBox MainContainer = new PictureBox();

	public void Initialize(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width - 20, 104);
		var ContainerLoca = new Point(10, (Capsule.Height - 104) / 2);
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		Control.Image(Capsule, MainContainer, ContainerSize, ContainerLoca, ContainerBCol);

		InitContainer1();
		InitContainer2();

		Tool.Round(MainContainer, 6);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}