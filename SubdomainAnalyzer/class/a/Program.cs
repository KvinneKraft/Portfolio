﻿using System;
using System.Windows.Forms;

namespace SubdomainAnalyzer
{
    static class Program
    {
	[STAThread] static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    MainGUI DashApp = new MainGUI();
	    
	    Application.Run(DashApp.DashApp);
	    Environment.Exit(-1);
	}
    }
}
