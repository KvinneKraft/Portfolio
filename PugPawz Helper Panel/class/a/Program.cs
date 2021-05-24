using System;
using System.Drawing;
using System.Windows.Forms;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace DashApplication
{
    static class Program
    {
	readonly static MainGUI mainGUI = new MainGUI();

	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    try
	    {
		using (DashWindow dashWindow = new DashWindow())
		{
		    Size AppSize = new Size(350, 350);

		    string AppTitle = ("Dash Application");

		    Color AppMCol = Color.CornflowerBlue;
		    Color AppBCol = Color.White;

		    dashWindow.InitializeWindow(AppSize, AppTitle, AppBCol, AppMCol, CloseHideApp: false);

		    dashWindow.MenuBar.Button1.Click += (s, e) =>
			Environment.Exit(-1);
		    dashWindow.FormClosing += (s, e) =>
			Environment.Exit(-1);

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
