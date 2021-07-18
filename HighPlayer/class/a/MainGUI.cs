﻿// Author: Dashie
// Version: 1.0


using System;
using System.IO;
using System.Net;
using System.Linq;
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
	readonly static MessageContainer MsgContainer = new MessageContainer();
	readonly static DashControls Controls = new DashControls();
	readonly static DashTools Tools = new DashTools();

	
	public static void SendMessage(string Msg, bool IsError = true, int VisibilityTimeout = 2000)
	{
	    MsgContainer.SetColor(IsError ? Color.DarkRed : Color.DarkGreen, Color.White);
	    MsgContainer.Show(Msg, new Point(4, GetMessageBoxY()), VisibilityTimeout);
	}


	public static int GetMessageBoxY() => (Initialize1.RowBar.Visible() ? Initialize1.RowBar.Panel.Height + Initialize3.Toolbar.Panel1.Top 
	    : (Initialize3.Toolbar.Visible() ? Initialize3.Toolbar.Panel1.Height + Initialize3.Toolbar.Panel1.Top : Initialize1.RowBar.Panel.Top));


	public static void UpdateMenu()
	{
	    Tools.SortCode(("Add Items"), () =>
	    {
		Initialize1.RowBar.DropMenu.GetItemStack().Clear();
		Initialize2.DropMenu.GetItemStack().Clear();

		foreach ((string, int) Mood in UrlDatabase.Moods)
		{
		    Initialize1.RowBar.DropMenu.AddItem(Mood.Item1);
		    Initialize2.DropMenu.AddItem(Mood.Item1);
		}
	    });

	    Tools.SortCode(("Set Colors"), () =>
	    {
		void RegisterColor(ClickDropMenu DropMenu)
		{
		    DropMenu.RegisterUpdateColor
		    (
			Color.FromArgb(9, 40, 54),
			Color.FromArgb(13, 57, 77),
			Color.FromArgb(16, 70, 94)
		    );
		}

		RegisterColor(Initialize1.RowBar.DropMenu);
		RegisterColor(Initialize2.DropMenu);
	    });

	    Tools.SortCode(("Set Executor"), () =>
	    {
		foreach (var Item in Initialize2.DropMenu.ItemStack)
		{
		    int Id = Initialize2.DropMenu.ItemStack.IndexOf(Item);

		    Initialize2.DropMenu.SetMouseClickHook(Id, () =>
		    {
			// What to do when the search bar specifies a mood.
		    });
		}

		foreach (var Item in Initialize1.RowBar.DropMenu.ItemStack)
		{
		    int Id = Initialize1.RowBar.DropMenu.ItemStack.IndexOf(Item);

		    Initialize1.RowBar.DropMenu.SetMouseClickHook(Id, () => Initialize1.RowBar
			.URLDatabase.Rows[0].SetMood(Initialize1.RowBar.DropMenu.ItemStack[Id].Item.Text));
		}
	    });
	}


	public class Init1
	{
	    public class InjectBar
	    {
		void Hook1(Control Parent)
		{
		    try
		    {
			var CurrentRow = URLDatabase.Rows[0];

			if (CurrentRow.TxtBox1.Text.Length > 1)
			{
			    if (!CurrentRow.TxtBox2.Text.Equals("Mood"))
			    {
				if (CurrentRow.TxtBox3.Text.Length > 1)
				{
				    UrlDatabase.AddRow(UrlDatabase.Panel1, CurrentRow.TxtBox1.Text,
					CurrentRow.TxtBox2.Text, CurrentRow.TxtBox3.Text);

				    Hide();
				    
				    SendMessage("Successfully added new song to list :D", false);
				}
			    }

			    else
			    {
				SendMessage("You must select a mood.  Please try again.");
			    }

			    return;
			}
			
			SendMessage("You must enter a Title and Url.  Please try again.");
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		void Hook2()
		{
		    try
		    {
			Hide();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		
		public readonly string[] Defaults = { ("Entry Name"), ("Mood"), ("Entry Url") };

		public readonly ClickDropMenu DropMenu = new ClickDropMenu();
		public readonly URLDatabase URLDatabase = new URLDatabase();

		public readonly DashPanel Panel = new DashPanel();

		public void Initialize(DashWindow Inst)
		{
		    try
		    {
			Tools.SortCode(("Add Container"), () =>
			{
			    Size PanelSize = new Size(Inst.Width - 8, 32);
			    Point PanelLoca = new Point(4, 60);
			    Color PanelBCol = Color.FromArgb(5, 23, 31);

			    Controls.Panel(Inst, Panel, PanelSize, PanelLoca, PanelBCol);
			    Panel.Hide();
			});

			DashPanel CurrentTable() => URLDatabase.Panel1;

			Tools.SortCode(("Table and Columns"), () =>
			{
			    URLDatabase.BackgroundColor = Color.FromArgb(5, 23, 31);
			    URLDatabase.CheckBoxColor = Color.FromArgb(7, 35, 46);
			    URLDatabase.ColumnColor = Color.FromArgb(7, 35, 45);

			    URLDatabase.AddTable(Panel, URLDatabase.BackgroundColor);
			    URLDatabase.AddRow(CurrentTable(), Defaults[0], Defaults[1], Defaults[2]);

			    URLDatabase.RemoveExtraHeight();
			    URLDatabase.CenterTable();
			});

			var CurrentRow = URLDatabase.Rows[0];

			Tools.SortCode(("On Click"), () =>
			{
			    void SetTextBoxEvent(URLDatabase.RowItem Item)
			    {
				Item.TxtBox1.Click += (s, e) =>
				{
				    if (Item.TxtBox1.Text.Equals(Defaults[0]))
				    {
					Item.TxtBox1.Clear();
				    }
				};

				Item.TxtBox3.Click += (s, e) =>
				{
				    if (Item.TxtBox3.Text.Equals(Defaults[2]))
				    {
					Item.TxtBox3.Clear();
				    }
				};
			    }

			    SetTextBoxEvent(CurrentRow);
			});

			Tools.SortCode(("Dropdown Menu"), () =>
			{
			    Point MenuLoca = new Point(Panel.Left + CurrentTable().Left + CurrentRow.TxtBox2.Parent.Left - 2, Panel.Top + CurrentTable().Top + CurrentRow.TxtBox2.Parent.Top + CurrentRow.TxtBox2.Parent.Height);
			    Color MenuUpperBCol = Color.FromArgb(5, 23, 31);
			    Color MenuLowerBCol = Color.FromArgb(5, 23, 31);

			    DropMenu.AddTo(Inst, MenuLoca, MenuUpperBCol, MenuLowerBCol);
			    
			    DropMenu.RegisterVisibilityTrigger
			    (
				CurrentRow.TxtBox2, 
				
				new Control[] 
				{
				    Initialize3.Panel, Inst, UrlDatabase.Panel1,
				    Initialize1.Panel1, Initialize2.Panel1,
				}
			    );
			});

			Tools.SortCode(("Other Event Handlers"), () =>
			{
			    void RegisterKeyCodes(Control control)
			    {
				control.KeyDown += (s, e) =>
				{
				    switch (e.KeyCode)
				    {
					case Keys.Enter: Hook1(Inst); break;
					case Keys.Escape: Hook2(); break;
				    }
				};
			    }

			    foreach (Control a in URLDatabase.Rows[0].PanelL1.Controls)
			    {
				foreach (Control b in a.Controls)
				{
				    foreach (Control c in b.Controls)
				    {
					RegisterKeyCodes(c);
				    }

				    RegisterKeyCodes(b);
				}

				RegisterKeyCodes(a);
			    }
			});
		    }

		    catch (Exception E)
		    {
			ErrorHandler.GetException(E);
		    }
		}


		public bool IsDefaultWorthy() => (URLDatabase.Rows[0].TxtBox1.Text.Equals(Defaults[0]));
		public bool Visible() => Panel.Visible;


		public void Show()
		{
		    if (!Initialize3.Toolbar.Visible())
		    {
			if (IsDefaultWorthy())
			{
			    var CurrentRow = URLDatabase.Rows[0];

			    CurrentRow.TxtBox1.Text = Defaults[0];
			    CurrentRow.TxtBox2.Text = Defaults[1];
			    CurrentRow.TxtBox3.Text = Defaults[2];
			}

			Panel.Show();
			return;
		    }

		    SendMessage("You have are already doing something.  Finish first!");
		}

		public void Hide()
		{
		    Panel.Hide();
		}
	    }


	    public readonly InjectBar RowBar = new InjectBar();

	    public readonly DashPanel Panel1 = new DashPanel();
	    public readonly DashPanel Panel2 = new DashPanel();

	    readonly Button Button1 = new Button();
	    readonly Button Button2 = new Button();
	    readonly Button Button3 = new Button();


	    void Hook1()
	    {
		try
		{
		    if (!RowBar.Visible()) RowBar.Show();
		    else RowBar.Hide();
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
		    if (!MoodMenu.IsVisible())
		    {
			Linker.CenterDialog(MoodMenu.Parent, Inst);
			MoodMenu.Show();
		    }

		    else
		    {
			MoodMenu.Hide();
		    }
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
		    Quicky = new Quickify()
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

		Tools.SortCode(("Initializers"), () =>
		{
		    RowBar.Initialize(Inst);
		});
	    }
	}


	public class Init2
	{
	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();

	    public readonly ClickDropMenu DropMenu = new ClickDropMenu();
	    public readonly DashPanel Panel1 = new DashPanel();

	    readonly DashPanel Panel2 = new DashPanel();
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

		    DropMenu.RegisterVisibilityTrigger(Label2, new Control[] {Initialize3
			.Panel, Inst, UrlDatabase.Panel1, Initialize1.Panel1, Initialize2.Panel1});
		    
		    UpdateMenu();
		});

		Tools.SortCode(("Last Touches"), () =>
		{
		    Tools.Round(TextBox.Parent, 4);
		});
	    }
	}


	public readonly static URLDatabase UrlDatabase = new URLDatabase();

	public class Init3
	{
	    public class ToolBar
	    {
		public readonly DashPanel Panel1 = new DashPanel();
		public readonly DashPanel Panel2 = new DashPanel();
		
		readonly Button Button1 = new Button();
		readonly Button Button2 = new Button();
		readonly Button Button3 = new Button();

		void ButtonHandler1()
		{
		    try
		    {
			MessageBox.Show(UrlDatabase.Panel1.Controls.Count.ToString());

			foreach (URLDatabase.RowItem Row in UrlDatabase.Rows.ToList())
			{
			    if (UrlDatabase.IsChecked(Row))
			    {
				UrlDatabase.Rows.Remove(Row);
				UrlDatabase.Panel1.Controls.Remove(Row.PanelL1);

				Row.PanelL1.Dispose();
			    }
			}

			MessageBox.Show(UrlDatabase.Panel1.Controls.Count.ToString());

			UrlDatabase.UpdateTableSize();
			UrlDatabase.ReorganizeRows();

			Hide();
		    }

		    catch (Exception E)
		    {
			throw ErrorHandler.GetException(E);
		    }
		}

		void ButtonHandler2()
		{
		    try
		    {
			foreach (URLDatabase.RowItem Row in UrlDatabase.Rows)
			{
			    if (UrlDatabase.IsChecked(Row))
			    {
				Tools.StartProcess(Row.TxtBox3.Text);
			    }
			}

			Hide();
		    }

		    catch (Exception E)
		    {
			throw ErrorHandler.GetException(E);
		    }
		}

		void ButtonHandler3()
		{
		    try
		    {
			foreach (URLDatabase.RowItem Row in UrlDatabase.Rows)
			{
			    UrlDatabase.SetRowStatus(Row, Button3.Text.Equals("Select All"));
			}

			bool SelectAll = Button3.Text.Equals("Select All");

			Button3.Text = (SelectAll ? "Deselect All" : "Select All");

			if (!SelectAll) Hide(); 
		    }

		    catch (Exception E)
		    {
			throw ErrorHandler.GetException(E);
		    }
		}

		public void Initialize(DashWindow Inst)
		{
		    Tools.SortCode(("Register Containers"), () =>
		    {
			Size Panel1Size = new Size(UrlDatabase.Panel1.Width, 28);
			Point Panel1Loca = new Point(-2, 60);
			Color Panel1BCol = Initialize2.Panel1.BackColor;

			Size Panel2Size = new Size(345, 22);
			Point Panel2Loca = new Point(-2, -2);
			Color Panel2BCol = Panel1BCol;

			Controls.Panel(Inst, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
			Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);

			Panel1.BringToFront();
			Panel1.Hide();
		    });

		    Tools.SortCode(("Register Buttons"), () => 
		    {
			Quicky = new Quickify()
			{
			    BttnSize = new Size(100, 22),
			    BttnBCol = Panel2.BackColor,
			    BttnFCol = Color.White,
			    BttnParent = Panel2,
			    BttnBorder = true,
			    BttnFpts = 8,
			    BttnFid = 0,
			};

			Point Button3Loca = new Point(230, 0);
			Point Button2Loca = new Point(115, 0);
			Point Button1Loca = new Point(0, 0);

			Quicky.QuickButton(Button3, ("Select All"), Button3Loca);
			Quicky.QuickButton(Button1, ("Delete"), Button1Loca);
			Quicky.QuickButton(Button2, ("Open"), Button2Loca);

			Button1.Click += (s, e) => ButtonHandler1();
			Button2.Click += (s, e) => ButtonHandler2();
			Button3.Click += (s, e) => ButtonHandler3();
		    });
		}


		public bool Visible() => Panel1.Visible;

		public void Hide()
		{
		    Panel1.Hide();
		}

		public void Show()
		{
		    Panel1.Show();
		}

		public void RegisterEvents()
		{
		    Tools.SortCode(("Event Register"), () => 
		    {
			foreach (URLDatabase.RowItem Item in UrlDatabase.Rows)
			{
			    Item.WhenUnchecked = () =>
			    {
				if (!UrlDatabase.AreAnyChecked())
				{
				    Hide();
				}
			    };

			    Item.WhenChecked = () =>
			    {
				if (!Visible())
				{
				    Show();
				}
			    };
			}
		    });
		}
	    }


	    public readonly DashPanel Panel = new DashPanel();
	    public readonly ToolBar Toolbar = new ToolBar();

	    public void Initiate(DashWindow Inst)
	    {
		Tools.SortCode(("Datatable Container"), () => 
		{
		    Size PanelSize = new Size(Inst.Width - 6, Inst.Height - Initialize2.Panel1.Top - Initialize2.Panel1.Height - Initialize1.Panel1.Height - 2);
		    Point PanelLoca = new Point(3, Initialize2.Panel1.Top + Initialize2.Panel1.Height + 2);
		    Color ScrollerBBCol = Color.FromArgb(7, 35, 46);
		    Color PanelBCol = Inst.values.getBarColor();

		    Controls.Panel(Inst, Panel, PanelSize, PanelLoca, PanelBCol);
		 
		    UrlDatabase.AddTable(Panel, ScrollerBBCol);
		    UrlDatabase.LoadRowsFromConfig();

		    UpdateMenu();

		    Toolbar.Initialize(Inst);
		    Toolbar.RegisterEvents();
		});
	    }
	}


	public readonly static Init1 Initialize1 = new Init1();
	public readonly static Init2 Initialize2 = new Init2();
	public readonly static Init3 Initialize3 = new Init3();

	static MoodMenu MoodMenu = new MoodMenu();
	static DashWindow Inst = new DashWindow();
	static DashLink Linker = new DashLink();
	static Quickify Quicky = null;

	public static void SetMessageBoxDefaults()
	{
	    if (MsgContainer.ContainerParent == null)
	    {
		MsgContainer.ContainerWidth = Inst.Width - 7;
		MsgContainer.ContainerParent = Inst;
	    }
	}

	public void Initiator(DashWindow inst)
	{
	    try
	    {
		Initialize1.Initiate(inst);
		Initialize2.Initiate(inst);
		Initialize3.Initiate(inst);

		Inst = inst;

		SetMessageBoxDefaults();

		MoodMenu.Initiate(inst, this);
		Linker.CenterDialog(MoodMenu.Parent, Inst);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}
    }
}
