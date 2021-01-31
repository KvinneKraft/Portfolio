// Author: Dashie
// Version: 1.0
//
// <description>
//

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Principal;
using System.Collections.Generic;

namespace DashlorisX
{
    public class DashSys
    {
	public bool IsPrivileged() =>
	    new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }
}
