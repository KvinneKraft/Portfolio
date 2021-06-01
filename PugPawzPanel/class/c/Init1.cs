using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace DashApplication
{
    public class Init1
    {
	readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();

	PictureBox Frame = null;
	DashWindow App = null;
	

	void HideWindows()
	{
	    foreach (Control c1 in Frame.Controls)
	    {
		c1.Hide();
	    }
	}

	void DefaultHookProcedure(object obj1, object obj2)
	{
	    HideWindows();

	    if (obj1 == null)
	    {
		obj1 = obj2;
	    }
	}


	PageB pageB = null;
	void Hook1()
	{
	    try
	    {
		DefaultHookProcedure(pageB, new PageB(App, Frame));
		// SQL Query
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	PageC pageC = null;
	void Hook2()
	{
	    try
	    {
		DefaultHookProcedure(pageC, new PageC(App, Frame));
		// Insert Product
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	PageD pageD = null;
	void Hook3()
	{
	    try
	    {
		DefaultHookProcedure(pageD, new PageD(App, Frame));
		// Insert Blog Post
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	
	void Hook4()
	{
	    try
	    {
		// Launch FileZilla -- get url from config
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	
	void Hook5()
	{
	    try
	    {
		// Launch Control Panel -- get url from config
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	PageA pageA = null;
	void Hook6()
	{
	    try
	    {
		DefaultHookProcedure(pageA, new PageA(App, Frame));


	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly PictureBox Container1 = new PictureBox();

	readonly Button Button1 = new Button();
	readonly Button Button2 = new Button();
	readonly Button Button3 = new Button();
	readonly Button Button4 = new Button();
	readonly Button Button5 = new Button();
	readonly Button Button6 = new Button();

	delegate void ButtonEventHolder();

	void S1()
	{
	    try
	    {
		var Cont1Size = new Size(315, Frame.Height - 10);
		var Cont1Loca = new Point(-2, 5);
		var Cont1BCol = Frame.BackColor;

		Controls.Image(Frame, Container1, Cont1Size, Cont1Loca, Cont1BCol);

		var ButtonSize = new Size(45, Cont1Size.Height);
		var ButtonBCol = Frame.BackColor;
		var ButtonFCol = Color.White;

		void AddButton(Button button, Point loca, string text, ButtonEventHolder execute)
		{
		    Controls.Button(Container1, button, ButtonSize, loca, ButtonBCol, ButtonFCol, 1, 7, (text));

		    button.Click += (s, e) =>
		    {
			foreach (Button butn in Container1.Controls)
			{
			    butn.BackColor = Frame.BackColor;
			}

			button.BackColor = Color.FromArgb(40, 51, 66);

			execute();
		    };

		    Tools.Round(button, 6);
		}

		var Button1Loca = new Point(  0, 0);
		var Button2Loca = new Point( 55, 0);
		var Button3Loca = new Point(110, 0);
		var Button4Loca = new Point(165, 0);
		var Button5Loca = new Point(215, 0);
		var Button6Loca = new Point(270, 0);

		AddButton(Button1, Button1Loca, (@"< / >"),  () => Hook1());
		AddButton(Button2, Button2Loca, (@"I-App"),  () => Hook2());
		AddButton(Button3, Button3Loca, (@"I-Blog"), () => Hook3());
		AddButton(Button4, Button4Loca, (@"FTP://"), () => Hook4());
		AddButton(Button5, Button5Loca, (@":/Ctrl"), () => Hook5());
		AddButton(Button6, Button6Loca, (@"Hey!"),   () => Hook6());

		Button6.BackColor = Color.FromArgb(40, 51, 66);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void SecondaryInitiate(DashWindow app, PictureBox frame)
	{
	    try
	    {
		Frame = frame;
		App = app;

		S1();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
