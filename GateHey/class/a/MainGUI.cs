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

using DashFramework.Runnables;
using DashFramework.Erroring;
using DashFramework.Dialog;


namespace GateHey
{
    public partial class MainGUI
    {
	public readonly static DashControls Controls = new DashControls();
	public readonly static DashTools Tools = new DashTools();


	public class Initiator1
	{
	    readonly Runnable Runnables = new Runnable();

	    readonly DashPanel Panel1 = new DashPanel();
	    readonly DashPanel Panel2 = new DashPanel();

	    readonly Button Bttn1 = new Button();
	    readonly Button Bttn2 = new Button();
	    readonly Button Bttn3 = new Button();

	    readonly string ErrorMsg = ($"One or more of your settings seem to be invalid.  You can solve this by going through your settings and using common sense in order to figure out what has to be changed.  Integers require integral values and so do Strings require string values.\r\n\r\nError Code: ");
	    private string GetErrorMsg() => (ErrorMsg + Universal.GetLastError());
	    public bool IsScanning = true;

	    void ButtonHook1(Initiator2 MainSettings)
	    {
		try
		{
		    Runnables.RunTaskAsynchronously
		    (
			MainSettings.TxtBox1.Parent, () =>
			{
			    if (!IsScanning)
			    {
				if (!Universal.SettingsValidation(MainSettings, false))
				{
				    Tools.MsgBox($"{GetErrorMsg()}", icon: MessageBoxIcon.Warning);
				    return;
				}
	
				IsScanning = true;
				MainSettings.Dialog2.RunScan(MainSettings);
				IsScanning = false;
			    }

			    else
			    {   
				MainSettings.Dialog2.StopScan();
				IsScanning = false;
			    }
			}
		    );
		}

		catch (Exception E)
		{
		    throw ErrorHandler.GetException(E);
		}
	    }


	    void ButtonHook2(Initiator2 MainSettings)
	    {
		try
		{
		    if (Universal.SettingsValidation(MainSettings, false))
		    {
			Tools.MsgBox("Your settings are found to be valid.  You may now proceed.", icon: MessageBoxIcon.Information);
		    }

		    else
		    {
			Tools.MsgBox($"{GetErrorMsg()}", icon: MessageBoxIcon.Information);
		    }
		}

		catch (Exception E)
		{
		    throw ErrorHandler.GetException(E);
		}
	    }


	    readonly Dialog3 Dialog = new Dialog3();

