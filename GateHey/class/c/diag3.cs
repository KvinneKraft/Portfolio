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

using DashFramework.Erroring;
using DashFramework.Dialog;


namespace GateHey
{
    public class Dialog3
    {
	public readonly static DashControls Controls = new DashControls();
	public readonly static DashTools Tools = new DashTools();


	public class InitiateTop
	{
	    readonly Panel Panel = new Panel();
	    readonly Label Label = new Label();

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


	public class InitiateBottom
	{
	    readonly Button Bttn1 = new Button();
	    readonly Button Bttn2 = new Button();

	    readonly Panel Panel1 = new Panel();
	    readonly Panel Panel2 = new Panel();

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


	public class InitiateMiddle
	{
	    readonly TextBox TxtBox = new TextBox();
	    readonly Panel Panel = new Panel();

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


	readonly InitiateBottom InitiateB = new InitiateBottom();
	readonly InitiateMiddle InitiateM = new InitiateMiddle();
	readonly InitiateTop InitiateT = new InitiateTop();

	readonly DashWindow Parent = new DashWindow();

	public void Initiator(DashWindow inst)
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}


	public void Show() => Parent.Show();
	public void Hide() => Parent.Hide();
    }
}
