
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
    public class GameEditor
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	public readonly DashDialog DashDialog = new DashDialog();

	private void initializeComponent()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(8, 8, 8);
		var AppBColor = Color.FromArgb(9, 39, 66);
		var AppSize = new Size(300, 300);

		DashDialog.JustInitialize(AppSize, string.Format("GamePanel-X Editor   1.0"), AppBColor, MenuBarBColor, MenuBarMinim: true);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox SpecifyContainer = new PictureBox();
	private readonly PictureBox RunasContainer = new PictureBox();
	private readonly PictureBox RunasBox = new PictureBox();

	private readonly Color UncheckColor = Color.FromArgb(16, 16, 16);
	private readonly Color CheckColor = Color.FromArgb(8, 8, 8);

	private readonly TextBox ExecutionParameters = new TextBox();
	private readonly TextBox StartDirectory = new TextBox();
	private readonly TextBox DisplayName = new TextBox();
	private readonly TextBox FilePath = new TextBox();

	private Label GetLatestLabel()
	{
	    Label result = null;

	    if (SpecifyContainer.Controls.Count > 0)
	    {
		result = SpecifyContainer.Controls[SpecifyContainer.Controls.Count - 1] as Label;
	    }

	    return (result);
	}

	private void initializeContainer()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox ButtonContainer = new PictureBox();
	private readonly PictureBox BottomBar = new PictureBox();

	private readonly Button Cancel = new Button();
	private readonly Button Verify = new Button();
	private readonly Button Save = new Button();

	private void initializeBottomBar()
	{
	    try
	    {
		var BottomBarSize = new Size(DashDialog.Width, 30);
		var BottomBarLoca = new Point(0, DashDialog.Height - BottomBarSize.Height);
		var BottomBarBColor = DashDialog.MenuBar.Bar.BackColor;

		Control.Image(DashDialog, BottomBar, BottomBarSize, BottomBarLoca, BottomBarBColor);

		var ButtonSize = new Size(75, 26);
		var ButtonBColor = BottomBarBColor;
		var ButtonFColor = Color.White;

		var CancelLoca = new Point(ButtonSize.Width * 2 + 20, 0);
		var VerifyLoca = new Point(ButtonSize.Width + 10, 0);
		var SaveLoca = new Point(0, 0);

		Control.Button(ButtonContainer, Cancel, ButtonSize, CancelLoca, ButtonBColor, ButtonFColor, 1, 9, "Cancel");
		Control.Button(ButtonContainer, Verify, ButtonSize, VerifyLoca, ButtonBColor, ButtonFColor, 1, 9, "Verify");
		Control.Button(ButtonContainer, Save, ButtonSize, SaveLoca, ButtonBColor, ButtonFColor, 1, 9, "Save");

		Cancel.Click += (s, e) =>
		{
		    Hide();
		};

		Verify.Click += (s, e) =>
		{
		    // Do game-format verification process.
		    // Check file paths etc
		};

		Save.Click += (s, e) =>
		{
		    // Write new to config, then DashConfig.GamePanel, pass if necessary.
		    Hide();
		};

		var ButtonContainerSize = new Size((ButtonSize.Width * ButtonContainer.Controls.Count) + (ButtonContainer.Controls.Count * 10) - 10, ButtonSize.Height);
		var ButtonContainerLoca = new Point(-2, -2);
		var ButtonContainerBColor = BottomBarBColor;

		Control.Image(BottomBar, ButtonContainer, ButtonContainerSize, ButtonContainerLoca, ButtonContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void CheckDefaults(string parameters, string filename, string gamename, string directory, string runas)
	{
	    try
	    {
		if (gamename != "none" && runas != "none")
		{
		    ExecutionParameters.Text = parameters;
		    StartDirectory.Text = directory;
		    DisplayName.Text = gamename;
		    FilePath.Text = filename;

		    if (runas.ToLower() == "true")
		    {
			RunasBox.BackColor = CheckColor;
		    }

		    else
		    {
			RunasBox.BackColor = UncheckColor;
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private bool mustInitialize = true;

	public void Show(string parameters = "none", string filename = "none", string gamename = "none", string directory = "none", string runas = "none")
	{
	    try
	    {
		if (mustInitialize)
		{
		    initializeComponent();
		    initializeContainer();
		    initializeBottomBar();

		    mustInitialize = false;
		}

		CheckDefaults(parameters, filename, gamename, directory, runas);
		// change parameters, filename, gamename, directory and runas from here rather than through initialzie container.

		DashDialog.ShowAsIs(ShowDialog:false);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}

	public void Hide()
	{
	    try
	    {
	        DashDialog.Hide();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
