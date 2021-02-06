﻿// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MetaEditorX
{
    public static class Startup
    {
	private static MetaEditorX MetaEditorX;

	[STAThread]
	public static void Main()
	{
	    try
	    {
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		MetaEditorX = new MetaEditorX();
		MetaEditorX.Show();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
