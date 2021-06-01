using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using DashFramework.Interface.Controls;
using DashFramework.Runnables;
using DashFramework.Erroring;
using DashFramework.Dialog;

namespace DashApplication
{
    public class Init3
    {
	readonly string[] defaultSettingsFormat = new string[]
	{//Default Configuration Layout right here.
	    "",
	    "",
	    "",
	    "",
	    "",
	    "",
	    "",
	    "",
	    ""
	};

	//Get and Set methods for config here.

	public void SecondaryInitiate(DashWindow app, PictureBox frame)
	{
	    try
	    {
		//Create missing config directory
		//Create missing config file

		Runnable runnable = new Runnable();

		runnable.RunTaskLater
		(
		    app, 
		    
		    () => 
		    {
			MessageBox.Show("Test");
			//Load Configuration
			//Set values to temp variables
			//Update main values with temp variables every 500ms
		    }, 
		    
		    8000, true
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
