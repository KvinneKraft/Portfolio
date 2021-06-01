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
    public class Expansion
    {
	public readonly DashControls Control = new DashControls();
	public readonly DashTools Tool = new DashTools();


	public string getFile(string filePath)
	{
	    try
	    {
		filePath = ("");

		foreach (string r in resources.Resources.welcome.Split
		(
		    new[] 
		    {
			Environment.NewLine
		    }, 
		    
		    StringSplitOptions.RemoveEmptyEntries
		)){
		    filePath += ($"{r}\r\n");
		}
		
		return filePath;
	    }

	    catch
	    {
		return string.Empty;
	    }
	}


	public readonly PictureBox ContainerA1 = new PictureBox();

	public void RenderDefaultLayout(DashWindow app, PictureBox frame)
	{
	    try
	    {
		var ContSize = new Size(app.Width - 24, frame.Height - 20);
		var ContLoca = new Point(10, 10);
		var ContBCol = app.values.getBarColor();

		Control.Image(frame, ContainerA1, ContSize, ContLoca, ContBCol);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Show()
	{
	    ContainerA1.Show();
	}

	public void Hide()
	{
	    ContainerA1.Hide();
	}
    }
}
