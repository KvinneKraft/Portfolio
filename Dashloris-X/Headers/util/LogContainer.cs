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
	    var BContainerSize = new Size(Width, 30);
	    var BContainerLocation = new Point(0, Height - BContainerSize.Height);

	    try
	    {

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
	    try
	    {

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
