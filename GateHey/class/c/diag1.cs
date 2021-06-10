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
    public class Dialog1
    {
	class InitiateTop
	{
	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();


	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    Color barBCol = Inst.values.getBarColor();
		    Size diagSize = new Size(210, 150);
		    Color diagBCol = Inst.BackColor;

		    string diagTitle = ("Clairvoyant - Port Selector");

		    Parent.InitializeWindow(diagSize, diagTitle, diagBCol, barBCol, roundRadius:0, barClose: false);
		    Parent.ShowAsIs(false);

		    Parent.values.CenterTitle();
		    Parent.values.HideIcons();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	class InitiateBottom
	{
	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();

	    
	    readonly DashPanel Panel1 = new DashPanel();
	    readonly DashPanel Panel2 = new DashPanel();

	    readonly Button Bttn1 = new Button();
	    readonly Button Bttn2 = new Button();

	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    Tools.SortCode(("Panels"), () =>
		    {
			Point Panel1Loca = new Point(0, Parent.Height - 30);
			Color Panel1BCol = Parent.values.getBarColor();
			Size Panel1Size = new Size(Parent.Width, 30);

			Point Panel2Loca = new Point(-2, -2);
			Size Panel2Size = new Size(200, 20);
			Color Panel2BCol = Panel1BCol;

			Controls.Panel(Parent, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
			Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);
		    });


		    Tools.SortCode(("Buttons"), () => 
		    {
			void AddButton(Button Bttn, Point Loca, string Text)
			{
			    Size Size = new Size(90, 20);
			    Color BCol = Color.FromArgb(22, 29, 36);
			    Color FCol = Color.White;

			    Controls.Button(Panel2, Bttn, Size, Loca, BCol, FCol, 1, 8, (Text));

			    Bttn.FlatAppearance.MouseDownBackColor = Parent.values.getBarColor();
			    Bttn.MouseEnter += (s, e) => Bttn.BackColor = Color.FromArgb(31, 41, 51);
			    Bttn.MouseLeave += (s, e) => Bttn.BackColor = BCol;

			    Tools.Round(Bttn, 6);
			}

			var Loca2 = new Point(110, 0);
			var Loca1 = new Point(0, 0);

			AddButton(Bttn2, Loca2, ("Close Window"));
			AddButton(Bttn1, Loca1, ("Open File"));

			Bttn1.Click += (s, e) =>
			{
			    // Open File check format
			};

			Bttn2.Click += (s, e) =>
			{
			    Parent.Hide();
			};
		    });
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	class InitiateMiddle
	{
	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();

	    readonly DashPanel Panel = new DashPanel();
	    readonly TextBox TxtBox = new TextBox();

	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    Size PanelSize = new Size(Parent.Width - 4, Parent.Height - 52);
		    Point PanelLoca = new Point(2, 26);
		    Color PanelBCol = Color.FromArgb(22, 29, 36);

		    Controls.Panel(Parent, Panel, PanelSize, PanelLoca, PanelBCol);

		    Size TxtSize = new Size(PanelSize.Width - 10, PanelSize.Height - 10);
		    Point TxtLoca = new Point(5, 5);
		    Color TxtFCol = Color.White;
		    Color TxtBCol = PanelBCol;
		    
		    Controls.TextBox(Panel, TxtBox, TxtSize, TxtLoca, TxtBCol, TxtFCol, 1, 10, Multiline: true, FixedSize: false);

		    TxtBox.Text = string.Format
		    (
			"type in here, first erase me.\r\n" +
			"ranges: 443-8080\r\n" +
			"specific: 1,5,80\r\n" +
			"single: 80"
		    );
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

	public void Initiator(DashWindow inst)
	{
	    try
	    {
		InitiateT.Initiate(Parent, inst);
		InitiateB.Initiate(Parent, inst);
		InitiateM.Initiate(Parent, inst);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}


	public void Show() => Parent.Show();
	public void Hide() => Parent.Hide();
    }
}