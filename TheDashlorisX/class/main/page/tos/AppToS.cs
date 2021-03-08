
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
    public class AppToS
    {
	private readonly LabelPage LabelPage = new LabelPage();

	private string TermsOfServices()
	{
	    return string.Format
	    (
		"----( Start of Terms of Services )----\r\n\r\n" +
		"1) Whenever you make use of this application you are automatically agreeing with any of the bellow.  Which includes me not being responsible for whatever you inflict (whether it be negative or positive) upon another using this application.  I merely claim responsibility as the owner of this software as the developer of this software.\r\n\r\n" +
		"2) You know what you are using and why you are using it.  You also know why and how you can and cannot use this application under the law of your country.  This means you fully claim responsibility (as stated before) and hereby confirm you are aware of any missuse of this product, if any.  Missuse being any use which is not tolerated by your local government.  Local being your country, state and perhaps even city.  Depending on where you live of course.  Know that any request sent can be read and interpreted by the receiver without having to decipher any form of encryption.\r\n\r\n" +
		"3) You know what the purpose of this project is and you also confirm that you have downloaded this piece of software from my GitHub repository.  You can find my GitHub repository at; https://github.com/KvinneKraft -Please validate any files if necessary.\r\n\r\n" +
		"4) It is not allowed to modify any part(s) of this software, any change made to the code of this software or the assembly itself is strongly prohibited, unless done by the application itself, which if the case should be able to be traced back to my GitHub source code and therefore declare the modification as allowed.  If you do wish to modify any part of this application or simply have suggestions, then you can always reach out to me at KvinneKraft@protonmail.com.  If instead you wish to scroll through my code and see what could use some improvement then you can go to the repository linked above.\r\n\r\n" +
		"----( End of Terms of Services )----"
	    );
	}

	public void InitializePage(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var ContainerConfiguration = Tuple.Create("App ToS", Capsule.Size, new Point(0, 0));
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
