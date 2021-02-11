
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Reflection;
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

		DashDialog.JustInitialize(AppSize, ("Free DNS List  -  1.0"), AppBCol, MenuBarBCol);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly Dictionary<int, TextBox> DisplayTextBoxes = new Dictionary<int, TextBox>();
	private readonly Dictionary<int, Label> DisplayLabels = new Dictionary<int, Label>();

	private readonly PictureBox DisplayContainer1 = new PictureBox();
	private readonly PictureBox DisplayContainer2 = new PictureBox();

	private readonly Form DisplayContainer3 = new Form();

	private void InitDisplay1()
	{
	    try
	    {
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		var Container1Size = new Size(DashDialog.Width - 20, DashDialog.Height - DashDialog.MenuBar.Bar.Height - 50); // 28 = BottomBar
		var Container1Loca = new Point(10, 11 + DashDialog.MenuBar.Bar.Height);

		var Container2Size = new Size(Container1Size.Width - 14, Container1Size.Height - 14);
		var Container2Loca = new Point(7, 7);

		DisplayContainer3.FormBorderStyle = FormBorderStyle.None;
		DisplayContainer3.TopLevel = false;

		DisplayContainer3.VerticalScroll.Enabled = true;
		DisplayContainer3.VerticalScroll.Visible = true;

		DisplayContainer3.BackColor = ContainerBCol;
		DisplayContainer3.Location = new Point(0, 0);
		DisplayContainer3.Size = Container2Size;

		DisplayContainer2.Controls.Add(DisplayContainer3);
		DisplayContainer3.Show();

		Control.Image(DisplayContainer1, DisplayContainer2, Container2Size, Container2Loca, ContainerBCol);
		Control.Image(DashDialog, DisplayContainer1, Container1Size, Container1Loca, ContainerBCol);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private List<string> GetStringData() =>
	    ((Properties.Resources.dnslist).Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList());

	private void InitDisplay2()
	{
	    try
	    {
		foreach (string line in GetStringData)
		{

		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void DisplayComponent()
	{
	    try
	    {
		InitDisplay1();
		InitDisplay2();
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