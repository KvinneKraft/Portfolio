
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace GamePanelX
{
    public partial class GamePanelX
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	private readonly DashDialog DashDialog = new DashDialog();

	private void InitializeMainComponent()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(8, 8, 8);
		var AppBColor = Color.FromArgb(24, 24, 24);
		var AppSize = new Size(350, 350);

		DashDialog.JustInitialize(AppSize, string.Format("GamePanel-X  1.0"), AppBColor, MenuBarBColor, MenuBarMinim:true, CloseHideApp:false);

		DashDialog.MenuBar.Close.Click += (s, e) =>
		{
		    Application.Exit();
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly Form MainContainer = new Form();

	private void InitializeMainContainer()
	{
	    try
	    {
		var MainContainerSize = new Size(DashDialog.Width - 20, DashDialog.Height - 20 - DashDialog.MenuBar.Bar.Height);
	
		MainContainer.Location = new Point(10, 10 + DashDialog.MenuBar.Bar.Height - 1);
		MainContainer.BackColor = Color.FromArgb(10, 10, 10);

		MainContainer.FormBorderStyle = FormBorderStyle.None;
		MainContainer.TopLevel = false;

		Tool.Resize(MainContainer, MainContainerSize);
		Tool.Round(MainContainer, 6);

		MainContainer.Show();

		DashDialog.Controls.Add(MainContainer);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox BottomBarInnerContainer = new PictureBox();
	private readonly PictureBox BottomBar = new PictureBox();

	private readonly Button Remove = new Button();
	private readonly Button Games = new Button();
	private readonly Button Add = new Button();

	private void InitializeBottomMenuBar()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void StartApp()
	{
	    try
	    {
		InitializeMainComponent();
		InitializeMainContainer();
		InitializeBottomMenuBar();

		Application.Run(DashDialog);
		Environment.Exit(-1);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
