using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaEditorX
{
    public static class Startup
    {
	private static readonly MetaEditorX MetaEditorX = new MetaEditorX();

	[STAThread]
	public static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    MetaEditorX.Show();
	}
    }
}
