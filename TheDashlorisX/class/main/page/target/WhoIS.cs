
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class WHOIS
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();

	private void Init1(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		var ContainerSize = new Size(Capsule.Width, Capsule.Height);
		var ContainerLoca = new Point(0, 0);

		Control.Image(Capsule, S1Container1, ContainerSize, ContainerLoca, Capsule.BackColor);
		
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	

	private readonly PictureBox S2Container1 = new PictureBox();
	private readonly PictureBox S2Container2 = new PictureBox();

	private readonly Label S2Label1 = new Label();

	private void InitS2Con(DashWindow DashWindow)
	{
	    try
	    {
		var Container1Size = new Size(S1Container1.Width, 115);
		var Container1Loca = new Point(0, 0);

		Control.Image(S1Container1, S2Container1, Container1Size, Container1Loca, S1Container1.BackColor);

		var Container2Size = new Size(Container1Size.Width, Container1Size.Height - 30);
		var Container2Loca = new Point(0, 30);
		var Container2BCol = DashWindow.MenuBar.MenuBar.BackColor; 

		Control.Image(S2Container1, S2Container2, Container2Size, Container2Loca, Container2BCol);

		var LabelLoca = new Point(10, 0);

		Control.Label(S2Container1, S2Label1, Size.Empty, LabelLoca, S2Container1.BackColor, Color.White, 1, 15, ("NS-Lookup"));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly TextBox S2TextBox1 = new TextBox();

	private void Init2(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		InitS2Con(DashWindow);

		var TextBoxSize = new Size(S2Container2.Width - 20, S2Container2.Height - 20);
		var TextBoxLoca = new Point(10, 10);

		Control.TextBox(S2Container2, S2TextBox1, TextBoxSize, TextBoxLoca, S2Container2.BackColor, Color.White, 1, 7, ReadOnly:true, Multiline:true, ScrollBar:true, FixedSize:false);

		S2TextBox1.Text = string.Format
		(
		    "[!]: This functionality is experimental!"
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly Label S3Label1 = new Label();

	private void InitS3Con(DashWindow DashWindow)
	{
	    try
	    {
		var Container1Size = new Size(S1Container1.Width, 115);
		var Container1Loca = new Point(0, S2Container1.Top + S2Container1.Height + 20);

		Control.Image(S1Container1, S3Container1, Container1Size, Container1Loca, S1Container1.BackColor);

		var Container2Size = new Size(Container1Size.Width, Container1Size.Height - 30);
		var Container2Loca = new Point(0, 30);
		var Container2BCol = DashWindow.MenuBar.MenuBar.BackColor;

		Control.Image(S3Container1, S3Container2, Container2Size, Container2Loca, Container2BCol);

		var LabelLoca = new Point(10, 0);

		Control.Label(S3Container1, S3Label1, Size.Empty, LabelLoca, S3Container1.BackColor, Color.White, 1, 15, ("DNS Lookup"));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly TextBox S3TextBox1 = new TextBox();

	private void Init3(DashWindow DashWindow)
	{
	    try
	    {
		InitS3Con(DashWindow);

		var TextBoxSize = new Size(S3Container2.Width - 20, S3Container2.Height - 20);
		var TextBoxLoca = new Point(10, 10);

		Control.TextBox(S3Container2, S3TextBox1, TextBoxSize, TextBoxLoca, S3Container2.BackColor, Color.White, 1, 7, ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

	    	S3TextBox1.Text = string.Format
		(
		    "[!]: This functionality is experimental!"
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private void RefreshIPData(InitThaDashlorisX Parent)
	{
	    try
	    {
		using (Process proc = new Process())
		{
		    proc.StartInfo = new ProcessStartInfo()
		    {
			UseShellExecute = false,

			RedirectStandardOutput = true,
			RedirectStandardError = true,

			Arguments = "8.8.8.8",
			FileName = $"C:\\Windows\\System32\\nslookup.exe"
		    };

		    proc.OutputDataReceived += (s, e) =>
			S2TextBox1.AppendText(e.Data);

		    proc.Start();

		    proc.WaitForExit();
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private bool needInit = true;

	public void Show(PictureBox Capsule, DashWindow DashWindow, InitThaDashlorisX Parent)
	{
	    try
	    {
		if (needInit)
		{
		    Init1(Capsule, DashWindow);
		    Init2(Capsule, DashWindow);
		    Init3(DashWindow);

		    Tool.Round(S2Container2, 6);
		    Tool.Round(S3Container2, 6);

		    needInit = false;
		}

		RefreshIPData(Parent);

		Parent.HideContainers();

		S1Container1.Show();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Hide()
	{
	    try
	    {
		S1Container1.Hide();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
