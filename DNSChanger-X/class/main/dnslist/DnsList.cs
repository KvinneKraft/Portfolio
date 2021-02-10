
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DNSChangerX
{
    public class DnsList
    {
	private readonly InitDnsList InitDnsList = new InitDnsList();



	public DnsList()
	{
	    try
	    {
		InitDnsList.CoreComponent();//GUI itself, Dash Dialog thing.
		InitDnsList.DisplayComponent();//Columns and such
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
		InitDnsList.DashDialog.ShowAsIs();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
