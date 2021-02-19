
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
    public class About
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();



	private readonly PictureBox Container1 = new PictureBox();
	private readonly Label Label1 = new Label();

	private void InitContainer1(PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width - 20, Capsule.Height - 20);
		var ContainerLoca = new Point(10, 10);
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		Control.Image(Capsule, Container1, ContainerSize, ContainerLoca, ContainerBCol);
		Tool.Round(Container1, 6);
	    
		var LabelLoca = new Point(8, 8);
		var LabelBCol = ContainerBCol;
		var LabelFCol = Color.White;

		Control.Label(Container1, Label1, Size.Empty, LabelLoca, LabelBCol, LabelFCol, 1, 12, ("Dashloris-X 3.0 Information:"));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}



	private readonly PictureBox Container2 = new PictureBox();
	private readonly TextBox TextBox1 = new TextBox();

	private void InitContainer2()
	{
	    try
	    {
		var ContainerSize = new Size(Container1.Width - 16, Container1.Height - Label1.Height - (Label1.Top*3));
		var ContainerLoca = new Point(8, (Label1.Top * 2) + Label1.Height);
		var ContainerBCol = Color.FromArgb(3, 36, 71);

		Control.Image(Container1, Container2, ContainerSize, ContainerLoca, ContainerBCol);
		Tool.Round(Container2, 6);

		var TextBoxSize = new Size(ContainerSize.Width - 8, ContainerSize.Height - 8);
		var TextBoxLoca = new Point(4, 4);
		var TextBoxBCol = ContainerBCol;
		var TextBoxFCol = Color.White;

		Control.TextBox(Container2, TextBox1, TextBoxSize, TextBoxLoca, TextBoxBCol, TextBoxFCol, 1, 8, ReadOnly:true, ScrollBar:true, Multiline:true, FixedSize:false);
		Tool.Round(TextBox1, 6);

		TextBox1.Text = ("Yes");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Initialize(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		InitContainer1(Capsule);
		InitContainer2();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}