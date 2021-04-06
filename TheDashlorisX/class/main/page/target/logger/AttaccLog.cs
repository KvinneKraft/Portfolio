
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class AttaccLog
    {
	public class LogGUI
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();


	    private readonly PictureBox S1Container1 = new PictureBox();
	    private readonly PictureBox S1Container2 = new PictureBox();
	    private readonly PictureBox S1Container3 = new PictureBox();

	    private void Init1(DashWindow DashWindow, PictureBox Capsule)
	    {
		try
		{
		    var Container1Size = new Size(Capsule.Width, Capsule.Height);
		    var Container1Loca = new Point(0, 0);

		    Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Capsule.BackColor);

		    var Container2Size = new Size(Capsule.Width, 70);
		    var Container2Loca = new Point(0, 0);

		    var Container3Size = new Size(Capsule.Width, 135);
		    var Container3Loca = new Point(0, 90);

		    var ContainerBCol = DashWindow.MenuBar.MenuBar.BackColor;

		    Control.Image(S1Container1, S1Container2, Container2Size, Container2Loca, ContainerBCol);
		    Control.Image(S1Container1, S1Container3, Container3Size, Container3Loca, ContainerBCol);

		    Tool.Round(S1Container2, 6);
		    Tool.Round(S1Container3, 6);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    private readonly PictureBox S2Container = new PictureBox();

	    private readonly Button S2Button1 = new Button();
	    private readonly Button S2Button2 = new Button();
	    private readonly Button S2Button3 = new Button();
	    private readonly Button S2Button4 = new Button();

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


	    public bool RequiresInit = true;

	    public void InitializePage(DashWindow DashWindow, PictureBox Capsule)
	    {
		try
		{
		    Init1(DashWindow, Capsule);
		    Init2();
		    Init3();
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

	private readonly LogGUI SpruceLog = new LogGUI();
	public bool KeepStressing = true;

	public void CommenceLaunch(LockOn S3Class1, Settings S3Class2, DashWindow DashWindow, PictureBox Capsule)
	    //string Host, string Port, string Duration, string HTTPv, string SendDelay, string Timeout, 
	    //string DashWorkers, string MaxConnections, string ContentLength, bool UAR, List<string> ProxyList, 
	    //List<string> CredentialList)
	{
	    try
	    {
		new Thread(() =>
		{
		    if (SpruceLog.RequiresInit)
		    {
			void Invoker()
			{
			    SpruceLog.InitializePage(DashWindow, Capsule);
			}

			Capsule.Invoke(new MethodInvoker(Invoker));
		    }

		    foreach (Control Control in Capsule.Controls)
		    {
			if (Control.Visible)
			{
			    Control.Hide();
			}
		    }

		    SpruceLog.Show();
		})

		{ IsBackground = true }.Start();

		/*
		 * Method 1:
		 * Returns a tuple with parsed Host, Port, Duration and HTTPv.
		 * 
		 * Method 2:
		 * Returns a tuple with parsed SendDelay, Timeout, DashWorkers,
		 * Max Connections, Content Length and UAR.
		 * 
		 * Method 3:
		 * Returns a tuple with a list of proxies and credentials for these
		 * potential proxies.
		 */
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void StopAttack()
	{
	    try
	    {
		KeepStressing = false;
		Thread.Sleep(1250);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
