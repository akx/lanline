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
		
		public ShareManager()
		{
			shares = new List<SharePath>();
		}
		
		public bool AddPath(string fsPath, string vPath) {
			foreach(SharePath s in shares) {
				if(s.VPath == vPath) {
					return false;
				}
			}
			shares.Add(new SharePath(fsPath, vPath));
			return true;
		}
		
		public IEnumerable<SharePath> GetShares() {
			foreach(SharePath sp in shares) yield return sp;
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
			while(true) {
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
				string prog = String.Format("{0} files found, {1} directories left to enumerate", filesFound, dirsOpen);
				bw.ReportProgress((int)((sharesDone / (float)shares.Count) * 100), prog);
				if(!stepDone) {
					break;
				}
			}
		}
	}
}
