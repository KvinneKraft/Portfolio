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
    public partial class MainGUI
    {
	public class KushyRows
	{
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


	    List<RowItem> Rows = new List<RowItem>();

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


	    List<DashPanel> Tables = new List<DashPanel>();

	    public void AddTable(Control Parent, Color BackColor, Color ForeColor)
	    {
		try
		{
		    // Contains Rows: [Title   ],  [Mood   ],  [Url   ]
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

	public void Initiator(DashWindow inst)
	{
	    try
	    {
		// Process Code
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}
    }
}
