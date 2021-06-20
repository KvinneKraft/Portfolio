// Author: Dashie
// Version: 1.0
// Port Selector

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
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


namespace GateHey
{
    public class Universal : DashTools
    {
	readonly static DashTools Tools = new DashTools();
	readonly static DashNet DaNet = new DashNet();


	public static void MsgBox(string msg, bool toggle)
	{
	    if (toggle)
	    {
		Tools.MsgBox(msg, "Port Selector", icon: MessageBoxIcon.Warning);
	    }
	}


	public static int ScanType = 1;//1=single,2=multi,3=ranged

	public static bool PortSettingsCorrect(string data, bool toggle)
	{
	    try
	    {
		bool IsPort(string slice)
		{
		    try
		    {
			int port = int.Parse(slice);
			return (port > 0 && port <= 65535);
		    }

		    catch
		    {
			return false;
		    }
		}

		bool ArePortsOkay(string[] portData)
		{
		    foreach (string portSlice in portData)
		    {
			if (!IsPort(portSlice))
			{
			    return false;
			}
		    }

		    return true;
		}

		List<int> ports = new List<int>();

		string invalidMsg = ("The port selection found within the file seems incorrect.  Please make sure you use the correct format, else errors may occur.");

		if (data.Length < 1)
		{
		    MsgBox(invalidMsg, toggle);
		    return false;
		}

		else if (data.Contains("-"))
		{
		    string[] portData = data.Split('-');

		    if (portData.Length != 2)
		    {
			MsgBox(invalidMsg, toggle);
			return false;
		    }

		    else if (!ArePortsOkay(portData))
		    {
			MsgBox(invalidMsg, toggle);
			return false;
		    }

		    int min = int.Parse(portData[0]);
		    int max = int.Parse(portData[1]);

		    if (min >= max)
		    {
			MsgBox(invalidMsg, toggle);
			return false;
		    }

		    ports.Add(min);
		    ports.Add(max);

		    ScanType = 3;
		}

		else if (data.Contains(","))
		{
		    string[] portData = data.Split(',');

		    if (portData.Length < 1)
		    {
			MsgBox(invalidMsg, toggle);
			return false;
		    }

		    else if (!ArePortsOkay(portData))
		    {
			MsgBox(invalidMsg, toggle);
			return false;
		    }

		    foreach (string dataSlice in portData)
		    {
			ports.Add(int.Parse(dataSlice));
		    }

		    ScanType = 2;
		}

		else
		{
		    if (!IsPort(data))
		    {
			MsgBox(invalidMsg, toggle);
			return false;
		    }

		    ports.Add(int.Parse(data));
		    ScanType = 1;
		}

		Ports.Clear();
		Ports.AddRange(ports);

		return true;
	    }

	    catch
	    {
		return false;
	    }
	}


	public static string LastError = string.Empty;

	public static string GetLastError()
	{
	    return LastError;
	}


	private static void SetError(string Data)
	{
	    LastError = Data;
	}


	public static bool SettingsValidation(MainGUI.Initiator2 MainSettings, bool Toggle = true)
	{
	    try
	    {
		var ComponentValues = MainSettings.GetComponentValues();

		if (!PortSettingsCorrect(MainSettings.Dialog1.InitiateM.TxtBox.Text, Toggle))
		{
		    SetError("invalid port settings");
		    return false;
		}

		if (!DaNet.CanIP(ComponentValues["host"]))
		{
		    SetError("invalid host");
		    return false;
		}

		else if (!DaNet.CanInteger(ComponentValues["timeout"]))
		{
		    SetError("invalid timeout amount");
		    return false;
		}

		else if (!DaNet.CanInteger(ComponentValues["threads"]))
		{
		    SetError("invalid thread count");
		    return false;
		}
		
		else if (ComponentValues["packdata"].Length < 1)
		{
		    SetError("invalid packet data");
		    return false;
		}

		int timeout = int.Parse(ComponentValues["timeout"]);
		int threads = int.Parse(ComponentValues["threads"]);

		return (timeout > 1 && threads > 0);
	    }

	    catch
	    {
		SetError("exception occred.");
		return false;
	    }
	}

	public static List<int> Ports = new List<int>();
	public static bool DoScanning = false;
    }
}