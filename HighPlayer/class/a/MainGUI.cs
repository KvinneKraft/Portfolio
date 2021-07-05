// Author: Dashie
// Version: 1.0


using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
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
	public readonly static DashControls Controls = new DashControls();
	public readonly static KushyRows DataRow = new KushyRows();
	public readonly static DashTools Tools = new DashTools();


	class Init1
	{
	    public readonly DashPanel Panel1 = new DashPanel();
	    readonly DashPanel Panel2 = new DashPanel();

	    readonly Button Button1 = new Button();
	    readonly Button Button2 = new Button();
	    readonly Button Button3 = new Button();

	    void Hook1()
	    {
		try
		{
		    // Add New Row
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }

	    void Hook2()
	    {
		try
		{
		    // Show Categories Dialog
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }

	    void Hook3() => Environment.Exit(-1);

	    public void Initiate(DashWindow Inst)
	    {
		Tools.SortCode(("Main Panels"), () =>
		{
		    Point Panel1Loca = new Point(0, Inst.Height - 35);
		    Color Panel1BCol = Inst.values.getBarColor();
		    Size Panel1Size = new Size(Inst.Width, 34);

		    Point Panel2Loca = new Point(-2, -2);
		    Color Panel2BCol = Panel1BCol;
		    Size Panel2Size = new Size(380, 24);

		    Controls.Panel(Inst, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
		    Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);
		});

		Tools.SortCode(("Button Addons"), () =>
		{
		    Quickify Quicky = new Quickify()
		    {
			BttnSize = new Size(120, 24),
			BttnBCol = Panel2.BackColor,
			BttnParent = Panel2,
			BttnBorder = true,
			BttnFpts = 12,
		    };

		    Point Bttn2Loca = new Point(130, 0);
		    Point Bttn3Loca = new Point(260, 0);
		    Point Bttn1Loca = new Point(0, 0);

		    Quicky.QuickButton(Button1, "Add New Song", Bttn1Loca);
		    Quicky.QuickButton(Button2, "Mood Menu", Bttn2Loca);
		    Quicky.QuickButton(Button3, "Close Window", Bttn3Loca);
		});

		Tools.SortCode(("Button Event Handlers"), () =>
		{
		    Button1.Click += (s, q) => Hook1();
		    Button2.Click += (s, q) => Hook2();
		    Button3.Click += (s, q) => Hook3();
		});
	    }
	}


	class Init2
	{
	    readonly DashPanel Panel = new DashPanel();

	    public void Initiate(DashWindow Inst)
	    {
		Tools.SortCode(("Datatable Container"), () => 
		{
		    Size PanelSize = new Size(Inst.Width - 6, Inst.Height - Inst.values.getBar().Height - Initialize1.Panel1.Height);
		    Point PanelLoca = new Point(3, Inst.values.getBar().Height);
		    Color PanelBCol = Color.FromArgb(16, 16, 16);

		    Controls.Panel(Inst, Panel, PanelSize, PanelLoca, PanelBCol);
		});

		Tools.SortCode(("Datatable Insertion"), () => 
		{
		    Color ScrollerBCol = Inst.values.getBarColor();
		    Color PanelBCol = Panel.BackColor;

		    DataRow.AddTable(Panel, PanelBCol, ScrollerBCol);
		    DataRow.LoadRowsFromConfig();
		});
	    }
	}


	readonly static Init1 Initialize1 = new Init1();
	readonly static Init2 Initialize2 = new Init2();

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
