/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 15:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace Lanline
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			
			Logging.OnMessageLogged += delegate(string message) { 
				try {
					BeginInvoke(new MethodInvoker(delegate { statusLabel.Text = message; }));
				} catch(Exception) {
					
				}
			};
			try {
				SettingsManager.Instance.Load();
			} catch(Exception e) {
				Logging.LogExceptionToFile(e, "Setting load");
				MessageBox.Show("Oops! There was a problem loading the settings file. Reverting to defaults.");
			}
			SharingServer.Instance.Start();
			//ShareManager.Instance.AddPath("u:\\Shareable", "Shareable");
			//ShareManager.Instance.AddPath("c:\\Users\\Aarni\\My Documents\\My Music", "Music");			
			//NetworkManager.Instance.AddHost("127.0.0.1", NetworkManager.LANLINE_PORT, true);
			DoRefreshShares();
			RefreshSharesList();			
			RefreshXfersList();
			RefreshHostsList();
			Text = "Lanline " + Application.ProductVersion;
		}
		
		
		
		void SharesListDragDrop(object sender, DragEventArgs e)
		{
			DataObject dobj = e.Data as DataObject;
			StringCollection sc = dobj.GetFileDropList();
			foreach(string s in sc) {
				if(Directory.Exists(s)) {
					string defVpath = Path.GetFileName(s.TrimEnd('\\'));
					string vpath = PromptWindow.Prompt(
					    "Virtual Path",
					    "Enter the virtual path for the new share " + s + " (or just accept the default if you feel like that)",
					    defVpath
					);
					if(vpath != null) {
						if(!ShareManager.Instance.AddPath(s, vpath)) {
							MessageBox.Show("Could not add the path. Duplicate vpath, maybe?");
						}
					}
				}
			}
			RefreshSharesList();
		}
		
		void RefreshSharesList() {
			sharesList.Items.Clear();
			foreach(SharePath sp in ShareManager.Instance.GetShares()) {
				ListViewItem lvi = new ListViewItem(new string[]{
				                                    	sp.FsPath,
				                                    	sp.VPath,
				                                    	(sp.Valid ? "Valid" : "Invalid"),
				                                    	sp.FileCount.ToString()
				                                    });
				lvi.Tag = sp;
				sharesList.Items.Add(lvi);
			}
			sharesTab.Text = String.Format("Shares ({0})", ShareManager.Instance.TotalFiles);
			sharesList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}
		
		
		void SharesListQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			e.Action = DragAction.Drop;
		}
		
		void SharesListDragEnter(object sender, DragEventArgs e)
		{
			if((e.Data as DataObject).ContainsFileDropList()) e.Effect = DragDropEffects.Copy;
			
		}
		
		void RefreshSharesBtnClick(object sender, EventArgs e)
		{
			DoRefreshShares();
		}	
		
		void DoRefreshShares() {
			ShareManager.Instance.RefreshShares(delegate(object ssender, RunWorkerCompletedEventArgs ea) {
			                                    	Logging.Log("Share refresh completed. {0} total files, {1} MiB.", ShareManager.Instance.TotalFiles, ShareManager.Instance.TotalBytes / 1024 / 1024);
			                                    	BeginInvoke(new MethodInvoker(delegate{RefreshSharesList();}));
			                                    });
		}
		
		void RefreshXfersList() {
			xfersList.BeginUpdate();
			xfersList.Items.Clear();
			int nUp = 0, nDown = 0, nQueued = 0;
			foreach(Transfer tr in XferManager.Instance.EnumerateTransfers()) {
				int progBars = (int)Math.Round((tr.Status == TransferStatus.Completed ? 100 : tr.Progress) / 5.0);
				ListViewItem lvi = new ListViewItem(new string[]{
					(tr.Direction == TransferDirection.Out ? "^" : "V") + " " + tr.HostName,
					tr.File1,
					tr.GetProgString(),
					(tr.Status == TransferStatus.Busy ? "|".Repeat(progBars).PadRight(20, '.') : ""),
					tr.Direction.ToString(),
					tr.Status.ToString(),
					Math.Round(tr.GetAverageSpeed(), 1) + "K/s"
				});
				lvi.Tag = tr;
				xfersList.Items.Add(lvi);
				if(tr.Direction == TransferDirection.Out && tr.Status == TransferStatus.Busy) nUp++;
				if(tr.Direction == TransferDirection.In) {
					if(tr.Status == TransferStatus.Busy) nDown ++;
					if(tr.Status == TransferStatus.Idle) nQueued ++;
				}				
			}
			xfersList.EndUpdate();
			xfersTab.Text = String.Format("Xfers ({0}/{1} dn, {2} up)", nDown, nQueued, nUp);
		}
		
		void ClearCompletedXfersButtonClick(object sender, EventArgs e)
		{
			XferManager.Instance.ClearCompleted();
			RefreshXfersList();
		}
		
		void SharesListDoubleClick(object sender, EventArgs e)
		{
			ListViewItem lvi = null;
			try {
				lvi = (sender as ListView).SelectedItems[0];
			} catch(Exception) {
				return;
			}
			SharePath sp = (lvi.Tag) as SharePath;
			if(sp != null) {
				if(MessageBox.Show("Unshare path " + sp.FsPath + "?", "Unshare", MessageBoxButtons.YesNo) == DialogResult.Yes) {
					ShareManager.Instance.RemoveSharePath(sp);
					RefreshSharesList();
				}
			}
		}
		
		void RefreshHostsList() {
			hostsList.Items.Clear();
			hostsList.BeginUpdate();
			foreach(Host h in NetworkManager.Instance.EnumerateKnownHosts()) {
				ListViewItem lvi = new ListViewItem(new string[] {
				   h.Ip,
				   h.Name,
				   h.NFiles.ToString(),
				   h.ClientVersion,
				   (h.HasFileList ? "Yes" : "No")
		        });
				lvi.Tag = h;
				hostsList.Items.Add(lvi);
			}
			hostsList.EndUpdate();
			hostsList.AutoResizeColumns((hostsList.Items.Count > 0 ? ColumnHeaderAutoResizeStyle.ColumnContent : ColumnHeaderAutoResizeStyle.HeaderSize));
		}
		
		void ReverifyHostsBtnClick(object sender, EventArgs e)
		{
			BackgroundWorker bw = new BackgroundWorker();
			bw.WorkerReportsProgress = true;
			bw.WorkerSupportsCancellation = true;
			bw.DoWork += delegate(object senderBw, DoWorkEventArgs ev) {
				int i = 0;
				int n = NetworkManager.Instance.NHosts;
				foreach(Host h in NetworkManager.Instance.EnumerateKnownHosts()) {
					if((senderBw as BackgroundWorker).CancellationPending) break;
					h.Verify();
					i++;
					(senderBw as BackgroundWorker).ReportProgress((int)((i / (float)n) * 100));
				}
			};
			bw.RunWorkerCompleted += delegate(object senderBw, RunWorkerCompletedEventArgs ev) { 
				BeginInvoke(new MethodInvoker(delegate{RefreshHostsList();}));	
			};
			(new ProgressWindow("Reverifying hosts...", bw)).Show();
			bw.RunWorkerAsync();
		}
		
		void AddHostBtnClick(object sender, EventArgs e)
		{
			
			string ip = PromptWindow.Prompt("Add Host", "Enter host IP address:", "");
			if(ip == null) return;
			bool ok = false;
			while(!ok) {
				ok = (NetworkManager.Instance.AddHost(ip, NetworkManager.LANLINE_PORT, true) != null);
				if(!ok) {
					if(MessageBox.Show(
						"Could not verify that the host is alive.\n" + 
						"Are you sure Lanline is running on it?\n\n" +
						"Try the same host again?", "Oops.", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != 
						DialogResult.Yes
					) {
						break;
					}
				}
			}
			RefreshHostsList();
		}
		
		private Host GetListSelectedHost() {
			return (hostsList.SelectedItems.Count > 0 ? (hostsList.SelectedItems[0]).Tag as Host : null);
		}
		
		void ForgetHostToolStripMenuItemClick(object sender, EventArgs e)
		{
			Host selectedHost = GetListSelectedHost();
			Debug.Print(selectedHost.ToString());
			NetworkManager.Instance.RemoveKnownHost(selectedHost);
			RefreshHostsList();
		}
		
		
		void HostsListDoubleClick(object sender, EventArgs e)
		{
			Host selectedHost = GetListSelectedHost();
			if(selectedHost != null) {
				BrowserManager.Instance.Open(selectedHost);
			}
		}
		
		void UpdateTimerTick(object sender, EventArgs e)
		{
			if(StatusManager.Instance.GetAndLowerFlag(StatusFlag.XfersChanged)) {
				XferManager.Instance.StartQueuedConnections();
				RefreshXfersList();
			}
			if(StatusManager.Instance.GetAndLowerFlag(StatusFlag.NetworkChanged)) {
				RefreshHostsList();
			}
			if(StatusManager.Instance.GetAndLowerFlag(StatusFlag.SharesChanged)) {
				RefreshSharesList();
			}
		}
		
		void BrowseFilesToolStripMenuItemClick(object sender, EventArgs e)
		{
			Host selectedHost = GetListSelectedHost();
			if(selectedHost != null) {
				BrowserManager.Instance.Open(selectedHost);
			}
		}
		
		void RefreshSettingsPage() {
			defaultDownloadDirectoryBox.Text = SettingsManager.Instance.DefaultDownloadFolder;
		}
		
		void TabControl1Selected(object sender, TabControlEventArgs e)
		{
			if(e.TabPage == settingsTab) RefreshSettingsPage();
		}
		
		void BrowseDefaultDirButtonClick(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.SelectedPath = SettingsManager.Instance.DefaultDownloadFolder;
			if(fbd.ShowDialog() == DialogResult.OK) {
				if(Directory.Exists(fbd.SelectedPath)) {
					SettingsManager.Instance.DefaultDownloadFolder = fbd.SelectedPath;
					RefreshSettingsPage();
				}
			}
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			int dlCount, ulCount;
			XferManager.Instance.GetRunningCounts(out dlCount, out ulCount);
			string msg = "Are you sure you want to exit Lanline?";
			if(dlCount > 0) msg += "\nThere are running downloads.";
			if(ulCount > 0) msg += "\nThere are running uploads.";
			e.Cancel = (MessageBox.Show(msg, "Lanline", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No);
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			try {
				XferManager.Instance.StopAndClearAll();
			} catch(Exception) {
				
			}
			SettingsManager.Instance.Save();
		}
	}
}
