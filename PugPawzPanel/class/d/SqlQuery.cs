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
    public class PageB : Expansion
    {
	public PageB(DashWindow app, PictureBox frame)
	{
	    try
	    {
		RenderDefaultLayout(app, frame);

		
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
