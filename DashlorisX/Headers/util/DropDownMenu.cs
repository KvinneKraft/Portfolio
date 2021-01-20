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

using DashlorisX.Properties;

namespace DashlorisX
{
    public class DropDownMenu
    {
	new readonly DashControls Controls = new DashControls();
	readonly DashTools Tools = new DashTools();

	public void Hide()
	{
	    try
	    {
		Container.Hide();
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	public void Show()
	{
	    try
	    {
		Container.Show();
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly public PictureBox ContentContainer = new PictureBox();
	readonly public PictureBox Container = new PictureBox();

	private Color ItemColor = Color.Empty;

	public void SetupMenu(Control Top, Point MenuLocation, Color ItemColor, Color MenuColor, Color MenuBorderColor)
	{
	    try
	    {
		var MContainerSize = new Size(100, 0);
		var MContainerLocation = MenuLocation;
		var MContainerBackColor = MenuColor;

		try
		{
		    Controls.Image(Top, Container, MContainerSize, MContainerLocation, null, MContainerBackColor);
		}

		catch (Exception E)
		{
		    throw (E);
		}

		var CContainerSize = new Size(MContainerSize.Width - 4, MContainerSize.Height - 4);
		var CContainerLocation = new Point(2, 2);
		var CContainerBackColor = MenuColor;

		try
		{
		    Controls.Image(Container, ContentContainer, CContainerSize, CContainerLocation, null, CContainerBackColor);
		}

		catch (Exception E)
		{
		    throw (E);
		}

		var CRectangleSize = new Size(MContainerSize.Width - 2, MContainerSize.Height - 2);
		var CRectangleLocation = new Point(1, 1);
		var CRectangleBackColor = MenuBorderColor;

		try
		{
		    Tools.PaintRectangle(Container, 2, CRectangleSize, CRectangleLocation, CRectangleBackColor);
		}

		catch (Exception E)
		{
		    throw (E);
		}

		this.ItemColor = ItemColor;

		Top.MouseEnter += (s, e) =>
		{
		    if (Container.Visible)
		    {
			Hide();
		    }
		};

		Hide();
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }
	}

	readonly public Dictionary<int, Control> MenuItems = new Dictionary<int, Control>();

	// Check for size, if item size greater, resize!
    }

    public class DropdownMenu : Form
    {
	new readonly DashControls Controls = new DashControls();

	readonly DropDownMenu DropMenu = new DropDownMenu();
	readonly DashTools Tools = new DashTools();

	public DropdownMenu()
	{
	    Label HOVER = new Label();

	    Controls.Label(this, HOVER, Tools.GetFontSize("Yessir", 10), new Point(50, 50), Color.Black, Color.White, 1, 10, "YESSIR");

	    DropMenu.SetupMenu(this, new Point(HOVER.Left, HOVER.Top + HOVER.Height), Color.FromArgb(8, 8, 8), Color.FromArgb(16, 16, 16), Color.FromArgb(8, 8, 8));

	    HOVER.MouseHover += (s, e) =>
	    {
		DropMenu.Show();
	    };
	}
    }
}
