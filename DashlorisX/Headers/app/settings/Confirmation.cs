// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

using DashlorisX.Properties;

namespace DashlorisX
{
    public class Confirmation : Form
    {
	new readonly DashControls Controls = new DashControls();

	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X   Confirm Configuration", minim:false);
	readonly DashTools Tools = new DashTools();

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(300, 250);
	    MinimumSize = new Size(300, 250);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Confirm Configuration";
	    Tag = "DashlorisX Confirm Configuration";
	    Name = "Settings";

	    Icon = Resources.ICON;

	    BackColor = Color.FromArgb(6, 17, 33);//MidnightBlue;

	    Tools.Round(this, 6);
	    ResumeLayout(false);
	}

	private void InitializeMenuBar()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		MenuBar.Add(this, 26, MenuBarBColor, MenuBarBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox BottomContainer = new PictureBox();
	readonly PictureBox BottomButtonContainer = new PictureBox();

	readonly Button Cancel = new Button();
	readonly Button Accept = new Button();

	private void InitializeBottomBar()
	{
	    var BContainerSize = new Size(Width, 40);
	    var BContainerLocation = new Point(0, Height - BContainerSize.Height);

	    var BBContainerSize = new Size(210, 24);
	    var BBContainerLocation = new Point((BContainerSize.Width - BBContainerSize.Width) / 2, (BContainerSize.Height - BBContainerSize.Height) / 2);

	    var ContainerBColor = MenuBar.Bar.BackColor;

	    try
	    {
		Controls.Image(BottomContainer, BottomButtonContainer, BBContainerSize, BBContainerLocation, null, ContainerBColor);
		Controls.Image(this, BottomContainer, BContainerSize, BContainerLocation, null, ContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var ButtonSize = new Size(100, 24);
	    var ButtonBColor = ContainerBColor;
	    var ButtonFColor = Color.White;

	    var AcceptLocation = new Point(ButtonSize.Width + 10, 0);
	    var CancelLocation = new Point(0, 0);

	    try
	    {
		Controls.Button(BottomButtonContainer, Accept, ButtonSize, AcceptLocation, ButtonBColor, ButtonFColor, 1, 10, "Accept", Color.Empty);
		Controls.Button(BottomButtonContainer, Cancel, ButtonSize, CancelLocation, ButtonBColor, ButtonFColor, 1, 10, "Cancel", Color.Empty);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    Cancel.Click += (s, e) =>
	    {
		Hide();
	    };

	    Accept.Click += (s, e) =>
	    {
		// Launch Attacc
	    };
	}

	readonly PictureBox MainContainer = new PictureBox();
	readonly TextBox TextContainer = new TextBox();

	private void InitializeContainer()
	{
	    var MContainerSize = new Size(Width - 20, Height - BottomContainer.Height - MenuBar.Bar.Height - 20);
	    var MContainerLocation = new Point(10, 10 + MenuBar.Bar.Height);

	    var TContainerSize = new Size(MContainerSize.Width - 8, MContainerSize.Height - 8);
	    var TContainerLocation = new Point(4, 4);
	    var TContainerFColor = Color.White;

	    var ContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Controls.TextBox(MainContainer, TextContainer, TContainerSize, TContainerLocation, ContainerBColor, TContainerFColor, 1, 9, Color.Empty, FIXEDSIZE: false, MULTILINE: true);
		Controls.Image(this, MainContainer, MContainerSize, MContainerLocation, null, ContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public Confirmation()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeBottomBar();
		InitializeContainer();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
