// Author: Dashie
// Version: 1.0


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
    public class Dialog3
    {
	public readonly static DashControls Controls = new DashControls();
	public readonly static DashTools Tools = new DashTools();


	public class InitiateTop
	{
	    readonly DashPanel Panel = new DashPanel();
	    readonly Label Label = new Label();

	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    Tools.SortCode(("Main Window"), () =>
		    {
			Size ParentSize = new Size(350, 250);
			Color ParentBCol = Inst.BackColor;

			Parent.InitializeWindow(ParentSize, ("Service Policy"), ParentBCol, Color.Empty, appMenuBar: false);
		    });

		    Tools.SortCode(("Top Bar"), () =>
		    {
			Size PnlSize = new Size(Parent.Width, 30);
			Color PnlBCol = Inst.values.getBarColor();
			Point PnlLoca = new Point(0, 0);

			Controls.Panel(Parent, Panel, PnlSize, PnlLoca, PnlBCol);
			Tools.Interactive(Panel, Parent);

			Point LblLoca = new Point(-2, -2);
			Color LblFCol = Color.White;

			Controls.Label(Panel, Label, Size.Empty, LblLoca, PnlBCol, LblFCol, ("GateHey - Service Policy"));
			Tools.Interactive(Label, Parent);
		    });

		    Tools.SortCode(("Last Touches"), () =>
		    {
			Size Size = new Size(Parent.Width - 2, Parent.Height - 2);
			Point Loca = new Point(1, 1);
			Color BCol = Panel.BackColor;

			Tools.PaintRectangle(Parent, 2, Size, Loca, BCol);
		    });
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	public class InitiateBottom
	{
	    readonly Button Bttn1 = new Button();
	    readonly Button Bttn2 = new Button();

	    readonly DashPanel Panel1 = new DashPanel();
	    readonly DashPanel Panel2 = new DashPanel();


	    void Bttn1Hook(DashWindow Parent)
	    {
		try
		{
		    Parent.Hide();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    void Bttn2Hook()
	    {
		try
		{
		    using (Process proc = new Process())
		    {
			proc.StartInfo = new ProcessStartInfo()
			{
			    UseShellExecute = true,
			    FileName = ("https://pugpawz.com"),
			};

			proc.Start();
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    Tools.SortCode(("Panels"), () =>
		    {
			Size Pnl1Size = new Size(Parent.Width, 28);
			Point Pnl1Loca = new Point(0, Parent.Height - 28);
			Color Pnl1BCol = Inst.values.getBarColor();

			Size Pnl2Size = new Size(210, 20);
			Point Pnl2Loca = new Point(-2, -2);
			Color Pnl2BCol = Pnl1BCol;

			Controls.Panel(Parent, Panel1, Pnl1Size, Pnl1Loca, Pnl1BCol);
			Controls.Panel(Panel1, Panel2, Pnl2Size, Pnl2Loca, Pnl2BCol);
		    });

		    Tools.SortCode(("Buttons"), () =>
		    {
			void AddButton(Button Bttn, Point Loca, string Text)
			{
			    var Size = new Size(90, 20);
			    var BCol = Color.FromArgb(22, 29, 36);
			    var FCol = Color.White;

			    Controls.Button(Panel2, Bttn, Size, Loca, BCol, FCol, 1, 8, (Text));

			    Bttn.FlatAppearance.MouseDownBackColor = Panel1.BackColor;
			    Bttn.MouseEnter += (s, e) => Bttn.BackColor = Color.FromArgb(31, 41, 51);
			    Bttn.MouseLeave += (s, e) => Bttn.BackColor = BCol;

			    Tools.Round(Bttn, 6);
			}

			var Loca2 = new Point(120, 0);
			var Loca1 = new Point(0, 0);

			AddButton(Bttn1, Loca1, ("Close"));
			AddButton(Bttn2, Loca2, ("Website"));

			Bttn1.Click += (s, e) => Bttn1Hook(Parent);
			Bttn2.Click += (s, e) => Bttn2Hook();
		    });
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	public class InitiateMiddle
	{
	    readonly TextBox TxtBox = new TextBox();
	    readonly DashPanel Panel = new DashPanel();

	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    // TextBox + Panel
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	readonly InitiateBottom InitiateB = new InitiateBottom();
	readonly InitiateMiddle InitiateM = new InitiateMiddle();
	readonly InitiateTop InitiateT = new InitiateTop();

	readonly DashWindow Parent = new DashWindow();

	public void Initiator(DashWindow Inst)
	{
	    try
	    {
		InitiateT.Initiate(Parent, Inst);
		InitiateB.Initiate(Parent, Inst);
		InitiateM.Initiate(Parent, Inst);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}


	public void Show() => Parent.Show();
	public void Hide() => Parent.Hide();
    }
}
