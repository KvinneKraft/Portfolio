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
	readonly DashControls Controls = new DashControls();
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

	public void SetupMenu(Control Top, Point MenuLocation, Color MenuColor, Color MenuBorderColor)
	{
	    var MContainerSize = new Size(10, 0);
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
	    var CContainerBackColor = Color.White;//MenuColor;

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

	    Top.MouseEnter += (s, e) =>
	    {
		if (Container.Visible)
		{
		    Hide();
		}
	    };

	    Hide();
	}

	readonly public Dictionary<int, Label> MenuItems = new Dictionary<int, Label>();
	
	private int GetY()
	{
	    if (MenuItems.Count > 0)
	    {
		var Item = ContentContainer.Controls[ContentContainer.Controls.Count - 1];
		return (Item.Top + Item.Height);
	    }

	    return 0;
	}

	public void AddItem(Label Object, string ItemName, Color ItemBColor, Color ItemFColor, int Index = -2, int ItemWidth = -2, int ItemHeight = -2, int ItemTextSize = 10)
	{
	    if (ItemWidth == -2 || ItemHeight == -2)
	    {
		if (ItemWidth == -2)
		{
		    ItemWidth = ContentContainer.Width;
		}

		if (ItemHeight == -2)
		{
		    ItemHeight = Tools.GetFontSize(ItemName, ItemTextSize).Height + 6;
		}
	    }

	    var ItemSize = new Size(ItemWidth, ItemHeight);
	    var ItemLocation = new Point(0, GetY());
	    var ItemBackColor = ItemBColor;
	    var ItemForeColor = ItemFColor;

	    try
	    {
		Controls.Label(ContentContainer, Object, ItemSize, ItemLocation, ItemBackColor, ItemForeColor, 1, ItemTextSize, ItemName);
		Object.TextAlign = ContentAlignment.TopCenter;
	    }

	    catch (Exception E)
	    {
		throw (E);
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
		Tools.Resize(ContentContainer, CContainerSize);
		Tools.Resize(Container, MContainerSize);
	    }

	    catch (Exception E)
	    {
		throw (E);
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
		throw (E);
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
		throw (E);
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
		throw (E);
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
		Tools.Resize(ContentContainer, CContainerSize);
		Tools.Resize(Container, MContainerSize);
	    }

	    catch (Exception E)
	    {
		throw (E);
	    }

	    return true;
	}
    }
}
