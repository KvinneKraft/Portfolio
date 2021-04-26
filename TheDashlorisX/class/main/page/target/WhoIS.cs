
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

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class WHOIS
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();

	private void Init1(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width, Capsule.Height);
		var ContainerLoca = new Point(0, 0);

		Control.Image(Capsule, S1Container1, ContainerSize, ContainerLoca, Capsule.BackColor);
		
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	

	private readonly PictureBox S2Container1 = new PictureBox();
	private readonly PictureBox S2Container2 = new PictureBox();

	private readonly TextBox S2TextBox1 = new TextBox();

	private readonly Label S2Label1 = new Label();

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


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly TextBox S3TextBox1 = new TextBox();

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


	private bool needInit = true;

	public void Show(PictureBox Capsule, DashWindow DashWindow, InitThaDashlorisX Parent)
	{
	    try
	    {
		if (needInit)
		{
		    Init1(Capsule, DashWindow);
		    Init2();
		    Init3();

		    needInit = false;
		}

		Parent.HideContainers();
		S1Container1.Show();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Hide()
	{
	    try
	    {
		S1Container1.Hide();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
