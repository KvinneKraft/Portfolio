
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
    public class PUTPOSTGET
    {
	public enum HeaderType
	{
	    PUT = 1, POST = 2, GET = 3
	};

	/*type=Header Type; h=Custom Header; rua=Random User-Agent; w=Workers; cw=Core-Workers;*/
	public static void Launch(int type)
	{
	    switch ((HeaderType) type)
	    {
		case HeaderType.PUT:
		    break;
		case HeaderType.POST:
		    break;
		case HeaderType.GET:
		    break;
	    }
	}
    }
}
