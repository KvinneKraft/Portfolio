// Author: Dashie
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
		var AppSize = new Size(350, 300);

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
		var ContainerLoca = new Point(10, 11 + DashDialog.MenuBar.Bar.Height);
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

		MetaInnerContainer.FormBorderStyle = FormBorderStyle.None;
		MetaInnerContainer.BackColor = MetaContainer.BackColor;

		MetaInnerContainer.VerticalScroll.Enabled = true;
		MetaInnerContainer.VerticalScroll.Visible = true;
		MetaInnerContainer.TopLevel = false;

		MetaContainer.Controls.Add(MetaInnerContainer);

		Tool.Round(MetaInnerContainer, 8);

		MetaInnerContainer.Show();
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly PictureBox MetaBarContainer = new PictureBox();
	public readonly PictureBox ButtonContainer = new PictureBox();

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

		var ButtonSize = new Size(70, 24);
		var ButtonBColor = MetaBarBColor;
		var ButtonFColor = Color.White;

		var MiscLoca = new Point(ButtonSize.Width + 10, 0);
		var LoadLoca = new Point(0, 0);

		Control.Button(ButtonContainer, Misc, ButtonSize, MiscLoca, ButtonBColor, ButtonFColor, 1, 8, "Misc");
		Control.Button(ButtonContainer, Load, ButtonSize, LoadLoca, ButtonBColor, ButtonFColor, 1, 8, "Load");

		var ContainerSize = new Size((ButtonSize.Width * ButtonContainer.Controls.Count) + (10 * (ButtonContainer.Controls.Count - 1)), Load.Height);
		var ContainerLoca = new Point((MetaBarContainer.Width - ContainerSize.Width) / 2, (MetaBarContainer.Height - ContainerSize.Height) / 2);
		var ContainerBColor = MetaBarBColor;

		Control.Image(MetaBarContainer, ButtonContainer, ContainerSize, ContainerLoca, ContainerBColor);

		Tool.Round(Load, 6);
		Tool.Round(Misc, 6);
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}
    }
}
