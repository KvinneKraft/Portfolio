
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



	private readonly Label Label1 = new Label();

	private void InitContainer1()
	{
	    try
	    { 
		var LabelLoca = new Point(8, 8);
		var LabelBCol = Container1.BackColor;
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

		TextBox1.Text = string.Format
		(
		    "\r\nThis application was made for educational purposes only. I therefore do not claim any responsibility for any harm inflicted because of some form of use of this application. I therefore I am also going to state that you are claiming responsibility, automatically, whenever you make use of this application.\r\n\r\n" +
		    "This project started off as a small thing I wanted to work on, a small project to prove my point basically. But after releasing 1.0 I felt like I could improve it so much more, so that is when 2.0 was born. At this point I took a small break from this project and worked on the DNS Spoofer X (you can find it on https://github.com/KvinneKraft as well, even on here https://pugpawz.com.), after taking a break I basically continued working on this application. I decided to recode the entire project, mainly because of many improvements I wanted to work into the project. There were many parts of the code where I wondered to myself, did I really code this? It took me two weeks to put the previous release together, which included restless nights. But yea. Here it is.\r\n\r\n" +
		    "The main purpose of this application is the stress testing of web servers using the Dashloris-X method, this method is based on the original slowloris method. The difference is the variation and of course my way of putting it together, it is based on it but not the same. This version has many fixes, it makes you able to create endless connections without using too much bandwidth. When I tried running the current library against my own website the load would peak at a few GB/ps, and that by just using my home connection.\r\n\r\n" +
		    "Because of its fatal capabilities must I ask you to be careful, do not make use of this client if you do not know what it does or is for that sake. I love all of you, peace out man!\r\n\r\n" +
		    "If you find any bugs or have any suggestions then you can reach out to me at KvinneKraft@protonmail.com."
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}



	private readonly PictureBox Container1 = new PictureBox();

	public void Initialize(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width - 20, Capsule.Height - 20);
		var ContainerLoca = new Point(10, 10);
		var ContainerBCol = Color.FromArgb(2, 55, 110);

		Control.Image(Capsule, Container1, ContainerSize, ContainerLoca, ContainerBCol);
		
		InitContainer1();
		InitContainer2();

		Tool.Round(Container1, 6);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}