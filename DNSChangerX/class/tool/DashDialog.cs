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
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;
using System.Collections.Generic;

using DNSChangerX.Properties;

namespace DNSChangerX
{
    public class DashDialog : Form
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	private void InitializeComponent(Size AppSize, string AppTitle, Color AppBColor, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None)
	{
	    try
	    {
		SuspendLayout();

		MaximumSize = AppSize;
		MinimumSize = AppSize;

		this.FormBorderStyle = FormBorderStyle;
		this.StartPosition = StartPosition;

		Text = AppTitle;
		Name = AppTitle;

		Icon = Resources.ICON;

		BackColor = AppBColor;

		Tool.Round(this, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public DashMenuBar MenuBar = null;

	private void InitializeMenuBar(string AppTitle, bool AppMinim, bool AppClose, bool AppHide, Color MenuBarBColor)
	{
	    try
	    {
		MenuBar = new DashMenuBar(AppTitle, AppMinim, AppClose, AppHide);
		MenuBar.Add(this, 26, MenuBarBColor, MenuBarBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void DoInitializeApp(Size AppSize, string AppTitle, Color AppBColor, Color MenuBarBColor, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None, bool MenuBarMinim = false, bool MenuBarClose = true, bool CloseHideApp = true)
	{
	    try
	    {
		InitializeComponent(AppSize, AppTitle, AppBColor, StartPosition: StartPosition, FormBorderStyle: FormBorderStyle);
		InitializeMenuBar(AppTitle, MenuBarMinim, MenuBarClose, CloseHideApp, MenuBarBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	
	private bool DoInitialize = true;

	public void Show(Size AppSize, string AppTitle, Color AppBColor, Color MenuBarBColor, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None, bool ShowDialog = true, bool MenuBarMinim = false, bool MenuBarClose = true, bool CloseHideApp = true)
	{
	    try
	    {
		if (DoInitialize)
		{
		    DoInitializeApp(AppSize, AppTitle, AppBColor, MenuBarBColor, StartPosition, FormBorderStyle, MenuBarMinim, MenuBarClose, CloseHideApp);
		    DoInitialize = false;
		}

		ShowAsIs(ShowDialog);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void JustInitialize(Size AppSize, string AppTitle, Color AppBColor, Color MenuBarBColor, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None, bool MenuBarMinim = false, bool MenuBarClose = true, bool CloseHideApp = true)
	{
	    try
	    {
		DoInitializeApp(AppSize, AppTitle, AppBColor, MenuBarBColor, StartPosition, FormBorderStyle,  MenuBarMinim:MenuBarMinim, MenuBarClose:MenuBarClose, CloseHideApp:CloseHideApp);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void ShowAsIs(bool ShowDialog = true)
	{
	    try
	    {
		if (ShowDialog)
		{
		    this.ShowDialog();
		}

		else
		{
		    Show();
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
