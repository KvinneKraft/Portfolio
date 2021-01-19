// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

using DashlorisX.Properties;

namespace DashlorisX
{
    public class TOS : Form
    {
	new readonly DashControls Controls = new DashControls();

	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X  TOS", minim:false);
	readonly DashTools Tools = new DashTools();

	private void InitializeMenuBar()
	{
	    try
	    {
		var BAR_COLA = Color.FromArgb(19, 36, 64);
		MenuBar.Add(this, 26, BAR_COLA, BAR_COLA);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void InitializeLayout()
	{
	    try
	    {
		BackColor = Color.FromArgb(6, 17, 33);
		Tools.Round(this, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	
	readonly PictureBox MainContainer = new PictureBox();
	readonly PictureBox TextContainer = new PictureBox();

	private static string GetText()
	{
	    return string.Format(
		"(1) When using this application you automatically agree with the Terms of Services.\r\n\r\n" +
		"(2) When using this application you automatically confirm you claim responsibility for any use if any.\r\n\r\n" +
		"(3) When using this application you automatically confirm you are aware of the impact this application can have when used wrongly.\r\n\r\n" +
		"(4) When using this application you automatically confirm you are aware of the laws corresponding to DDoS/DoS/Flood attacks in your country.\r\n\r\n" +
		"(5) When using this application you automatically confirm I Dashie am not responsible for any harm inflicted upon any using this application or any of its sub-components.\r\n\r\n"
	    );
	}

	readonly TextBox TextBox = new TextBox() { Text = GetText() };
	readonly Button Agree = new Button();

	private void InitializeTextBox()
	{
	    try
	    {
		var MCONT_SIZE = new Size(Width - 20, Height - 20 - MenuBar.Bar.Height);
		var MCONT_LOCA = new Point(10, MenuBar.Bar.Height + 10);
		var MCONT_BCOL = Color.FromArgb(16, 16, 16);

		try
		{
		    Controls.Image(this, MainContainer, MCONT_SIZE, MCONT_LOCA, null, MCONT_BCOL);
		    Tools.Round(MainContainer, 4);
		}

		catch
		{
		    throw new Exception("Main Container");
		}

		var TCONT_SIZE = new Size(MCONT_SIZE.Width - 5, MCONT_SIZE.Height - 5);
		var TCONT_LOCA = new Point(3, 3);
		var TCONT_BCOL = MCONT_BCOL;

		try
		{
		    Controls.Image(MainContainer, TextContainer, TCONT_SIZE, TCONT_LOCA, null, TCONT_BCOL);
		    Tools.Round(TextContainer, 6);
		}
		
		catch
		{
		    throw new Exception("Text Container");
		}

		var TBOX_SIZE = TCONT_SIZE;
		var TBOX_LOCA = new Point(0, 0);
		var TBOX_BCOL = MCONT_BCOL;
		var TBOX_FCOL = Color.White;

		try
		{
		    Controls.TextBox(TextContainer, TextBox, TBOX_SIZE, TBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 9, Color.Empty, READONLY: true, SCROLLBAR: true, MULTILINE: true, FIXEDSIZE: false);
		}

		catch
		{
		    throw new Exception("Text Box");
		}

		var BUTT_SIZE = new Size(85, 26);
		var BUTT_LOCA = new Point((TextBox.Width - BUTT_SIZE.Width) / 2, TextBox.Height - BUTT_SIZE.Height - 10);
		var BUTT_BCOL = Color.FromArgb(3, 18, 26);//MenuBar.Bar.BackColor;
		var BUTT_FCOL = Color.White;

		try
		{
		    Controls.Button(TextBox, Agree, BUTT_SIZE, BUTT_LOCA, BUTT_BCOL, BUTT_FCOL, 1, 10, "I Agree", Color.Empty);

		    Agree.Click += (s, e) =>
		    {
			Close();
		    };

		    Tools.Round(Agree, 6);
		}

		catch
		{
		    throw new Exception("Agree");
		}

		var MRECT_SIZE = new Size(MCONT_SIZE.Width - 2, MCONT_SIZE.Height - 2);
		var MRECT_LOCA = new Point(1, 1);
		var MRECT_BCOL = Color.FromArgb(8, 8, 8);

		try
		{
		    Tools.PaintRectangle(MainContainer, 2, MRECT_SIZE, MRECT_LOCA, MRECT_BCOL);
		}

		catch
		{
		    throw new Exception("Main Container Rectangle");
		}
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public TOS()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeLayout();
		InitializeTextBox();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(300, 300);
	    MinimumSize = new Size(300, 300);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX TOS";
	    Tag = "DashlorisX TOS";
	    Name = "TOS";

	    Icon = Resources.ICON;

	    ResumeLayout(false);
	}
    }
}
