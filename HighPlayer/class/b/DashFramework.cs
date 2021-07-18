
//
// All rights reserved to me Dashie for coding all of this.  If you wish to make use of this code, you can.
// Just make sure to leave this top part in if you are going to redistribute any part of my code.  Thank you.
//
// -Dashie

#pragma warning disable IDE1006

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Net.Sockets;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Security.Principal;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Runnables;
using DashFramework.DashLogic;
using DashFramework.Erroring;
using DashFramework.Dialog;

using HighPlayer.resources;

namespace DashFramework
{
    namespace AsyncSafe
    {
	public class AsyncSendMessage
	{
	    Runnable runnables = new Runnable();

	    public delegate void runnableDelegate();
	    public runnableDelegate messageHandler;
	    public int updateInterval = 350;

	    public AsyncSendMessage()
	    {
		runnables.RunTaskLaterAsynchronously
		(
		    null, 
		
		    () => 
		    {
			foreach (string message in messageQueue)
			{
			    if (messageHandler == null)
			    {
				continue;
			    }

			    messageHandler();
			}
		    }, 
		
		    updateInterval, 
		    true
		);
	    }


	    readonly List<string> messageQueue = new List<string>();

	    public void Send(string content)
	    {
		messageQueue.Add(content);
	    }
	}
    }


    namespace Runnables
    {
	public class Runnable
	{
	    public delegate void RunnableHolder();


