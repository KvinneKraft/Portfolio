using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class PortScanner : Form
    {
	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	private void SendError(string m)
	{
	    MessageBox.Show($"It appears that an error has occurred.\r\n\r\nThe error message: \r\n{m}\r\n\r\nYou can send this to me at KvinneKraft@protonmail.com if you want to fix this.\r\n\r\nFor now you can click OK and restart the application.", "Oh...Ow", MessageBoxButtons.OK, MessageBoxIcon.Error);
	    Close();
	}
	public class GUI
	{
	    readonly public static PictureBox BRAND = new PictureBox();
	    readonly public static PictureBox BAR = new PictureBox();

	    readonly public static Button CLOSE = new Button();

	    readonly public static Label TITLE = new Label();
	    
	    public static void InitializeGUI(Form TOP)
	    {
		TOP.BackColor = Color.MidnightBlue;
		TOP.FormBorderStyle = FormBorderStyle.None;
		TOP.Icon = Properties.Resources.ICON_ICO;

		try
		{
		    var BAR_SIZE = new Size(TOP.Width, 24);
		    var BAR_LOCA = new Point(0, 0);
		    var BAR_COLA = Color.FromArgb(8, 8, 8);

		    CONTROL.Image(TOP, BAR, BAR_SIZE, BAR_LOCA, null, BAR_COLA);

		    var BRAND_IMAG = Properties.Resources.ICON_PNG1;
		    var BRAND_SIZE = BRAND_IMAG.Size;
		    var BRAND_LOCA = new Point(5, 0);
		    var BRAND_COLA = BAR_COLA;

		    CONTROL.Image(BAR, BRAND, BRAND_SIZE, BRAND_LOCA, BRAND_IMAG, BRAND_COLA);

		    var TITLE_TEXT = "Dash Port Scanner";
		    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 10);
		    var TITLE_LOCA = TOOL.GetCenter(TOP, TITLE, new Point((BAR.Width - TITLE_SIZE.Width) / 2, (BAR.Height - TITLE_SIZE.Height) / 2));
		    var TITLE_BCOL = BAR_COLA;
		    var TITLE_FCOL = Color.White;

		    CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 10, TITLE_TEXT);

		    var CLOSE_SIZE = new Size(55, BAR_SIZE.Height);
		    var CLOSE_LOCA = new Point(BAR_SIZE.Width - CLOSE_SIZE.Width, 0);
		    var CLOSE_BCOL = BAR_COLA;
		    var CLOSE_FCOL = Color.White;

		    CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 12, "X", Color.Empty);

		    CLOSE.Click += (s, e) =>
			TOP.Close();

		    var RECT_SIZE = new Size(TOP.Width, TOP.Height - BAR_SIZE.Height + 1);
		    var RECT_LOCA = new Point(0, BAR_SIZE.Height - 2);
		    var RECT_COLA = BAR_COLA;

		    TOOL.PaintRectangle(TOP, 4, RECT_SIZE, RECT_LOCA, RECT_COLA);

		    foreach (Control CON in TOP.Controls)
			TOOL.Interactive(CON, TOP);

		    foreach (Control CON in BAR.Controls)
			TOOL.Interactive(CON, TOP);
    			TOOL.Interactive(TOP, TOP);
		}

		catch
		{
		    throw new Exception("InitializeGUI()");
		};

		TOOL.Round(TOP, 6);
	    }
	}

	public class OPS
	{
	    readonly public static RichTextBox PORT = new RichTextBox();
	    readonly public static RichTextBox LOGS = new RichTextBox();

	    readonly public static PictureBox PORTCONTAINER = new PictureBox();
	    readonly public static PictureBox LOGSCONTAINER = new PictureBox();
	    readonly public static PictureBox CONTAINER = new PictureBox();

	    readonly public static Button YES = new Button();// { Visible = false };
	    readonly public static Button NUU = new Button();// { Visible = false };
	    readonly public static Button TOGGLE = new Button();

	    public enum Type
	    {
		None, Info, Warning, Error, Success
	    }

	    public static void print(string m, Type type)
	    {
		string p = "(?) ";

		switch (type)
		{
		    case Type.None:
			p = "";
			break;
		    case Type.Warning:
			p = "(-) ";
			break;
		    case Type.Error:
			TOGGLE.Text = "Start Scan";
			p = "(!) ";
			break;
		    case Type.Success:
			p = "(+) ";
			break;
		}

		LOGS.AppendText($"{p}{m}\r\n");
	    }

	    private static readonly List<int> closed = new List<int>();
	    private static readonly List<int> opened = new List<int>();

	    public static Thread THREAD = null;

	    private static void StartScanning()
	    {
		opened.Clear();
		closed.Clear();

		THREAD = new Thread(() =>
		{
		    var r_host = TargetContainer.IP_BOX.Text.ToLower();
		    var host = string.Empty;

		    if (!IPAddress.TryParse(r_host, out IPAddress ham))
		    {
			if (!r_host.Contains("http://") && !r_host.Contains("https://")) r_host = "https://" + r_host;

			if (!Uri.TryCreate(r_host, UriKind.RelativeOrAbsolute, out Uri bacon))
			{
			    print("Missing IPv4 or Uri host value!", Type.Error);
			    return;
			}

			try
			{
			    host = Dns.GetHostAddresses(bacon.Host)[0].ToString();
			}

			catch
			{
			    print("Invalid host value!", Type.Error);
			    return;
			}
		    }

		    else
		    {
			host = ham.ToString();

			if (ham.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
			{
			    print("Invalid IPv4 host value!", Type.Error);
			    return;
			}
		    }

		    var s_texts = PORT.Text.Replace(" ", "");

		    var s_ports = new List<string>();
		    var ports = new List<int>();

		    void HandleIntegerError() =>
			print("You can not specify an incorrect integral value man.", Type.Error);

		    int mode = 0;

		    if (s_texts.Contains(","))
			s_ports.AddRange(s_texts.Split(','));

		    else if (s_texts.Contains("-"))
		    {
			var range = s_texts.Split('-');

			int a = 0;
			int b = 0;

			try
			{
			    if (!int.TryParse(range[0], out a) || !int.TryParse(range[1], out b))
			    {
				HandleIntegerError();
				return;
			    }

			    if (b < a)
			    {
				print("The lowest port in a range of ports can not be lower than the end of the range.", Type.Error);
				return;
			    }

			    for (int k = a; k <= b; k += 1)
			    {
				ports.Add(k);
			    }

			    mode = 1;
			}

			catch (Exception e)
			{
			    print($"{e.Message} \r\n\r\nOne or more of your port values are invalid.", Type.Error);
			    return;
			}
		    }

		    else
			s_ports.Add(s_texts);

		    var c = 0;

		    if (mode != 1)
		    {
			foreach (var k in s_ports)
			{
			    try
			    {
				if (!int.TryParse(k, out c))
				{
				    HandleIntegerError();
				    return;
				}

				ports.Add(c);
			    }

			    catch
			    {
				HandleIntegerError();
				return;
			    }
			}
		    }

		    print($"Started scanning {host} !", Type.Success);

		    foreach (var port in ports)
		    {
			var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			try
			{
			    if (ports[ports.IndexOf(port)] == ports.Count / 2)
			    {
				print("Almost done!", Type.Info);
			    }

			    var result = sock.BeginConnect(host, port, null, null);
			    var succes = result.AsyncWaitHandle.WaitOne(175, true);

			    if (sock.Connected)
			    {
				print("Discovered an open port!", Type.Info);
				opened.Add(port);
			    }

			    else
			    {
				closed.Add(port);
			    }
			}

			catch
			{
			    closed.Add(port);
			}

			sock.Close();
		    }

		    print("The scan has ended successfully!", Type.Success);
		    print("Press YES to see all open ports and NUU to see all closed ports.", Type.Info);

		    CONTAINER.Show();

		    TOGGLE.Text = "Start Scan";
		})

		{ IsBackground = true };

		THREAD.Start();
	    }

	    private static void SaveToFile()
	    {
		if (closed.Count < 1 && opened.Count < 1)
		{
		    print("You must first run a scan.", Type.Error);
		    return;
		};

		using (var save = new SaveFileDialog())
		{
		    try
		    {
			save.RestoreDirectory = true;
			save.CheckPathExists = true;
			save.ValidateNames = true;

			save.Filter = "Text File|*.txt";
			save.Title = "Save Scan Results";

			var resu = save.ShowDialog();

			if (resu != DialogResult.OK)
			{
			    print("No file was specified.", Type.Warning);
			    return;
			};

			var file = save.FileName;
			var hand = File.Create(file);

			if (hand == null)
			{
			    print("An error occurred while creating your file.", Type.Error);
			    return;
			};

			hand.Close();

			var format = new string[opened.Count + closed.Count];

			for (int k = 0; k < opened.Count; k += 1)
			{
			    format[k] = $"-==: {opened[k]}";

			    if (k == 0)
			    {
				format[k] = $"Scan Date: {DateTime.Now}\r\nHost: {TargetContainer.IP_BOX.Text}\r\n------------------\r\n[Open TCP Ports]\r\n" + format[k];
			    };
			};

			for (int k = opened.Count; k < opened.Count + closed.Count - 1; k += 1)
			{
			    format[k] = $"-==: {closed[k - 1]}";

			    if (k == opened.Count)
			    {
				format[k] = $"\r\n[Closed TCP Ports]\r\n" + format[k];
			    };
			};

			File.WriteAllLines(file, format);

			print($"Saved file to \"{file}\" !", Type.Info);
		    }

		    catch
		    {
			print("An unknown error has occurred while exporting your file.", Type.Error);
		    };
		};
	    }

	    private static void PrintPorts(bool isOpen)
	    {
		if (closed.Count < 1 && opened.Count < 1)
		{
		    print("You must first run a scan.", Type.Error);
		    return;
		};

		string paw = "[Accessible Ports (TCP)]\r\n";

		if (isOpen)
		{
		    foreach (var p in opened)
		    {
			paw += $"-==: {p}\r\n";
		    };
		}

		else
		{
		    paw = "[Inaccessible Ports (TCP)]\r\n";

		    foreach (var p in closed)
		    {
			paw += $"-==: {p}\r\n";
		    };
		};

		if (!paw.Contains("-==:"))
		{
		    print("There are no ports to show.", Type.Error);
		    return;
		};

		LOGS.AppendText(paw);
	    }

	    public static void InitializeContainer(Form TOP)
	    {
		try
		{
		    var TOGGLE_SIZE = new Size(145, 28);
		    var TOGGLE_LOCA = new Point((TOP.Width - TOGGLE_SIZE.Width) / 2, GUI.BAR.Height + 6);
		    var TOGGLE_BCOL = Color.FromArgb(12, 12, 12);
		    var TOGGLE_FCOL = Color.White;

		    CONTROL.Button(TOP, TOGGLE, TOGGLE_SIZE, TOGGLE_LOCA, TOGGLE_BCOL, TOGGLE_FCOL, 1, 12, "Start Scan", Color.Empty);

		    TOGGLE.Click += (s, e) =>
		    {
			if (TOGGLE.Text != "Stop Scan")
			{
			    opened.Clear();
			    closed.Clear();

			    CONTAINER.Hide();

			    TOGGLE.Text = "Stop Scan";

			    StartScanning();
			}

			else
			{
			    CONTAINER.Hide();

			    TOGGLE.Text = "Start Scan";

			    THREAD.Abort();
			};

			return;
		    };

		    TOOL.Round(TOGGLE, 6);

		    var y = GUI.BAR.Height + 10 + TOGGLE.Height + 2;

		    var PORTCONTAINER_SIZE = new Size(TOP.Width - 20, 80);
		    var PORTCONTAINER_LOCA = new Point(10, y);
		    var PORTCONTAINER_BCOL = Color.FromArgb(64, 25, 112);//LogContainer.LOG.BackColor;

		    CONTROL.Image(TOP, PORTCONTAINER, PORTCONTAINER_SIZE, PORTCONTAINER_LOCA, null, PORTCONTAINER_BCOL);
		    TOOL.Round(PORTCONTAINER, 6);

		    var PORT_SIZE = new Size(PORTCONTAINER.Width - 10, PORTCONTAINER.Height - 10);
		    var PORT_LOCA = new Point(5, 5);
		    var PORT_BCOL = Color.FromArgb(64, 25, 112);
		    var PORT_FCOL = Color.White;

		    CONTROL.RichTextBox(PORTCONTAINER, PORT, PORT_SIZE, PORT_LOCA, PORT_BCOL, PORT_FCOL, 1, 11, "80, 443, 8080, 53, 56, 21, 22, 22565");

		    PORT.ScrollBars = RichTextBoxScrollBars.ForcedVertical;

		    var RECT_SIZE = new Size(PORTCONTAINER_SIZE.Width - 2, PORTCONTAINER_SIZE.Height - 2);
		    var RECT_LOCA = new Point(1, 1);
		    var RECT_COLA = GUI.BAR.BackColor;

		    TOOL.PaintRectangle(PORTCONTAINER, 2, RECT_SIZE, RECT_LOCA, RECT_COLA);

		    var LOGCONTAINER_SIZE = new Size(PORTCONTAINER_SIZE.Width, TOP.Height - PORTCONTAINER.Top - PORTCONTAINER.Height - 20);
		    var LOGCONTAINER_LOCA = new Point(PORTCONTAINER_LOCA.X, PORTCONTAINER.Top + PORTCONTAINER.Height + 10);
		    var LOGCONTAINER_BCOL = PORT_BCOL;

		    CONTROL.Image(TOP, LOGSCONTAINER, LOGCONTAINER_SIZE, LOGCONTAINER_LOCA, null, LOGCONTAINER_BCOL);
		    TOOL.Round(LOGSCONTAINER, 6);

		    var LOG_SIZE = new Size(LOGCONTAINER_SIZE.Width - 10, LOGCONTAINER_SIZE.Height - 10);
		    var LOG_LOCA = new Point(5, 5);
		    var LOG_BCOL = PORT_BCOL;
		    var LOG_FCOL = PORT_FCOL;

		    CONTROL.RichTextBox(LOGSCONTAINER, LOGS, LOG_SIZE, LOG_LOCA, LOG_BCOL, LOG_FCOL, 1, 9, "(?) Press F1 for shortcut options.\r\n");

		    LOGS.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
		    LOGS.ReadOnly = true;

		    RECT_SIZE = new Size(LOGCONTAINER_SIZE.Width - 2, LOGCONTAINER_SIZE.Height - 2);

		    TOOL.PaintRectangle(LOGSCONTAINER, 2, RECT_SIZE, RECT_LOCA, RECT_COLA);

		    var CONTAINER_SIZE = new Size(105, 22);
		    var CONTAINER_LOCA = new Point(LOGS.Width - CONTAINER_SIZE.Width - 25, LOG_SIZE.Height - CONTAINER_SIZE.Height - 5);
		    var CONTAINER_COLA = Color.Transparent;

		    CONTROL.Image(LOGS, CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		    TOOL.Round(YES, 6);
		    TOOL.Round(NUU, 6);

		    var OPTION_SIZE = new Size((CONTAINER_SIZE.Width - 5) / 2, CONTAINER_SIZE.Height);
		    var OPTION_LOCA = new Point(0, 0);
		    var OPTION_BCOL = Color.MidnightBlue;
		    var OPTION_FCOL = Color.White;

		    CONTROL.Button(CONTAINER, YES, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, "Yes", Color.Empty);

		    OPTION_LOCA.X += OPTION_SIZE.Width + 5;

		    CONTROL.Button(CONTAINER, NUU, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, "Nuu", Color.Empty);

		    NUU.Click += (s, e) => PrintPorts(false);
		    YES.Click += (s, e) => PrintPorts(true);

		    void SetupEvents(Control con)
		    {
			con.KeyDown += (s, e) =>
			{
			    switch (e.KeyData)
			    {
				case Keys.F1:
				    print("\r\n[Key Shortcuts]", Type.None);
				    print("-==: F1   =  SHOW HELP MENU", Type.None);
				    print("-==: F2  =  CLEAR TEXT LOG", Type.None);
				    print("-==: F3  =  SAVE RESULTS TO FILE", Type.None);
				    print("-==: F4  =  CLOSE PORT SCANNER\r\n", Type.None);
				    break;
				case Keys.F2:
				    LOGS.Clear();
				    break;
				case Keys.F3:
				    SaveToFile();
				    break;
				case Keys.F4:
				    TOP.Close();
				    break;
			    };
			};
		    }

		    SetupEvents(TOP);

		    for (int s = 0; s < TOP.Controls.Count; s += 1)
		    {
			Control con = TOP.Controls[s];

			for (int k = 0; k < con.Controls.Count; k += 1)
			{
			    SetupEvents(con.Controls[k]);
			};

			SetupEvents(con);
		    };
		}

		catch
		{
		    throw new Exception("OPSInitialize()");
		};
	    }
	}

	public PortScanner()
	{
	    InitializeComponent();

	    try
	    {
		GUI.InitializeGUI(this);
		OPS.InitializeContainer(this);

		//OPS.CONTAINER.Hide();
	    }

	    catch (Exception e)
	    {
		SendError($"PortScanner::{e.Message}");
	    }
	}

	public void InitializeComponent()
	{
	    SuspendLayout();

	    MaximumSize = new Size(300, 300);
	    MinimumSize = new Size(300, 300);

	    StartPosition = FormStartPosition.CenterScreen;

	    MaximizeBox = false;
	    MinimizeBox = false;

	    Name = "Port Scanner";
	    Text = "Port Scanner";

	    Closing += (s, e) =>
	    {
		OPS.CONTAINER.Hide();
		OPS.TOGGLE.Text = "Start Scan";
	    };

	    ResumeLayout(false);
	}
    }
}