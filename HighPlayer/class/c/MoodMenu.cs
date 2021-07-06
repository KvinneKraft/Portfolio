// Author: Dashie
// Version: 1.0


using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using DashFramework.Interface.Controls;
using DashFramework.Interface.Tools;

using DashFramework.Erroring;
using DashFramework.Dialog;

namespace HighPlayer
{
    public class MoodMenu 
    {
        static readonly DashControls Controls = new DashControls();
        static readonly DashWindow Parent = new DashWindow();


        static readonly DashTools Tools = new DashTools();

        public class Init1 
        {
            
	    public void Initiate(DashWindow Inst)
	    {
		// Remove Title
	    }
        }


        public class Init2
        {
	    readonly DashPanel Panel1 = new DashPanel();//main surrounding container
	    readonly DashPanel Panel2 = new DashPanel();//inner textbox container
	    readonly DashPanel Panel3 = new DashPanel();//actual textbox container

	    readonly Label Label1 = new Label();

	    public void Initiate(DashWindow Inst)
	    {
		// Size of bottom bar will be 30 
	    }
	}


	public class Init3
	{
	    readonly DashPanel Panel1 = new DashPanel();//main bottom bar
	    readonly DashPanel Panel2 = new DashPanel();//bottom bar inner container

	    readonly Button Button1 = new Button();//new mood (prints format text to log)
	    readonly Button Button2 = new Button();//save current mood settings (also to config)
	    readonly Button Button3 = new Button();//

	    public void Initiate(DashWindow Inst, MainGUI Origin)
	    {
		// Size of bar will be 30 and the buttons will be 24
	    }
	}


	readonly Init1 Initialize1 = new Init1();
	readonly Init2 Initialize2 = new Init2();
	readonly Init3 Initialize3 = new Init3();

	public void Initiate(DashWindow inst, MainGUI origin)
        {
            try
            {
		Initialize1.Initiate(inst);
		Initialize2.Initiate(inst);
		Initialize3.Initiate(inst, origin);
	    }

            catch (Exception E) 
            {
                ErrorHandler.JustDoIt(E);
            }
        }

	public void Show() { Parent.Show(); Parent.BringToFront(); }
	public void Hide() { Parent.Hide(); Parent.SendToBack();  }
    }
}