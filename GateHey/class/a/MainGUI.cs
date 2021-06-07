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

	readonly Button Bttn1 = new Button();
	readonly Button Bttn2 = new Button();
	readonly Button Bttn3 = new Button();

	public void Initiate(DashWindow Parent)
	{
	    try
	    {
		var Panel1Size = new Size(Parent.Width, 30);
		var Panel1Loca = new Point(0, Parent.Height - 30);
		var Panel1BCol = Parent.values.getBarColor();

		var Panel2Size = new Size(320, 20);
		var Panel2Loca = new Point(-2, -2);
		var Panel2BCol = Panel1BCol;

		Controls.Panel(Parent, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
		Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);

		void AddButton(Button Bttn, Point Loca, string Text)
		{
		    var Size = new Size(100, 20);
		    var BCol = Color.FromArgb(22, 29, 36);
		    var FCol = Color.White;

		    Controls.Button(Panel2, Bttn, Size, Loca, BCol, FCol, 1, 8, (Text));

		    Bttn.FlatAppearance.MouseDownBackColor = Panel1BCol;

		    Bttn.MouseEnter += (s, e) =>
		    {
			Bttn.BackColor = Color.FromArgb(31, 41, 51);
		    };

		    Bttn.MouseLeave += (s, e) =>
		    {
			Bttn.BackColor = BCol;
		    };

		    Tools.Round(Bttn, 6);
		}

		var Loca1 = new Point(0, 0);
		var Loca2 = new Point(110, 0);
		var Loca3 = new Point(220, 0);

		AddButton(Bttn1, Loca1, ("Start Scanning"));
		AddButton(Bttn2, Loca2, ("Validate Settings"));
		AddButton(Bttn3, Loca3, ("The T.O.S"));
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }


    public class Initiator2
    {
	readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();


	readonly DashPanel Panel1 = new DashPanel(); // Outter Capsule (Host Settings Label + Settings Container)
	readonly DashPanel Panel2 = new DashPanel(); // Inner Capsule 1 (Capsule for Inner Capsule 2 for border)
	readonly DashPanel Panel3 = new DashPanel(); // Inner Capsule 2 (Main Capsule for all settings)

	public void Initiate(DashWindow Parent)
	{
	    try
	    {
		var TitleSize = 
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
