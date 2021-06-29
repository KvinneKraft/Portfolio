// Author: Dashie
// Version: 1.0


using System;
using System.IO;
using System.Net;
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

namespace HighPlayer
{
    public partial class MainGUI
    {
	public static DashControls Controls = new DashControls();
	public static DashTools Tools = new DashTools();



	public void Initiator(DashWindow inst)
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}
    }
}
