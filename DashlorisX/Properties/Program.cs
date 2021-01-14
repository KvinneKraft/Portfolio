// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.Windows.Forms;

namespace DashlorisX
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    var loris = new DashlorisX();

	    loris.ShowDialog();
	}
    }
}
