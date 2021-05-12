using System;
using System.Windows.Forms;

namespace PugPawz_Helper_Panel
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);
	    //Application.Run(new MainGUI()); //-> Start Mechanism
	}
    }
}
