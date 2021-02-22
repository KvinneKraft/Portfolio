﻿
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

namespace TheDashlorisX
{
    public class InitThaDashlorisX
    {
	public readonly DashDialog DashDialog = new DashDialog();

	public void InitMainComponent()
	{
	    try
	    {	    
		Size ApplicationSize = new Size(425, 350);

		Color ApplicationBCol = Color.FromArgb(36, 1, 112);
		Color MenuBarBCol = Color.FromArgb(14, 0, 57);

		DashDialog.JustInitialize(ApplicationSize, ("Tha Dashloris-X  -  3.0"), ApplicationBCol, MenuBarBCol, CloseHideApp:false);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	private readonly PictureBox BottomBarContainer1 = new PictureBox();
	private readonly Label BottomBarLabel1 = new Label();

	public void InitBottomBarComponent()
	{
	    try
	    {
		var ContainerSize = new Size(DashDialog.Width, 28);
		var ContainerLoca = new Point(0, DashDialog.Height - 28);
		var ContainerBCol = DashDialog.MenuBar.Bar.BackColor;

		Control.Image(DashDialog, BottomBarContainer1, ContainerSize, ContainerLoca, ContainerBCol);

		var LabelText = ($"All Rights Reserved (c) Dashies Softwaries {DateTime.Today.Year}");
		var LabelSize = Tool.GetFontSize(LabelText, 6);
		var LabelLoca = new Point((ContainerSize.Width - LabelSize.Width + 100) / 2, (ContainerSize.Height - LabelSize.Height) / 2);
		var LabelBCol = ContainerBCol;
		var LabelFCol = Color.White;

		Control.Label(BottomBarContainer1, BottomBarLabel1, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, 6, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox SideBarContainer1 = new PictureBox();
	private readonly PictureBox SideBarContainer2 = new PictureBox();
	private readonly PictureBox SideBarContainer3 = new PictureBox();

	private readonly Button SideBarButton1 = new Button();
	private readonly Button SideBarButton2 = new Button();
	private readonly Button SideBarButton3 = new Button();
	private readonly Button SideBarButton4 = new Button();
	private readonly Button SideBarButton5 = new Button();

	private void RoundControls(Control.ControlCollection Controls)
	{
	    try
	    {
		foreach (Control Control in Controls)
		{
		    Tool.Round(Control, 6);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void InitSideBarComponent()
	{
	    try
	    {
		var ContainerBCol = DashDialog.MenuBar.Bar.BackColor;

		var Container1Size = new Size(100, DashDialog.Height - DashDialog.MenuBar.Bar.Height - BottomBarContainer1.Height);
		var Container1Loca = new Point(0, DashDialog.MenuBar.Bar.Height);

		var Container2Size = new Size(Container1Size.Width - 20, 136);
		var Container2Loca = new Point(10, 30);

		var Container3Size = new Size(Container2Size.Width, 26);
		var Container3Loca = new Point(10, Container1Size.Height - 36);

		Control.Image(SideBarContainer1, SideBarContainer3, Container3Size, Container3Loca, ContainerBCol);
		Control.Image(SideBarContainer1, SideBarContainer2, Container2Size, Container2Loca, ContainerBCol);
		Control.Image(DashDialog, SideBarContainer1, Container1Size, Container1Loca, ContainerBCol);

		var OptionSize = new Size(Container2Size.Width, 26);
		var OptionBCol = ContainerBCol;
		var OptionFCol = Color.White;

		int x = 0;

		var Option3Loca = new Point(x, OptionSize.Height * 2 + 16);
		var Option4Loca = new Point(x, OptionSize.Height * 3 + 24);
		var Option2Loca = new Point(x, OptionSize.Height + 8);
		var Option1Loca = new Point(x, 0);
		var Option5Loca = new Point(x, 0);

		Control.Button(SideBarContainer3, SideBarButton5, OptionSize, Option5Loca, OptionBCol, OptionFCol, 1, 9, "Launch");
		Control.Button(SideBarContainer2, SideBarButton1, OptionSize, Option1Loca, OptionBCol, OptionFCol, 1, 9, "Target");
		Control.Button(SideBarContainer2, SideBarButton2, OptionSize, Option2Loca, OptionBCol, OptionFCol, 1, 9, "Settings");
		Control.Button(SideBarContainer2, SideBarButton3, OptionSize, Option3Loca, OptionBCol, OptionFCol, 1, 9, "Is Online");
		Control.Button(SideBarContainer2, SideBarButton4, OptionSize, Option4Loca, OptionBCol, OptionFCol, 1, 9, "About");

		RoundControls(SideBarContainer2.Controls);
		RoundControls(SideBarContainer3.Controls);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly PictureBox CapsuleContainer1 = new PictureBox();

	private readonly Settings SettingsPanel = new Settings();
	private readonly IsOnline IsOnlinePanel = new IsOnline();
	private readonly Target TargetPanel = new Target();
	private readonly Launch LaunchPanel = new Launch();
	private readonly About AboutPanel = new About();

	public void InitContainerComponent()
	{
	    try
	    {
		var CapsuleSize = new Size(DashDialog.Width - SideBarContainer1.Width - 2, DashDialog.Height - DashDialog.MenuBar.Bar.Height - BottomBarContainer1.Height);
		var CapsuleLoca = new Point(SideBarContainer1.Width, DashDialog.MenuBar.Bar.Height);
		var CapsuleBCol = DashDialog.BackColor; //Color.FromArgb(2, 55, 110);

		Control.Image(DashDialog, CapsuleContainer1, CapsuleSize, CapsuleLoca, CapsuleBCol);

		//TargetPanel.Initialize(DashDialog, CapsuleContainer1);
		//SettingsPanel.Initialize(DashDialog, CapsuleContainer1);
		IsOnlinePanel.Initialize(DashDialog, CapsuleContainer1);
		//LaunchPanel.Initialize(DashDialog, CapsuleContainer1);
		//AboutPanel.Initialize(DashDialog, CapsuleContainer1);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void ReInitMenuBar()
	{
	    try
	    {
		DashDialog.MenuBar.SLogo.BackColor = DashDialog.MenuBar.Bar.BackColor;

		DashDialog.MenuBar.Close.Click += (s, e) =>
		{
		    Environment.Exit(-1);
		}; 
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}