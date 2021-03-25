
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
    public class ServerPing
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();
	private readonly PictureBox S1Container2 = new PictureBox();
	private readonly PictureBox S1Container3 = new PictureBox();

	private void Init1()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S2Container1 = new PictureBox();

	private readonly TextBox S2TextBox1 = new TextBox();
	private readonly TextBox S2TextBox2 = new TextBox();
	private readonly TextBox S2TextBox3 = new TextBox();
	private readonly TextBox S2TextBox4 = new TextBox();

	private readonly Label S2Label1 = new Label();
	private readonly Label S2Label2 = new Label();
	private readonly Label S2Label3 = new Label();
	private readonly Label S2Label4 = new Label();
	private readonly Label S2Label5 = new Label();
	private readonly Label S2Label6 = new Label();

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

	private readonly Button S3Button1 = new Button();

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


	private bool isInitialized = false;

	public void InitializePage(DashWindow DashWindow, PictureBox Capsule, InitThaDashlorisX Parent)
	{
	    try
	    {
		if (!isInitialized)
		{
		    Init1();
		    Init2();
		    Init3();

		    isInitialized = true;
		}

		Show();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}

	public void Show()
	{
	    try
	    {
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
