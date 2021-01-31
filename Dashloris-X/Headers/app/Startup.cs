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
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DashlorisX
{
    public static class Program
    {
	private static void ValidateProcess()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private static void ValidatePrivileges()
	{
	    try
	    {
		DashSys DashSys = new DashSys();

		if (!DashSys.IsPrivileged())
		{
		    DashBox DashBox = new DashBox();

		    var ContainerBColor = Color.FromArgb(9, 39, 66);
		    var MenuBarBColor = Color.FromArgb(19, 36, 64);
		    var AppBColor = Color.FromArgb(6, 17, 33);

		    int DialogResult = DashBox.Show("It seems like you have insufficient permissions.\r\n\r\nYou should run this application with elevated privileges, if you do not you may experience issues while running the application.\r\n\r\nAt this point you can either choose to press \'Yes\' to open up this application as administrator, or you can choose to press \'No\' which will close the application.", "Insufficient Privileges", AppBColor, MenuBarBColor, ContainerBColor, Color.White, Buttons: DashBox.Buttons.YesNo);

		    if (DialogResult == 1)
		    {
			using (Process process = new Process())
			{
			    var StartInfo = new ProcessStartInfo();

			    StartInfo.WorkingDirectory = Environment.CurrentDirectory;
			    StartInfo.FileName = DashSys.GetCurrentFileLocation();

			    StartInfo.UseShellExecute = true;
			    StartInfo.Verb = "runas";

			    process.StartInfo = StartInfo;
			    process.Start();
			}
		    }

		    Environment.Exit(-1);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private static void ShowToS()
	{
	    try
	    {
		new TOS().Show();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private static void DashlorisX()
	{
	    try
	    {
		new DashlorisX().ShowDialog();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	[STAThread]
	public static void Main()
	{
	    try
	    {
		ValidateProcess();
		ValidatePrivileges();
		ShowToS();

		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		DashlorisX();

		Application.Exit();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Fatal Error");
	    }
	}
    }
}
