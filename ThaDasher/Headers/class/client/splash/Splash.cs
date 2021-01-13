using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class Splash : Form
    {
	readonly static DashTools TOOL = new DashTools();
	readonly static Interface INER = new Interface();

	private void RegisterTimer()
	{
	    var timer = new System.Windows.Forms.Timer()
	    {
		Interval = 100, //2500,
		Enabled = true,
	    };

	    timer.Tick += (s, e) =>
	    {
		timer.Stop();Hide();
		INER.ShowDialog();
	    };

	    timer.Start();
	}

	public Splash(Image image)
	{
	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterScreen;

	    BackgroundImage = image;
	     
	    TOOL.Resize(this, BackgroundImage.Size);
	    TOOL.Interactive(this, this);

	    TransparencyKey = Color.FromArgb(28, 28, 28);
	    BackColor = TransparencyKey;

	    RegisterTimer();
	}
    }
}