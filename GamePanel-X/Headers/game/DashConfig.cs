
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

		var RawData = File.ReadAllLines("gameData\\games.config").ToList();

		for (int g = 0, c = 1; g < RawData.Count;)
		{
		    if (RawData[g].StartsWith(" ") || RawData[g].StartsWith("#"))
		    {
			g += 1;
			continue;
		    }

		    if (RawData.Count >= g + 5)
		    {
			if (RawData[g].ToCharArray()[0].ToString() == c.ToString())
			{
			    AddGameData
			    (
				g,

				new List<string>()
				{
				    RawData[g + 1],
				    RawData[g + 2],
				    RawData[g + 3],
				    RawData[g + 4],
				    RawData[g + 5]
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

	public void LoadGames()
	{
	    try
	    {

	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
