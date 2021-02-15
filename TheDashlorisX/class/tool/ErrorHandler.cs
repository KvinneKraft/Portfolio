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

namespace TheDashlorisX
{
    public class ErrorHandler
    {
	public readonly DashControls Control = new DashControls();
	public readonly DashTools Tool = new DashTools();

	public static string GetFormat(Exception E)
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

	public static Exception GetException(Exception E)
	{
	    return new Exception(GetFormat(E));
	}

	public static void Utilize(string error, string title, bool thread = false)
	{
	    new Thread(() => 
	    {
		var ContainerBCol = Color.FromArgb(9, 39, 66);
		var MenuBarBCol = Color.FromArgb(19, 36, 64);
		var AppBCol = Color.FromArgb(6, 17, 33);

		new DashBox().Show(error, title, AppBCol, MenuBarBCol,ContainerBCol, Color.White);

		Environment.Exit(-1);
	    })

	    { IsBackground = thread }.Start();
	}

	public static void JustDoIt(Exception E, string title = "Error Handler")
	{
	    Utilize(GetFormat(E), title);
	}
    }
}
