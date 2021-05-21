using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;
using DashFramework.Erroring;
using DashFramework.Dialog;


namespace SpigotHelper
{
    public class SectorInitor
    {
	readonly DashControls Control = new DashControls();
	readonly DashTools Tool = new DashTools();


	public void SendLog(string data)
	{
	    if (S2TextBox1.InvokeRequired)
	    {
		S2TextBox1.Parent.Invoke
		(
		    new MethodInvoker
		    (
			() =>
			{
			    S2TextBox1.AppendText($"{data}\r\n");
			    Console.WriteLine(data);
			}
		    )
		);
	    }

	    else
	    {
		S2TextBox1.AppendText($"{data}\r\n");
	    }
	}


	readonly ServerManager DashServer = new ServerManager();

	void ButtonHookA()
	{
	    try
	    {
		try
		{
		    if (!S1Button1.Text.Equals("Start Server"))
		    {
			if (DashServer.IsServerRunning())
			{
			    DashServer.ServerCommand(this, "stop");
			}
		    }

		    else
		    {
			DashServer.RefreshServerProcess(this);

			new Thread
			(
			    () =>
			    {
				DashServer.ServerProc.StartInfo.WorkingDirectory = ($"{DashServer.serverDirLocation}");
				DashServer.ServerProc.StartInfo.FileName = ($"{DashServer.serverBatLocation}");
				DashServer.ServerProc.Start();

				DashServer.ServerProc.BeginOutputReadLine();
			    }
			).Start();
		    }

		    S1Button1.Text = ($"{(S1Button1.Text.Equals("Start Server") ? "Stop" : "Start")} Server");
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void ButtonHookB()
	{
	    try
	    {
		try
		{
		    using (Process proc = new Process())
		    {
			proc.StartInfo = new ProcessStartInfo()
			{
			    FileName = ("SpigotHelper.conf"),
			    UseShellExecute = true
			};

			proc.Start();
		    }
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void ButtonHookC()
	{
	    try
	    {
		try
		{
		    using (Process proc = new Process())
		    {
			proc.StartInfo = new ProcessStartInfo()
			{
			    UseShellExecute = true,
			    FileName = "https://papermc.io/downloads"
			};

			proc.Start();
		    }
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly PictureBox S1Container = new PictureBox();

	public readonly Button S1Button1 = new Button();
	public readonly Button S1Button2 = new Button();
	public readonly Button S1Button3 = new Button();

	public void InitSector1(DashWindow App, PictureBox Main)
	{
	    try
	    {
		var ContainerImag = Properties.Resources.containerWallpaper as Image;
		var ContainerSize = new Size(252, 48);
		var ContainerLoca = new Point(-2, 15);

		Control.Image(Main, S1Container, ContainerSize, ContainerLoca, Color.White, ContainerImag);

		var ButtonSize = new Size(120, 18);
		var ButtonBCol = App.MenuBar.MenuBar.BackColor;
		var ButtonFCol = Color.LightGray;

		var Button1Loca = new Point(1, 1);
		var Button2Loca = new Point(130, 1);
		var Button3Loca = new Point(-2, 28);

		Control.Button(S1Container, S1Button1, ButtonSize, Button1Loca, ButtonBCol, ButtonFCol, 1, 9, ("Start Server"));
		Control.Button(S1Container, S1Button2, ButtonSize, Button2Loca, ButtonBCol, ButtonFCol, 1, 9, ("Config Server"));
		Control.Button(S1Container, S1Button3, ButtonSize, Button3Loca, ButtonBCol, ButtonFCol, 1, 9, ("API Downloads"));

		foreach (Button Button in S1Container.Controls)
		{
		    Button.BackgroundImage = Properties.Resources.buttonWallpaper;
		    Button.TextAlign = ContentAlignment.BottomCenter;

		    var Loca = new Point(Button.Left - 1, Button.Top - 1);
		    var Size = new Size(121, 19);

		    Tool.PaintRectangle(S1Container, 1, Size, Loca, Color.MidnightBlue);
		}

		S1Button1.Click += (s, e) => 
		{
		    try
		    {
			ButtonHookA();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		};

		S1Button2.Click += (s, e) => 
		{
		    try
		    {
			ButtonHookB();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		};
		
		S1Button3.Click += (s, e) => 
		{
		    try
		    {
			ButtonHookC();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	void HookA()
	{
	    try
	    {
		SendLog("===-Shortcut Keys-===:");
		SendLog("(F1)  --:  display this text.");
		SendLog("(F2)  --:  open your '.minecraft' folder.");
		SendLog("(F3)  --:  open your '/plugins' folder.");
		SendLog("(F4)  --:  open the latest client log.");
		SendLog("(F5)  --:  open the latest server log.");
		SendLog("(F6)  --:  forcibly accept the EULA.");
		SendLog("(F7)  --:  show or hide the console window.");
		SendLog("(F8)  --:  generate server run bat.");
		SendLog("(F9)  --:  download and setup PaperSpigot.");
		SendLog("(F10) --:  download and setup Plugman.");
		SendLog("(F11) --:  clear this log.");
		SendLog("-=-=-=-=-=-=-=-=-=-=-=-");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	
	void OpenLocation(string Path, bool isDirectory = true)
	{
	    if (isDirectory)
	    {
		if (!Directory.Exists(Path))
		{
		    SendLog($"(!) Unable to find the folder path: {Path}");
		    return;
		}
	    }

	    else
	    {
		if (!File.Exists(Path))
		{
		    SendLog($"(!) Unable to find the file path: {Path}");
		    return;
		}
	    }

	    SendLog($"(-) Opening ({(isDirectory ? "folder" : "file")}) {Path} ....");

	    using (Process proc = new Process())
	    {
		proc.StartInfo = new ProcessStartInfo()
		{
		    UseShellExecute = true,
		    FileName = Path,
		};

		proc.Start();
	    }
	}

	void HookB()
	{
	    try
	    {
		string dotminecraft = ($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\.minecraft");
		OpenLocation(dotminecraft);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void HookC()
	{
	    try
	    {
		string plugins = ($@"{DashServer.serverDirLocation}\plugins");
		OpenLocation(plugins);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void HookD()
	{
	    try
	    {
		string clientLog = ($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\.minecraft\logs\latest.log");
		OpenLocation(clientLog, false);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void HookE()
	{
	    try
	    {
		string serverLog = ($@"{DashServer.serverDirLocation}\logs\latest.log");
		OpenLocation(serverLog, false);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void HookF()
	{
	    try
	    {
		string EULAPath = ($@"{DashServer.serverDirLocation}\eula.txt");

		if (!File.Exists(EULAPath))
		{
		    SendLog($@"(!) The path {DashServer.serverDirLocation}\eula.txt does not exist.");
		    return;
		}

		string[] eula = File.ReadAllLines(EULAPath);

		for (int k = 0, u = 0; k < eula.Length; k += 1)
		{
		    if (eula[k].ToLower().Contains("eula="))
		    {
			eula[k] = ("eula=true");
			u = k;

			File.WriteAllLines(EULAPath, eula);

			SendLog("(!) EULA has been set to true!");
		    }

		    if (k == eula.Length - 1)
		    {
			if (!eula[u].Equals("eula=true"))
			{
			    SendLog("(!) File did not contain line: eula=false");
			}
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void HookG()
	{
	    try
	    {
		if (DashServer.IsServerRunning())
		{
		    if (DashServer.IsVisible())
		    {
			DashServer.Hide();
			return;
		    }

		    DashServer.Show();
		    return;
		}

		SendLog("(!) Server is not running.  Press the Start Server button!");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void ServerIsRunning() =>
	    SendLog("(!) You must first stop your running server.  It is running.  If you do not see it, press F12.  It will forcibly kill any java.exe instance.");

	public void HookH()
	{
	    try
	    {
		if (!DashServer.IsServerRunning())
		{
		    string filePath = ($"{DashServer.serverBatLocation}");

		    try
		    {
			SendLog("(-) Creating server run file ....");

			// For version 3.0 a startup parameter editor
			string runtimeLine = ("java -Xms1G -Xmx1G -XX:+UseG1GC -XX:+ParallelRefProcEnabled -XX:MaxGCPauseMillis=200 -XX:+UnlockExperimentalVMOptions -XX:+DisableExplicitGC -XX:+AlwaysPreTouch -XX:G1NewSizePercent=30 -XX:G1MaxNewSizePercent=40 -XX:G1HeapRegionSize=8M -XX:G1ReservePercent=20 -XX:G1HeapWastePercent=5 -XX:G1MixedGCCountTarget=4 -XX:InitiatingHeapOccupancyPercent=15 -XX:G1MixedGCLiveThresholdPercent=90 -XX:G1RSetUpdatingPauseTimePercent=5 -XX:SurvivorRatio=32 -XX:+PerfDisableSharedMem -XX:MaxTenuringThreshold=1 -Dusing.aikars.flags=https://mcflags.emc.gs -Daikars.new.flags=true -jar server.jar nogui");

			File.WriteAllText(filePath, runtimeLine);

			SendLog($"+++: If you wish to change the amount of RAM allocated then change the -Xms1G and -Xmx1G numeric values equally. ");
			SendLog($"(!) Operation has been completed.  Succesfully created {filePath}.");
		    }

		    catch
		    {
			SendLog($"(!) Operation has been canceled.  Unable to write startup command to {filePath}.");
		    }

		    return;
		}

		ServerIsRunning();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void HookI()
	{
	    try
	    {
		if (!DashServer.IsServerRunning())
		{
		    List<string> paperVersions = DashServer.GetPaperSpigotVersions("https://papermc.io/api/v1/paper");

		    if (paperVersions == null || paperVersions.Count < 2)
		    {
			SendLog("(!) Operation has been canceled.  Unable to read response from https://papermc.io/api/v1/paper.  Make sure you are connected to the internet when using this functionality.");
			return;
		    }

		    string downloadUrl = ($"https://papermc.io/api/v1/paper/{paperVersions[0]}/latest/download");



		    return;
		}

		ServerIsRunning();//PAPERSPIGOT | For version 3.0 a custom version selector
				  // Get latest version https://papermc.io/api/v1/paper
				  // Get latest download by version https://papermc.io/api/v1/paper/{url}/latest/download
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void HookJ()
	{
	    try
	    {
		if (DashServer.IsServerRunning())
		{

		    return;
		}

		ServerIsRunning();//PLUGMAN | For version 3.0 a custom version selector
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void HookK()
	{
	    try
	    {
		S2TextBox1.Clear();
		Console.Clear();

		SendLog("(!) Cleared log!");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void HookL()
	{
	    try
	    {
		foreach (Process proc in Process.GetProcesses())
		{
		    if (proc.ProcessName.ToLower() == "java.exe")
		    {
			SendLog($"+++: Killing {proc.ProcessName}:{proc.Id} ....");

			try
			{
			    proc.Kill();
			}

			catch
			{
			    SendLog($"(!) Something went wrong while trying to kill {proc.ProcessName}:{proc.Id}");
			}
		    }
		}

		SendLog("(!) Operation has been completed succesfully.");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	void SetEventHandler(Control To)
	{
	    try
	    {
		To.KeyDown += (s, e) =>
		{
		    switch (e.KeyCode)
		    {
			case Keys.F1: HookA();  break; // A
			case Keys.F2: HookB();  break; // B
			case Keys.F3: HookC();  break; // C
			case Keys.F4: HookD();  break; // D
			case Keys.F5: HookE();  break; // E
			case Keys.F6: HookF();  break; // F
			case Keys.F7: HookG();  break; // G
			case Keys.F8: HookH();  break; // H
			case Keys.F9: HookI();  break; // I
			case Keys.F10: HookJ(); break; // J
			case Keys.F11: HookK(); break; // K
			case Keys.F12: HookL(); break; // L
		    }
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly TextBox S2TextBox1 = new TextBox();
	readonly TextBox S2TextBox2 = new TextBox();

	readonly Button S2Button = new Button();

	readonly Label S2Label2 = new Label();

	private void InitSector2ConsoleOutput(PictureBox Main)
	{
	    try
	    {
		var TextBox1Size = new Size(S2Container2.Width, S2Container2.Height - 20);
		var TextBox1BCol = Color.FromArgb(18, 18, 18);
		var TextBox1Loca = new Point(0, 20);

		Control.TextBox(S2Container2, S2TextBox1, TextBox1Size, TextBox1Loca, TextBox1BCol, Color.White, 1, 7,
		    ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

		var LabelSize = new Size(15, 20);
		var LabelLoca = new Point(0, 0);
		var LabelBCol = Color.FromArgb(10, 10, 10);

		Control.Label(S2Container2, S2Label2, LabelSize, LabelLoca, LabelBCol, Color.White, 1, 9, ("$:"));
		S2Label2.TextAlign = ContentAlignment.MiddleCenter;

		var TextBox2Size = new Size(S2Container2.Width - 90, 20);
		var TextBox2BCol = Color.FromArgb(10, 10, 10);
		var TextBox2Loca = new Point(15, 0);

		Control.TextBox(S2Container2, S2TextBox2, TextBox2Size, TextBox2Loca, TextBox2BCol, Color.White, 1, 9);

		S2TextBox2.KeyDown += (s, e) =>
		{
		    if (e.KeyData == Keys.Enter)
		    {
			S2Button.PerformClick();
		    }
		};

		S2TextBox2.Text = ("/my_command");

		var ButtonSize = new Size(75, 20);
		var ButtonLoca = new Point(S2Container2.Width - 75, 0);
		var ButtonBCol = Color.FromArgb(10, 10, 10);

		Control.Button(S2Container2, S2Button, ButtonSize, ButtonLoca, ButtonBCol, Color.White, 1, 9, ("Execute"));

		S2Button.Click += (s, e) =>
		{
		    try
		    {
			if (DashServer.IsServerRunning())
			{ 
			    DashServer.ServerCommand(this, S2TextBox2.Text);
			}

			else
			{
			    SendLog("(!) Cannot execute command; server is not running.");
			}
		    }

		    catch (Exception E)
		    {
			ErrorHandler.JustDoIt(E);
		    }
		};

		S2Button.TextAlign = ContentAlignment.MiddleCenter;

		DashServer.CreateDefaultConfig(this);
		DashServer.StartConfigLoader(this);
		DashServer.StartServerLoader(this);
		
		SendLog($"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
		SendLog($"+++: Configuration has been loaded succesfully, you may now make use of this panel safely.");
		SendLog($"+++: For optional shortcut keys, press F1, you will be surprised.  (Sarcastic laugh insertion)");
		SendLog($"+++: If you have any questions, please message me at KvinneKraft@protonmail.com. Thank you fluffz.  -Dashie");
		SendLog($"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

		foreach (Control a in Main.Controls)
		{
		    foreach (Control b in a.Controls)
		    {
			foreach (Control c in b.Controls)
			{
			    SetEventHandler(c);
			}

			SetEventHandler(b);
		    }

		    SetEventHandler(a);
		}

		SetEventHandler(S1Container);
		SetEventHandler(Main);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly PictureBox S2Container1 = new PictureBox();
	readonly PictureBox S2Container2 = new PictureBox();

	readonly Label S2Label1 = new Label();

	public void InitSector2(DashWindow App, PictureBox Main)
	{
	    try
	    {
		var Container1Size = new Size(Main.Width, Main.Height - S1Container.Height - S1Container.Top - 25);
		var Container1Loca = new Point(-2, S1Container.Height + S1Container.Top + 25);

		Control.Image(Main, S2Container1, Container1Size, Container1Loca, Color.Transparent, S1Container.BackgroundImage);

		var Label1Loca = new Point(5, 0);

		Control.Label(S2Container1, S2Label1, Size.Empty, Label1Loca, Color.Transparent, Color.White, 1, 16, ("Server Console"));
		S2Label1.Image = S1Container.BackgroundImage;

		var Container2Size = new Size(Main.Width, S2Container1.Height - Label1Loca.Y - S2Label1.Height - 5);
		var Container2Loca = new Point(0, S2Label1.Height + S2Label1.Top + 5);

		Control.Image(S2Container1, S2Container2, Container2Size, Container2Loca, Color.Transparent, S2Label1.Image);
		
		InitSector2ConsoleOutput(Main);

		App.FormClosing += (s, e) =>
		{
		    if (DashServer.IsServerRunning())
		    {
			DashServer.ServerCommand(this, "stop");
		    }
		};

		App.VisibleChanged += (s, e) =>
		{
		    if (App.Visible)
		    {
			DashServer.Hide();
			return; // Gotta make it so it does not show the COnsoleWindow at all
		    }

		    DashServer.Show();
		};

		S2TextBox1.Select();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }

    static class Program
    { 
	static readonly PictureBox DashWindowContainer = new PictureBox();
	static readonly DashWindow DashWindow = new DashWindow();

	static readonly SectorInitor DashInit = new SectorInitor();

	static readonly DashControls Control = new DashControls();
	static readonly DashTools Tool = new DashTools();

	static DashWindow GetDashWindow()
	{
	    try
	    {
		var AppMCol = Color.FromArgb(53, 37, 89);//(132, 66, 245);
		var AppBCol = Color.FromArgb(54, 160, 255);
		var AppSize = new Size(300, 350);

		DashWindow.InitializeWindow(AppSize, ("Spigot Helper"), AppBCol, AppMCol, CloseHideApp: false);

		DashWindow.MenuBar.LogoLayer2.BackgroundImage = Properties.Resources.appWallpaper;
		DashWindow.BackgroundImage = Properties.Resources.appWallpaper;

		var ContainerSize = new Size(280, 330 - DashWindow.MenuBar.MenuBar.Height);
		var ContainerImag = Properties.Resources.containerWallpaper as Image;
		var ContainerLoca = new Point(-2, 36); 

		Control.Image(DashWindow, DashWindowContainer, ContainerSize, ContainerLoca, AppBCol, ContainerImag);

		DashInit.InitSector1(DashWindow, DashWindowContainer);
		DashInit.InitSector2(DashWindow, DashWindowContainer);

		Tool.Round(DashWindowContainer, 8);

		return DashWindow;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	[STAThread]
	static void Main()
	{
	    try
	    {
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(GetDashWindow());
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
        }
    }
}
