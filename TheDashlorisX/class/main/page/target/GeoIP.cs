
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
    public class GEOIP
    {
	public readonly DashControls Control = new DashControls();
	public readonly DashTools Tool = new DashTools();


	private readonly ControlHelper CHelper = new ControlHelper();

	private readonly TextBox S1TextBox1 = new TextBox();
	private readonly TextBox S1TextBox2 = new TextBox();
	private readonly TextBox S1TextBox3 = new TextBox();
	private readonly TextBox S1TextBox4 = new TextBox();
	private readonly TextBox S1TextBox5 = new TextBox();
	private readonly TextBox S1TextBox6 = new TextBox();

	private readonly Button S1Button1 = new Button();

	private readonly Label S1Label2 = new Label();
	private readonly Label S1Label3 = new Label();
	private readonly Label S1Label4 = new Label();
	private readonly Label S1Label5 = new Label();
	private readonly Label S1Label6 = new Label();
	private readonly Label S1Label7 = new Label();

	private void Init1S2(PictureBox Capsule, InitThaDashlorisX Parent)
	{
	    try
	    {
		//Parent = S1Container3|Country, City, Address, Postal Code, Zip, TimeZone
		var Label1Size = CHelper.GetFontSize("Country:");
		var Label1Loca = new Point(0, 0);

		var TextBox1Size = new Size(130, 20);
		var TextBox1Loca = CHelper.ControlX(Label1Size, Label1Loca, Extra: 0);

		var Label2Size = CHelper.GetFontSize("City:");
		var Label2Loca = CHelper.ControlX(TextBox1Size, TextBox1Loca);

		var TextBox2Size = CHelper.TextBoxSize(Label2Size, Label2Loca);
		var TextBox2Loca = CHelper.ControlX(Label2Size, Label2Loca, Extra: 0);

		var Label3Size = CHelper.GetFontSize("Address:");
		var Label3Loca = new Point(0, 30);

		var TextBox3Size = new Size(100, 20);
		var TextBox3Loca = CHelper.ControlX(Label3Size, Label3Loca, Extra: 0);

		var Label4Size = CHelper.GetFontSize("Postal Code:");
		var Label4Loca = CHelper.ControlX(TextBox3Size, TextBox3Loca);

		var TextBox4Size = CHelper.TextBoxSize(Label4Size, Label4Loca);
		var TextBox4Loca = CHelper.ControlX(Label4Size, Label4Loca, Extra: 0);

		var Label5Size = CHelper.GetFontSize("Zip Code:");
		var Label5Loca = new Point(0, 60);

		var TextBox5Size = new Size(110, 20);
		var TextBox5Loca = CHelper.ControlX(Label5Size, Label5Loca, Extra: 0);

		var Label6Size = CHelper.GetFontSize("Time Zone:");
		var Label6Loca = CHelper.ControlX(TextBox5Size, TextBox5Loca);

		var TextBox6Size = CHelper.TextBoxSize(Label6Size, Label6Loca);
		var TextBox6Loca = CHelper.ControlX(Label6Size, Label6Loca, Extra: 0);

		CHelper.AddTextBox(S1TextBox1, TextBox1Size, TextBox1Loca, ("Loading ...."));
		CHelper.AddTextBox(S1TextBox2, TextBox2Size, TextBox2Loca, ("Loading ...."));
		CHelper.AddTextBox(S1TextBox3, TextBox3Size, TextBox3Loca, ("Loading ...."));
		CHelper.AddTextBox(S1TextBox4, TextBox4Size, TextBox4Loca, ("Loading ...."));
		CHelper.AddTextBox(S1TextBox5, TextBox5Size, TextBox5Loca, ("Loading ...."));
		CHelper.AddTextBox(S1TextBox6, TextBox6Size, TextBox6Loca, ("Loading ...."));

		foreach (Control TextBox in S1Container3.Controls)
		{
		    if (TextBox is TextBox)
		    {
			((TextBox)TextBox).TextAlign = HorizontalAlignment.Center;
		    }
		}

		CHelper.AddLabel(S1Label2, Label1Size, Label1Loca, ("Country:"));
		CHelper.AddLabel(S1Label3, Label2Size, Label2Loca, ("City:"));
		CHelper.AddLabel(S1Label4, Label3Size, Label3Loca, ("Address:"));
		CHelper.AddLabel(S1Label5, Label4Size, Label4Loca, ("Postal Code:"));
		CHelper.AddLabel(S1Label6, Label5Size, Label5Loca, ("Zip Code:"));
		CHelper.AddLabel(S1Label7, Label6Size, Label6Loca, ("Time Zone:"));

		var ButtonSize = new Size(100, 20);
		var ButtonLoca = new Point((S1Container3.Width - 100) / 2, 90);

		Control.Button(S1Container3, S1Button1, ButtonSize, ButtonLoca, S1Container1.BackColor, Color.White, 1, 8, "Go Back");

		S1Button1.Click += (s, e) =>
		{
		    foreach (Control Control in Capsule.Controls)
		    {
			if (Control.Visible)
			{
			    Control.Hide();
			}
		    }

		    Parent.S3Class1.Show(Parent);
		};

		Tool.Round(S1Button1, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S1Container1 = new PictureBox();
	private readonly PictureBox S1Container2 = new PictureBox();
	private readonly PictureBox S1Container3 = new PictureBox();

	private readonly Label S1Label1 = new Label();

	private void Init1(PictureBox Capsule, DashWindow DashWindow, InitThaDashlorisX Parent)
	{
	    try
	    {
		var Container1Size = new Size(Capsule.Width, 165);
		var Container1Loca = new Point(0, (Capsule.Height - 185) / 2);
		var Container1BCol = Capsule.BackColor;

		var Container2Size = new Size(Container1Size.Width, Container1Size.Height - 35);
		var Container2Loca = new Point(0, 35);
		var Container2BCol = DashWindow.MenuBar.MenuBar.BackColor;

		var Container3Size = new Size(Container2Size.Width - 20, Container2Size.Height - 20);
		var Container3Loca = new Point(10, 10);

		Control.Image(S1Container1, S1Container2, Container2Size, Container2Loca, Container2BCol);
		Control.Image(S1Container2, S1Container3, Container3Size, Container3Loca, Container2BCol);
		Control.Image(Capsule, S1Container1, Container1Size, Container1Loca, Container1BCol);

		Tool.Round(S1Container2, 6);

		var Label1Size = CHelper.GetFontSize("GEOIP Results", 16);
		var Label1Loca = new Point(10, 0);

		Control.Label(S1Container1, S1Label1, Label1Size, Label1Loca, Container1BCol, Color.White, 1, 16, ("GEOIP Results"));

		CHelper.TextBoxBCol = Capsule.BackColor;
		CHelper.LabelBCol = Container2BCol;

		CHelper.TextBoxParent = S1Container3;
		CHelper.LabelParent = S1Container3;

		CHelper.TextBoxFCol = Color.White;
		CHelper.LabelFCol = Color.White;

		Init1S2(Capsule, Parent);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private void RefreshIPData(InitThaDashlorisX Parent)
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private bool needInit = true;

	public void Show(PictureBox Capsule, DashWindow DashWindow, InitThaDashlorisX Parent)
	{
	    try
	    {
		if (needInit)
		{
		    Init1(Capsule, DashWindow, Parent);

		    needInit = false;
		}
		
		Parent.HideContainers();

		RefreshIPData(Parent);

		S1Container1.Show();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
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