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

namespace DashlorisX
{
    public class DashBox
    {
	public enum Buttons
	{
	    OKCancel = 0, YesNo = 1, OK = 2
	}

	public int Show(string Message, string Title, Color AppBColor, Color MenuBarBColor, Color TextContainerBColor, Color ForeColor, Buttons Buttons)//, [Optional] Icon icon, [Optional] Point iconLocation, [Optional] List<Button> buttonList)
	{
	    try
	    {
		using (DashDialog DashDialog = new DashDialog())
		{
		    var Instance = new ControlInitialization();

		    DashDialog.JustInitialize(Instance.GetAppSize(), Title, AppBColor, MenuBarBColor);
		    
		    Instance.DoInitializeBottomBar(DashDialog, MenuBarBColor, ForeColor, Buttons);
		    Instance.DoInitializeMessageContainer(DashDialog, TextContainerBColor, ForeColor);

		    DashDialog.ShowAsIs();

		    return Instance.ClickedValue;
		}
	    }

	    catch (Exception E)
	    {
		var DialogResult = MessageBox.Show("There was an error while loading one or more dialogs.  This is preventing a message from being shown.  Would you still wish to continue?  \r\n\r\nPlease, keep in mind that if you continue, you may experience issues.\r\n\r\nPress OK to continue or press CANCEL to close the application and show the stack-trace.", "Error Handler", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

		if (DialogResult.Equals(System.Windows.Forms.DialogResult.Cancel))
		{
		    throw (ErrorHandler.GetException(E));
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

	    public void DoInitializeMessageContainer(DashDialog DashDialog, Color BackColor, Color ForeColor)
	    {
		Size AppSize = GetAppSize();

		var UTContainerSize = new Size(AppSize.Width - 22, AppSize.Height - 22 - BottomBarContainer.Height - DashDialog.MenuBar.Bar.Height);
		var UTContainerLocation = new Point(11, DashDialog.MenuBar.Bar.Height + 11);

		var ITContainerSize = new Size(UTContainerSize.Width - 10, UTContainerSize.Height - 10);
		var ITContainerLocation = new Point(5, 5);

		try
		{
		    Control.Image(UpperTextContainer, InnerTextContainer, ITContainerSize, ITContainerLocation, BackColor);
		    Control.Image(DashDialog, UpperTextContainer, UTContainerSize, UTContainerLocation, BackColor);
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}

		var TextBoxSize = ITContainerSize;
		var TextBoxLocation = new Point(0, 0);

		try
		{
		    Control.TextBox(InnerTextContainer, TextBox, TextBoxSize, TextBoxLocation, BackColor, ForeColor, 1, 9, ReadOnly:true, Multiline:true, ScrollBar:true, FixedSize:false);
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
		var BOContainerLocation = new Point(0, 0);

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
		    
		    for (int k = 1; k <= 2; k += 1)
		    {
			Control.Button(BottomBarButtonContainer, GetButtonObject(k), ButtonSize, Button1Location, BackColor, ForeColor, 1, 10, GetButtonText(k));

			GetButtonObject(k).Click += (s, e) =>
			{
			    ClickedValue = k - 1;
			    DashDialog.Close();
			};
		    }
		}

		catch (Exception E)
		{
		    throw (ErrorHandler.GetException(E));
		}
	    }

	    public Size GetAppSize()
	    {
		return new Size(200, 250);// Get Size
	    }
	}
    }
}
