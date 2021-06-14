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
    public class Var
    {
	readonly static DashNet DaNet = new DashNet();

	public static bool SettingsValidation(Initiator2 MainSettings)
	{
	    try
	    {
		var ComponentValues = MainSettings.GetComponentValues();

		if (!DaNet.CanIP(ComponentValues["host"]))
		{
		    return false;
		}

		// Host [DashNet]
		// Timeout
		// Threads
		// Packet Data

		return true;
	    }

	    catch
	    {
		return false;
	    }
	}

	public static List<int> Ports = new List<int>();
    }
}