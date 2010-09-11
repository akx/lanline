/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 15:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace Lanline
{
	/// <summary>
	/// Description of SharePath.
	/// </summary>
	/// 
	
	public class ShareFileInfo {
		public string absoluteFsPath;
		public string relativeVPath;
		public long size;
		public ShareFileInfo(string absoluteFsPath, string relativeVPath, long size)
		{
			this.absoluteFsPath = absoluteFsPath;
			this.relativeVPath = relativeVPath;
			this.size = size;
		}
		
	}
	
	public class SharePath
	{
		string fsPath;
		string vPath;
		bool valid;
		bool refreshInProgress;
		List<ShareFileInfo> files;
		Queue<DirectoryInfo> directoryQueue;
		
		
		
		public string FsPath {
			get { return fsPath; }
		}
		
		public string VPath {
			get { return vPath; }
		}
		
		public bool Valid {
			get { return valid; }
		}
		
		public bool RefreshInProgress {
			get { return refreshInProgress; }
		}
		
		public long TotalBytes {
			get {
				long bytes = 0;
				foreach(ShareFileInfo sfi in files) bytes += sfi.size;
				return bytes;
			}
		}
		
		
		public SharePath(string fsPath, string vPath)
		{
			this.fsPath = Path.GetFullPath(fsPath);
			this.vPath = vPath;	
			files = new List<ShareFileInfo>();
			directoryQueue = new Queue<DirectoryInfo>();
			valid = false;
			refreshInProgress = false;
			
		}
		
		public void BeginRecache() {
			valid = Directory.Exists(fsPath);
			files = new List<ShareFileInfo>();
			directoryQueue = new Queue<DirectoryInfo>();
			
			if(valid) {
				refreshInProgress = true;
				directoryQueue.Enqueue(new DirectoryInfo(fsPath));
			} else {
				refreshInProgress = false;
			}
		}
		
		public void DoRefreshStep() {
			if(directoryQueue.Count <= 0) {
				refreshInProgress = false;
				return;
			}
			DirectoryInfo di = directoryQueue.Dequeue();
			lock(this) {
				foreach(FileInfo fi in di.GetFiles()) {
					
					if((fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
					if((fi.Attributes & FileAttributes.System) == FileAttributes.System) continue;
					if(fi.FullName.IndexOf('$') > -1) continue;
					string relVpath = Path.GetFullPath(fi.FullName).Replace(fsPath, "").Trim('\\');
					files.Add(new ShareFileInfo(fi.FullName, relVpath, fi.Length));
				}
				foreach(DirectoryInfo sdi in di.GetDirectories()) {
					directoryQueue.Enqueue(sdi);
				}
			}
		}
		
		public int FileCount { get { return files.Count; } }
		public int DirQueueLength { get { return directoryQueue.Count; } }
		
		public IEnumerable<ShareFileInfo> EnumerateFiles() {
			foreach(ShareFileInfo sfi in files) yield return sfi;
		}
		
	}
	
	
}
