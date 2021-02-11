
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DNSChangerX
{
    public class InitDnsList
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	public readonly DashDialog DashDialog = new DashDialog();

	public void CoreComponent()
	{
	    try
	    {
		var MenuBarBCol = Color.FromArgb(14, 0, 57);
		var AppBCol = Color.FromArgb(36, 1, 112);
		var AppSize = new Size(250, 250);

		DashDialog.JustInitialize(AppSize, ("DNS List  -  1.0"), AppBCol, MenuBarBCol);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox DisplayContainer1 = new PictureBox();
	private readonly PictureBox DisplayContainer2 = new PictureBox();
	private readonly PictureBox DisplayContainer3 = new PictureBox();

	private readonly Dictionary<int, TextBox> DisplayTextBoxes = new Dictionary<int, TextBox>();
	private readonly Dictionary<int, Button> DisplayButtons = new Dictionary<int, Button>();

	public void DisplayComponent()
	{
	    try
	    {
		// DNS Servers from embedded file auto add control thing. [<provider>,<ip1>,<ip2>]
		// Click = Copy
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox BottomContainer1 = new PictureBox();
	private readonly Button BottomButton1 = new Button();

	public void BottomComponent()
	{
	    try
	    {
		var ContainerSize = new Size(DashDialog.Width, 28);
		var ContainerLoca = new Point(0, DashDialog.Height - 28);
		var ContainerBCol = DashDialog.MenuBar.Bar.BackColor;

		Control.Image(DashDialog, BottomContainer1, ContainerSize, ContainerLoca, ContainerBCol);

		var ButtonSize = new Size(85, 24);
		var ButtonLoca = new Point((ContainerSize.Width - 85) / 2, (ContainerSize.Height - 24) / 2);
		var ButtonFCol = Color.White;

		Control.Button(BottomContainer1, BottomButton1, ButtonSize, ButtonLoca, ContainerBCol, ButtonFCol, 1, 9, "Done");

		BottomButton1.Click += (s, e) =>
		{
		    DashDialog.Hide();
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}