
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace GamePanelX
{
    public class DashConfig
    {
	private readonly DashControls Control = new DashControls();
	private readonly DashTools Tool = new DashTools();

	public readonly Dictionary<int, List<string>> GameData = new Dictionary<int, List<string>>();

	private void AddGameData(int id, List<string> data)
	{
	    try
	    {
		GameData.Add(id, data);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void VerifyConfigExistence()
	{
	    try
	    {
		try
		{
		    if (!Directory.Exists("gameData"))
		    {
			Directory.CreateDirectory("gameData");
		    }

		    if (!File.Exists(@"gameData\games.config"))
		    {
			File.WriteAllText(@"gameData\games.config", "# Only modify this file if you know what the fuck you are doing!");
		    }
		}

		catch
		{
		    throw (new Exception("i/o error"));
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void ReadGames()
	{
	    try
	    {
		GameData.Clear();

		VerifyConfigExistence();

		var rawData = File.ReadAllLines("gameData\\games.config").ToList();

		for (int g = 0, c = 1; g < rawData.Count;)
		{
		    if (rawData[g].StartsWith(" ") || rawData[g].StartsWith("#"))
		    {
			g += 1;
			continue;
		    }

		    if (rawData.Count >= g + 5)
		    {
			if (rawData[g].ToCharArray()[0].ToString() == c.ToString())
			{
			    AddGameData
			    (
				c,

				new List<string>()
				{
				    rawData[g + 1].Replace("parameters: ", string.Empty),
				    rawData[g + 2].Replace("file-name: ", string.Empty),
				    rawData[g + 3].Replace("game-name: ", string.Empty),
				    rawData[g + 4].Replace("directory: ", string.Empty),
				    rawData[g + 5].Replace("runas: ", string.Empty)
				}
			    );

			    g += 6;
			    c += 1;

			    continue;
			}
		    }

		    break;
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
	
	public readonly Dictionary<int, PictureBox> GameControls = new Dictionary<int, PictureBox>();

	private int GetSlotY(GamePanel GamePanel)
	{
	    try
	    {
		int y = 0;

		if (GamePanel.GameContainer.Controls.Count > 0)
		{
		    Control Control = GamePanel.GameContainer.Controls[GamePanel.GameContainer.Controls.Count - 1];
		    y = Control.Top + Control.Height;
		}

		return y;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private int GetCheckBoxY(GamePanel GamePanel)
	{
	    try
	    {
		int y = 0;

		if (GamePanel.CheckBoxContainer.Controls.Count > 0)
		{
		    Control Control = GamePanel.CheckBoxContainer.Controls[GamePanel.CheckBoxContainer.Controls.Count - 1];
		    y = Control.Top + Control.Height;
		}

		return y;
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void SetupColorCheckBox(PictureBox CheckBox, Color CheckBoxBColor)
	{
	    CheckBox.Click += (s, e) =>
	    {
		if (CheckBox.BackColor == Color.FromArgb(64, 64, 64))
		{
		    CheckBox.BackColor = CheckBoxBColor;
		}

		else
		{
		    CheckBox.BackColor = Color.FromArgb(64, 64, 64);
		}
	    };
	}

	private void AddGameSlot(int id, GamePanel GamePanel)
	{
	    try
	    {
		PictureBox GameContainer = new PictureBox();

		var GameSize = new Size(GamePanel.GameContainer.Width, 26);
		var GameLoca = new Point(0, GetSlotY(GamePanel));
		var GameBColor = GamePanel.GameContainer.BackColor;

		Control.Image(GamePanel.GameContainer, GameContainer, GameSize, GameLoca, GameBColor);

		Button Launch = new Button();
		Button Edit = new Button();

		var LaunchSize = new Size(GameContainer.Width - 75, GameSize.Height);
		var LaunchLoca = new Point(0, 0);

		var EditSize = new Size(75, GameSize.Height);
		var EditLoca = new Point(LaunchSize.Width, 0);

		var ButtonBColor = GameBColor;
		var ButtonFColor = Color.White;
		
		Control.Button(GameContainer, Launch, LaunchSize, LaunchLoca, ButtonBColor, ButtonFColor, 1, 10, GameData[id][2]);

		Launch.Click += (s, e) =>
		{
		    // Executioner by id.  gameData[<button id>][<option id>]
		    MessageBox.Show($"{id}");
		};

		Control.Button(GameContainer, Edit, EditSize, EditLoca, ButtonBColor, ButtonFColor, 1, 9, "Edit");

		Edit.Click += (s, e) =>
		{
		    MessageBox.Show($"{id}");
		};

		GameControls.Add(id, GameContainer);

		PictureBox CheckBoxContainer = new PictureBox();
		PictureBox CheckBox = new PictureBox();

		var CheckBoxContainerSize = new Size(28, 26);
		var CheckBoxContainerLoca = new Point((GamePanel.CheckBoxContainer.Width - CheckBoxContainerSize.Width) / 2, GetCheckBoxY(GamePanel));
		var CheckBoxContainerBColor = GamePanel.CheckBoxContainer.BackColor;

		var CheckBoxSize = new Size(16, 16);
		var CheckBoxLoca = new Point((CheckBoxContainerSize.Width - CheckBoxSize.Width) / 2, (CheckBoxContainerSize.Height - CheckBoxSize.Height) / 2);
		var CheckBoxBColor = Color.FromArgb(9, 39, 66);

		Control.Image(GamePanel.CheckBoxContainer, CheckBoxContainer, CheckBoxContainerSize, CheckBoxContainerLoca, CheckBoxContainerBColor);
		Control.Image(CheckBoxContainer, CheckBox, CheckBoxSize, CheckBoxLoca, CheckBoxBColor);
		
		SetupColorCheckBox(CheckBox, CheckBoxBColor);
	
		GameControls.Add(-id, CheckBoxContainer);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void ShowGames(GamePanel GamePanel)
	{
	    GamePanel.CheckBoxContainer.Show();
	    GamePanel.GameContainer.Show();
	}

	private void HideGames(GamePanel GamePanel)
	{
	    GamePanel.CheckBoxContainer.Hide();
	    GamePanel.GameContainer.Hide();
	}

	private readonly DashBox DashBox = new DashBox();

	private string ParseErrorMessage()
	{
	    return string.Format
	    (
		@"I am sorry, but apparently there was an error while trying to load the game-data from the configuration file at gameData\games.config.\r\n\r\n" +
		"Would you like to remove the invalid configuration file and place a new one? Execution will continue after decision making."
	    );
	}

	private void ReloadGameConfig()
	{
	    try
	    {
		Directory.Delete("gameData", true);
		VerifyConfigExistence();
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	private void HandleParseError()
	{
	    try
	    {
		var ContainerBColor = Color.FromArgb(9, 39, 66);
		var MenuBarBColor = Color.FromArgb(19, 36, 64);
		var AppBColor = Color.FromArgb(6, 17, 33);

		int Result = DashBox.Show(ParseErrorMessage(), "Dash Config Parse Error", AppBColor, MenuBarBColor, ContainerBColor, Color.White);

		if (Result == 1)
		{
		    ReloadGameConfig();
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}

	public void LoadGames(GamePanel GamePanel)
	{
	    try
	    {
		if (GameData.Count > 0)
		{
		    GameControls.Clear();

		    for (int k = 0; k < GameData.Count; k += 1)
		    {
			if (!GameData.ContainsKey(k + 1))
			{
			    HandleParseError();
			    break;
			}

			AddGameSlot(k + 1, GamePanel);
		    }

		    ShowGames(GamePanel);
		}

		else
		{
		    HideGames(GamePanel);
		}
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
