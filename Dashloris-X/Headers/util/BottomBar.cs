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
using System.Collections.Generic;

namespace DashlorisX
{
    public class BottomBar
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	public readonly PictureBox ButtonContainer = new PictureBox();
	public readonly PictureBox BarContainer = new PictureBox();

	private Size GetTotalSize(List<Button> Buttons)
	{
	    var ButtonSize = Size.Empty;

	    for (int k = 0; k < Buttons.Count; k += 1)
	    {
		if (Buttons[k].Height > ButtonSize.Height)
		{
		    ButtonSize.Height = Buttons[k].Height;
		}

		ButtonSize.Width += Buttons[k].Width + 10;
	    }

	    return new Size(ButtonSize.Width - 10, ButtonSize.Height);
	}

	public void Create(Control Top, Color BackColor, int Height, List<Button> Buttons)
	{
	    try
	    {
		var BarSize = new Size(Top.Width, Height);
		var BarLocation = new Point(0, Top.Height - Height);

		var ButtonContainerSize = GetTotalSize(Buttons);
		var ButtonContainerLocation = new Point((BarSize.Width - ButtonContainerSize.Width) / 2, (BarSize.Height - ButtonContainerSize.Height) / 2);

		Control.Image(Top, BarContainer, BarSize, BarLocation, BackColor);
		Control.Image(BarContainer, ButtonContainer, ButtonContainerSize, ButtonContainerLocation, BackColor);

		for (int k = 0, x = 0, y = 0; k < Buttons.Count; k += 1, x += Buttons[k].Width)
		{
		    Buttons[k].Location = new Point(x, y);

		    ButtonContainer.Controls.Add(Buttons[k]);

		    if (k != Buttons.Count - 1)
		    {
			x += 10;
		    }
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
