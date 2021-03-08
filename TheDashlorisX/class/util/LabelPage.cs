
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
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();


	private readonly PictureBox S1Container1 = new PictureBox();

	private void Init1(PictureBox Capsule, Size ContainerSize, Point ContainerLoca)
	{
	    try
	    {
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

	private void Init2(int Pages)
	{
	    try
	    {
		var ContainerSize = new Size(S1Container1.Width, 24);
		var ContainerLoca = new Point(0, 0);

		Control.Image(S1Container1, S2Container1, ContainerSize, ContainerLoca, Color.MidnightBlue);

		var Label1Size = Tool.GetFontSize("Page:", 9);
		var Label2Size = Tool.GetFontSize($"1/{Pages} ", 9);

		var Label1Loca = new Point((S2Container1.Width - (Label1Size.Width + Label2Size.Width)) / 2, (S2Container1.Height - Label1Size.Height) / 2);
		var Label2Loca = new Point(Label1Loca.X + Label1Size.Width, Label1Loca.Y);

		Control.Label(S2Container1, S2Label2, Label2Size, Label2Loca, S2Container1.BackColor, Color.White, 1, 9, $"1/{Pages}");
		Control.Label(S2Container1, S2Label1, Label1Size, Label1Loca, S2Container1.BackColor, Color.White, 1, 9, "Page:");
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

	private void Init3(string Title)
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

		var LabelText = ($"{Title}");
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

	private void Init4(string Message, Color LabelBCol, Color LabelFCol)
	{
	    try
	    {
		var ContainerSize = new Size(S1Container1.Width - 5, S1Container1.Height - S3Container1.Height - S2Container1.Height - 4);
		var ContainerLoca = new Point(3, S2Container1.Height + 2);
		var ContainerBCol = Color.FromArgb(24, 24, 24);

		Control.Image(S1Container1, S4Container1, ContainerSize, ContainerLoca, ContainerBCol);

		var LabelText = Message;
		var LabelFSiz = TextRenderer.MeasureText(LabelText, Tool.GetFont(1, 9), Size.Empty, flags: TextFormatFlags.WordBreak);//Tool.GetFontSize(TermsOfServices(), 9);
		var LabelSize = new Size(ContainerSize.Width - 4, LabelFSiz.Height - 4);
		var LabelLoca = new Point(2, 2);

		Control.Label(S4Container1, S4Label1, LabelSize, LabelLoca, LabelBCol, LabelFCol, 1, 9, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	public void SetupPages(PictureBox Capsule, Tuple<string, Size, Point> ContainerSetup, Tuple<Color, Color, string, int> LabelSetup) //string TopBarTitle, Color ConBCol, Color ConFCol, Size ConSize, Point ConLoca, Color LabelBCol, Color LabelFCol, string PageData, int Pages)
	{
	    try
	    {
		Init1(Capsule, ContainerSetup.Item2, ContainerSetup.Item3);
		Init2(LabelSetup.Item4);
		Init3(ContainerSetup.Item1);
		Init4(LabelSetup.Item3, LabelSetup.Item1, LabelSetup.Item2);
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}