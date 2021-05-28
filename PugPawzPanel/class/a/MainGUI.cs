// Author: Dashie
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


	readonly PictureBox ContB1 = new PictureBox();

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

		var runnable = new DashFramework.Runnables.Runnable();

		runnable.RunTaskLater
		(
		    inst, 
		    
		    () => 
		    {
			MessageBox.Show("Hey!");
		    }, 
		    
		    5000
		);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}
    }
}
