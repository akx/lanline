/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 20:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lanline
{
	/// <summary>
	/// Description of BrowserWindow.
	/// </summary>
	public partial class BrowserWindow : Form
	{
		private Host host;
		public Host Host {
			get { return host; }
		}

		
		public BrowserWindow(Host host)
		{
			InitializeComponent();
			this.host = host;
			Text = string.Format("Browser: {0} ({1})", host.Ip, host.Name);
			//if(!host.HasFileList) RefreshFileListBtnClick(null, null);
			if(host.HasFileList) RefreshTree();
		}
		
		void RefreshTree() {
			
			string selectedNodePath = null;
			if(treeView1.SelectedNode != null) {
				selectedNodePath = treeView1.SelectedNode.FullPath;	
			}
			treeView1.Nodes.Clear();
			if(!host.HasFileList) return;
			Dictionary<string, TreeNode> nodeCache = new Dictionary<string, TreeNode>();
			HashSet<TreeNode> roots = new HashSet<TreeNode>();
			foreach(string s in host.FileList) {
				string[] parts = s.Split('\\');
				string path = "";
				TreeNode currentParent = null;
				TreeNode currentNode;
				for(int i = 0; i < parts.Length - 1; i++) {
					path = (path + "\\" + parts[i]).TrimStart('\\');
					if(nodeCache.ContainsKey(path)) {
						currentNode = nodeCache[path];
					} else {
						//System.Diagnostics.Debug.Print("Creating new node for path " + path);
						currentNode = new TreeNode(parts[i]);
						currentNode.Tag = path;
						currentNode.ContextMenuStrip = treeContextMenu;
						nodeCache[path] = currentNode;
					}
					if(i == 0) roots.Add(currentNode);
					if(currentParent != null && !currentParent.Nodes.Contains(currentNode)) {
						//System.Diagnostics.Debug.Print("Bechilding {0} to {1}", currentNode.Tag, currentParent.Tag);
						currentParent.Nodes.Add(currentNode);
					}
					currentParent = currentNode;
				}
			}
			treeView1.BeginUpdate();
			foreach(TreeNode r in roots) {
				//System.Diagnostics.Debug.Print("Root: {0}", r.Tag);
				r.Collapse(false);
				treeView1.Nodes.Add(r);
			}
			treeView1.EndUpdate();
			if(selectedNodePath != null && nodeCache.ContainsKey(selectedNodePath)) {
				nodeCache[selectedNodePath].EnsureVisible();
				treeView1.SelectedNode = nodeCache[selectedNodePath];
			}
			statusLabel.Text = String.Format("{0} directories, {1} files", nodeCache.Count, host.FileList.Length);
		}
		
		void RefreshFileListBtnClick(object sender, EventArgs e)
		{
			statusLabel.Text = "Requesting file list...";
			Refresh();
			try {
				host.RequestFileList();
			} catch(Exception) {
				host.InvalidateFileList();
				MessageBox.Show("Problem requesting file list.");	
			}
			RefreshTree();
			
		}
		
		void TreeView1BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;
			
		}
		
		void TreeView1NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			listView1.Items.Clear();
			listView1.BeginUpdate();
			TreeNode node = e.Node;
			string path = (node.Tag) as string;
			foreach(string s in host.FileList) {
				string pathPart = s.Substring(0, s.LastIndexOf('\\'));
				if(pathPart == path) {
					string[] parts = s.Split('$');
					ListViewItem lvi = new ListViewItem(new string[]{
					                                    	Path.GetFileName(parts[0]),
					                                    	Math.Round((float)(Convert.ToInt64(parts[1]) / 1024) / 1024.0, 2).ToString()
					                                    });
					lvi.Tag = parts[0];
					listView1.Items.Add(lvi);
				}
			}
			listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listView1.EndUpdate();
		}
		
		void ListView1DoubleClick(object sender, EventArgs e)
		{
			ListViewItem selectedItem = null;
			if ((sender as ListView).SelectedItems.Count > 0) {
				selectedItem = (sender as ListView).SelectedItems[0];
			}
			if(selectedItem != null) {
				string localDir = SettingsManager.Instance.DefaultDownloadFolder;
				string remotePath = selectedItem.Tag as string;
				string localPath = Path.Combine(localDir, Path.GetFileName(remotePath));
				if(!XferManager.Instance.CanDownloadToLocalFile(localPath)) {
					MessageBox.Show("Local file " + localPath + " exists or is already in download list, will not overwrite.");
					return;
				}
				if(MessageBox.Show("Download to " + localPath + "?", "Download", MessageBoxButtons.YesNo) == DialogResult.Yes) {
					XferManager.Instance.Track(new IncomingTransfer(host, remotePath, localPath));
				}
			}
		}
		
		void DownloadEntireFolderToolStripMenuItemClick(object sender, EventArgs e)
		{
			TreeNode node = treeView1.SelectedNode;
			if(node == null) {
				MessageBox.Show("No folder selected.");
				return;
			}
			string rootPath = node.Tag as string;
			int lastSlash = rootPath.LastIndexOf('\\');
			string rootPathLastPart = (lastSlash == -1 ? rootPath : rootPath.Substring(lastSlash)).Trim('\\');
			List<string> files = new List<string>();
			long totalSize = 0;
			foreach(string path in host.FileList) {
				if(path.StartsWith(rootPath)) {
					string[] parts = path.Split('$');
					files.Add(parts[0]);
					totalSize += (Convert.ToInt64(parts[1]));
				}
			}
			string msg = String.Format("You are about to download {0} files, totalling {1} MiB of data.\nContinue?", files.Count, Math.Round(totalSize / 1024 / 1024.0f, 2));
			if(MessageBox.Show(msg, "Mass Download", MessageBoxButtons.YesNo) == DialogResult.Yes) {
				FolderBrowserDialog fbd = new FolderBrowserDialog();
				fbd.SelectedPath = SettingsManager.Instance.DefaultDownloadFolder;
				fbd.ShowNewFolderButton = true;
				fbd.Description = "Select a folder to save the downloaded folder in.";
				if(fbd.ShowDialog() == DialogResult.OK) {
					foreach(string remotePath in files) {
						string relPath = Path.Combine(rootPathLastPart, remotePath.Substring(rootPath.Length + 1));
						string localPath = Path.Combine(fbd.SelectedPath, relPath);
						if(XferManager.Instance.CanDownloadToLocalFile(localPath)) XferManager.Instance.Track(new IncomingTransfer(host, remotePath, localPath));
					}
				}
			}
		}
	}
}
