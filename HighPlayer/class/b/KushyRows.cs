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
    public class KushyRows
    {
	readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();


	class RowItem
	{
	    public DashPanel PanelL1 = new DashPanel();//Title, Mood, Url
	    public DashPanel PanelL2 = new DashPanel();//CheckBox


	    public TextBox TxtBox1 = new TextBox();// new TextBox();
	    public TextBox TxtBox2 = new TextBox();
	    public TextBox TxtBox3 = new TextBox();


	    public string Title
	    {
		get { return TxtBox1.Text; }
		set { TxtBox1.Text = value; }
	    }

	    public string Mood
	    {
		get { return TxtBox2.Text; }
		set { TxtBox2.Text = value; }
	    }

	    public string Url
	    {
		get { return TxtBox3.Text; }
		set { TxtBox3.Text = value; }
	    }
	}


	readonly List<RowItem> Rows = new List<RowItem>();

	public void LoadRowsFromConfig()
	{
	    Tools.SortCode((""), () =>
	    {

	    });
	}

	public void AddRow(DashPanel Table, string Title, string Category, string Url)
	{
	    Tools.SortCode((""), () =>
	    {
		// - Add The Panels to the Bottom each time one gets Added. (allow toggle)
		// - Add Size to the Container which is missing, if any.
		// - Reset scrollbar ContentContainer everytime you resize.
	    });
	}


	readonly CustomScrollBar CustomScroller = new CustomScrollBar();

	readonly DashPanel Panel1 = new DashPanel();//Content Container <-- Moves This
	readonly DashPanel Panel2 = new DashPanel();//Parent <-- Adds Scrollbar Onto This
	
	public void AddTable(Control Parent, Color MainBackColor, Color ScrollerBackColor)
	{
	    try
	    {
		Tools.SortCode(("Core Table"), () =>
		{
		    Size Panel1Size = new Size(Parent.Width - 20, Parent.Height);
		    Point Panel1Loca = new Point(0, 0);
		    Color Panel1BCol = MainBackColor;

		    Point Panel2Loca = new Point(Panel1Size.Width, 0);
		    Size Panel2Size = new Size(20, Parent.Height);
		    Color Panel2BCol = ScrollerBackColor;

		    Controls.Panel(Parent, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
		    Controls.Panel(Parent, Panel2, Panel2Size, Panel2Loca, Panel2BCol);
		});

		Tools.SortCode(("Scrollbar Addon"), () =>
		{
		    Color ScrollBarBCol = Tools.NegativeRGB(20, ScrollerBackColor);
		    Size ScrollSize = new Size(20, Panel1.Height);
		    Color ScrollConBCol = Panel2.BackColor;
		    Point ScrollLoca = new Point(0, 0);

		    CustomScroller.ScrollbarSet(Panel2, Panel1, ScrollSize, 
			ScrollLoca, ScrollConBCol, ScrollBarBCol);
		    
		    CustomScroller.SetCollection(Parent);
		});
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	/*
	 - InsertRow (Title, Mood, Url)
	 - RemoveRow (by Url)
	 - AddCheckbox (to each row)
	 - MoveRow (by url, up or down)
	 */
    }
}