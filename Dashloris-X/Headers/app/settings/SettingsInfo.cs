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
    public class SettingsInfo : Form
    {
	new readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(300, 250);
	    MinimumSize = new Size(300, 250);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    BackColor = Color.FromArgb(6, 17, 33);

	    Text = "DashlorisX Settings Info";
	    Tag = "DashlorisX Settings Info";
	    Name = "Settings Information";

	    Icon = Resources.ICON;

	    Tools.Round(this, 6);
	    ResumeLayout(false);
	}

	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X   Settings Information", minim: false);

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

	readonly PictureBox BottomBar = new PictureBox();
	new readonly Button Close = new Button();

	private void InitializeBottomBar()
	{
	    var BottomBarSize = new Size(Width, 28);
	    var BottomBarLocation = new Point(0, Height - BottomBarSize.Height);
	    var BottomBarBColor = MenuBar.Bar.BackColor;

	    var CloseSize = new Size(100, 27);
	    var CloseLocation = new Point((BottomBarSize.Width - CloseSize.Width) / 2, 0);
	    var CloseBColor = BottomBarBColor;
	    var CloseFColor = Color.White;

	    try
	    {
		Controls.Button(BottomBar, Close, CloseSize, CloseLocation, CloseBColor, CloseFColor, 1, 10, "Close", Color.Empty);
		Tools.Round(Close, 6);

		Close.Click += (s, e) =>
		{
		    Hide();
		};

		Controls.Image(this, BottomBar, BottomBarSize, BottomBarLocation, null, BottomBarBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox InnerTextContainer = new PictureBox();
	readonly PictureBox MainTextContainer = new PictureBox();

	private static string GetText()
	{
	    return string.Format
	    (
		"[HTTP Version]\r\n" +
		"This setting specifies the version of the http header sent.\r\n" +
		"- HTTP/1.0 : Not many websites support this.\r\n" +
		"- HTTP/1.1 : Quite some websites support this.\r\n" +
		"- HTTP/1.2 : The majority of websites support this.\r\n" +
		"Note: I purposely postponed the implementation of HTTP 2.0.\r\n\r\n" +
		
		"[User-Agent]\r\n" +
		"This setting specifies the user-agent used by the http header sent.  You can use any user-agent, I recommend to use some common ones to make yourself as ordinary as possible.\r\n\r\n" +

		"[Method]\r\n" +
		"This setting specifies the type of the http header sent.  Read some more about this over here: https://www.tutorialspoint.com/http/http_methods.htm\r\n\r\n" +
		"Though some of these methods are not compatible with the Content-Length field, the future beholds updates making it able to be integrated.  Besides, perhaps you find a use for these methods.\r\n\r\n" +
	
		"[Cookie]\r\n" +
		"This setting specifies the cookie that will be sent with the http header to the server targeted.  You can use any type of cookie as long as it follows the already present format.  [Cookie1=cookie1;Cookie2=cookie2]"
	    );
	}

	readonly TextBox TextLog = new TextBox() { Text = GetText() };

	private void InitializeContainer()
	{
	    var MContainerSize = new Size(Width - 20, Height - 20 - BottomBar.Height - MenuBar.Bar.Height);
	    var MContainerLocation = new Point(10, MenuBar.Bar.Height + 10);
	    var MContainerBColor = Color.FromArgb(9, 39, 66);

	    var TContainerSize = new Size(MContainerSize.Width - 8, MContainerSize.Height - 8);
	    var TContainerLocation = new Point(4, 4);
	    var TContainerBColor = MContainerBColor;

	    try
	    {
		Controls.Image(MainTextContainer, InnerTextContainer, TContainerSize, TContainerLocation, null, TContainerBColor);
		Controls.Image(this, MainTextContainer, MContainerSize, MContainerLocation, null, MContainerBColor);

		Tools.Round(InnerTextContainer, 8);
		Tools.Round(MainTextContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var TextLogSize = TContainerSize;
	    var TextLogLocation = new Point(0, 0);
	    var TextLogBColor = TContainerBColor;
	    var TextLogFColor = Color.White;

	    try
	    {
		Controls.TextBox(InnerTextContainer, TextLog, TextLogSize, TextLogLocation, TextLogBColor, TextLogFColor, 1, 8, Color.Empty, READONLY:true, SCROLLBAR:true, FIXEDSIZE:false, MULTILINE:true);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public SettingsInfo()
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
