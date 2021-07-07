using System;
using System.Drawing;
using System.Windows.Forms;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace HighPlayer
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
		    Size AppSize = new Size(475, 400);

		    string AppTitle = ("Dashie's Lovely High Music Helper");

		    Color AppMCol = Color.FromArgb(8, 34, 46);
		    Color AppBCol = AppMCol;

		    dashWindow.InitializeWindow(AppSize, AppTitle, AppBCol, AppMCol, barClose: false);

		    dashWindow.values.setTitleSize(10);
		    dashWindow.values.ResizeTitle(12);
		    dashWindow.values.CenterTitle();

		    dashWindow.values.onControlClick(1, () => Environment.Exit(-1));
		    dashWindow.FormClosing += (s, e) => Environment.Exit(-1);

		    MainGUI mainGUI = new MainGUI();

		    mainGUI.Initiator(dashWindow);

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
