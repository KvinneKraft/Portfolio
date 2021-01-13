
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
    public class DASHLORIS4
    {
	/*cd=Connection Delay; clt=Connection Live Time; ccc=Cycle Connection Count; rua=Random User-Agent*/
	public static void Launch()//(int cd, int clt, int ccc, bool rua)
	{
	    var clt = HTTP.DASHLORIS4.CONNECTION_LIVE_TIME;
	    var ccc = HTTP.DASHLORIS4.CYCLE_CONNECTION_COUNT;
	    var rua = HTTP.DASHLORIS4.RANDOM_USER_AGENT;
	    var cbd = HTTP.DASHLORIS4.CONNECTION_BURST_DELAY;
	    var byt = HTTP.DASHLORIS4.BYTES;

	    //^^^
	}
    }
}
