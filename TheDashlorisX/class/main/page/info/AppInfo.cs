
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
    public class AppInfo
    {
	private readonly LabelPage LabelPage = new LabelPage();

	private string TermsOfServices()
	{
	    return string.Format
	    (
		"Gots to write info here\r\n\r\n" 
		// Change page number to according amount
	    );
	}

	public void InitializePage(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var ContainerConfiguration = Tuple.Create("App Information", Capsule.Size, new Point(0, 0));
		var LabelConfiguration = Tuple.Create(Color.FromArgb(28, 28, 28), Color.White, TermsOfServices(), 3);

		LabelPage.SetupPages(Capsule, ContainerConfiguration, LabelConfiguration);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
