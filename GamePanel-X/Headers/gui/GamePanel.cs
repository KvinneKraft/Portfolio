
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
		var AppBColor = Color.FromArgb(24, 24, 24);
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
	public readonly PictureBox GameContainer = new PictureBox();

	private void setupCheckBoxContainer()
	{
	    try
	    {
		var CheckBoxContainerSize = new Size(28, DashDialog.Height - 2);
		var CheckBoxContainerLoca = new Point(1, 1);
		var CheckBoxContainerBColor = Color.FromArgb(24, 24, 24);

		Control.Image(MainContainer, CheckBoxContainer, CheckBoxContainerSize, CheckBoxContainerLoca, CheckBoxContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void setupGameContainer()
	{
	    try
	    {

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
		var NoGamesMessageLoca = new Point((MainContainer.Width - NoGamesMessageSize.Width) / 2 + 5, (MainContainer.Height - NoGamesMessageSize.Height) / 2 - 5);

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
    }
}
