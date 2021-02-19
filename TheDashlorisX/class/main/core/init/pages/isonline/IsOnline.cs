
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



	private readonly PictureBox TopContainer = new PictureBox();

	// topinit1
	private readonly Label TopLabel1 = new Label();
	private readonly Label TopLabel2 = new Label();
	private readonly Label TopLabel3 = new Label();
	private readonly Label TopLabel4 = new Label();

	private readonly TextBox TextBox1 = new TextBox();
	private readonly TextBox TextBox2 = new TextBox();
	private readonly TextBox TextBox3 = new TextBox();
	// dropdownmenu

	private void InitContainer1()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}



	private readonly PictureBox BottomContainer = new PictureBox();

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

	public void Initialize(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		InitContainer1();
		InitContainer2();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}