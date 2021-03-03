
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
		var ContainerBCol = Color.FromArgb(8, 8, 8);

		Control.Image(Capsule, S1Container1, ContainerSize, ContainerLoca, ContainerBCol);
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

	private void Init2()
	{
	    try
	    {
		var ContainerSize = new Size(S1Container1.Width, 24);
		var ContainerLoca = new Point(0, 0);
		
		Control.Image(S1Container1, S2Container1, ContainerSize, ContainerLoca, Color.MidnightBlue);

		var Label1Size = Tool.GetFontSize("Page:", 9);
		var Label2Size = Tool.GetFontSize("1/10 ", 9);

		var Label1Loca = new Point((S2Container1.Width - (Label1Size.Width + Label2Size.Width)) / 2, (S2Container1.Height - Label1Size.Height) / 2);
		var Label2Loca = new Point(Label1Loca.X + Label1Size.Width, Label1Loca.Y);

		Control.Label(S2Container1, S2Label1, Label1Size, Label1Loca, S2Container1.BackColor, Color.White, 1, 9, "Page:");
		Control.Label(S2Container1, S2Label2, Label2Size, Label2Loca, S2Container1.BackColor, Color.White, 1, 9, "1/10");
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

		Tool.Round(S3Button1, 6);
		Tool.Round(S3Button2, 6);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	private readonly PictureBox S4Container1 = new PictureBox();
	private readonly PictureBox S4Container2 = new PictureBox();

	private readonly Label S4Label1 = new Label();

	private string TermsOfServices()
	{
	    return string.Format
	    (
		"1\r\n" +
		"2\r\n" +
		"3\r\n" +
		"4\r\n"
	    );
	}

	private void Init4()
	{
	    try
	    {

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
