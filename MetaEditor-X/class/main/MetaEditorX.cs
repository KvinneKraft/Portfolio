// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MetaEditorX
{
    public partial class MetaEditorX
    {
	public readonly InitMetaEditorX Initialize = new InitMetaEditorX();

	public void Show()
	{
	    try
	    {
		Initialize.Component();
		Initialize.Container();
		Initialize.BottomBar();
	    }

	    catch (Exception E)
	    {
		ErrorHandler.JustDoIt(E);
	    }
	}
    }
}
