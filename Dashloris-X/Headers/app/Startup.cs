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
	private static readonly DashBox DashBox = new DashBox();

	private static int ShowBox(string Message, string Title)
	{
	    var ContainerBColor = Color.FromArgb(9, 39, 66);
	    var MenuBarBColor = Color.FromArgb(19, 36, 64);
	    var AppBColor = Color.FromArgb(6, 17, 33);

	    return DashBox.Show(Message, Title, AppBColor, MenuBarBColor, ContainerBColor, Color.White, Buttons: DashBox.Buttons.YesNo);
	}

	private static void ValidateProcess()
	{
	    try
	    {
		var processName = Process.GetCurrentProcess().ProcessName;
		var currentProcess = Process.GetCurrentProcess();

		bool isProcessRunning()
		{
		    foreach (Process process in Process.GetProcesses())
		    {
			if (process.ProcessName.Contains(processName))
			{
			    if (process.Id != currentProcess.Id)
			    {
				return true;
			    }
			}
		    }

		    return false;
		}

		if (isProcessRunning())
		{
		    int DialogResult = ShowBox("It seems like this application is already opened.\r\n\r\nI would recommend you first close the other instance of this application before making use of this one.\r\n\r\nIf you do not, you may experience over-use which may cause your network to get exhausted.\r\n\r\nConnections may also not close properly when using multiple instances, please know what you are doing.\r\n\r\nIf you are sure and want to keep using this application, press \'Yes\' or if you are unsure, press \'No\' to terminate this process.", "Instance Already Running");

		    if (DialogResult == 2 || DialogResult == -1 || DialogResult == 0)
		    {
			Environment.Exit(-1);
		    }
		}
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
		    int DialogResult = ShowBox("It seems like you have insufficient permissions.\r\n\r\nYou should run this application with elevated privileges, if you do not you may experience issues while running the application.\r\n\r\nAt this point you can either choose to press \'Yes\' to open up this application as administrator, or you can choose to press \'No\' which will close the application.", "Insufficient Privileges");

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
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		ValidateProcess();
		ValidatePrivileges();
		ShowToS();
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
