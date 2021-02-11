
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DNSChangerX
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    DashSys DashSys = new DashSys();

	    if (!DashSys.IsPrivileged())
	    {
		DashBox DashBox = new DashBox();

		var ContainerBCol = Color.FromArgb(2, 55, 110);
		var MenuBarBCol = Color.FromArgb(14, 0, 57);
		var AppBCol = Color.FromArgb(36, 1, 112);
		var AppSize = new Size(334, 180);

		int result = DashBox.Show("In order to make use of this application you must run this application as an elevated user, preferably as an administrator.\r\n\r\nI am sorry for this inconvience but Windows requires you to be an elevated user in order to change the configuration you can change using this application unless setup differently.\r\n\r\nYou can either press Yes to restart this application as an elevated user or press No to exit the application.", "Insufficient Privileges", AppBCol, MenuBarBCol, ContainerBCol, Color.White, DashBox.Buttons.YesNo);

		if (result == 1)
		{
		    using (Process Process = new Process())
		    {
			Process.StartInfo = new ProcessStartInfo()
			{
			    FileName = DashSys.GetCurrentFileLocation(),
			    UseShellExecute = true,
			    Verb = "runas"
			};

			Process.Start();
		    }
		}
	    }

	    else
	    {
		new DNSChangerX().Show();
	    }

	    Application.ExitThread();
	    Application.Exit();
	}
    }
}
