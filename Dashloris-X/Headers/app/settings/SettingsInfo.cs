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
    public class SettingsInfo
    {
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

	public LogContainer InfoContainer = null;

	public void Show()
	{
	    try
	    {
		if (InfoContainer == null)
		{
		    var InfoContainerTitle = string.Format("Dashloris-X   Settings Information");
		    var InfoContainerSize = new Size(350, 250);

		    InfoContainer = new LogContainer(InfoContainerSize, InfoContainerTitle);
		}

		var InfoContainerBColor = InfoContainer.TextContainer.BackColor;
		var InfoMenuBarBColor = InfoContainer.MenuBar.Bar.BackColor;
		var InfoBackColor = InfoContainer.BackColor;

		var InfoTitle = string.Format("Dashloris-X   Settings Information");
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
