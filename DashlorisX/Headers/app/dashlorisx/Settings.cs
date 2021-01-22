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

	readonly PictureBox ConfigurationContainer = new PictureBox();

	readonly TextBox UserAgentBox = new TextBox() { Text = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36" };
	readonly TextBox MethodBox = new TextBox() { Text = "POST", ReadOnly = true };
	readonly TextBox CookieBox = new TextBox() { Text = "Cookie=8f75a1acb808d4f709bc4d71b9ab0343" };
	readonly TextBox HTTPvBox = new TextBox() { Text = "1.1", ReadOnly = true };

	readonly Label UserAgentLabel = new Label();
	readonly Label MethodLabel = new Label();
	readonly Label CookieLabel = new Label();
	readonly Label HTTPvLabel = new Label();

	readonly DropDownMenu MethodMenu = new DropDownMenu();
	readonly DropDownMenu HTTPvMenu = new DropDownMenu();

	private void InitializeDropdownMenus()
	{
	    var MenuBorderBColor = Color.FromArgb(10, 10, 10);
	    var MenuBColor = Color.FromArgb(10, 10, 10);

	    var MenuItemBColor = MenuBColor;
	    var MenuItemFColor = Color.White;

	    Point GetMenuLocation(TextBox Object) =>
		(new Point(Object.Parent.Left + ConfigurationContainer.Left, Object.Parent.Top + Object.Parent.Height + ConfigurationContainer.Top));
	    
	    try
	    {
		HTTPvMenu.SetupMenu(this, GetMenuLocation(HTTPvBox), MenuBColor, MenuBorderBColor);

		HTTPvMenu.AddItem(new Label(), "1.0", MenuItemBColor, MenuItemFColor, ItemTextSize: 7, ItemWidth: 75, ItemHeight: 16);
		HTTPvMenu.AddItem(new Label(), "1.1", MenuItemBColor, MenuItemFColor, ItemTextSize: 7, ItemWidth: 75, ItemHeight: 16);
		HTTPvMenu.AddItem(new Label(), "1.2", MenuItemBColor, MenuItemFColor, ItemTextSize: 7, ItemWidth: 75, ItemHeight: 16);

		HTTPvMenu.Container.BringToFront();

		HTTPvBox.Click += (s, e) =>
		{
		    HTTPvMenu.Show();
		};
	    
		MethodMenu.SetupMenu(this, GetMenuLocation(MethodBox), MenuBColor, MenuBorderBColor);

		MethodMenu.AddItem(new Label(), "PUT", MenuItemBColor, MenuItemFColor, ItemTextSize: 7, ItemWidth: 75, ItemHeight: 16);
		MethodMenu.AddItem(new Label(), "POST", MenuItemBColor, MenuItemFColor, ItemTextSize: 7, ItemWidth: 75, ItemHeight: 16);
		MethodMenu.AddItem(new Label(), "GET", MenuItemBColor, MenuItemFColor, ItemTextSize: 7, ItemWidth: 75, ItemHeight: 16);
		MethodMenu.AddItem(new Label(), "HEAD", MenuItemBColor, MenuItemFColor, ItemTextSize: 7, ItemWidth: 75, ItemHeight: 16);

		MethodMenu.Container.BringToFront();

		MethodBox.Click += (s, e) =>
		{
		    MethodMenu.Show();
		};
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
	    var HTTPVButtonSize = new Size(75, 20);

	    var UserAgentLabelText = string.Format("User-Agent:");
	    var UserAgentLabelSize = Tools.GetFontSize(UserAgentLabelText, 10);
	    var UserAgentLabelLocation = new Point(HTTPVButtonLocation.X + HTTPVButtonSize.Width + 10, HTTPVLabelLocation.Y); // HTTPBox.Left + HTTPBox.Width + 10

	    var UserAgentButtonLocation = new Point(UserAgentLabelLocation.X + UserAgentLabelSize.Width, UserAgentLabelLocation.Y - 1);
	    var UserAgentButtonSize = new Size(CContainerSize.Width - UserAgentLabelLocation.X - UserAgentLabelSize.Width - (UserAgentButtonLocation.X - UserAgentButtonLocation.X) - (HTTPVLabelLocation.X), 20);

	    var MethodLabelText = string.Format("Method:");
	    var MethodLabelSize = Tools.GetFontSize(MethodLabelText, 10);
	    var MethodLabelLocation = new Point(HTTPVLabelLocation.X, HTTPVButtonLocation.Y + HTTPVButtonSize.Height + 5);

	    var MethodButtonLocation = new Point(MethodLabelLocation.X + MethodLabelSize.Width, MethodLabelLocation.Y - 1);
	    var MethodButtonSize = new Size(75, 20);

	    var CookieLabelText = string.Format("Cookie:");
	    var CookieLabelSize = Tools.GetFontSize(CookieLabelText, 10);
	    var CookieLabelLocation = new Point(MethodButtonLocation.X + MethodButtonSize.Width + 10, MethodLabelLocation.Y);

	    var CookieButtonLocation = new Point(CookieLabelLocation.X + CookieLabelSize.Width, CookieLabelLocation.Y - 1);
	    var CookieButtonSize = new Size(CContainerSize.Width - CookieLabelLocation.X - CookieLabelSize.Width - (CookieButtonLocation.X - CookieButtonLocation.X) - (MethodLabelLocation.X), 20);

	    try
	    {
		Controls.Label(ConfigurationContainer, HTTPvLabel, HTTPVLabelSize, HTTPVLabelLocation, LabelBColor, LabelFColor, 1, 10, HTTPVLabelText);
		Controls.Label(ConfigurationContainer, UserAgentLabel, UserAgentLabelSize, UserAgentLabelLocation, LabelBColor, LabelFColor, 1, 10, UserAgentLabelText);
		Controls.Label(ConfigurationContainer, MethodLabel, MethodLabelSize, MethodLabelLocation, LabelBColor, LabelFColor, 1, 10, MethodLabelText);
		Controls.Label(ConfigurationContainer, CookieLabel, CookieLabelSize, CookieLabelLocation, LabelBColor, LabelFColor, 1, 10, CookieLabelText);

		Controls.TextBox(ConfigurationContainer, HTTPvBox, HTTPVButtonSize, HTTPVButtonLocation, TextBoxBColor, TextBoxFColor, 1, 8, Color.Empty);
		Controls.TextBox(ConfigurationContainer, UserAgentBox, UserAgentButtonSize, UserAgentButtonLocation, TextBoxBColor, TextBoxFColor, 1, 8, Color.Empty);
		Controls.TextBox(ConfigurationContainer, MethodBox, MethodButtonSize, MethodButtonLocation, TextBoxBColor, TextBoxFColor, 1, 8, Color.Empty);
		Controls.TextBox(ConfigurationContainer, CookieBox, CookieButtonSize, CookieButtonLocation, TextBoxBColor, TextBoxFColor, 1, 8, Color.Empty);

		foreach (Control controlo in ConfigurationContainer.Controls)
		{
		    if (controlo is PictureBox)
		    {
			((TextBox)controlo.Controls[0]).TextAlign = HorizontalAlignment.Center;
			Tools.Round(controlo, 6);
		    }
		}

		Tools.Resize(ConfigurationContainer, new Size(CContainerSize.Width, CookieLabelLocation.Y + CookieLabelSize.Height + UserAgentButtonLocation.Y + 2));

		var RectangleSize = new Size(ConfigurationContainer.Width - 2, ConfigurationContainer.Height - 2);
		var RectangleLocation = new Point(1, 1);
		var RectangleBColor = Color.FromArgb(8, 8, 8);

		Tools.PaintRectangle(ConfigurationContainer, 2, RectangleSize, RectangleLocation, RectangleBColor);
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
	readonly Button Save = new Button();
	readonly Button Help = new Button();

	private void InitializeBottomBar()
	{
	    var BContainerSize = new Size(Width, 44);
	    var BContainerLocation = new Point(0, ConfigurationContainer.Top + ConfigurationContainer.Height + 11);
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

	    var CloseLocation = new Point(ButtonSize.Width * 2 + 20, 0);
	    var HelpLocation = new Point(ButtonSize.Width + 10, 0);
	    var SaveLocation = new Point(0, 0);

	    try
	    {
		Controls.Button(InnerBottomBarContainer, Save, ButtonSize, SaveLocation, ButtonBColor, ButtonFColor, 1, 10, "Save", Color.Empty);
		Controls.Button(InnerBottomBarContainer, Help, ButtonSize, HelpLocation, ButtonBColor, ButtonFColor, 1, 10, "Help", Color.Empty);
		Controls.Button(InnerBottomBarContainer, Close, ButtonSize, CloseLocation, ButtonBColor, ButtonFColor, 1, 10, "Close", Color.Empty);

		Close.Click += (s, e) =>
		{
		    Hide();
		};

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

	    var IContainerSize = new Size(InnerBottomBarContainer.Controls[InnerBottomBarContainer.Controls.Count - 1].Left + ButtonSize.Width, ButtonSize.Height);
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
