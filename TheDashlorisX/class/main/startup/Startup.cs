using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheDashlorisX
{
    static class Startup
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    ThaDashloris ThaDashloris = new ThaDashloris();

	    ThaDashloris.RunApplication();
	}
    }
}
