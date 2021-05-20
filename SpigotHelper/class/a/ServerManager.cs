using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;
using DashFramework.Erroring;
using DashFramework.Dialog;


namespace SpigotHelper
{
    public class ServerManager
    {
	public readonly System.Timers.Timer ConfigTimer = new System.Timers.Timer() { Enabled = false };

	public string serverBatLocation = (@"F:\Programming\PrivateSociety\Minecraft\Server\run.bat");
	public string serverDirLocation = (@"F:\Programming\PrivateSociety\Minecraft\Server");
	public string updateDirLocation = (@"F:\Programming\MCSpigot\VoyantSMP");

	public bool usePlugMan = true;

	public int configLoadInterval = 10;
	public int pluginLoadInterval = 10;

	public readonly FileSystemWatcher Voyant = new FileSystemWatcher();


	public bool IsServerRunning()
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


	public void StartServerLoader(SectorInitor Inst)
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
			    Inst.SendLog($"(-) Loading plugin {e.Name} into server ....");

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
				void CopyFiles()
				{
				    try
				    {
					File.Copy(e.FullPath, $"{serverDirLocation}\\plugins\\{getName(e.Name)}", true);
				    }

				    catch
				    {
					Inst.SendLog("(!) Could not copy new file!");
				    }
				}

				if (usePlugMan)
				{
				    Inst.SendLog($"(-) Running plugman commands ....");
				    ServerCommand(Inst, $"plugman unload {getName(e.Name).Replace(".jar", "")}");

				    CopyFiles();

				    ServerCommand(Inst, $"plugman load {getName(e.Name).Replace(".jar", "")}");
				    Inst.SendLog($"(+) Operation has been completed!");
				}

				else
				{
				    Inst.SendLog($"(-) Restarting server ....");
				    ServerCommand(Inst, "stop");

				    CopyFiles();

				    ServerCommand(Inst, "start");
				    Inst.SendLog($"(-) Operation has been completed!");
				}
			    }
			}

			catch (Exception E)
			{
			    ErrorHandler.JustDoIt(E);
			}
		    })

		    { IsBackground = true }.Start();
		};

		Voyant.IncludeSubdirectories = true;
		Voyant.EnableRaisingEvents = true;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void WriteConfig()
	{
	    try
	    {
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
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void CreateDefaultConfig(SectorInitor Inst)
	{
	    try
	    {
		if (!File.Exists("SpigotHelper.conf"))
		{
		    Inst.SendLog("(!) SpigotHelper.conf does not exist!");
		    Inst.SendLog("(-) I am creating one just for you ....");

		    WriteConfig();

		    Inst.SendLog("(+) Operation has been completed!");
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void StartConfigLoader(SectorInitor Inst)
	{
	    try
	    {
		void UpdateConfiguration()
		{
		    try
		    {
			CreateDefaultConfig(Inst);

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

			int asInt(string entry) => int.Parse(entry);

			configLoadInterval = asInt(fileLines[0]);
			pluginLoadInterval = asInt(fileLines[1]);

			usePlugMan = (fileLines[2].ToLower() == "true") ? true : false;

			for (int i = 4; i <= 5; i += 1)
			{
			    if (!Directory.Exists(fileLines[i]))
			    {
				throw new Exception("!");
			    }
			}

			if (!File.Exists(fileLines[3]))
			{
			    throw new Exception("!");
			}

			serverBatLocation = fileLines[3];
			serverDirLocation = fileLines[4];
			updateDirLocation = fileLines[5];

			if (!File.Exists($@"{serverDirLocation}\plugins\plugman.jar"))
			{
			    throw new Exception("!");
			}
		    }

		    catch
		    {
			MessageBox.Show($"There was an error while trying to load SpigotHelper.conf!\r\n\r\n" + 
			    "Please ensure that all directories and files exist.\r\n\r\nAlso make sure Plugman.jar" + 
			    " is in your plugins directory under the given name, whenever using this setting in " + 
			    "the configuration file (default=true).", "Oh no!");

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


	public Process ServerProc = new Process();

	public void ServerCommand(SectorInitor Inst, string command)
	{
	    try
	    {
		var Streampy = ServerProc.StandardInput.BaseStream;

		if (Streampy.CanWrite)
		{
		    StreamWriter ServerStream = new StreamWriter(Streampy);

		    ServerStream.WriteLine(command.Replace("/", ""));
		    ServerStream.AutoFlush = true;

		    Inst.SendLog($"(-) Executing: {command}");
		}

		else
		{
		    Inst.SendLog($"(!) Server refused to accept your command!");
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void RefreshServerProcess(SectorInitor Inst)
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
			CreateNoWindow = true,
		    }
		};

		ServerProc.OutputDataReceived += (ss, ee) =>
		{
		    try
		    {
			Console.WriteLine(ee.Data);
			Inst.SendLog(ee.Data);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		};

		void ButtonSwitch()
		{
		    if (!Inst.S1Button1.Text.Equals("Start Server"))
		    {
			Inst.S1Button1.Text = ("Start Server");
		    }
		}

		ServerProc.ErrorDataReceived += (ss, ee) => ButtonSwitch();
		ServerProc.Exited += (ss, ee) => ButtonSwitch();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	[DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] static extern bool IsWindowVisible(IntPtr hWnd);
	[DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();

	public bool IsVisible()
	{
	    try
	    {
		return IsWindowVisible(GetConsoleWindow());
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	[DllImport("User32")] private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

	private const int SW_HIDE = 0;
	private const int SW_SHOW = 5;

	public void Show()
	{
	    try
	    {
		ShowWindow(GetConsoleWindow(), SW_SHOW);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Hide()
	{
	    try
	    {
		ShowWindow(GetConsoleWindow(), SW_HIDE);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
