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
		    Size AppSize = new Size(475, 300);

		    string AppTitle = ("Dashie's Lovely High Music Helper");

		    Color AppMCol = Color.FromArgb(28, 37, 46);
		    Color AppBCol = Color.FromArgb(37, 48, 63);

		    dashWindow.InitializeWindow(AppSize, AppTitle, AppBCol, AppMCol, barClose: false);
		    dashWindow.values.setTitleSize(10);

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
