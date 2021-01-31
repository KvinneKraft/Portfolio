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
	private readonly DashTools Tool = new DashTools();

	public Point CalculateCenter(Control Top, Control Object, Point ObjectLocation)
	{
	    if (ObjectLocation.X == -2)
	    {
		ObjectLocation.X = (Top.Width - Object.Width) / 2;
	    }

	    if (ObjectLocation.Y == -2)
	    {
		ObjectLocation.Y = (Top.Height - Object.Height) / 2;
	    }

	    return ObjectLocation;
	}

	public readonly Dictionary<TextBox, PictureBox> TextBoxContainers = new Dictionary<TextBox, PictureBox>(); 

	public void TextBox(Control Top, TextBox Object, Size ObjectSize, Point ObjectLocation, Color ObjectBColor, Color ObjectFColor, int FontTypeID, int FontSize, bool ReadOnly = false, bool Multiline = false, bool ScrollBar = false, bool FixedSize = true, bool TabStop = false)
	{
	    try
	    {
		Tool.Resize(Object, ObjectSize);

		Object.Location = CalculateCenter(Top, Object, ObjectLocation);

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

		    var ResizedSize = new Size(ObjectSize.Width - 10, Tool.GetFontSize("http", FontSize).Height);
		    var RelocatedLocation = new Point(5, (ObjectSize.Height - ResizedSize.Height) / 2 + 1);

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

	public void Button(Control Top, Button Object, Size ObjectSize, Point ObjectLocation, Color ObjectBColor, Color ObjectFColor, int FontTypeID, int FontSize, string ButtonText, bool TabStop = false)
	{
	    try
	    {
		Tool.Resize(Object, ObjectSize);

		Object.Location = CalculateCenter(Top, Object, ObjectLocation);

		Object.BackColor = ObjectBColor;
		Object.ForeColor = ObjectFColor;

		Object.Font = Tool.GetFont(FontTypeID, FontSize);
		Object.Text = ButtonText;

		Object.FlatAppearance.BorderColor = ObjectBColor;
		Object.FlatAppearance.BorderSize = 0;

		Object.FlatStyle = FlatStyle.Flat;
		Object.TabStop = TabStop;

		Top.Controls.Add(Object);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Label(Control Top, Label Object, Size ObjectSize, Point ObjectLocation, Color ObjectBColor, Color ObjectFColor, int FontTypeID, int FontSize, string LabelText, bool TabStop = false)
	{
	    try
	    {
		if (ObjectSize == Size.Empty)
		{
		    ObjectSize = Tool.GetFontSize(LabelText, FontSize);
		}

		Tool.Resize(Object, ObjectSize);

		Object.Location = CalculateCenter(Top, Object, ObjectLocation);

		Object.BackColor = ObjectBColor;
		Object.ForeColor = ObjectFColor;

		Object.Font = Tool.GetFont(FontTypeID, FontSize);
		Object.Text = LabelText;

		Object.BorderStyle = BorderStyle.None;
		Object.FlatStyle = FlatStyle.Flat;

		Object.TabStop = TabStop;

		Top.Controls.Add(Object);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void Image(Control Top, PictureBox Object, Size ObjectSize, Point ObjectLocation, Color BackColor, Image ObjectImage = null, bool TabStop = false)
	{
	    try
	    {
		if (ObjectSize == Size.Empty)
		{
		    if (ObjectImage == null)
		    {
			throw new Exception("No image specified.");
		    }

		    ObjectSize = ObjectImage.Size;
		}

		Tool.Resize(Object, ObjectSize);

		Object.Location = CalculateCenter(Top, Object, ObjectLocation);

		Object.BackColor = BackColor;
		Object.TabStop = TabStop;

		Object.BorderStyle = BorderStyle.None;
		Object.Image = ObjectImage;

		Top.Controls.Add(Object);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	// Add Scrollbar here.
    }
}