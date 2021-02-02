﻿
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

	private void initializeMainComponent()
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

	private readonly DashConfig GameConfig = new DashConfig();
	private readonly Form MainContainer = new Form();
	private readonly Label NoGames = new Label();

	private class DashConfig
	{
	    public readonly Dictionary<int, List<string>> GameData = new Dictionary<int, List<string>>();

	    public void ReadGames()
	    {
		try
		{
		    GameData.Clear();

		    var RawData = File.ReadAllLines("gameData\\games.config", );
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}

	private void initializeMainContainer()
	{
	    try
	    {
		Tool.Resize(MainContainer, new Size(DashDialog.Width - 2, DashDialog.Height - DashDialog.MenuBar.Bar.Height));

		MainContainer.Location = new Point(1, DashDialog.MenuBar.Bar.Height - 1);
		MainContainer.BackColor = Color.FromArgb(10, 10, 10);

		MainContainer.FormBorderStyle = FormBorderStyle.None;
		MainContainer.TopLevel = false;

		MainContainer.VerticalScroll.Enabled = true;
		MainContainer.VerticalScroll.Visible = true;

		MainContainer.Show();
		
		DashDialog.Controls.Add(MainContainer);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var NoGamesText = string.Format("No Games have yet been added :c\r\nClick this text to add one right now!");
	    var NoGamesLocation = new Point(-2, -2);
	    var NoGamesBColor = MainContainer.BackColor;
	    var NoGamesFColor = Color.White;

	    try
	    {
		Control.Label(MainContainer, NoGames, Size.Empty, NoGamesLocation, NoGamesBColor, NoGamesFColor, 1, 12, NoGamesText);

		NoGames.Left -= 8;
		NoGames.Top -= 8;

		NoGames.TextAlign = ContentAlignment.MiddleCenter;
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
		initializeMainComponent();
		initializeMainContainer();

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
