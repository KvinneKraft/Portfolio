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

using MetaEditorX.Properties;

namespace MetaEditorX
{
    public class LogContainer : Form
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	private void ColorApp(Color MenuBarBColor, Color ContainerBColor, Color AppBColor)
	{
	    try
	    {
		InnerTextContainer.BackColor = ContainerBColor;
		TextContainer.BackColor = ContainerBColor;
		TextBox.BackColor = ContainerBColor;

		BackColor = AppBColor;

		MenuBar.Recolor(MenuBarBColor);

		BottomContainer.BackColor = MenuBarBColor;
		BottomClose.BackColor = MenuBarBColor;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Send(string Message)
	{
	    MenuBar.Bar.Parent.Invoke
	    (
		new MethodInvoker
		(
		    delegate () 
		    {
			Visible = false;
		    }
		)
	    );
	}

	public void Show(string Message, string Title, Color MenuBarBColor, Color ContainerBColor, Color AppBColor, bool ShowDialog = true)
	{
	    try
	    {
		ColorApp(MenuBarBColor, ContainerBColor, AppBColor);

		MenuBar.UpdateTitle(Title);

		TextBox.Text = Message;

		if (ShowDialog)
		{
		    this.ShowDialog();
		}

		else
		{
		    Show();
		}
	    }

	    catch (Exception E)
	    {
		ErrorHandler.Utilize(ErrorHandler.GetFormat(E), "Error Handler");
	    }
	}

	private void InitializeComponent(Size AppSize, string AppTitle, FormStartPosition AppStartPosition = FormStartPosition.CenterScreen)
	{
	    try
	    {
		SuspendLayout();

		FormBorderStyle = FormBorderStyle.None;
		StartPosition = AppStartPosition;

		BackColor = Color.FromArgb(6, 17, 33);

		MaximumSize = AppSize;
		MinimumSize = AppSize;

		Icon = Resources.ICON;

		Text = AppTitle;
		Name = AppTitle;

		Tool.Round(this, 6);
		ResumeLayout(false);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public readonly DashMenuBar MenuBar = new DashMenuBar(string.Empty, minim:false);

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

	public readonly PictureBox BottomContainer = new PictureBox();
	public readonly Button BottomClose = new Button();

	private void InitializeBottomBar()
	{
	    var BContainerSize = new Size(Width, 34);
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

	    var ButtonSize = new Size(100, BContainerSize.Height);
	    var ButtonLocation = new Point((BContainerSize.Width - ButtonSize.Width) / 2, 0);
	    var ButtonFColor = Color.White;
	    var ButtonBColor = MenuBar.Bar.BackColor;

	    try
	    {
		Control.Button(BottomContainer, BottomClose, ButtonSize, ButtonLocation, ButtonBColor, ButtonFColor, 1, 10, "Close");
		Tool.Round(BottomClose, 6);

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

	public readonly PictureBox InnerTextContainer = new PictureBox();
	public readonly PictureBox TextContainer = new PictureBox();

	public readonly TextBox TextBox = new TextBox();

	private void InitializeContainer()
	{
	    var TContainerSize = new Size(Width - 22, Height - 22 - MenuBar.Bar.Height - BottomContainer.Height);
	    var TContainerLocation = new Point(11, MenuBar.Bar.Height + 11);
	    var TContainerBColor = Color.FromArgb(9, 39, 66);

	    var IContainerSize = new Size(TContainerSize.Width - 10, TContainerSize.Height - 10);
	    var IContainerLocation = new Point(5, 5);
	    var IContainerBColor = TContainerBColor;

	    try
	    {
		Control.Image(TextContainer, InnerTextContainer, IContainerSize, IContainerLocation, IContainerBColor);
		Control.Image(this, TextContainer, TContainerSize, TContainerLocation, TContainerBColor);

		Tool.Round(InnerTextContainer, 10);
		Tool.Round(TextContainer, 6);
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
