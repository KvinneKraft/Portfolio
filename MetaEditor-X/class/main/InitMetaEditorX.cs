// Author: Dashie
// Version: 1.0
//
// *coughs*  this application was made for personal use only, yet is worth its place in my portfolio.  Its nice
// fully custom user interface is worth more than the functionality of this thing, but yea, who even cares, ahaha.
//
// I have a lot of other projects coming which will be a lot more codeful than this shit.  Have a nice day man!
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MetaEditorX
{
    public class InitMetaEditorX
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	private Exception GetFormat(Exception E) =>
	    ErrorHandler.GetException(E);

	public readonly DashDialog DashDialog = new DashDialog();

	public void Component()
	{
	    try
	    {
		var MenuBarBColor = Color.FromArgb(0, 27, 56);
		var AppBColor = Color.FromArgb(22, 61, 105);
		var AppSize = new Size(275, 215);

		DashDialog.JustInitialize(AppSize, "Meta Editor-X", AppBColor, MenuBarBColor, CloseHideApp:false);
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly PictureBox MetaContainer = new PictureBox();
	
	public void Container()
	{
	    try
	    {
		var ContainerSize = new Size(DashDialog.Width - 20, DashDialog.Height - 51 - DashDialog.MenuBar.Bar.Height);
		var ContainerLoca = new Point(10, 10 + DashDialog.MenuBar.Bar.Height);
		var ContainerBColor = Color.FromArgb(13, 34, 56);

		Control.Image(DashDialog, MetaContainer, ContainerSize, ContainerLoca, ContainerBColor);

		Tool.Round(MetaContainer, 8);

		InnerContainer();
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly Form MetaInnerContainer = new Form();

	public void InnerContainer()
	{
	    try
	    {
		MetaInnerContainer.Size = new Size(MetaContainer.Width - 3, MetaContainer.Height - 4);
		MetaInnerContainer.Location = new Point(2, 2);

		Tool.Round(MetaInnerContainer, 8);

		MetaInnerContainer.FormBorderStyle = FormBorderStyle.None;
		MetaInnerContainer.BackColor = MetaContainer.BackColor;

		MetaInnerContainer.TopLevel = false;
		MetaInnerContainer.Visible = true;

		MetaContainer.Controls.Add(MetaInnerContainer);
		
		AddMetaFields();
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	private readonly Dictionary<int, TextBox> TextBoxValues = new Dictionary<int, TextBox>();
	private readonly Dictionary<int, Label> LabelValues = new Dictionary<int, Label>();

	private readonly List<string> MetaType = new List<string>()
	{
	    "Creation Time", "Creation Time UTC", "Last Access Time",
	    "Last Access Time UTC", "Is Read Only",
	};

	private int GetMetaID(string type)
	{
	    return MetaType.IndexOf(type);
	}

	private Point GetLabelLocation(int index)
	{
	    Point Location = new Point(5, 8);

	    if (MetaInnerContainer.Controls.Count > 0)
	    {
		Control Control = MetaInnerContainer.Controls[MetaInnerContainer.Controls.Count - 2];
		Location.Y = Control.Height + Control.Top + 10;
	    }

	    return Location;
	}

	private void AddMetaRow(int index)
	{
	    var Label = new Label();

	    var LabelText = $"{MetaType[index]}:";
	    var LabelSize = Tool.GetFontSize(LabelText, 9);
	    var LabelLoca = GetLabelLocation(index);
	    var LabelBColor = MetaInnerContainer.BackColor;

	    try
	    {
		Control.Label(MetaInnerContainer, Label, LabelSize, LabelLoca, LabelBColor, Color.White, 1, 9, LabelText);
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }

	    var TextBox = new TextBox()
	    {
		Text = "Awaiting File ....",
	    };
	    
	    var TextBoxSize = new Size(MetaInnerContainer.Width - Label.Left - Label.Width - 10, Label.Height + 6);
	    var TextBoxLoca = new Point(Label.Left + Label.Width + 2, Label.Top - 2);
	    var TextBoxBColor = DashDialog.MenuBar.Bar.BackColor;

	    try
	    {
		Control.TextBox(MetaInnerContainer, TextBox, TextBoxSize, TextBoxLoca, TextBoxBColor, Color.FromArgb(200, 200, 200), 1, 9);
		Tool.Round(MetaInnerContainer.Controls[MetaInnerContainer.Controls.Count - 1], 6);
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }

	    TextBoxValues.Add(index, TextBox);
	    LabelValues.Add(index, Label);
	}

	public void AddMetaFields()
	{
	    try
	    {
		for (int k = 0; k < MetaType.Count; k += 1)
		{
		    AddMetaRow(k);
		}
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public readonly PictureBox MetaBarContainer = new PictureBox();
	public readonly PictureBox ButtonContainer = new PictureBox();

	public readonly Button About = new Button();
	public readonly Button Save = new Button();
	public readonly Button Load = new Button();

	private string CurrentFile = string.Empty;

	private void SaveFileData()
	{
	    try
	    {
		if (!File.Exists(CurrentFile))
		{
		    MessageBox.Show("The file that was specified before, does not exist!", "Oh no", MessageBoxButtons.OK, MessageBoxIcon.Error);
		    return;
		}

		try
		{
		    FileInfo FileInfo = new FileInfo(CurrentFile);

		    FileInfo.CreationTime = DateTime.Parse($"{TextBoxValues[0].Text}");
		    FileInfo.CreationTimeUtc = DateTime.Parse($"{TextBoxValues[1].Text}");
		    FileInfo.LastAccessTime = DateTime.Parse($"{TextBoxValues[2].Text}");
		    FileInfo.LastAccessTimeUtc = DateTime.Parse($"{TextBoxValues[3].Text}");
		    FileInfo.IsReadOnly = (TextBoxValues[4].Text.ToLower() == "true");
		}

		catch
		{
		    MessageBox.Show("The information supplied does not suit the standard format.  Please follow the format already present.  For example, repeat the Date-Time format and only use True or False for the Is Read Only box.", "Oh no", MessageBoxButtons.OK, MessageBoxIcon.Error);
		    return;
		}
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	private void LoadFileData()
	{
	    try
	    {
		using (var OpenFileDialog = new OpenFileDialog())
		{
		    OpenFileDialog.Filter = ("Executables (*.exe)|*.exe|Installer (*.msi)|*.msi|Any (experimental)|*.*");
		    OpenFileDialog.Title = ("Select A File");

		    OpenFileDialog.CheckFileExists = true;
		    OpenFileDialog.CheckPathExists = true;
		    OpenFileDialog.Multiselect = false;

		    var DialogResult = OpenFileDialog.ShowDialog();

		    if (DialogResult != DialogResult.OK)
		    {
			MessageBox.Show("You have canceled the operation.  You may reselect your file, if any.", "Oh no", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		    }

		    var FileInfo = new FileInfo(OpenFileDialog.FileName);

		    TextBoxValues[0].Text = $"{FileInfo.CreationTime}";
		    TextBoxValues[1].Text = $"{FileInfo.CreationTimeUtc}";
		    TextBoxValues[2].Text = $"{FileInfo.LastAccessTime}";
		    TextBoxValues[3].Text = $"{FileInfo.LastAccessTimeUtc}";
		    TextBoxValues[4].Text = $"{FileInfo.IsReadOnly}";

		    CurrentFile = OpenFileDialog.FileName;
		}
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	private void SetupTriggerEvents()
	{
	    try
	    {
		Save.Click += (s, e) =>
		{
		    if (CurrentFile != string.Empty)
		    {
			SaveFileData();
		    }
		};

		Load.Click += (s, e) =>
		{
		    LoadFileData();
		};

		About.Click += (s, e) =>
		{
		    MessageBox.Show("Usually I do not really do these type of things, but for now I must, this project is boring me because of its simplicity but also its difficulty.  The .NET Framework (which is used to put this together.) does not directly support the modification of file data.  Since there are many other products capable of modifying files in such a manner, I figured why not cancel the entire project but atleast make it work as intended at first.  So yea, here you go with a nearly useless tool.  But it was a nice project, I loved working on the design, it looks kind of smooth.  I am open for suggestions, if you have any, send a message to KvinneKraft@protonmail.com.  Regardless, I am going to head out now.  I still did spend about 5 days working on this, but yea.  Much love to all of you!  Please do not see this thing as an example, ahaha, it is not.", "Hey there!", MessageBoxButtons.OK, MessageBoxIcon.Information);
		};
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}

	public void BottomBar()
	{
	    try
	    {
		var MetaBarSize = new Size(DashDialog.Width, 30);
		var MetaBarLoca = new Point(0, DashDialog.Height - MetaBarSize.Height);
		var MetaBarBColor = DashDialog.MenuBar.Bar.BackColor;

		Control.Image(DashDialog, MetaBarContainer, MetaBarSize, MetaBarLoca, MetaBarBColor);

		var ContainerSize = new Size(230, 24);
		var ContainerLoca = new Point((MetaBarContainer.Width - ContainerSize.Width) / 2, (MetaBarContainer.Height - ContainerSize.Height) / 2);
		var ContainerBColor = MetaBarBColor;

		Control.Image(MetaBarContainer, ButtonContainer, ContainerSize, ContainerLoca, ContainerBColor);

		var ButtonSize = new Size(70, 24);
		var ButtonBColor = MetaBarBColor;
		var ButtonFColor = Color.White;

		var AboutLoca = new Point(140 + 20, 0);
		var SaveLoca = new Point(70 + 10, 0);
		var LoadLoca = new Point(0, 0);
		
		Control.Button(ButtonContainer, About, ButtonSize, AboutLoca, ButtonBColor, ButtonFColor, 1, 8, "About");
		Control.Button(ButtonContainer, Load, ButtonSize, LoadLoca, ButtonBColor, ButtonFColor, 1, 8, "Load");
		Control.Button(ButtonContainer, Save, ButtonSize, SaveLoca, ButtonBColor, ButtonFColor, 1, 8, "Save");

		foreach (Button Button in ButtonContainer.Controls)
		{
		    Tool.Round(Button, 6);
		}

		Tool.Round(ButtonContainer, 6);

		SetupTriggerEvents();
	    }

	    catch (Exception E)
	    {
		throw (GetFormat(E));
	    }
	}
    }
}
