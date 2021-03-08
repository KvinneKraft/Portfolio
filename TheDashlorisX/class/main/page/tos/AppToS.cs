
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
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();

	private void Init1(PictureBox Capsule)
	{
	    try
	    {
		var ContainerSize = Capsule.Size;
		var ContainerLoca = new Point(0, 0);

		Control.Image(Capsule, S1Container1, ContainerSize, ContainerLoca, Color.MidnightBlue);
		Tool.Round(S1Container1, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S2Container1 = new PictureBox();

	private readonly Label S2Label1 = new Label();//Title MEssage
	private readonly Label S2Label2 = new Label();//not Necessary (1/4)

	private int S2PageID = 1;

	private void Init2()
	{
	    try
	    {
		var ContainerSize = new Size(S1Container1.Width, 24);
		var ContainerLoca = new Point(0, 0);
		
		Control.Image(S1Container1, S2Container1, ContainerSize, ContainerLoca, Color.MidnightBlue);

		var Label1Size = Tool.GetFontSize("Page:", 9);
		var Label2Size = Tool.GetFontSize("1/3 ", 9);

		var Label1Loca = new Point((S2Container1.Width - (Label1Size.Width + Label2Size.Width)) / 2, (S2Container1.Height - Label1Size.Height) / 2);
		var Label2Loca = new Point(Label1Loca.X + Label1Size.Width, Label1Loca.Y);

		Control.Label(S2Container1, S2Label1, Label1Size, Label1Loca, S2Container1.BackColor, Color.White, 1, 9, "Page:");
		Control.Label(S2Container1, S2Label2, Label2Size, Label2Loca, S2Container1.BackColor, Color.White, 1, 9, "1/3");
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S3Container1 = new PictureBox();
	private readonly PictureBox S3Container2 = new PictureBox();

	private readonly Button S3Button1 = new Button();//Left
	private readonly Button S3Button2 = new Button();//Right

	private void S3ChangePage(bool Forward)
	{
	    try
	    {
		if (Forward)
		{
		    if (S2PageID > 2)
		    {
			return;
		    }
		    
		    S4Label1.Top -= S4Container1.Height;
		    S2PageID += 1;
		}

		else
		{
		    if (S2PageID < 2)
		    {
			return;
		    }

		    S4Label1.Top += S4Container1.Height;
		    S2PageID -= 1;
		}

		S2Label2.Text = $"{S2PageID}" + S2Label2.Text.Substring(1, 2);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private readonly Label S3Label1 = new Label();

	private void Init3()
	{
	    try
	    {
		var Container1Size = new Size(S2Container1.Width, 30);
		var Container1Loca = new Point(0, S1Container1.Height - 30);

		var Container2Size = new Size(95, 26);
		var Container2Loca = new Point(Container1Size.Width - 105, 1);

		var ContainerBCol = S2Container1.BackColor;

		Control.Image(S1Container1, S3Container1, Container1Size, Container1Loca, ContainerBCol);
		Control.Image(S3Container1, S3Container2, Container2Size, Container2Loca, ContainerBCol);

		var LabelText = ("The Terms of Service");
		var LabelSize = Tool.GetFontSize(LabelText, 9);
		var LabelLoca = new Point(10, (S3Container1.Height - LabelSize.Height) / 2);

		Control.Label(S3Container1, S3Label1, LabelSize, LabelLoca, ContainerBCol, Color.White, 1, 9, (LabelText));

		var Button2Loca = new Point(50, 0);
		var Button1Loca = new Point(0, 0);

		var ButtonSize = new Size(45, 26);

		Control.Button(S3Container2, S3Button1, ButtonSize, Button1Loca, ContainerBCol, Color.White, 1, 9, ("<"));
		Control.Button(S3Container2, S3Button2, ButtonSize, Button2Loca, ContainerBCol, Color.White, 1, 9, (">"));

		S3Button1.Click += (s, e) => S3ChangePage(false);
		S3Button2.Click += (s, e) => S3ChangePage(true);

		Tool.Round(S3Button1, 6);
		Tool.Round(S3Button2, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S4Container1 = new PictureBox();

	private readonly Label S4Label1 = new Label();

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

	private void Init4()
	{
	    try
	    {
		var ContainerSize = new Size(S1Container1.Width - 5, S1Container1.Height - S3Container1.Height - S2Container1.Height - 4);
		var ContainerLoca = new Point(3, S2Container1.Height + 2);
		var ContainerBCol = Color.FromArgb(24, 24, 24);

		Control.Image(S1Container1, S4Container1, ContainerSize, ContainerLoca, ContainerBCol);

		var LabelText = TermsOfServices();
		var LabelFSiz = TextRenderer.MeasureText(LabelText, Tool.GetFont(1, 9), Size.Empty, flags:TextFormatFlags.WordBreak);//Tool.GetFontSize(TermsOfServices(), 9);
		var LabelSize = new Size(ContainerSize.Width - 4, LabelFSiz.Height - 4);
		var LabelLoca = new Point(2, 2);

		Control.Label(S4Container1, S4Label1, LabelSize, LabelLoca, ContainerBCol, Color.White, 1, 9, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void InitializePage(DashDialog DashDialog, PictureBox Capsule)
	{
	    try
	    {
		Init1(Capsule);
		Init2();
		Init3();
		Init4();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
