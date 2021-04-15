
// Author: Dashie
// Version: 3.0
// Coded while high....

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
using DashFramework.Networking;
using DashFramework.Erroring;
using DashFramework.Dialog;

namespace TheDashlorisX
{
    public class Settings
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	public readonly PictureBox S1Container1 = new PictureBox();
	private readonly PictureBox S1Container2 = new PictureBox();

	private void InitS1(PictureBox Capsule, DashWindow DashWindow)
	{
	    try
	    {
		var Container1Size = new Size(Capsule.Width, 140);
		var Container1Loca = new Point(-2, -2);
		var Container1BCol = Capsule.BackColor;

		Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Container1BCol);

		var ContainerBCol = DashWindow.MenuBar.MenuBar.BackColor;

		var Container2Size = new Size(Capsule.Width, 140);//95);
		var Container2Loca = new Point(0, 0);
		
		Control.Image(S1Container1, S1Container2, Container2Size, Container2Loca, ContainerBCol);
 
		Tool.Round(S1Container2, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S2Container1 = new PictureBox();
	private readonly PictureBox S2Container2 = new PictureBox();

	public readonly PictureBox S2Container3 = new PictureBox();

	public readonly TextBox S2Textbox1 = new TextBox();
	public readonly TextBox S2Textbox2 = new TextBox();
	public readonly TextBox S2Textbox3 = new TextBox();
	public readonly TextBox S2Textbox4 = new TextBox();
	public readonly TextBox S2Textbox5 = new TextBox();

	public readonly Label S2Label1 = new Label();
	private readonly Label S2Label2 = new Label();
	private readonly Label S2Label3 = new Label();
	private readonly Label S2Label4 = new Label();
	private readonly Label S2Label5 = new Label();
	private readonly Label S2Label6 = new Label();
	private readonly Label S2Label7 = new Label();
	private readonly Label S2Label8 = new Label();

	private readonly ControlHelper CHelper = new ControlHelper();
	private readonly DropMenu DropMenu = new DropMenu();

	private void S2SetupDropDownMenu()
	{
	    try
	    {
		var DropMenuLoca = new Point(S1Container2.Left + S2Label7.Left + 10, 10 + S1Container2.Top + S2Label7.Top + S2Label7.Height);
		var DropMenuBCol = S2Textbox4.BackColor;

		DropMenu.SetupMenu(S1Container1, DropMenuLoca, DropMenuBCol, DropMenuBCol);

		int ItemWidth = S2Label7.Width - 4;
		int ItemHeight = 16;

		var ItemBCol = S2Container1.BackColor;

		DropMenu.AddItem(new Label(), ("(POST)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 6);
		DropMenu.AddItem(new Label(), ("(PUT)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 6);
		DropMenu.AddItem(new Label(), ("(GET)"), ItemBCol, Color.White, ItemWidth: ItemWidth, ItemHeight: ItemHeight, ItemTextSize: 6);
		
		foreach (Control Item in DropMenu.ContentContainer.Controls)
		{
		    try
		    {
			Item.Click += (s, e) =>
			{
			    S2Label7.Text = $"---{Item.Text}---";
			};
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}

		S2Label7.MouseEnter += (s, e) =>
		{
		    try
		    {
			DropMenu.Show();
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

	private void InitS2(PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = new Size(S1Container2.Width - 20, S1Container2.Height - 20);
		var ContainerLoca = new Point(10, 10);

		Control.Image(S1Container2, S2Container1, ContainerSize, ContainerLoca, S1Container2.BackColor);

		CHelper.TextBoxParent = S2Container1;
		CHelper.LabelParent = S2Container1;

		var Label1Loca = new Point(0, 0);
		var Label1Size = CHelper.GetFontSize("Send Delay:");

		var TextBox1Loca = new Point(Label1Size.Width, 0);
		var TextBox1Size = new Size(70, 20);

		var Label2Loca = CHelper.ControlX(TextBox1Size, TextBox1Loca);
		var Label2Size = CHelper.GetFontSize("Timeout:");

		var TextBox2Loca = CHelper.ControlX(Label2Size, Label2Loca, Extra: 0);
		var TextBox2Size = CHelper.TextBoxSize(Label2Size, Label2Loca);

		var Label3Loca = new Point(0, Label1Size.Height + Label1Loca.Y + 10);
		var Label3Size = CHelper.GetFontSize("Dash Workers:");

		var TextBox3Loca = CHelper.ControlX(Label3Size, Label3Loca, Label3Loca.Y, 0);
		var TextBox3Size = new Size(55, 20);

		var Label4Loca = CHelper.ControlX(TextBox3Size, TextBox3Loca, Label3Loca.Y);
		var Label4Size = CHelper.GetFontSize("Max Connections:");

		var TextBox4Loca = CHelper.ControlX(Label4Size, Label4Loca, Label3Loca.Y, 0);
		var TextBox4Size = CHelper.TextBoxSize(Label4Size, Label4Loca);

		var Label5Loca = new Point(0, Label3Size.Height + Label3Loca.Y + 10);
		var Label5Size = CHelper.GetFontSize("Content-Length:");

		var TextBox5Loca = CHelper.ControlX(Label5Size, Label5Loca, Label5Loca.Y, 0);
		var TextBox5Size = new Size(70, 20);

		var Label6Loca = CHelper.ControlX(TextBox5Size, TextBox5Loca, Label5Loca.Y);
		var Label6Size = CHelper.GetFontSize("Method:");

		var Label7Loca = CHelper.ControlX(Label6Size, Label6Loca, Label5Loca.Y, 0);
		var Label7Size = CHelper.TextBoxSize(Label6Size, Label6Loca);

		CHelper.TextBoxBCol = S1Container1.Parent.BackColor;
		CHelper.TextBoxFCol = Color.White;

		CHelper.AddTextBox(S2Textbox1, TextBox1Size, TextBox1Loca, ("500"));
		CHelper.AddTextBox(S2Textbox2, TextBox2Size, TextBox2Loca, ("500"));
		CHelper.AddTextBox(S2Textbox3, TextBox3Size, TextBox3Loca, ("8"));
		CHelper.AddTextBox(S2Textbox4, TextBox4Size, TextBox4Loca, ("500"));
		CHelper.AddTextBox(S2Textbox5, TextBox5Size, TextBox5Loca, ("4012"));

		CHelper.LabelBCol = S2Container1.BackColor;
		CHelper.LabelFCol = Color.White;

		CHelper.AddLabel(S2Label1, Label1Size, Label1Loca, ("Send Delay:"));
		CHelper.AddLabel(S2Label2, Label2Size, Label2Loca, ("Timeout:"));
		CHelper.AddLabel(S2Label3, Label3Size, Label3Loca, ("Dash Workers:"));
		CHelper.AddLabel(S2Label4, Label4Size, Label4Loca, ("Max Connections:"));
		CHelper.AddLabel(S2Label5, Label5Size, Label5Loca, ("Content-Length:"));
		CHelper.AddLabel(S2Label6, Label6Size, Label6Loca, ("Method:"));
		CHelper.AddLabel(S2Label7, Label7Size, Label7Loca, ("--- (POST) ---"), 7);

		S2Label7.TextAlign = ContentAlignment.MiddleCenter;
		S2Label7.BackColor = (S2Textbox1.BackColor);

		foreach (Control Control in S2Container1.Controls)
		{ 
		    if (Control is PictureBox)
		    {
			Tool.Round(Control, 6);
		    }
		}

		S2SetupDropDownMenu();

		var Label8Loca = new Point(0, TextBox5Loca.Y + TextBox5Size.Height + 10);
		var Label8Size = CHelper.GetFontSize("Random-UA:");

		CHelper.AddLabel(S2Label8, Label8Size, Label8Loca, ("Random-UA:"));

		var CheckBoxSize = new Size(16, 16);
		var CheckBoxLoca = CHelper.ControlX(Label8Size, Label8Loca, Label8Loca.Y + 2, 0);

		Control.CheckBox(S2Container1, S2Container2, S2Container3, CheckBoxSize, CheckBoxLoca, Capsule.BackColor, Color.DarkMagenta, true);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly TextBox S3TextBox1 = new TextBox();
	private readonly TextBox S3TextBox2 = new TextBox();

	private readonly List<PictureBox> S3ContainerCollection = new List<PictureBox>();

	private readonly Button S3Selector1 = new Button();
	private readonly Button S3Selector2 = new Button();

	private void S3AddTextBox(TextBox TextBox, Button Button, Size Size, Point Loca, string Text, int FontHeight = 8)
	{
	    try
	    {
		PictureBox Container = new PictureBox();

		var ContainerSize = new Size(Size.Width + 20, Size.Height);
		var BCol = S1Container1.Parent.BackColor;

		Control.Image(S3Container1, Container, ContainerSize, Loca, Color.White);

		TextBox.TextAlign = HorizontalAlignment.Center;
		TextBox.Text = (Text);

		Control.TextBox(Container, TextBox, Size, new Point(), BCol, Color.White, 1, FontHeight);

		var ButtonLoca = new Point(ContainerSize.Width - 20, 0);
		var ButtonSize = new Size(20, 20);

		Control.Button(Container, Button, ButtonSize, ButtonLoca, Color.DarkMagenta, Color.White, 1, FontHeight, ("^"));

		S3ContainerCollection.Add(Container);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private bool isInitialized = false;

	public void Initialize(PictureBox Capsule, DashWindow DashWindow, InitThaDashlorisX Parent)
	{
	    try
	    {
		if (!isInitialized)
		{
		    InitS1(Capsule, DashWindow);
		    InitS2(Capsule);

		    Hide();

		    isInitialized = true;
		}
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