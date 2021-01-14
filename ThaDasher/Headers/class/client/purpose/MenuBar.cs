using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class MenuBar
    {
	readonly static DashControls CONTROL = new DashControls();

	public static void InitializeMBar(Form TOP)
	{
	    try
	    {
		CONTROL.MenuBar(TOP, (int)MENUBAR.STYLE.THIC, true, Color.FromArgb(8,8,8), Color.FromArgb(8,8,8));
	    }

	    catch
	    {
		throw new Exception("InitializeMBar()");
	    }
	}
    }
}