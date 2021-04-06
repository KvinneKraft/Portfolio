
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
		    Init1();
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
			SpruceLog.InitializePage(DashWindow, Capsule);
		    }

		    SpruceLog.Show();
		})

		{ IsBackground = true };

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
