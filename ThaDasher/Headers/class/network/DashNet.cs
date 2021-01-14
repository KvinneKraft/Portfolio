
//
// Author: Dashie
// Version: 1.0
//
// Extensive Dash library for personal use only.  This code was not made for 
// abuse but rather for the right use.  This entire application started off as  
// a small project but turned into something next level.
//
// All methods are meant for stress-testing networks.  You are responsible for
// what you do with this, so use this code wisely.
//

using System;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class DashNet
    {
	readonly public MethodConfig MethodConfig = new MethodConfig();
	
	private static void SendMessage(string s)
	{
	    void print() => LogContainer.LOG.AppendText($"(+) {s}\r\n");

	    if (LogContainer.LOG.InvokeRequired)
	    {
		LogContainer.LOG.Invoke
		(
		    new MethodInvoker
		    (
			delegate () 
			{
			    print();
			}
		    )
		);
	    }

	    else
	    {
		print();
	    }
	}

	static public string host = string.Empty;

	static public int duration = 0;
	static public int port = 0;

	public void SendAttack(string shost, int sport, int sdura)
	{
	    host = shost;
	    duration = sdura;
	    port = sport;

	    var m = SettingsContainer.CURRENT_METHOD.ToLower();

	    switch (m)
	    {
		case "dashloris 4.0": DASHLORIS4.Launch(); break;
		case "slowloris 2.0": SLOWLORIS2.Launch();  break;
		case "put head": PUTPOSTGET.Launch(1); break;
		case "post head": PUTPOSTGET.Launch(2);  break;
		case "get head": PUTPOSTGET.Launch(3); break;
		case "h-flood": HFLOOD.Launch(); break;

		case "long socks": LONGSOCKS.Launch(); break;
		case "multi flood": MULTIFLOOD.Launch(); break;
		case "multi socks": MULTISOCKS.Launch(); break;
		case "wavesss": WAVESSS.Launch(); break;

		case "overload": OVERLOAD.Launch(); break;
		case "wavy baby": WAVYBABY.Launch(); break;
		case "go ham": GOHAM.Launch(); break;
		case "insta flood": INSTAFLOOD.Launch(); break;
	    };

	    // Change dis
	    StartWorkers();
	}

	readonly static public List<Thread> workers = new List<Thread>();

	public void StartWorkers()
	{
	    foreach (var worker in workers)
	    {
		worker.Start();
	    };

	    TaskbarContainer.START.Text = "Stop";
	}

	public void StopWorkers()
	{
	    foreach (var worker in workers)
	    {
		worker.Abort();
	    };

	    workers.Clear();

	    TaskbarContainer.START.Text = "Start";
	}
    }
}
