
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
    public class GamePanel
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	public readonly DashDialog DashDialog = new DashDialog();

	public void initializeMainComponent()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(8, 8, 8);
		var AppBColor = Color.FromArgb(8, 8, 8);
		var AppSize = new Size(350, 350);

		DashDialog.JustInitialize(AppSize, string.Format("GamePanel-X  1.0"), AppBColor, MenuBarBColor, MenuBarMinim: true, CloseHideApp: false);

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

	public readonly PictureBox CheckBoxContainer = new PictureBox();

	private void setupCheckBoxContainer()
	{
	    try
	    {
		var CheckBoxContainerSize = new Size(28, DashDialog.Height - 2);
		var CheckBoxContainerLoca = new Point(0, 1);
		var CheckBoxContainerBColor = Color.FromArgb(8, 8, 8);

		Control.Image(MainContainer, CheckBoxContainer, CheckBoxContainerSize, CheckBoxContainerLoca, CheckBoxContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public readonly PictureBox GameContainer = new PictureBox();

	private void setupGameContainer()
	{
	    try
	    {
		var GameContainerSize = new Size(MainContainer.Width - CheckBoxContainer.Width - CheckBoxContainer.Left - 21, MainContainer.Height - 2);
		var GameContainerLoca = new Point(CheckBoxContainer.Width + CheckBoxContainer.Left + 1, 2);
		var GameContainerBColor = Color.FromArgb(9, 39, 66);
		
		Control.Image(MainContainer, GameContainer, GameContainerSize, GameContainerLoca, GameContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public readonly Label NoGamesMessage = new Label();
	public readonly Form MainContainer = new Form();

	public void initializeMainContainer()
	{
	    try
	    {
		Tool.Resize(MainContainer, new Size(DashDialog.Width - 4, DashDialog.Height - DashDialog.MenuBar.Bar.Height - 30));

		MainContainer.Location = new Point(2, DashDialog.MenuBar.Bar.Height - 1);
		MainContainer.BackColor = Color.FromArgb(9, 39, 66);

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

	    try
	    {
		setupCheckBoxContainer();
		setupGameContainer();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    try
	    {
		var NoGamesMessageText = string.Format("No Games have yet been added :c\r\nClick this text to add one right now!");
		var NoGamesMessageSize = TextRenderer.MeasureText(NoGamesMessageText, Tool.GetFont(1, 12));
		var NoGamesMessageLoca = new Point((MainContainer.Width - NoGamesMessageSize.Width) / 2 - 8, (MainContainer.Height - NoGamesMessageSize.Height) / 2 - 5);

		var NoGamesBColor = MainContainer.BackColor;
		var NoGamesFColor = Color.White;

		Control.Label(MainContainer, NoGamesMessage, Size.Empty, NoGamesMessageLoca, NoGamesBColor, NoGamesFColor, 1, 12, NoGamesMessageText);

		NoGamesMessage.TextAlign = ContentAlignment.MiddleCenter;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox BottomContainer = new PictureBox();
	private readonly PictureBox ButtonContainer = new PictureBox();

	private readonly Button Remove = new Button();
	private readonly Button Games = new Button();
	private readonly Button Add = new Button();

	private readonly GameEditor GameEditor = new GameEditor();

	public void initializeMainBottomBar()
	{
	    try
	    {
		var BottomContainerSize = new Size(DashDialog.Width, 30);
		var BottomContainerLoca = new Point(0, DashDialog.Height - BottomContainerSize.Height - 1);
		var BottomContainerBColor = DashDialog.MenuBar.Bar.BackColor;

		Control.Image(DashDialog, BottomContainer, BottomContainerSize, BottomContainerLoca, BottomContainerBColor);

		var ButtonSize = new Size(70, BottomContainerSize.Height - 8);
		var ButtonBColor = BottomContainerBColor;
		var ButtonFColor = Color.White;

		var Button3Loca = new Point(160, 0);
		var Button2Loca = new Point(80, 0);
		var Button1Loca = new Point(0, 0);

		Control.Button(ButtonContainer, Remove, ButtonSize, Button3Loca, ButtonBColor, ButtonFColor, 1, 9, "Remove");
		Control.Button(ButtonContainer, Games, ButtonSize, Button2Loca, ButtonBColor, ButtonFColor, 1, 9, "Games");
		Control.Button(ButtonContainer, Add, ButtonSize, Button1Loca, ButtonBColor, ButtonFColor, 1, 9, "Add");

		Remove.Click += (s, e) =>
		{
		    // Remove game from config and gui.
		};

		Games.Click += (s, e) =>
		{
		    // Show a dialog with websites where you can download games for free.
		};

		Add.Click += (s, e) =>
		{
		    if (!GameEditor.DashDialog.Visible)
		    {
			GameEditor.Show();
		    }

		    else
		    {
			GameEditor.Hide();
		    }
		};

		var ButtonContainerSize = new Size(Button3Loca.X + ButtonSize.Width, ButtonSize.Height);
		var ButtonContainerLoca = new Point((BottomContainer.Width - ButtonContainerSize.Width) / 2 - 8, (BottomContainer.Height - ButtonContainerSize.Height) / 2);
		var ButtonContainerBColor = BottomContainerBColor;

		Control.Image(BottomContainer, ButtonContainer, ButtonContainerSize, ButtonContainerLoca, ButtonContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
