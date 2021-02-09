
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
    public partial class DNSChangerX
    {
	private readonly Initialize Initialize = new Initialize();

	public void Show()
	{
	    try
	    {
		Initialize.CoreComponent();
		Initialize.TopComponent();
		Initialize.BottomComponent();

		Application.Run(Initialize.DashDialog);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
