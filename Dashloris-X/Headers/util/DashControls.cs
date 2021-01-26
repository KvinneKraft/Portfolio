// Author: Dashie
// Version: 1.0
//
// A component library for my projects.  Feel free to use it or perhaps give
// me some suggestions on how to improve it, if any.  
//
// Version: 3.0
// GitHub: https://github.com/KvinneKraft
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
    public class DashControls
    {
	readonly DashTools Tool = new DashTools();

	public readonly Dictionary<TextBox, PictureBox> TextBoxContainers = new Dictionary<TextBox, PictureBox>(); 

	public void TextBox(Control Top, TextBox Object, Size ObjectSize, Point ObjectLocation, Color ObjectBColor, Color ObjectFColor, int FontTypeID, int FontSize, bool ReadOnly = false, bool Multiline = false, bool ScrollBar = false, bool FixedSize = true, bool TabStop = false)
	{
	    try
	    {
		Tool.Resize(Object, ObjectSize);

		if (ObjectLocation.X == -2)
		{
		    ObjectLocation.X = (Top.Width - Object.Width) / 2;
		}

		if (ObjectLocation.Y == -2)
		{
		    ObjectLocation.Y = (Top.Height - Object.Height) / 2;
		}

		Object.BackColor = ObjectBColor;
		Object.ForeColor = ObjectFColor;

		Object.BorderStyle = BorderStyle.None;

		Object.Multiline = Multiline;
		Object.ReadOnly = ReadOnly;
		Object.TabStop = TabStop;

		if (FixedSize)
		{
		    var TextBoxContainer = new PictureBox();

		    Tool.Resize(TextBoxContainer, ObjectSize);
		    
		    TextBoxContainer.Location = ObjectLocation;
		    TextBoxContainer.Font = Tool.GetFont(FontTypeID, FontSize);

		    TextBoxContainer.BorderStyle = BorderStyle.None;
		    
		    TextBoxContainer.BackColor = ObjectBColor;
		    TextBoxContainer.ForeColor = ObjectFColor;

		    Top.Controls.Add(TextBoxContainer);

		    TextBoxContainer.Click += (s, e) =>
		    {
			Object.Select();
		    };

		    var ResizedSize = new Size(ObjectSize.Width - 10, Tool.GetFontSize("HTTP", FontSize).Height);
		    var RelocatedLocation = new Point(5, (TextBoxContainer.Height - ObjectSize.Height) / 2);

		    Object.Location = RelocatedLocation;
		    Tool.Resize(Object, ResizedSize);

		    TextBoxContainers.Add(Object, TextBoxContainer);
		    TextBoxContainer.Controls.Add(Object);
		}

		else
		{
		    Object.Location = ObjectLocation;
		    Object.Font = Tool.GetFont(FontTypeID, FontSize);

		    if (ScrollBar)
		    {
			Object.ScrollBars = ScrollBars.Vertical;
		    }

		    Top.Controls.Add(Object);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Button()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Label()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Image()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	// Add Scrollbar here.
    }
}