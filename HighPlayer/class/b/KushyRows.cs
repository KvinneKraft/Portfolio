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

	public void AddRow(DashPanel Table, string Title, string Category, string Url)
	{
	    try
	    {
		// Contains Data.
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly CustomScrollBar CustomScroller = new CustomScrollBar();
	readonly DashPanel Panel1 = new DashPanel();
	
	public void AddTable(Control Parent, Color BackColor)
	{
	    try
	    {
		Tools.SortCode(("Core Table"), () =>
		{
		    Size PanelSize = new Size(Parent.Width - 28, Parent.Height - 8);
		    Point PanelLoca = new Point(4, 4);
		    Color PanelBCol = BackColor;
		    
		    Controls.Panel(Parent, Panel1, PanelSize, PanelLoca, PanelBCol);
		});

		Tools.SortCode(("Scrollbar Addon"), () =>
		{
		    Size ScrollSize = new Size(20, Panel1.Height);
		    Point ScrollLoca = new Point(Parent.Width - 20, 4);
		    Color ScrollConBCol = Panel1.BackColor;
		    Color ScrollBarBCol = Color.FromArgb(8,8,8);

		    CustomScroller.ScrollbarSet(Parent, Panel1, ScrollSize, 
			ScrollLoca, ScrollConBCol, ScrollBarBCol);
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