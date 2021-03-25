
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class InitThaDashlorisX : DashGlobe
    {
	public readonly DashWindow DashWindow = new DashWindow();

	private void Init1()
	{
	    try
	    {
		var DialogMCol = Color.FromArgb(17, 38, 94);
		var DialogBCol = Color.FromArgb(6, 14, 36);
		var DialogSize = new Size(400, 350);

		DashWindow.InitializeWindow(DialogSize, ("Tha Dashloris-X  -  3.0"), DialogBCol, DialogMCol);

		DashWindow.MenuBar.Button1.Click += (s, e) =>
		{
		    ExitApplication();
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S2Container1 = new PictureBox();//Bottom Bar : S2Container1 = Sector 2 Container 1
	private readonly PictureBox S2Container2 = new PictureBox();//Icon Container
	private readonly PictureBox S2Container3 = new PictureBox();//Icon Itself

	public void InitS2Events()
	{
	    try
	    {
		S2Container3.MouseClick += (s, e) =>
		{
		    if (!S3Container3.Visible)
		    {
			S3Container1.Show();
		    }

		    else
		    {
			S3Container1.Hide();
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly Button S2Button1 = new Button();

	private void Init2()
	{
	    try
	    {
		var Container1Size = new Size(DashWindow.Width, 34);
		var Container1Loca = new Point(0, DashWindow.Height - 34);
		var Container1BCol = DashWindow.MenuBar.MenuBar.BackColor;

		Control.Image(DashWindow, S2Container1, Container1Size, Container1Loca, Container1BCol);

		var Container2Size = new Size(26, 26);
		var Container2Loca = new Point(5, 4);

		Control.Image(S2Container1, S2Container2, Container2Size, Container2Loca, Color.Black);
		Tool.Round(S2Container2, 6);

		var Container3Loca = new Point(1, 1);
		var Container3Size = new Size(24, 24);

		Control.Image(S2Container2, S2Container3, Container3Size, Container3Loca, Container1BCol, Properties.Resources.MenuBarIcon);
		InitS2Events();

		var Button1Size = new Size(100, 24);
		var Button1Loca = new Point(Container1Size.Width - 105, 5);
		var Button1Cola = DashWindow.BackColor;//6, 14, 36

		Control.Button(S2Container1, S2Button1, Button1Size, Button1Loca, Button1Cola, Color.White, 1, 9, ("Launch Attack"));
		Tool.Round(S2Button1, 6);

		// Status Text in the middle of bar?
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();
	private readonly PictureBox S3Container3 = new PictureBox();
	private readonly PictureBox S3Image1 = new PictureBox();

	private readonly Button S3Button1 = new Button();
	private readonly Button S3Button2 = new Button();
	private readonly Button S3Button3 = new Button();
	private readonly Button S3Button4 = new Button();
	private readonly Button S3Button5 = new Button();

	private void S3HideContainers()
	{
	    try
	    {
		foreach (Control Control in Capsule.Controls)
		{
		    if (Control.Visible)
		    {
			Control.Hide();
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private delegate void PageLink();

	public void HideReference()
	{
	    try
	    {
		S3HideContainers();

		S3Class1.S3Settings.Hide();
		S3Class1.Show();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public readonly ServerPing S3Class3 = new ServerPing();
	public readonly PortScan S3Class2 = new PortScan();
	public readonly AppInfo S3Class4 = new AppInfo();
	public readonly AppToS S3Class5 = new AppToS();
	public readonly LockOn S3Class1 = new LockOn();

	private delegate void PageHandle(DashWindow DashWindow, PictureBox Capsule, InitThaDashlorisX Parent);

	private void InitS3Events()
	{
	    try
	    {
		var Initializers = new Dictionary<Button, PageHandle>()
		{
		    { S3Button1, S3Class1.InitializePage },
		    { S3Button2, S3Class2.InitializePage },
		    { S3Button3, S3Class3.InitializePage },
		    { S3Button4, S3Class4.InitializePage },
		    { S3Button5, S3Class5.InitializePage },
		};

		var Buttons = new List<(Button, string)>()//Tuple<Button, string>>()
		{
		    (S3Button1, "Target Info"),
		    (S3Button2, "Port Scanner"),
		    (S3Button3, "Server Ping"),
		    (S3Button4, "About App"),
		    (S3Button5, "The T.O.S"),
		};

		foreach (var Init in Initializers)
		{
		    Init.Key.Click += (s, e) =>
		    {
			S3HideContainers();
			
			Init.Value.Invoke(DashWindow, Capsule, this);

			S3Container1.Hide();
		    };
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly Label S3Label1 = new Label();

	private void Init3()
	{
	    try
	    {
		var Container1Size = new Size(125, DashWindow.Height - S2Container1.Height - DashWindow.MenuBar.MenuBar.Height);
		var Container1Loca = new Point(2, DashWindow.MenuBar.MenuBar.Height);
		var Container1BCol = S2Container1.BackColor;

		var RectLoca1 = new Point(Container1Size.Width - 1, 0);
		var RectLoca2 = new Point(RectLoca1.X, Container1Size.Height - 1);

		Control.Image(DashWindow, S3Container1, Container1Size, Container1Loca, Container1BCol);
		Tool.PaintLine(S3Container1, DashWindow.BackColor, 3, RectLoca1, RectLoca2);

		S3Container1.Hide();

		var Image1Imag = DashWindow.MenuBar.LogoLayer1.Image;
		var Image1Size = DashWindow.MenuBar.LogoLayer1.Size;
		var Image1Loca = new Point(DashWindow.MenuBar.LogoLayer1.Left - 2, -DashWindow.MenuBar.MenuBar.Height + DashWindow.MenuBar.LogoLayer1.Top);

		Control.Image(S3Container1, S3Image1, Image1Size, Image1Loca, Container1BCol, Image1Imag);

		var Label1Loca = new Point(11, 10);

		Control.Label(S3Container2, S3Label1, Size.Empty, Label1Loca, Container1BCol, Color.White, 1, 10, ("Dash Menu"));
		
		var Container2Size = new Size(Container1Size.Width - 20, 145);
		var Container2Loca = new Point(0, Label1Loca.Y + S3Label1.Height + 15);

		Control.Image(S3Container2, S3Container3, Container2Size, Container2Loca, Container1BCol);

		var Buttons = new List<(Button, string)>()
		{
		    (S3Button1, "Target Info"),
		    (S3Button2, "Port Scanner"),
		    (S3Button3, "Server Ping"),
		    (S3Button4, "About App"),
		    (S3Button5, "The T.O.S"),
		};

		var ButtonBCol = DashWindow.BackColor;
		var ButtonSize = new Size(105, 24);

		for (int k = 0, y = 0; k < Buttons.Count; k += 1, y = (5 * k) + (ButtonSize.Height * k))
		{
		    var ButtonObje = Buttons[k].Item1;
		    var ButtonLoca = new Point(0, y);
		    var ButtonText = Buttons[k].Item2;

		    Control.Button(S3Container3, ButtonObje, ButtonSize, ButtonLoca, ButtonBCol, Color.White, 1, 9, ButtonText);
		    Tool.Round(ButtonObje, 6);
		}

		var Container3Size = new Size(Container1Size.Width - 20, Container2Size.Height + Container2Loca.Y);
		var Container3Loca = new Point(10, 25);

		Control.Image(S3Container1, S3Container2, Container3Size, Container3Loca, Container1BCol);

		InitS3Events();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox Capsule = new PictureBox();


	private void Init4()
	{
	    try
	    {
		var ContainerSize = new Size(DashWindow.Width - 30, DashWindow.Height - DashWindow.MenuBar.MenuBar.Height - 30 - S2Container1.Height);
		var ContainerLoca = new Point(15, DashWindow.MenuBar.MenuBar.Height + 15);
		var ContainerBCol = DashWindow.BackColor;

		Control.Image(DashWindow, Capsule, ContainerSize, ContainerLoca, ContainerBCol);
		Tool.Round(Capsule, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void InitializeApp()
	{
	    try
	    { 
		Init1();//GUI
		Init2();//BottomBar
		Init3();//SideMenu
		Init4();//Capsule

		S3Container1.BringToFront();

		S3Class2.InitializePage(DashWindow, Capsule, this);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}