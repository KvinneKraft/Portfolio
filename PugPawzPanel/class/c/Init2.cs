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
    public class Init2
    {
	readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();


	public void SecondaryInitiate(DashWindow app, PictureBox frame)
	{
	    try
	    {
		// Main Frame Functionality + Pages and all shit
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
