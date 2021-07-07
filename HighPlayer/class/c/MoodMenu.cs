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
    public class MoodMenu 
    {
	public static readonly DashWindow Parent = new DashWindow();
	
	static readonly DashControls Controls = new DashControls();
        static readonly DashTools Tools = new DashTools();

        public class Init1 
        {
	    public void Initiate(DashWindow Inst)
	    {
		Tools.SortCode(("Main Window Creation"), () =>
		{
		    Parent.InitializeWindow(new Size(300, 250), ("Fluffy Mood Menu"), Inst.BackColor, 
			Color.FromArgb(5, 23, 31), FormStartPosition.Manual);

		    Parent.values.ResizeTitle(15);
		    Parent.values.CenterTitle();
		    Parent.values.HideIcons();
		});
	    }
        }


        public class Init2
        {
	    readonly DashPanel Panel1 = new DashPanel();
	    readonly DashPanel Panel2 = new DashPanel();
	    readonly DashPanel Panel3 = new DashPanel();
	    
	    readonly TextBox TextBox = new TextBox();
	    readonly Label Label = new Label();

	    public void Initiate(DashWindow Inst)
	    {
		Tools.SortCode(("Base Containers"), () =>
		{
		    Size Panel1Size = new Size(Parent.Width - 20, Parent.Height - 80);
		    Size Panel2Size = new Size(Panel1Size.Width, Panel1Size.Height - 24);
		    Size Panel3Size = new Size(Panel2Size.Width - 16, Panel2Size.Height - 16);
		    Point Panel1Loca = new Point(-2, 35);
		    Point Panel2Loca = new Point(0, 24);
		    Point Panel3Loca = new Point(8, 8);
		    Color Panel1BCol = Parent.BackColor;
		    Color Panel2BCol = Color.FromArgb(10, 45, 59);
		    
		    Controls.Panel(Parent, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
		    Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);
		    Controls.Panel(Panel2, Panel3, Panel3Size, Panel3Loca, Panel2BCol);
		});

		Tools.SortCode(("Title Injection"), () =>
		{
		    Point LabelLoca = new Point(0, 0);
		    Color LabelBCol = Panel1.BackColor;
		    Color LabelFCol = Color.White;

		    Controls.Label(Panel1, Label, Size.Empty, LabelLoca, LabelBCol, 
			LabelFCol, ("Loaded Moods"), FontSize: 13);
		});

		Tools.SortCode(("Mod Container"), () =>
		{
		    Size TextBoxSize = Panel3.Size;
		    Point TextBoxLoca = new Point(0, 0);
		    Color TextBoxBCol = Panel3.BackColor;
		    Color TextBoxFCol = Color.White;

		    Controls.TextBox(Panel3, TextBox, TextBoxSize, TextBoxLoca, TextBoxBCol, 
			TextBoxFCol, 1, 12, Multiline: true, FixedSize: false);
		    
		    Tools.Round(Panel2, 6);
		});
	    }
	}


	public class Init3
	{
	    readonly DashPanel Panel1 = new DashPanel();//main bottom bar
	    readonly DashPanel Panel2 = new DashPanel();//bottom bar inner container

	    readonly Button Button1 = new Button();//new mood (prints format text to log)
	    readonly Button Button2 = new Button();//save current mood settings (also to config)
	    readonly Button Button3 = new Button();//

	    public void Initiate(DashWindow Inst, MainGUI Origin)
	    {
		Tools.SortCode(("Containers"), () => 
		{
		    Size Panel1Size = new Size(Parent.Width, 34);
		    Size Panel2Size = new Size(270, 22);
		    Point Panel1Loca = new Point(0, Parent.Height - 34);
		    Point Panel2Loca = new Point(-2, -2);
		    Color Panel1BCol = Parent.values.getBarColor();

		    Controls.Panel(Parent, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
		    Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel1BCol);
		});

		Tools.SortCode(("Buttons"), () =>
		{
		    Quickify quicky = new Quickify()
		    {
			BttnBCol = Panel1.BackColor,
			BttnSize = new Size(75, 22),
			BttnParent = Panel2,
			BttnBorder = true,
			BttnFpts = 12,
		    };

		    Point Button3Loca = new Point(195, 0);
		    Point Button2Loca = new Point(-2, 0);
		    Point Button1Loca = new Point(0, 0);

		    quicky.QuickButton(Button1, ("New"), Button1Loca);
		    quicky.QuickButton(Button2, ("Save"), Button2Loca);
		    quicky.QuickButton(Button3, ("Refresh"), Button3Loca);
		});
	    }
	}


	readonly Init1 Initialize1 = new Init1();
	readonly Init2 Initialize2 = new Init2();
	readonly Init3 Initialize3 = new Init3();

	public void Initiate(DashWindow inst, MainGUI origin)
        {
            try
            {
		Initialize1.Initiate(inst);
		Initialize2.Initiate(inst);
		Initialize3.Initiate(inst, origin);
	    }

            catch (Exception E) 
            {
                ErrorHandler.JustDoIt(E);
            }
        }

	public void Show() { Parent.Show(); Parent.BringToFront(); }
	public void Hide() { Parent.Hide(); Parent.SendToBack();  }
    }
}