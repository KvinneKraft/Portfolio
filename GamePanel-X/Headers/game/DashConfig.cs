
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

	public void ReadGames()
	{
	    try
	    {
		GameData.Clear();

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

	// Use picturebox as button container.  Flexible integration.
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

	private void AddGameSlot(int id, GamePanel GamePanel)
	{
	    try
	    {
		PictureBox GameContainer = new PictureBox();

		var GameSize = new Size(GamePanel.GameContainer.Width, 26);
		var GameLoca = new Point(0, GetSlotY(GamePanel));
		var GameBColor = GamePanel.GameContainer.BackColor;

		Control.Image(GamePanel.GameContainer, GameContainer, GameSize, GameLoca, GameBColor);
		GameControls.Add(id, GameContainer);

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
			    // Missing brick error.
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
