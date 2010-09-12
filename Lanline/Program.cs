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
using System.Runtime.CompilerServices;
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Main(string[] args)
		{
			try {
				Run();
			} catch(Exception exc) {
				MessageBox.Show("Error. :(\n\n" + exc.ToString());
			}
		}
		
		[MethodImpl(MethodImplOptions.NoInlining)]
		static void Run() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			/*
			 * Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomain_CurrentDomain_UnhandledException);
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			*/
			Application.Run(new MainForm());
			
		}

		static void AppDomain_CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs ea)
		{
			Logging.LogExceptionToFile(ea.ExceptionObject as Exception, "Uncaught application exception");
			MessageBox.Show(
					"An unhandled exception happened. :(\nDetails:\n\n" + ea.ExceptionObject.ToString()+"\n\nSaving this to lanline-exception.txt.",
					"Exception :(",
					MessageBoxButtons.OK,
					MessageBoxIcon.Stop
			);
		}

		static void Application_ThreadException(object sender, ThreadExceptionEventArgs ea)
		{
			Logging.LogExceptionToFile(ea.Exception, "Uncaught toplevel exception");
			MessageBox.Show(
					"An unhandled exception happened. :(\nDetails:\n\n" + ea.Exception.ToString()+"\n\nSaving this to lanline-exception.txt.",
					"Exception :(",
					MessageBoxButtons.OK,
					MessageBoxIcon.Stop
				);
			
		}
		
		
	}
}
