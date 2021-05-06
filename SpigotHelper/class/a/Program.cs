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


	readonly System.Timers.Timer ConfigTimer = new System.Timers.Timer() { Enabled = false };

	string serverBatLocation = (@"F:\Programming\PrivateSociety\Minecraft\Server\run.bat");
	string serverDirLocation = (@"F:\Programming\PrivateSociety\Minecraft\Server");
	string updateDirLocation = (@"F:\Programming\MCSpigot\VoyantSMP");

	bool usePlugMan = true;

	int configLoadInterval = 10;
	int pluginLoadInterval = 10;

	FileSystemWatcher Voyant = new FileSystemWatcher();

	void SendLog(string data)
	{
	    if (S2TextBox1.InvokeRequired)
	    {
		S2TextBox1.Parent.Invoke
		(
		    new MethodInvoker
		    (
			() => S2TextBox1.AppendText($"{data}\r\n")
		    )
		);
	    }

	    else
	    {
		S2TextBox1.AppendText($"{data}\r\n");
	    }
	}
	void StartServerLoader()
	{
	    try
	    {
		Voyant.Filter = ("*.jar");
		Voyant.Path = (updateDirLocation);

		Voyant.Created += (s, e) =>
		{
		    new Thread(() =>
		    {
			try
			{
			    SendLog($"(-) Loading plugin {e.Name} into server ....");

			    Thread.Sleep(1000);

			    string getName(string fullName)
			    {
				var data = e.Name.Split('\\');

				if (data.Length > 1)
				{
				    return data[1];
				}

				return data[0];
			    }

			    if (IsServerRunning())
			    {
				if (usePlugMan)
				{
				    SendLog($"(-) Running plugman commands ....");
				    ServerCommand($"plugman unload {getName(e.Name).Replace(".jar", "")}");

				    File.Copy(e.FullPath, $"{serverDirLocation}\\plugins\\{getName(e.Name)}", true);

				    ServerCommand($"plugman load {getName(e.Name).Replace(".jar", "")}");
				    SendLog($"(+) Done!");
				}

				else
				{
				    SendLog($"(-) Restarting server ....");
				    ServerCommand("stop");

				    File.Copy(e.FullPath, $"{serverDirLocation}\\plugins\\{getName(e.Name)}", true);

				    ServerCommand("start");
				    SendLog($"(-) Done!");
				}
			    }

			    SendLog("(+) Plugin has been loaded into plugins folder!");
			}

			catch (Exception E)
			{
			    ErrorHandler.JustDoIt(E);
			}
		    }).Start();
		};

		Voyant.IncludeSubdirectories = true;
		Voyant.EnableRaisingEvents = true;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void CreateDefaultConfig()
	{
	    try
	    {
		if (!File.Exists("SpigotHelper.conf"))
		{
		    SendLog("(!) SpigotHelper.conf does not exist!");
		    SendLog("(-) Creating one just for you ....");

		    using (StreamWriter writer = File.CreateText("SpigotHelper.conf"))
		    {
			writer.WriteLine($"config_loader_interval={configLoadInterval}");
			writer.WriteLine($"plugin_loader_interval={pluginLoadInterval}");
			writer.WriteLine($"use_plugman={usePlugMan}");
			writer.WriteLine($"server_bat={serverBatLocation}");
			writer.WriteLine($"server_dir={serverDirLocation}");
			writer.WriteLine($"update_dir={updateDirLocation}");
			writer.Close();
		    }

		    SendLog("(+) Done!");
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void StartConfigLoader()
	{
	    try
	    {
		void UpdateConfiguration()
		{
		    try
		    {
			CreateDefaultConfig();
			
			var fileLines = new List<string>();

			string data = string.Empty;

			foreach (string line in File.ReadAllLines("SpigotHelper.conf"))
			{
			    fileLines.Add(line.Split('=')[1]);
			    data += line;
			}
			
			if (fileLines.Count != 6)
			{
			    throw new Exception("!");
			}

			int asInt(string entry) =>
			    int.Parse(entry);

			configLoadInterval = asInt(fileLines[0]);
			pluginLoadInterval = asInt(fileLines[1]);
			
			usePlugMan = (fileLines[2].ToLower() == "true") 
			    ? true : false;

			for (int i = 4; i <= 5; i += 1)
			{
			    if (!Directory.Exists(fileLines[i]))
			    {
				throw new Exception ("!");
			    }
			}

			if (!File.Exists(fileLines[3]))
			{
			    throw new Exception("!");
			}

			serverBatLocation = fileLines[3];
			serverDirLocation = fileLines[4];
			updateDirLocation = fileLines[5];

			if (!File.Exists($"{serverDirLocation}\\plugins\\plugman.jar"))
			{
			    throw new Exception("!");
			}
		    }

		    catch
		    {
			MessageBox.Show($"There was an error while trying to load SpigotHelper.conf!\r\n\r\nPlease ensure that all directories and files exist.\r\n\r\nAlso make sure Plugman.jar is in your plugins directory under the given name, whenever using this setting in the configuration file (default=true).", "Oh no!");
			Environment.Exit(-1);
		    }
		}

		UpdateConfiguration();

		if (!ConfigTimer.Enabled)
		{
		    ConfigTimer.Interval = (configLoadInterval * 1000);

		    ConfigTimer.Elapsed += (s, e) =>
		    {
			UpdateConfiguration();
		    };

		    ConfigTimer.AutoReset = true;
		    ConfigTimer.Enabled = true;
		}

		ConfigTimer.Start();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly PictureBox S1Container = new PictureBox();

	readonly Button S1Button1 = new Button();
	readonly Button S1Button2 = new Button();
	readonly Button S1Button3 = new Button();

	Process ServerProc = new Process();
	
	void ServerCommand(string command)
	{
	    try
	    {
		var Streampy = ServerProc.StandardInput.BaseStream;

		if (Streampy.CanWrite)
		{
		    StreamWriter ServerStream = new StreamWriter(Streampy);

		    ServerStream.WriteLine(command.Replace("/", ""));
		    ServerStream.AutoFlush = true;

		    SendLog($"(!) Executing: {command} ....");
		}

		else
		{
		    SendLog($"(!) Server refused to accept your command!");
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	void RefreshServerProcess()
	{
	    try
	    {
		ServerProc = new Process()
		{
		    StartInfo = new ProcessStartInfo()
		    {
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			RedirectStandardInput = true,
			UseShellExecute = false,
			CreateNoWindow = true
		    }
		};

		ServerProc.OutputDataReceived += (ss, ee) =>
		{
		    SendLog(ee.Data);
		};

		void ButtonSwitch()
		{
		    if (!S1Button1.Text.Equals("Start Server"))
		    {
			S1Button1.Text = ("Start Server");
		    }
		}

		ServerProc.ErrorDataReceived += (ss, ee) =>
		    ButtonSwitch();

		ServerProc.Exited += (ss, ee) =>
		    ButtonSwitch();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

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
			if (!S1Button1.Text.Equals("Start Server"))
			{
			    if (IsServerRunning())
			    {
				ServerCommand("stop");
			    }
			}

			else
			{
			    RefreshServerProcess();

			    new Thread
			    (
				() =>
				{
				    ServerProc.StartInfo.WorkingDirectory = ($"{serverDirLocation}");
				    ServerProc.StartInfo.FileName = ($"{serverBatLocation}");
				    ServerProc.Start();

				    ServerProc.BeginOutputReadLine();
				}
			    ).Start();
			}

			S1Button1.Text = ($"{(S1Button1.Text.Equals("Start Server") ? "Stop" : "Start")} Server");
		    }

		    catch (Exception E)
		    {
			ErrorHandler.JustDoIt(E);
		    }
		};

		S1Button2.Click += (s, e) => 
		{
		    try
		    { 
			using (Process proc = new Process())
			{
			    proc.StartInfo = new ProcessStartInfo()
			    {
				UseShellExecute = true,
				FileName = "SpigotHelper.conf"
			    };

			    proc.Start();
			}
		    }

		    catch (Exception E)
		    {
			ErrorHandler.JustDoIt(E);
		    }
		};
		
		S1Button3.Click += (s, e) => 
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

	bool IsServerRunning()
	{
	    try
	    {
		Process.GetProcessById(ServerProc.Id);
	    }

	    catch
	    {
		return false;
	    }

	    return true;
	}

	private void InitSector2ConsoleOutput()
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
			if (IsServerRunning())
			{ 
			    ServerCommand(S2TextBox2.Text);
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

		CreateDefaultConfig();

		StartConfigLoader();
		StartServerLoader();

		SendLog($"(!) Hey there {Environment.UserName}, thank you for using my Spigot Helper!");
		SendLog($"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
		SendLog($"(!) Configuration has been loaded succesfully, you may now make use of the panel safely.");
		SendLog($"(!) If you have any questions, please message me at KvinneKraft@protonmail.com. Thank you fluffz.  -Dashie");
		SendLog($"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
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
		
		InitSector2ConsoleOutput();

		App.FormClosing += (s, e) =>
		{
		    if (IsServerRunning())
		    {
			ServerCommand("stop");
		    }
		};
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
