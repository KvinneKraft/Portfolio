
// Author: Dashie
// Version: 1.0

using System;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class PortScanner2 : Form
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	readonly PictureBox BAR = new PictureBox();
	readonly Button CLOSE = new Button();
	readonly Label TITLE = new Label();

	private void LoadMenuBar()
	{
	    var BAR_SIZE = new Size(Width, 26);
	    var BAR_LOCA = new Point(0, 0);
	    var BAR_BCOL = Color.FromArgb(8, 8, 8);

	    CONTROL.Image(this, BAR, BAR_SIZE, BAR_LOCA, null, BAR_BCOL);

	    var CLOSE_SIZE = new Size(65, BAR.Height);
	    var CLOSE_LOCA = new Point(BAR.Width - CLOSE_SIZE.Width, 0);
	    var CLOSE_BCOL = BAR.BackColor;
	    var CLOSE_FCOL = Color.White;

	    CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 10, "X", Color.Empty);

	    CLOSE.Click += (s, e) =>
	    {
		Hide();
	    };

	    var TITLE_TEXT = "Port Scanner 2.0";
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 8);
	    var TITLE_LOCA = new Point(10, (BAR.Height - TITLE_SIZE.Height) / 2);
	    var TITLE_BCOL = BAR_BCOL;
	    var TITLE_FCOL = Color.White;

	    CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, TITLE_TEXT);

	    TOOL.Interactive(TITLE, this);
	    TOOL.Interactive(BAR, this);

	    var RECT_SIZE = new Size(Width - 1, Height - BAR.Height);
	    var RECT_LOCA = new Point(0, BAR.Height - 1);
	    var RECT_BCOL = BAR.BackColor;

	    TOOL.PaintRectangle(this, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	}

	private void LoadGLayout()
	{
	    SuspendLayout();

	    MaximumSize = new Size(250, 192);
	    MinimumSize = new Size(250, 192);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    Icon = Properties.Resources.ICON_ICO;
	    BackColor = Color.MidnightBlue;

	    MaximizeBox = false;
	    MinimizeBox = false;

	    Name = "Port Scanner 2.0";
	    Text = "Port Scanner 2.0";

	    TOOL.Round(this, 6);
	    ResumeLayout(false);
	}

	readonly PictureBox TARGET_CONTAINER = new PictureBox();

	readonly TextBox HOST_T = new TextBox() { TextAlign = HorizontalAlignment.Center, Text = "https://www.google.co.uk" };
	readonly TextBox PORT_T = new TextBox() { TextAlign = HorizontalAlignment.Center, Text = "43,80,443,23,22,21,8080,2222" };

	readonly Label HOST_L = new Label();
	readonly Label PORT_L = new Label();

	readonly PictureBox INNER_METHOD_CONTAINER = new PictureBox();
	readonly PictureBox METHOD_CONTAINER = new PictureBox();

	readonly static PictureBox UDP_S = new PictureBox();
	readonly static PictureBox TCP_S = new PictureBox();
	readonly static PictureBox RAW_S = new PictureBox();

	readonly Label UDP_L = new Label() { Name = "udp" };
	readonly Label TCP_L = new Label() { Name = "tcp" };
	readonly Label RAW_L = new Label() { Name = "raw" };

	readonly static Color DISABLED = Color.FromArgb(14, 14, 14);
	readonly static Color ENABLED = Color.DarkGreen;

	readonly ScanProgress SCAN_PROGRESS = new ScanProgress();

	readonly PictureBox INNER_SELECT_CONTAINER = new PictureBox();
	readonly PictureBox SELECT_CONTAINER = new PictureBox();

	readonly Button OPT_1 = new Button();
	readonly Button OPT_2 = new Button();
	readonly Button OPT_3 = new Button();

	public static string host = string.Empty;
	readonly public int port = 80;

	public class ScanProgress : Form
	{
	    readonly DashControls CONTROL = new DashControls();
	    readonly DashTools TOOL = new DashTools();

	    private void LoadGLayout()
	    {
		SuspendLayout();

		MaximumSize = new Size(250, 250);
		MinimumSize = new Size(250, 250);

		StartPosition = FormStartPosition.CenterScreen;
		FormBorderStyle = FormBorderStyle.None;

		Icon = Properties.Resources.ICON_ICO;
		BackColor = Color.MidnightBlue;

		ShowInTaskbar = true;

		MaximizeBox = false;
		MinimizeBox = false;

		Name = "Scan Progress";
		Text = "Scan Progress";

		TOOL.Round(this, 6);
		ResumeLayout(false);
	    }

	    readonly PictureBox BAR = new PictureBox();
	    readonly Button CLOSE = new Button();
	    readonly Label TITLE = new Label();

	    private void LoadMenuBar()
	    {
		var BAR_SIZE = new Size(Width, 26);
		var BAR_LOCA = new Point(0, 0);
		var BAR_BCOL = Color.FromArgb(8, 8, 8);

		CONTROL.Image(this, BAR, BAR_SIZE, BAR_LOCA, null, BAR_BCOL);

		var CLOSE_SIZE = new Size(65, BAR.Height);
		var CLOSE_LOCA = new Point(BAR.Width - CLOSE_SIZE.Width, 0);
		var CLOSE_BCOL = BAR.BackColor;
		var CLOSE_FCOL = Color.White;

		CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 10, "X", Color.Empty);

		CLOSE.Click += (s, e) =>
		{
		    Hide();
		};

		var TITLE_TEXT = "Scan Progress";
		var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 8);
		var TITLE_LOCA = new Point(10, (BAR.Height - TITLE_SIZE.Height) / 2);
		var TITLE_BCOL = BAR_BCOL;
		var TITLE_FCOL = Color.White;

		CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, TITLE_TEXT);

		TOOL.Interactive(TITLE, this);
		TOOL.Interactive(BAR, this);

		var RECT_SIZE = new Size(Width - 2, Height - BAR.Height);
		var RECT_LOCA = new Point(1, BAR.Height - 1);
		var RECT_BCOL = BAR.BackColor;

		TOOL.PaintRectangle(this, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	    }

	    readonly PictureBox LOG_CONTAINER = new PictureBox();
	    readonly TextBox PROGRESS_LOG = new TextBox() { Text = "Dashie is me" };

	    private void LoadPogress()
	    {
		var LOGC_SIZE = new Size(Width - 20, Height - BAR.Height - 22);
		var LOGC_LOCA = new Point(10, BAR.Height + 10);
		var LOGC_BCOL = Color.White; //Color.MidnightBlue;

		CONTROL.Image(this, LOG_CONTAINER, LOGC_SIZE, LOGC_LOCA, null, LOGC_BCOL);

		var PLOG_SIZE = new Size(LOGC_SIZE.Width - 3, LOGC_SIZE.Height - 3);
		var PLOG_LOCA = new Point(2, 2);
		var PLOG_BCOL = Color.FromArgb(14, 14, 14);
		var PLOG_FCOL = Color.White;

		CONTROL.TextBox(LOG_CONTAINER, PROGRESS_LOG, PLOG_SIZE, PLOG_LOCA, PLOG_BCOL, PLOG_FCOL, 1, 8, Color.Empty, READONLY: true, MULTILINE: true, SCROLLBAR: true, FIXEDSIZE: false);

		var RECT_SIZE = new Size(LOGC_SIZE.Width, LOGC_SIZE.Height);
		var RECT_LOCA = new Point(0, 0);
		var RECT_BCOL = BAR.BackColor;

		TOOL.PaintRectangle(LOG_CONTAINER, 4, RECT_SIZE, RECT_LOCA, RECT_BCOL);
		TOOL.Round(LOG_CONTAINER, 8);
	    }

	    public List<int> portData = new List<int>();

	    private void SetPort(string portdata)
	    {
		portData.Clear();

		try
		{
		    if (portdata.Contains(","))
		    {
			var rawies = portdata.Replace(" ", ",").Split(',');

			foreach (var rawy in rawies)
			{
			    if (rawy.Length > 0)
			    {
				var port = int.Parse(rawy);

				if (port < 1 || port > 65535)
				{
				    portData.Add(-1);
				    break;
				}

				portData.Add(port);
			    }
			}
		    }

		    else if (portdata.Contains("-"))
		    {
			var poarr = portdata.Split('-');
			var port1 = int.Parse(poarr[0]);
			var port2 = int.Parse(poarr[1]);

			for (int k = port1; k <= port2; k += 1)
			{
			    portData.Add(k);
			}
		    }

		    else
		    {//Fix single ports, work on dashloris
			var port = int.Parse(portdata.Replace(",", "").Replace(" ", ""));
			portData.Add(port);
		    }
		}

		catch (Exception e)
		{
		    portData.Add(-1);
		}
	    }

	    public void Stop()
	    {
		PROGRESS_LOG.AppendText("[-] Stopping scan ....\r\n");
		SCAN_PERMISSION = false;
	    }

	    public bool SCAN_PERMISSION = false;

	    private void StartScan()
	    {
		string ip = host;
		
		PROGRESS_LOG.AppendText($"[+] Scanning {host} ....\r\n");

		var sockType = SocketType.Stream;
		var protType = ProtocolType.Tcp;

		if (UDP_S.BackColor == ENABLED)
		{
		    sockType = SocketType.Dgram;
		    protType = ProtocolType.Udp;
		}

		else if (RAW_S.BackColor == ENABLED)
		{
		    sockType = SocketType.Raw;
		    //protType = ProtocolType.Tcp;
		}

		new Thread(() =>
		{
		    bool FOUND = false;

		    SCAN_PERMISSION = true;

		    foreach (var port in portData)
		    {
			try
			{
			    if (!SCAN_PERMISSION)
			    {
				break;
			    }

			    var sock = new Socket(AddressFamily.InterNetwork, sockType, protType);

			    if (portData.IndexOf(port) + 1 == portData.Count / 2)
			    {
				PROGRESS_LOG.AppendText("[/] Almost done scanning, be patient.\r\n");
			    }

			    var result = sock.BeginConnect(host, port, null, null);
			    var succes = result.AsyncWaitHandle.WaitOne(175, true);

			    if (sock.Connected)
			    {
				PROGRESS_LOG.AppendText($"[+] Discovered an open port: {port}.\r\n");
				FOUND = true;
			    }

			    sock.Close();
			}

			catch
			{  }
		    }

		    if (!FOUND)
		    {
			PROGRESS_LOG.AppendText("[!] Found no open ports!\r\n");
		    }

		    PROGRESS_LOG.AppendText("[+] Done scanning!\r\n");

		    SCAN_PERMISSION = false;
		})

		{ IsBackground = true }.Start();
	    }

	    public ScanProgress()
	    {
		try
		{
		    LoadGLayout();
		    LoadMenuBar();
		    LoadPogress();
		}

		catch
		{
		    Environment.Exit(-1);
		}
	    }

	    public void Show(string portdata)
	    {
		PROGRESS_LOG.Clear();
		PROGRESS_LOG.AppendText("[+] Configuring ports and host ....\r\n");

		SetPort(portdata);

		if (!portData.Contains(-1) && portData.Count > 0)
		{
		    StartScan();
		}

		else
		{
		    PROGRESS_LOG.AppendText("[!] Invalid port(s) specified.  Retry!\r\n");
		}

		Show();
	    }
	}
	
	public bool SetHost()
	{
	    var r_host = HOST_T.Text.ToLower();

	    host = string.Empty;

	    if (!IPAddress.TryParse(r_host, out IPAddress ham))
	    {
		if (!r_host.Contains("http://") && !r_host.Contains("https://")) r_host = "https://" + r_host;

		if (!Uri.TryCreate(r_host, UriKind.RelativeOrAbsolute, out Uri bacon))
		{
		    MessageBox.Show("Invalid URL specified.  Retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		    return false;
		};

		try
		{
		    host = Dns.GetHostAddresses(bacon.Host)[0].ToString();
		}

		catch
		{
		    MessageBox.Show("Invalid host specified.  Retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		    return false;
		}
	    }

	    else
	    {
		host = ham.ToString();

		if (ham.AddressFamily != AddressFamily.InterNetwork)
		{
		    MessageBox.Show("Invalid IPv4 address specified.  Retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		    return false;
		}
	    }

	    if (host.Length < 7 || host == string.Empty)
	    {
		MessageBox.Show("No host was specified.  Retry!", "Host Address Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		return false;
	    }

	    return true;
	}
	
	bool IsOnline()
	{
	    var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

	    try
	    {
		var result = sock.BeginConnect(host, port, null, null);
		var succes = result.AsyncWaitHandle.WaitOne(500, true);
		
		return sock.Connected;
	    }

	    catch
	    {
		return false;
	    }
	}

	private void LoadScanner()
	{
	    void ThrowError(Exception e)
	    {
		MessageBox.Show($"Something happened while trying to load up Port Scanner 2.0.\r\n\r\nPlease contact me at KvinneKraft@protonmail.com with the following:\r\n\r\n[{e.Message}]\r\n{e.StackTrace}\r\n\r\nThank you very much!", "(Error Occurrence)");
		Environment.Exit(-1);
	    };

	    try
	    {
		try
		{
		    var TCON_SIZE = new Size(Width - 24, 55);
		    var TCON_LOCA = new Point(12, BAR.Height + 12);
		    var TCON_BCOL = Color.MidnightBlue;

		    CONTROL.Image(this, TARGET_CONTAINER, TCON_SIZE, TCON_LOCA, null, TCON_BCOL);

		    try
		    {
			var LBOX_TEXT = "Host(s):";
			var LBOX_SIZE = TOOL.GetFontSize(LBOX_TEXT, 10);
			var LBOX_LOCA = new Point(5, 6);
			var LBOX_BCOL = BackColor;
			var LBOX_FCOL = Color.White;

			CONTROL.Label(TARGET_CONTAINER, HOST_L, LBOX_SIZE, LBOX_LOCA, LBOX_BCOL, LBOX_FCOL, 1, 10, LBOX_TEXT);

			LBOX_SIZE = TOOL.GetFontSize(LBOX_TEXT, 10);
			LBOX_LOCA.Y += 8 + LBOX_SIZE.Height;
			LBOX_TEXT = "Port(s):";

			CONTROL.Label(TARGET_CONTAINER, PORT_L, LBOX_SIZE, LBOX_LOCA, LBOX_BCOL, LBOX_FCOL, 1, 10, LBOX_TEXT);

			var TBOX_SIZE = new Size(TARGET_CONTAINER.Width - LBOX_SIZE.Width - 10 - LBOX_LOCA.X, 20);
			var TBOX_LOCA = new Point(LBOX_LOCA.X + LBOX_SIZE.Width + 4, 5);
			var TBOX_BCOL = Color.FromArgb(14, 14, 14);
			var TBOX_FCOL = Color.White;

			CONTROL.TextBox(TARGET_CONTAINER, HOST_T, TBOX_SIZE, TBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 9, Color.Empty);

			TBOX_LOCA.X = LBOX_LOCA.X + LBOX_SIZE.Width + 4;
			TBOX_LOCA.Y += 5 + TBOX_SIZE.Height;

			CONTROL.TextBox(TARGET_CONTAINER, PORT_T, TBOX_SIZE, TBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 10, Color.Empty);
		    }

		    catch (Exception e)
		    {
			ThrowError(e);
		    }

		    var RECT_SIZE = new Size(TCON_SIZE.Width + 2, TCON_SIZE.Height + 2);
		    var RECT_LOCA = new Point(TCON_LOCA.X - 1, TCON_LOCA.Y - 1);
		    var RECT_BCOL = BAR.BackColor;

		    TOOL.PaintRectangle(this, 1, RECT_SIZE, RECT_LOCA, RECT_BCOL);
		}

		catch (Exception e)
		{
		    ThrowError(e);
		}

		try
		{
		    var MCON_SIZE = new Size(Width - 26, 27);//17 + 10
		    var MCON_LOCA = new Point(-1, TARGET_CONTAINER.Top + TARGET_CONTAINER.Height + 12);
		    var MCON_BCOL = Color.MidnightBlue;

		    CONTROL.Image(this, METHOD_CONTAINER, MCON_SIZE, MCON_LOCA, null, MCON_BCOL);

		    try
		    {
			var IMC_SIZE = new Size(177, 17);
			var IMC_LOCA = new Point(-1, -1);
			var IMC_BCOL = MCON_BCOL;

			CONTROL.Image(METHOD_CONTAINER, INNER_METHOD_CONTAINER, IMC_SIZE, IMC_LOCA, null, IMC_BCOL);

			INNER_METHOD_CONTAINER.Left -= 2;

			var METL_TEXT = "UDP";
			var METL_SIZE = TOOL.GetFontSize(METL_TEXT, 12);
			var METL_LOCA = new Point(0, 0);
			var METL_BCOL = Color.MidnightBlue;
			var METL_FCOL = Color.White;

			CONTROL.Label(INNER_METHOD_CONTAINER, UDP_L, METL_SIZE, METL_LOCA, METL_BCOL, METL_FCOL, 1, 12, METL_TEXT);

			var METI_SIZE = new Size(17, 17);
			var METI_LOCA = new Point(METL_SIZE.Width + METL_LOCA.X, 0);
			var METI_BCOL = Color.FromArgb(14, 14, 14);
			
			CONTROL.Image(INNER_METHOD_CONTAINER, UDP_S, METI_SIZE, METI_LOCA, null, METI_BCOL);

			void UpdateLX() => METL_LOCA.X += UDP_S.Left + UDP_S.Width + 5;
			void UpdatePX() => METI_LOCA.X += METL_SIZE.Width + 22;

			METL_TEXT = "TCP"; UpdateLX();

			CONTROL.Label(INNER_METHOD_CONTAINER, TCP_L, METL_SIZE, METL_LOCA, METL_BCOL, METL_FCOL, 1, 12, "TCP"); UpdatePX();
			CONTROL.Image(INNER_METHOD_CONTAINER, TCP_S, METI_SIZE, METI_LOCA, null, ENABLED); METL_TEXT = "RAW"; UpdateLX();
			CONTROL.Label(INNER_METHOD_CONTAINER, RAW_L, TOOL.GetFontSize(METL_TEXT, 12), METL_LOCA, METL_BCOL, METL_FCOL, 1, 12, "RAW"); UpdatePX();
			CONTROL.Image(INNER_METHOD_CONTAINER, RAW_S, METI_SIZE, METI_LOCA, null, METI_BCOL); RAW_S.Left += 2;

			foreach (Control c1 in INNER_METHOD_CONTAINER.Controls)
			{
			    if (c1 is PictureBox)
			    {
				c1.Click += (s, e) =>
				{
				    foreach (Control c2 in INNER_METHOD_CONTAINER.Controls)
				    {
					if (c2 is PictureBox)
					{
					    c2.BackColor = DISABLED;
					}
				    }

				    c1.BackColor = ENABLED;
				};
			    }
			}
		    }

		    catch (Exception e)
		    {
			ThrowError(e);
		    }

		    var RECT_SIZE = new Size(MCON_SIZE.Width + 2, MCON_SIZE.Height + 2);
		    var RECT_LOCA = new Point(MCON_LOCA.X - 1, MCON_LOCA.Y - 1);
		    var RECT_BCOL = BAR.BackColor;

		    TOOL.PaintRectangle(this, 1, RECT_SIZE, RECT_LOCA, RECT_BCOL);
		}

		catch (Exception e)
		{
		    ThrowError(e);
		}

		try
		{
		    var MCON_SIZE = new Size(Width - 24, 34);//24
		    var MCON_LOCA = new Point(12, METHOD_CONTAINER.Top + METHOD_CONTAINER.Height + 12);
		    var MCON_BCOL = Color.MidnightBlue;

		    CONTROL.Image(this, SELECT_CONTAINER, MCON_SIZE, MCON_LOCA, null, MCON_BCOL);

		    try
		    {
			var ISC_SIZE = new Size(205, 24);
			var ISC_LOCA = new Point(-1, -1);
			var ISC_BCOL = MCON_BCOL;

			CONTROL.Image(SELECT_CONTAINER, INNER_SELECT_CONTAINER, ISC_SIZE, ISC_LOCA, null, ISC_BCOL);

			var PRESS_SIZE = new Size(65, 24);
			var PRESS_LOCA = new Point(0, 0);
			var PRESS_BCOL = Color.FromArgb(10, 10, 10);
			var PRESS_FCOL = Color.White;

			CONTROL.Button(INNER_SELECT_CONTAINER, OPT_1, PRESS_SIZE, PRESS_LOCA, PRESS_BCOL, PRESS_FCOL, 1, 9, "Online", Color.Empty);

			OPT_1.Click += (s, e) =>
			{
			    if (SetHost())
			    {
				if (IsOnline())
				{
				    MessageBox.Show("It turns out the host is reachable.", "Host Reachability", MessageBoxButtons.OK, MessageBoxIcon.Question);
				}

				else
				{
				    MessageBox.Show("It turns out the host is unreachable.", "Host Reachability", MessageBoxButtons.OK, MessageBoxIcon.Question);
				}
			    }
			};

			PRESS_LOCA.X += PRESS_SIZE.Width + 5;

			CONTROL.Button(INNER_SELECT_CONTAINER, OPT_2, PRESS_SIZE, PRESS_LOCA, PRESS_BCOL, PRESS_FCOL, 1, 9, "Scan", Color.Empty);

			OPT_2.Click += (s, e) =>
			{
			    if (!SCAN_PROGRESS.Visible)
			    {
				if (SetHost())
				{
			    	    SCAN_PROGRESS.Show(PORT_T.Text);
				}
			    }

			    else
			    {
				SCAN_PROGRESS.Stop();
				SCAN_PROGRESS.Hide();
				SCAN_PROGRESS.Show(PORT_T.Text);
			    }
			};

			PRESS_LOCA.X += PRESS_SIZE.Width + 5;

			CONTROL.Button(INNER_SELECT_CONTAINER, OPT_3, PRESS_SIZE, PRESS_LOCA, PRESS_BCOL, PRESS_FCOL, 1, 9, "Help", Color.Empty);

			OPT_3.Click += (s, e) =>
			{
			    MessageBox.Show("Well, there is not much to say besides, this is a port scanner.  Nice, huh?", "Port Scanner 2.0 Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			};
		    }

		    catch (Exception e)
		    {
			ThrowError(e);
		    }

		    var RECT_SIZE = new Size(MCON_SIZE.Width + 2, MCON_SIZE.Height + 2);
		    var RECT_LOCA = new Point(MCON_LOCA.X - 1, MCON_LOCA.Y - 1);
		    var RECT_BCOL = BAR.BackColor;

		    TOOL.PaintRectangle(this, 1, RECT_SIZE, RECT_LOCA, RECT_BCOL);
		}

		catch (Exception e)
		{
		    ThrowError(e);
		}
	    }

	    catch (Exception e)
	    {
		ThrowError(e);
	    }
	}

	public PortScanner2()
	{
	    try
	    {
		LoadGLayout();
		LoadMenuBar();
		LoadScanner();
	    }

	    catch
	    {
		Environment.Exit(-1);
	    }
	}
    }
}