	    public void RunTaskAsynchronously(Control parent, RunnableHolder execute)
	    {
		try
		{
		    new Thread(() =>
		    {
			if (parent != null)
			{
			    parent.Invoke
			    (
				new MethodInvoker
				(
				    () =>
				    {
					try
					{
					    execute();
					}

					catch (Exception E)
					{
					    throw (ErrorHandler.GetException(E));
					}
				    }
				)
			    );
			}

			else
			{
			    execute();
			}
		    })

		    { IsBackground = true }.Start();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void RunTaskSynchronously(Control parent, RunnableHolder execute)
	    {
		try
		{
		    parent.Invoke(new MethodInvoker(() =>
		    {
			try
			{
			    execute();
			}

			catch (Exception E)
			{
			    throw (ErrorHandler.GetException(E));
			}
		    }));
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void RunTaskLater(Control parent, RunnableHolder execute, int startWhen, bool autoReset = false)
	    {
		try
		{
		    System.Timers.Timer timer = new System.Timers.Timer();

		    timer.Elapsed += (s, e) =>
		    {
			execute();
		    };

		    timer.AutoReset = autoReset;
		    timer.Interval = startWhen;
		    timer.Enabled = true;
		    timer.Start();
		}

		catch (Exception E)
		{
		    throw ErrorHandler.GetException(E);
		}
	    }


	    public void RunTaskLaterAsynchronously(Control parent, RunnableHolder execute, int startWhen, bool autoReset = false)
	    {
		try
		{
		    System.Timers.Timer timer = new System.Timers.Timer();

		    timer.Elapsed += (s, e) =>
		    {
			new Thread(() =>
			{
			    if (parent != null)
			    {
				parent.Invoke
				(
				    new MethodInvoker
				    (
					() =>
					{
					    try
					    {
						execute();
					    }

					    catch (Exception E)
					    {
						throw (ErrorHandler.GetException(E));
					    }
					}
				    )
				);
			    }

			    else
			    {
				execute();
			    }
			})

			{ IsBackground = true }.Start();
		    };

		    timer.AutoReset = autoReset;
		    timer.Interval = startWhen;
		    timer.Enabled = true;
		    timer.Start();
		}

		catch (Exception E)
		{
		    throw ErrorHandler.GetException(E);
		}
	    }
	}
    }


    namespace Data
    {
	public class Dashlet<A, B, C>//Three Datatype Storage
	{
	    public A a;
	    public B b;
	    public C c;

	    public A Item1() => a;
	    public B Item2() => b;
	    public C Item3() => c;

	    public Dashlet(A a, B b, C c)
	    {
		this.a = a;
		this.b = b;
		this.c = c;
	    }
	}


	public class DashList<A>//One Datatype Storage
	{
	    private readonly List<A> a = new List<A>();

	    public A Get(int id)
	    {
		if (a.Count - 1 < id)
		{
		    return default(A);
		}

		return a[id];
	    }

	    public bool Remove(int id)
	    {
		if (a.Count - 1 < id)
		{
		    return false;
		}

		a.RemoveAt(id);

		return true;
	    }
	}


	public class Manipulation
	{
	    public List<string> GetLineIf(string file, string contains)
	    {
		try
		{
		    var lines = new List<string>();

		    if (!File.Exists(file))
		    {
			return null;
		    }

		    foreach (string line in File.ReadAllLines(file))
		    {
			if (line.Contains(contains))
			{
			    lines.Add(line);
			}
		    }

		    return lines;
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public List<object> ListRemove(List<object> obj, object criteria)
	    {
		try
		{
		    for (int id = 0; id < obj.Count; id += 1)
		    {
			if (obj[id] == criteria)
			{
			    obj.RemoveAt(id);
			}
		    }

		    return obj;
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public string Replace(string obj, string to, params string[] wha)
	    {
		try
		{
		    foreach (string criteria in wha)
		    {
			obj = obj.Replace(criteria, to);
		    }

		    return obj;
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	public class System
	{
	    public bool MoveFile(string from, string to, bool exceptionFail = false)
	    {
		try
		{
		    if (!File.Exists(from))
		    {
			throw new Exception("#1");
		    }

		    else if (File.Exists(to))
		    {
			File.Delete(to);
		    }

		    File.Move(from, to);

		    if (!File.Exists(to))
		    {
			throw new Exception("#2");
		    }

		    return true;
		}

		catch (Exception E)
		{
		    if (exceptionFail)
		    {
			ErrorHandler.GetException(E);
		    }
		}

		return false;
	    }
	}
    }


    namespace Interface
    {
	namespace Controls
	{
	    public class DashPanel : Panel
	    {
		public void AddChild(Control control)
		{
		    try
		    {
			Controls.Add(control);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}
	    }


	    public class DashTreeView : TreeView
	    {
		public void AddNode(string name)
		{
		    try
		    {
			TreeNode node = new TreeNode(name);

			Nodes.Add(node);
			Update();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void RemoveNode(string node)
		{
		    try
		    {
			if (Nodes.ContainsKey(node))
			{
			    Nodes.RemoveByKey(node);
			    Update();
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void ResetNodes()
		{
		    try
		    {
			Nodes.Clear();
			Update();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}
	    }


	    public class DashListBox : ListBox
	    {
		public void Add(object obj)
		{
		    Items.Add(obj);
		}


		public void Remove(int Id = 0)
		{
		    if (Items.Count > Id)
		    {
			Items.RemoveAt(Id);
		    }
		}


		public string Get(int Id = -1, object Item = null)
		{
		    if (Id > -1)
		    {
			if (Items.Count > -1)
			{
			    return Items[Id].ToString();
			}
		    }

		    else if (Item != null)
		    {
			if (Items.Contains(Item))
			{
			    return Items[Items.IndexOf(Item)].ToString();
			}
		    }

		    return string.Empty;
		}


		public bool IsNull()
		{
		    return (this == null);
		}
	    }


	    public class CustomScroller
	    {
		public class Properties
		{
		    public Control.ControlCollection Children;
		    public Control ContentContainer;
		    public Control Parent;

		    public Control.ControlCollection GetCollection()
		    {
			return Children;
		    }

		    public Control GetContentContainer()
		    {
			return ContentContainer;
		    }

		    public Control GetParent()
		    {
			return Parent;
		    }

		    public bool HasBeenSetup()
		    {
			return (Parent != null && Children != null && ContentContainer != null);
		    }
		}


		public readonly Properties properties = new Properties();


		public bool ScrollingDown(MouseEventArgs e)
		{
		    return (e.Delta < 1);
		}


		public int MinimumHeight = 100; 

		public void RegMouseEventHandler()
		{
		    try
		    {
			if (!properties.HasBeenSetup())
			{
			    return;
			}

			foreach (Control Control in properties.Children)
			{
			    Control.MouseWheel += (s, e) =>
			    {
				if (properties.ContentContainer.Height <= MinimumHeight)
				{
				    return;
				}

				int ContentContainerIncrement = 50;
				
				if (ScrollingDown(e))
				{
				    if (properties.ContentContainer.Bottom <= properties.ContentContainer.Parent.Height)
				    {
					properties.ContentContainer.Top = -(properties.ContentContainer.Height - properties.ContentContainer.Parent.Height);
					return;
				    }

				    properties.ContentContainer.Top -= ContentContainerIncrement;
				}

				else
				{
				    if (properties.ContentContainer.Top >= 0)
				    {
					properties.ContentContainer.Top = 0;
					return;
				    }

				    properties.ContentContainer.Top += ContentContainerIncrement;
				}
			    };
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void SetCollection(Control parent)
		{
		    try
		    {
			properties.Children = parent.Controls;
			
			void AddToCollection(Control This)
			{
			    properties.Children.Add(This);
			}

			foreach (Control a in parent.Controls)
			{
			    foreach (Control b in a.Controls)
			    {
				foreach (Control c in b.Controls)
				{
				    foreach (Control d in c.Controls)
				    {
					AddToCollection(d);
				    }

				    AddToCollection(c);
				}

				AddToCollection(b);
			    }

			    AddToCollection(a);
			}

			RegMouseEventHandler();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		
		readonly DashControls Controls = new DashControls();
		readonly DashTools Tools = new DashTools();

		public void ScrollbarSet(Control parent, Control contentContainer)
		{
		    try
		    {
			properties.ContentContainer = contentContainer;
			properties.Parent = parent;

			SetCollection(parent);
		    }

		    catch (Exception E)
		    {
			ErrorHandler.JustDoIt(E);
		    }
		}
	    }


	    public class AshamedClass//I spit on it.
	    {
		private readonly DashControls Control = new DashControls();


		public Control TextBoxParent = new Control();

		public Color TextBoxBCol = Color.FromArgb(28, 28, 28);
		public Color TextBoxFCol = Color.White;

		public void AddTextBox(TextBox TextBox, Size Size, Point Loca, string Text, int FontHeight = 8)
		{
		    try
		    {
			TextBox.TextAlign = HorizontalAlignment.Center;
			TextBox.Text = (Text);

			Control.TextBox(TextBoxParent, TextBox, Size, Loca, TextBoxBCol, TextBoxFCol, 1, FontHeight);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public Size TextBoxSize(Size Size, Point Loca, int Height = 21)
		{
		    try
		    {
			int Width = (TextBoxParent.Width - Loca.X - Size.Width);
			return new Size(Width, Height);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public Control LabelParent = new Control();

		public Color LabelBCol = Color.FromArgb(28, 28, 28);
		public Color LabelFCol = Color.White;

		public void AddLabel(Label Label, Size Size, Point Loca, string Text, int FontHeight = 10)
		{
		    try
		    {
			Control.Label(LabelParent, Label, Size, Loca, LabelBCol, LabelFCol, Text, 1, FontHeight);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		private readonly DashTools Tool = new DashTools();

		public Size GetFontSize(string Text, int FontHeight = 10)
		{
		    try
		    {
			Font Font = Tool.GetFont(1, FontHeight);
			return TextRenderer.MeasureText(Text, Font);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public Point ControlX(Size Size, Point Loca, int Y = -1, int Extra = 0)
		{
		    try
		    {
			int X = (Size.Width + Loca.X + Extra);

			if (Y == -1)
			    Y = Loca.Y;

			return new Point(X, Y);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}
	    }


	    public class Quickify
	    {
		private readonly DashControls Controls = new DashControls();
		private readonly DashTools Tools = new DashTools();


		public Control BttnParent = null;

		public Color BttnBCol = Color.Black;
		public Color BttnFCol = Color.White;

		public Size BttnSize = Size.Empty;
		public bool BttnBorder = true;

		public int BttnFpts = 8;
		public int BttnFid = 1;

		void ApplyToButton(ValueType Type, object With)
		{
		    try
		    {
			switch ((int)Type)
			{
			    case 0: BttnParent = (Button)With; break;
			    case 9: BttnBorder = (bool)With; break;
			    case 1: BttnFCol = (Color)With; break;
			    case 2: BttnBCol = (Color)With; break;
			    case 5: BttnSize = (Size)With; break;
			    case 3: BttnFpts = (int)With; break;
			    case 4: BttnFid = (int)With; break;
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		public void QuickButton(Button button, string text, Point loca, Control parent = null)
		{
		    parent = parent == null ? BttnParent : parent;

		    try
		    {
			Controls.Button(parent, button, BttnSize,
			    loca, BttnBCol, BttnFCol, BttnFid, BttnFpts, text);

			if (BttnBorder)
			    Tools.Round(button, 6);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public Control TxtboxParent = null;

		public Color TxtboxBCol = Color.Black;
		public Color TxtboxFCol = Color.White;

		public Size TxtboxSize = Size.Empty;

		public bool TxtboxBorder = true;
		public bool Multiline = false;
		public bool Scrollbar = false;
		public bool FixedSize = true;
		public bool Readonly = true;

		public int TxtboxFpts = 8;
		public int TxtboxFid = 1;

		void ApplyToTextBox(ValueType Type, object With)
		{
		    try
		    {
			switch ((int)Type)
			{
			    case 0: TxtboxParent = (Control)With; break;
			    case 9: TxtboxBorder = (bool)With; break;
			    case 1: TxtboxFCol = (Color)With; break;
			    case 2: TxtboxBCol = (Color)With; break;
			    case 5: TxtboxSize = (Size)With; break;
			    case 10: FixedSize = (bool)With; break;
			    case 3: TxtboxFpts = (int)With; break;
			    case 6: Multiline = (bool)With; break;
			    case 7: Scrollbar = (bool)With; break;
			    case 4: TxtboxFid = (int)With; break;
			    case 8: Readonly = (bool)With; break;
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		public void QuickTxtBox(TextBox txtbox, string text, Point loca, Control parent = null)
		{
		    parent = parent == null ? TxtboxParent : parent;

		    try
		    {
			Controls.TextBox(parent, txtbox, TxtboxSize, loca, TxtboxBCol,
			    TxtboxFCol, TxtboxFpts, TxtboxFid, Readonly, Multiline, Scrollbar, FixedSize);

			if (TxtboxBorder)
			    Tools.Round(txtbox.Parent is
				PictureBox ? txtbox.Parent : txtbox, 6);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public Control LblParent = null;

		public Color LblBCol = Color.Black;
		public Color LblFCol = Color.White;

		public int LblFpts = 8;
		public int LblFid = 1;

		void ApplyToLabel(ValueType Type, object With)
		{
		    try
		    {
			switch ((int)Type)
			{
			    case 0: LblParent = (Control)With; break;
			    case 1: LblFCol = (Color)With; break;
			    case 2: LblBCol = (Color)With; break;
			    case 3: LblFpts = (int)With; break;
			    case 4: LblFid = (int)With; break;
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		public void QuickLabel(Label label, string text, Point loca, Control parent = null)
		{
		    parent = parent == null ? LblParent : parent;

		    try
		    {
			Size lblSize = Tools.GetFontSize(text, LblFpts, LblFid);

			Controls.Label(parent, label, lblSize, loca, LblBCol,
			    LblFCol, text, LblFid, LblFpts);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public enum ValueType
		{
		    Parent = 0, FCol, BCol, Fpts,
		    Fid, Size, Mline, Sbar, Ronly,
		    Brdr, Fixs
		};

		public enum Module
		{
		    Label = 0, Button, TextBox
		};

		public void SetValue(Module For, ValueType Type, object With)
		{
		    switch ((int)For)
		    {
			case 0: ApplyToLabel(Type, With); break;
			case 1: ApplyToButton(Type, With); break;
			case 2: ApplyToTextBox(Type, With); break;
		    }
		}
	    }


	    public class DashControls
	    {
		private readonly DashTools Tool = new DashTools();

		public void CheckBox(Control Top, PictureBox Container1, PictureBox Container2, Size Size, Point Loca, Color DeselectedBCol, [Optional] Color SelectedBCol, [Optional] bool Select)
		{//fukin recode this, you lazy fyck.
		    try
		    {
			Image(Top, Container1, Size, Loca, DeselectedBCol);

			Size.Height -= 4;
			Size.Width -= 4;

			Image(Container1, Container2, Size, new Point(2, 2), DeselectedBCol);

			Tool.Round(Container1, 2);
			Tool.Round(Container2, 2);

			if (Select)
			    Container2.BackColor = SelectedBCol;

			void UpdateColor()
			{
			    Color Col = SelectedBCol;

			    if (Container2.BackColor == Col)
			    {
				Col = DeselectedBCol;
			    }

			    Container2.PerformLayout();
			    Container2.BackColor = Col;
			};

			Tool.RegisterClickEvent(Container1, UpdateColor);
			Tool.RegisterClickEvent(Container2, UpdateColor);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void RichTextBox(Control Top, RichTextBox Object, Size ObjectSize, Point ObjectLocation, Color ObjectBCol, Color ObjectFCol, int FontTypeID, int FontSize, bool ReadOnly = false, bool MultiLine = false, bool ScrollBar = false, bool TabStop = false)
		{
		    try
		    {
			Tool.Resize(Object, ObjectSize);

			Object.Location = Tool.OGetCenter(Top, Object, ObjectLocation);
			Object.Font = Tool.GetFont(FontTypeID, FontSize);
			Object.BorderStyle = BorderStyle.None;
			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;

			if (ScrollBar)
			{
			    Object.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
			}

			Object.Multiline = MultiLine;
			Object.ReadOnly = ReadOnly;
			Object.TabStop = TabStop;

			Top.Controls.Add(Object);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public readonly Dictionary<TextBox, int> TextBoxContainers = new Dictionary<TextBox, int>();

		public void TextBox(Control Top, TextBox Object, Size ObjectSize, Point ObjectLocation, Color ObjectBCol, Color ObjectFCol, int FontTypeID, int FontSize, bool ReadOnly = false, bool Multiline = false, bool ScrollBar = false, bool FixedSize = true, bool TabStop = false)
		{
		    try
		    {
			Tool.Resize(Object, ObjectSize);

			Object.Location = Tool.OGetCenter(Top, Object, ObjectLocation);
			Object.BorderStyle = BorderStyle.None;
			Object.Multiline = Multiline;
			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;
			Object.ReadOnly = ReadOnly;
			Object.TabStop = TabStop;

			if (FixedSize)
			{
			    var TextBoxContainer = new PictureBox();

			    Tool.Resize(TextBoxContainer, ObjectSize);

			    TextBoxContainer.Font = Tool.GetFont(FontTypeID, FontSize);
			    TextBoxContainer.BorderStyle = BorderStyle.None;
			    TextBoxContainer.Location = ObjectLocation;
			    TextBoxContainer.BackColor = ObjectBCol;
			    TextBoxContainer.ForeColor = ObjectFCol;

			    Top.Controls.Add(TextBoxContainer);

			    TextBoxContainer.Click += (s, e) =>
			    {
				Object.Select();
			    };

			    var ResizedSize = new Size(ObjectSize.Width - 10, Tool.GetFontSize("http", FontSize).Height);
			    var RelocatedLocation = new Point(5, (ObjectSize.Height - ResizedSize.Height) / 2);

			    Object.Location = RelocatedLocation;

			    Tool.Resize(Object, ResizedSize);

			    TextBoxContainers.Add(Object, TextBoxContainers.Count + 1);
			    TextBoxContainer.Controls.Add(Object);
			}

			else
			{
			    Object.Location = ObjectLocation;
			    Object.Font = Tool.GetFont(FontTypeID, FontSize);

			    if (ScrollBar)
			    {
				Object.ScrollBars = ScrollBars.Vertical;
			    }

			    Top.Controls.Add(Object);
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void TreeView(Control Top, DashTreeView Object, Size ObjectSize, Point ObjectLoca, Color ObjectBCol, Color ObjectFCol)
		{
		    try
		    {
			Tool.Resize(Object, ObjectSize);

			Object.Location = Tool.OGetCenter(Top, Object, ObjectLoca);
			Object.BorderStyle = BorderStyle.None;
			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;

			Top.Controls.Add(Object);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void ListBox(Control Top, DashListBox Object, Size ObjectSize, Point ObjectLoca, Color ObjectBCol, Color ObjectFCol)
		{
		    try
		    {
			Tool.Resize(Object, ObjectSize);

			Object.Location = Tool.OGetCenter(Top, Object, ObjectLoca);
			Object.BorderStyle = BorderStyle.None;
			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;

			Top.Controls.Add(Object);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void Button(Control Top, Button Object, Size ObjectSize, Point ObjectLocation, Color ObjectBCol, Color ObjectFCol, int FontTypeID, int FontSize, string ButtonText, bool TabStop = false)
		{
		    try
		    {
			Tool.Resize(Object, ObjectSize);

			Object.Location = Tool.OGetCenter(Top, Object, ObjectLocation);
			Object.Font = Tool.GetFont(FontTypeID, FontSize);
			Object.FlatAppearance.BorderColor = ObjectBCol;
			Object.FlatAppearance.BorderSize = 0;
			Object.FlatStyle = FlatStyle.Flat;
			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;
			Object.TabStop = TabStop;
			Object.Text = ButtonText;

			Top.Controls.Add(Object);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void Label(Control Top, Label Object, Size ObjectSize, Point ObjectLocation, Color ObjectBCol, Color ObjectFCol, string LabelText, int FontTypeID = 1, int FontSize = 8, bool TabStop = false)
		{
		    try
		    {
			if (ObjectSize == Size.Empty)
			{
			    ObjectSize = Tool.GetFontSize(LabelText, FontSize);
			}

			Tool.Resize(Object, ObjectSize);

			Object.Location = Tool.OGetCenter(Top, Object, ObjectLocation);
			Object.Font = Tool.GetFont(FontTypeID, FontSize);
			Object.BorderStyle = BorderStyle.None;
			Object.FlatStyle = FlatStyle.Flat;
			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;
			Object.TabStop = TabStop;
			Object.Text = LabelText;

			Top.Controls.Add(Object);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void Image(Control Top, PictureBox Object, Size ObjectSize, Point ObjectLoca, Color BackColor, Image ObjectImage = null, bool TabStop = false)
		{
		    try
		    {
			if (ObjectSize == Size.Empty)
			{
			    if (ObjectImage == null)
			    {
				throw new Exception("No image specified.");
			    }

			    ObjectSize = ObjectImage.Size;
			}

			Tool.Resize(Object, ObjectSize);

			Object.Location = Tool.OGetCenter(Top, Object, ObjectLoca);
			Object.BorderStyle = BorderStyle.None;
			Object.BackColor = BackColor;
			Object.Image = ObjectImage;
			Object.TabStop = TabStop;

			Top.Controls.Add(Object);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void Panel(Control Top, DashPanel Object, Size ObjectSize, Point ObjectLoca, Color ObjectBCol, Control.ControlCollection ChildObjects = null, string ObjectId = "A panel!")
		{
		    try
		    {
			Tool.Resize(Object, ObjectSize);

			Object.Location = Tool.OGetCenter(Top, Object, ObjectLoca);
			Object.BorderStyle = BorderStyle.None;
			Object.BackColor = ObjectBCol;
			Object.Name = ObjectId;

			if (ChildObjects != null)
			{
			    foreach (Control control in ChildObjects)
			    {
				Object.AddChild(control);
			    }
			}

			Top.Controls.Add(Object);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}
	    }
	}


	namespace Tools
	{
	    public class DashResources
	    {
		public string GetCurrentNamespace()
		{
		    return Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace;
		}


		public string GetStringFrom(string fn)
		{
		    try
		    {
			string nsn = GetType().Namespace;

			using (Stream strm = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{nsn}.{fn}"))
			{
			    using (StreamReader rdr = new StreamReader(strm))
			    {
				return rdr.ReadToEnd();
			    }
			}
		    }

		    catch
		    {
			return string.Empty;
		    }
		}
	    }


	    public class DashLink
	    {
		public void CenterDialog(Control Dialog, Control Parent)
		{
		    try
		    {
			Point ParentLoca = Parent.PointToScreen(Point.Empty);

			int Y = ParentLoca.Y + ((Parent.Height - Dialog.Height) / 2);
			int X = ParentLoca.X + ((Parent.Width - Dialog.Width) / 2);

			Dialog.Location = new Point(X, Y);
		    }

		    catch (Exception E)
		    {
			ErrorHandler.JustDoIt(E);
		    }
		}

		// Auto Update Ideology:
		// - When parent GUI moves update added dialog
		// - Old Dialog Location on Screen
		// - Update it by changed value of Parent
		// - If changed value is higher than old +
		// - If changed value is lower than old -
	    }
	    

	    public class DashTools
	    {
		public bool ArrayContains(object obj, object[] arr)
		{
		    for (int k = 0; k < arr.Length; k += 1)
			if (arr[k] != obj && !arr[k].Equals(obj))
			    return false;
		    return true;
		}


		public bool IsAnyNull(params object[] targets)
		{
		    foreach (object target in targets)
			if (target == null)
			    return true;
		    return false;
		}


		public string RGBString(Color cc) 
		    => ($"{cc.R}, {cc.G}, {cc.B}");

		public Color NegativeRGB(int minus, Color origin)
		{
		    return 
		    (
			Color.FromArgb
			(
			    origin.R - minus,
			    origin.G - minus,
			    origin.B - minus
			)
		    );
		}

		public Color PositiveRGB(int plus, Color origin)
		{
		    return
		    (
			Color.FromArgb
			(
			    origin.R + plus,
			    origin.G + plus,
			    origin.B + plus
			)
		    );
		}


		public string GetCurrentDate() => DateTime.Now.ToLongDateString();
		public string GetCurrentTime() => DateTime.Now.ToLongTimeString();


		readonly DashResources Resource = new DashResources();

		public void SetTxtBoxContents(TextBox TxtBox, string such, bool isResource = false)
		{
		    try
		    {
			if (isResource)
			{
			    such = Resource.GetStringFrom($"{such}");
			}

			TxtBox.Text = ($"{such}");
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void AddBorderTo(Control Con, int Ptw, Color BCol)
		{
		    try
		    {
			Size Size = new Size(Con.Width - 1, Con.Height - 1);
			Point Loca = new Point(0, 0);

			PaintRectangle(Con, Ptw, Size, Loca, BCol);
		    }

		    catch (Exception E)
		    {
			ErrorHandler.JustDoIt(E);
		    }
		}


		public void MsgBox(string msg, string title = "Dash Notification",
		    MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
		{
		    try
		    {
			MessageBox.Show(msg, title, buttons, icon);
		    }

		    catch (Exception E)
		    {
			ErrorHandler.GetException(E);
		    }
		}


		public IEnumerable<Control> GetTypes(Control from, params Type[] types)
		{
		    List<Type> typeList = types.ToList();

		    foreach (Control con in from.Controls)
		    {
			if (typeList.Contains(con.GetType()))
			{
			    yield return con;
			}
		    }
		}
		

		public delegate void VoidRun();

		public void SortCode(string Tag, VoidRun runThis)
		{
		    try
		    {
			runThis();
		    }

		    catch (Exception E)
		    {
			ErrorHandler.JustDoIt(E);
		    }
		}


		public delegate bool BooleanRun();
	    
		public bool SortBooleanCode(string Tag, BooleanRun runThis)
		{
		    try
		    {
			return runThis();
		    }

		    catch
		    {
			return false;
		    }
		}


		public Size SubstractSize(int Amount, Size Size)
		{
		    return new Size(Size.Width - Amount, Size.Height - Amount);
		}


		public void StartProcess(string Path, bool UseShell = true, bool NoAppear = false)
		{
		    try
		    {
			using (Process proc = new Process())
			{
			    proc.StartInfo = new ProcessStartInfo()
			    {
				UseShellExecute = UseShell,
				CreateNoWindow = !NoAppear,
				FileName = Path,
			    };

			    proc.Start();
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void OpenUrl(string Destination)
		{
		    try
		    {
			using (var Process = new Process())
			{
			    Process.StartInfo = new ProcessStartInfo()
			    {
				FileName = Destination,
				UseShellExecute = true,
			    };

			    Process.Start();
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void SetUrl(Control Object, string Destination)
		{
		    try
		    {
			Object.Click += (s, e) =>
			{
			    try
			    {
				OpenUrl(Destination);
			    }

			    catch (Exception E)
			    {
				throw (ErrorHandler.GetException(E));
			    }
			};
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void SetDock(Control For, DockStyle DockStyle)
		{
		    try
		    {
			For.Dock = DockStyle;
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public delegate void Holder();

		public void RegisterClickEvent(Control For, Holder This)
		{
		    try
		    {
			For.Click += (s, e) =>
			{
			    try
			    {
				This();
			    }

			    catch (Exception E)
			    {
				throw (ErrorHandler.GetException(E));
			    }
			};
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public Point GetCenterFor(Control This, Control BasedOn, bool FromLeft = true, bool FromTop = true, int X = -1, int Y = -1)
		{
		    try
		    {
			int x = FromLeft ? (BasedOn.Width - This.Width) / 2 : (X != -1 ? X : 0);
			int y = FromTop ? (BasedOn.Height - This.Height) / 2 : (Y != -1 ? Y : 0);

			return new Point(x, y);
		    }

		    catch
		    {
			return Point.Empty;
		    }
		}
		
		public Point OGetCenter(Control BasedOn, Control This, Point Coords)
		{
		    try
		    {
			int x = (Coords.X < 0 ? (BasedOn.Width - This.Width) / 2 : Coords.X);
			int y = (Coords.Y < 0 ? (BasedOn.Height - This.Height) / 2 : Coords.Y);

			return new Point(x, y);
		    }

		    catch
		    {
			return Point.Empty;
		    }
		}


		public void AlignContainerTextBoxes(Control container, HorizontalAlignment alignment)
		{
		    try
		    {
			foreach (Control a1 in container.Controls)
			{
			    foreach (Control a2 in a1.Controls)
			    {
				if (a2 is TextBox)
				{
				    ((TextBox)a2).TextAlign = alignment;
				}
			    }
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public void RoundContainerControls(Control container)
		{
		    try
		    {
			foreach (Control a1 in container.Controls)
			{
			    foreach (Control a2 in a1.Controls)
			    {
				Round(a2, 6);
			    }

			    Round(a1, 6);
			}
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public Size GetFontSize(string Text, int Size = 10, int Id = 1)
		{
		    return TextRenderer.MeasureText(Text, GetFont(Id, Size));
		}


		public void Resize(Control Object, Size Size)
		{
		    Object.MaximumSize = Size;
		    Object.MinimumSize = Size;
		}


		public void PaintRectangle(Control Object, int Thickness, Size Size, Point Location, Color Color)
		{
		    Object.Paint += (s, e) =>
		    {
			var graphics = e.Graphics;

			graphics.SmoothingMode = SmoothingMode.HighQuality;

			using (Pen pen = new Pen(Color, Thickness))
			{
			    graphics.DrawRectangle(pen, new Rectangle(Location, Size));
			};
		    };
		}


		public void PaintLine(Control Object, Color Color, int Thickness, Point Location1, Point Location2)
		{
		    Object.Paint += (s, e) =>
		    {
			var graphics = e.Graphics;

			graphics.SmoothingMode = SmoothingMode.HighQuality;

			using (Pen pen = new Pen(Color, Thickness))
			{
			    graphics.DrawLine(pen, Location1, Location2);
			};
		    };
		}


		public void PaintCircle(Control Object, Color Color, int Thickness, Point Location, Size Size)
		{
		    Object.Paint += (s, e) =>
		    {
			var graphics = e.Graphics;

			graphics.SmoothingMode = SmoothingMode.HighQuality;

			using (Pen pen = new Pen(Color, Thickness))
			{
			    graphics.DrawEllipse(pen, new RectangleF(Location, Size));
			};
		    };
		}


		[DllImport("User32.dll")] static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);
		public void UseResourceCursor(Control Object, byte[] BYTES)
		{
		    var curse = new Cursor(CreateIconFromResource(BYTES, (uint)BYTES.Length, false, 0x00030000));

		    Object.Cursor = curse;
		    Object.Update();
		}


		public void Interactive(Control Object, Control Target)
		{
		    var Location = Point.Empty;

		    Object.MouseMove += (s, e) =>
		    {
			if (Location.IsEmpty)
			{
			    return;
			}

			Target.Location = new Point(Target.Location.X + (e.X - Location.X),
			    Target.Location.Y + (e.Y - Location.Y));
		    };

		    Object.MouseDown += (s, e) =>
		    {
			Location = new Point(e.X, e.Y);
		    };

		    Object.MouseUp += (s, e) =>
		    {
			Location = Point.Empty;
		    };
		}


		public class ReadOnlyForm : Form
		{
		    public void PaintOwner(PaintEventArgs e)
		    {
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			base.OnPaint(e);
		    }
		}


		public readonly ReadOnlyForm ReadForm = new ReadOnlyForm();

		public void Round(Control Object, int Radius)
		{
		    try
		    {
			Object.Paint += (s, e) =>
			{
			    try
			    {
				ReadForm.PaintOwner(e);

				GraphicsPath GraphicsPath = new GraphicsPath();

				var Rectangle = new Rectangle(0, 0, Object.Width, Object.Height);

				int R = Radius * 3;

				int H = Rectangle.Height;
				int W = Rectangle.Width;

				int X = Rectangle.X;
				int Y = Rectangle.X;

				GraphicsPath.AddArc(X, Y, R, R, 170, 90);
				GraphicsPath.AddArc(X + W - R, Y, R, R, 270, 90);
				GraphicsPath.AddArc(X + W - R, Y + H - R, R, R, 0, 90);
				GraphicsPath.AddArc(X, Y + H - R, R, R, 80, 90);

				Object.Region = new Region(GraphicsPath);
			    }

			    catch (Exception E)
			    {
				throw (ErrorHandler.GetException(E));
			    }
			};
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		[DllImport("gdi32.dll")] private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
		readonly PrivateFontCollection FontCollection = new PrivateFontCollection();

		public Font GetFont(int FontId, int Height)
		{
		    try
		    {
			if (FontCollection.Families.Length < 2)
			{
			    var RawDataCollection = new List<byte[]>()
			    {
				Resources.primary,
				Resources.secondary,
			    };

			    for (int k = 0; k < RawDataCollection.Count; k += 1)
			    {
				byte[] RawData = RawDataCollection[k];

				var Pointer = Marshal.AllocCoTaskMem(RawData.Length);

				Marshal.Copy(RawData, 0, Pointer, RawData.Length);

				uint Reference = 0;

				AddFontMemResourceEx(Pointer, (uint)RawData.Length, IntPtr.Zero, ref Reference);
				FontCollection.AddMemoryFont(Pointer, RawData.Length);
			    };
			};

			return new Font(FontCollection.Families[FontId], Height);
		    }

		    catch
		    {
			return new Font("Modern", Height, FontStyle.Regular);
		    };
		}
	    }
	}
    }


    namespace Erroring
    {
	public class MessageContainer
	{
	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();
	    
	    public Control ContainerParent = null;

	    public Color BackColor = Color.DarkRed;
	    public Color ForeColor = Color.White;

	    public int ContentConfigured = -1;
	    public int ContainerHeight = 50;
	    public int ContainerWidth = -1;
	    public int ContentFontSize = 14;
	    public int ContentFontId = 1;

	    public void ChangeSettings(Control ContainerParent, int ContainerWidth, int ContainerHeight, int ContentFontSize, int ContentFontId, Color ForeColor, Color BackColor)
    	    {
		this.ContainerParent = ContainerParent;
		this.ContentFontSize = ContentFontSize;
		this.ContainerHeight = ContainerHeight;
		this.ContainerWidth = ContainerWidth;
		this.ContentFontId = ContentFontId;
		this.ForeColor = ForeColor;
		this.BackColor = BackColor;
	    }

	    public void SetColor(Color BackColor, Color ForeColor)
	    {
		Container.BackColor = BackColor;
		Content.BackColor = BackColor;
		Content.ForeColor = ForeColor;

		this.BackColor = BackColor;
		this.ForeColor = ForeColor;
	    }


	    readonly Runnable Runnable = new Runnable();
	    delegate void Synchronize();

	    void Sync(Synchronize sync) => Runnable.
		RunTaskSynchronously(ContainerParent, () => sync());


	    readonly DashPanel Container = new DashPanel();
	    readonly Label Content = new Label();

	    public bool Initialized() => (ContentConfigured != -1);

	    public void Show(string Message, Point ContainerLoca, int VisibilityTimeout = -1)
	    {
		try
		{
		    Tools.SortCode(("Validation Process"), () =>
		    {
			if (Initialized() && Container.Visible)
			{
			    return;
			}

			else if (ContainerParent == null)
			    return;
		    });
		    
		    Tools.SortCode(("Visibility Handler"), () =>
		    {
			Point GetCenter() => new Point((Container.Width 
			    - GetContentSize().Width) / 2, (Container.Height - GetContentSize().Height) / 2);

			Size GetContentSize() => Tools.GetFontSize(Message,
			    ContentFontSize, ContentFontId);

			ContainerWidth = ContainerWidth == -1 ? ContainerParent.Width : ContainerWidth;
			ContainerLoca = ContainerLoca.Equals(Point.Empty) ? GetCenter() : ContainerLoca;
			
			if (!Initialized())
			{
			    Tools.SortCode(("Container Initialization"), () =>
			    {
				Controls.Panel(ContainerParent, Container, new Size
				    (ContainerWidth, ContainerHeight), ContainerLoca, BackColor);

				Controls.Label(Container, Content, GetContentSize(), new Point(-2, -2),
				    BackColor, ForeColor, (Message), ContentFontId, ContentFontSize);

				ContentConfigured += 1;
			    });
			}

			Container.Location = ContainerLoca;

			if (!Content.Text.Equals(Message))
			{
			    Content.Size = GetContentSize();
			    Content.Location = GetCenter();
			    Content.Text = Message;
			}

			SetColor(BackColor, ForeColor);

			Container.BringToFront();
			Container.Show();
		    });

		    Tools.SortCode(("Visibility Timeout"), () =>
		    {
			if (VisibilityTimeout > 50)
			{
			    Runnable.RunTaskLater(null, () => 
			    {
				new Thread(() =>
				{
				    Color GetBackAlpha(int Alpha, Control Control) => Color.FromArgb(Alpha,
					Control.BackColor.R, Control.BackColor.G, Control.BackColor.B);

				    Color GetForeAlpha(int Alpha, Control Control) => Color.FromArgb(Alpha,
					Control.ForeColor.R, Control.ForeColor.G, Control.ForeColor.B);

				    void UpdateColors(Color A, Color B, Color C)
				    {
					Sync(() => 
					{ 
					    Container.BackColor = A;
					    Content.ForeColor = B;
					    Content.BackColor = C;
					});
				    }

				    for (int k = 255; k > 40; k -= 15)
				    {
					UpdateColors
					(
					    GetBackAlpha(k, Container),
					    GetForeAlpha(k, Content),
					    GetBackAlpha(k, Content)
					);

					Thread.Sleep(10);
				    }

				    Sync(() => Container.Hide());

				    UpdateColors(BackColor, ForeColor, BackColor);
				})

				{ IsBackground = true }.Start();
			    }, 
			    
			    VisibilityTimeout);
			}
		    });
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	public class ErrorHandler
	{
	    public static void JustDoIt(Exception E, string title = ("Error Handler")) => Utilize(GetRawFormat(E), title);
	    public static Exception GetException(Exception E) => new Exception(GetRawFormat(E));


	    public static string ErrorFormat = string.Format
	    (
		"----------------------\r\n" +
		"[A]\r\n" +
		"----------------------\r\n" +
		"[B]\r\n" +
		"----------------------\r\n" +
		"[C]\r\n"
	    );

	    public static string GetRawFormat(Exception E)
	    {
		return ErrorFormat.Replace("[A]", $"{E.StackTrace}")
		    .Replace("[B]", $"{E.Message}")
		    .Replace("[C]", $"{E.Source}");
	    }


	    public static void Utilize(string description, string title)
	    {
		DashDialog ErrorDialog = new DashDialog();

		Color ContainerBCol = Color.FromArgb(9, 39, 66);
		Color MenuBarBCol = Color.FromArgb(19, 36, 64);
		Color AppBCol = Color.FromArgb(6, 17, 33);

		ErrorDialog.Show(AppBCol, Color.White, Size.Empty, description, title, DashDialog.Buttons.OK);

		Environment.Exit(-1);
	    }
	}
    }


    namespace DashLogic
    {
	public class EfficiencyTools
	{
	    public Size Resize(Size Original, int X, int Y, bool Add = true)
	    {
		Original.Height += Y;
		Original.Width += X;

		if (!Add)
		{
		    Original.Height -= (2 * Y);
		    Original.Width -= (2 * X);
		}

		return (Original);
	    }
	}
    }


    namespace Forms
    {
	public class DashForm : Form
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();


	    private readonly DashMenuBar MenuBar = new DashMenuBar("Dash App", false, true);

	    public void AddMenubar()
	    {
		try
		{
		    Color BarBCol = Color.FromArgb(8, 8, 8);
		    MenuBar.AddMe(this, BarBCol, BarBCol);
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }


#pragma warning disable CS0109

	    public FormStartPosition FSPos = FormStartPosition.CenterScreen;
	    public FormBorderStyle FBSty = FormBorderStyle.None;

	    public string AppTitle = string.Format("Dash App");
	    public bool AsDialog = true;

	    public Color BackCol = Color.MidnightBlue;
	    public Size AppSize = new Size(350, 350);

	    public new void Show(bool MenuBar = true)
	    {
		try
		{
		    if (!Size.Equals(AppSize))
		    {
			BackColor = BackCol;
			Text = AppTitle;

			Icon AppIco = Resources.ICON;

			Icon = AppIco;

			FormBorderStyle = FBSty;
			StartPosition = FSPos;

			Tool.Interactive(this, this);
			Tool.Resize(this, AppSize);

			if (MenuBar)
			    AddMenubar();

			if (AsDialog)
			    ShowDialog();
			else
			    Show();
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

#pragma warning restore CS0109
	}
    }


    namespace Dialog
    {
	public class DashDialog
	{
	    private readonly EfficiencyTools Efficiency = new EfficiencyTools();
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();


	    public DashWindow Dialog = new DashWindow();

	    public void InitS1(Size DialogSize, string Title, Color DialogBCol)
	    {
		try
		{
		    if (Dialog.Controls.Count < 1)
		    {
			Dialog = new DashWindow();
		    }

		    Dialog.InitializeWindow(DialogSize, Title, DialogBCol, Color.Empty,
			appMenuBar: false, startPosition: FormStartPosition.CenterParent);

		    Tool.Interactive(Dialog, Dialog);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public readonly DashPanel S2Container1 = new DashPanel();
	    public readonly DashPanel S2Container2 = new DashPanel();
	    public readonly TextBox S2TextBox1 = new TextBox();
	    public readonly Button S2Button1 = new Button();
	    public readonly Button S2Button2 = new Button();
	    public readonly Label S2Label1 = new Label();

	    private int S2ButtonID = 0;

	    public void InitS2(Color DialogFCol, string Description, string Title, Buttons DialogButtons)
	    {
		try
		{
		    var LabelLoca = new Point(-2, 12);

		    Control.Label(Dialog, S2Label1, Size.Empty, LabelLoca, Dialog.BackColor, DialogFCol, Title, 1, 10);

		    var Container1Size = new Size(Dialog.Width - 20, Dialog.Height - (S2Label1.Height + 68));
		    var Container1Loca = new Point(10, S2Label1.Height + S2Label1.Top + 10);
		    var Container1BCol = Dialog.BackColor;

		    Control.Panel(Dialog, S2Container1, Container1Size, Container1Loca, Container1BCol);
		    Tool.Round(S2Container1, 6);

		    var TextBoxSize = Efficiency.Resize(Container1Size, 8, 8, false);
		    var TextBoxLoca = new Point(4, 4);

		    Control.TextBox(S2Container1, S2TextBox1, TextBoxSize, TextBoxLoca, Container1BCol, DialogFCol, 1, 8, true, true, true, false);
		    S2TextBox1.Text = Description;

		    var Container2Loca = new Point(Container1Loca.X, Container1Size.Height + Container1Loca.Y + 10);
		    var Container2Size = new Size(Container1Size.Width, 24);

		    Control.Panel(Dialog, S2Container2, Container2Size, Container2Loca, Container1BCol);

		    string[] Texts = new string[] { "Okay", "" };

		    void Action1()
		    {
			Texts[1] = ("Cancel");
		    }

		    void Action2()
		    {
			Texts[0] = ("Yes");
			Texts[1] = ("No");
		    }

		    switch (DialogButtons)
		    {
			case Buttons.OKCancel: Action1(); break;
			case Buttons.YesNo: Action2(); break;
		    }

		    var ButtonObjects = new List<Button>() { S2Button1 };

		    if (Texts[1].Length > 0)
		    {
			ButtonObjects.Add(S2Button2);
		    }

		    var ButtonSize = new Size((Container2Size.Width / 2) - 30, 24);
		    var ButtonBCol = Color.MidnightBlue;

		    Point ButtonStartPoint()
		    {
			if (ButtonObjects.Count <= 1)
			{
			    return new Point((Container2Size.Width - ButtonSize.Width) / 2);
			}

			return new Point(10);
		    };

		    var ButtonLoca = ButtonStartPoint();

		    for (int k = 0; k < ButtonObjects.Count; k += 1)
		    {
			if (k >= 1)
			{
			    ButtonLoca.X = (Container2Size.Width - ButtonSize.Width - 10);
			}

			Control.Button(S2Container2, ButtonObjects[k], ButtonSize, ButtonLoca,
			    ButtonBCol, DialogFCol, 1, 9, Texts[k]);
		    }

		    S2Button1.Click += (s, e) =>
		    {
			S2ButtonID = 0;
			Dialog.Hide();
		    };

		    S2Button2.Click += (s, e) =>
		    {
			S2ButtonID = 1;
			Dialog.Hide();
		    };
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public enum Buttons { OKCancel, YesNo, OK };

	    public int Show(Color DialogBCol, Color DialogFCol, Size DialogSize, string Description, string Title, Buttons DialogButtons = Buttons.OK)
	    {
		try
		{
		    if (DialogSize == Size.Empty)
		    {
			DialogSize = new Size(350, 350);
		    }

		    InitS1(DialogSize, Title, DialogBCol);
		    InitS2(DialogFCol, Description, Title, DialogButtons);

		    Dialog.ShowAsIs();

		    return S2ButtonID;
		}

		catch (Exception E)
		{
		    MessageBox.Show(ErrorHandler.GetRawFormat(E));
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public int Show()
	    {
		try
		{
		    Dialog.ShowAsIs();
		    return S2ButtonID;
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	public class DashMenuBar
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();


	    public class Values
	    {
		public readonly PictureBox LogoLayer1 = new PictureBox();
		public readonly PictureBox LogoLayer2 = new PictureBox();
		public readonly DashPanel Bar = new DashPanel();
		public readonly Button Button1 = new Button();
		public readonly Button Button2 = new Button();
		public readonly Label Title = new Label();

		public bool Minimize = false;
		public bool Close = false;
		public bool Hide = false;


		public void setLogoBackColor(Color to)
		{
		    LogoLayer1.BackColor = Bar.BackColor;
		    LogoLayer2.BackColor = to;
		}


		public Control.ControlCollection getControls() => Bar.Controls;
		public Control getParent() => Bar.Parent;

		public Color getBarColor() => Bar.BackColor;

		public void setLocationOf(Control me, Point to) => me.Location = to;
		public void setColorOf(Control me, Color to) => me.BackColor = to;
		public void setBarBackColor(Color to) => Bar.BackColor = to;
		public void setTitle(string to) => Title.Text = to;

		public int parentHeight() => Bar.Parent.Height;
		public int parentWidth() => Bar.Parent.Width;
		public int Height() => Bar.Height;
		public int Width() => Bar.Width;
	    }


	    public readonly Values values = new Values();

	    public DashMenuBar(string title, bool minimizeButton, bool closeButton, bool hideDialog = true)
	    {
		try
		{
		    values.Minimize = minimizeButton;
		    values.Close = closeButton;
		    values.Hide = hideDialog;

		    values.setTitle(title);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public enum Heights { Light = 24, Medium = 26, Heavy = 28, Fat = 30 };

	    public void AddMe(Control parent, Color barBCol, Color borderBCol, int barHeight = 26)
	    {
		try
		{
		    var MenuBarSize = new Size(parent.Width, barHeight);
		    var MenuBarLoca = new Point(0, 0);
		    var MenuBarBCol = barBCol;

		    Control.Panel(parent, values.Bar, MenuBarSize, MenuBarLoca, MenuBarBCol);
		    Tool.Interactive(values.Bar, parent);

		    var LogoSize = Resources.LOGO.Size;
		    var LogoLoca = new Point(5, 2);

		    Control.Image(parent, values.LogoLayer2, LogoSize, LogoLoca, parent.BackColor, ObjectImage: Resources.LOGO);
		    Control.Image(values.Bar, values.LogoLayer1, LogoSize, LogoLoca, barBCol, ObjectImage: Resources.LOGO);

		    Tool.Interactive(values.LogoLayer1, parent);
		    Tool.Interactive(values.LogoLayer2, parent);

		    var TitleSize = Tool.GetFontSize(values.Title.Text, 9);
		    var TitleLoca = new Point(LogoSize.Width + LogoLoca.X + 5, (MenuBarSize.Height - TitleSize.Height) / 2);

		    Control.Label(values.Bar, values.Title, TitleSize, TitleLoca, barBCol, Color.White, values.Title.Text, 1, 9);
		    Tool.Interactive(values.Title, parent);

		    var ButtonSize = new Size(65, barHeight);
		    var ButtonLoca = new Point(MenuBarSize.Width - ButtonSize.Width, 0);

		    if (values.Close)
		    {
			Control.Button(values.Bar, values.Button1, ButtonSize, ButtonLoca, barBCol, Color.White, 1, 10, "X");
			Tool.Interactive(values.Button1, parent);

			values.Button1.Click += (s, e) =>
			{
			    if (!values.Hide)
			    {
				Application.Exit();
				Environment.Exit(0);
			    }

			    else
			    {
				parent.Hide();
			    }
			};
		    }

		    if (values.Close && values.Minimize)
		    {
			ButtonLoca.X -= ButtonSize.Width;
		    }

		    else if (values.Minimize)
		    {
			Control.Button(values.Bar, values.Button2, ButtonSize, ButtonLoca, barBCol, Color.White, 1, 10, "-");
			Tool.Interactive(values.Button2, parent);

			values.Button2.Click += (s, e) =>
			{
			    parent.SendToBack();
			};
		    }

		    values.Button1.TextAlign = ContentAlignment.BottomCenter;

		    var RectangleSize = new Size(values.Width() - 4, values.parentHeight() - values.Height());
		    var RectangleLocation = new Point(2, values.Height() + values.Bar.Top - 2);

		    Tool.PaintRectangle(parent, 3, RectangleSize, RectangleLocation, borderBCol);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void UpdateTitle(string newValue, int fontSize = 8)
	    {
		try
		{
		    var newSize = Tool.GetFontSize(newValue, fontSize);
		    var newLabelLoca = new Point(values.Title.Left, (values.Height() - newSize.Height) / 2);

		    values.setLocationOf(values.Title, newLabelLoca);
		    values.setTitle($"{newValue}");

		    Tool.Resize(values.Title, newSize);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void UpdateColor(Color newValue1, Color newValue2)
	    {
		try
		{
		    foreach (Control Control in values.getControls())
		    {
			values.setColorOf(Control, newValue1);
		    }

		    values.setLogoBackColor(values.getBarColor());
		    values.setBarBackColor(newValue1);

		    var RectSize = new Size(values.Width() - 4, values.parentHeight() - values.Height());
		    var RectLoca = new Point(2, values.Height() + values.Bar.Top - 3);

		    Tool.PaintRectangle(values.getParent(), 3, RectSize, RectLoca, newValue2);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	public class DashWindow : Form
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();


	    public class Values
	    {
		public DashMenuBar MenuBar = null;

		public Control.ControlCollection getControls() => MenuBar.values.getControls();
		public Control getParent() => MenuBar.values.getParent();
		public DashPanel getBar() => MenuBar.values.Bar;
		public Label getTitle() => MenuBar.values.Title;

		public Color getBarColor() => MenuBar.values.getBarColor();

		public void setLocationOf(Control me, Point to) => me.Location = to;
		public void setColorOf(Control me, Color to) => me.BackColor = to;
		public void setBarBackColor(Color to) => MenuBar.values.setBarBackColor(to);
		public void setTitle(string to) => MenuBar.values.setTitle(to);


		public void SetTitleLocation(Point to)
		{
		    try
		    {
			if (to.Y == -2)
			{
			    to.Y = (MenuBar.values.Bar.Height - MenuBar.values.Title.Height) / 2;
			}

			if (to.X == -2)
			{
			    to.X = (MenuBar.values.Bar.Width - MenuBar.values.Title.Width) / 2;
			}

			MenuBar.values.Title.Location = to;
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}


		public int parentHeight() => MenuBar.values.parentHeight();
		public int parentWidth() => MenuBar.values.parentWidth();
		public int Height() => MenuBar.values.Height();
		public int Width() => MenuBar.values.Width();


		public delegate void holder();

		public void onControlClick(int id, holder action)
		{
		    Control me = MenuBar.values.Button1;

		    if (id == 2) me = MenuBar.values.Button2;

		    me.Click += (s, e) =>
		    {
			action.Invoke();
		    };
		}


		public void HideIcons()
		{
		    MenuBar.values.LogoLayer1.Hide();
		    MenuBar.values.LogoLayer2.Hide();
		}


		public void HideTitle()
		{
		    MenuBar.values.Title.Hide();
		}


		readonly DashTools Tools = new DashTools();

		public void CenterTitle() => MenuBar.values.Title.Location 
		    = Tools.GetCenterFor(MenuBar.values.Title, MenuBar.values.Bar);

		public void ResizeTitle(int FontSize, int Id = 1, bool CenterTitle = true)
		{
		    try
		    {
			MenuBar.values.Title.Font = new Font(MenuBar.values.Title
			    .Font.FontFamily, FontSize);

			Tools.Resize(MenuBar.values.Title, Tools.GetFontSize(MenuBar
			    .values.Title.Text, FontSize, Id));

			if (CenterTitle) this.CenterTitle();
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}
	    }

	    public Values values = new Values();


	    private void InitS1(Size appSize, string appTitle, Color appBCol,
		FormStartPosition startPosition = FormStartPosition.CenterScreen, FormBorderStyle borderStyle = FormBorderStyle.None, int roundRadius = -1)
	    {
		try
		{
		    SuspendLayout();

		    MaximumSize = appSize;
		    MinimumSize = appSize;

		    FormBorderStyle = borderStyle;
		    StartPosition = startPosition;

		    BackColor = appBCol;
		    Icon = Resources.ICON;

		    Text = appTitle;
		    Name = appTitle;

		    if (roundRadius > 0)
		    {
			Tool.Round(this, roundRadius);
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    private void InitS2(string appTitle, bool appMinim, bool appClose, bool appHide, Color barBCol)
	    {
		try
		{
		    values.MenuBar = new DashMenuBar(appTitle, appMinim, appClose, appHide);
		    values.MenuBar.AddMe(this, barBCol, barBCol);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void InitializeWindow(Size appSize, string appTitle, Color appBCol, Color barBCol,
		FormStartPosition startPosition = FormStartPosition.CenterScreen, FormBorderStyle borderStyle = FormBorderStyle.None,
		bool barMinim = false, bool barClose = true, bool hideApp = true, bool appMenuBar = true, int roundRadius = 8)
	    {
		try
		{
		    InitS1(appSize, appTitle, appBCol, startPosition, borderStyle, roundRadius);

		    if (appMenuBar)
		    {
			InitS2(appTitle, barMinim, barClose, hideApp, barBCol);
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    private bool DoInitialize = true;

	    public void ShowWindow(Size appSize, string appTitle, Color appBCol, Color barBCol,
		FormStartPosition startPosition = FormStartPosition.CenterScreen, FormBorderStyle borderStyle = FormBorderStyle.None,
		bool showDialog = true, bool barMinim = false, bool barClose = true, bool closeHideApp = true, bool appMenuBar = true)
	    {
		try
		{
		    if (DoInitialize)
		    {
			InitializeWindow(appSize, appTitle, appBCol, barBCol, startPosition, borderStyle, barMinim, barClose, closeHideApp, appMenuBar);
			DoInitialize = false;
		    }

		    ShowAsIs(showDialog);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void ShowAsIs(bool showDialog = true)
	    {
		try
		{
		    if (showDialog)
		    {
			ShowDialog();
		    }

		    else
		    {
			Show();
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	public class LabelPage
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();


	    public readonly PictureBox S1Container1 = new PictureBox();

	    private void Init1(PictureBox Capsule, Size ContainerSize, Point ContainerLoca)
	    {
		try
		{
		    Control.Image(Capsule, S1Container1, ContainerSize, ContainerLoca, Color.MidnightBlue);
		    Tool.Round(S1Container1, 6);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    private readonly PictureBox S2Container1 = new PictureBox();

	    private readonly Label S2Label1 = new Label();
	    private readonly Label S2Label2 = new Label();

	    private int S2PageID = 1;
	    private int S2Pages = 0;

	    private void SetPageCount(string PageData, Size ConSize)
	    {
		try
		{
		    S2Pages = (TextRenderer.MeasureText
		    (
			PageData, Tool.GetFont(1, 9), new Size(ConSize.Width, 800),
			flags: TextFormatFlags.WordBreak
		    )

		    .Height / ConSize.Height) + 1;
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    private void Init2(string PageData, Size ConSize)
	    {
		try
		{
		    var ContainerSize = new Size(S1Container1.Width, 24);
		    var ContainerLoca = new Point(0, 0);

		    Control.Image(S1Container1, S2Container1, ContainerSize, ContainerLoca, Color.MidnightBlue);

		    SetPageCount(PageData, ConSize);

		    var Label1Size = Tool.GetFontSize("Page:", 9);
		    var Label2Size = Tool.GetFontSize($"1/{S2Pages} ", 9);

		    var Label1Loca = new Point((S2Container1.Width - (Label1Size.Width + Label2Size.Width)) / 2, (S2Container1.Height - Label1Size.Height) / 2);
		    var Label2Loca = new Point(Label1Loca.X + Label1Size.Width, Label1Loca.Y);

		    Control.Label(S2Container1, S2Label2, Label2Size, Label2Loca, S2Container1.BackColor, Color.White, ($"1/{S2Pages}"), 1, 9);
		    Control.Label(S2Container1, S2Label1, Label1Size, Label1Loca, S2Container1.BackColor, Color.White, ("Page:"), 1, 9);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    private readonly PictureBox S3Container1 = new PictureBox();
	    private readonly PictureBox S3Container2 = new PictureBox();

	    private readonly Button S3Button1 = new Button();
	    private readonly Button S3Button2 = new Button();

	    private void S3ChangePage(bool Forward)
	    {
		try
		{
		    if (Forward)
		    {
			if (S2PageID >= S2Pages)
			{
			    return;
			}

			S4Label1.Top -= S4Container1.Height;
			S2PageID += 1;
		    }

		    else
		    {
			if (S2PageID < 2)
			{
			    return;
			}

			S4Label1.Top += S4Container1.Height;
			S2PageID -= 1;
		    }

		    S2Label2.Text = $"{S2PageID}" + S2Label2.Text.Substring(1, 2);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    private readonly Label S3Label1 = new Label();

	    private void Init3(string Title)
	    {
		try
		{
		    var Container1Size = new Size(S2Container1.Width, 30);
		    var Container1Loca = new Point(0, S1Container1.Height - 30);

		    var Container2Size = new Size(95, 26);
		    var Container2Loca = new Point(Container1Size.Width - 105, 1);

		    var ContainerBCol = S2Container1.BackColor;

		    Control.Image(S1Container1, S3Container1, Container1Size, Container1Loca, ContainerBCol);
		    Control.Image(S3Container1, S3Container2, Container2Size, Container2Loca, ContainerBCol);

		    var LabelText = ($"{Title}");
		    var LabelSize = Tool.GetFontSize(LabelText, 9);
		    var LabelLoca = new Point(10, (S3Container1.Height - LabelSize.Height) / 2);

		    Control.Label(S3Container1, S3Label1, LabelSize, LabelLoca, ContainerBCol, Color.White, LabelText, 1, 9);

		    var Button2Loca = new Point(50, 0);
		    var Button1Loca = new Point(0, 0);

		    var ButtonSize = new Size(45, 26);

		    Control.Button(S3Container2, S3Button1, ButtonSize, Button1Loca, ContainerBCol, Color.White, 1, 9, ("<"));
		    Control.Button(S3Container2, S3Button2, ButtonSize, Button2Loca, ContainerBCol, Color.White, 1, 9, (">"));

		    S3Button1.Click += (s, e) => S3ChangePage(false);
		    S3Button2.Click += (s, e) => S3ChangePage(true);

		    Tool.Round(S3Button1, 6);
		    Tool.Round(S3Button2, 6);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    private readonly PictureBox S4Container1 = new PictureBox();
	    private readonly Label S4Label1 = new Label();

	    private void Init4(string Message, Color LabelBCol, Color LabelFCol)
	    {
		try
		{
		    var ContainerSize = new Size(S1Container1.Width - 5, S1Container1.Height - S3Container1.Height - S2Container1.Height - 4);
		    var ContainerLoca = new Point(3, S2Container1.Height + 2);
		    var ContainerBCol = LabelBCol;

		    Control.Image(S1Container1, S4Container1, ContainerSize, ContainerLoca, ContainerBCol);

		    var LabelText = Message;
		    var LabelFSiz = TextRenderer.MeasureText(LabelText, Tool.GetFont(1, 9), Size.Empty, flags: TextFormatFlags.WordBreak);
		    var LabelSize = new Size(ContainerSize.Width - 4, LabelFSiz.Height - 4);
		    var LabelLoca = new Point(2, 2);

		    Control.Label(S4Container1, S4Label1, LabelSize, LabelLoca, LabelBCol, LabelFCol, LabelText, 1, 9);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void SetupPages(PictureBox Capsule, Tuple<string, Size, Point> ContainerSetup, Tuple<Color, Color, string> LabelSetup)
	    {
		try
		{
		    Init1(Capsule, ContainerSetup.Item2, ContainerSetup.Item3);
		    Init2(LabelSetup.Item3, ContainerSetup.Item2);
		    Init3(ContainerSetup.Item1);
		    Init4(LabelSetup.Item3, LabelSetup.Item1, LabelSetup.Item2);
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }
	}


	public class ClickDropMenu
	{
	    public class DropItem { public readonly Label Item = new Label(); } /*class for future multi-use*/

	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();


	    public Color ItemBackColor = Color.FromArgb(5, 23, 31);
	    public Color ItemForeColor = Color.White;

	    public bool ItemCenterText = true;

	    public int ItemFontSize = 12;
	    public int ItemHeight = 30;
	    public int ItemWidth = 100;
	    public int ItemFontId = 1;

	    public int GetItemTop(int Id = -19)
	    {
		try
		{
		    if (Id == -19)
		    {
			if (ItemStack.Count < 1)
			{
			    return 0;
			}

			Id = ItemStack.Count - 1;
		    }

		    else
		    {
			if (ItemStack.Count <= Id)
			{
			    return 0;
			}
		    }

		    Label item = ItemStack[Id].Item;

		    return (item.Height + item.Top);
		}

		catch
		{
		    return -1;
		}
	    }

	    public void InsertItem(DropItem item, string name)
	    {
		Tools.SortCode(("Insert item into Container"), () =>
		{
		    try
		    {
			Size ItemSize = new Size(ItemWidth, ItemHeight);
			Point ItemLoca = new Point(0, GetItemTop());

			Controls.Label(LowerContainer, item.Item, ItemSize, ItemLoca,
			    ItemBackColor, ItemForeColor, (name), ItemFontId, ItemFontSize);

			if (ItemCenterText)
			{
			    item.Item.TextAlign = ContentAlignment.MiddleCenter;
			}

			ItemStack.Add(item);
		    }

		    catch
		    {
			return;
		    }
		});

		Tools.SortCode(("Adjust Size"), () =>
		{
		    try
		    {
			UpdateContainerSizes();
		    }

		    catch
		    {
			return;
		    }
		});
	    }

	    public void AddItem(params string[] names)
	    {
		Tools.SortCode(("Add items to Container"), () =>
		{
		    try
		    {
			DropItem GetItem() => new DropItem();

			for (int k = 0; k < names.Length; k += 1)
			{
			    InsertItem(GetItem(), names[k]);
			}
		    }

		    catch
		    {
			return;
		    }
		});
	    }


	    public readonly List<DropItem> ItemStack = new List<DropItem>();
	    public List<DropItem> GetItemStack() => ItemStack;

	    public bool ItemExists(int Id = -1)
	    {
		try
		{
		    if (Id != -19 && ItemStack.Count <= Id)
		    {
			return false;
		    }

		    else if (Id == -19 && ItemStack.Count < 1)
		    {
			return false;
		    }

		    return (ItemStack.Count - 1 > -1);
		}

		catch
		{
		    return false;
		}
	    }

	    public void UpdateItemLocations()
	    {
		try
		{
		    for (int k = 0, y = 0; k < LowerContainer.Controls.Count; k += 1, y += ItemHeight)
		    {
			LowerContainer.Controls[k].Top = y;
		    }
		}

		catch
		{
		    return;
		}
	    }

	    public bool RemoveItem(int Id = -19)
	    {
		try
		{
		    if (!ItemExists())
		    {
			return false;
		    }

		    LowerContainer.Controls.RemoveAt(Id);
		    ItemStack.RemoveAt(Id);

		    UpdateContainerSizes();
		    UpdateItemLocations();

		    return true;
		}

		catch
		{
		    return false;
		}
	    }

	    public bool RenameItem(string newName, int Id = -19)
	    {
		try
		{
		    if (!ItemExists())
		    {
			return false;
		    }

		    ItemStack[Id].Item.Text = newName;

		    return true;
		}

		catch
		{
		    return false;
		}
	    }


	    public DashPanel UpperContainer = new DashPanel() { Visible = false };
	    public DashPanel LowerContainer = new DashPanel() { Visible = true };

	    public bool UpdateContainerSizes()
	    {
		try
		{
		    Size UpperSize = Size.Empty;
		    Size LowerSize = Size.Empty;

		    if (ItemStack.Count > 0)
		    {
			LowerSize = new Size(ItemWidth - 4, ItemHeight - 10);
			UpperSize = new Size(ItemWidth, ItemHeight);
		    }

		    else
			goto skip;

		    Label Item = ItemStack[ItemStack.Count - 1].Item;

		    if (Item.Height + Item.Top > LowerContainer.Height)
		    {
			LowerSize = new Size(LowerContainer.Width, Item.Height + Item.Top);
			UpperSize = new Size(UpperContainer.Width, Item.Height + Item.Top);
		    }

		skip:
		    if (UpperSize != Size.Empty && LowerSize != Size.Empty)
		    {
			LowerSize.Height -= 2;
			UpperSize.Height += 8;

			Tools.Resize(UpperContainer, UpperSize);
			Tools.Resize(LowerContainer, LowerSize);
		    }

		    return true;
		}

		catch
		{
		    return false;
		}
	    }


	    public Control ContainerParent = new Control();

	    public void AddTo(Control ContainerParent, Point ContainerLoca, Color UpperBCol, Color LowerBCol)
	    {
		Tools.SortCode(("Container Insertions"), () =>
		{
		    try
		    {
			Size UpperContainerSize = new Size(ItemWidth, ItemHeight);
			Size LowerContainerSize = new Size(ItemWidth - 4, ItemHeight - 10);
			Point LowerContainerLoca = new Point(2, 5);

			Controls.Panel(UpperContainer, LowerContainer, LowerContainerSize, LowerContainerLoca, LowerBCol);
			Controls.Panel(ContainerParent, UpperContainer, UpperContainerSize, ContainerLoca, UpperBCol);

			this.ContainerParent = ContainerParent;
		    }

		    catch
		    {
			return;
		    }
		});
	    }


	    public void AddTrigger(Control Trigger, bool Hider = true)
	    {
		if (Hider && (Trigger == UpperContainer || Trigger == LowerContainer
		    || LowerContainer.Controls.Contains(Trigger))) return;

		Trigger.MouseEnter += (s, e) =>
		{
		    UpperContainer.Visible = !Hider;
		    if (!Hider) UpperContainer.BringToFront();
		};
	    }


	    public void RegisterVisibilityTrigger(Control ShowTrigger, params Control[] HideTrigger)
	    {
		try
		{
		    Tools.SortCode(("Hide Trigger Setup"), () =>
		    {
			foreach (Control ParentA in HideTrigger)
			{
			    AddTrigger(ParentA);

			    foreach (Control ParentB in ParentA.Controls)
			    {
				AddTrigger(ParentB);

				foreach (Control ParentC in ParentB.Controls)
				{
				    AddTrigger(ParentC);

				    foreach (Control ParentD in ParentC.Controls)
				    {
					AddTrigger(ParentD);
				    }
				}
			    }
			}
		    });

		    Tools.SortCode(("Show Trigger Setup"), () =>
		    {
			AddTrigger(ShowTrigger, false);
		    });
		}

		catch
		{
		    return;
		}
	    }


	    public void RegisterUpdateColor(Color onHover, Color onMouseDown, Color onClick)
	    {
		try
		{
		    foreach (Label item in LowerContainer.Controls)
		    {
			item.MouseLeave += (s, e) => item.BackColor = ItemBackColor;
			item.MouseDown += (s, e) => item.BackColor = onMouseDown;
			item.MouseEnter += (s, e) => item.BackColor = onHover;
			item.MouseClick += (s, e) => item.BackColor = onClick;
			item.MouseUp += (s, e) => item.BackColor = onHover;
		    }
		}

		catch
		{
		    return;
		}
	    }


	    public void LinkTriggerBackColorToMenu(Control Trigger, Color OriginalColor)
	    {
		try
		{
		    UpperContainer.VisibleChanged += (s, e) =>
		    {
			if (UpperContainer.Visible)
			{
			    Trigger.BackColor = UpperContainer.BackColor;
			}

			else
			{
			    Trigger.BackColor = OriginalColor;
			}
		    };
		}

		catch
		{
		    return;
		}
	    }


	    public delegate void RunHook();

	    public bool SetMouseClickHook(int Id, RunHook Run)
	    {
		try
		{
		    if (!ItemExists(Id))
		    {
			return false;
		    }

		    ItemStack[Id].Item.Click += (s, e) =>
		    {
			try
			{
			    Run();
			}

			catch
			{
			    return;
			}
		    };

		    return true;
		}

		catch
		{
		    return false;
		}
	    }
	}

	
	public class CheckBoxDropMenu
	{
	    // Future use.
	}
    }


    namespace Networking
    {
	public class DashNet
	{
	    private void HandleError(Exception E) => ErrorHandler.JustDoIt(E);


	    public bool AllowedDomain(string data) => !new List<string>()
		{ ".gov", ".govt", ".edu" }.Any(data.EndsWith);


	    public bool CanInteger(string data) => GetInteger(data) != -1;
	    public bool CanPort(string data) => (GetPort(data) != -1);
	    public bool CanByte(string data) => CanDuration(data);
	    public bool CanIP(string data) => GetIP(data) != string.Empty;


	    public bool CanDuration(string data)
	    {
		int duration = GetInteger(data);
		return (duration != 1 && duration >= 10);
	    }


	    public int GetInteger(string data)
	    {
		try
		{
		    return int.Parse(data);
		}

		catch
		{
		    return -1;
		}
	    }


	    public string GetIP(string data)
	    {
		try
		{
		    var r_host = data.ToLower();

		    if (!IPAddress.TryParse(r_host, out IPAddress ham))
		    {
			if (!r_host.Contains("http://") && !r_host.Contains("https://"))
			{
			    r_host = "https://" + r_host;
			}

			if (!Uri.TryCreate(r_host, UriKind.RelativeOrAbsolute, out Uri bacon))
			{
			    return string.Empty;
			}

			try
			{
			    r_host = Dns.GetHostAddresses(bacon.Host)[0].ToString();
			}

			catch
			{
			    return string.Empty;
			}
		    }

		    else
		    {
			r_host = ham.ToString();

			if (ham.AddressFamily != AddressFamily.InterNetwork && ham.AddressFamily != AddressFamily.InterNetworkV6)
			{
			    return string.Empty;
			}
		    }

		    if (r_host.Length < 7 || r_host == string.Empty)
		    {
			return string.Empty;
		    }

		    return r_host;
		}

		catch
		{
		    return string.Empty;
		}
	    }


	    public AddressFamily GetAddressFamily(string data)
	    {
		try
		{
		    return IPAddress.Parse(data).AddressFamily;
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public bool IsHostReachable(string host, int port = 80, SocketType socketType = SocketType.Stream, ProtocolType protocol = ProtocolType.Tcp, string packetData = ".", int timeout = 500)
	    {
		try
		{
		    using (Socket socket = new Socket(GetAddressFamily(host), socketType, protocol)
		    { LingerState = new LingerOption(true, 0) })
		    {
			IAsyncResult socketResult = socket.BeginConnect(host, port, null, null);
			bool socketSuccess = socketResult.AsyncWaitHandle.WaitOne(timeout, true);

			if (packetData != ".")
			{
			    socket.Send(Encoding.ASCII.GetBytes(packetData), SocketFlags.None);
			}

			bool connected = socket.Connected;

			socket.Disconnect(false);

			return connected;
		    }
		}

		catch
		{
		    return false;
		}
	    }


	    public int GetPort(string data)
	    {
		try
		{
		    int iData = GetInteger(data);

		    if (iData == -1 || iData < 0 || iData > 65535)
		    {
			return -1;
		    }

		    return iData;
		}

		catch
		{
		    return -1;
		}
	    }
	}
    }


    namespace DashInteract
    {
	public class DashApp
	{
	    public bool IsAdministrator() => new WindowsPrincipal(WindowsIdentity.GetCurrent())
		.IsInRole(WindowsBuiltInRole.Administrator);

	    public bool IsRunning(string ProcessName) => Process.
		GetProcessesByName(ProcessName).Length > 1;

	    public string GetFilePath() => Assembly.
		GetExecutingAssembly().Location;


	    readonly DashTools Tools = new DashTools();

	    public void RestartMe()
	    {
		StartMe();
		CloseMe();
	    }

	    public void CloseMe()
	    {
		Application.Exit();
	    }

	    public void StartMe()
	    {
		try
		{
		    Tools.StartProcess(GetFilePath());
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }
	}
    }
}

#pragma warning restore IDE1006
