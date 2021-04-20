
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
    public class GEOWhoosh
    {
	public readonly DashControls Control = new DashControls();
	public readonly DashTools Tool = new DashTools();


	public readonly WHOOSH whoosh = new WHOOSH();
	//public readonly GEOIP geoip = new GEOIP();


	public class WHOOSH : GEOWhoosh
	{
	    private readonly PictureBox S1Container1 = new PictureBox();

	    private readonly TextBox S1TextBox1 = new TextBox();
	    private readonly TextBox S1TextBox2 = new TextBox();
	    private readonly TextBox S1TextBox3 = new TextBox();
	    private readonly TextBox S1TextBox4 = new TextBox();
	    private readonly TextBox S1TextBox5 = new TextBox();
	    private readonly TextBox S1TextBox6 = new TextBox();

	    private readonly Button S1Button1 = new Button();

	    private readonly Label S1Label1 = new Label();
	    private readonly Label S1Label2 = new Label();
	    private readonly Label S1Label3 = new Label();
	    private readonly Label S1Label4 = new Label();
	    private readonly Label S1Label5 = new Label();
	    private readonly Label S1Label6 = new Label();
	    private readonly Label S1Label7 = new Label();

	    public void Init1()
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
			

			needInit = false;
		    }

		    S1Container1.Show();
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
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


	/*public class GEOIP : GEOWhoosh
	{
	    private bool needInit = true;

	    public void Show(PictureBox Capsule, DashWindow DashWindow, InitThaDashlorisX Parent)
	    {
		try
		{
		    if (needInit)
		    {


			needInit = false;
		    }

		    S1Container1.Show();
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
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
	}*/
    }
}