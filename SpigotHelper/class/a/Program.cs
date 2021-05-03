using System;
using System.Drawing;
using System.Windows.Forms;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;
using DashFramework.Erroring;
using DashFramework.Dialog;

namespace SpigotHelper
{
    // Stone / Oak themed buttons
    // Minecraft Font Style

    public class SectorInitor
    {
	readonly DashControls Control = new DashControls();
	readonly DashTools Tool = new DashTools();


	readonly PictureBox S1Container = new PictureBox();

	readonly Button S1Button1 = new Button();
	readonly Button S1Button2 = new Button();
	readonly Button S1Button3 = new Button();
	readonly Button S1Button4 = new Button();

	public void InitSector1(DashWindow App, PictureBox Main)
	{
	    try
	    {
		var ContainerImag = Properties.Resources.containerWallpaper as Image;
		var ContainerSize = new Size(250, 50);
		var ContainerLoca = new Point(-2, 15);

		Control.Image(Main, S1Container, ContainerSize, ContainerLoca, Color.White, ContainerImag);

		var ButtonSize = new Size(120, 20);
		var ButtonBCol = App.MenuBar.MenuBar.BackColor;
		var ButtonFCol = Color.White;

		var Button1Loca = new Point(0, 0);
		var Button2Loca = new Point(130, 0);
		var Button3Loca = new Point(0, 30);
		var Button4Loca = new Point(130, 30);

		Control.Button(S1Container, S1Button1, ButtonSize, Button1Loca, ButtonBCol, ButtonFCol, 1, 7, ("Start Server"));
		Control.Button(S1Container, S1Button2, ButtonSize, Button2Loca, ButtonBCol, ButtonFCol, 1, 7, ("Config Server"));
		Control.Button(S1Container, S1Button3, ButtonSize, Button3Loca, ButtonBCol, ButtonFCol, 1, 7, ("Update Plugins"));
		Control.Button(S1Container, S1Button4, ButtonSize, Button4Loca, ButtonBCol, ButtonFCol, 1, 7, ("API Downloads"));

		foreach (Button Button in S1Container.Controls)
		{
		    Tool.Round(Button, 6);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly TextBox S2TextBox1 = new TextBox();
	readonly TextBox S2TextBox2 = new TextBox();

	readonly Button S2Button = new Button();

	readonly Label S2Label2 = new Label();

	private void InitSector2ConsoleOutput()
	{
	    try
	    {
		var TextBox1Size = new Size(S2Container2.Width, S2Container2.Height - 20);
		var TextBox1BCol = Color.FromArgb(6, 6, 6);
		var TextBox1Loca = new Point(0, 20);

		Control.TextBox(S2Container2, S2TextBox1, TextBox1Size, TextBox1Loca, TextBox1BCol, Color.White, 1, 8,
		    ReadOnly: true, Multiline: true, ScrollBar: true, FixedSize: false);

		var LabelSize = new Size(15, 20);
		var LabelLoca = new Point(0, 0);
		var LabelBCol = Color.MidnightBlue;

		Control.Label(S2Container2, S2Label2, LabelSize, LabelLoca, LabelBCol, Color.White, 1, 11, ("$:"));
		S2Label2.TextAlign = ContentAlignment.MiddleCenter;

		var TextBox2Size = new Size(S2Container2.Width - 90, 20);
		var TextBox2BCol = Color.MidnightBlue;
		var TextBox2Loca = new Point(15, 0);

		Control.TextBox(S2Container2, S2TextBox2, TextBox2Size, TextBox2Loca, TextBox2BCol, Color.White, 1, 9, ReadOnly: true);

		var ButtonSize = new Size(75, 20);
		var ButtonLoca = new Point(S2Container2.Width - 75, 0);
		var ButtonBCol = Color.MidnightBlue;

		Control.Button(S2Container2, S2Button, ButtonSize, ButtonLoca, ButtonBCol, Color.White, 1, 9, ("Execute"));
		S2Button.TextAlign = ContentAlignment.MiddleCenter;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}


	readonly PictureBox S2Container1 = new PictureBox();
	readonly PictureBox S2Container2 = new PictureBox();

	readonly Label S2Label1 = new Label();

	public void InitSector2(DashWindow App, PictureBox Main)
	{
	    try
	    {
		var Container1Size = new Size(Main.Width, Main.Height - S1Container.Height - S1Container.Top - 15);
		var Container1Loca = new Point(-2, S1Container.Height + S1Container.Top + 15);

		Control.Image(Main, S2Container1, Container1Size, Container1Loca, Color.Transparent, S1Container.BackgroundImage);

		var Label1Loca = new Point(5, 0);

		Control.Label(S2Container1, S2Label1, Size.Empty, Label1Loca, Color.Transparent, Color.White, 1, 14, ("Server Console:"));
		S2Label1.Image = S1Container.BackgroundImage;

		var Container2Size = new Size(Main.Width, S2Container1.Height - Label1Loca.Y - S2Label1.Height - 5);
		var Container2Loca = new Point(0, S2Label1.Height + S2Label1.Top + 5);

		Control.Image(S2Container1, S2Container2, Container2Size, Container2Loca, Color.Transparent, S2Label1.Image);
		
		InitSector2ConsoleOutput();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }

    static class Program
    { 
	static readonly PictureBox DashWindowContainer = new PictureBox();
	static readonly DashWindow DashWindow = new DashWindow();

	static readonly SectorInitor DashInit = new SectorInitor();

	static readonly DashControls Control = new DashControls();
	static readonly DashTools Tool = new DashTools();

	static DashWindow GetDashWindow()
	{
	    try
	    {
		var AppMCol = Color.FromArgb(153, 54, 54);//(132, 66, 245);
		var AppBCol = Color.FromArgb(54, 160, 255);
		var AppSize = new Size(300, 350);

		DashWindow.InitializeWindow(AppSize, ("Spigot Helper"), AppBCol, AppMCol, CloseHideApp: false);

		DashWindow.MenuBar.LogoLayer2.BackgroundImage = Properties.Resources.appWallpaper;
		DashWindow.BackgroundImage = Properties.Resources.appWallpaper;

		var ContainerSize = new Size(280, 330 - DashWindow.MenuBar.MenuBar.Height);
		var ContainerImag = Properties.Resources.containerWallpaper as Image;
		var ContainerLoca = new Point(-2, 36); 

		Control.Image(DashWindow, DashWindowContainer, ContainerSize, ContainerLoca, AppBCol, ContainerImag);

		Tool.Round(DashWindowContainer, 8);

		DashInit.InitSector1(DashWindow, DashWindowContainer);
		DashInit.InitSector2(DashWindow, DashWindowContainer);

		return DashWindow;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	[STAThread]
	static void Main()
	{
	    try
	    {
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(GetDashWindow());
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
        }
    }
}
