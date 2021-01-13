//man ye has to fix dis u shit.
//
// Author: Dashie
// Version: 1.0
//

using System;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class MethodConfig : Form
    {
	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	private void InitializeComponent()
	{
	    SuspendLayout();

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterScreen;

	    Icon = Properties.Resources.ICON_ICO;
	    BackColor = Color.MidnightBlue;

	    TOOL.Resize(this, new Size(250, 300));

	    Text = "Method Configurator";
	    Name = "MethodConfig";

	    ResumeLayout(false);
	}

	readonly static PictureBox BAR = new PictureBox();

	readonly static Button SELECT = new Button();
	readonly static Button CLOSE = new Button();

	readonly static Label TITLE = new Label();

	private void InitializeMenuBar()
	{
	    try
	    {
		var BAR_SIZE = new Size(Width, 26);
		var BAR_LOCA = new Point(0, 0);
		var BAR_BCOL = Color.FromArgb(8, 8, 8);

		CONTROL.Image(this, BAR, BAR_SIZE, BAR_LOCA, null, BAR_BCOL);
		TOOL.Interactive(BAR, this);

		var CLOSE_SIZE = new Size(65, 26);
		var CLOSE_LOCA = new Point(BAR_SIZE.Width - CLOSE_SIZE.Width - 1, 0);
		var CLOSE_BCOL = BAR_BCOL;
		var CLOSE_FCOL = Color.White;

		CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 10, "X", Color.Empty);
		CLOSE.Click += (s, e) => Hide();

		var TITLE_SIZE = TOOL.GetFontSize(Text, 9);
		var TITLE_LOCA = new Point(10, (BAR_SIZE.Height - TITLE_SIZE.Height) / 2);
		var TITLE_BCOL = BAR_BCOL;
		var TITLE_FCOL = Color.White;

		CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 9, Text);

		var SELECT_SIZE = new Size(100, 26);
		var SELECT_LOCA = new Point((Width - SELECT_SIZE.Width) / 2, 0);
		var SELECT_BCOL = Color.FromArgb(10, 10, 10);
		var SELECT_FCOL = Color.White;

		CONTROL.Button(this, SELECT, SELECT_SIZE, SELECT_LOCA, SELECT_BCOL, SELECT_FCOL, 1, 11, "Select", Color.Empty);

		SELECT.Click += (s, q) => Hide();

		TOOL.Round(SELECT, 6);

		foreach (Control control in BAR.Controls)
		{
		    TOOL.Interactive(control, this);
		};

		var RECT_SIZE = new Size(Width - 1, Height - BAR_SIZE.Height);
		var RECT_LOCA = new Point(0, BAR_SIZE.Height - 1);
		var RECT_BCOL = BAR_BCOL;

		TOOL.PaintRectangle(this, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	    }

	    catch
	    {
		throw new Exception("InitializeMenuBar");
	    }
	}

	readonly static Form CONTAINER = new Form();

	public void MultiForm(Form TOP, Form FORM, Size SIZE, Point LOCA, Color BCOL)
	{
	    TOOL.Resize(FORM, SIZE);

	    if (LOCA.X < 0)
	    {
		LOCA.X = (TOP.Width - SIZE.Width) / 2;
	    };

	    if (LOCA.Y < 0)
	    {
		LOCA.Y = ((TOP.Height + BAR.Height) - SIZE.Height) / 2;
	    };

	    FORM.BackColor = BCOL;
	    FORM.Location = LOCA;

	    FORM.FormBorderStyle = FormBorderStyle.None;
	    FORM.TopLevel = false;

	    TOP.Controls.Add(FORM);

	    FORM.Show();
	}

	// METHOD_CONFIGURATION["HTTP->DASHLORIS4"]["CONNECTION_BURST_DELAY"];
	readonly HTTP http = new HTTP();
	readonly TCP tcp = new TCP();
	readonly UDP udp = new UDP();

	readonly PictureBox RECTUM = new PictureBox();

	private void InitializeConfCon()
	{
	    try
	    {
		var CONT_SIZE = new Size(Width - 20, Height - BAR.Height - 20);
		var CONT_LOCA = new Point(-1, -1);
		var CONT_BCOL = BAR.BackColor;

		MultiForm(this, CONTAINER, CONT_SIZE, CONT_LOCA, CONT_BCOL);

		var RECT_SIZE = new Size(CONT_SIZE.Width + 1, CONT_SIZE.Height + 1);
		var RECT_LOCA = new Point(CONTAINER.Left - 1, CONTAINER.Top - 1);
		var RECT_COLA = BAR.BackColor;

		CONTROL.Image(this, RECTUM, RECT_SIZE, RECT_LOCA, null, RECT_COLA);
	    }

	    catch
	    {
		throw new Exception("InitializeConfCon[CONTAINER]");
	    };

	    try
	    {
		try
		{
		    http.InitializeHTTPConfiguration(CONTAINER);
		}

		catch (Exception e)
		{
		    throw new Exception($"HTTP_MODULE -> {e.Message}");
		};

		try
		{
		    tcp.InitializeTCPConfiguration(CONTAINER);
		}

		catch (Exception e)
		{
		    throw new Exception($"TCP_MODULE -> {e.Message}");
		};

		try
		{
		    udp.InitializeUDPConfiguration(CONTAINER);
		}

		catch (Exception e)
		{
		    throw new Exception($"UDP_MODULE -> {e.Message}");
		};

		try
		{
		    foreach (Control control in CONTAINER.Controls)
		    {
			foreach (Control scontrol in control.Controls)
			{
			    foreach (Control sscontrol in scontrol.Controls)
			    {
				if (sscontrol is TextBox)
				{
				    if (!sscontrol.Text.Contains(")"))
				    {
					((TextBox)sscontrol).TextAlign = HorizontalAlignment.Center;
				    };
				};
			    };
			};
		    };
		}

		catch
		{
		    throw new Exception("TEXT-ALIGNMENT");
		}
	    }

	    catch (Exception e)
	    {
		throw new Exception($"InitializeConfCon[CONFIGURATION -> {e.Message}]");
	    };
	}

	public MethodConfig()
	{
	    InitializeComponent();
	    
	    try
	    {
		InitializeMenuBar();
		InitializeConfCon();
	    }

	    catch (Exception e)
	    {
		throw new Exception($"MethodConfig -> {e.Message}()");
	    };
	}

	new public void Show()
	{
	    Control container = null;

	    for (int k = 0; k < CONTAINER.Controls.Count; k += 1)
	    {
		container = CONTAINER.Controls[k];
		
		if (container is PictureBox)
		{
		    Application.DoEvents();
		    container.Visible = false;
		}
	    }
	    
	    var CLIENT_SIZE = new Size(Width, BAR.Height + 20 + SELECT.Height + 10);
	    var m = SettingsContainer.CURRENT_METHOD.ToLower();

	    switch (m)
	    {
		case "dashloris 4.0":
		    CLIENT_SIZE.Height += 25 * HTTP.DASHLORIS4.CONTAINER.Controls.Count / 2;
		    HTTP.DASHLORIS4.CONTAINER.Visible = true;
	        break;
		case "slowloris 2.0":
		    CLIENT_SIZE.Height += 25 * HTTP.SLOWLORIS2.CONTAINER.Controls.Count / 2;
		    HTTP.SLOWLORIS2.CONTAINER.Visible = true;
		    break;
		case "put head": case "post head": case "get head":
		    CLIENT_SIZE.Height += 25 * HTTP.PUTPOSTGET.CONTAINER.Controls.Count / 2;
		    HTTP.PUTPOSTGET.CONTAINER.Visible = true;
		    break;
		case "h-flood":
		    CLIENT_SIZE.Height += 25 * HTTP.HFLOOD.CONTAINER.Controls.Count / 2;
		    HTTP.HFLOOD.CONTAINER.Visible = true;
		    break;
		case "long socks":
		    CLIENT_SIZE.Height += 25 * TCP.LONGSOCKS.CONTAINER.Controls.Count / 2;
		    TCP.LONGSOCKS.CONTAINER.Visible = true;
		    break;
		case "multi flood":
		    CLIENT_SIZE.Height += 25 * TCP.MULTIFLOOD.CONTAINER.Controls.Count / 2;
		    TCP.MULTIFLOOD.CONTAINER.Visible = true;
		    break;
		case "multi socks":
		    CLIENT_SIZE.Height += 25 * TCP.MULTISOCKS.CONTAINER.Controls.Count / 2;
		    TCP.MULTISOCKS.CONTAINER.Visible = true;
		    break;
		case "wavesss":
		    CLIENT_SIZE.Height += 25 * TCP.WAVESSS.CONTAINER.Controls.Count / 2;
		    TCP.WAVESSS.CONTAINER.Visible = true;
		    break;
		case "overload":
		    CLIENT_SIZE.Height += 25 * UDP.OVERLOAD.CONTAINER.Controls.Count / 2;
		    UDP.OVERLOAD.CONTAINER.Visible = true;
		    break;
		case "wavy baby":
		    CLIENT_SIZE.Height += 25 * UDP.WAVYBABY.CONTAINER.Controls.Count / 2;
		    UDP.WAVYBABY.CONTAINER.Visible = true;
		    break;
		case "go ham":
		    CLIENT_SIZE.Height += 25 * UDP.GOHAM.CONTAINER.Controls.Count / 2;
		    UDP.GOHAM.CONTAINER.Visible = true;
		    break;
		case "insta flood":
		    CLIENT_SIZE.Height += 25 * UDP.INSTAFLOOD.CONTAINER.Controls.Count / 2;
		    UDP.INSTAFLOOD.CONTAINER.Visible = true;
		    break;
	    }

	    TOOL.Resize(this, CLIENT_SIZE);

	    var CONTAINER_SIZE = new Size(CONTAINER.Width, CLIENT_SIZE.Height - BAR.Height - 20 - SELECT.Height - 10);
	    TOOL.Resize(CONTAINER, CONTAINER_SIZE);

	    var RECTUM_SIZE = new Size(CONTAINER_SIZE.Width + 2, CONTAINER_SIZE.Height + 2); 
	    TOOL.Resize(RECTUM, RECTUM_SIZE);

	    var SELECT_LOCA = new Point(SELECT.Left, Height - SELECT.Height - 10);
	    SELECT.Location = SELECT_LOCA;

	    ShowDialog();
	}
    }
}