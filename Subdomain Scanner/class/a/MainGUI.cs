
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

	readonly Label LabelA1 = new Label();
	readonly Label LabelA2 = new Label();
	readonly Label LabelA3 = new Label();
	readonly Label LabelA4 = new Label();
	readonly Label LabelA5 = new Label();
	
	void InitA2()
	{
	    try
	    {
		// Add TextBoxes
		// Add Labels
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly PictureBox ContainerA1 = new PictureBox();
	readonly PictureBox ContainerA2 = new PictureBox();

	Size CapsuleSize(int NWidth, int Height) =>
	    new Size(Capsule.Width - NWidth, Height);

	void InitA()
	{
	    try
	    {
		var Cont1Size = new Size(Capsule.Width, 115);
		var Cont1Loca = new Point(0, 10);
		var Cont1BCol = Color.FromArgb(88, 184, 142);//(127, 219, 136);//100, 161, 106);

		var Cont2Size = new Size(Cont1Size.Width - 20, 95);
		var Cont2Loca = new Point(10, 10);

		Controls.Image(ContainerA1, ContainerA2, Cont2Size, Cont2Loca, Cont1BCol);
		Controls.Image(Capsule, ContainerA1, Cont1Size, Cont1Loca, Cont1BCol);

		InitA2();

		Tools.Round(ContainerA1, 6);
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly TextBox TextBoxB1 = new TextBox();

	void SendLog(string Text) =>
	    TextBoxB1.AppendText($"{Text}\r\n");

	void InitB2()
	{
	    try
	    {
		var TextBoxSize = ContainerB2.Size;
		var TextBoxLoca = new Point(0, 0);
		//var TextBoxBCol = Color.FromArgb(100, 161, 106);

		Controls.TextBox(ContainerB3, TextBoxB1, TextBoxSize, TextBoxLoca, ContainerA2.BackColor, Color.Black, 1, 8, 
		    ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

		SendLog("(!)  Hey there!  Press \'F1\' for more options.  I did not take the time to add buttons but rather shortcut keys.");
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly PictureBox ContainerB1 = new PictureBox();
	readonly PictureBox ContainerB2 = new PictureBox();
	readonly PictureBox ContainerB3 = new PictureBox();

	void InitB()
	{
	    try
	    {
		int ContHeight = (Capsule.Height - (ContainerA1.Height + ContainerA1.Top + 10));

		var Cont1Loca = new Point(0, ContainerA1.Height + ContainerA1.Top + 10);
		var Cont1Size = new Size(Capsule.Width, ContHeight);
		var Cont1BCol = ContainerA1.BackColor;

		var Cont2Size = new Size(Cont1Size.Width - 14, Cont1Size.Height  - 14);
		var Cont2Loca = new Point(7, 7);

		var Cont3Loca = new Point(0, 0);

		Controls.Image(ContainerB1, ContainerB2, Cont2Size, Cont2Loca, Cont1BCol);
		Controls.Image(ContainerB2, ContainerB3, Cont2Size, Cont3Loca, Cont1BCol);
		Controls.Image(Capsule, ContainerB1, Cont1Size, Cont1Loca, Cont1BCol);

		InitB2();

		Tools.Round(ContainerB1, 6);

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

	readonly PictureBox Capsule = new PictureBox();

	public MainGUI()
	{
	    try
	    {
		var AppABCo = Color.FromArgb(243, 255, 189);
		var AppMBCo = Color.FromArgb(1, 93, 145);
		var AppFGCo = Color.White;
		var AppSize = new Size(350, 325);

		DashApp.InitializeWindow(AppSize, ("Subdomain Scanner"), AppABCo, AppMBCo, CloseHideApp:false);

		DashApp.MenuBar.LogoLayer1.Top -= 3;
		DashApp.MenuBar.LogoLayer2.Top -= 3;

		DashApp.MenuBar.LogoLayer1.Left = 3;
		DashApp.MenuBar.LogoLayer2.Left = 3;

		DashApp.FormClosing += (s, e) =>
		{
		    Environment.Exit(-1);
		};

		var CapsuleSize = new Size(DashApp.Width - 25, DashApp.Height - 38);
		var CapsuleLoca = new Point(13, 26);

		Controls.Image(DashApp, Capsule, CapsuleSize, CapsuleLoca, AppABCo);

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
