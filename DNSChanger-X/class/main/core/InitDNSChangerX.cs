
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DNSChangerX
{
    public class Initialize
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	public readonly DashDialog DashDialog = new DashDialog();

	public void CoreComponent()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly Label TopLabel1 = new Label(); //ip1 
	private readonly Label TopLabel2 = new Label(); //ip2
	private readonly Label TopLabel3 = new Label(); //version
	private readonly Label TopLabel4 = new Label(); //4
	private readonly Label TopLabel5 = new Label(); //6

	public readonly TextBox TopTextBox1 = new TextBox();
	public readonly TextBox TopTextBox2 = new TextBox();

	private readonly PictureBox TopContainer1 = new PictureBox();
	private readonly PictureBox TopContainer2 = new PictureBox();
	private readonly PictureBox TopContainer3 = new PictureBox();

	private readonly PictureBox TopSelectBox1 = new PictureBox();
	private readonly PictureBox TopSelectBox2 = new PictureBox();

	private readonly Button TopButton1 = new Button(); //spoof

	public void TopComponent()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox BottomContainer1 = new PictureBox();
	private readonly PictureBox BottomContainer2 = new PictureBox();
	private readonly PictureBox BottomContainer3 = new PictureBox();

	private readonly Button BottomButton1 = new Button(); //is online
	private readonly Button BottomButton2 = new Button(); //dns list
	private readonly Button BottomButton3 = new Button(); //help
	private readonly Button BottomButton4 = new Button(); //about

	public void BottomComponent()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}


