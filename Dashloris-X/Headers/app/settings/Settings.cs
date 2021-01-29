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
    public partial class Settings
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	public readonly DashDialog DashDialog = new DashDialog();

	private void InitializeComponent()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		var AppBColor = Color.FromArgb(6, 17, 33);
		var AppTitle = string.Format("Dashloris-X   Settings");
		var AppSize = new Size(450, 250);

		DashDialog.JustInitialize(AppSize, AppTitle, AppBColor, MenuBarBColor, StartPosition:FormStartPosition.CenterParent);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox ConfigurationContainer = new PictureBox();//This shit hurts my eyes help.

	private readonly DropDownMenu MethodMenu = new DropDownMenu();
	private readonly DropDownMenu HTTPvMenu = new DropDownMenu();

	public readonly static TextBox UserAgentBox = new TextBox() { Text = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36" };
	public readonly static TextBox CookieBox = new TextBox() { Text = "Cookie=cookie data man." };
	public readonly static TextBox MethodBox = new TextBox() { Text = "POST", ReadOnly = true };
	public readonly static TextBox HTTPvBox = new TextBox() { Text = "HTTP/1.1", ReadOnly = true };

	private readonly Label UserAgentLabel = new Label();
	private readonly Label MethodLabel = new Label();
	private readonly Label CookieLabel = new Label();
	private readonly Label HTTPvLabel = new Label();

	private void InitializeDropdownMenus()
	{
	    var MenuBorderBColor = MethodBox.BackColor;
	    var MenuBColor = MethodBox.BackColor;

	    Point GetMenuLocation(TextBox Object) =>
		(new Point(Object.Parent.Left + ConfigurationContainer.Left + 1, Object.Parent.Top + Object.Parent.Height + ConfigurationContainer.Top - 5));

	    try
	    {
		MethodMenu.SetupMenu(DashDialog, GetMenuLocation(MethodBox), MenuBColor, MenuBorderBColor);
		HTTPvMenu.SetupMenu(DashDialog, GetMenuLocation(HTTPvBox), MenuBColor, MenuBorderBColor);

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

		Tool.Round(MethodMenu.Container, 6);
		Tool.Round(HTTPvMenu.Container, 6);

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
		throw (ErrorHandler.GetException(E));
	    }

	    var MenuItemBColor = MenuBColor;
	    var MenuItemFColor = Color.White;

	    Label GetLabel() => new Label();
	    
	    int GetID(int id)
	    {
		switch (id)
		{
		    case 0: return MethodMenu.ContentContainer.Controls.Count - 1;
		    case 1: return HTTPvMenu.ContentContainer.Controls.Count - 1;
		    default: return -1;
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
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void InitializeSettings()
	{
	    var CContainerSize = new Size(DashDialog.Width - 20, 70);
	    var CContainerLocation = new Point(10, DashDialog.MenuBar.Bar.Height + DashDialog.MenuBar.Bar.Top + 10);
	    var CContainerBColor = Color.FromArgb(9, 39, 66);//16, 16, 16);

	    try
	    {
		Control.Image(DashDialog, ConfigurationContainer, CContainerSize, CContainerLocation, CContainerBColor);
		Tool.Round(ConfigurationContainer, 6);
	    }

	    catch
	    {
		throw new Exception("Option Container");
	    }

	    var LabelBColor = ConfigurationContainer.BackColor;
	    var LabelFColor = Color.White;

	    var HTTPVLabelText = string.Format("HTTP Version:");
	    var HTTPVLabelSize = Tool.GetFontSize(HTTPVLabelText, 10);
	    var HTTPVLabelLocation = new Point(10, 11);

	    var TextBoxBColor = Color.FromArgb(10, 10, 10);
	    var TextBoxFColor = Color.White;

	    var HTTPVButtonLocation = new Point(HTTPVLabelLocation.X + HTTPVLabelSize.Width, HTTPVLabelLocation.Y - 1);
	    var HTTPVButtonSize = new Size(75, 19);

	    var UserAgentLabelText = string.Format("User-Agent:");
	    var UserAgentLabelSize = Tool.GetFontSize(UserAgentLabelText, 10);
	    var UserAgentLabelLocation = new Point(HTTPVButtonLocation.X + HTTPVButtonSize.Width + 10, HTTPVLabelLocation.Y); // HTTPBox.Left + HTTPBox.Width + 10

	    var UserAgentButtonLocation = new Point(UserAgentLabelLocation.X + UserAgentLabelSize.Width, UserAgentLabelLocation.Y - 1);
	    var UserAgentButtonSize = new Size(CContainerSize.Width - UserAgentLabelLocation.X - UserAgentLabelSize.Width - (UserAgentButtonLocation.X - UserAgentButtonLocation.X) - (HTTPVLabelLocation.X), 19);

	    var MethodLabelText = string.Format("Method:");
	    var MethodLabelSize = Tool.GetFontSize(MethodLabelText, 10);
	    var MethodLabelLocation = new Point(HTTPVLabelLocation.X, HTTPVButtonLocation.Y + HTTPVButtonSize.Height + 5);

	    var MethodButtonLocation = new Point(MethodLabelLocation.X + MethodLabelSize.Width, MethodLabelLocation.Y - 1);
	    var MethodButtonSize = new Size(75, 19);

	    var CookieLabelText = string.Format("Cookie:");
	    var CookieLabelSize = Tool.GetFontSize(CookieLabelText, 10);
	    var CookieLabelLocation = new Point(MethodButtonLocation.X + MethodButtonSize.Width + 10, MethodLabelLocation.Y);

	    var CookieButtonLocation = new Point(CookieLabelLocation.X + CookieLabelSize.Width, CookieLabelLocation.Y - 1);
	    var CookieButtonSize = new Size(CContainerSize.Width - CookieLabelLocation.X - CookieLabelSize.Width - (CookieButtonLocation.X - CookieButtonLocation.X) - (MethodLabelLocation.X), 19);

	    try
	    {
		Control.Label(ConfigurationContainer, HTTPvLabel, HTTPVLabelSize, HTTPVLabelLocation, LabelBColor, LabelFColor, 1, 10, HTTPVLabelText);
		Control.Label(ConfigurationContainer, UserAgentLabel, UserAgentLabelSize, UserAgentLabelLocation, LabelBColor, LabelFColor, 1, 10, UserAgentLabelText);
		Control.Label(ConfigurationContainer, MethodLabel, MethodLabelSize, MethodLabelLocation, LabelBColor, LabelFColor, 1, 10, MethodLabelText);
		Control.Label(ConfigurationContainer, CookieLabel, CookieLabelSize, CookieLabelLocation, LabelBColor, LabelFColor, 1, 10, CookieLabelText);

		Control.TextBox(ConfigurationContainer, HTTPvBox, HTTPVButtonSize, HTTPVButtonLocation, TextBoxBColor, TextBoxFColor, 1, 9);
		Control.TextBox(ConfigurationContainer, UserAgentBox, UserAgentButtonSize, UserAgentButtonLocation, TextBoxBColor, TextBoxFColor, 1, 9);
		Control.TextBox(ConfigurationContainer, MethodBox, MethodButtonSize, MethodButtonLocation, TextBoxBColor, TextBoxFColor, 1, 9);
		Control.TextBox(ConfigurationContainer, CookieBox, CookieButtonSize, CookieButtonLocation, TextBoxBColor, TextBoxFColor, 1, 9);

		foreach (Control controlo in ConfigurationContainer.Controls)
		{
		    if (controlo is PictureBox)
		    {
			((TextBox)controlo.Controls[0]).TextAlign = HorizontalAlignment.Center;
			Tool.Round(controlo, 6);
		    }
		}

		Tool.Resize(ConfigurationContainer, new Size(CContainerSize.Width, CookieLabelLocation.Y + CookieLabelSize.Height + UserAgentButtonLocation.Y + 2));
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    InitializeDropdownMenus();
	}

	private readonly PictureBox InnerBottomBarContainer = new PictureBox();
	private readonly PictureBox BottomBar = new PictureBox();

	private readonly Button Close = new Button();
	private readonly Button Help = new Button();

	private readonly SettingsInfo SettingsInfoDialog = new SettingsInfo();

	private void InitializeBottomBar()
	{
	    var BContainerSize = new Size(DashDialog.Width, 44);
	    var BContainerLocation = new Point(0, ConfigurationContainer.Top + ConfigurationContainer.Height + 11);//26);
	    var BContainerBColor = DashDialog.MenuBar.Bar.BackColor;

	    try
	    {
		Control.Image(DashDialog, BottomBar, BContainerSize, BContainerLocation, BContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var ButtonBColor = BottomBar.BackColor;
	    var ButtonFColor = Color.White;
	    var ButtonSize = new Size(100, 32);

	    var CloseLocation = new Point(0, 0);
	    var HelpLocation = new Point(ButtonSize.Width + 10, 0);

	    try
	    {
		Control.Button(InnerBottomBarContainer, Close, ButtonSize, CloseLocation, ButtonBColor, ButtonFColor, 1, 11, "Close");
		Control.Button(InnerBottomBarContainer, Help, ButtonSize, HelpLocation, ButtonBColor, ButtonFColor, 1, 11, "Help");

		Help.Click += (s, e) =>
		{
		    if (SettingsInfoDialog.InfoContainer == null || !SettingsInfoDialog.InfoContainer.Visible)
		    {
			SettingsInfoDialog.Show();
		    }
		};

		Close.Click += (s, e) =>
		{
		    DashDialog.Hide();
		};

		foreach (Control control in InnerBottomBarContainer.Controls)
		{
		    if (control is Button)
		    {
			Tool.Round(control, 6);
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	    
	    var IContainerSize = new Size(210, ButtonSize.Height);
	    var IContainerLocation = new Point((BottomBar.Width - IContainerSize.Width) / 2, (BottomBar.Height - ButtonSize.Height) / 2 - 1);
	    var IContainerBColor = BContainerBColor;

	    try
	    {
		Control.Image(BottomBar, InnerBottomBarContainer, IContainerSize, IContainerLocation, IContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private bool DoInitialize = true;

	public void Show()
	{
	    try
	    {
		if (DoInitialize)
		{
		    InitializeComponent();
		    InitializeSettings();
		    InitializeBottomBar();

		    Tool.Resize(DashDialog, new Size(DashDialog.Width, BottomBar.Top + BottomBar.Height));

		    DoInitialize = false;
		}

		DashDialog.ShowAsIs(ShowDialog:true);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
