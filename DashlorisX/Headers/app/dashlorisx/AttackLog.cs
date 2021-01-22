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
    public class AttackLog : Form
    {
	new readonly DashControls Controls = new DashControls();

	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X  Attack Log", minim: false);
	readonly DashTools Tools = new DashTools();

	private void InitializeMenuBar()
	{
	    try
	    {
		var BAR_COLA = Color.FromArgb(19, 36, 64);
		MenuBar.Add(this, 26, BAR_COLA, BAR_COLA);
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

	readonly PictureBox BottomBarContainer = new PictureBox();
	readonly PictureBox BottomBar = new PictureBox();

	new readonly Button Close = new Button();// Close
	readonly Button Clear = new Button();// Clear

	private void InitializeBottomBar()
	{
	    var CONT_SIZE = new Size(Width - 2, 28);
	    var CONT_LOCA = new Point(1, Height - CONT_SIZE.Height);
	    var CONT_BCOL = MenuBar.Bar.BackColor;

	    try
	    {
		Controls.Image(this, BottomBar, CONT_SIZE, CONT_LOCA, null, CONT_BCOL);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var BCON_SIZE = new Size(180, 26);
	    var BCON_LOCA = new Point((CONT_SIZE.Width - BCON_SIZE.Width) / 2, (CONT_SIZE.Height - BCON_SIZE.Height) / 2);
	    var BCON_BCOL = CONT_BCOL;

	    try
	    {
		Controls.Image(BottomBar, BottomBarContainer, BCON_SIZE, BCON_LOCA, null, BCON_BCOL);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var BUTTON_SIZE = new Size(85, 26);
	    var BUTTON_BCOL = BCON_BCOL;
	    var BUTTON_FCOL = Color.White;

	    var CLOSE_LOCA = new Point(BUTTON_SIZE.Width + 10, 0);
	    var CLEAR_LOCA = new Point(0, 0);

	    try
	    {
		Controls.Button(BottomBarContainer, Clear, BUTTON_SIZE, CLEAR_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "Clear", Color.Empty);

		Clear.Click += (s, e) =>
		{
		    TextLog.Clear();
		};

		Controls.Button(BottomBarContainer, Close, BUTTON_SIZE, CLOSE_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "Close", Color.Empty);

		Close.Click += (s, e) =>
		{
		    Hide();
		};
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox InnerTextContainer = new PictureBox();
	readonly PictureBox TextContainer = new PictureBox();

	private static string GetFormat()
	{
	    return string.Format(
		"(+)>  Please make sure you have the consent of the server administrator, I am not responsible for any damage caused by this application.\r\n" +
		"(+)>  MAKE THE WORLD BURN!"
	    );
	}

	readonly public TextBox TextLog = new TextBox() { Text = GetFormat() };

	private void InitializeMainContainer()
	{
	    var CONT_SIZE = new Size(Width - 22, Height - MenuBar.Bar.Height - BottomBar.Height - 21);
	    var CONT_LOCA = new Point(11, MenuBar.Bar.Height + MenuBar.Bar.Top + 10);
	    var CONT_BCOL = Color.FromArgb(16, 16, 16);

	    try
	    {
		Controls.Image(this, TextContainer, CONT_SIZE, CONT_LOCA, null, CONT_BCOL);
		Tools.Round(TextContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var ICON_SIZE = new Size(CONT_SIZE.Width - 14, CONT_SIZE.Height - 14);
	    var ICON_LOCA = new Point(7, 7);
	    var ICON_BCOL = CONT_BCOL;

	    try
	    {
		Controls.Image(TextContainer, InnerTextContainer, ICON_SIZE, ICON_LOCA, null, ICON_BCOL);
		Tools.Round(InnerTextContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var TBOX_SIZE = ICON_SIZE;
	    var TBOX_LOCA = new Point(0, 0);
	    var TBOX_BCOL = CONT_BCOL;
	    var TBOX_FCOL = Color.White;

	    try
	    {
		Controls.TextBox(InnerTextContainer, TextLog, TBOX_SIZE, TBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 8, Color.Empty, READONLY: true, MULTILINE: true, SCROLLBAR: true, FIXEDSIZE: false);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    var RECT_SIZE = new Size(CONT_SIZE.Width - 4, CONT_SIZE.Height - 4);
	    var RECT_LOCA = new Point(2, 2);
	    var RECT_BCOL = Color.FromArgb(8, 8, 8);

	    try
	    {
		Tools.PaintRectangle(TextContainer, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public AttackLog()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeLayout();
		InitializeBottomBar();
		InitializeMainContainer();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(325, 250);
	    MinimumSize = new Size(325, 250);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Attack Log";
	    Tag = "DashlorisX Attack Log";
	    Name = "Attack Log";

	    Icon = Resources.ICON;

	    ResumeLayout(false);
	}
    }
}