	    void ButtonHook3()
	    {
		try
		{
		    Dialog.Show();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    public void Initiate(DashWindow Parent, Initiator2 MainSettings)
	    {
		try
		{
		    var Panel1Size = new Size(Parent.Width, 30);
		    var Panel1Loca = new Point(0, Parent.Height - 30);
		    var Panel1BCol = Parent.values.getBarColor();

		    var Panel2Size = new Size(320, 20);
		    var Panel2Loca = new Point(-2, -2);
		    var Panel2BCol = Panel1BCol;

		    Controls.Panel(Parent, Panel1, Panel1Size, Panel1Loca, Panel1BCol);
		    Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);

		    void AddButton(Button Bttn, Point Loca, string Text)
		    {
			try
			{
			    var Size = new Size(100, 20);
			    var BCol = Color.FromArgb(22, 29, 36);
			    var FCol = Color.White;

			    Controls.Button(Panel2, Bttn, Size, Loca, BCol, FCol, 1, 8, (Text));

			    Bttn.FlatAppearance.MouseDownBackColor = Panel1BCol;
			    Bttn.MouseEnter += (s, e) => Bttn.BackColor = Color.FromArgb(31, 41, 51);
			    Bttn.MouseLeave += (s, e) => Bttn.BackColor = BCol;

			    Tools.Round(Bttn, 6);
			}

			catch (Exception E)
			{
			    throw (ErrorHandler.GetException(E));
			}
		    }

		    var Loca1 = new Point(0, 0);
		    var Loca2 = new Point(110, 0);
		    var Loca3 = new Point(220, 0);

		    AddButton(Bttn1, Loca1, ("Start Scanning"));
		    AddButton(Bttn2, Loca2, ("Validate Settings"));
		    AddButton(Bttn3, Loca3, ("Service Policy"));

		    Bttn1.Click += (s, e) => ButtonHook1(MainSettings);
		    Bttn2.Click += (s, e) => ButtonHook2(MainSettings);
		    Bttn3.Click += (s, e) => ButtonHook3();

		    Dialog.Initiator(Parent);
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }
	}


	public class Initiator2
	{
	    public readonly TextBox TxtBox1 = new TextBox();//Host
	    public readonly TextBox TxtBox2 = new TextBox();//Timneout
	    public readonly TextBox TxtBox3 = new TextBox();//Threads
	    public readonly TextBox TxtBox4 = new TextBox();//Packet Data

	    public readonly Dialog1 Dialog1 = new Dialog1();
	    public readonly Dialog2 Dialog2 = new Dialog2();

	    readonly DropMenu DropMenu = new DropMenu();

	    readonly Button Bttn1 = new Button();//Ports Dialog
	    readonly Label Label2 = new Label();//Host
	    readonly Label Label3 = new Label();//Protocol
	    readonly Label Label4 = new Label();//Protocol Hover Box
	    readonly Label Label5 = new Label();//Timeout
	    readonly Label Label6 = new Label();//Threads
	    readonly Label Label7 = new Label();//Packet Data

	    public Dictionary<string, string> GetComponentValues()
	    {
		try
		{
		    return new Dictionary<string, string>()
		    {
			{ "protocol", $"{Label3.Text.Replace("-==( ", "").Replace(" )==-", "")}" },
			{ "packdata", TxtBox4.Text },
			{ "timeout", TxtBox2.Text },
			{ "threads", TxtBox3.Text },
			{ "host", TxtBox1.Text },
		    };
		}

		catch
		{
		    return null;
		}
	    }


	    void AddComponents(DashWindow Parent)
	    {
		try
		{
		    ControlHelper ConHelp = new ControlHelper()
		    {
			TextBoxFCol = Color.FromArgb(209, 209, 209),
			TextBoxBCol = Parent.BackColor,
			LabelBCol = Panel3.BackColor,
			LabelFCol = Color.White,
			TextBoxParent = Panel3,
			LabelParent = Panel3,
		    };

		    Size GetFontSize(string text, int size = 10) => Tools.GetFontSize(text, size);

		    int Y1 = 0;

		    Tools.SortCode(("Top Section"), () =>
		    {
			Size Lbl1Size = GetFontSize("Host:");
			Point Lbl1Loca = new Point(0, 2);

			Size Txt1Size = new Size(150, 21);
			Point Txt1Loca = ConHelp.ControlX(Lbl1Size, Lbl1Loca, 0);

			ConHelp.AddTextBox(TxtBox1, Txt1Size, Txt1Loca, ("https://www.google.co.uk"));

			Size Lbl2Size = GetFontSize("Protocol:");
			Point Lbl2Loca = ConHelp.ControlX(Txt1Size, Txt1Loca, 2, 10);

			Size Lbl3Size = ConHelp.TextBoxSize(Lbl2Size, Lbl2Loca, 18);
			Point Lbl3Loca = ConHelp.ControlX(Lbl2Size, Lbl2Loca, 2);

			ConHelp.AddLabel(Label4, Lbl3Size, Lbl3Loca, ("-==( TCP )==-"), 8);
			ConHelp.AddLabel(Label3, Lbl2Size, Lbl2Loca, ("Protocol:"));
			ConHelp.AddLabel(Label2, Lbl1Size, Lbl1Loca, ("Host:"));

			Label4.TextAlign = ContentAlignment.MiddleCenter;
			Label4.BackColor = Color.FromArgb(22, 29, 36);

			Y1 = (Txt1Size.Height + Txt1Loca.Y) + 10;
		    });

		    Tools.SortCode(("Middle Section"), () =>
		    {
			Size BttnSize = new Size(100, 20);
			Point BttnLoca = new Point(0, Y1 - 2);
			Color BttnBCol = Label4.BackColor;
			Color BttnFCol = Color.White;

			Bttn1.FlatAppearance.MouseDownBackColor = Parent.values.getBarColor();

			Bttn1.MouseEnter += (s, e) => Bttn1.BackColor = Color.FromArgb(31, 41, 51);
			Bttn1.MouseLeave += (s, e) => Bttn1.BackColor = BttnBCol;

			Controls.Button(Panel3, Bttn1, BttnSize, BttnLoca, BttnBCol, BttnFCol, 1, 9, ("Port Selector"));

			Dialog1.Initiator(Parent);
			Dialog2.Initiator(Parent);

			Bttn1.Click += (s, e) =>
			{
			    try
			    {
				if (!Dialog1.Visible())
				{
				    Dialog1.Show();
				}

				else
				{
				    Dialog1.Hide();
				}
			    }

			    catch (Exception E)
			    {
				throw (ErrorHandler.GetException(E));
			    }
			};

			Size Lbl1Size = GetFontSize("Timeout:");
			Point Lbl1Loca = ConHelp.ControlX(BttnSize, BttnLoca, Y1, 10);

			Size Txt1Size = new Size(55, 21);
			Point Txt1Loca = ConHelp.ControlX(Lbl1Size, Lbl1Loca, Y1 - 2);

			Size Lbl2Size = GetFontSize("Threads:");
			Point Lbl2Loca = ConHelp.ControlX(Txt1Size, Txt1Loca, Y1, 10);

			Size Txt2Size = ConHelp.TextBoxSize(Lbl2Size, Lbl2Loca);
			Point Txt2Loca = ConHelp.ControlX(Lbl2Size, Lbl2Loca, Y1 - 2);

			ConHelp.AddTextBox(TxtBox2, Txt1Size, Txt1Loca, ("5000"));
			ConHelp.AddTextBox(TxtBox3, Txt2Size, Txt2Loca, ("16"));

			ConHelp.AddLabel(Label5, Lbl1Size, Lbl1Loca, ("Timeout:"));
			ConHelp.AddLabel(Label6, Lbl2Size, Lbl2Loca, ("Threads:"));

			Y1 = (Txt2Size.Height + Txt2Loca.Y) + 10;
		    });

		    Tools.SortCode(("Bottom Section"), () =>
		    {
			Size LblSize = GetFontSize("Packet Data:");
			Point LblLoca = new Point(0, Y1);

			Size TxtSize = ConHelp.TextBoxSize(LblSize, LblLoca);
			Point TxtLoca = ConHelp.ControlX(LblSize, LblLoca, Y1 - 2);

			ConHelp.AddTextBox(TxtBox4, TxtSize, TxtLoca, ("Caterpillars flying through the sky with fluffy horns."));
			ConHelp.AddLabel(Label7, LblSize, LblLoca, ("Packet Data:"));
		    });

		    Tools.SortCode(("Rounding Controls"), () =>
		    {
			List<Control> controlCol = Tools.GetTypes(Panel3,
			    typeof(PictureBox), typeof(Button)).ToList();

			foreach (Control con in controlCol)
			{
			    Tools.Round(con, 6);
			}
		    });

		    Tools.SortCode(("Dropdown Menu"), () =>
		    {
			Point MenuLoca = new Point(Label4.Left, Label4.Height + Label4.Top);
			Color MenuBarBCol = Color.FromArgb(4, 4, 4);
			Color MenuBCol = Label4.BackColor;

			DropMenu.SetupMenu(Panel3, MenuLoca, MenuBCol, MenuBarBCol, true, Label4);

			void AddItem(string Text)
			{
			    try
			    {
				Color ItemFCol = Color.White;
				Color ItemBCol = MenuBCol;

				int ItemWidth = Label4.Width;

				DropMenu.AddItem(new Label(), ($"( {Text} )"), ItemBCol, ItemFCol,
				    ItemWidth: Label4.Width, ItemHeight: 18, ItemTextSize: 8);
			    }

			    catch (Exception E)
			    {
				throw (ErrorHandler.GetException(E));
			    }
			}

			AddItem("ICP");
			AddItem("UDP");
			AddItem("TCP");

			DropMenu.SetHoverColor(TxtBox1.BackColor);
			DropMenu.ClickUpdateLabel(Label4);
			DropMenu.ReloadDropDownMenu();
		    });
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }


	    readonly DashPanel Panel1 = new DashPanel(); // Outter Capsule (Host Settings Label + Settings Container)
	    readonly DashPanel Panel2 = new DashPanel(); // Inner Capsule 1 (Capsule for Inner Capsule 2 for border)
	    readonly DashPanel Panel3 = new DashPanel(); // Inner Capsule 2 (Main Capsule for all settings)

	    readonly Label Label1 = new Label();

	    public void Initiate(DashWindow Parent)
	    {
		try
		{
		    Size Panel1Size = new Size(Parent.Width - 40, 143);
		    Point Panel1Loca = new Point(-2, -2);
		    Color Panel1BCol = Parent.BackColor;

		    Controls.Panel(Parent, Panel1, Panel1Size, Panel1Loca, Panel1BCol);

		    Tools.SortCode(("Main Containers"), () =>
		    {
			Size TitleSize = Tools.GetFontSize("Host Settings", 14);//19px // 23
			Point TitleLoca = new Point(0, 0);
			Color TitleBCol = Parent.BackColor;
			Color TitleFCol = Color.White;

			Controls.Label(Panel1, Label1, TitleSize, TitleLoca, TitleBCol, TitleFCol, ("Host Settings"), 1, 14);

			Size Panel2Size = new Size(Panel1Size.Width, 110);
			Point Panel2Loca = new Point(0, 33);
			Color Panel2BCol = Parent.values.getBarColor();

			Size Panel3Size = Tools.SubstractSize(20, Panel2Size);
			Point Panel3Loca = new Point(10, 10);
			Color Panel3BCol = Panel2BCol;

			Controls.Panel(Panel1, Panel2, Panel2Size, Panel2Loca, Panel2BCol);
			Controls.Panel(Panel2, Panel3, Panel3Size, Panel3Loca, Panel3BCol);

			Tools.Round(Panel2, 6);
		    });

		    AddComponents(Parent);
		}

		catch (Exception E)
		{
		    ErrorHandler.JustDoIt(E);
		}
	    }
	}


	readonly Initiator1 InitiateBottom = new Initiator1();
	readonly Initiator2 InitiateMiddle = new Initiator2();

	public void Initiator(DashWindow inst)
	{
	    try
	    {
		InitiateBottom.Initiate(inst, InitiateMiddle);
		InitiateMiddle.Initiate(inst);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}
    }
}
