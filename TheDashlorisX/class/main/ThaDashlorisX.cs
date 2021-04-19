
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

using DashFramework.Erroring;

namespace TheDashlorisX
{
    public partial class ThaDashloris : InitThaDashlorisX
    {
	public void RunApplication()
	{
	    try
	    {
		InitializeApp();

		new InsufficientPermissions();
		new AlreadyRunning();

		Application.Run(DashWindow);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }


    public class DashGlobe
    {
	public void ExitApplication()
	{
	    Environment.Exit(-1);
	    Application.Exit();
	}
    }
}
