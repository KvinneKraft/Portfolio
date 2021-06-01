﻿// Author: Dashie
// Version: 1.0


using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace DashApplication
{
    public class MainGUI
    {
	DashControls Controls = new DashControls();
	DashTools Tools = new DashTools();

	DashWindow App = null;


	readonly PictureBox ContA1 = new PictureBox();

	void Init1()
	{
	    try
	    {
		var Cont1Size = new Size(App.Width, 38);
		var Cont1Loca = new Point(0, App.Bottom - 38);
		var Cont1BCol = App.values.getBarColor();

		Controls.Image(App, ContA1, Cont1Size, Cont1Loca, Cont1BCol);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly public PictureBox ContB1 = new PictureBox();
    
	readonly Init1 initiate1 = new Init1();
	readonly Init2 initiate2 = new Init2();
	readonly Init3 initiate3 = new Init3();

	void Init2()
	{
	    try
	    {
		int MainFrameHeight = App.Height - App.values.Height() - ContA1.Height; // Recode MenuBar patterns and add methods for obtainable values.
		int MainFrameWidth = App.Width - 4;

		var Cont1Size = new Size(MainFrameWidth, MainFrameHeight);
		var Cont1Loca = new Point(2, App.values.Height());
		var Cont1BCol = App.BackColor;

		Controls.Image(App, ContB1, Cont1Size, Cont1Loca, Cont1BCol);

		initiate1.SecondaryInitiate(App, ContA1);
		initiate2.SecondaryInitiate(App, ContB1);
		initiate3.SecondaryInitiate(App, ContB1);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void Initiator(DashWindow inst)
	{
	    try
	    {
		App = inst;

		Init1();
		Init2();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}
    }
}
