
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
    public class AppHelp
    {
	private readonly LogContainer LogContainer = new LogContainer(new Size(300, 300), ("App Help"));

	private string AppHelpText()
	{
	    return string.Format
	    (
		"---------------------------------\r\n" +
		" Frequently Asked Questions:\r\n" +
		"---------------------------------\r\n" +
		" [1]. Question: What is the use of two IP specification entries?\r\n" +
		" [1]. Answer: You only have to use one, but I found it useful to have the ability to specify multiple IP addresses. It is mainly a personal preference besides it should not make much of a difference since there can only be one primary host.\r\n" +
		"----------------\r\n" +
		" [2]. Question: Why should I use this tool to begin with?\r\n" +
		" [2]. Answer: Well, I do not really have a reason except for the fact I am the Developer.\r\n" +
		"----------------\r\n" +
		" [3]. Question: How can I tell whether this application is safe?\r\n" +
		" [3]. Answer: If you downloaded this application from either my GitHub (https://github.com/KvinneKraft) or my website (https://pugpawz.com/) you can be certain of it being the right version. If you still feel skeptical then you can compile this application by yourself, all of the necessary source code is available in my repository on my GitHub.\r\n" +
		"----------------\r\n" + 
		" [4]. Question: Where can I find updates?\r\n" +
		" [4]. Answer: You can find any of the latest and oldest versions on both my GitHub and my website. The urls are in the previous answer.\r\n" +
		"----------------\r\n" +
		" [5]. Question: How about the anonomity of the DNS server(s)?\r\n" +
		" [5]. Answer: What happens server side is not within my hands, it is up to you to find the right DNS server. If you choose the wrong one though, you may end up risking your information and if worst comes to worst, it can cost you your personal information. This is also why I recommend the use of this application to those who actually know what it does. Simply said.\r\n" +
		"---------------------------------"
	    );
	}

	public void Show(DashDialog DashDialog, PictureBox Container)
	{
	    try
	    {
		var ContainerBCol = Container.BackColor;
		var MenuBarBCol = DashDialog.MenuBar.Bar.BackColor;
		var AppBCol = DashDialog.BackColor;

		LogContainer.Show(AppHelpText(), "App Information", MenuBarBCol, ContainerBCol, AppBCol);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
