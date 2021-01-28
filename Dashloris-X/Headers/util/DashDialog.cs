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

using DashlorisX.Properties;

namespace DashlorisX
{
    public class DashDialog : Form
    {
	private new readonly DashControls Controls = new DashControls();
	private readonly DashTools Tools = new DashTools();

	private bool DoInitialize = true;

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

		Tools.Round(this, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private DashMenuBar MenuBar = null;

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

	public void Show(Size AppSize, string AppTitle, Color AppBColor, Color MenuBarBColor, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None, bool ShowDialog = true, bool MenuBarMinim = false, bool MenuBarClose = true, bool CloseHideApp = true)
	{
	    try
	    {
		if (DoInitialize)
		{
		    InitializeComponent(AppSize, AppTitle, AppBColor, StartPosition:StartPosition, FormBorderStyle:FormBorderStyle);
		    InitializeMenuBar(AppTitle, MenuBarMinim, MenuBarClose, CloseHideApp, MenuBarBColor);
		}

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

	// Add Bottom Bar method thing
    }
}
