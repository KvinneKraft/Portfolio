// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DashlorisX
{
    public class DashMenuBar
    {
	public readonly DashControls Control = new DashControls();
	public readonly DashTools Tool = new DashTools();

	bool minim = true, close = true, hide = true;

	public DashMenuBar(string title, bool minim = true, bool close = true, bool hide = true)
	{
	    this.minim = minim;
	    this.close = close;

	    Title.Text = (title);
	}

	public readonly PictureBox SLogo = new PictureBox();
	public readonly PictureBox Logo = new PictureBox();
	public readonly PictureBox Bar = new PictureBox();

	public readonly Label Title = new Label();

	public readonly Button Close = new Button();
	public readonly Button Minim = new Button();

	public void UpdateTitle(string Title)
	{
	    try
	    {
		Tool.Resize(this.Title, Tool.GetFontSize(Title, 8));

		this.Title.Text = Title;
		this.Title.Location = new Point(this.Title.Left, (Bar.Height - this.Title.Height) / 2);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Recolor(Color Color)
	{
	    try
	    {
		Close.BackColor = Color;
		Minim.BackColor = Color;
		Title.BackColor = Color;

		var RectangleSize = new Size(Bar.Width - 1, Bar.Parent.Height - Bar.Height + 1);
		var RectangleLocation = new Point(0, Bar.Height + Bar.Top - 2);

		Tool.PaintRectangle(Bar.Parent, 2, RectangleSize, RectangleLocation, Color);

		SLogo.BackColor = Bar.Parent.BackColor;
		Logo.BackColor = Color;
		Bar.BackColor = Color;
	    }

	    catch (Exception E)
	    {
		throw ErrorHandler.GetException(E);
	    }
	}

	public void Add(Form Top, int Height, Color BarCola, Color BorCola)
	{
	    var MenuBarSize = new Size(Top.Width, Height);
	    var MenuBarLocation = new Point(0, 0);
	    var MenuBarBColor = BarCola;

	    try
	    {
		Control.Image(Top, Bar, MenuBarSize, MenuBarLocation, MenuBarBColor);
		Tool.Interactive(Bar, Top);
	    }

	    catch (Exception E)
	    {
		throw ErrorHandler.GetException(E);
	    }

	    var LogoSize = new Size(38, 32);
	    var LogoLocation = new Point(5, 5);

	    try
	    {
		Control.Image(Top, SLogo, LogoSize, LogoLocation, Top.BackColor/*Color.FromArgb(6, 17, 33)*/, ObjectImage: Properties.Resources.LOGO);
		Control.Image(Bar, Logo, LogoSize, LogoLocation, BarCola, ObjectImage: Properties.Resources.LOGO);

		Tool.Interactive(SLogo, Top);
		Tool.Interactive(Logo, Top);
	    }

	    catch (Exception E)
	    {
		throw ErrorHandler.GetException(E);
	    }

	    var TitleText = (Title.Text);
	    var TitleSize = Tool.GetFontSize(TitleText, 8);
	    var TitleLocation = new Point(Logo.Width + Logo.Left + 5, (Bar.Height - TitleSize.Height) / 2);

	    try
	    {
		Control.Label(Bar, Title, TitleSize, TitleLocation, BarCola, Color.White, 1, 8, TitleText);
		Tool.Interactive(Title, Top);
	    }

	    catch (Exception E)
	    {
		throw ErrorHandler.GetException(E);
	    }

	    var ButtonSize = new Size(65, Height);
	    var ButtonLocation = new Point(Bar.Width - ButtonSize.Width, 0);

	    try
	    {

		if (close)
		{
		    Control.Button(Bar, Close, ButtonSize, ButtonLocation, BarCola, Color.White, 1, 10, "X");
		    Tool.Interactive(Close, Top);

		    Close.Click += (s, e) =>
		    {
			if (!hide)
			{
			    Application.Exit();
			}

			else
			{
			    Top.Hide();//Close();
			}
		    };
		}

		if (close && minim)
		{
		    ButtonLocation.X -= ButtonSize.Width;
		}

		if (minim)
		{
		    Control.Button(Bar, Minim, ButtonSize, ButtonLocation, BarCola, Color.White, 1, 10, "-");
		    Tool.Interactive(Minim, Top);

		    Minim.Click += (s, e) => Top.SendToBack();
		}
	    }

	    catch (Exception E)
	    {
		throw ErrorHandler.GetException(E);
	    }

	    var RectangleSize = new Size(Bar.Width - 2, Top.Height - Bar.Height + 1);
	    var RectangleLocation = new Point(1, Bar.Height + Bar.Top - 2);

	    try
	    {
		Tool.PaintRectangle(Top, 2, RectangleSize, RectangleLocation, BorCola);
	    }

	    catch (Exception E)
	    {
		throw ErrorHandler.GetException(E);
	    }
	}
    }
}
