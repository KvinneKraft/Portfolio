
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
    public class About
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();



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



	private readonly PictureBox Container2 = new PictureBox();
	private readonly TextBox TextBox1 = new TextBox();
	private readonly Label Label1 = new Label();

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