// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DashlorisX
{
    public class ErrorHandler
    {
	public readonly DashControls Control = new DashControls();
	public readonly DashTools Tool = new DashTools();

	public static string GetFormat(Exception E)
	{
	    return string.Format
	    (
		$"An error has occurred and has prevented the application from functioning any further, safely.  Please send the following to KvinneKraft@protonmail.com if you wish to help me fix this.\r\n" +
		$"----------------------\r\n" +
		$"{E.StackTrace}\r\n" + 
		$"----------------------\r\n" + 
		$"{E.Message}\r\n" + 
		$"----------------------\r\n" + 
		$"{E.Source}\r\n" + 
		$"----------------------\r\n"
	    );
	}

	public static Exception GetException(Exception E)
	{
	    return new Exception(GetFormat(E));
	}

	public static void Utilize(string error, string title, bool thread = false)
	{
	    new Thread(() => {
		new Dialog(error, title).ShowDialog();
	    })

	    { IsBackground = thread }.Start();
	}

	private class Dialog : Form
	{
	    public Dialog(string error, string title)
	    {
		//Custom Dialog Soon
		MessageBox.Show(error, title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		Environment.Exit(-1);
	    }
	}
    }
}
