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
    public partial class Settings : Form
    {
	new readonly DashControls Controls = new DashControls();
	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X   Settings", minim:false);
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

	readonly PictureBox ConfigurationContainer = new PictureBox();

	readonly TextBox UserAgentBox = new TextBox() { Text = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36" };
	readonly TextBox MethodBox = new TextBox() { Text = "POST" };
	readonly TextBox CookieBox = new TextBox() { Text = "Cookie=8f75a1acb808d4f709bc4d71b9ab0343" };
	readonly TextBox HTTPvBox = new TextBox() { Text = "1.1" };

	readonly Label UserAgentLabel = new Label();
	readonly Label MethodLabel = new Label();
	readonly Label CookieLabel = new Label();
	readonly Label HTTPvLabel = new Label();

	private void InitializeSettings()
	{
	    try
	    {
		var CCON_SIZE = new Size(Width - 20, 70);
		var CCON_LOCA = new Point(10, MenuBar.Bar.Height + MenuBar.Bar.Top + 10);
		var CCON_BCOL = Color.FromArgb(9, 39, 66);//16, 16, 16);

		try
		{
		    Controls.Image(this, ConfigurationContainer, CCON_SIZE, CCON_LOCA, null, CCON_BCOL);
		    Tools.Round(ConfigurationContainer, 6);
		}

		catch
		{
		    throw new Exception("Option Container");
		}
		
		var LABEL_BCOL = ConfigurationContainer.BackColor;
		var LABEL_FCOL = Color.White;

		var HTTPVERSIONL_TEXT = string.Format("HTTP Version:");
		var HTTPVERSIONL_SIZE = Tools.GetFontSize(HTTPVERSIONL_TEXT, 10);
		var HTTPVERSIONL_LOCA = new Point(10, 11);

		var TEXTBOX_BCOL = Color.FromArgb(10, 10, 10);
		var TEXTBOX_FCOL = Color.White;

		var HTTPVERSIONB_LOCA = new Point(HTTPVERSIONL_LOCA.X + HTTPVERSIONL_SIZE.Width, HTTPVERSIONL_LOCA.Y - 1);
		var HTTPVERSIONB_SIZE = new Size(75, 20);

		var USERAGENTL_TEXT = string.Format("User-Agent:");
		var USERAGENTL_SIZE = Tools.GetFontSize(USERAGENTL_TEXT, 10);
		var USERAGENTL_LOCA = new Point(HTTPVERSIONB_LOCA.X + HTTPVERSIONB_SIZE.Width + 10, HTTPVERSIONL_LOCA.Y); // HTTPBox.Left + HTTPBox.Width + 10

		var USERAGENTB_LOCA = new Point(USERAGENTL_LOCA.X + USERAGENTL_SIZE.Width, USERAGENTL_LOCA.Y - 1);
		var USERAGENTB_SIZE = new Size(CCON_SIZE.Width - USERAGENTL_LOCA.X - USERAGENTL_SIZE.Width - (USERAGENTB_LOCA.X - USERAGENTB_LOCA.X) - (HTTPVERSIONL_LOCA.X), 20);

		var METHODL_TEXT = string.Format("Method:");
		var METHODL_SIZE = Tools.GetFontSize(METHODL_TEXT, 10);
		var METHODL_LOCA = new Point(HTTPVERSIONL_LOCA.X, HTTPVERSIONB_LOCA.Y + HTTPVERSIONB_SIZE.Height + 5);

		var METHODB_LOCA = new Point(METHODL_LOCA.X + METHODL_SIZE.Width, METHODL_LOCA.Y - 1);
		var METHODB_SIZE = new Size(75, 20);

		var COOKIEL_TEXT = string.Format("Cookie:");
		var COOKIEL_SIZE = Tools.GetFontSize(COOKIEL_TEXT, 10);
		var COOKIEL_LOCA = new Point(METHODB_LOCA.X + METHODB_SIZE.Width + 10, METHODL_LOCA.Y);

		var COOKIEB_LOCA = new Point(COOKIEL_LOCA.X + COOKIEL_SIZE.Width, COOKIEL_LOCA.Y - 1);
		var COOKIEB_SIZE = new Size(CCON_SIZE.Width - COOKIEL_LOCA.X - COOKIEL_SIZE.Width - (COOKIEB_LOCA.X - COOKIEB_LOCA.X) - (METHODL_LOCA.X), 20);

		try
		{
		    Controls.Label(ConfigurationContainer, HTTPvLabel, HTTPVERSIONL_SIZE, HTTPVERSIONL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, HTTPVERSIONL_TEXT);
		    Controls.Label(ConfigurationContainer, UserAgentLabel, USERAGENTL_SIZE, USERAGENTL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, USERAGENTL_TEXT);
		    Controls.Label(ConfigurationContainer, MethodLabel, METHODL_SIZE, METHODL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, METHODL_TEXT);
		    Controls.Label(ConfigurationContainer, CookieLabel, COOKIEL_SIZE, COOKIEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, COOKIEL_TEXT);

		    Controls.TextBox(ConfigurationContainer, HTTPvBox, HTTPVERSIONB_SIZE, HTTPVERSIONB_LOCA, TEXTBOX_BCOL, TEXTBOX_FCOL, 1, 8, Color.Empty);
		    Controls.TextBox(ConfigurationContainer, UserAgentBox, USERAGENTB_SIZE, USERAGENTB_LOCA, TEXTBOX_BCOL, TEXTBOX_FCOL, 1, 8, Color.Empty);
		    Controls.TextBox(ConfigurationContainer, MethodBox, METHODB_SIZE, METHODB_LOCA, TEXTBOX_BCOL, TEXTBOX_FCOL, 1, 8, Color.Empty);
		    Controls.TextBox(ConfigurationContainer, CookieBox, COOKIEB_SIZE, COOKIEB_LOCA, TEXTBOX_BCOL, TEXTBOX_FCOL, 1, 8, Color.Empty);

		    foreach (Control controlo in ConfigurationContainer.Controls)
		    {
			if (controlo is PictureBox)
			{
			    ((TextBox)controlo.Controls[0]).TextAlign = HorizontalAlignment.Center;
			    Tools.Round(controlo, 6);
			}
		    }

		    Tools.Resize(ConfigurationContainer, new Size(CCON_SIZE.Width, COOKIEL_LOCA.Y + COOKIEL_SIZE.Height + USERAGENTB_LOCA.Y + 2));

		    var RECT_SIZE = new Size(ConfigurationContainer.Width - 2, ConfigurationContainer.Height - 2);
		    var RECT_LOCA = new Point(1, 1);
		    var RECT_BCOL = Color.FromArgb(8, 8, 8);

		    Tools.PaintRectangle(ConfigurationContainer, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
		}

		catch (Exception E)
		{
		    throw (E);
		}
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox InnerBottomBarContainer = new PictureBox();
	readonly PictureBox BottomBar = new PictureBox();

	new readonly Button Close = new Button();
	readonly Button Save = new Button();
	readonly Button Help = new Button();

	private void InitializeBottomBar()
	{
	    var BCON_SIZE = new Size(Width, 44);
	    var BCON_LOCA = new Point(0, ConfigurationContainer.Top + ConfigurationContainer.Height + 11);
	    var BCON_BCOL = MenuBar.Bar.BackColor;

	    try
	    {
		Controls.Image(this, BottomBar, BCON_SIZE, BCON_LOCA, null, BCON_BCOL);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var BUTTON_BCOL = BottomBar.BackColor;
	    var BUTTON_FCOL = Color.White;
	    var BUTTON_SIZE = new Size(100, 32);

	    var CLOSE_LOCA = new Point(BUTTON_SIZE.Width * 2 + 20, 0);
	    var HELP_LOCA = new Point(BUTTON_SIZE.Width + 10, 0);
	    var SAVE_LOCA = new Point(0, 0);

	    try
	    {
		Controls.Button(InnerBottomBarContainer, Save, BUTTON_SIZE, SAVE_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "Save", Color.Empty);
		Controls.Button(InnerBottomBarContainer, Help, BUTTON_SIZE, HELP_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "Help", Color.Empty);
		Controls.Button(InnerBottomBarContainer, Close, BUTTON_SIZE, CLOSE_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "Close", Color.Empty);

		foreach (Control control in InnerBottomBarContainer.Controls)
		{
		    if (control is Button)
		    {
			Tools.Round(control, 8);
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    // Auto-Resize Inner Contaienr to button space. Last button.left + last button.width

	    var ICON_SIZE = new Size(InnerBottomBarContainer.Controls[InnerBottomBarContainer.Controls.Count - 1].Left + BUTTON_SIZE.Width, BUTTON_SIZE.Height);
	    var ICON_LOCA = new Point((BottomBar.Width - ICON_SIZE.Width) / 2, (BottomBar.Height - BUTTON_SIZE.Height) / 2);
	    var ICON_BCOL = BCON_BCOL;

	    try
	    {
		Controls.Image(BottomBar, InnerBottomBarContainer, ICON_SIZE, ICON_LOCA, null, ICON_BCOL);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void ReinitializeApp()
	{
	    try
	    {
		var NEW_SIZE = new Size(Width, BottomBar.Top + BottomBar.Height);
		Tools.Resize(this, NEW_SIZE);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public Settings()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeLayout();
		InitializeSettings();
		InitializeBottomBar();
		ReinitializeApp();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(450, 250);
	    MinimumSize = new Size(450, 250);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Settings";
	    Tag = "DashlorisX Settings";
	    Name = "Settings";

	    Icon = Resources.ICON;

	    ResumeLayout(false);
	}
    }
}
