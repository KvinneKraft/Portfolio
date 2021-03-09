
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

	private string AppInfoData()
	{
	    return string.Format
	    (
		"\r\n----( Start of Application Info )----\r\n\r\n" +
		"So, what is truly the use of this application; you may ask?  This application was designed for stress-testing webservers using the Slowloris technique.  The unique thing about my application is that it uses a custom version of Slowloris, I like to call it Dashloris.  It does nearly the same as the original Slowloris technique, the difference being how it does what it does, such as connecting to the servers at a high-rate without using that much bandwidth (70-100 bytes per connection), the original Slowloris would slowly build up it connections, however Dashloris builds them up at a faster pace while maintaining its obscurity.\r\n\r\n" +
		"Another unique thing about the Dashloris technique is that it allows you to configure how it does what it is best at, connecting to a server with headers.  This is very useful for those who are trying to see whether their server is Slowloris/Dashloris -Proof or whether they will need to up their security in a sense.  Now, before I start declaring this application as an official stress-test tool, when you use this application you are automatically agreeing to my ToS, which states that I am unable to be held responsible for any of the harm you may inflict upon another for that matter.  This application can damage equipment when used wrongly, please be sure you know what you are doing before you start using this application.\r\n\r\n" +
		"So, you got a big list of options which you can change to whatever your preference may be (assuming it is a valid value for the given column) but that is not all!  I also implemented a proxy list, this will allow you to connect to the web server through a proxy and therefore make your connection a bit more obscure. \r\n\r\n" +
		"Like I have said before, if you have any suggestions, let me know, I am open for them.\r\n\r\n" +
		"----( End of Application Info )----" 
	    );
	}

	public void InitializePage(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		var ContainerConfiguration = Tuple.Create("App Information", Capsule.Size, new Point(0, 0));
		var LabelConfiguration = Tuple.Create(Color.FromArgb(28, 28, 28), Color.White, AppInfoData());

		LabelPage.SetupPages(Capsule, ContainerConfiguration, LabelConfiguration);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
