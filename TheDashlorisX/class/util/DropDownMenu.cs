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
using System.Windows.Forms;
using System.Collections.Generic;

using TheDashlorisX.Properties;

namespace TheDashlorisXd
{
    public class DropDownMenu
    {
	readonly DashControls Control = new DashControls();
	readonly DashTools Tool = new DashTools();

	public void Hide()
	{
	    try
	    {
		Container.Hide();
		Container.SendToBack();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Show()
	{
	    try
	    {
		Container.Show();
		Container.BringToFront();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public readonly PictureBox ContentContainer = new PictureBox();
	public readonly PictureBox Container = new PictureBox();

	public void SetupMenu(Control Top, Point MenuLocation, Color MenuColor, Color MenuBorderColor)
	{
	    var MContainerSize = new Size(10, 0);
	    var MContainerLocation = MenuLocation;
	    var MContainerBackColor = MenuColor;

	    try
	    {
		Control.Image(Top, Container, MContainerSize, MContainerLocation, MContainerBackColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var CContainerSize = new Size(MContainerSize.Width - 4, MContainerSize.Height - 4);
	    var CContainerLocation = new Point(2, 2);
	    var CContainerBackColor = Color.White;//MenuColor;

	    try
	    {
		Control.Image(Container, ContentContainer, CContainerSize, CContainerLocation, CContainerBackColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var CRectangleSize = new Size(MContainerSize.Width - 2, MContainerSize.Height - 2);
	    var CRectangleLocation = new Point(1, 1);
	    var CRectangleBackColor = MenuBorderColor;

	    try
	    {
		Tool.PaintRectangle(Container, 2, CRectangleSize, CRectangleLocation, CRectangleBackColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    Top.MouseEnter += (s, e) =>
	    {
		if (Container.Visible)
		{
		    Hide();
		}
	    };

	    Hide();
	}

	public readonly Dictionary<int, Label> MenuItems = new Dictionary<int, Label>();
	
	private int GetY()
	{
	    if (MenuItems.Count > 0)
	    {
		var Item = ContentContainer.Controls[ContentContainer.Controls.Count - 1];
		return (Item.Top + Item.Height);
	    }

	    return 0;
	}

	public void AddItem(Label Object, string ItemName, Color ItemBCol, Color ItemFCol, int Index = -2, int ItemWidth = -2, int ItemHeight = -2, int ItemTextSize = 10)
	{
	    if (ItemWidth == -2 || ItemHeight == -2)
	    {
		if (ItemWidth == -2)
		{
		    ItemWidth = ContentContainer.Width;
		}

		if (ItemHeight == -2)
		{
		    ItemHeight = Tool.GetFontSize(ItemName, ItemTextSize).Height + 6;
		}
	    }

	    var ItemSize = new Size(ItemWidth, ItemHeight);
	    var ItemLocation = new Point(0, GetY());
	    var ItemBackColor = ItemBCol;
	    var ItemForeColor = ItemFCol;

	    try
	    {
		Control.Label(ContentContainer, Object, ItemSize, ItemLocation, ItemBackColor, ItemForeColor, 1, ItemTextSize, ItemName);
		Object.TextAlign = ContentAlignment.TopCenter;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    int GetContentContainerWidth()
	    {
		if (ItemSize.Width > ContentContainer.Width)
		{
		    return (ItemSize.Width);
		}

		return (ContentContainer.Width);
	    }

	    var CContainerSize = new Size(GetContentContainerWidth(), ContentContainer.Height + ItemHeight);
	    var MContainerSize = new Size(CContainerSize.Width + 4, CContainerSize.Height + 4);

	    try
	    {
		Tool.Resize(ContentContainer, CContainerSize);
		Tool.Resize(Container, MContainerSize);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    if (Index == -2)
	    {
		Index = MenuItems.Count;
	    }

	    MenuItems.Add(Index, Object);
	}

	public Label GetItem(int Index = -2)
	{
	    try
	    {
		if (Index == -2)
		{
		    Index = MenuItems.Count - 1;
		}

		if (MenuItems.Count - 1 < Index)
		{
		    return null;
		}

		return MenuItems[Index];
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void ReloadDropDownMenu()
	{
	    try
	    {
		var ItemLocation = new Point(0, 0);
		var MenuItems = new List<Label>();

		foreach (Label MenuItem in this.MenuItems.Values)
		{
		    MenuItem.Location = ItemLocation;
		    
		    ItemLocation.Y += MenuItem.Height;

		    MenuItems.Add(MenuItem);
		}

		this.MenuItems.Clear();

		for (int k = 0; k < MenuItems.Count; k += 1)
		{
		    this.MenuItems.Add(k, MenuItems[k]);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public bool RemoveItem(int Index = -2)
	{
	    try
	    {
		if (Index == -2)
		{
		    var Count = MenuItems.Count;

		    if (Count >= 1)
		    {
			Count -= 1;
		    }

		    Index = Count;
		}

		if (!MenuItems.ContainsKey(Index))
		{
		    return false;
		}

		ContentContainer.Controls.RemoveAt(Index);
		MenuItems.Remove(Index);

		ReloadDropDownMenu();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    int GetMenuHeight()
	    {
		var Height = 0;

		if (MenuItems.Count > 0)
		{
		    Height = MenuItems[MenuItems.Count - 1].Top + MenuItems[MenuItems.Count - 1].Height - 4;
		}

		return Height;
	    }

	    var CContainerSize = new Size(ContentContainer.Width, GetMenuHeight());
	    var MContainerSize = new Size(CContainerSize.Width + 4, CContainerSize.Height + 4);

	    try
	    {
		Tool.Resize(ContentContainer, CContainerSize);
		Tool.Resize(Container, MContainerSize);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    return true;
	}
    }
}
