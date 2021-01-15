﻿// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DashlorisX
{
    public class ErrorHandler
    {
	public readonly DashControls Control = new DashControls();
	public readonly DashTools Tool = new DashTools();

	private class Dialog : Form
	{
	    public Dialog(string error, string title)
	    {
		//Custom Dialog Soon
		MessageBox.Show(error, title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	    }
	}

	public static void Utilize(string error, string title, bool thread = false)
	{
	    new Thread(() => {
		new Dialog(error, title).ShowDialog();
	    })

	    { IsBackground = thread }.Start();
	}
    }
}
