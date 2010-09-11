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
			ShareManager.Instance.AddPath("u:\\Shareable", "Shareable");
			ShareManager.Instance.AddPath("c:\\Users\\Aarni\\My Documents\\My Music", "Music");
			MainServer.Instance.Start();
			
			RefreshSharesList();
			DoRefreshShares();
		}
		
		
		
		void SharesListDragDrop(object sender, DragEventArgs e)
		{
			DataObject dobj = e.Data as DataObject;
			StringCollection sc = dobj.GetFileDropList();
			foreach(string s in sc) {
				if(Directory.Exists(s)) {
					string defVpath = Path.GetFileName(Path.GetDirectoryName(s));
					string vpath = PromptWindow.Prompt("Virtual Path", "Enter the virtual path for the new share (or just accept the default if you feel like that)", defVpath);
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
	}
}
