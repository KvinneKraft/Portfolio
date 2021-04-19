
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class InsufficientPermissions
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	private readonly PictureBox Container = new PictureBox();

	private readonly Button Button1 = new Button();
	private readonly Button Button2 = new Button();

	private readonly Label Label1 = new Label();

	private void LoadSubComponents(DashWindow DashWindow)
	{
	    try
	    {
		string message = "You are required to run this application as administrator.\r\n\r\nRestart this application with administrative rights?";
		
		var LabelSize = new Size(DashWindow.Width - 20, 95);
		var LabelLoca = new Point(10, DashWindow.MenuBar.MenuBar.Height + 10);

		Control.Label(DashWindow, Label1, LabelSize, LabelLoca, DashWindow.BackColor, Color.White, 1, 10, (message));

		Label1.TextAlign = ContentAlignment.MiddleCenter;

		var ContainerSize = new Size(180, 20);
		var ContainerLoca = new Point((DashWindow.Width - 200) / 2, LabelSize.Height + LabelLoca.Y + 15);
		var ContainerBCol = DashWindow.BackColor;

		Control.Image(DashWindow, Container, ContainerSize, ContainerLoca, ContainerBCol);
		
		var ButtonSize = new Size(85, 20);
		var ButtonFCol = Color.White;
		var ButtonBCol = DashWindow.MenuBar.MenuBar.BackColor; 

		var Button1Loca = new Point(0, 0);
		var Button2Loca = new Point(95, 0);

		Control.Button(Container, Button1, ButtonSize, Button1Loca, ButtonBCol, ButtonFCol, 1, 8, ("Yes"));
		Control.Button(Container, Button2, ButtonSize, Button2Loca, ButtonBCol, ButtonFCol, 1, 8, ("No"));

		Tool.Round(Button1, 6);
		Tool.Round(Button2, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public InsufficientPermissions()
	{
	    try
	    {
		using (DashWindow DashWindow = new DashWindow())
		{
		    var DashWindowSize = new Size();
		    var DashWindowBCol = Color.FromArgb(6, 14, 36);
		    var DashWindowMCol = Color.FromArgb(17, 38, 94);

		    DashWindow.InitializeWindow(DashWindowSize, ("Not administrator?"), DashWindowBCol, DashWindowMCol, CloseHideApp:false);

		    LoadSubComponents(DashWindow);

		    DashWindow.ShowAsIs();
		}

		Application.Exit();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }


    public class AlreadyRunning
    {

    }
}