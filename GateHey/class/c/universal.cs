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

	public static bool SettingsValidation(Initiator2 MainSettings)
	{
	    try
	    {
		var ComponentValues = MainSettings.GetComponentValues();

		if (!DaNet.CanIP(ComponentValues["host"]))
		{
		    return false;
		}

		else if (!DaNet.CanInteger(ComponentValues["timeout"]))
		{
		    return false;
		}

		else if (!DaNet.CanInteger(ComponentValues["threads"]))
		{
		    return false;
		}


		else if (ComponentValues["packdata"].Length < 1)
		{
		    return false;
		}

		int timeout = int.Parse(ComponentValues["timeout"]);
		int threads = int.Parse(ComponentValues["threads"]);

		return (timeout > 1 && threads > 0);
	    }

	    catch
	    {
		return false;
	    }
	}

	public static List<int> Ports = new List<int>();
    }
}