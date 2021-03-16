
//
// All rights reserved to me Dashie for coding all of this.  If you wish to make use of this code, you can.
// Just make sure to leave this top part in if you are going to redistribute any part of my code.  Thank you.
//
// -Dashie

using System;
using System.Net;
using System.Linq;
using System.Drawing;
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

using DashFramework.DashLogic;
using DashFramework.Erroring;
using DashFramework.Dialog;

using TheDashlorisX.Properties;

// Implement DropDownMenu.cs and DashMessageBox.cs (also recode from the brain);

namespace DashFramework
{
    namespace Interface
    {
	namespace Controls
	{
	    public class DashControls
	    {
		private readonly DashTools Tool = new DashTools();

		public void SetUrl(Control Object, string Destination)
		{
		    try
		    {
			Object.Click += (s, e) =>
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
			};
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		public Point CalculateCenter(Control Top, Control Object, Point ObjectLocation)
		{
		    if (ObjectLocation.X == -2)
		    {
			ObjectLocation.X = (Top.Width - Object.Width) / 2;
		    }

		    if (ObjectLocation.Y == -2)
		    {
			ObjectLocation.Y = (Top.Height - Object.Height) / 2;
		    }

		    return ObjectLocation;
		}

		public void CheckBox(Control Top, PictureBox Container1, PictureBox Container2, Size Size, Point Loca, Color DeselectedBCol, [Optional] Color SelectedBCol, [Optional] bool Select)
		{
		    try
		    {
			Image(Top, Container1, Size, Loca, DeselectedBCol);

			Size.Height -= 4;
			Size.Width -= 4;

			Loca.X += 2;
			Loca.Y += 2;

			Image(Top, Container2, Size, Loca, DeselectedBCol);

			if (Select)
			    Container2.BackColor = SelectedBCol;

			void UpdateColor()
			{
			    if (Container2.BackColor == SelectedBCol)
			    {
				Container2.BackColor = DeselectedBCol;
			    }

			    else
			    {
				Container2.BackColor = SelectedBCol;
			    }
			};

			Container1.Click += (s, e) =>
			    UpdateColor();
			
			Container2.Click += (s, e) =>
			    UpdateColor();
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

			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;

			Object.Location = CalculateCenter(Top, Object, ObjectLocation);
			Object.Font = Tool.GetFont(FontTypeID, FontSize);

			Object.BorderStyle = BorderStyle.None;

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

		public readonly Dictionary<TextBox, PictureBox> TextBoxContainers = new Dictionary<TextBox, PictureBox>();

		public void TextBox(Control Top, TextBox Object, Size ObjectSize, Point ObjectLocation, Color ObjectBCol, Color ObjectFCol, int FontTypeID, int FontSize, bool ReadOnly = false, bool Multiline = false, bool ScrollBar = false, bool FixedSize = true, bool TabStop = false)
		{
		    try
		    {
			Tool.Resize(Object, ObjectSize);

			Object.Location = CalculateCenter(Top, Object, ObjectLocation);

			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;

			Object.BorderStyle = BorderStyle.None;

			Object.Multiline = Multiline;
			Object.ReadOnly = ReadOnly;
			Object.TabStop = TabStop;

			if (FixedSize)
			{
			    var TextBoxContainer = new PictureBox();

			    Tool.Resize(TextBoxContainer, ObjectSize);

			    TextBoxContainer.Location = ObjectLocation;
			    TextBoxContainer.Font = Tool.GetFont(FontTypeID, FontSize);

			    TextBoxContainer.BorderStyle = BorderStyle.None;

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

			    TextBoxContainers.Add(Object, TextBoxContainer);
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

		public void Button(Control Top, Button Object, Size ObjectSize, Point ObjectLocation, Color ObjectBCol, Color ObjectFCol, int FontTypeID, int FontSize, string ButtonText, bool TabStop = false)
		{
		    try
		    {
			Tool.Resize(Object, ObjectSize);

			Object.Location = CalculateCenter(Top, Object, ObjectLocation);

			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;

			Object.Font = Tool.GetFont(FontTypeID, FontSize);
			Object.Text = ButtonText;

			Object.FlatAppearance.BorderColor = ObjectBCol;
			Object.FlatAppearance.BorderSize = 0;

			Object.FlatStyle = FlatStyle.Flat;
			Object.TabStop = TabStop;

			Top.Controls.Add(Object);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		public void Label(Control Top, Label Object, Size ObjectSize, Point ObjectLocation, Color ObjectBCol, Color ObjectFCol, int FontTypeID, int FontSize, string LabelText, bool TabStop = false)
		{
		    try
		    {
			if (ObjectSize == Size.Empty)
			{
			    ObjectSize = Tool.GetFontSize(LabelText, FontSize);
			}

			Tool.Resize(Object, ObjectSize);

			Object.Location = CalculateCenter(Top, Object, ObjectLocation);

			Object.BackColor = ObjectBCol;
			Object.ForeColor = ObjectFCol;

			Object.Font = Tool.GetFont(FontTypeID, FontSize);
			Object.Text = LabelText;

			Object.BorderStyle = BorderStyle.None;
			Object.FlatStyle = FlatStyle.Flat;

			Object.TabStop = TabStop;

			Top.Controls.Add(Object);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		public void Image(Control Top, PictureBox Object, Size ObjectSize, Point ObjectLocation, Color BackColor, Image ObjectImage = null, bool TabStop = false)
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

			Object.Location = CalculateCenter(Top, Object, ObjectLocation);

			Object.BackColor = BackColor;
			Object.TabStop = TabStop;

			Object.BorderStyle = BorderStyle.None;
			Object.Image = ObjectImage;

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
	    public class DashTools
	    {
		public Size GetFontSize(string Text, int Size)
		{
		    return TextRenderer.MeasureText(Text, GetFont(1, Size));
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

			Target.Location = new Point(Target.Location.X + (e.X - Location.X), Target.Location.Y + (e.Y - Location.Y));
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
				Resources.main,
				Resources.cute,
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

			return new Font(FontCollection.Families[FontId], Height, FontStyle.Regular);
		    }

		    catch { /*Silenced for now.*/ return new Font("Modern", Height, FontStyle.Regular); };
		}
	    }
	}
    }


    namespace Erroring
    {
	public class ErrorHandler
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();
	    
	    public static string GetRawFormat(Exception E)
	    {
		return string.Format
		(
		    //$"An error has occurred and has prevented the application from functioning any further, safely.\r\n\r\nPlease send the following to KvinneKraft@protonmail.com if you wish to help me fix this issue.\r\n\r\n" +
		    $"----------------------\r\n" +
		    $"{E.StackTrace}\r\n" +
		    $"----------------------\r\n" +
		    $"{E.Message}\r\n" +
		    $"----------------------\r\n" +
		    $"{E.Source}\r\n"
		    //$"I would also recommend making sure you actually downloaded the application from my website https://pugpawz.com and not some other sketchy website.\r\n\r\nAll the latest versions are available at my GitHub at https://github.com/KvinneKraft"
		);
	    }

	    public static Exception GetException(Exception E) =>
		new Exception(GetRawFormat(E));

	    public static void Utilize(string description, string title)
	    {
		DashDialog ErrorDialog = new DashDialog();

		Color ContainerBCol = Color.FromArgb(9, 39, 66);
		Color MenuBarBCol = Color.FromArgb(19, 36, 64);
		Color AppBCol = Color.FromArgb(6, 17, 33);

		ErrorDialog.Show(AppBCol, Color.White, Size.Empty, description, title, DashDialog.Buttons.OK);

		Environment.Exit(-1);
	    }

	    public static void JustDoIt(Exception E, string title = ("Error Handler")) =>
		Utilize(GetRawFormat(E), title);
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


    namespace Dialog
    {
	public class DashDialog
	{//Just update pre-initialized component in future.	
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
			AppMenuBar:false, StartPosition:FormStartPosition.CenterParent);

		    Tool.Interactive(Dialog, Dialog);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public readonly PictureBox S2Container1 = new PictureBox();
	    public readonly PictureBox S2Container2 = new PictureBox();

	    public readonly TextBox S2TextBox1 = new TextBox();

	    public readonly Button S2Button1 = new Button();
	    public readonly Button S2Button2 = new Button();
	    
	    private int S2ButtonID = 0;

	    public readonly Label S2Label1 = new Label();

	    public void InitS2(Color DialogFCol, string Description, string Title, Buttons DialogButtons)
	    {
		try
		{
		    var LabelLoca = new Point(-2, 12);

		    Control.Label(Dialog, S2Label1, Size.Empty, LabelLoca, Dialog.BackColor, DialogFCol, 1, 10, Title);

		    var Container1Size = new Size(Dialog.Width - 20, Dialog.Height - (S2Label1.Height + 68));
		    var Container1Loca = new Point(10, S2Label1.Height + S2Label1.Top + 10);
		    var Container1BCol = Dialog.BackColor;

		    Control.Image(Dialog, S2Container1, Container1Size, Container1Loca, Container1BCol);
		    Tool.Round(S2Container1, 6);

		    var TextBoxSize = Efficiency.Resize(Container1Size, 8, 8, false);
		    var TextBoxLoca = new Point(4, 4);

		    Control.TextBox(S2Container1, S2TextBox1, TextBoxSize, TextBoxLoca, Container1BCol, DialogFCol, 1, 8, true, true, true, false);
		    S2TextBox1.Text = Description;

		    var Container2Loca = new Point(Container1Loca.X, Container1Size.Height + Container1Loca.Y + 10);
		    var Container2Size = new Size(Container1Size.Width, 24);

		    Control.Image(Dialog, S2Container2, Container2Size, Container2Loca, Container1BCol);

		    string []Texts = new string[] { "Okay", "" }; 

		    switch (DialogButtons)
		    {
			case Buttons.OKCancel:
	    			Texts[1] = ("Cancel");
			    break;

			case Buttons.YesNo:
				Texts[0] = ("Yes");
				Texts[1] = ("No");
			    break;
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

	    // If already initialized, allow the ability to show already initialized dialog.  |  Assuming all parameters are given.
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
	    
	    public bool Minimize = false;
	    public bool Close = false;
	    public bool Hide = false;

	    public readonly Label MenuBarTitle = new Label();

	    public DashMenuBar(string Title, bool MinimizeButton, bool CloseButton, bool HideDialog = true)
	    {
		try
		{
		    Minimize = MinimizeButton;
		    Close = CloseButton;
		    Hide = HideDialog;

		    MenuBarTitle.Text = (Title);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public readonly PictureBox MenuBar = new PictureBox();

	    public readonly PictureBox LogoLayer1 = new PictureBox();
	    public readonly PictureBox LogoLayer2 = new PictureBox();

	    public readonly Button Button1 = new Button();
	    public readonly Button Button2 = new Button();

	    public void AddMe(Control Surface, Color BarBCol, Color BorderBCol, int MenuBarHeight = 26)
	    {
		var MenuBarSize = new Size(Surface.Width, MenuBarHeight);
		var MenuBarLoca = new Point(0, 0);
		var MenuBarBCol = BarBCol;

		Control.Image(Surface, MenuBar, MenuBarSize, MenuBarLoca, MenuBarBCol);
		Tool.Interactive(MenuBar, Surface);

		var LogoSize = new Size(38, 32);
		var LogoLoca = new Point(5, 5);

		Control.Image(Surface, LogoLayer2, LogoSize, LogoLoca, Surface.BackColor, ObjectImage: Resources.LOGO);
		Control.Image(MenuBar, LogoLayer1, LogoSize, LogoLoca, BarBCol, ObjectImage: Resources.LOGO);

		Tool.Interactive(LogoLayer1, Surface);
		Tool.Interactive(LogoLayer2, Surface);
		
		var TitleSize = Tool.GetFontSize(MenuBarTitle.Text, 8);
		var TitleLoca = new Point(LogoSize.Width + LogoLoca.X + 5, (MenuBarSize.Height - TitleSize.Height) / 2);

		Control.Label(MenuBar, MenuBarTitle, TitleSize, TitleLoca, BarBCol, Color.White, 1, 8, MenuBarTitle.Text);
		Tool.Interactive(MenuBarTitle, Surface);

		var ButtonSize = new Size(65, MenuBarHeight);
		var ButtonLoca = new Point(MenuBarSize.Width - ButtonSize.Width, 0);

		if (Close)
		{
		    Control.Button(MenuBar, Button1, ButtonSize, ButtonLoca, BarBCol, Color.White, 1, 10, "X");
		    Tool.Interactive(Button1, Surface);

		    Button1.Click += (s, e) =>
		    {
			if (!Hide)
			{
			    Application.Exit();
			}

			else
			{
			    Surface.Hide();
			}
		    };
		}

		if (Close && Minimize)
		{
		    ButtonLoca.X -= ButtonSize.Width;
		}

		else if (Minimize)
		{
		    Control.Button(MenuBar, Button2, ButtonSize, ButtonLoca, BarBCol, Color.White, 1, 10, "-");

		    Button2.Click += (s, e) =>
		    {
			Surface.SendToBack();
		    };

		    Tool.Interactive(Button2, Surface);
		}

		var RectangleSize = new Size(MenuBar.Width - 2, Surface.Height - MenuBar.Height + 1);
		var RectangleLocation = new Point(1, MenuBar.Height + MenuBar.Top - 2);
		
		Tool.PaintRectangle(Surface, 3, RectangleSize, RectangleLocation, BorderBCol);
	    }

	    public void UpdateTitle(string NewValue)
	    {
		try
		{
		    var NewLabelSize = Tool.GetFontSize(NewValue, 8);
		    var NewLabelLoca = new Point(MenuBarTitle.Left, (MenuBar.Height - NewLabelSize.Height) / 2);

		    MenuBarTitle.Location = NewLabelLoca;
		    MenuBarTitle.Text = NewValue;

		    Tool.Resize(MenuBarTitle, NewLabelSize);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public void UpdateColor(Color NewValue1, Color NewValue2)
	    {
		try
		{
		    foreach (Control Control in MenuBar.Controls)
		    {
			Control.BackColor = NewValue1;
		    }

		    LogoLayer2.BackColor = MenuBar.Parent.BackColor;
		    MenuBar.BackColor = NewValue1;

		    var RectSize = new Size(MenuBar.Width-2, MenuBar.Parent.Height - MenuBar.Height);
		    var RectLoca = new Point(1, MenuBar.Height + MenuBar.Top - 3);

		    Tool.PaintRectangle(MenuBar.Parent, 3, RectSize, RectLoca, NewValue2);
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

	    private void InitS1(Size AppSize, string AppTitle, Color AppBCol, FormStartPosition AppStartPosition = FormStartPosition.CenterScreen, FormBorderStyle AppBorderStyle = FormBorderStyle.None)
	    {
		try
		{
		    SuspendLayout();

		    Icon = Resources.ICON;

		    FormBorderStyle = AppBorderStyle;
		    StartPosition = AppStartPosition;

		    MaximumSize = AppSize;
		    MinimumSize = AppSize;

		    BackColor = AppBCol;

		    Text = AppTitle;
		    Name = AppTitle;

		    Tool.Round(this, 6);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public DashMenuBar MenuBar = null;

	    private void InitS2(string AppTitle, bool AppMinim, bool AppClose, bool AppHide, Color MenuBarBCol)
	    {
		try
		{
		    MenuBar = new DashMenuBar(AppTitle, AppMinim, AppClose, AppHide);
		    MenuBar.AddMe(this, MenuBarBCol, MenuBarBCol);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public void InitializeWindow(Size AppSize, string AppTitle, Color AppBCol, Color MenuBarBCol, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None, bool MenuBarMinim = false, bool MenuBarClose = true, bool CloseHideApp = true, bool AppMenuBar = true)
	    {
		try
		{
		    InitS1(AppSize, AppTitle, AppBCol, AppStartPosition: StartPosition, AppBorderStyle: FormBorderStyle);

		    if (AppMenuBar)
		    {
			InitS2(AppTitle, MenuBarMinim, MenuBarClose, CloseHideApp, MenuBarBCol);
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private bool DoInitialize = true;

	    public void ShowWindow(Size AppSize, string AppTitle, Color AppBCol, Color MenuBarBCol, FormStartPosition StartPosition = FormStartPosition.CenterScreen, FormBorderStyle FormBorderStyle = FormBorderStyle.None, bool ShowDialog = true, bool MenuBarMinim = false, bool MenuBarClose = true, bool CloseHideApp = true, bool AppMenuBar = true)
	    {
		try
		{
		    if (DoInitialize)
		    {
			InitializeWindow(AppSize, AppTitle, AppBCol, MenuBarBCol, StartPosition, FormBorderStyle, MenuBarMinim, MenuBarClose, CloseHideApp, AppMenuBar);
			DoInitialize = false;
		    }

		    ShowAsIs(ShowDialog);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public void ShowAsIs(bool ShowDialog = true)
	    {
		try
		{
		    if (ShowDialog)
		    {
			this.ShowDialog();
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
		S2Pages = (TextRenderer.MeasureText
		(
		    PageData, Tool.GetFont(1, 9), new Size(ConSize.Width, 800),
		    flags: TextFormatFlags.WordBreak
		)

		.Height / ConSize.Height) + 1;
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

		    Control.Label(S2Container1, S2Label2, Label2Size, Label2Loca, S2Container1.BackColor, Color.White, 1, 9, $"1/{S2Pages}");
		    Control.Label(S2Container1, S2Label1, Label1Size, Label1Loca, S2Container1.BackColor, Color.White, 1, 9, "Page:");
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

		    Control.Label(S3Container1, S3Label1, LabelSize, LabelLoca, ContainerBCol, Color.White, 1, 9, (LabelText));

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
		    var LabelFSiz = TextRenderer.MeasureText(LabelText, Tool.GetFont(1, 9), Size.Empty, flags: TextFormatFlags.WordBreak);//Tool.GetFontSize(TermsOfServices(), 9);
		    var LabelSize = new Size(ContainerSize.Width - 4, LabelFSiz.Height - 4);
		    var LabelLoca = new Point(2, 2);

		    Control.Label(S4Container1, S4Label1, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, 9, LabelText);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public void SetupPages(PictureBox Capsule, Tuple<string, Size, Point> ContainerSetup, Tuple<Color, Color, string> LabelSetup) //string TopBarTitle, Color ConBCol, Color ConFCol, Size ConSize, Point ConLoca, Color LabelBCol, Color LabelFCol, string PageData, int Pages)
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


	public class DropMenu
	{
	    private readonly EfficiencyTools Efficiency = new EfficiencyTools();
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();

	    public void Hide()
	    {
		try
		{
		    Container.Hide();
		    Container.SendToBack();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public void Show()
	    {
		try
		{
		    Container.Show();
		    Container.BringToFront();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	    
	    public readonly PictureBox ContentContainer = new PictureBox();
	    public readonly PictureBox Container = new PictureBox();

	    public void SetupMenu(Control Top, Point MenuLocation, Color MenuColor, Color MenuBorderColor)
	    {
		var Container1Size = new Size(10, 0);
		var Container1Loca = MenuLocation;
		var Container1BCol = MenuColor;

		Control.Image(Top, Container, Container1Size, Container1Loca, Container1BCol);

		var Container2Size = Efficiency.Resize(Container1Size, 4, 4, false);
		var Container2Loca = new Point(2, 2);
		var Container2BCol = Color.White;

		Control.Image(Container, ContentContainer, Container2Size, Container2Loca, Container2BCol);

		var RectSize = Efficiency.Resize(Container1Size, 4, 4, false);
		var RectLoca = new Point(1, 1);
		var RectBCol = MenuBorderColor;

		Tool.PaintRectangle(Container, 2, RectSize, RectLoca, RectBCol);

		Top.MouseEnter += (s, e) =>
		{
		    if (Container.Visible)
		    {
			Hide();
		    }
		};

		Hide();
	    }

	    public readonly Dictionary<int, Label> MenuItems = new Dictionary<int, Label>();

	    private int GetY()
	    {
		if (MenuItems.Count > 0)
		{
		    var Item = ContentContainer.Controls[ContentContainer.Controls.Count - 1];
		    return (Item.Top + Item.Height);
		}

		return 0;
	    }

	    public void AddItem(Label Object, string ItemName, Color ItemBCol, Color ItemFCol, int Index = -2, int ItemWidth = -2, int ItemHeight = -2, int ItemTextSize = 10)
	    {
		if (ItemWidth == -2 || ItemHeight == -2)
		{
		    if (ItemWidth == -2)
		    {
			ItemWidth = ContentContainer.Width;
		    }

		    if (ItemHeight == -2)
		    {
			ItemHeight = Tool.GetFontSize(ItemName, ItemTextSize).Height + 6;
		    }
		}

		var ItemSize = new Size(ItemWidth, ItemHeight);
		var ItemLoca = new Point(0, GetY());

		Control.Label(ContentContainer, Object, ItemSize, ItemLoca, ItemBCol, ItemFCol, 1, ItemTextSize, ItemName);
		Object.TextAlign = ContentAlignment.TopCenter;

		int GetContentContainerWidth()
		{
		    if (ItemSize.Width > ContentContainer.Width)
		    {
			return (ItemSize.Width);
		    }

		    return (ContentContainer.Width);
		}

		var Container2Size = new Size(GetContentContainerWidth(), ContentContainer.Height + ItemHeight);
		var Container1Size = Efficiency.Resize(Container2Size, 4, 4);

		Tool.Resize(ContentContainer, Container2Size);
		Tool.Resize(Container, Container1Size);

		if (Index == -2)
		{
		    Index = MenuItems.Count;
		}

		MenuItems.Add(Index, Object);
	    }

	    public Label GetItem(int Index = -2)
	    {
		try
		{
		    if (Index == -2)
		    {
			Index = MenuItems.Count - 1;
		    }

		    if (MenuItems.Count - 1 < Index)
		    {
			return null;
		    }

		    return MenuItems[Index];
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private void ReloadDropDownMenu()
	    {
		try
		{
		    var ItemLoca = new Point(0, 0);
		    var MenuItem = new List<Label>();

		    foreach (Label Item in this.MenuItems.Values)
		    {
			Item.Location = ItemLoca;
			ItemLoca.Y += Item.Height;
		 
			MenuItem.Add(Item);
		    }

		    this.MenuItems.Clear();

		    for (int k = 0; k < MenuItems.Count; k += 1)
		    {
			this.MenuItems.Add(k, MenuItems[k]);
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public bool RemoveItem(int Index = -2)
	    {
		try
		{
		    if (Index == -2)
		    {
			var Count = MenuItems.Count;

			if (Count >= 1)
			{
			    Count -= 1;
			}

			Index = Count;
		    }

		    if (!MenuItems.ContainsKey(Index))
		    {
			return false;
		    }

		    ContentContainer.Controls.RemoveAt(Index);
		    MenuItems.Remove(Index);

		    ReloadDropDownMenu();

		    int GetMenuHeight()
		    {
			var Height = 0;

			if (MenuItems.Count > 0)
			{
			    Height = MenuItems[MenuItems.Count - 1].Top + MenuItems[MenuItems.Count - 1].Height - 4;
			}

			return Height;
		    }

		    var Container2Size = new Size(ContentContainer.Width, GetMenuHeight());
		    var Container1Size = Efficiency.Resize(Container2Size, 4, 4);

		    Tool.Resize(ContentContainer, Container2Size);
		    Tool.Resize(Container, Container1Size);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}

		return true;
	    }
	}
    }


    namespace Networking
    {
	public class DashNet
	{
	    private void HandleError(Exception E) =>
		ErrorHandler.JustDoIt(E);

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

	    public bool CanInteger(string data) =>
		GetInteger(data) != -1;

	    public bool CanDuration(string data)
	    {
		int duration = GetInteger(data);
		return (duration != 1 && duration >= 10);
	    }

	    public bool CanByte(string data) =>
		(CanDuration(data));
	    
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

	    public bool CanIP(string data) =>
		(GetIP(data) != string.Empty);

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

	    public bool IsHostReachable(string host, int port = 80, int timeout = 500)
	    {
		try
		{
		    using (Socket socket = new Socket(GetAddressFamily(host), SocketType.Stream, ProtocolType.Tcp))
		    {
			IAsyncResult socketResult = socket.BeginConnect(host, port, null, null);
			bool socketSuccess = socketResult.AsyncWaitHandle.WaitOne(timeout, true);
			return socket.Connected;
		    }
		}

		catch
		{
		    return false;
		}
	    }

	    public bool AllowedDomain(string data) =>
		new List<string>() { ".gov", ".govt", ".edu" }.Any(data.EndsWith);
	    
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

	    public bool CanPort(string data) =>
		(GetPort(data) != -1);
	}
    }

    
    namespace System
    {
	public class DashInteract
	{
	    public bool isAdministrator() =>
		 new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

	    public string getFilePath() =>
		Assembly.GetExecutingAssembly().Location;
	}
    }
}
