// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using DashlorisX.Properties;

namespace DashlorisX
{
    public class LogContainer : Form
    {
	readonly DashControls Control = new DashControls();
	readonly DashTools Tool = new DashTools();

	private void ColorApp(Color MenuBarBColor, Color ContainerBColor, Color AppBColor)
	{
	    try
	    {
		MenuBar.Recolor(MenuBarBColor);

		InnerTextContainer.BackColor = ContainerBColor;
		TextContainer.BackColor = ContainerBColor;
		TextBox.BackColor = ContainerBColor;

		BackColor = AppBColor;

		BottomContainer.BackColor = ContainerBColor;
		BottomClose.BackColor = ContainerBColor;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Show(string Message, string Title, Color MenuBarBColor, Color ContainerBColor, Color AppBColor)
	{
	    try
	    {
		ColorApp(MenuBarBColor, ContainerBColor, AppBColor);

		MenuBar.UpdateTitle(Title);

		TextBox.Text = Message;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    ShowDialog();
	}

	private void InitializeComponent(Size AppSize, string AppTitle, FormStartPosition AppStartPosition = FormStartPosition.CenterScreen)
	{
	    try
	    {
		SuspendLayout();

		FormBorderStyle = FormBorderStyle.None;
		StartPosition = AppStartPosition;

		MaximumSize = AppSize;
		MinimumSize = AppSize;

		Icon = Resources.ICON;

		Text = AppTitle;
		Name = AppTitle;

		ResumeLayout(false);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	readonly DashMenuBar MenuBar = new DashMenuBar(string.Empty, minim:false);

	private void InitializeMenuBar()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		MenuBar.Add(this, 26, MenuBarBColor, MenuBarBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	readonly PictureBox BottomContainer = new PictureBox();
	readonly Button BottomClose = new Button();

	private void InitializeBottomBar()
	{
	    var BContainerSize = new Size(Width, 26);
	    var BContainerLocation = new Point(0, Height - BContainerSize.Height);
	    var BContainerBColor = MenuBar.Bar.BackColor;

	    try
	    {
		Control.Image(this, BottomContainer, BContainerSize, BContainerLocation, BContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var ButtonSize = new Size(100, 26);
	    var ButtonLocation = new Point((BContainerSize.Width - ButtonSize.Width) / 2, 0);
	    var ButtonFColor = Color.White;
	    var ButtonBColor = MenuBar.Bar.BackColor;

	    try
	    {
		Control.Button(BottomContainer, BottomClose, ButtonSize, ButtonLocation, ButtonBColor, ButtonFColor, 1, 9, "Close");

		BottomClose.Click += (s, e) =>
		{
		    Hide();
		};
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	readonly PictureBox InnerTextContainer = new PictureBox();
	readonly PictureBox TextContainer = new PictureBox();

	readonly TextBox TextBox = new TextBox();

	private void InitializeContainer()
	{
	    var TContainerSize = new Size(Width - 20, Height - 20 - MenuBar.Bar.Height - BottomContainer.Height);
	    var TContainerLocation = new Point(10, MenuBar.Bar.Height + 10);
	    var TContainerBColor = Color.FromArgb(6, 17, 33);

	    var IContainerSize = new Size(TContainerSize.Width - 10, TContainerSize.Height - 10);
	    var IContainerLocation = new Point(5, 5);
	    var IContainerBColor = TContainerBColor;

	    try
	    {
		Control.Image(TextContainer, InnerTextContainer, IContainerSize, IContainerLocation, IContainerBColor);
		Control.Image(this, TextContainer, TContainerSize, TContainerLocation, TContainerBColor);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }

	    var TextBoxSize = IContainerSize;
	    var TextBoxLocation = new Point(0, 0);
	    var TextBoxBColor = TContainerBColor;
	    var TextBoxFColor = Color.White;

	    try
	    {
		Control.TextBox(InnerTextContainer, TextBox, TextBoxSize, TextBoxLocation, TextBoxBColor, TextBoxFColor, 1, 8, ReadOnly:true, Multiline:true, ScrollBar:true, FixedSize:false);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public LogContainer(Size AppSize, string AppTitle, FormStartPosition AppStartPosition = FormStartPosition.CenterScreen)
	{
	    try
	    {
		InitializeComponent(AppSize, AppTitle, AppStartPosition);

		InitializeMenuBar();
		InitializeBottomBar();
		InitializeContainer();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}
    }
}
