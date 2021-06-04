﻿using System;
using System.Drawing;
using System.Windows.Forms;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace GateHey
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    try
	    {
		using (DashWindow dashWindow = new DashWindow())
		{
		    Size AppSize = new Size(400, 400);

		    string AppTitle = ("GateHey");

		    Color AppMCol = Color.FromArgb(28, 37, 46);
		    Color AppBCol = Color.FromArgb(37, 48, 63);

		    dashWindow.InitializeWindow(AppSize, AppTitle, AppBCol, AppMCol, hideApp: false, roundRadius: 0);

		    MainGUI mainGUI = new MainGUI();
		    mainGUI.Initiator(dashWindow);

		    dashWindow.values.onControlClick(1, () => Environment.Exit(-1));
		    dashWindow.FormClosing += (s, e) => Environment.Exit(-1);

		    Application.Run(dashWindow);
		}
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
