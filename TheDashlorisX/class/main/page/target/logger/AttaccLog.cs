
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

using DashFramework.Networking;
using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class AttaccLog
    { 
	public readonly SpruceLog SprucyLog = new SpruceLog();
	private readonly DashNet DashNet = new DashNet();


	private void Print(string Data, bool NewLine = true) =>
	    SprucyLog.Print(Data, NewLine);

	public bool Visible() =>
	    (SprucyLog.S1Container1.Visible);


	(string, int, int, string) ParseSect1(LockOn S3Class1)
	{
	    try
	    {
		if (!DashNet.CanIP(S3Class1.S2TextBox1.Text))
		{//individual parameters with values for identification externally.
		    return (null, -1, -1, null);
		}

		if (!DashNet.CanPort(S3Class1.S2TextBox2.Text))
		{
		    return (null, -1, -1, null);
		}

		if (!DashNet.CanDuration(S3Class1.S2TextBox3.Text))
		{
		    return (null, -1, -1, null);
		}

		int Duration = DashNet.GetInteger(S3Class1.S2TextBox3.Text);
		int Port = DashNet.GetPort(S3Class1.S2TextBox2.Text);

		string Replace(string Variable, string newValue, params string[] replaceValues)
		{
		    try
		    {
			foreach (string value in replaceValues)
			{
			    Variable = Variable.Replace(value, newValue);
			}

			return Variable;
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		string HTTPv = Replace(S3Class1.S2Label5.Text, "", "(", ")", "---", " ");
		string Host = DashNet.GetIP(S3Class1.S2TextBox1.Text);

		return (Host, Port, Duration, HTTPv);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	(int, int, int, int, int, bool) ParseSect2(Settings S3Class2)
	{
	    try
	    {
		var possibleInts = new TextBox[]
		{
		    S3Class2.S2Textbox1,
		    S3Class2.S2Textbox2,
		    S3Class2.S2Textbox3,
		    S3Class2.S2Textbox4,
		    S3Class2.S2Textbox5
		};

		var Collection = new List<int>();

		foreach (var TextBox in possibleInts)
		{
		    if (!DashNet.CanInteger(TextBox.Text))
		    {
			return (-1, -1, -1, -1, -1, false);
		    }

		    Collection.Add(DashNet.GetInteger(TextBox.Text));
		}

		bool UAR = (S3Class2.S2Container3.BackColor == 
		    Color.DarkMagenta ? true : false);

		return (Collection[0], Collection[1], 
		    Collection[2], Collection[3], Collection[4], UAR);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	(List<string>, List<string>) ParseSect3(Settings S3Class2)
	{
	    try
	    {
		return S3Class2.GetProxyData();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly DashArtillery Artillery = new DashArtillery();

	public void CommenceLaunch(LockOn S3Class1, Settings S3Class2, DashWindow DashWindow, PictureBox Capsule)
	{
	    try
	    {
		new Thread(() =>
		{
		    if (SprucyLog.RequiresInit)
		    {
			void Invoker()
			{
			    try
			    {
				SprucyLog.InitializePage(DashWindow, Capsule);
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

		    Print("Validating configuration ....");

		    SprucyLog.Show();
		})

		{ IsBackground = true }.Start();

		/*Send Delay, Timeout, Dash Workers, Max Cons, Content Length and UAR*/
		(int, int, int, int, int, bool) Tier2Configuration = ParseSect2(S3Class2);
		/*Proxy Hosts and Proxy Credentials*/
		(List<string>, List<string>) Tier3Configuration = ParseSect3(S3Class2);
		/*Host, Port, Duration and HTTP Version*/
		(string, int, int, string) Tier1Configuration = ParseSect1(S3Class1);

		if (Tier1Configuration.Item1 == null)
		{
		    Print("The main host panel settings section was found incorrectly setup. Invalid values detected!");
		    return;
		}

		else if (Tier2Configuration.Item1 == -1)
		{
		    Print("The main fancy settings section was found incorrectly setup. Invalid values detected!");
		    return;
		}

		else if (Tier3Configuration.Item1 == null)
		{
		    Print("The proxy configuration section was found incorrectly setup. Invalid values detected!");
		    return;
		}

		Print("No errors found! proceeding with execution ....");

		//---: Pass configuration to attack method in here.
		//---: In attack method, start treating the configuration
		//---: sets based on tier wise order.

		Artillery.Launch(this, SprucyLog);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public class DashArtillery
	{
	    private void StartDurationCounter()
	    {
		try
		{

		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private void StopDurationCounter()
	    {
		try
		{

		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public void Launch(AttaccLog Inst, SpruceLog Logy)
	    {
		try
		{
		    StartDurationCounter();

		    while (Logy.KeepStressing)
		    {

		    }

		    if (!Logy.KeepStressing)
		    {
			StopDurationCounter();//Only if Forcibly stopped.
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}
    }
}
