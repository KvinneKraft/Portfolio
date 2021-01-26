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

namespace DashlorisX.Headers.util
{
    public class LogContainer : Form
    {
	readonly DashControls Control = new DashControls();
	readonly DashTools Tool = new DashTools();

	private void InitializeComponent()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	readonly DashMenuBar MenuBar = new DashMenuBar(string.Empty, minim:false);

	private void InitializeMenuBar()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public LogContainer()
	{
	    InitializeComponent();

	    try
	    {

	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
