
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Networking;
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
		    "[!]: This functionality is experimental!\r\n"
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly Button S3Button1 = new Button();
	private readonly Label S3Label1 = new Label();

	private void InitS3Con(DashWindow DashWindow, InitThaDashlorisX Parent)
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

		Control.Label(S3Container1, S3Label1, Size.Empty, LabelLoca, S3Container1.BackColor, Color.White, 1, 15, ("Whoosh Lookup"));

		var ButtonSize = new Size(85, 20);
		var ButtonLoca = new Point(S3Label1.Width + 20, 4);

		Control.Button(S3Container1, S3Button1, ButtonSize, ButtonLoca, S3Container2.BackColor, Color.White, 1, 8, ("Go Back"));

		S3Button1.Click += (s, e) =>
		{
		    Parent.HideContainers();
		    Parent.S3Class1.Show(Parent);
		};

		Tool.Round(S3Button1, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly TextBox S3TextBox1 = new TextBox();

	private void Init3(DashWindow DashWindow, InitThaDashlorisX Parent)
	{
	    try
	    {
		InitS3Con(DashWindow, Parent);

		var TextBoxSize = new Size(S3Container2.Width - 20, S3Container2.Height - 20);
		var TextBoxLoca = new Point(10, 10);

		Control.TextBox(S3Container2, S3TextBox1, TextBoxSize, TextBoxLoca, S3Container2.BackColor, Color.White, 1, 7, ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

	    	S3TextBox1.Text = string.Format
		(
		    "[!]: This functionality is experimental!\r\n"
		);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly DashNet DashNet = new DashNet();

	private void RefreshIPData(InitThaDashlorisX Parent)
	{
	    try
	    {
		string getHost()
		{
		    try
		    {
			string host = (Parent.S3Class1.S2TextBox1.Text);

			if (!DashNet.CanIP(host))
			{
			    SendLog(S2TextBox1, ("[!]: Invalid host was specified."));
			    SendLog(S3TextBox1, ("[!]: Invalid host was specified."));

			    return string.Empty;
			}

			return host;
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		var Host = getHost().Replace("https://", "").Replace("http://", "");

		if (Host == string.Empty)
		{
		    return;
		}

		void SendLog(TextBox TextBox, string Message)
		{
		    TextBox.Parent.Invoke
		    (
			new MethodInvoker
			(
			    () => TextBox.AppendText($"{Message}\r\n")
			)
		    );
		}

		SendLog(S2TextBox1, ($"Loading data for {Host} ...."));
		SendLog(S3TextBox1, ($"Loading data for {Host} ...."));

		using (Process proc = new Process())
		{
		    proc.StartInfo = new ProcessStartInfo()
		    {
			FileName = $"C:\\Windows\\System32\\nslookup.exe",
			Arguments = ($"-opt {Host}"),

			RedirectStandardOutput = true,
			RedirectStandardError = true,

			UseShellExecute = false,
			CreateNoWindow = true,
		    };

		    proc.OutputDataReceived += (s, e) =>
		    {
			var data = e.Data;

			if (data != null && data.Length > 1)
			{
			    SendLog(S2TextBox1, data);
			}
		    };

		    proc.Start();

		    proc.BeginOutputReadLine();
		    proc.WaitForExit();
		}

		string url = ($"http://ip-api.com/xml/{Host}");

		HttpWebRequest requestor = (WebRequest.Create(url) as HttpWebRequest);

		requestor.AutomaticDecompression = DecompressionMethods.GZip;

		using (HttpWebResponse responsor = requestor.GetResponse() as HttpWebResponse)
		{
		    using (Stream streamor = responsor.GetResponseStream())
		    {
			using (StreamReader reador = new StreamReader(streamor))
			{
			    SendLog(S3TextBox1, reador.ReadToEnd().Replace("  ", "\r\n"));
			}
		    }
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
		    Init3(DashWindow, Parent);

		    Tool.Round(S2Container2, 6);
		    Tool.Round(S3Container2, 6);

		    needInit = false;
		}

		new Thread(() => RefreshIPData(Parent))
		{ IsBackground = true }.Start();

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
