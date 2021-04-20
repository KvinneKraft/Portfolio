
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
	public readonly GEOIP geoip = new GEOIP();

	public class GEOIP
	{
	    public readonly DashControls Control = new DashControls();
	    public readonly DashTools Tool = new DashTools();


	    private readonly PictureBox S1Container1 = new PictureBox();
	    private readonly PictureBox S1Container2 = new PictureBox();

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

	    public void Init1(PictureBox Capsule, DashWindow DashWindow, InitThaDashlorisX Parent)
	    {
		try
		{
		    var Container1Size = new Size(Capsule.Width, Capsule.Height);
		    var Container1Loca = new Point(0, 0);
		    var Container1BCol = Capsule.BackColor;

		    var Container2Size = new Size(Container1Size.Width, Container1Size.Height - 35);
		    var Container2Loca = new Point(0, 35);
		    var Container2BCol = DashWindow.MenuBar.MenuBar.BackColor;

		    Control.Image(S1Container1, S1Container2, Container2Size, Container2Loca, Container2BCol);
		    Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Container1BCol);

		    Tool.Round(S1Container2, 6);

		    ControlHelper CHelper = new ControlHelper();

		    var Label1Size = CHelper.GetFontSize("GEOIP Results", 16);
		    var Label1Loca = new Point(10, 0);

		    Control.Label(S1Container1, S1Label1, Label1Size, Label1Loca, Container1BCol, Color.White, 1, 16, ("GEOIP Results"));

		    
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
			Init1(Capsule, DashWindow, Parent);

			needInit = false;
		    }

		    S1Container1.BringToFront();
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