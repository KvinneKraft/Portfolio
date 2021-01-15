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
using System.Windows.Forms;
using System.Collections.Generic;

namespace DashlorisX
{
    public partial class DashlorisX : Form
    {
	new public readonly DashControls Controls = new DashControls();
	public readonly DashTools Tools = new DashTools();

	public readonly DashMenuBar MenuBar = new DashMenuBar();

	private void InitializeMenuBar()
	{
	    try
	    {
		Color BAR_COLA = Color.FromArgb(8, 8, 8);
		MenuBar.Add(this, 26, BAR_COLA, BAR_COLA);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void ReinitializeComponent()
	{
	    try
	    {
		BackColor = Color.MidnightBlue;
		Tools.Round(this, 4);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox MainContainer = new PictureBox();

	readonly Label Duration_L = new Label();
	readonly Label Bytes_L = new Label();
	readonly Label Host_L = new Label();
	readonly Label Port_L = new Label();

	readonly TextBox Duration_T = new TextBox();
	readonly TextBox Bytes_T = new TextBox();
	readonly TextBox Host_T = new TextBox();
	readonly TextBox Port_T = new TextBox();

	private void InitializeMainField()
	{
	    try
	    {
		// 66 // 24 height
		var MCONTA_SIZE = new Size(Width - 20, 66);
		var MCONTA_LOCA = new Point(10, MenuBar.Bar.Height + 10);
		var MCONTA_COLA = Color.FromArgb(16, 16, 16);

		Controls.Image(this, MainContainer, MCONTA_SIZE, MCONTA_LOCA, null, MCONTA_COLA);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly PictureBox OptionContainer = new PictureBox();
	readonly PictureBox InnerOptionContainer = new PictureBox();

	readonly Button Properties = new Button();
	readonly Button Online = new Button();
	readonly Button Launch = new Button();
	readonly Button About = new Button();

	private void InitializeOptionField()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	private void InitializeInterfa()
	{
	    ReinitializeComponent();
	    InitializeMainField(); 
	    InitializeOptionField();
	}

	public DashlorisX()
	{
	    InitializeComponent();

	    try
	    {
		InitializeMenuBar();
		InitializeInterfa();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize($"{E.StackTrace}\r\n----------------------\r\n{E.Message}", "Error Handler");
	    }
	}
    }
}
