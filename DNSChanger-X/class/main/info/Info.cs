
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
    public class AppInfo
    {
	private readonly LogContainer LogContainer = new LogContainer(new Size(300, 300), ("App Information"));

	private string AppInfoText()
	{
	    return string.Format
	    (
		"This application was designed for educational use only. Though I know this application is able to the opposite of its purpose, I must state that I refuse to be held responsible.  Any missuse made using this application is on you.\r\n\r\n" +
		"DNS Changer X will allow you to change your DNS servers to any server you wish, whether it be ipv4 or ipv6 (which are the two only supported protocol versions.). All you will need is access to the actual DNS server itself. This is nice because sometimes it is just better to make use of a different DNS server, for whatever reason you may come up with. To be honest, I used my own DNS changer back in school in order to bypass the network restrictions, because I wanted to play minecraft at the time but the security they used prevented any domain to be resolved.\r\n\r\n" +
		"Now though, you can actually do this quite easy, no more command line you have to learn the syntax for and no more complicated interfaces. A simplistic way to make effective changes. If you come across anything, such as bugs or whatever, then you should contact me at KvinneKraft@protonmail.com with the stack-trace (if any.) given.\r\n\r\n" +
		"I love all of you, thank you for using this application.\r\n-Dashie"
	    );
	}

	public void Show(DashDialog DashDialog, PictureBox Container)
	{
	    try
	    {
		var ContainerBColor = Container.BackColor;
		var MenuBarBColor = DashDialog.MenuBar.Bar.BackColor;
		var AppBColor = DashDialog.BackColor;

		LogContainer.Show(AppInfoText(), "App Information", MenuBarBColor, ContainerBColor, AppBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}