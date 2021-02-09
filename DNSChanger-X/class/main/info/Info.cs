
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
	private readonly LogContainer LogContainer = new LogContainer(new Size(250, 250), ("App Information"));

	private string AppInfoText()
	{
	    return string.Format
	    (
		"1",
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
		var ContainerBColor = Color.FromArgb(12, 12, 12);
		var MenuBarBColor = Color.FromArgb(8, 8, 8);
		var AppBColor = Color.FromArgb(20, 20, 20);
		    
		LogContainer.Show(AppInfoText(), "App Information", MenuBarBColor, ContainerBColor, AppBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}