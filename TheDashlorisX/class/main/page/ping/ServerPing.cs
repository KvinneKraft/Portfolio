
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class ServerPing
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();
	private readonly PictureBox S1Container2 = new PictureBox();
	private readonly PictureBox S1Container3 = new PictureBox();

	private void Init1(DashWindow DashWindow, PictureBox Capsule)
	{
	    try
	    {
		var Container1Size = Capsule.Size;
		var Container1Loca = new Point(0, 0);

		Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Capsule.BackColor);

		var Container2Size = new Size(Capsule.Width, 100);
		var Container2Loca = new Point(0, 0);

		var Container3Size = new Size(Capsule.Width, 135);
		var Container3Loca = new Point(0, 118);

		var ContainerBCol = DashWindow.MenuBar.MenuBar.BackColor;

		Control.Image(S1Container1, S1Container2, Container2Size, Container2Loca, ContainerBCol);
		Control.Image(S1Container1, S1Container3, Container3Size, Container3Loca, ContainerBCol);

		Tool.Round(S1Container2, 6);
		Tool.Round(S1Container3, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S2Container1 = new PictureBox();

	private readonly TextBox S2TextBox1 = new TextBox();
	private readonly TextBox S2TextBox2 = new TextBox();
	private readonly TextBox S2TextBox3 = new TextBox();
	private readonly TextBox S2TextBox4 = new TextBox();

	private readonly Label S2Label1 = new Label();
	private readonly Label S2Label2 = new Label();
	private readonly Label S2Label3 = new Label();
	private readonly Label S2Label4 = new Label();
	private readonly Label S2Label5 = new Label();
	private readonly Label S2Label6 = new Label();

	private readonly DropMenu S2DropMenu = new DropMenu();

	private void S2SetupDropDownMenu()
	{
	    try
	    {
		var DropMenuLoca = new Point(S1Container2.Left + S2Label6.Left + 10, S1Container2.Top + S2Label6.Top + S2Label6.Height + 10);
		var DropMenuBCol = S2Label6.BackColor;

		S2DropMenu.SetupMenu(S1Container1, DropMenuLoca, DropMenuBCol, DropMenuBCol);

		int ItemWidth = S2Label6.Width - 4;
		int ItemHeight = 18;

		var ItemBCol = S2Container1.BackColor;

		S2DropMenu.AddItem(new Label(), ("(T.C.P)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);
		S2DropMenu.AddItem(new Label(), ("(U.D.P)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);
		S2DropMenu.AddItem(new Label(), ("(ICMPv)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 7);

		S2Label6.MouseEnter += (s, e) =>
		{
		    try
		    {
			S2DropMenu.Show();
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

	private void Init2(PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(S1Container2.Width - 20, S1Container2.Height - 20);
		var ContainerLoca = new Point(10, 10);
		var ContainerBCol = S1Container2.BackColor;

		Control.Image(S1Container2, S2Container1, ContainerSize, ContainerLoca, ContainerBCol);

		ControlHelper CHelper = new ControlHelper()
		{
		    TextBoxBCol = Capsule.BackColor,
		    TextBoxParent = S2Container1,
		    TextBoxFCol = Color.White,

		    LabelParent = S2Container1,
		    LabelBCol = ContainerBCol,
		    LabelFCol = Color.White
		};

		var Label1Size = CHelper.GetFontSize("Host:");
		var Label1Loca = new Point(0, 0);

		var TextBox1Size = new Size(165, 20);
		var TextBox1Loca = new Point(Label1Size.Width, 0);

		var Label2Size = CHelper.GetFontSize("Port:");
		var Label2Loca = CHelper.ControlX(TextBox1Size, TextBox1Loca);

		var TextBox2Size = CHelper.TextBoxSize(Label2Size, Label2Loca);
		var TextBox2Loca = CHelper.ControlX(Label2Size, Label2Loca, Extra: 0);

		var Label3Size = CHelper.GetFontSize("Packet Size:");
		var Label3Loca = new Point(0, TextBox2Loca.Y + TextBox2Size.Height + 10);

		var TextBox3Size = new Size(100, 20);
		var TextBox3Loca = CHelper.ControlX(Label3Size, Label3Loca, Extra: 0);

		var Label4Size = CHelper.GetFontSize("T.T.L:");
		var Label4Loca = CHelper.ControlX(TextBox3Size, TextBox3Loca);

		var TextBox4Size = CHelper.TextBoxSize(Label4Size, Label4Loca);
		var TextBox4Loca = CHelper.ControlX(Label4Size, Label4Loca, Extra: 0);

		var Label5Size = CHelper.GetFontSize("Protocol:");
		var Label5Loca = new Point(0, TextBox4Loca.Y + TextBox2Size.Height + 10);

		var Label6Size = new Size(100, Label5Size.Height);
		var Label6Loca = CHelper.ControlX(Label5Size, Label5Loca, Extra: 0);

		CHelper.AddTextBox(S2TextBox1, TextBox1Size, TextBox1Loca, ("https://pugpawz.com"));
		CHelper.AddTextBox(S2TextBox2, TextBox2Size, TextBox2Loca, ("80"));
		CHelper.AddTextBox(S2TextBox3, TextBox3Size, TextBox3Loca, ("2021"));
		CHelper.AddTextBox(S2TextBox4, TextBox4Size, TextBox4Loca, ("75"));

		foreach (Control Control in S2Container1.Controls)
		{
		    if (Control is PictureBox)
		    {
			Tool.Round(Control, 6);
		    }
		}

		CHelper.AddLabel(S2Label3, Label3Size, Label3Loca, ("Packet Size:"));
		CHelper.AddLabel(S2Label5, Label5Size, Label5Loca, ("Protocol:"));
		CHelper.AddLabel(S2Label4, Label4Size, Label4Loca, ("T.T.L:"));
		CHelper.AddLabel(S2Label1, Label1Size, Label1Loca, ("Host:"));
		CHelper.AddLabel(S2Label2, Label2Size, Label2Loca, ("Port:"));
		CHelper.AddLabel(S2Label6, Label6Size, Label6Loca, ("--- (TCP) ---"), 8);

		S2Label6.TextAlign = ContentAlignment.MiddleCenter;
		S2Label6.BackColor = Capsule.BackColor;

		S2SetupDropDownMenu();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly TextBox S3TextBox1 = new TextBox();

	private readonly Button S3Button1 = new Button();

	private readonly Label S3Label1 = new Label();

	private void Init3()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private bool isInitialized = false;

	public void InitializePage(DashWindow DashWindow, PictureBox Capsule, InitThaDashlorisX Parent)
	{
	    try
	    {
		if (!isInitialized)
		{
		    Init1(DashWindow, Capsule);
		    Init2(Capsule);
		    Init3();

		    isInitialized = true;
		}

		Show();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}

	public void Show()
	{
	    try
	    {
		S1Container1.Show();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Hide()
	{
	    try
	    {
		S1Container1.Hide();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
