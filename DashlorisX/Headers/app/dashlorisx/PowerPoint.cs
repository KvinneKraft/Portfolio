// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

using DashlorisX.Properties;

namespace DashlorisX
{ 
    public class PowerPoint
    {
	readonly private List<Thread> workers = new List<Thread>();
	readonly private AttackLog LogLog = new AttackLog();

	private void SendHeader()
	{

	}

	public void StartAttack()
	{
	    // When receiving stop signal, abort current thread.
	    // Thread.CurrentThread.Abort();
	    // 
	    // Start waiting thread, when showdialog, run launch code.+
	    
	    LogLog.ShowDialog();
	    StopAttack();
	}

	public void StopAttack()
	{
	    DashlorisX.Launch.Text = "Stopping ....";

	    LogLog.Hide();

	    foreach (Thread worker in workers)
	    {
		worker.Abort();
	    }

	    workers.Clear();

	    DashlorisX.Launch.Text = "Launch";
	}
    }
}
