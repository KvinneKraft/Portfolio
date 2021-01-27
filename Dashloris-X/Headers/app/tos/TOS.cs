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
    public class TOS
    {
	private static string GetText()
	{
	    return string.Format
	    (
		"(1) When using this application you automatically agree with the Terms of Services.\r\n\r\n" +
		"(2) When using this application you automatically confirm you claim responsibility for any use if any.\r\n\r\n" +
		"(3) When using this application you automatically confirm you are aware of the impact this application can have when used wrongly.\r\n\r\n" +
		"(4) When using this application you automatically confirm you are aware of the laws corresponding to DDoS/DoS/Flood attacks in your country.\r\n\r\n" +
		"(5) When using this application you automatically confirm I Dashie am not responsible for any harm inflicted upon any using this application or any of its sub-components.\r\n\r\n" + 
		"For your information, there are certain domains that are blacklisted.  \r\n\r\nIf you wish to still use these domains then you will have to toggle the corresponding mode on.  \r\n\r\nYou can find it by pressing F1 when you put your cursor inside of the host input box.  \r\n\r\nKeep in mind, this is not recommended, but hey, your responsibility.  *hats off*"
	    );
	}

	private LogContainer ToSContainer = null;

	public void Show()
	{
	    try
	    {
		if (ToSContainer == null)
		{
		    var ToSContainerTitle = string.Format("Dashloris-X   ToS");
		    var ToSContainerSize = new Size(325, 250);

		    ToSContainer = new LogContainer(ToSContainerSize, ToSContainerTitle);
		}

		var ToSContainerBColor = ToSContainer.TextContainer.BackColor;
		var ToSMenuBarBColor = ToSContainer.MenuBar.Bar.BackColor;
		var ToSBackColor = ToSContainer.BackColor;

		var ToSTitle = string.Format("Dashloris-X   ToS");
		var ToSText = GetText();

		ToSContainer.Show(ToSText, ToSTitle, ToSMenuBarBColor, ToSContainerBColor, ToSBackColor);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
