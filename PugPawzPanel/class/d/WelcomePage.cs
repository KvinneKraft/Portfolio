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
    public class PageA : Expansion
    {
	readonly TextBox textBox1 = new TextBox();

	public PageA(DashWindow app, PictureBox frame)
	{
	    try
	    {
		RenderDefaultLayout(app, frame);

		var textBoxSize = new Size(ContainerA1.Width - 2, ContainerA1.Height - 2);
		var textBoxLoca = new Point(1, 1);
		var textBoxBCol = Color.FromArgb(18, 24, 33);
		var textBoxFCol = Color.White;

		Control.TextBox(ContainerA1, textBox1, textBoxSize, textBoxLoca, textBoxBCol, textBoxFCol,
		    1, 8, ReadOnly: true, Multiline: true, FixedSize: false);

		textBox1.AppendText($"{getFile("welcome.txt").Replace("{p}", Environment.UserName)}");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
