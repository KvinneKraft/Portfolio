// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DashlorisX
{
    public class DashBox
    {
	private class Dialog : Form
	{

	}

	private static readonly List<Button> buttonList = new List<Button>(); // Initialize Upon First Show

	public static void Show(string message, string title, [Optional] Icon icon, [Optional] Point iconLocation, [Optional] List<Button> buttonList)
	{

	}
    }
}
