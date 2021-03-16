
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
    public class Settings
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();//Main Capsule Container
	private readonly PictureBox S1Container2 = new PictureBox();//Main Configuration Container
	private readonly PictureBox S1Container3 = new PictureBox();//Main Proxy Container

	private void InitS1(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		var Container1Loca = new Point(0, 0);
		var Container1BCol = Capsule.BackColor;

		Control.Image(Capsule, S1Container1, Capsule.Size, Container1Loca, Container1BCol);

		var ContainerSize = new Size(Capsule.Width, 105);//102 original calculation
		var ContainerBCol = DashWindow.MenuBar.MenuBar.BackColor;

		var Container3Loca = new Point(0, ContainerSize.Height + 10);
		var Container2Loca = new Point(0, 0);

		Control.Image(S1Container1, S1Container3, ContainerSize, Container3Loca, ContainerBCol);
		Control.Image(S1Container1, S1Container2, ContainerSize, Container2Loca, ContainerBCol);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S2Container1 = new PictureBox();//Configuration Container;

	private void InitS2()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();//Proxy List Container
	private readonly PictureBox S3Container2 = new PictureBox();//Proxy Option Container

	private void InitS3()
	{

	}


	public void Initialize(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		InitS1(Capsule, DashWindow);
		InitS2();
		InitS3();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
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