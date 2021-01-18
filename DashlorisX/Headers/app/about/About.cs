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

	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X  About", minim:false);
	readonly DashTools Tools = new DashTools();

	private void InitializeMenuBar()
	{
	    try
	    {
		var BAR_COLA = Color.FromArgb(8, 8, 8);
		MenuBar.Add(this, 26, BAR_COLA, BAR_COLA);
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
	    return string.Format(
		"\r\nHey there, thank you for using this application, this application was originally made for personal use only, until I realized its usability for other people like you.\r\n\r\n" +
		"This application will allow you to stress-test any http(s) web server using a pile of settings making you able to customize each request to your liking.  I must say that some settings can cause your own network to crash, so therefore, I refuse to claim responsibility for any use of this piece of ware by you the user.\r\n\r\n" +
		"For those who are curious, Dashloris-X is based on the Slowloris test method, the difference between my form of Slowloris and the original Slowloris attack method is the header customization and user-friendly configuration.\r\n\r\n" +
		"You can read up on the original Slowloris DDoS here: [https://www.imperva.com/learn/ddos/slowloris/]\r\n\r\n" +
		"The developer of this application is me Dashie, also known as KvinneKraft, you can find the source code of this piece of software here: [https://github.com/KvinneKraft/Portfolio/tree/main/DashlorisX]\r\n\r\n" +
		"Blessed be )o("
	    );
	}
	
	readonly PictureBox InfoContainer = new PictureBox();
	readonly TextBox TextContainer = new TextBox() { Text = GetText() };

	private void InitializeInformation()
	{
	    try
	    {
		var ICONT_SIZE = new Size(Width - 24, Height - MenuBar.Bar.Height - 24);
		var ICONT_LOCA = new Point(12, MenuBar.Bar.Height + 11);
		var ICONT_BCOL = Color.FromArgb(16, 16, 16);

		Controls.Image(this, InfoContainer, ICONT_SIZE, ICONT_LOCA, null, ICONT_BCOL);
		Tools.Round(InfoContainer, 6);

		var ICREC_SIZE = new Size(ICONT_SIZE.Width - 4, ICONT_SIZE.Height - 4);
		var ICREC_LOCA = new Point(2, 2);
		var ICREC_BCOL = Color.FromArgb(8, 8, 8);

		Tools.PaintRectangle(InfoContainer, 2, ICREC_SIZE, ICREC_LOCA, ICREC_BCOL);

		var TEXT_SIZE = new Size(InfoContainer.Width - 4, InfoContainer.Height - 5);
		var TEXT_LOCA = new Point(4, 3);
		var TEXT_BCOL = InfoContainer.BackColor;
		var TEXT_FCOL = Color.White;

		Controls.TextBox(InfoContainer, TextContainer, TEXT_SIZE, TEXT_LOCA, TEXT_BCOL, TEXT_FCOL, 1, 8, Color.Empty, READONLY: true, MULTILINE: true, SCROLLBAR: true, FIXEDSIZE: false);

		TextContainer.TextAlign = HorizontalAlignment.Center;
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

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(300, 250);
	    MinimumSize = new Size(300, 250);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Info";
	    Tag = "DashlorisX Info";
	    Name = "About";

	    Icon = Resources.ICON;
	    MaximizeBox = false;

	    ResumeLayout(false);
	}
    }
}
