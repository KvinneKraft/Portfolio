
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
    public partial class GamePanelX
    {
	private readonly DashConfig GameConfig = new DashConfig();
	private readonly GamePanel GamePanel = new GamePanel();

	public void StartApp()
	{
	    try
	    {
		GamePanel.initializeMainComponent();
		GamePanel.initializeMainContainer();

		GamePanel.CheckBoxContainer.Hide();
		GamePanel.GameContainer.Hide();

		GameConfig.ReadGames();
		GameConfig.LoadGames(GamePanel);

		Application.Run(GamePanel.DashDialog);
		Environment.Exit(-1);
	    }

	    catch (Exception E)
	    {
		throw (ErrorHandler.GetException(E));
	    }
	}
    }
}
