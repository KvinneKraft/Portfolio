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


	class Init1
	{
	    readonly DashPanel Panel1 = new DashPanel();
	    readonly DashPanel Panel2 = new DashPanel();

	    readonly Button Button1 = new Button();
	    readonly Button Button2 = new Button();
	    readonly Button Button3 = new Button();

	    public void Initiate(DashWindow Inst)
	    {
		Tools.SortCode(("Main Panels"), () =>
		{
		    Size Panel1Size = new Size(Inst.Width, 34);
		    Point Panel1Loca = new Point(0, Inst.Height - 35);
		    Color Panel1BCol = Inst.values.getBarColor();

		    Size Panel2Size = new Size(330, 24);
		    Point Panel2Loca = new Point(-2, -2);
		    Color Panel2BCol = Panel1BCol;

		    Controls.Panel(Inst, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
		    Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);
		});

		Tools.SortCode(("Button Addons"), () =>
		{

		});
	    }
	}


	class Init2
	{
	    public void Initiate(DashWindow Inst)
	    {
		// Middle Container
	    }
	}


	readonly Init1 Initialize1 = new Init1();
	readonly Init2 Initialize2 = new Init2();

	public void Initiator(DashWindow inst)
	{
	    try
	    {
		Initialize1.Initiate(inst);
		Initialize2.Initiate(inst);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}
    }
}
