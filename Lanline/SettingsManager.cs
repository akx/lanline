/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 12.9.2010
 * Time: 0:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Lanline
{
	/// <summary>
	/// Description of SettingsManager.
	/// </summary>
	public class SettingsManager
	{
		private static SettingsManager _instance;

		public static SettingsManager Instance {
			get { return (_instance = _instance ?? new SettingsManager());
			}
		}
		
		string defaultDownloadFolder;
		string computerName;
		
		public string DefaultDownloadFolder {
			get { return defaultDownloadFolder; }
			set { defaultDownloadFolder = value; }
		}
		
		public string ComputerName {
			get { return computerName; }
		}
		
		
		
		SettingsManager() {
			try {
				defaultDownloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			} catch(Exception) {
				defaultDownloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			}
			
			try {
				computerName = System.Environment.MachineName;
			} catch(Exception) {
				computerName = "(unnamed)";
			}
		}
	}
}
