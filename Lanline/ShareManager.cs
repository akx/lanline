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
using System.ComponentModel;
using System.Globalization;

namespace Lanline
{
	/// <summary>
	/// Description of ShareManager.
	/// </summary>
	public class ShareManager
	{
		private static ShareManager _instance;

		public static ShareManager Instance {
			get { if(_instance == null) {
					_instance = new ShareManager();
				}
				return _instance;
			}
		}
		
		private List<SharePath> shares;
		
		public int TotalFiles {
			get {
				int i = 0;
				foreach(SharePath sp in shares) if(sp.Valid) i += sp.FileCount;
				return i;
			}
		}
		
		public long TotalBytes {
			get {
				long bytes = 0;
				foreach(SharePath sp in shares) if(sp.Valid) bytes += sp.TotalBytes;
				return bytes;
			}
		}
		
		public ShareManager()
		{
			shares = new List<SharePath>();
		}
		
		public bool AddPath(string fsPath, string vPath) {
			vPath = vPath.Replace("/", "_").ToLowerInvariant();
			foreach(SharePath s in shares) {
				if(s.VPath == vPath) {
					return false;
				}
			}
			shares.Add(new SharePath(fsPath, vPath));
			StatusManager.Instance.RaiseFlag(StatusFlag.SharesChanged);
			return true;
		}
		
		public IEnumerable<SharePath> GetShares() {
			foreach(SharePath sp in shares) yield return sp;
		}
		
		public void RemoveSharePath(SharePath sp) {
			if(shares.Contains(sp)) {
				shares.Remove(sp);
				StatusManager.Instance.RaiseFlag(StatusFlag.SharesChanged);
			}
		}
		
		public void ClearShares() {
			shares.Clear();
			StatusManager.Instance.RaiseFlag(StatusFlag.SharesChanged);
		}
		
		public void RefreshShares(RunWorkerCompletedEventHandler onComplete) {
			foreach(SharePath sp in shares) {
				sp.BeginRecache();
			}
			BackgroundWorker bw = new BackgroundWorker();
			bw.WorkerReportsProgress = true;
			bw.WorkerSupportsCancellation = true;
			bw.DoWork += new DoWorkEventHandler(bw_DoWork);
			if(onComplete != null) bw.RunWorkerCompleted += onComplete;
			(new ProgressWindow("Share Refresh", bw)).Show();
			
			bw.RunWorkerAsync();
			
		}

		void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker bw = sender as BackgroundWorker;
			int maxDirsOpen = 0;
			while(true) {
				if(bw.CancellationPending) break;
				bool stepDone = false;
				int sharesDone = 0;
				int filesFound = 0;
				int dirsOpen = 0;
				foreach(SharePath sp in shares) {
					filesFound += sp.FileCount;
					dirsOpen += sp.DirQueueLength;
					if(sp.RefreshInProgress) {
						sp.DoRefreshStep();
						stepDone = true;
						break;
					} else {
						sharesDone ++;
					}
				}
				maxDirsOpen = Math.Max(dirsOpen, maxDirsOpen);
				string prog = String.Format("{0} files found, {1} directories left to enumerate", filesFound, dirsOpen);
				float leftHere = (maxDirsOpen > 0 ? 1.0f - (dirsOpen / (float)maxDirsOpen) : 0);
				float progr = (sharesDone / (float)shares.Count) + (leftHere / ((float)shares.Count + 1));
				if(progr<0) progr=0;
				if(progr>1) progr=1;
				bw.ReportProgress((int)(progr * 100), prog);
				if(!stepDone) {
					break;
				}
			}
			StatusManager.Instance.RaiseFlag(StatusFlag.SharesChanged);
		}
		
		public string[] GetFullVFileList() {
			List<string> files = new List<string>();
			foreach(SharePath sp in shares) {
				lock(sp) {
					foreach(ShareFileInfo sfi in sp.EnumerateFiles()) {
						files.Add(sp.VPath + "\\" + sfi.relativeVPath + "$" + sfi.size.ToString(CultureInfo.InvariantCulture));
					}
				}
			}
			return files.ToArray();
		}
		
		public ShareFileInfo ResolveVPath(string vPath) {
			//Logging.Debug("Resolving vpath for " + vPath);
			string[] vPathParts = vPath.Split(new char[]{'/'}, 2);
			string vRoot = vPathParts[0];
			string relPath = vPathParts[1].Replace("/", "\\");
			foreach(SharePath sp in shares) {
				if(sp.VPath == vRoot) { // find vpath root {
					//System.Diagnostics.Debug.Print("vroot found, looking for relpath {0}", relPath);//Resolving vpath for " + vPath);
					foreach(ShareFileInfo sfi in sp.EnumerateFiles()) {
						if(sfi.relativeVPath == relPath) return sfi;
					}
				}
			}
			return null;
		}
		
	}
}
