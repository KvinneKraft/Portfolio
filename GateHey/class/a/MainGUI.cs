// Author: Dashie
// Version: 1.0


using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;


namespace GateHey
{
    public class Initiator1
    {
	readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();


	readonly DashPanel Panel1 = new DashPanel();
	readonly DashPanel Panel2 = new DashPanel();

	public void Initiate(DashWindow Parent)
	{
	    try
	    {
		var Panel1Size = new Size(Parent.Width, 30);
		var Panel1Loca = new Point(0, Parent.Height - 30);
		var Panel1BCol = Parent.values.getBarColor();

		var Panel2Size = new Size(255, 20);
		var Panel2Loca = new Point(-2, -2);
		var Panel2BCol = Color.White; //Panel1BCol;

		Controls.Panel(Parent, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
		Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }


    public class Initiator2
    {
	readonly DashPanel Panel1 = new DashPanel(); // Outter Capsule (Host Settings Label + Settings Container)
	readonly DashPanel Panel2 = new DashPanel(); // Inner Capsule 1 (Capsule for Inner Capsule 2 for border)
	readonly DashPanel Panel3 = new DashPanel(); // Inner Capsule 2 (Main Capsule for all settings)

	public void Initiate(DashWindow Parent)
	{
	    try
	    {
		// Middle part, host settings etc
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }


    public partial class MainGUI
    {
	readonly Initiator1 InitiateBottom = new Initiator1();
	readonly Initiator2 InitiateMiddle = new Initiator2();

	public void Initiator(DashWindow inst)
	{
	    try
	    {
		InitiateBottom.Initiate(inst);
		InitiateMiddle.Initiate(inst);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}
    }
}
