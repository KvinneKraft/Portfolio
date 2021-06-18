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
    public class Universal
    {
	readonly static DashNet DaNet = new DashNet();


	public static string LastError = string.Empty;

	public static string GetLastError()
	{
	    return LastError;
	}


	private static void SetError(string Data)
	{
	    LastError = Data;
	}


	public static bool SettingsValidation(MainGUI.Initiator2 MainSettings)
	{
	    try
	    {
		var ComponentValues = MainSettings.GetComponentValues();

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
    }
}