﻿// Author: Dashie
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


	public class RowItem
	{
	    public DashPanel PanelL1 = new DashPanel();//Title, Mood, Url
	    public DashPanel PanelL2 = new DashPanel();//CheckBox
	    public DashPanel PanelL3 = new DashPanel();


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


	public readonly List<RowItem> Rows = new List<RowItem>();

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
		if (AddYes)
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
		for (int k = 0, x = 0, y = 0; k < Rows.Count; k += 1, y += 22 + (4/*future-border*/))
		{
		    //if (Rows[k].AllowVisibility())
		    //{
			Rows[k].PanelL1.Location = new Point(x, y);
		    //}
		}
	    });
	}


	public Color RowBColor = Color.FromArgb(8, 8, 8);
	public bool AddToTop = true;

	public void AddRow(DashPanel Table, string Title, string MoodName, string Url)
	{
	    RowItem Row = new RowItem();

	    Tools.SortCode(("Add Row Container"), () =>
	    {
		Size Panel1Size = new Size(Table.Width, 22); // Ball
		Point Panel1Loca = GetRowPosition();
		Color Panel1BCol = RowBColor;

		Controls.Panel(Table, Row.PanelL1, Panel1Size, Panel1Loca, Panel1BCol);

		Size Panel2Size = new Size(Panel1Size.Width - 22, 22);
		Point Panel2Loca = new Point(0, 0);
		Color Panel2BCol = RowBColor;
		
		Size Panel3Size = new Size(22, 22);
		Point Panel3Loca = new Point(Panel1Size.Width - 22, 0);
		Color Panel3BCol = RowBColor;

		Controls.Panel(Row.PanelL1, Row.PanelL2, Panel2Size, Panel2Loca, Panel2BCol);
		Controls.Panel(Row.PanelL1, Row.PanelL3, Panel3Size, Panel3Loca, Panel3BCol);
	    });

	    Tools.SortCode(("Add Column Entries"), () =>
	    {
		void AddColumn(TextBox TxtBox, Size size, Point loca, string text)
		{
		    Color TxtBoxFCol = Color.Black;//Color.White;
		    Color TxtBoxBCol = Color.White;//RowBColor;

		    Controls.TextBox(Row.PanelL2, TxtBox, size, loca,
			TxtBoxBCol, TxtBoxFCol, 1, 10);

		    TxtBox.TextAlign = HorizontalAlignment.Center;
		    TxtBox.Text = text;
		}

		Size TxtBox1Size = new Size(150, 22);
		Point TxtBox1Loca = new Point(0, 0);

		Size TxtBox2Size = new Size(95, 22);
		Point TxtBox2Loca = new Point(TxtBox1Size.Width + 4, 0);

		Size TxtBox3Size = new Size(Row.PanelL2.Width - 245, 22);
		Point TxtBox3Loca = new Point(TxtBox2Loca.X + TxtBox2Size.Width + 4, 0);
	
		AddColumn(Row.TxtBox2, TxtBox2Size, TxtBox2Loca, (MoodName));
		AddColumn(Row.TxtBox1, TxtBox1Size, TxtBox1Loca, (Title));
		AddColumn(Row.TxtBox3, TxtBox3Size, TxtBox3Loca, (Url));;
	    });

	    Tools.SortCode(("Add CheckBox Control"), () => 
	    {
		// Rewrite Class
		// Row.PanelL3 is parent

		Rows.Add(Row);
	    });

	    // - add size to the container which is missing, if any.
	    // - reset scrollbar contentcontainer everytime you resize.
	    // - when filtering, update allow visibility value.
	    // - when updating visibility: reorganize.
	    // - do not forget to set rowbcol

	    ReorganizeRows();
	    UpdateTableSize();
	}


	readonly CustomScrollBar CustomScroller = new CustomScrollBar();

	readonly DashPanel Panel1 = new DashPanel();//Content Container <-- Moves This
	readonly DashPanel Panel2 = new DashPanel();//Parent <-- Adds Scrollbar Onto This
	
	public void UpdateTableSize()
	{
	    try
	    {
		if (Rows.Count < 1)
		    return;
		
		DashPanel Panel = Rows[Rows.Count - 1].PanelL1;

		if (Panel.Height + Panel.Top > Panel1.Height)
		{
		    Tools.Resize(Panel1, new Size(Panel.Width, Panel.Height + Panel.Top));
		}

		CustomScroller.properties.ContentContainer = Panel1;
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}

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
		    Size ScrollSize = new Size(20, Panel2.Height);
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