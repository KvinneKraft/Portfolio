﻿// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MetaEditorX
{
    public class InitMetaEditorX
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	private Exception GetFormat(Exception E) =>
	    ErrorHandler.GetException(E);

	public readonly DashDialog DashDialog = new DashDialog();

	public void Component()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(0, 27, 56);
		var AppBColor = Color.FromArgb(22, 61, 105);
		var AppSize = new Size(275, 215);

		DashDialog.JustInitialize(AppSize, "Meta Editor-X", AppBColor, MenuBarBColor, CloseHideApp:false);
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly PictureBox MetaContainer = new PictureBox();
	
	public void Container()
	{
	    try
	    {
		var ContainerSize = new Size(DashDialog.Width - 20, DashDialog.Height - 51 - DashDialog.MenuBar.Bar.Height);
		var ContainerLoca = new Point(10, 10 + DashDialog.MenuBar.Bar.Height);
		var ContainerBColor = Color.FromArgb(13, 34, 56);

		Control.Image(DashDialog, MetaContainer, ContainerSize, ContainerLoca, ContainerBColor);

		Tool.Round(MetaContainer, 8);

		InnerContainer();
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly Form MetaInnerContainer = new Form();

	public void InnerContainer()
	{
	    try
	    {
		MetaInnerContainer.Size = new Size(MetaContainer.Width - 3, MetaContainer.Height - 4);
		MetaInnerContainer.Location = new Point(2, 2);

		Tool.Round(MetaInnerContainer, 8);

		MetaInnerContainer.FormBorderStyle = FormBorderStyle.None;
		MetaInnerContainer.BackColor = MetaContainer.BackColor;

		MetaInnerContainer.TopLevel = false;
		MetaInnerContainer.Visible = true;

		MetaContainer.Controls.Add(MetaInnerContainer);
		
		AddMetaFields();
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	private readonly Dictionary<int, TextBox> TextBoxValues = new Dictionary<int, TextBox>();
	private readonly Dictionary<int, Label> LabelValues = new Dictionary<int, Label>();

	private readonly List<string> MetaType = new List<string>()
	{
	    "Creation Time", "Creation Time UTC", "Last Access Time",
	    "Last Access Time UTC", "Is Read Only",
	};

	private int GetMetaID(string type)
	{
	    return MetaType.IndexOf(type);
	}

	private Point GetLabelLocation(int index)
	{
	    Point Location = new Point(5, 8);

	    if (MetaInnerContainer.Controls.Count > 0)
	    {
		Control Control = MetaInnerContainer.Controls[MetaInnerContainer.Controls.Count - 2];
		Location.Y = Control.Height + Control.Top + 10;
	    }

	    return Location;
	}

	private void AddMetaRow(int index)
	{
	    var Label = new Label();

	    var LabelText = $"{MetaType[index]}:";
	    var LabelSize = Tool.GetFontSize(LabelText, 9);
	    var LabelLoca = GetLabelLocation(index);
	    var LabelBColor = MetaInnerContainer.BackColor;

	    try
	    {
		Control.Label(MetaInnerContainer, Label, LabelSize, LabelLoca, LabelBColor, Color.White, 1, 9, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }

	    var TextBox = new TextBox()
	    {
		Text = "Awaiting File ....",
	    };
	    
	    var TextBoxSize = new Size(MetaInnerContainer.Width - Label.Left - Label.Width - 10, Label.Height + 6);
	    var TextBoxLoca = new Point(Label.Left + Label.Width + 2, Label.Top - 2);
	    var TextBoxBColor = DashDialog.MenuBar.Bar.BackColor;

	    try
	    {
		Control.TextBox(MetaInnerContainer, TextBox, TextBoxSize, TextBoxLoca, TextBoxBColor, Color.FromArgb(200, 200, 200), 1, 9);
		Tool.Round(MetaInnerContainer.Controls[MetaInnerContainer.Controls.Count - 1], 6);
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }

	    TextBoxValues.Add(index, TextBox);
	    LabelValues.Add(index, Label);
	}

	public void AddMetaFields()
	{
	    try
	    {
		for (int k = 0; k < MetaType.Count; k += 1)
		{
		    AddMetaRow(k);
		}
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly PictureBox MetaBarContainer = new PictureBox();
	public readonly PictureBox ButtonContainer = new PictureBox();

	public readonly Button Save = new Button();
	public readonly Button Load = new Button();
	public readonly Button Misc = new Button();

	public void BottomBar()
	{
	    try
	    {
		var MetaBarSize = new Size(DashDialog.Width, 30);
		var MetaBarLoca = new Point(0, DashDialog.Height - MetaBarSize.Height);
		var MetaBarBColor = DashDialog.MenuBar.Bar.BackColor;

		Control.Image(DashDialog, MetaBarContainer, MetaBarSize, MetaBarLoca, MetaBarBColor);

		var ContainerSize = new Size(230, 24);
		var ContainerLoca = new Point((MetaBarContainer.Width - ContainerSize.Width) / 2, (MetaBarContainer.Height - ContainerSize.Height) / 2);
		var ContainerBColor = MetaBarBColor;

		Control.Image(MetaBarContainer, ButtonContainer, ContainerSize, ContainerLoca, ContainerBColor);

		var ButtonSize = new Size(70, 24);
		var ButtonBColor = MetaBarBColor;
		var ButtonFColor = Color.White;

		var MiscLoca = new Point(140 + 20, 0);
		var SaveLoca = new Point(70 + 10, 0);
		var LoadLoca = new Point(0, 0);
		
		Control.Button(ButtonContainer, Misc, ButtonSize, MiscLoca, ButtonBColor, ButtonFColor, 1, 8, "Misc");
		Control.Button(ButtonContainer, Load, ButtonSize, LoadLoca, ButtonBColor, ButtonFColor, 1, 8, "Load");
		Control.Button(ButtonContainer, Save, ButtonSize, SaveLoca, ButtonBColor, ButtonFColor, 1, 8, "Save");

		foreach (Button Button in ButtonContainer.Controls)
		{
		    Tool.Round(Button, 6);
		}

		Tool.Round(ButtonContainer, 6);
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}
    }
}
