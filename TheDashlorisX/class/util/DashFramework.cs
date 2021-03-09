
//
// All rights reserved to me Dashie for coding all of this.  If you wish to make use of this code, you can.
// Just make sure to leave this top part in if you are going to redistribute any part of my code.  Thank you.
//
// -Dashie

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Networking;
using DashFramework.Erroring;
using DashFramework.Dialog;

namespace DashFramework
{
    namespace Erroring
    {
	public class ErrorHandler
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();


	    public static string GetRawFormat(Exception E)
	    {
		return string.Format
		(
		    $"An error has occurred and has prevented the application from functioning any further, safely.\r\n\r\nPlease send the following to KvinneKraft@protonmail.com if you wish to help me fix this issue.\r\n\r\n" +
		    $"----------------------\r\n" +
		    $"{E.StackTrace}\r\n" +
		    $"----------------------\r\n" +
		    $"{E.Message}\r\n" +
		    $"----------------------\r\n" +
		    $"{E.Source}\r\n" +
		    $"----------------------\r\n\r\n" +
		    $"I would also recommend making sure you actually downloaded the application from my website https://pugpawz.com and not some other sketchy website.\r\n\r\nAll the latest versions are available at my GitHub at https://github.com/KvinneKraft"
		);
	    }

	    public static Exception GetException(Exception E) =>
		new Exception(GetRawFormat(E));

	    public static void Utilize(string description, string title)
	    {
		DashBox ErrorDialog = new DashBox();

		Color ContainerBCol = Color.FromArgb(9, 39, 66);
		Color MenuBarBCol = Color.FromArgb(19, 36, 64);
		Color AppBCol = Color.FromArgb(6, 17, 33);

		ErrorDialog.ShowMe(description, title, AppBCol, MenuBarBCol, ContainerBCol, Color.White);
		
		Environment.Exit(-1);
	    }

	    public static void JustDoIt(Exception E, string title = ("Error Handler")) =>
		Utilize(GetRawFormat(E), title);
	}
    }


    namespace Dialog
    {
	public class DashBox
	{
	    public enum Button { OKCancel, YesNo, OK }

	    public int ShowMe(string message, string title, Color AppBCol, Color MenuBarBCol, Color TextContainerBCol, Color TextContainerForeColor)
	    {
		try
		{

		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}

		return -1;
	    }
	}
    }


    namespace Networking
    {
	public class DashNet
	{
	    private readonly DashBox DashBox = new DashBox();
	    
	}
    }
}
