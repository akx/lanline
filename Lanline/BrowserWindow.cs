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
		}
		
		void RefreshFileListBtnClick(object sender, EventArgs e)
		{
			statusLabel.Text = "Requesting file list...";
			Refresh();
			try {
				host.RequestFileList();
			} catch(Exception exc) {
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
				string localDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string remotePath = selectedItem.Tag as string;
				string localPath = Path.Combine(localDir, Path.GetFileName(remotePath));
				if(MessageBox.Show("Download", "Download to " + localPath + "?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
					IncomingTransfer it = new IncomingTransfer(host, remotePath, localPath);
					XferManager.Instance.Track(it);
					it.Start();
				}
			}
		}
	}
}
