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

using DashFramework.Erroring;
using DashFramework.Dialog;


namespace GateHey
{
    public class Var
    {
	public static List<int> Ports = new List<int>();

	public static bool SettingsValidation(Initiator2 MainSettings)
	{
	    try
	    {
		//MainSettings.Dialog1.InitiateM.TxtBox.Text -> Ports

		return true;
	    }

	    catch
	    {
		return false;
	    }
	}
    }
}