
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class SpruceLog
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	public readonly PictureBox S1Container1 = new PictureBox();
	private readonly PictureBox S1Container2 = new PictureBox();
	private readonly PictureBox S1Container3 = new PictureBox();

	private void Init1(DashWindow DashWindow, PictureBox Capsule)
	{
	    try
	    {
		var Container1Size = new Size(Capsule.Width, 225);
		var Container1Loca = new Point(0, -2);

		Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Capsule.BackColor);

		var Container2Size = new Size(Capsule.Width, 70);
		var Container2Loca = new Point(0, 0);

		var Container3Size = new Size(Capsule.Width, 135);
		var Container3Loca = new Point(0, 90);

		var ContainerBCol = DashWindow.MenuBar.MenuBar.BackColor;

		Control.Image(S1Container1, S1Container2, Container2Size, Container2Loca, ContainerBCol);
		Control.Image(S1Container1, S1Container3, Container3Size, Container3Loca, ContainerBCol);

		Tool.Round(S1Container2, 6);
		Tool.Round(S1Container3, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S2Container = new PictureBox();

	private readonly Button S2Button1 = new Button();
	private readonly Button S2Button2 = new Button();
	private readonly Button S2Button3 = new Button();
	private readonly Button S2Button4 = new Button();

	private void S2SetupButtonEvents()
	{
	    try
	    {
		S2Button1.Click += (s, e) =>
		{
		    S3TextBox.Clear();
		    SendLog("+ Ah mate, you gotta lock on...still!");
		};

		S2Button2.Click += (s, e) =>
		{
		    try
		    {
			SendLog("- Loading explorer dialog ....", true);

			using (SaveFileDialog Dialog = new SaveFileDialog())
			{
			    Dialog.Title = ($"Save Log - {DateTime.Now}");

			    Dialog.CheckFileExists = false;
			    Dialog.OverwritePrompt = true;
			    Dialog.CheckPathExists = true;

			    DialogResult result = Dialog.ShowDialog();

			    if (result == DialogResult.OK)
			    {
				try
				{
				    File.WriteAllText(Dialog.FileName, S3TextBox.Text);

				    if (!File.Exists(Dialog.FileName))
				    {
					throw new Exception("!~");
				    }

				    SendLog($"+ successfully saved log to {Dialog.FileName} !", true);
				}

				catch
				{
				    SendLog($"~ Unable to save log to {Dialog.FileName} !");
				};
			    }

			    else
			    {
				SendLog("~ Operation has been cancelled!");
			    }
			};

			SendLog("+ Save Log operation complete!");
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		};

		S2Button3.Click += (s, e) =>
		{
		    KeepVerbosing = (!KeepVerbosing ? true : false);
		    SendLog($"+ Verbose = {(KeepVerbosing ? "ON" : "OFF")}");
		};

		S2Button4.Click += (s, e) =>
		{
		    try
		    {
			SendLog($"- Resetting statistics ....", true);

			UpdateStats();

			SendLog($"+ Statistics have been reset successfully!", true);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		};
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}

	private void Init2(DashWindow DashWindow)
	{
	    try
	    {
		var ContainerSize = new Size(205, 50);
		var ContainerLoca = new Point(5, -2);

		Control.Image(S1Container2, S2Container, ContainerSize, ContainerLoca, S1Container2.BackColor);

		var ButtonSize = new Size(100, 20);
		var ButtonBCol = DashWindow.BackColor;
		var ButtonFCol = Color.White;

		var Button2Loca = new Point(ButtonSize.Width + 5, 0);
		var Button4Loca = new Point(Button2Loca.X, 30);
		var Button3Loca = new Point(0, 30);
		var Button1Loca = new Point(0, 0);

		Control.Button(S2Container, S2Button1, ButtonSize, Button1Loca, ButtonBCol, ButtonFCol, 1, 8, ("Clear Log"));
		Control.Button(S2Container, S2Button2, ButtonSize, Button2Loca, ButtonBCol, ButtonFCol, 1, 8, ("Save Log"));
		Control.Button(S2Container, S2Button3, ButtonSize, Button3Loca, ButtonBCol, ButtonFCol, 1, 8, ("Verbose"));
		Control.Button(S2Container, S2Button4, ButtonSize, Button4Loca, ButtonBCol, ButtonFCol, 1, 8, ("Reset Stats"));

		foreach (Button Button in S2Container.Controls)
		{
		    Tool.Round(Button, 6);
		}

		S2SetupButtonEvents();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container = new PictureBox();
	private readonly TextBox S3TextBox = new TextBox();
	
	private void Init3()
	{
	    try
	    {
		var ContainerSize = new Size(S1Container3.Width - 20, S1Container3.Height - 20);
		var ContainerLoca = new Point(10, 10);

		Control.Image(S1Container3, S3Container, ContainerSize, ContainerLoca, S2Button1.BackColor);
		Tool.Round(S3Container, 6);

		var TextBoxSize = new Size(ContainerSize.Width - 10, ContainerSize.Height - 10);
		var TextBoxLoca = new Point(5, 5);

		Control.TextBox(S3Container, S3TextBox, TextBoxSize, TextBoxLoca, S2Button1.BackColor, Color.White, 1, 7, ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

		S3TextBox.Text = string.Format
		(
		    $"+ I am waiting for you to fukin press that button, ye?\r\n" +
		    $"+ Current Date: {DateTime.Now}\r\n" +
		    $"+ Verbose = OFF\r\n"
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly Dictionary<string, int> Stats = new Dictionary<string, int>()
	{		// Pass as 'ref' for external modification.
	    { "connections", 0 },
	    { "requests", 0 }
	};

	private void UpdateStats(int Value1 = 0, int Value2 = 0)
	{
	    try
	    {
		Stats["connections"] = Value1;
		Stats["requests"] = Value2;

		S4TextBox1.Text = $"{Stats["connections"]}";
		S4TextBox2.Text = $"{Stats["requests"]}";
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S4Container = new PictureBox();

	public readonly TextBox S4TextBox1 = new TextBox();
	public readonly TextBox S4TextBox2 = new TextBox();

	private readonly Label S4Label1 = new Label();
	private readonly Label S4Label2 = new Label();

	private void Init4(DashWindow DashWindow)
	{
	    try
	    {
		ControlHelper CHelper = new ControlHelper();

		var ContainerSize = new Size(S1Container2.Width - S2Container.Left - S2Container.Width - 10, 50);
		var ContainerLoca = new Point(S1Container2.Width - ContainerSize.Width - 5, -2);

		Control.Image(S1Container2, S4Container, ContainerSize, ContainerLoca, S2Container.BackColor);

		var LabelBCol = S4Container.BackColor;
		var LabelFCol = Color.White;

		var Label1Size = CHelper.GetFontSize("Cons:", 10);
		var Label1Loca = new Point(0, 0);

		var Label2Size = CHelper.GetFontSize("Reqs:", 10);
		var Label2Loca = new Point(0, Label1Size.Height + 12);

		var TextBoxBCol = S3Container.BackColor;
		var TextBoxFCol = Color.White;

		CHelper.TextBoxParent = S4Container;
		CHelper.LabelParent = S4Container;

		var TextBox1Size = CHelper.TextBoxSize(Label1Size, Label1Loca);
		var TextBox1Loca = CHelper.ControlX(Label1Size, Label1Loca, Extra: 0);

		var TextBox2Size = CHelper.TextBoxSize(Label2Size, Label2Loca);
		var TextBox2Loca = CHelper.ControlX(Label2Size, Label2Loca, Extra: 0);

		Control.TextBox(S4Container, S4TextBox1, TextBox1Size, TextBox1Loca, TextBoxBCol, TextBoxFCol, 1, 8, true);
		Control.TextBox(S4Container, S4TextBox2, TextBox2Size, TextBox2Loca, TextBoxBCol, TextBoxFCol, 1, 8, true);

		S4TextBox1.TextAlign = HorizontalAlignment.Center;
		S4TextBox2.TextAlign = HorizontalAlignment.Center;

		UpdateStats();

		Tool.Round(S4TextBox1.Parent, 6);
		Tool.Round(S4TextBox2.Parent, 6);

		Control.Label(S4Container, S4Label1, Label1Size, Label1Loca, LabelBCol, LabelFCol, 1, 10, ("Cons:"));
		Control.Label(S4Container, S4Label2, Label2Size, Label2Loca, LabelBCol, LabelFCol, 1, 10, ("Reqs:"));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public bool RequiresInit = true;

	public void InitializePage(DashWindow DashWindow, PictureBox Capsule)
	{
	    try
	    {
		Init1(DashWindow, Capsule);
		Init2(DashWindow);
		Init3();
		Init4(DashWindow);

		RequiresInit = false;
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}


	public bool KeepVerbosing = false;
	public bool KeepStressing = true;

	public void StopAttack()
	{
	    try
	    {
		SendLog("- I am asking the Commander to quit shelling ....");
		SendLog("- Processing forceful command ....", true);

		KeepStressing = false;
		Thread.Sleep(1500);

		SendLog("+ Forceful command has been executed!", true);
		SendLog("+ Artillery has retreated successfully!");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void SendLog(string Data, bool checkVerbose = false, bool newLine = true)
	{
	    try
	    {
		if (checkVerbose && !KeepVerbosing)
		{
		    return;
		}

		void SendIt()
		{
		    S3TextBox.AppendText($"{Data}{(newLine ? "\r\n" : string.Empty)}");
		}

		S3TextBox.Parent.Invoke(new MethodInvoker(SendIt));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Show()
	{
	    try
	    {
		S1Container1.Parent.Invoke(
		    new MethodInvoker(S1Container1.Show));
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
		S1Container1.Parent.Invoke(
		    new MethodInvoker(S1Container1.Hide));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
