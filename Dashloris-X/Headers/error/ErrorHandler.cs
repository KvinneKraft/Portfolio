// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
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
		$"----------------------\r\n" +
		$"I would also recommend making sure you actually downloaded the application from my website https://pugpawz.com and not some other sketchy website.  Latest versions are available at my GitHub at https://github.com/KvinneKraft"
	    );
	}

	public static Exception GetException(Exception E)
	{
	    return new Exception(GetFormat(E));
	}

	public static void Utilize(string error, string title, bool thread = false)
	{
	    new Thread(() => 
	    {
		new DashBox().Show(error, title, Color.FromArgb(14, 14, 14), Color.FromArgb(6, 6, 6), Color.FromArgb(20, 20, 20), Color.White, false);
		Environment.Exit(-1);
	    })

	    { IsBackground = thread }.Start();
	}
    }
}
