
// Author: Dashie
// Version: 3.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TheDashlorisX
{
    public class LabelPage
    {
	public void SetupPages(Tuple<string, Color, Color, Size, Point> ContainerSetup, Tuple<Color, Color, string, int> LabelSetup) //string TopBarTitle, Color ConBCol, Color ConFCol, Size ConSize, Point ConLoca, Color LabelBCol, Color LabelFCol, string PageData, int Pages)
	{
	    try
	    {
		// ContainerSetup -> Tuple Settings
		// LabelSetup -> Tuple Settings
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}