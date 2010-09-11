/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 12.9.2010
 * Time: 0:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;

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
			get {
				return defaultDownloadFolder;
			}
			set {
				defaultDownloadFolder = value;
				Save();
			}
		}
		
		public string ComputerName {
			get { return computerName; }
		}
		
		
		
		SettingsManager() {
			Logging.Debug("Initialized new SettingsManager.");
			LoadDefaults();
		}
		
		void LoadDefaults() {
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
		
		string GetSettingsFilePath() {
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lanline.conf");
		}
		
		public void Save() {
			StringBuilder settingsSb = new StringBuilder();
			settingsSb.AppendLine("downloadFolder = " + defaultDownloadFolder);
			settingsSb.AppendLine("computerName = " + computerName);
			foreach(SharePath sp in ShareManager.Instance.GetShares()) {
				settingsSb.AppendLine("share:" + sp.VPath + " = " + sp.FsPath);
			}
			try {
				using(FileStream settingsFs = new FileStream(GetSettingsFilePath(), FileMode.OpenOrCreate)) {
					settingsFs.WriteUTF8(settingsSb.ToString());
				}
			} catch(Exception) {
				
			}
		}
		
		public void Load() {
			string filename = GetSettingsFilePath();
			if(!File.Exists(filename)) return;
			ShareManager.Instance.ClearShares();
			using(FileStream settingsFs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				TextReader tr = new StreamReader(settingsFs, Encoding.UTF8);
				while(true) {
					string line = tr.ReadLine();
					if(line == null) break;
					string key = line.Substring(0, line.IndexOf("=")).Trim();
					string value = line.Substring(line.IndexOf("=") + 1).Trim();
					if(key == "downloadFolder" && Directory.Exists(value)) defaultDownloadFolder = value;
					if(key == "computerName") computerName = value;
					if(key.StartsWith("share:")) {
						key = key.Substring(6);
						ShareManager.Instance.AddPath(value, key);
						StatusManager.Instance.RaiseFlag(StatusFlag.SharesChanged);
					}
				}
			}
		}
	}
}
