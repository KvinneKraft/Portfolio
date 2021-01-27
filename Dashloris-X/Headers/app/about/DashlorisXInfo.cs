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
    public class DashlorisXInfo
    {
	private static string GetText()
	{
	    return string.Format
	    (
		$"Heyo {Environment.UserName}!\r\n\r\nthank you for using this application, this application was originally made for personal use only, until I realized its potential.\r\n\r\n" +
		$"This application will allow you to stress-test any type of web server using a pile of settings, making you able to customize each request to your liking.\r\n\r\nI must say that some settings can cause your own network to crash, so therefore, I refuse to claim responsibility for any use of this piece of ware by you the user.\r\n\r\n" +
		$"The Dashloris-X method is based on the Slowloris method, the difference between my form, Dashloris-X, and the original Slowloris attack method, is the header customization and user-friendly configuration that comes with this application.\r\n\r\n" +
		$"You can read up on the original Slowloris DDoS method here: https://www.imperva.com/learn/ddos/slowloris/ \r\n\r\n" +
		$"The developer of this application is me Dashie, also known as KvinneKraft, you can find the source code of this piece of software over here on my github at https://github.com/KvinneKraft/Portfolio/tree/main/DashlorisX \r\n\r\n" +
		$"#2WeekProject\r\nBlessed be )o("
	    );
	}

	public LogContainer InfoContainer = null;

	public void Show()
	{
	    try
	    {
		if (InfoContainer == null)
		{
		    var InfoContainerTitle = string.Format("Dashloris-X   Information");
		    var InfoContainerSize = new Size(350, 250);

		    InfoContainer = new LogContainer(InfoContainerSize, InfoContainerTitle, FormStartPosition.CenterParent);
		}

		var InfoContainerBColor = InfoContainer.TextContainer.BackColor;
		var InfoMenuBarBColor = InfoContainer.MenuBar.Bar.BackColor;
		var InfoBackColor = InfoContainer.BackColor;

		var InfoTitle = string.Format("Dashloris-X   Information");
		var InfoText = GetText();

		InfoContainer.Show(InfoText, InfoTitle, InfoMenuBarBColor, InfoContainerBColor, InfoBackColor);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
