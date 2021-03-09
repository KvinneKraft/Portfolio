﻿
//
// All rights reserved to me Dashie for coding all of this.  If you wish to make use of this code, you can.
// Just make sure to leave this top part in if you are going to redistribute any part of my code.  Thank you.
//
// -Dashie

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Networking;
using DashFramework.Erroring;
using DashFramework.Dialog;

namespace DashFramework
{
    namespace Interface
    {
	namespace Controls
	{
	    public class DashControls
	    {

	    }
	}


	namespace Tools
	{
	    public class DashTools
	    {

	    }
	}
    }


    namespace Erroring
    {
	public class ErrorHandler
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();
	    
	    public static string GetRawFormat(Exception E)
	    {
		return string.Format
		(
		    $"An error has occurred and has prevented the application from functioning any further, safely.\r\n\r\nPlease send the following to KvinneKraft@protonmail.com if you wish to help me fix this issue.\r\n\r\n" +
		    $"----------------------\r\n" +
		    $"{E.StackTrace}\r\n" +
		    $"----------------------\r\n" +
		    $"{E.Message}\r\n" +
		    $"----------------------\r\n" +
		    $"{E.Source}\r\n" +
		    $"----------------------\r\n\r\n" +
		    $"I would also recommend making sure you actually downloaded the application from my website https://pugpawz.com and not some other sketchy website.\r\n\r\nAll the latest versions are available at my GitHub at https://github.com/KvinneKraft"
		);
	    }

	    public static Exception GetException(Exception E) =>
		new Exception(GetRawFormat(E));

	    public static void Utilize(string description, string title)
	    {
		DashBox ErrorDialog = new DashBox();

		Color ContainerBCol = Color.FromArgb(9, 39, 66);
		Color MenuBarBCol = Color.FromArgb(19, 36, 64);
		Color AppBCol = Color.FromArgb(6, 17, 33);

		ErrorDialog.ShowMe(description, title, AppBCol, MenuBarBCol, ContainerBCol, Color.White);
		
		Environment.Exit(-1);
	    }

	    public static void JustDoIt(Exception E, string title = ("Error Handler")) =>
		Utilize(GetRawFormat(E), title);
	}
    }


    namespace Dialog
    {
	public class DashWindow : Form
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();

	    private void InitS1(Size AppSize, string AppTitle, Color AppBCol, FormStartPosition AppStartPosition = FormStartPosition.CenterScreen, FormBorderStyle AppBorderStyle = FormBorderStyle.None)
	    {
		try
		{
		    SuspendLayout();

		    Icon = Properties.Resources.ICON;

		    FormBorderStyle = AppBorderStyle;
		    StartPosition = AppStartPosition;

		    MaximumSize = AppSize;
		    MinimumSize = AppSize;

		    BackColor = AppBCol;

		    Text = AppTitle;
		    Name = AppTitle;

		    Tool.Round(this, 6);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private readonly DashMenuBar MenuBar = null;

	    private void InitS2(string AppTitle, bool AppMinim, bool AppClose, bool AppHide, Color MenuBarBCol)
	    {
		try
		{
		    MenuBar = new DashMenuBar(AppTitle, AppMinim, AppClose, AppHide);
		    MenuBar.Add(this, 26, MenuBarBCol, MenuBarBCol);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private void DoInitializeApp(Size AppSize, string AppTitle, Color AppBCol, Color MenuBarBCol, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None, bool MenuBarMinim = false, bool MenuBarClose = true, bool CloseHideApp = true)
	    {
		try
		{
		    InitS1(AppSize, AppTitle, AppBCol, AppStartPosition: StartPosition, AppBorderStyle: FormBorderStyle);
		    InitS2(AppTitle, MenuBarMinim, MenuBarClose, CloseHideApp, MenuBarBCol);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private bool DoInitialize = true;

	    public void Show(Size AppSize, string AppTitle, Color AppBCol, Color MenuBarBCol, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None, bool ShowDialog = true, bool MenuBarMinim = false, bool MenuBarClose = true, bool CloseHideApp = true)
	    {
		try
		{
		    if (DoInitialize)
		    {
			DoInitializeApp(AppSize, AppTitle, AppBCol, MenuBarBCol, StartPosition, FormBorderStyle, MenuBarMinim, MenuBarClose, CloseHideApp);
			DoInitialize = false;
		    }

		    ShowAsIs(ShowDialog);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public void JustInitialize(Size AppSize, string AppTitle, Color AppBCol, Color MenuBarBCol, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None, bool MenuBarMinim = false, bool MenuBarClose = true, bool CloseHideApp = true)
	    {
		try
		{
		    DoInitializeApp(AppSize, AppTitle, AppBCol, MenuBarBCol, StartPosition, FormBorderStyle, MenuBarMinim: MenuBarMinim, MenuBarClose: MenuBarClose, CloseHideApp: CloseHideApp);
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


    namespace Networking
    {
	public class DashNet
	{
	    private readonly DashBox DashBox = new DashBox();
	    
	}
    }
}
