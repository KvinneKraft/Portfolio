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
    public class Confirmation
    {
	private readonly DashControls Controls = new DashControls();
	private readonly DashTools Tools = new DashTools();

	public DashDialog DashDialog = new DashDialog();

	private void InitializeComponent()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		var AppBColor = Color.FromArgb(6, 17, 33);
		var AppTitle = string.Format("Dashloris-X   Confirm Configuration");
		var AppSize = new Size(300, 250);

		DashDialog.JustInitialize(AppSize, AppTitle, AppBColor, MenuBarBColor, StartPosition: FormStartPosition.CenterParent);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox BottomButtonContainer = new PictureBox();
	private readonly PictureBox BottomContainer = new PictureBox();

	private readonly Button Cancel = new Button();
	private readonly Button Accept = new Button();

	private static readonly DashNet DashNet = new DashNet();

	public static bool ValidateConfiguration()
	{
	    var Results = new List<bool>()
	    {
		DashNet.ConfirmInteger(DashlorisX.DurationTextBox.Text),
		DashNet.ConfirmBytes(DashlorisX.BytesTextBox.Text),
		DashNet.ConfirmPort(DashlorisX.PortTextBox.Text),
		DashNet.ConfirmIP(DashlorisX.HostTextBox.Text),
	    };
	    
	    foreach (bool Result in Results)
	    {
		if (!Result)
		{
		    return false;
		}
	    }

	    if (DashlorisX.BlockDomains && !DashNet.IsAllowedDomain(DashlorisX.HostTextBox.Text))
	    {
		return false;
	    }

	    return true;
	}

	public static PowerPoint PowPow = new PowerPoint();

	private void InitializeBottomBar()
	{
	    var BContainerSize = new Size(DashDialog.Width, 40);
	    var BContainerLocation = new Point(0, DashDialog.Height - BContainerSize.Height);

	    var BBContainerSize = new Size(210, 26);
	    var BBContainerLocation = new Point((BContainerSize.Width - BBContainerSize.Width) / 2, (BContainerSize.Height - BBContainerSize.Height) / 2);

	    var ContainerBColor = DashDialog.MenuBar.Bar.BackColor;

	    try
	    {
		Controls.Image(BottomContainer, BottomButtonContainer, BBContainerSize, BBContainerLocation, ContainerBColor);
		Controls.Image(DashDialog, BottomContainer, BContainerSize, BContainerLocation, ContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var ButtonSize = new Size(100, 26);
	    var ButtonBColor = ContainerBColor;
	    var ButtonFColor = Color.White;

	    var AcceptLocation = new Point(ButtonSize.Width + 10, 0);
	    var CancelLocation = new Point(0, 0);

	    try
	    {
		Controls.Button(BottomButtonContainer, Accept, ButtonSize, AcceptLocation, ButtonBColor, ButtonFColor, 1, 10, "Accept");
		Controls.Button(BottomButtonContainer, Cancel, ButtonSize, CancelLocation, ButtonBColor, ButtonFColor, 1, 10, "Cancel");

		Accept.Click += (s, e) =>
		{
		    if (ValidateConfiguration())
		    {
			DashlorisX.Launch.Text = "Flooding ....";

			PowPow.StartAttack();
			PowPow.StopAttack();

			DashDialog.Hide();
		    }

		    else
		    {
			DashDialog.Hide();
		    }
		};

		Cancel.Click += (s, e) =>
		{
		    DashDialog.Hide();
		};

		foreach (Button button in BottomButtonContainer.Controls)
		{
		    Tools.Round(button, 6);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox MainContainer = new PictureBox();
	private readonly TextBox TextContainer = new TextBox();

	private void InitializeContainer()
	{
	    var MContainerSize = new Size(DashDialog.Width - 22, DashDialog.Height - BottomContainer.Height - DashDialog.MenuBar.Bar.Height - 20);
	    var MContainerLocation = new Point(11, 10 + DashDialog.MenuBar.Bar.Height);

	    var TContainerSize = new Size(MContainerSize.Width - 8, MContainerSize.Height - 8);
	    var TContainerLocation = new Point(4, 4);
	    var TContainerFColor = Color.White;

	    var ContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Controls.TextBox(MainContainer, TextContainer, TContainerSize, TContainerLocation, ContainerBColor, TContainerFColor, 1, 9, FixedSize: false, Multiline: true, ScrollBar:true, ReadOnly:true);
		Controls.Image(DashDialog, MainContainer, MContainerSize, MContainerLocation, ContainerBColor);

		Tools.Round(MainContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void InitializeEvents()
	{
	    DashDialog.VisibleChanged += (s, e) =>
	    {
		if (DashDialog.Visible)
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

	private bool DoInitialize = true;

	public void Show()
	{
	    try
	    {
		if (DoInitialize)
		{
		    InitializeComponent();
		    InitializeBottomBar();
		    InitializeContainer();
		    InitializeEvents();

		    DoInitialize = false;
		}
		
		DashDialog.ShowAsIs(ShowDialog:true);
		MessageBox.Show("!");
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
