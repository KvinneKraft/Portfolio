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
    public partial class About : Form
    {
	new readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(300, 250);
	    MinimumSize = new Size(300, 250);

	    StartPosition = FormStartPosition.CenterParent;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Info";
	    Tag = "DashlorisX Info";
	    Name = "About";

	    Icon = Resources.ICON;
	    MaximizeBox = false;

	    ResumeLayout(false);
	}

	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X  About", minim: false);

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
	 
	private void InitializeLayout()
	{
	    try
	    {
		BackColor = Color.FromArgb(6, 17, 33);
		Tools.Round(this, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private static string GetText()
	{
	    return string.Format
	    (
		$"\r\nHeyo {Environment.UserName}!\r\n\r\nthank you for using this application, this application was originally made for personal use only, until I realized its potential.\r\n\r\n" +
		$"This application will allow you to stress-test any type of web server using a pile of settings, making you able to customize each request to your liking.\r\n\r\nI must say that some settings can cause your own network to crash, so therefore, I refuse to claim responsibility for any use of this piece of ware by you the user.\r\n\r\n" +
		$"The Dashloris-X method is based on the Slowloris method, the difference between my form, Dashloris-X, and the original Slowloris attack method, is the header customization and user-friendly configuration that comes with this application.\r\n\r\n" +
		$"You can read up on the original Slowloris DDoS method here: https://www.imperva.com/learn/ddos/slowloris/ \r\n\r\n" +
		$"The developer of this application is me Dashie, also known as KvinneKraft, you can find the source code of this piece of software over here: https://github.com/KvinneKraft/Portfolio/tree/main/DashlorisX \r\n\r\n" +
		$"#2WeekProject\r\nBlessed be )o("
	    );
	}
	
	readonly PictureBox InfoContainer = new PictureBox();
	readonly TextBox TextContainer = new TextBox() { Text = GetText() };

	private void InitializeInformation()
	{
	    var IContainerSize = new Size(Width - 24, Height - MenuBar.Bar.Height - 24);
	    var IContainerLocation = new Point(12, MenuBar.Bar.Height + 11);
	    var IContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Controls.Image(this, InfoContainer, IContainerSize, IContainerLocation, null, IContainerBColor);
		Tools.Round(InfoContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var TEXT_SIZE = new Size(InfoContainer.Width - 4, InfoContainer.Height - 5);
	    var TEXT_LOCA = new Point(4, 3);
	    var TEXT_BCOL = InfoContainer.BackColor;
	    var TEXT_FCOL = Color.White;

	    try
	    {
		Controls.TextBox(InfoContainer, TextContainer, TEXT_SIZE, TEXT_LOCA, TEXT_BCOL, TEXT_FCOL, 1, 9, Color.Empty, READONLY: true, MULTILINE: true, SCROLLBAR: true, FIXEDSIZE: false);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public About()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeLayout();
		InitializeInformation();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
