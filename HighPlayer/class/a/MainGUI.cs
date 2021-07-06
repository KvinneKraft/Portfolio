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
		    Point Panel1Loca = new Point(3, Inst.Height - 35);
		    Color Panel1BCol = Inst.values.getBarColor();
		    Size Panel1Size = new Size(Inst.Width - 6, 34);

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
			BttnBCol = Inst.values.getBarColor(),
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
	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();
	    
	    public readonly DashPanel Panel1 = new DashPanel();
	    readonly DashPanel Panel2 = new DashPanel();

	    readonly ClickDropMenu DropMenu = new ClickDropMenu(); 
	    readonly TextBox TextBox = new TextBox();

	    readonly Label Label1 = new Label();
	    readonly Label Label2 = new Label();

	    public void Initiate(DashWindow Inst)
	    {
		Size LabelSize() => Tools.GetFontSize("Search", 16);

		Tools.SortCode(("Main Container"), () =>
		{
		    Size Panel1Size = new Size(Inst.values.getBar().Width - 6, 34);
		    Point Panel1Loca = new Point(3, Inst.values.getBar().Height);
		    Color Panel1BCol = Color.FromArgb(5, 23, 31);

		    Size Panel2Size = new Size(Panel1Size.Width - 40 - LabelSize().Width, 24);
		    Point Panel2Loca = new Point(-2, -2);
		    Color Panel2BCol = Panel1BCol;

		    Controls.Panel(Inst, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
		    Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);
		});

		Tools.SortCode(("Search Label"), () =>
		{
		    Point LabelLoca = new Point(0, -2);
		    Color LabelBCol = Panel1.BackColor;
		    Color LabelFCol = Color.White;

		    Controls.Label(Panel2, Label1, LabelSize(), LabelLoca, LabelBCol, 
			LabelFCol, "Search:", FontSize: 16);

		    Label1.Click += (s, e) => TextBox.Select();
		});

		Tools.SortCode(("Search Bar"), () =>
		{ 
		    Size TextBoxSize = new Size(Panel2.Size.Width - 110 - LabelSize().Width, 24);
		    Point TextBoxLoca = new Point(LabelSize().Width, 0);
		    Color TextBoxBCol = Color.FromArgb(7, 35, 46);
		    Color TextBoxFCol = Color.White;

		    Controls.TextBox(Panel2, TextBox, TextBoxSize, TextBoxLoca, 
			TextBoxBCol, Label1.ForeColor, 1, 12);
		    
		    TextBox.TextAlign = HorizontalAlignment.Center;
		});

		Tools.SortCode(("Mood Filter"), () =>
		{
		    Size DropTriggerSize = new Size(100, 20);
		    Point DropTriggerLoca = new Point(Panel2.Width - 100, -2);
		    Color DropTriggerBCol = Panel1.BackColor;
		    Color DropTriggerFCol = Color.White;

		    Controls.Label(Panel2, Label2, DropTriggerSize, DropTriggerLoca,
			DropTriggerBCol, DropTriggerFCol, ("Mood Filter"), FontSize: 13);

		    Label2.Font = new Font(Label2.Font, FontStyle.Bold);
		    Label2.TextAlign = ContentAlignment.MiddleCenter;

		    Point UpperContainerLoca = new Point(Panel1.Left+Panel2.Left 
			+ DropTriggerLoca.X, Panel1.Top+Panel2.Top+Label2.Top+Label2.Height);

		    Color LowerContainerBCol = Panel1.BackColor;
		    Color UpperContainerBCol = Panel1.BackColor;

		    DropMenu.AddTo(Inst, UpperContainerLoca, UpperContainerBCol, LowerContainerBCol);
		    DropMenu.RegisterVisibilityTrigger(Label2, new Control[] 
		    {
			Initialize1.Panel1, Initialize2.Panel1,
			Initialize3.Panel, Inst, DataRow.Panel1
		    });

		    DropMenu.AddItem("high", "dashie", "is", "me");
		    DropMenu.RegisterUpdateColor
		    (
			Color.FromArgb(9, 40, 54), 
			Color.FromArgb(13, 57, 77),
			Color.FromArgb(16, 70, 94)
		    );
		});

		Tools.SortCode(("Last Touches"), () =>
		{
		    Tools.Round(TextBox.Parent, 4);
		});
	    }
	}


	class Init3
	{
	    public readonly DashPanel Panel = new DashPanel();

	    public void Initiate(DashWindow Inst)
	    {
		Tools.SortCode(("Datatable Container"), () => 
		{
		    Size PanelSize = new Size(Inst.Width - 6, Inst.Height - Initialize2.Panel1.Top - Initialize2.Panel1.Height - Initialize1.Panel1.Height - 2);
		    Point PanelLoca = new Point(3, Initialize2.Panel1.Top + Initialize2.Panel1.Height + 2);
		    Color ScrollerBBCol = Color.FromArgb(7, 35, 46);
		    Color PanelBCol = Inst.values.getBarColor();

		    Controls.Panel(Inst, Panel, PanelSize, PanelLoca, PanelBCol);
		    DataRow.AddTable(Panel, ScrollerBBCol);

		    DataRow.LoadRowsFromConfig();
		});
	    }
	}


	readonly static Init1 Initialize1 = new Init1();
	readonly static Init2 Initialize2 = new Init2();
	readonly static Init3 Initialize3 = new Init3();

	public void Initiator(DashWindow inst)
	{
	    try
	    {
		Initialize1.Initiate(inst);
		Initialize2.Initiate(inst);
		Initialize3.Initiate(inst);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}
    }
}
