
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
		"" +
		"" +
		"" +
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

		LogContainer.Show(AppHelpText(), "App Information", MenuBarBColor, ContainerBColor, AppBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
