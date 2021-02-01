
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
	private readonly DashDialog DashDialog = new DashDialog();

	public void StartApp()
	{


	    Application.Run(DashDialog);
	}
    }
}
