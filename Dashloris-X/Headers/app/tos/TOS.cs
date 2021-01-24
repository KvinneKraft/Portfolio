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

	private void InitializeMenuBar()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);

		MenuBar.Add(this, 26, MenuBarBColor, MenuBarBColor);

		MenuBar.Close.Click += (s, e) =>
		{
		    Environment.Exit(-1);
		};
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
	    return string.Format
	    (
		"(1) When using this application you automatically agree with the Terms of Services.\r\n\r\n" +
		"(2) When using this application you automatically confirm you claim responsibility for any use if any.\r\n\r\n" +
		"(3) When using this application you automatically confirm you are aware of the impact this application can have when used wrongly.\r\n\r\n" +
		"(4) When using this application you automatically confirm you are aware of the laws corresponding to DDoS/DoS/Flood attacks in your country.\r\n\r\n" +
		"(5) When using this application you automatically confirm I Dashie am not responsible for any harm inflicted upon any using this application or any of its sub-components.\r\n\r\n" + 
		"For your information, there are certain domains that are blacklisted.  If you wish to still use these domains then you will have to toggle the corresponding mode on.  You can find it by pressing F1 when you put your cursor inside of the host input box.  Keep in mind, this is not recommended, but hey, your responsibility.  *hats off*\r\n\r\n"
	    );
	}

	readonly TextBox TextBox = new TextBox() { Text = GetText() };
	readonly Button Agree = new Button();

	private void InitializeTextBox()
	{
	    var MContainerSize = new Size(Width - 20, Height - 20 - MenuBar.Bar.Height);
	    var MContainerLocation = new Point(10, MenuBar.Bar.Height + 10);
	    var MContainerBColor = Color.FromArgb(9, 39, 66);

	    try
	    {
		Controls.Image(this, MainContainer, MContainerSize, MContainerLocation, null, MContainerBColor);
		Tools.Round(MainContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var TContainerSize = new Size(MContainerSize.Width - 5, MContainerSize.Height - 5);
	    var TContainerLocation = new Point(3, 3);
	    var TContainerBColor = MContainerBColor;

	    try
	    {
		Controls.Image(MainContainer, TextContainer, TContainerSize, TContainerLocation, null, TContainerBColor);
		Tools.Round(TextContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var TextBoxSize = TContainerSize;
	    var TextBoxLocation = new Point(0, 0);
	    var TextBoxBColor = MContainerBColor;
	    var TextBoxFColor = Color.White;

	    try
	    {
		Controls.TextBox(TextContainer, TextBox, TextBoxSize, TextBoxLocation, TextBoxBColor, TextBoxFColor, 1, 9, Color.Empty, READONLY: true, SCROLLBAR: true, MULTILINE: true, FIXEDSIZE: false);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var ButtonSize = new Size(85, 26);
	    var ButtonLocation = new Point((TextBox.Width - ButtonSize.Width) / 2, TextBox.Height - ButtonSize.Height - 10);
	    var ButtonBColor = Color.FromArgb(3, 18, 26);//MenuBar.Bar.BackColor;
	    var ButtonFColor = Color.White;

	    try
	    {
		Controls.Button(TextBox, Agree, ButtonSize, ButtonLocation, ButtonBColor, ButtonFColor, 1, 10, "I Agree", Color.Empty);

		Agree.Click += (s, e) =>
		{
		    Close();
		};

		Tools.Round(Agree, 6);
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
    }
}
