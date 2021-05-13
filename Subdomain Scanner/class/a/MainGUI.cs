
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

namespace SubdomainAnalyzer
{
    public partial class MainGUI
    {
	Exception GetExep(Exception E) =>
	    ErrorHandler.GetException(E);


	readonly ControlHelper ConHelp = new ControlHelper();

	void InitConHelper()
	{
	    try
	    {
		ConHelp.TextBoxParent = ContainerA2;
		ConHelp.LabelParent = ContainerA2;

		ConHelp.TextBoxBCol = Color.FromArgb(41, 44, 89);
		ConHelp.TextBoxFCol = Color.White;

		ConHelp.LabelBCol = ContainerA1.BackColor;
		ConHelp.LabelFCol = Color.White;

		ConHelp.FontID = 0;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly TextBox TextBoxA1 = new TextBox();
	readonly TextBox TextBoxA2 = new TextBox();
	readonly TextBox TextBoxA3 = new TextBox();
	readonly TextBox TextBoxA4 = new TextBox();

	readonly Label LabelA1 = new Label();
	readonly Label LabelA2 = new Label();
	readonly Label LabelA3 = new Label();
	readonly Label LabelA4 = new Label();
	readonly Label LabelA5 = new Label();
	readonly Label LabelA6 = new Label();

	void InitA2()
	{
	    try
	    {
		InitConHelper();

		var Lab1Text = ("Website:");
		var Lab1Size = ConHelp.GetFontSize(Lab1Text);
		var Lab1Loca = new Point(0, 0);

		var Tex1Text = ("pugpawz.com");
		var Tex1Size = new Size(135, 20);
		var Tex1Loca = ConHelp.ControlX(Lab1Size, Lab1Loca, Extra: 0);

		var Lab2Text = ("Ports:");
		var Lab2Size = ConHelp.GetFontSize(Lab2Text);
		var Lab2Loca = ConHelp.ControlX(Tex1Size, Tex1Loca);

		var Tex2Text = ("80,443");
		var Tex2Size = ConHelp.TextBoxSize(Lab2Size, Lab2Loca);
		var Tex2Loca = ConHelp.ControlX(Lab2Size, Lab2Loca, Extra: 0);

		ConHelp.AddTextBox(TextBoxA1, Tex1Size, Tex1Loca, Tex1Text);
		ConHelp.AddTextBox(TextBoxA2, Tex2Size, Tex2Loca, Tex2Text);

		ConHelp.AddLabel(LabelA1, Lab1Size, Lab1Loca, Lab1Text);
		ConHelp.AddLabel(LabelA2, Lab2Size, Lab2Loca, Lab2Text);

		var Lab3Text = ("Sub Domain List:");
		var Lab3Size = ConHelp.GetFontSize(Lab3Text);
		var Lab3Loca = new Point(0, Lab2Loca.Y + Lab2Size.Height + 8);

		var Tex3Text = ($@"C:\Users\{Environment.UserName}\Desktop\sub domains.txt");
		var Tex3Size = ConHelp.TextBoxSize(Lab3Size, Lab3Loca);
		var Tex3Loca = ConHelp.ControlX(Lab3Size, Lab3Loca, Extra: 0);

		var Lab4Text = ("Connect Timeout:");
		var Lab4Size = ConHelp.GetFontSize(Lab4Text);
		var Lab4Loca = new Point(0, Lab3Loca.Y + Lab3Size.Height + 8);

		var Tex4Text = ("400");
		var Tex4Size = new Size(45, 20);
		var Tex4Loca = ConHelp.ControlX(Lab4Size, Lab4Loca, Extra: 0);

		ConHelp.AddTextBox(TextBoxA3, Tex3Size, Tex3Loca, Tex3Text);
		ConHelp.AddTextBox(TextBoxA4, Tex4Size, Tex4Loca, Tex4Text);

		ConHelp.AddLabel(LabelA3, Lab3Size, Lab3Loca, Lab3Text);
		ConHelp.AddLabel(LabelA4, Lab4Size, Lab4Loca, Lab4Text);

		var Lab5Text = ("Verbose:");
		var Lab5Size = ConHelp.GetFontSize(Lab5Text);
		var Lab5Loca = ConHelp.ControlX(Tex4Size, Tex4Loca);

		var Lab6Text = ("false");
		var Lab6Size = ConHelp.TextBoxSize(Lab5Size, Lab5Loca, 18);
		var Lab6Loca = ConHelp.ControlX(Lab5Size, Lab5Loca, Y: Lab5Loca.Y + 1, Extra: 0);

		ConHelp.AddLabel(LabelA5, Lab5Size, Lab5Loca, Lab5Text);
		ConHelp.LabelBCol = ConHelp.TextBoxBCol;

		ConHelp.AddLabel(LabelA6, Lab6Size, Lab6Loca, Lab6Text, 9);
		LabelA6.TextAlign = ContentAlignment.MiddleCenter;
		
		foreach (Control a in ContainerA2.Controls)
		{
		    if (a is PictureBox)
		    {
			Tools.Round(a, 6);
		    }
		}

		//+ Shortcut Key for Sub Domain List
		//+ Shortcut Key for loading default Sub Domain List
		//+ 
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
		var Cont1Size = new Size(Capsule.Width, 93);
		var Cont1Loca = new Point(0, 10);
		var Cont1BCol = Color.FromArgb(112, 74, 125);//(127, 219, 136);//100, 161, 106);

		var Cont2Size = new Size(Cont1Size.Width - 20, 73);
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

		Controls.TextBox(ContainerB3, TextBoxB1, TextBoxSize, TextBoxLoca, ContainerA2.BackColor, Color.White, 1, 8, 
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

		DashApp.InitializeWindow(AppSize, ("Subdomain Connector"), AppABCo, AppMBCo, CloseHideApp:false);

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
