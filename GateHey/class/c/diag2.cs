// Author: Dashie
// Version: 1.0
// Dash Shell

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

using DashFramework.Erroring;
using DashFramework.Dialog;


namespace GateHey
{
    public class Dialog2
    {
	class InitiateTop
	{
	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();


	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    Color barBCol = Inst.values.getBarColor();
		    Size diagSize = new Size(425, 285);
		    Color diagBCol = Inst.BackColor;

		    string diagTitle = ("Dash - Shell");

		    Parent.InitializeWindow(diagSize, diagTitle, diagBCol, barBCol, roundRadius: 0);
		    Parent.values.setTitleLocation(new Point(10, -2));
		    Parent.values.HideIcons();
		    Parent.ShowAsIs(false);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	class InitiateBottom
	{
	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();


	    readonly DashPanel Panel = new DashPanel();
	    readonly TextBox TxtBox = new TextBox();
	    readonly Label Lbl = new Label();

	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{

		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	class InitiateMiddle
	{
	    readonly DashControls Controls = new DashControls();
	    readonly DashTools Tools = new DashTools();


	    public class DashShell
	    {
		public readonly TextBox TerminalLog = new TextBox();

		public void setDefaultText()
		{
		    try
		    {
			TerminalLog.Text = string.Format(
			    $">>> Hey there {Environment.UserName} !\r\n" +
			    ">>> Type 'help' for help, thank you for using this."
			);
		    }

		    catch (Exception E)
		    {
			throw (ErrorHandler.GetException(E));
		    }
		}
	    }


	    readonly CustomScrollBar DashBar = new CustomScrollBar();
	    readonly DashShell Shell = new DashShell();
	    readonly DashPanel Panel = new DashPanel();

	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    var PanelSize = new Size(Parent.Width - 4, Parent.Height - 56);
		    var PanelLoca = new Point(2, 26);
		    var PanelBCol = Color.FromArgb(22, 29, 36);

		    var TextBoxSize = new Size(PanelSize.Width - 10, PanelSize.Height - 10);
		    var TextBoxLoca = new Point(5, 5);
		    var TextBoxFCol = Color.White;
		    var TextBoxBCol = PanelBCol;

		    Controls.TextBox(Panel, Shell.TerminalLog, TextBoxSize, TextBoxLoca, TextBoxBCol, TextBoxFCol, 0, 9, true, true, FixedSize: false);
		    Controls.Panel(Parent, Panel, PanelSize, PanelLoca, PanelBCol);

		    Shell.setDefaultText();
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }
	}


	readonly InitiateBottom InitiateB = new InitiateBottom();
	readonly InitiateMiddle InitiateM = new InitiateMiddle();
	readonly InitiateTop InitiateT = new InitiateTop();

	readonly DashWindow Parent = new DashWindow();

	public void Initiator(DashWindow inst)
	{
	    try
	    {
		InitiateT.Initiate(Parent, inst);
		InitiateB.Initiate(Parent, inst);
		InitiateM.Initiate(Parent, inst);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.GetException(E);
	    }
	}


	public void Show() => Parent.Show();
	public void Hide() => Parent.Hide();
    }
}