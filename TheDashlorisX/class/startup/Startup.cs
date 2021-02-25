
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TheDashlorisX
{
    public void GamePanel()
    {

    }

    static class Startup
    {
	[STAThread]
	static void Main()
	{
	    GamePanel(); Application.Exit();

	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    ThaDashloris ThaDashloris = new ThaDashloris();

	    ThaDashloris.RunApplication();
	}
    }
}
