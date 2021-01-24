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

	    StartPosition = FormStartPosition.CenterParent;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Confirm Configuration";
	    Tag = "DashlorisX Confirm Configuration";
	    Name = "Settings";

	    Icon = Resources.ICON;

	    BackColor = Color.FromArgb(6, 17, 33);//MidnightBlue;

	    Tools.Round(this, 6);
	    ResumeLayout(false);
	}

	readonly static DashNet DashNet = new DashNet();

	public static bool ValidateConfiguration()
	{
	    if (DashNet.ConfirmInteger(DashlorisX.BytesTextBox.Text) && DashNet.ConfirmInteger(DashlorisX.DurationTextBox.Text))
	    {
		if (DashNet.ConfirmIP(DashlorisX.HostTextBox.Text) && DashNet.ConfirmPort(DashlorisX.PortTextBox.Text))
		{
		    if (DashlorisX.BlockDomains && !DashNet.IsAllowedDomain(DashlorisX.HostTextBox.Text))
		    {
			return false;
		    }

		    return true;
		}

		return false;
	    }

	    MessageBox.Show("The bytes specified or the duration specified was found the be invalid.  Please correct this and then retry.", "Integer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

	    return false;
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

	readonly public PowerPoint PowPow = new PowerPoint();

	readonly Button Cancel = new Button();
	readonly Button Accept = new Button();

	public readonly List<Thread> workers = new List<Thread>();

	private void InitializeBottomBar()
	{
	    var BContainerSize = new Size(Width, 40);
	    var BContainerLocation = new Point(0, Height - BContainerSize.Height);

	    var BBContainerSize = new Size(210, 26);
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

	    var ButtonSize = new Size(100, 26);
	    var ButtonBColor = ContainerBColor;
	    var ButtonFColor = Color.White;

	    var AcceptLocation = new Point(ButtonSize.Width + 10, 0);
	    var CancelLocation = new Point(0, 0);

	    try
	    {
		Controls.Button(BottomButtonContainer, Accept, ButtonSize, AcceptLocation, ButtonBColor, ButtonFColor, 1, 10, "Accept", Color.Empty);
		Controls.Button(BottomButtonContainer, Cancel, ButtonSize, CancelLocation, ButtonBColor, ButtonFColor, 1, 10, "Cancel", Color.Empty);

		Accept.Click += (s, e) =>
		{
		    if (ValidateConfiguration())
		    {
			DashlorisX.Launch.Text = "Stop Flooding";

			workers.Add(
			    new Thread(() =>
			    {
				PowPow.StartAttack();
				workers.Clear();
			    }
			));

			foreach (Thread worker in workers)
			{
			    worker.IsBackground = true;
			    worker.Start();
			}
		    }
		    
		    Hide();
		};

		Cancel.Click += (s, e) =>
		{
		    Hide();
		};

		foreach (Button button in BottomButtonContainer.Controls)
		{
		    Tools.Round(button, 6);
		}
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox MainContainer = new PictureBox();
	readonly TextBox TextContainer = new TextBox();

	private void InitializeContainer()
	{
	    var MContainerSize = new Size(Width - 22, Height - BottomContainer.Height - MenuBar.Bar.Height - 20);
	    var MContainerLocation = new Point(11, 10 + MenuBar.Bar.Height);

	    var TContainerSize = new Size(MContainerSize.Width - 8, MContainerSize.Height - 8);
	    var TContainerLocation = new Point(4, 4);
	    var TContainerFColor = Color.White;

	    var ContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Controls.TextBox(MainContainer, TextContainer, TContainerSize, TContainerLocation, ContainerBColor, TContainerFColor, 1, 9, Color.Empty, FIXEDSIZE: false, MULTILINE: true, SCROLLBAR:true, READONLY:true);
		Controls.Image(this, MainContainer, MContainerSize, MContainerLocation, null, ContainerBColor);

		Tools.Round(MainContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void InitializeEvents()
	{
	    VisibleChanged += (s, e) =>
	    {
		if (Visible)
		{
		    TextContainer.Text = string.Format(
			$"\r\nCurrent Configuration:\r\n" +
			$"-=====================-\r\n" +
			$"$ Method: {Settings.MethodBox.Text}\r\n" +
			$"$ HTTP Version: {Settings.HTTPvBox.Text}\r\n" +
			$"$ User-Agent: {Settings.UserAgentBox.Text}\r\n" +
			$"$ Cookie(s): {Settings.CookieBox.Text}\r\n" +
			$"$ Host: {DashlorisX.HostTextBox.Text}\r\n" +
			$"$ Port: {DashlorisX.PortTextBox.Text}\r\n" +
			$"$ Duration: {DashlorisX.DurationTextBox.Text} seconds\r\n" +
			$"$ Packet Size: {DashlorisX.BytesTextBox.Text} bytes\r\n" +
			$"-=====================-\r\n" +
			$"Click Accept to launch the attack!\r\n"
		    );
		}
	    };
	}

	public Confirmation()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeBottomBar();
		InitializeContainer();
		InitializeEvents();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
