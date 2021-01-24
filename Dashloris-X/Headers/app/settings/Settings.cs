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

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(450, 250);
	    MinimumSize = new Size(450, 250);

	    StartPosition = FormStartPosition.CenterParent;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Settings";
	    Tag = "DashlorisX Settings";
	    Name = "Settings";

	    Icon = Resources.ICON;

	    ResumeLayout(false);
	}

	private void InitializeMenuBar()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		MenuBar.Add(this, 26, MenuBarBColor, MenuBarBColor);
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

	readonly PictureBox ConfigurationContainer = new PictureBox();//This shit hurts my eyes help.

	readonly DropDownMenu MethodMenu = new DropDownMenu();
	readonly DropDownMenu HTTPvMenu = new DropDownMenu();

	public readonly static TextBox UserAgentBox = new TextBox() { Text = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36" };
	public readonly static TextBox CookieBox = new TextBox() { Text = "Cookie=cookie data man." };
	public readonly static TextBox MethodBox = new TextBox() { Text = "POST", ReadOnly = true };
	public readonly static TextBox HTTPvBox = new TextBox() { Text = "HTTP/1.1", ReadOnly = true };

	readonly Label UserAgentLabel = new Label();
	readonly Label MethodLabel = new Label();
	readonly Label CookieLabel = new Label();
	readonly Label HTTPvLabel = new Label();

	private void InitializeDropdownMenus()
	{
	    var MenuBorderBColor = MethodBox.BackColor;
	    var MenuBColor = MethodBox.BackColor;

	    Point GetMenuLocation(TextBox Object) =>
		(new Point(Object.Parent.Left + ConfigurationContainer.Left + 1, Object.Parent.Top + Object.Parent.Height + ConfigurationContainer.Top - 5));

	    try
	    {
		MethodMenu.SetupMenu(this, GetMenuLocation(MethodBox), MenuBColor, MenuBorderBColor);
		HTTPvMenu.SetupMenu(this, GetMenuLocation(HTTPvBox), MenuBColor, MenuBorderBColor);

		MethodMenu.Container.BringToFront();
		HTTPvMenu.Container.BringToFront();

		ConfigurationContainer.MouseEnter += (s, e) =>
		{
		    if (MethodMenu.Container.Visible)
		    {
			MethodMenu.Hide();
		    }

		    if (HTTPvMenu.Container.Visible)
		    {
			HTTPvMenu.Hide();
		    }
		};

		Tools.Round(MethodMenu.Container, 6);
		Tools.Round(HTTPvMenu.Container, 6);

		HTTPvBox.MouseEnter += (s, e) =>
		{
		    HTTPvMenu.Show();
		    HTTPvBox.BringToFront();
		};

		MethodBox.MouseEnter += (s, e) =>
		{
		    MethodMenu.Show();
		    MethodBox.BringToFront();
		};
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var MenuItemBColor = MenuBColor;
	    var MenuItemFColor = Color.White;

	    Label GetLabel() => new Label();
	    
	    int GetID(int id)
	    {
		switch (id)
		{
		    case 0:
			return MethodMenu.ContentContainer.Controls.Count - 1;
		    case 1:
			return HTTPvMenu.ContentContainer.Controls.Count - 1;
		    default:
			return -1;
		}
	    }

	    try
	    {
		MethodMenu.AddItem(GetLabel(), " ", MenuItemBColor, MenuItemFColor, ItemTextSize: 7, ItemWidth: HTTPvBox.Width + 1, ItemHeight: 5);

		foreach (string method in new string[] { "POST", "PUT", "GET", "HEAD" })
		{
		    MethodMenu.AddItem(GetLabel(), method, MenuItemBColor, MenuItemFColor, ItemTextSize: 8, ItemWidth: HTTPvBox.Width + 1, ItemHeight:16);

		    var label = MethodMenu.GetItem(GetID(0));

		    label.Click += (s, e) =>
		    {
			MethodBox.Text = method;
		    };
		}

		HTTPvMenu.AddItem(GetLabel(), " ", MenuItemBColor, MenuItemFColor, ItemTextSize: 7, ItemWidth: HTTPvBox.Width + 1, ItemHeight: 5);

		foreach (string version in new string[] { "HTTP/1.0", "HTTP/1.1", "HTTP/1.2" })
		{
		    HTTPvMenu.AddItem(GetLabel(), version, MenuItemBColor, MenuItemFColor, ItemTextSize: 8, ItemWidth: HTTPvBox.Width + 1, ItemHeight:16);

		    var label = HTTPvMenu.GetItem(GetID(1));

		    label.Click += (s, e) =>
		    {
			HTTPvBox.Text = version;
		    };
		}

		foreach (var labelList in new Control.ControlCollection[] { HTTPvMenu.ContentContainer.Controls, MethodMenu.ContentContainer.Controls })
		{
		    foreach (Label label in labelList)
		    {
			if (label.Text != " ")
			{
			    label.MouseEnter += (s, e) =>
			    {
				label.BackColor = Color.FromArgb(16, 16, 16);
			    };

			    label.MouseLeave += (s, e) =>
			    {
				label.BackColor = MenuItemBColor;
			    };
			}
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void InitializeSettings()
	{
	    var CContainerSize = new Size(Width - 20, 70);
	    var CContainerLocation = new Point(10, MenuBar.Bar.Height + MenuBar.Bar.Top + 10);
	    var CContainerBColor = Color.FromArgb(9, 39, 66);//16, 16, 16);

	    try
	    {
		Controls.Image(this, ConfigurationContainer, CContainerSize, CContainerLocation, null, CContainerBColor);
		Tools.Round(ConfigurationContainer, 6);
	    }

	    catch
	    {
		throw new Exception("Option Container");
	    }

	    var LabelBColor = ConfigurationContainer.BackColor;
	    var LabelFColor = Color.White;

	    var HTTPVLabelText = string.Format("HTTP Version:");
	    var HTTPVLabelSize = Tools.GetFontSize(HTTPVLabelText, 10);
	    var HTTPVLabelLocation = new Point(10, 11);

	    var TextBoxBColor = Color.FromArgb(10, 10, 10);
	    var TextBoxFColor = Color.White;

	    var HTTPVButtonLocation = new Point(HTTPVLabelLocation.X + HTTPVLabelSize.Width, HTTPVLabelLocation.Y - 1);
	    var HTTPVButtonSize = new Size(75, 19);

	    var UserAgentLabelText = string.Format("User-Agent:");
	    var UserAgentLabelSize = Tools.GetFontSize(UserAgentLabelText, 10);
	    var UserAgentLabelLocation = new Point(HTTPVButtonLocation.X + HTTPVButtonSize.Width + 10, HTTPVLabelLocation.Y); // HTTPBox.Left + HTTPBox.Width + 10

	    var UserAgentButtonLocation = new Point(UserAgentLabelLocation.X + UserAgentLabelSize.Width, UserAgentLabelLocation.Y - 1);
	    var UserAgentButtonSize = new Size(CContainerSize.Width - UserAgentLabelLocation.X - UserAgentLabelSize.Width - (UserAgentButtonLocation.X - UserAgentButtonLocation.X) - (HTTPVLabelLocation.X), 19);

	    var MethodLabelText = string.Format("Method:");
	    var MethodLabelSize = Tools.GetFontSize(MethodLabelText, 10);
	    var MethodLabelLocation = new Point(HTTPVLabelLocation.X, HTTPVButtonLocation.Y + HTTPVButtonSize.Height + 5);

	    var MethodButtonLocation = new Point(MethodLabelLocation.X + MethodLabelSize.Width, MethodLabelLocation.Y - 1);
	    var MethodButtonSize = new Size(75, 19);

	    var CookieLabelText = string.Format("Cookie:");
	    var CookieLabelSize = Tools.GetFontSize(CookieLabelText, 10);
	    var CookieLabelLocation = new Point(MethodButtonLocation.X + MethodButtonSize.Width + 10, MethodLabelLocation.Y);

	    var CookieButtonLocation = new Point(CookieLabelLocation.X + CookieLabelSize.Width, CookieLabelLocation.Y - 1);
	    var CookieButtonSize = new Size(CContainerSize.Width - CookieLabelLocation.X - CookieLabelSize.Width - (CookieButtonLocation.X - CookieButtonLocation.X) - (MethodLabelLocation.X), 19);

	    try
	    {
		Controls.Label(ConfigurationContainer, HTTPvLabel, HTTPVLabelSize, HTTPVLabelLocation, LabelBColor, LabelFColor, 1, 10, HTTPVLabelText);
		Controls.Label(ConfigurationContainer, UserAgentLabel, UserAgentLabelSize, UserAgentLabelLocation, LabelBColor, LabelFColor, 1, 10, UserAgentLabelText);
		Controls.Label(ConfigurationContainer, MethodLabel, MethodLabelSize, MethodLabelLocation, LabelBColor, LabelFColor, 1, 10, MethodLabelText);
		Controls.Label(ConfigurationContainer, CookieLabel, CookieLabelSize, CookieLabelLocation, LabelBColor, LabelFColor, 1, 10, CookieLabelText);

		Controls.TextBox(ConfigurationContainer, HTTPvBox, HTTPVButtonSize, HTTPVButtonLocation, TextBoxBColor, TextBoxFColor, 1, 9, Color.Empty);
		Controls.TextBox(ConfigurationContainer, UserAgentBox, UserAgentButtonSize, UserAgentButtonLocation, TextBoxBColor, TextBoxFColor, 1, 9, Color.Empty);
		Controls.TextBox(ConfigurationContainer, MethodBox, MethodButtonSize, MethodButtonLocation, TextBoxBColor, TextBoxFColor, 1, 9, Color.Empty);
		Controls.TextBox(ConfigurationContainer, CookieBox, CookieButtonSize, CookieButtonLocation, TextBoxBColor, TextBoxFColor, 1, 9, Color.Empty);

		foreach (Control controlo in ConfigurationContainer.Controls)
		{
		    if (controlo is PictureBox)
		    {
			((TextBox)controlo.Controls[0]).TextAlign = HorizontalAlignment.Center;
			Tools.Round(controlo, 6);
		    }
		}

		Tools.Resize(ConfigurationContainer, new Size(CContainerSize.Width, CookieLabelLocation.Y + CookieLabelSize.Height + UserAgentButtonLocation.Y + 2));
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    InitializeDropdownMenus();
	}

	readonly PictureBox InnerBottomBarContainer = new PictureBox();
	readonly PictureBox BottomBar = new PictureBox();

	new readonly Button Close = new Button();
	readonly Button Help = new Button();

	readonly SettingsInfo SettingsInfoDialog = new SettingsInfo();

	private void InitializeBottomBar()
	{
	    var BContainerSize = new Size(Width, 44);
	    var BContainerLocation = new Point(0, ConfigurationContainer.Top + ConfigurationContainer.Height + 11);//26);
	    var BContainerBColor = MenuBar.Bar.BackColor;

	    try
	    {
		Controls.Image(this, BottomBar, BContainerSize, BContainerLocation, null, BContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var ButtonBColor = BottomBar.BackColor;
	    var ButtonFColor = Color.White;
	    var ButtonSize = new Size(100, 32);

	    var CloseLocation = new Point(0, 0);
	    var HelpLocation = new Point(ButtonSize.Width + 10, 0);

	    try
	    {
		Controls.Button(InnerBottomBarContainer, Help, ButtonSize, HelpLocation, ButtonBColor, ButtonFColor, 1, 10, "Help", Color.Empty);
		Controls.Button(InnerBottomBarContainer, Close, ButtonSize, CloseLocation, ButtonBColor, ButtonFColor, 1, 10, "Close", Color.Empty);

		Help.Click += (s, e) =>
		{
		    if (!SettingsInfoDialog.Visible)
		    {
			SettingsInfoDialog.ShowDialog();
		    }
		};

		Close.Click += (s, e) =>
		{
		    Hide();
		};

		foreach (Control control in InnerBottomBarContainer.Controls)
		{
		    if (control is Button)
		    {
			Tools.Round(control, 6);
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	    
	    var IContainerSize = new Size(210, ButtonSize.Height);
	    var IContainerLocation = new Point((BottomBar.Width - IContainerSize.Width) / 2, (BottomBar.Height - ButtonSize.Height) / 2 - 1);
	    var IContainerBColor = BContainerBColor;

	    try
	    {
		Controls.Image(BottomBar, InnerBottomBarContainer, IContainerSize, IContainerLocation, null, IContainerBColor);
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
    }
}
