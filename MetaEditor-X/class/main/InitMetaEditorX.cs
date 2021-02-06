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
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MetaEditorX
{
    public class InitMetaEditorX
    {
	private Exception GetFormat(Exception E) =>
	    ErrorHandler.GetException(E);

	public readonly DashDialog DashDialog = new DashDialog();

	public void Component()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(45, 41, 77);
		var AppBColor = Color.FromArgb(178, 121, 252);
		var AppSize = new Size(350, 300);

		DashDialog.JustInitialize(AppSize, "Meta Editor-X", AppBColor, MenuBarBColor, CloseHideApp:false);
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly PictureBox MetaContainer = new PictureBox();
	
	public void Container()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly Form MetaInnerContainer = new Form();

	public void InnerContainer()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly PictureBox MetaBarContainer = new PictureBox();
	public readonly PictureBox ButtonContainer = new PictureBox();

	public readonly Button Load = new Button();
	public readonly Button Misc = new Button();

	public void BottomBar()
	{
	    try
	    {
		//
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}
    }
}
