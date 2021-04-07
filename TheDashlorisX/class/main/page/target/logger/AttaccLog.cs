
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
		    var Container1Size = new Size(Capsule.Width, 225);
		    var Container1Loca = new Point(0, -2);

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

	    private void SendLog(string Data, bool newLine = true)
	    {
		try
		{
		    S3TextBox.AppendText($"{Data}{(newLine ? "\r\n" : string.Empty)}");
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private void S2SetupButtonEvents()
	    {
		try
		{
		    S2Button1.Click += (s, e) =>
		    {
			S3TextBox.Clear();
			SendLog("+ Ah mate, you gotta lock on...still!");
		    };

		    S2Button2.Click += (s, e) =>
		    {
			// SaveFileDialog
		    };

		    S2Button3.Click += (s, e) =>
		    {

		    };

		    S2Button4.Click += (s, e) =>
		    {

		    };
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private void Init2(DashWindow DashWindow)
	    {
		try
		{
		    var ContainerSize = new Size(210, 50);
		    var ContainerLoca = new Point(-2, -2);

		    Control.Image(S1Container2, S2Container, ContainerSize, ContainerLoca, S1Container2.BackColor);

		    var ButtonSize = new Size(100, 20);
		    var ButtonBCol = DashWindow.BackColor;
		    var ButtonFCol = Color.White;

		    var Button2Loca = new Point(ButtonSize.Width + 10, 0);
		    var Button4Loca = new Point(Button2Loca.X, 30);
		    var Button3Loca = new Point(0, 30);
		    var Button1Loca = new Point(0, 0);

		    Control.Button(S2Container, S2Button1, ButtonSize, Button1Loca, ButtonBCol, ButtonFCol, 1, 8, ("Clear Log"));
		    Control.Button(S2Container, S2Button2, ButtonSize, Button2Loca, ButtonBCol, ButtonFCol, 1, 8, ("Save Log"));
		    Control.Button(S2Container, S2Button3, ButtonSize, Button3Loca, ButtonBCol, ButtonFCol, 1, 8, ("Verbose"));
		    Control.Button(S2Container, S2Button4, ButtonSize, Button4Loca, ButtonBCol, ButtonFCol, 1, 8, ("Cancel"));

		    foreach (Button Button in S2Container.Controls)
		    {
			Tool.Round(Button, 6);
		    }

		    S2SetupButtonEvents();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    private readonly PictureBox S3Container = new PictureBox();
	    private readonly TextBox S3TextBox = new TextBox();

	    private void Init3()
	    {
		try
		{
		    var ContainerSize = new Size(S1Container3.Width - 20, S1Container3.Height - 20);
		    var ContainerLoca = new Point(10, 10);

		    Control.Image(S1Container3, S3Container, ContainerSize, ContainerLoca, S2Button1.BackColor);
		    Tool.Round(S3Container, 6);

		    var TextBoxSize = new Size(ContainerSize.Width - 10, ContainerSize.Height - 10);
		    var TextBoxLoca = new Point(5, 5);

		    Control.TextBox(S3Container, S3TextBox, TextBoxSize, TextBoxLoca, S2Button1.BackColor, Color.White, 1, 7, ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

		    S3TextBox.Text = string.Format
		    (
			$"+ I am waiting for you to fukin press that button, ye?\r\n" +
			$"+ Current Date: {DateTime.Now}\r\n" +
	    		$"+ Verbose = OFF\r\n"
		    );
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
		    Init2(DashWindow);
		    Init3();

		    RequiresInit = false;
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

	public bool KeepVerbosing = false;
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
			    try
			    {
				SpruceLog.InitializePage(DashWindow, Capsule);
			    }

			    catch (Exception E)
			    {
				throw (ErrorHandler.GetException(E));
			    }
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
