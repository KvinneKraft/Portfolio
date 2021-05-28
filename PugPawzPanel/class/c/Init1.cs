using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace DashApplication
{
    public class Init1
    {
	readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();


	public void SecondaryInitiate(DashWindow app, PictureBox frame)
	{
	    try
	    {
		// Bottom Menu Bar Functionality
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
