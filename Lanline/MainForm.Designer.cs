/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 15:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Lanline
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ColumnHeader columnHeader1;
			System.Windows.Forms.ColumnHeader columnHeader2;
			System.Windows.Forms.ColumnHeader columnHeader3;
			System.Windows.Forms.ColumnHeader Path;
			System.Windows.Forms.ColumnHeader Vpath;
			System.Windows.Forms.ColumnHeader columnHeader4;
			System.Windows.Forms.ColumnHeader columnHeader5;
			System.Windows.Forms.ColumnHeader columnHeader6;
			System.Windows.Forms.ColumnHeader columnHeader7;
			System.Windows.Forms.ColumnHeader columnHeader8;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.hostsPage = new System.Windows.Forms.TabPage();
			this.hostsList = new System.Windows.Forms.ListView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.addHost = new System.Windows.Forms.ToolStripButton();
			this.discoverBtn = new System.Windows.Forms.ToolStripButton();
			this.sharesTab = new System.Windows.Forms.TabPage();
			this.sharesList = new System.Windows.Forms.ListView();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.refreshSharesBtn = new System.Windows.Forms.ToolStripButton();
			this.xfersTab = new System.Windows.Forms.TabPage();
			this.xfersList = new System.Windows.Forms.ListView();
			columnHeader1 = new System.Windows.Forms.ColumnHeader();
			columnHeader2 = new System.Windows.Forms.ColumnHeader();
			columnHeader3 = new System.Windows.Forms.ColumnHeader();
			Path = new System.Windows.Forms.ColumnHeader();
			Vpath = new System.Windows.Forms.ColumnHeader();
			columnHeader4 = new System.Windows.Forms.ColumnHeader();
			columnHeader5 = new System.Windows.Forms.ColumnHeader();
			columnHeader6 = new System.Windows.Forms.ColumnHeader();
			columnHeader7 = new System.Windows.Forms.ColumnHeader();
			columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.statusStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.hostsPage.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.sharesTab.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.xfersTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// columnHeader1
			// 
			columnHeader1.Text = "File";
			// 
			// columnHeader2
			// 
			columnHeader2.Text = "%";
			// 
			// columnHeader3
			// 
			columnHeader3.Text = "Host";
			// 
			// Path
			// 
			Path.Text = "Path";
			// 
			// Vpath
			// 
			Vpath.Text = "Vpath";
			// 
			// columnHeader4
			// 
			columnHeader4.Text = "Host";
			// 
			// columnHeader5
			// 
			columnHeader5.Text = "Name";
			// 
			// columnHeader6
			// 
			columnHeader6.Text = "Files";
			// 
			// columnHeader7
			// 
			columnHeader7.Text = "Validity";
			// 
			// columnHeader8
			// 
			columnHeader8.Text = "#Files";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.statusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 369);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(273, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.hostsPage);
			this.tabControl1.Controls.Add(this.sharesTab);
			this.tabControl1.Controls.Add(this.xfersTab);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(273, 369);
			this.tabControl1.TabIndex = 3;
			// 
			// hostsPage
			// 
			this.hostsPage.Controls.Add(this.hostsList);
			this.hostsPage.Controls.Add(this.toolStrip1);
			this.hostsPage.Location = new System.Drawing.Point(4, 22);
			this.hostsPage.Name = "hostsPage";
			this.hostsPage.Padding = new System.Windows.Forms.Padding(3);
			this.hostsPage.Size = new System.Drawing.Size(265, 343);
			this.hostsPage.TabIndex = 0;
			this.hostsPage.Text = "Network";
			this.hostsPage.UseVisualStyleBackColor = true;
			// 
			// hostsList
			// 
			this.hostsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									columnHeader4,
									columnHeader5,
									columnHeader6});
			this.hostsList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hostsList.Location = new System.Drawing.Point(3, 28);
			this.hostsList.Name = "hostsList";
			this.hostsList.Size = new System.Drawing.Size(259, 312);
			this.hostsList.TabIndex = 5;
			this.hostsList.UseCompatibleStateImageBehavior = false;
			this.hostsList.View = System.Windows.Forms.View.Details;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.addHost,
									this.discoverBtn});
			this.toolStrip1.Location = new System.Drawing.Point(3, 3);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(259, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// addHost
			// 
			this.addHost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.addHost.Image = ((System.Drawing.Image)(resources.GetObject("addHost.Image")));
			this.addHost.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addHost.Name = "addHost";
			this.addHost.Size = new System.Drawing.Size(55, 22);
			this.addHost.Text = "Add Host";
			// 
			// discoverBtn
			// 
			this.discoverBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.discoverBtn.Image = ((System.Drawing.Image)(resources.GetObject("discoverBtn.Image")));
			this.discoverBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.discoverBtn.Name = "discoverBtn";
			this.discoverBtn.Size = new System.Drawing.Size(81, 22);
			this.discoverBtn.Text = "Discover Hosts";
			// 
			// sharesTab
			// 
			this.sharesTab.Controls.Add(this.sharesList);
			this.sharesTab.Controls.Add(this.toolStrip2);
			this.sharesTab.Location = new System.Drawing.Point(4, 22);
			this.sharesTab.Name = "sharesTab";
			this.sharesTab.Padding = new System.Windows.Forms.Padding(3);
			this.sharesTab.Size = new System.Drawing.Size(265, 343);
			this.sharesTab.TabIndex = 1;
			this.sharesTab.Text = "Shares";
			this.sharesTab.UseVisualStyleBackColor = true;
			// 
			// sharesList
			// 
			this.sharesList.AllowDrop = true;
			this.sharesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									Path,
									Vpath,
									columnHeader7,
									columnHeader8});
			this.sharesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sharesList.Location = new System.Drawing.Point(3, 28);
			this.sharesList.Name = "sharesList";
			this.sharesList.Size = new System.Drawing.Size(259, 312);
			this.sharesList.TabIndex = 2;
			this.sharesList.UseCompatibleStateImageBehavior = false;
			this.sharesList.View = System.Windows.Forms.View.Details;
			this.sharesList.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.SharesListQueryContinueDrag);
			this.sharesList.DragDrop += new System.Windows.Forms.DragEventHandler(this.SharesListDragDrop);
			this.sharesList.DragEnter += new System.Windows.Forms.DragEventHandler(this.SharesListDragEnter);
			// 
			// toolStrip2
			// 
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.refreshSharesBtn});
			this.toolStrip2.Location = new System.Drawing.Point(3, 3);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(259, 25);
			this.toolStrip2.TabIndex = 1;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// refreshSharesBtn
			// 
			this.refreshSharesBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.refreshSharesBtn.Image = ((System.Drawing.Image)(resources.GetObject("refreshSharesBtn.Image")));
			this.refreshSharesBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshSharesBtn.Name = "refreshSharesBtn";
			this.refreshSharesBtn.Size = new System.Drawing.Size(106, 22);
			this.refreshSharesBtn.Text = "Refresh Shared Files";
			this.refreshSharesBtn.Click += new System.EventHandler(this.RefreshSharesBtnClick);
			// 
			// xfersTab
			// 
			this.xfersTab.Controls.Add(this.xfersList);
			this.xfersTab.Location = new System.Drawing.Point(4, 22);
			this.xfersTab.Name = "xfersTab";
			this.xfersTab.Padding = new System.Windows.Forms.Padding(3);
			this.xfersTab.Size = new System.Drawing.Size(265, 343);
			this.xfersTab.TabIndex = 2;
			this.xfersTab.Text = "Xfers";
			this.xfersTab.UseVisualStyleBackColor = true;
			// 
			// xfersList
			// 
			this.xfersList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									columnHeader1,
									columnHeader2,
									columnHeader3});
			this.xfersList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xfersList.Location = new System.Drawing.Point(3, 3);
			this.xfersList.Name = "xfersList";
			this.xfersList.Size = new System.Drawing.Size(259, 337);
			this.xfersList.TabIndex = 0;
			this.xfersList.UseCompatibleStateImageBehavior = false;
			this.xfersList.View = System.Windows.Forms.View.Details;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(273, 391);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.Name = "MainForm";
			this.Text = "Lanline";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.hostsPage.ResumeLayout(false);
			this.hostsPage.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.sharesTab.ResumeLayout(false);
			this.sharesTab.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.xfersTab.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.ToolStripButton refreshSharesBtn;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ListView xfersList;
		private System.Windows.Forms.TabPage xfersTab;
		private System.Windows.Forms.ListView sharesList;
		private System.Windows.Forms.TabPage sharesTab;
		private System.Windows.Forms.TabPage hostsPage;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ToolStripButton discoverBtn;
		private System.Windows.Forms.ToolStripButton addHost;
		private System.Windows.Forms.ListView hostsList;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStrip toolStrip1;
	}
}
