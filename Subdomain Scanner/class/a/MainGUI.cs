
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace SubdomainScanner
{
    public partial class MainGUI
    {
	Exception GetExep(Exception E) =>
	    ErrorHandler.GetException(E);


	readonly TextBox TextBoxA1 = new TextBox();
	readonly TextBox TextBoxA2 = new TextBox();
	readonly TextBox TextBoxA3 = new TextBox();
	readonly TextBox TextBoxA4 = new TextBox();

	readonly Button ButtonA1 = new Button();

	void ATextBoxes()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly Label LabelA1 = new Label();
	readonly Label LabelA2 = new Label();
	readonly Label LabelA3 = new Label();
	readonly Label LabelA4 = new Label();
	readonly Label LabelA5 = new Label();
	
	void ALabels()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}

	void InitA()
	{
	    try
	    {
		ATextBoxes();
		ALabels();
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly PictureBox ContainerB1 = new PictureBox();
	readonly PictureBox ContainerB2 = new PictureBox();

	readonly TextBox TextBoxB1 = new TextBox();

	void InitB()
	{
	    try
	    {
		// Shortcut Key
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	public readonly DashWindow DashApp = new DashWindow();

	readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();

	public MainGUI()
	{
	    try
	    {
		var AppABCo = Color.FromArgb(243, 255, 189);
		var AppMBCo = Color.FromArgb(1, 93, 145);
		var AppFGCo = Color.White;
		var AppSize = new Size(300, 350);

		DashApp.InitializeWindow(AppSize, ("Subdomain Scanner"), AppABCo, AppMBCo, CloseHideApp:false);

		DashApp.MenuBar.LogoLayer1.Top -= 3;
		DashApp.MenuBar.LogoLayer2.Top -= 3;

		DashApp.MenuBar.LogoLayer1.Left = 3;
		DashApp.MenuBar.LogoLayer2.Left = 3;

		DashApp.FormClosing += (s, e) =>
		{
		    Environment.Exit(-1);
		};

		InitA();
		InitB();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
