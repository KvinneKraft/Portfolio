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


	    public bool AllowVisibility() => ICanHasVisibility;
	    public bool ICanHasVisibility = true;
	}

	readonly List<RowItem> Rows = new List<RowItem>();

	readonly List<(string, int, string)> Urls = new List<(string, int, string)>();
	readonly List<(string, int)> Moods = new List <(string, int)>();

	public void LoadRowsFromConfig(bool AddYes = true)
	{
	    Tools.SortCode(("Data Existence Validation"), () =>
	    {
		if (!File.Exists(@"data\settings-data.txt"))
		{
		    if (!Directory.Exists("data"))
		    {
			try
			{
			    Directory.CreateDirectory("data");
			}

			catch
			{
			    throw new Exception("unable to access/create folder 'data'");
			}
		    }

		    try
		    {
			File.WriteAllText(@"data\settings-data.txt",
			    resources.Resources.settings_data);
		    }

		    catch
		    {
			throw new Exception(@"unable to access/create 'data\settings-data.txt'");
		    }
		}

		if (!File.Exists(@"data\youtube-links.txt"))
		{
		    try
		    {
			File.WriteAllText(@"data\youtube-links.txt",
			    resources.Resources.youtube_links);
		    }

		    catch
		    {
			throw new Exception(@"unable to access/create 'data\youtube-links.txt'");
		    }
		}
	    });

	    bool isInteger(string me)
	    {
		try
		{
		    return
		    (
			int.Parse(me).
			ToString() == me
		    );
		}

		catch
		{
		    return false;
		}
	    }

	    Tools.SortCode(("Load Moods"), () =>
	    {
		string[] arr = File.ReadAllText(@"data\settings-data.txt").Replace("\"", "").Replace(", ", ",").Split(',');

		if (arr.Length < 2)
		    throw new Exception("invalid format in moods file");

		Moods.Clear();

		for (int k = 0; k < arr.Length / 2; k += 2)
		{
		    try
		    {
			if (arr.Length < k + 1 || arr[k].Length < 0 || !isInteger(arr[k + 1]))
			    throw new Exception("!");
			
			else if (arr[k] == " " || arr[k + 1] == " ")
			    continue;

			Moods.Add((arr[k], int.Parse(arr[k + 1])));
		    }

		    catch
		    {
			throw new Exception($"found invalid integral value in moods file");
		    }
		}
	    });

	    Tools.SortCode(("Load Urls"), () =>
	    {
		string[] arr = File.ReadAllText(@"data\youtube-links.txt").Replace("\"", "").Replace(", ", ",").Split(',');

		if (arr.Length < 3)
		    throw new Exception("invalid format in url file");

		Urls.Clear();

		for (int k = 0; k < arr.Length - 1; k += 3)
		{
		    if (arr[k].Length < 1 || arr[k + 2].Length < 1 || !isInteger(arr[k + 1]))
			throw new Exception("invalid settings format");

		    else if (arr[k] == " " || arr[k + 1] == " " || arr[k + 2] == " ")
			continue;

		    Urls.Add((arr[k], int.Parse(arr[k + 1]), arr[k + 2]));
		}
	    });

	    Tools.SortCode(("Optional Row Injector"), () => 
	    {
		for (int k = 0; k < Urls.Count; k += 1) AddRow(Panel1,
		    Urls[k].Item1, Moods[Urls[k].Item2].Item1, Urls[k].Item3);
	    });
	}


	Point GetRowPosition()
	{
	    try
	    { 
		if (Rows.Count > 0)
		{
		    if (AddToTop)
		    {
			return new Point
			(
			    0,
			    Rows[Rows.Count].PanelL1.Top +
			    Rows[Rows.Count].PanelL1.Height
			);
		    }

		    return new Point(0, 0);
		}
	    }

	    catch{}

	    return Point.Empty;
	}


	void ReorganizeRows()
	{
	    Tools.SortCode(("Row Reorganization"), () => 
	    {
		for (int k = 0, x = 0, y = 0; k < Rows.Count; k += 1, y += 22)
		{
		    if (Rows[k].AllowVisibility())
		    {
			Rows[k].PanelL1.Location = new Point(x, y);
		    }
		}
	    });
	}


	public Color RowBColor = Color.Empty;
	public bool AddToTop = true;

	public void AddRow(DashPanel Table, string Title, string MoodName, string Url)
	{
	    Tools.SortCode(("Add Row Container"), () =>
	    {
		RowItem Row = new RowItem();

		Size Panel1Size = new Size(Table.Width, 22);
		Point Panel1Loca = GetRowPosition();
		Color Panel1BCol = RowBColor;

		Size Panel2Size = new Size(Panel1Size.Width - 16, 22);
		Point Panel2Loca = new Point(0, 0);
		Color Panel2BCol = RowBColor;

		Controls.Panel(Row.PanelL1, Row.PanelL2, Panel2Size, Panel2Loca, Panel2BCol);
		Controls.Panel(Table, Row.PanelL1, Panel1Size, Panel1Loca, Panel1BCol);
	    });

	    Tools.SortCode(("Add Column Entries"), () =>
	    {
		// Row.PanelL1 is parent
		// [][][] by 100%
	    });

	    Tools.SortCode(("Add CheckBox Control"), () => 
	    {
		// Rewrite Class
		// Row.PanelL2 is parent
	    });

	    // - add size to the container which is missing, if any.
	    // - reset scrollbar contentcontainer everytime you resize.
	    // - when filtering, update allow visibility value.
	    // - when updating visibility: reorganize.
	    // - do not forget to set rowbcol

	    ReorganizeRows();
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