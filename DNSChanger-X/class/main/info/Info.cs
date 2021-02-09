
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
	private string AppInfo()
	{
	    return string.Format
	    (
		"",
		"",
		"",
		"",
		"",
		""
	    );
	}

	public void Show()
	{
	    try
	    {
		var LogTitle = ("App Information");
		var LogSize = new Size(250, 250);

		using (var LogContainer = new LogContainer(LogSize, LogTitle))
		{
		    var ContainerBColor = Color.FromArgb(12, 12, 12);
		    var MenuBarBColor = Color.FromArgb(8, 8, 8);
		    var AppBColor = Color.FromArgb(20, 20, 20);

		    LogContainer.Show(AppInfo(), LogTitle, MenuBarBColor, ContainerBColor, AppBColor);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}