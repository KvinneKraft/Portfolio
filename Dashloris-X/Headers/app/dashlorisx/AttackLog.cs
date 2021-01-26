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
    public class AttackLog : Form
    {
	new readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(325, 250);
	    MinimumSize = new Size(325, 250);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Attack Log";
	    Tag = "DashlorisX Attack Log";
	    Name = "Attack Log";

	    Icon = Resources.ICON;

	    ResumeLayout(false);
	}

	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X  Attack Log", minim: false);

	private void InitializeMenuBar()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		MenuBar.Add(this, 26, MenuBarBColor, MenuBarBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void InitializeLayout()
	{
	    try
	    {
		BackColor = Color.FromArgb(6, 17, 33);
		Tools.Round(this, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	readonly PictureBox BottomBarContainer = new PictureBox();
	readonly PictureBox BottomBar = new PictureBox();

	new readonly Button Close = new Button();// Close
	readonly Button Clear = new Button();// Clear

	private void InitializeBottomBar()
	{
	    var ContainerSize = new Size(Width - 2, 28);
	    var ContainerLocation = new Point(1, Height - ContainerSize.Height);
	    var ContainerBColor = MenuBar.Bar.BackColor;

	    try
	    {
		Controls.Image(this, BottomBar, ContainerSize, ContainerLocation, ContainerBColor);
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

		Controls.Button(BottomBarContainer, Close, ButtonSize, CloseLocation, ButtonBColor, ButtonFColor, 1, 10, "Close");

		Close.Click += (s, e) =>
		{
		    Hide();
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	readonly PictureBox InnerTextContainer = new PictureBox();
	readonly PictureBox TextContainer = new PictureBox();

	private static string GetFormat()
	{
	    return string.Format
	    (
		"(+)>  Please make sure you have the consent of the server administrator, I am not responsible for any damage caused by this application.\r\n\r\n"	  
    	    );
	}

	readonly public TextBox TextLog = new TextBox() { Text = GetFormat() };

	private void InitializeMainContainer()
	{
	    var ContainerSize = new Size(Width - 22, Height - MenuBar.Bar.Height - BottomBar.Height - 21);
	    var ContainerLocation = new Point(11, MenuBar.Bar.Height + MenuBar.Bar.Top + 10);
	    var ContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Controls.Image(this, TextContainer, ContainerSize, ContainerLocation, ContainerBColor);
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
	
	new void Hide() =>
	    Invoke(new MethodInvoker(delegate () { Visible = false; }));

	public AttackLog()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeLayout();
		InitializeBottomBar();
		InitializeMainContainer();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
