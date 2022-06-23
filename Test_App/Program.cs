using System;
using System.Threading;
using System.Windows.Forms;

namespace Test_App;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		bool createdNew = true;
		using (new Mutex(initiallyOwned: true, "EZ5", out createdNew))
		{
			if (createdNew)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(defaultValue: false);
				Application.Run(new Form1());
			}
			else
			{
				Form1 form = new Form1();
				Application.OpenForms[form.Name].Activate();
			}
		}
	}
}
