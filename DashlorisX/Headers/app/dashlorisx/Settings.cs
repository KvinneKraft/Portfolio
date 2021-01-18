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
    public partial class Settings : Form
    {
	new readonly DashControls Controls = new DashControls();
	readonly DashMenuBar MenuBar = new DashMenuBar("Dashloris-X   Settings", minim:false);
	readonly DashTools Tools = new DashTools();

	private void InitializeMenuBar()
	{
	    try
	    {
		var BAR_COLA = Color.FromArgb(8, 8, 8);
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

	readonly PictureBox ConfigurationContainer = new PictureBox();

	readonly TextBox UserAgentBox = new TextBox();
	readonly TextBox MethodBox = new TextBox();
	readonly TextBox CookieBox = new TextBox();
	readonly TextBox HTTPvBox = new TextBox();

	readonly Label UserAgentLabel = new Label();
	readonly Label MethodLabel = new Label();
	readonly Label CookieLabel = new Label();
	readonly Label HTTPvLabel = new Label();

	private void InitializeSettings()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox InnerBottomBarContainer = new PictureBox();
	readonly PictureBox BottomBar = new PictureBox();

	readonly Button Cancel = new Button();
	readonly Button Save = new Button();
	readonly Button Help = new Button();

	private void InitializeBottomBar()
	{
	    try
	    {
		// Location Y: ConfigurationContainer.Top + ConfigurationContainer.Height + 10
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void ReinitializeApp()
	{
	    try
	    {
		// Resize to preferred size: BottomBar.Top + BottomBar.Height
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public Settings()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeLayout();
		InitializeSettings();
		InitializeBottomBar();
		ReinitializeApp();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}

	private void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(300, 250);
	    MinimumSize = new Size(300, 250);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    Text = "DashlorisX Settings";
	    Tag = "DashlorisX Settings";
	    Name = "Settings";

	    Icon = Resources.ICON;

	    ResumeLayout(false);
	}
    }
}
