using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ThaDasher
{
    static class Program
    {
	private static Image splash_imag = Properties.Resources.SPLASH_PNG;
	private static Splash splash_diag;

	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);
	    
	    splash_diag = new Splash(splash_imag);

	    Application.Run(splash_diag);
	}
    }
}
