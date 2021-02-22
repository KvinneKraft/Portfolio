
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

	private readonly PictureBox Container2 = new PictureBox();
	private readonly PictureBox Container3 = new PictureBox();
	private readonly PictureBox PictureBox = new PictureBox();

	private readonly TextBox TextBox1 = new TextBox();
	private readonly TextBox TextBox2 = new TextBox();

	private readonly Label Label1 = new Label();
	private readonly Label Label2 = new Label();

	private void InitSection1()
	{
	    try
	    {
		// Top Section
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox Container5 = new PictureBox();
	private readonly PictureBox Container4 = new PictureBox();

	private readonly Button Button1 = new Button();
	private readonly Label Label3 = new Label();

	private void InitSection2()
	{
	    try
	    {
		// Bottom Section
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox Container1 = new PictureBox();

	private void InitSection3()
	{
	    try
	    {
		// Encapsulating Section
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
		InitSection1();
		InitSection2();
		InitSection3();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}