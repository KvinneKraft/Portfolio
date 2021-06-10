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

		    Parent.InitializeWindow(diagSize, diagTitle, diagBCol, barBCol, roundRadius: 0, barClose: false);
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


	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    // Bottom Section
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


	    public void Initiate(DashWindow Parent, DashWindow Inst)
	    {
		try
		{
		    // Middle Section
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