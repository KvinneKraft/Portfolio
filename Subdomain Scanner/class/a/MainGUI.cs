
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Networking;
using DashFramework.Erroring;
using DashFramework.Dialog;
using DashFramework.Data;

namespace SubdomainAnalyzer
{
    public partial class MainGUI
    {
	Exception GetExep(Exception E) =>
	    ErrorHandler.GetException(E);


	readonly ControlHelper ConHelp = new ControlHelper();

	void InitConHelper()
	{
	    try
	    {
		ConHelp.TextBoxParent = ContainerA2;
		ConHelp.LabelParent = ContainerA2;

		ConHelp.TextBoxBCol = Color.FromArgb(41, 44, 89);
		ConHelp.TextBoxFCol = Color.White;

		ConHelp.LabelBCol = ContainerA1.BackColor;
		ConHelp.LabelFCol = Color.White;

		ConHelp.FontID = 0;
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly TextBox TextBoxA1 = new TextBox();
	readonly TextBox TextBoxA2 = new TextBox();
	readonly TextBox TextBoxA3 = new TextBox();
	readonly TextBox TextBoxA4 = new TextBox();

	readonly Label LabelA1 = new Label();
	readonly Label LabelA2 = new Label();
	readonly Label LabelA3 = new Label();
	readonly Label LabelA4 = new Label();
	readonly Label LabelA5 = new Label();
	readonly Label LabelA6 = new Label();

	void InitA2()
	{
	    try
	    {
		InitConHelper();

		var Lab1Text = ("Website:");
		var Lab1Size = ConHelp.GetFontSize(Lab1Text);
		var Lab1Loca = new Point(0, 0);

		var Tex1Text = ("pugpawz.com");
		var Tex1Size = new Size(135, 20);
		var Tex1Loca = ConHelp.ControlX(Lab1Size, Lab1Loca, Extra: 0);

		var Lab2Text = ("Ports:");
		var Lab2Size = ConHelp.GetFontSize(Lab2Text);
		var Lab2Loca = ConHelp.ControlX(Tex1Size, Tex1Loca);

		var Tex2Text = ("80,443,8080,4040,2525,25,551,451,450,540,550,22,21");
		var Tex2Size = ConHelp.TextBoxSize(Lab2Size, Lab2Loca);
		var Tex2Loca = ConHelp.ControlX(Lab2Size, Lab2Loca, Extra: 0);

		ConHelp.AddTextBox(TextBoxA1, Tex1Size, Tex1Loca, Tex1Text);
		ConHelp.AddTextBox(TextBoxA2, Tex2Size, Tex2Loca, Tex2Text);

		ConHelp.AddLabel(LabelA1, Lab1Size, Lab1Loca, Lab1Text);
		ConHelp.AddLabel(LabelA2, Lab2Size, Lab2Loca, Lab2Text);

		var Lab3Text = ("Sub Domain List:");
		var Lab3Size = ConHelp.GetFontSize(Lab3Text);
		var Lab3Loca = new Point(0, Lab2Loca.Y + Lab2Size.Height + 8);

		var Tex3Text = ($@"<select location>");
		var Tex3Size = ConHelp.TextBoxSize(Lab3Size, Lab3Loca);
		var Tex3Loca = ConHelp.ControlX(Lab3Size, Lab3Loca, Extra: 0);

		var Lab4Text = ("Connect Timeout:");
		var Lab4Size = ConHelp.GetFontSize(Lab4Text);
		var Lab4Loca = new Point(0, Lab3Loca.Y + Lab3Size.Height + 8);

		var Tex4Text = ("400");
		var Tex4Size = new Size(45, 20);
		var Tex4Loca = ConHelp.ControlX(Lab4Size, Lab4Loca, Extra: 0);

		ConHelp.AddTextBox(TextBoxA3, Tex3Size, Tex3Loca, Tex3Text);
		ConHelp.AddTextBox(TextBoxA4, Tex4Size, Tex4Loca, Tex4Text);

		ConHelp.AddLabel(LabelA3, Lab3Size, Lab3Loca, Lab3Text);
		ConHelp.AddLabel(LabelA4, Lab4Size, Lab4Loca, Lab4Text);

		var Lab5Text = ("Verbose:");
		var Lab5Size = ConHelp.GetFontSize(Lab5Text);
		var Lab5Loca = ConHelp.ControlX(Tex4Size, Tex4Loca);

		var Lab6Text = ("false");
		var Lab6Size = ConHelp.TextBoxSize(Lab5Size, Lab5Loca, 18);
		var Lab6Loca = ConHelp.ControlX(Lab5Size, Lab5Loca, Y: Lab5Loca.Y + 1, Extra: 0);

		ConHelp.AddLabel(LabelA5, Lab5Size, Lab5Loca, Lab5Text);
		ConHelp.LabelBCol = ConHelp.TextBoxBCol;

		ConHelp.AddLabel(LabelA6, Lab6Size, Lab6Loca, Lab6Text, 9);
		LabelA6.TextAlign = ContentAlignment.MiddleCenter;

		LabelA6.Click += (s, e) =>
		{
		    LabelA6.Text = (HookDGetC() 
			? "false" : "true");
		};

		foreach (Control a in ContainerA2.Controls)
		{
		    if ((a is PictureBox) || (a == LabelA6))
		    {
			Tools.Round(a, 6);
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly PictureBox ContainerA1 = new PictureBox();
	readonly PictureBox ContainerA2 = new PictureBox();

	Size CapsuleSize(int NWidth, int Height) =>
	    new Size(Capsule.Width - NWidth, Height);

	void InitA()
	{
	    try
	    {
		var Cont1Size = new Size(Capsule.Width, 93);
		var Cont1Loca = new Point(0, 10);
		var Cont1BCol = Color.FromArgb(112, 74, 125);//(127, 219, 136);//100, 161, 106);

		var Cont2Size = new Size(Cont1Size.Width - 20, 73);
		var Cont2Loca = new Point(10, 10);

		Controls.Image(ContainerA1, ContainerA2, Cont2Size, Cont2Loca, Cont1BCol);
		Controls.Image(Capsule, ContainerA1, Cont1Size, Cont1Loca, Cont1BCol);

		InitA2();

		Tools.Round(ContainerA1, 6);
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly TextBox TextBoxB1 = new TextBox();

	void SendLog(string Text)
	{//Invoker here
	    TextBoxB1.AppendText($"{Text}\r\n");
	}

	void InitB2()
	{
	    try
	    {
		var TextBoxSize = ContainerB2.Size;
		var TextBoxLoca = new Point(0, 0);

		Controls.TextBox(ContainerB3, TextBoxB1, TextBoxSize, TextBoxLoca, ContainerA2.BackColor, Color.White, 1, 8, 
		    ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

		SendLog("(!)  Hey there!  Press \'F1\' for more options.  I did not take the time to add buttons but rather shortcut keys.");
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly PictureBox ContainerB1 = new PictureBox();
	readonly PictureBox ContainerB2 = new PictureBox();
	readonly PictureBox ContainerB3 = new PictureBox();

	void InitB()
	{
	    try
	    {
		int ContHeight = (Capsule.Height - (ContainerA1.Height + ContainerA1.Top + 10));

		var Cont1Loca = new Point(0, ContainerA1.Height + ContainerA1.Top + 10);
		var Cont1Size = new Size(Capsule.Width, ContHeight);
		var Cont1BCol = ContainerA1.BackColor;

		var Cont2Size = new Size(Cont1Size.Width - 14, Cont1Size.Height  - 14);
		var Cont2Loca = new Point(7, 7);

		var Cont3Loca = new Point(0, 0);

		Controls.Image(ContainerB1, ContainerB2, Cont2Size, Cont2Loca, Cont1BCol);
		Controls.Image(ContainerB2, ContainerB3, Cont2Size, Cont3Loca, Cont1BCol);
		Controls.Image(Capsule, ContainerB1, Cont1Size, Cont1Loca, Cont1BCol);

		InitB2();

		Tools.Round(ContainerB1, 6);
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	void HookA()
	{
	    try
	    {
		SendLog("-=-=-=-=-=-=-=-=-=-=-=-=-=-");
		SendLog("+ F1  ->  display this help menu.");
		SendLog("+ F2  ->  select your subdomain list from explorer.");
		SendLog("+ F3  ->  create and select the default subdomain list from memory.");
		SendLog("+ F4  ->  start/stop scanning the target website.");
		SendLog("+ F5  ->  save the current log to your harddrive.");
		SendLog("+ F6  ->  clear this log.");
		SendLog("-=-=-=-=-=-=-=-=-=-=-=-=-=-");
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}

	static List<string> defaultSubdomains()
	{
	    return new List<string>()
	    {
		"mail", "admin", "control", "controls", "panel", "border", "us",
		"raptor", "login", "guest", "user", "server", "remote", "peer",
		"applejuice", "smtp", "apache", "search", "forum", "forums", "game",
		"video", "play", "music", "reply", "confirm", "creation", "apple"
	    };
	}

	public List<string> subDomains = defaultSubdomains();

	void HookB()
	{
	    try
	    {
		using (OpenFileDialog diag = new OpenFileDialog())
		{
		    diag.CheckFileExists = true;
		    diag.CheckPathExists = true;

		    diag.Filter = ("Text File|*.txt");
		    diag.Title = ("Load File Dialog");

		    DialogResult resu = diag.ShowDialog();

		    if (resu == DialogResult.OK)
		    {
			SendLog($"(!) Reading file: {diag.FileName} ....");

			var inst = new DashFramework.Data.Manipulation();

			subDomains.Clear();

			foreach (string line in File.ReadAllLines(diag.FileName))
			{
			    subDomains.Add(inst.Replace(line, "", ".", " "));
			}

			TextBoxA3.Text = diag.FileName;

			SendLog($"(+) Operation has been completed.  {subDomains.Count} potential subdomains loaded!");
		    }

		    else
		    {
			SendLog("(!) Operation has been canceled.");
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}
	
	void HookC()
	{
	    try
	    {
		try
		{
		    SendLog($"(!) Creating the default subdomains list for you ....");

		    File.WriteAllLines("defaultsd.txt", defaultSubdomains());

		    subDomains.Clear();
		    subDomains.AddRange(defaultSubdomains());

		    TextBoxA3.Text = ($@"{Environment.CurrentDirectory}\defaultsd.txt");

		    SendLog($"(+) Operation has been completed. {subDomains.Count} subdomains have been loaded!");
		}

		catch
		{
		    SendLog("(!) Something caused the operation to fail.  Canceling ....");
		}
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	readonly Manipulation DashManip = new Manipulation();
	readonly DashNet DashNet = new DashNet();

	string HookDGetA()
	{
	    return DashNet.StripUrl(TextBoxA1.Text);
	}

        int HookDGetB()
	{
	    return DashNet.GetInteger(TextBoxA4.Text);
	}

	bool HookDGetC()
	{
	    return (LabelA6.Text.Equals("true"));
	}

	void HookDValA()
	{
	    try
	    {
		string domain = (HookDGetA());
		int timeout = (HookDGetB());

		if (timeout < 10)
		{
		    SendLog("(!) Operation has been canceled.  Timeout specified is invalid.  Must be more than 10 MS.");
		    return;
		}

		else if (!DashNet.CanIP(domain))
		{
		    SendLog("(!) Operation has been canceled.  Host could not be resolved to a valid IP address.");
		    return;
		}

		List<int> ports = new List<int>();

		foreach (string obj in TextBoxA2.Text.Split(','))
		{
		    if (!DashNet.CanPort(obj.Replace(" ", "")))
		    {
			SendLog($"(!) Invalid port found in port selection.  Port '{obj}' will not be added!");
			continue;
		    }

		    ports.Add(DashNet.GetPort(obj));
		}

		if (ports.Count < 1)
		{
		    SendLog("(!) Operation has been canceled.  No (valid) ports were specified.");
		    return;
		}

		else if (TextBoxA3.Text.Equals("<select location>") || subDomains.Count < 1)
		{
		    SendLog("(!) Operation has been canceled.  No subdomain list was specified.  Press F3 to use the default subdomain list.");
		    return;
		}

		SendLog($"(!) Started scanning {domain} using {subDomains.Count} subdomains ....");

		// Run asychronously | Take care of asynchronous instance:
		new Thread(() =>
		{
		    try
		    {
			isRunning = true;

			for (int d = 0; d < subDomains.Count; d += 1)
			{
			    if (isRunning)
			    {
				string exists()
				{
				    for (int p = 0; p < ports.Count; p += 1)
				    {
					if (DashNet.IsHostReachable($"{DashNet.GetIP(subDomains[d] + domain)}", ports[p], timeout))
					{
					    return ("does exist");
					}

					else if (DashNet.CanUrl($"{subDomains[d]}.{domain}"))
					{
					    return ("does exist (forcibly converted w/o port use)");
					}
				    }

				    return ("no exist");
				}

				string result = exists();

				if (result.Equals("no exist") && !HookDGetC())
				{
				    continue;
				}

				SendLog($"(!) {subDomains[d]}.{domain} -> {result}");
			    }
			}

			if (!isRunning)
			{
			    throw new Exception("cancel");
			}

			isRunning = false;

			SendLog("(!) Operation complete!  Results are shown above ^");
		    }

		    catch (Exception E)
		    {
			if (E.Message.Equals("cancel"))
			{
			    SendLog("(!) Operation has been canceled by user!");
			}

			else
			{
			    SendLog("(!) Operation has been canceled.  An error occurred while scanning.");
			}
		    }
		})

		{ IsBackground = true }.Start();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	bool isRunning = false;

	void HookD()
	{
	    try
	    {
		if (!isRunning)
		{
		    SendLog("(-) Validating input and starting scan ....");

		    try
		    {
			HookDValA();
		    }

		    catch
		    {
			SendLog("(!) Your current configuration was found to be invalid.  Please correct this and retry.  Please make sure you are using the already present format.  The sub domain list specified should specify merely the names of each individual subdomain rather than dots.  And they should all be on separate lines.  Future code will make this more user-friendly.");
		    }

		    return;
		}
		
		SendLog("(-) Canceling scan ....");
		isRunning = false;
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}
	
	void HookE()
	{
	    try
	    {
		using (SaveFileDialog diag = new SaveFileDialog())
		{
		    diag.OverwritePrompt = true;
		    diag.CheckPathExists = true;

		    diag.Filter = ("Any File|*.*");
		    diag.Title = ("Save Log Dialog");

		    DialogResult resu = diag.ShowDialog();

		    if (resu == DialogResult.OK)
		    {
			File.WriteAllText(diag.FileName, TextBoxB1.Text);
			SendLog($"(!) Operation complete!  File has been saved to: ({diag.FileName})!");
		    }

		    else
		    {
			SendLog("(!) Operation has been canceled.");
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}

	void HookF()
	{
	    try
	    {
		TextBoxB1.Clear();
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}

	void SetHook(Control con)
	{
	    try
	    {
		con.KeyDown += (s, e) =>
		{
		    try
		    {
			switch (e.KeyCode)
			{
			    case Keys.F1: HookA();  break; // Help
			    case Keys.F2: HookB();  break; // Select SD List
			    case Keys.F3: HookC();  break; // Create DEF SD
			    case Keys.F4: HookD();  break; // Toggle Scan
			    case Keys.F5: HookE();  break; // Save Log
			    case Keys.F6: HookF();  break; // Clear Log
			}
		    }

		    catch (Exception E)
		    {
			ErrorHandler.JustDoIt(E);
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}

	void InitC()
	{
	    try
	    {
		for (int a = 0; a < ContainerA1.Controls.Count; a += 1)
		{
		    for (int b = 0; b < ContainerA1.Controls[a].Controls.Count; b += 1)
		    {
			for (int c = 0; c < ContainerA1.Controls[a].Controls[b].Controls.Count; c += 1)
			{
			    SetHook(ContainerA1.Controls[a].Controls[b].Controls[c]);
			}

			SetHook(ContainerA1.Controls[a].Controls[b]);
		    }

		    SetHook(ContainerA1.Controls[a]);
		}

		for (int a = 0; a < ContainerB1.Controls.Count; a += 1)
		{
		    for (int b = 0; b < ContainerB1.Controls[a].Controls.Count; b += 1)
		    {
			for (int c = 0; c < ContainerB1.Controls[a].Controls[b].Controls.Count; c += 1)
			{
			    SetHook(ContainerB1.Controls[a].Controls[b].Controls[c]);
			}

			SetHook(ContainerB1.Controls[a].Controls[b]);
		    }

		    SetHook(ContainerB1.Controls[a]);
		}

		SetHook(ContainerA1);
		SetHook(ContainerA2);
	    }

	    catch (Exception E)
	    {
		throw (GetExep(E));
	    }
	}


	public readonly DashWindow DashApp = new DashWindow();

	readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();

	readonly PictureBox Capsule = new PictureBox();

	public MainGUI()
	{
	    try
	    {
		var AppABCo = Color.FromArgb(243, 255, 189);
		var AppMBCo = Color.FromArgb(1, 93, 145);
		var AppFGCo = Color.White;
		var AppSize = new Size(350, 325);

		DashApp.InitializeWindow(AppSize, ("Subdomain Connector"), AppABCo, AppMBCo, CloseHideApp:false);

		DashApp.MenuBar.LogoLayer1.Top -= 3;
		DashApp.MenuBar.LogoLayer2.Top -= 3;

		DashApp.MenuBar.LogoLayer1.Left = 3;
		DashApp.MenuBar.LogoLayer2.Left = 3;

		DashApp.FormClosing += (s, e) =>
		{
		    Environment.Exit(-1);
		};

		var CapsuleSize = new Size(DashApp.Width - 25, DashApp.Height - 38);
		var CapsuleLoca = new Point(13, 26);

		Controls.Image(DashApp, Capsule, CapsuleSize, CapsuleLoca, AppABCo);

		InitA();
		InitB();
		InitC();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
