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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DNSChangerX
{
    public class DashBox
    {
	public enum Buttons
	{
	    OKCancel = 0, YesNo = 1, OK = 2
	}

	public int Show(string Message, string Title, Color AppBCol, Color MenuBarBCol, Color TextContainerBCol, Color ForeColor, Buttons Buttons = Buttons.OK, Button Optional1 = null, Button Optional2 = null, Image Icon = null)//, [Optional] Icon icon, [Optional] Point iconLocation, [Optional] List<Button> buttonList)
	{
	    try
	    {
		using (DashDialog DashDialog = new DashDialog())
		{
		    var Instance = new ControlInitialization();

		    DashDialog.JustInitialize(Instance.GetAppSize(), Title, AppBCol, MenuBarBCol);

		    Instance.DoInitializeMessageContainer(DashDialog, Message, TextContainerBCol, ForeColor);
		    Instance.DoInitializeBottomBar(DashDialog, MenuBarBCol, ForeColor, Buttons);
		    Instance.DoInitializeApp(DashDialog);

		    DashDialog.ShowAsIs();

		    return Instance.ClickedValue;
		}
	    }

	    catch (Exception E)
	    {
		var DialogResult = MessageBox.Show("There was an error while loading one or more dialogs.  This is preventing a message from being shown.  Would you still wish to continue?  \r\n\r\nPlease, keep in mind that if you continue, you may experience issues.\r\n\r\nPress OK to continue or press CANCEL to close the application and show the stack-trace.", "Error Handler", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

		if (DialogResult == DialogResult.Cancel)
		{
		    ErrorHandler.Utilize(ErrorHandler.GetFormat(ErrorHandler.GetException(E)), "Error Handler");
		}
	    }

	    return -1;
	}

	private class ControlInitialization
	{
	    private readonly DashControls Control = new DashControls();
	    private readonly DashTools Tool = new DashTools();

	    public int ClickedValue = -1;

	    private readonly PictureBox UpperTextContainer = new PictureBox();
	    private readonly PictureBox InnerTextContainer = new PictureBox();
	    
	    private readonly TextBox TextBox = new TextBox();

	    public void DoInitializeMessageContainer(DashDialog DashDialog, string Message, Color BackColor, Color ForeColor)
	    {
		var AppSize = GetAppSize();

		var TextBoxFont = Tool.GetFont(1, 9);
		var TextBoxSize = new Size(AppSize.Width - 32, TextRenderer.MeasureText(Message, TextBoxFont, new Size(AppSize.Width - 32, 0), TextFormatFlags.WordBreak).Height);
		var TextBoxLocation = new Point(0, 0);

		var MainContainerSize = new Size(AppSize.Width - 22, TextBoxSize.Height + 10);
		var MainContainerLocation = new Point(11, DashDialog.MenuBar.Bar.Height + 11);

		var SubContainerSize = new Size(MainContainerSize.Width - 10, MainContainerSize.Height - 10);
		var SubContainerLocation = new Point(5, 5);

		try
		{
		    Control.TextBox(InnerTextContainer, TextBox, TextBoxSize, TextBoxLocation, BackColor, ForeColor, 1, 9, ReadOnly: true, Multiline: true, FixedSize: false);

		    TextBox.Text = Message;

		    Control.Image(DashDialog, UpperTextContainer, MainContainerSize, MainContainerLocation, BackColor);
		    Control.Image(UpperTextContainer, InnerTextContainer, SubContainerSize, SubContainerLocation, BackColor);

		    Tool.Round(UpperTextContainer, 6);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    private readonly PictureBox BottomBarButtonContainer = new PictureBox();
	    private readonly PictureBox BottomBarContainer = new PictureBox();

	    private readonly Button Button1 = new Button();
	    private readonly Button Button2 = new Button();

	    public void DoInitializeBottomBar(DashDialog DashDialog, Color BackColor, Color ForeColor, Buttons Buttons)
	    {
		var BOContainerSize = new Size(DashDialog.Width, 30);
		var BOContainerLocation = new Point(0, UpperTextContainer.Top + UpperTextContainer.Height + 12);

		try
		{
		    Control.Image(DashDialog, BottomBarContainer, BOContainerSize, BOContainerLocation, BackColor);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}

		var Button2Location = new Point(75, 0);
		var Button1Location = new Point(0, 0);

		var ButtonSize = new Size(65, 30);

		try
		{
		    Button GetButtonObject(int id)
		    {
			if (id == 1)
			{
			    return Button1;
			}

			else
			{
			    return Button2;
			}
		    }

		    string GetButtonText(int id)
		    {
			string text = string.Empty;

			if (Buttons == Buttons.OKCancel)
			{
			    if (id == 1)
			    {
				text = "OK";
			    }

			    else
			    {
				text = "Cancel";
			    }
			}

			else if (Buttons == Buttons.YesNo)
			{
			    if (id == 1)
			    {
				text = "Yes!";
			    }

			    else
			    {
				text = "No.";
			    }
			}

			else if (Buttons == Buttons.OK)
			{
			    if (id == 1)
			    {
				text = "OK!";
			    }
			}

			else
			{
			    text = "NOT FOUND";
			}

			return text;
		    }
		    
		    void SetButtonClickEvent(int id)
		    {
			GetButtonObject(id).Click += (s, e) =>
			{
			    ClickedValue = id;
			    DashDialog.Close();
			};
		    }

		    int b = 1;

		    if (Buttons != Buttons.OK)
		    {
			b += 1;
		    };

		    for (int k = 1, x = 0; k <= b; k += 1, x += ButtonSize.Width + 10)
		    {
			Control.Button(BottomBarButtonContainer, GetButtonObject(k), ButtonSize, new Point(x, 0), BackColor, ForeColor, 1, 10, GetButtonText(k));
			SetButtonClickEvent(k);
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}

		int GetTotalWidth()
		{
		    int Count = BottomBarButtonContainer.Controls.Count;
		    int Width = 0;

		    if (Count > 1)
		    {
			Width += 10;
		    }

		    Width += ButtonSize.Width * Count;

		    return Width;
		}

		var BUContainerSize = new Size(GetTotalWidth(), ButtonSize.Height);
		var BUContainerLocation = new Point((BOContainerSize.Width - BUContainerSize.Width) / 2, (BOContainerSize.Height - BUContainerSize.Height) / 2);

		try
		{
		    Control.Image(BottomBarContainer, BottomBarButtonContainer, BUContainerSize, BUContainerLocation, BackColor);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public void DoInitializeApp(DashDialog DashDialog)
	    {
		try
		{
		    Tool.Resize(DashDialog, new Size(BottomBarContainer.Width, BottomBarContainer.Top + BottomBarContainer.Height));

		    var RectangleSize = new Size(DashDialog.Width - 2, DashDialog.Height - DashDialog.MenuBar.Bar.Height + 1);
		    var RectangleLocation = new Point(1, DashDialog.MenuBar.Bar.Height + DashDialog.MenuBar.Bar.Top - 2);

		    Tool.PaintRectangle(DashDialog, 2, RectangleSize, RectangleLocation, DashDialog.MenuBar.Bar.BackColor);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public Size GetAppSize()
	    {
		return new Size(325, 250);// Get Size | Not even really going to be used.
	    }
	}
    }
}
