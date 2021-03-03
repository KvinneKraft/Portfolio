
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
    public class AppToS
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();

	private void Init1()
	{
	    //Capsule Cover
	}


	private readonly PictureBox S2Container1 = new PictureBox();

	private readonly Label S2Label1 = new Label();//Title MEssage
	private readonly Label S2Label2 = new Label();//not Necessary (1/4)

	private void Init2()
	{
	    //Top Bar
	}


	private readonly PictureBox S3Container1 = new PictureBox();//Button Container

	private readonly Button S3Button1 = new Button();//Left
	private readonly Button S3Button2 = new Button();//Right

	private readonly Label S3Label1 = new Label();

	private void Init3()
	{
	    //Bottom Bar
	}


	private readonly PictureBox S4Container1 = new PictureBox();
	private readonly PictureBox S4Container2 = new PictureBox();
	
	// List of Labels

	private void MagicBoxSetup()//I love this name.
	{

	}

	private void Init4()
	{
	    //Magic Box
	}

	public void InitializePage(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		Init1();
		Init2();
		Init3();
		Init4();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
