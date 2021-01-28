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
    public class AttackLog 
    {
	private readonly DashControls Controls = new DashControls();
	private readonly DashTools Tools = new DashTools();

	public readonly DashDialog DashDialog = new DashDialog();

	private void InitializeComponent()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		var AppBColor = Color.FromArgb(6, 17, 33);
		var AppTitle = string.Format("Dashloris-X   Attack Log");
		var AppSize = new Size(325, 250);

		DashDialog.JustInitialize(AppSize, AppTitle, AppBColor, MenuBarBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox BottomBarContainer = new PictureBox();
	private readonly PictureBox BottomBar = new PictureBox();

	private readonly Button Clear = new Button();// Clear
	public readonly Button Stop = new Button();// Close

	private void InitializeBottomBar()
	{
	    var ContainerSize = new Size(DashDialog.Width - 2, 28);
	    var ContainerLocation = new Point(1, DashDialog.Height - ContainerSize.Height);
	    var ContainerBColor = DashDialog.MenuBar.Bar.BackColor;

	    try
	    {
		Controls.Image(DashDialog, BottomBar, ContainerSize, ContainerLocation, ContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var BContainerSize = new Size(180, 26);
	    var BContainerLocation = new Point((ContainerSize.Width - BContainerSize.Width) / 2, (ContainerSize.Height - BContainerSize.Height) / 2);
	    var BContainerBColor = ContainerBColor;

	    try
	    {
		Controls.Image(BottomBar, BottomBarContainer, BContainerSize, BContainerLocation, BContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var ButtonSize = new Size(85, 26);
	    var ButtonBColor = BContainerBColor;
	    var ButtonFColor = Color.White;

	    var CloseLocation = new Point(ButtonSize.Width + 10, 0);
	    var ClearLocation = new Point(0, 0);

	    try
	    {
		Controls.Button(BottomBarContainer, Clear, ButtonSize, ClearLocation, ButtonBColor, ButtonFColor, 1, 10, "Clear");

		Clear.Click += (s, e) =>
		{
		    TextLog.Clear();
		};

		Controls.Button(BottomBarContainer, Stop, ButtonSize, CloseLocation, ButtonBColor, ButtonFColor, 1, 10, "Stop");

		Stop.Click += (s, e) =>
		{
		    if (Stop.Text != "Stopping ....")
		    {
			Stop.Text = "Stopping ....";

			Confirmation.PowPow.StopAttack();
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox InnerTextContainer = new PictureBox();
	private readonly PictureBox TextContainer = new PictureBox();

	private static string GetFormat()
	{
	    return string.Format
	    (
		"(+)>  When stopping the flood, please wait at least 10 seconds before sending another wave, because it may take a minute to take down every single connection estabilished.\r\n\r\n"	  
    	    );
	}

	public readonly TextBox TextLog = new TextBox() { Text = GetFormat() };

	private void InitializeMainContainer()
	{
	    var ContainerSize = new Size(DashDialog.Width - 22, DashDialog.Height - DashDialog.MenuBar.Bar.Height - BottomBar.Height - 21);
	    var ContainerLocation = new Point(11, DashDialog.MenuBar.Bar.Height + DashDialog.MenuBar.Bar.Top + 10);
	    var ContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Controls.Image(DashDialog, TextContainer, ContainerSize, ContainerLocation, ContainerBColor);
		Tools.Round(TextContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var IContainerSize = new Size(ContainerSize.Width - 14, ContainerSize.Height - 14);
	    var IContainerLocation = new Point(7, 7);
	    var IContainerBColor = ContainerBColor;

	    try
	    {
		Controls.Image(TextContainer, InnerTextContainer, IContainerSize, IContainerLocation, IContainerBColor);
		Tools.Round(InnerTextContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var TextBoxSize = IContainerSize;
	    var TextBoxLocation = new Point(0, 0);
	    var TextBoxBColor = ContainerBColor;
	    var TextBoxFColor = Color.White;

	    try
	    {
		Controls.TextBox(InnerTextContainer, TextLog, TextBoxSize, TextBoxLocation, TextBoxBColor, TextBoxFColor, 1, 8, ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Hide()
	{
	    DashDialog.Invoke
	    (
		new MethodInvoker
		(
		    delegate () 
		    {
			DashDialog.Visible = false;
		    }
		)
	    );
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
		    InitializeMainContainer();

		    DoInitialize = false;
		}

		DashDialog.ShowAsIs(ShowDialog:true);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
