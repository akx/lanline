/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 15:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Lanline
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += delegate(object sender, ThreadExceptionEventArgs ea) {
				MessageBox.Show(
					"An unhandled exception happened. :(\nDetails:\n\n" + ea.Exception.ToString()+"\n\nSaving this to lanline-exception.txt.",
					"Exception :(",
					MessageBoxButtons.OK,
					MessageBoxIcon.Stop
				);
				Logging.LogExceptionToFile(ea.Exception, "Uncaught toplevel exception");
			};
			
			Application.Run(new MainForm());
			
		}
		
	}
}
