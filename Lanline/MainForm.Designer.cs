﻿/*
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ColumnHeader Path;
			System.Windows.Forms.ColumnHeader Vpath;
			System.Windows.Forms.ColumnHeader columnHeader4;
			System.Windows.Forms.ColumnHeader columnHeader5;
			System.Windows.Forms.ColumnHeader columnHeader6;
			System.Windows.Forms.ColumnHeader columnHeader7;
			System.Windows.Forms.ColumnHeader columnHeader8;
			System.Windows.Forms.ColumnHeader fileHdr;
			System.Windows.Forms.ColumnHeader percHdr;
			System.Windows.Forms.ColumnHeader hostHdr;
			System.Windows.Forms.ColumnHeader progressHdr;
			System.Windows.Forms.ColumnHeader columnHeader1;
			System.Windows.Forms.ColumnHeader columnHeader2;
			System.Windows.Forms.ColumnHeader dirctionHdr;
			System.Windows.Forms.ColumnHeader statusHdr;
			System.Windows.Forms.ColumnHeader speedHdr;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.hostsPage = new System.Windows.Forms.TabPage();
			this.hostsList = new System.Windows.Forms.ListView();
			this.hostContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.browseFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.forgetHostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.netToolStrip = new System.Windows.Forms.ToolStrip();
			this.addHostBtn = new System.Windows.Forms.ToolStripButton();
			this.discoverBtn = new System.Windows.Forms.ToolStripButton();
			this.reverifyHostsBtn = new System.Windows.Forms.ToolStripButton();
			this.sharesTab = new System.Windows.Forms.TabPage();
			this.sharesList = new System.Windows.Forms.ListView();
			this.sharesToolStrip = new System.Windows.Forms.ToolStrip();
			this.refreshSharesBtn = new System.Windows.Forms.ToolStripButton();
			this.xfersTab = new System.Windows.Forms.TabPage();
			this.xfersList = new System.Windows.Forms.ListView();
			this.xfersToolStrip = new System.Windows.Forms.ToolStrip();
			this.clearCompletedXfersButton = new System.Windows.Forms.ToolStripButton();
			this.settingsTab = new System.Windows.Forms.TabPage();
			this.browseDefaultDirButton = new System.Windows.Forms.Button();
			this.defaultDownloadDirectoryBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.updateTimer = new System.Windows.Forms.Timer(this.components);
			Path = new System.Windows.Forms.ColumnHeader();
			Vpath = new System.Windows.Forms.ColumnHeader();
			columnHeader4 = new System.Windows.Forms.ColumnHeader();
			columnHeader5 = new System.Windows.Forms.ColumnHeader();
			columnHeader6 = new System.Windows.Forms.ColumnHeader();
			columnHeader7 = new System.Windows.Forms.ColumnHeader();
			columnHeader8 = new System.Windows.Forms.ColumnHeader();
			fileHdr = new System.Windows.Forms.ColumnHeader();
			percHdr = new System.Windows.Forms.ColumnHeader();
			hostHdr = new System.Windows.Forms.ColumnHeader();
			progressHdr = new System.Windows.Forms.ColumnHeader();
			columnHeader1 = new System.Windows.Forms.ColumnHeader();
			columnHeader2 = new System.Windows.Forms.ColumnHeader();
			dirctionHdr = new System.Windows.Forms.ColumnHeader();
			statusHdr = new System.Windows.Forms.ColumnHeader();
			speedHdr = new System.Windows.Forms.ColumnHeader();
			this.statusStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.hostsPage.SuspendLayout();
			this.hostContextMenu.SuspendLayout();
			this.netToolStrip.SuspendLayout();
			this.sharesTab.SuspendLayout();
			this.sharesToolStrip.SuspendLayout();
			this.xfersTab.SuspendLayout();
			this.xfersToolStrip.SuspendLayout();
			this.settingsTab.SuspendLayout();
			this.SuspendLayout();
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
			columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader7
			// 
			columnHeader7.Text = "Validity";
			// 
			// columnHeader8
			// 
			columnHeader8.Text = "#Files";
			// 
			// fileHdr
			// 
			fileHdr.Text = "File";
			// 
			// percHdr
			// 
			percHdr.Text = "%";
			percHdr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// hostHdr
			// 
			hostHdr.Text = "Host";
			// 
			// progressHdr
			// 
			progressHdr.Text = "Progress";
			// 
			// columnHeader1
			// 
			columnHeader1.Text = "Version";
			// 
			// columnHeader2
			// 
			columnHeader2.Text = "Got Filelist?";
			columnHeader2.Width = 75;
			// 
			// dirctionHdr
			// 
			dirctionHdr.Text = "Dir";
			// 
			// statusHdr
			// 
			statusHdr.Text = "Status";
			// 
			// speedHdr
			// 
			speedHdr.Text = "Speed";
			speedHdr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.statusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 429);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(643, 22);
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
			this.tabControl1.Controls.Add(this.settingsTab);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(643, 429);
			this.tabControl1.TabIndex = 3;
			this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1Selected);
			// 
			// hostsPage
			// 
			this.hostsPage.Controls.Add(this.hostsList);
			this.hostsPage.Controls.Add(this.netToolStrip);
			this.hostsPage.Location = new System.Drawing.Point(4, 24);
			this.hostsPage.Name = "hostsPage";
			this.hostsPage.Size = new System.Drawing.Size(635, 401);
			this.hostsPage.TabIndex = 0;
			this.hostsPage.Text = "Network";
			this.hostsPage.UseVisualStyleBackColor = true;
			// 
			// hostsList
			// 
			this.hostsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									columnHeader4,
									columnHeader5,
									columnHeader6,
									columnHeader1,
									columnHeader2});
			this.hostsList.ContextMenuStrip = this.hostContextMenu;
			this.hostsList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hostsList.FullRowSelect = true;
			this.hostsList.GridLines = true;
			this.hostsList.Location = new System.Drawing.Point(0, 25);
			this.hostsList.Margin = new System.Windows.Forms.Padding(0);
			this.hostsList.Name = "hostsList";
			this.hostsList.Size = new System.Drawing.Size(635, 376);
			this.hostsList.TabIndex = 5;
			this.hostsList.UseCompatibleStateImageBehavior = false;
			this.hostsList.View = System.Windows.Forms.View.Details;
			this.hostsList.DoubleClick += new System.EventHandler(this.HostsListDoubleClick);
			// 
			// hostContextMenu
			// 
			this.hostContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.browseFilesToolStripMenuItem,
									this.forgetHostToolStripMenuItem});
			this.hostContextMenu.Name = "hostContextMenu";
			this.hostContextMenu.Size = new System.Drawing.Size(140, 48);
			// 
			// browseFilesToolStripMenuItem
			// 
			this.browseFilesToolStripMenuItem.Name = "browseFilesToolStripMenuItem";
			this.browseFilesToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.browseFilesToolStripMenuItem.Text = "Browse Files";
			this.browseFilesToolStripMenuItem.Click += new System.EventHandler(this.BrowseFilesToolStripMenuItemClick);
			// 
			// forgetHostToolStripMenuItem
			// 
			this.forgetHostToolStripMenuItem.Name = "forgetHostToolStripMenuItem";
			this.forgetHostToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.forgetHostToolStripMenuItem.Text = "Forget Host";
			this.forgetHostToolStripMenuItem.Click += new System.EventHandler(this.ForgetHostToolStripMenuItemClick);
			// 
			// netToolStrip
			// 
			this.netToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.netToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.addHostBtn,
									this.discoverBtn,
									this.reverifyHostsBtn});
			this.netToolStrip.Location = new System.Drawing.Point(0, 0);
			this.netToolStrip.Name = "netToolStrip";
			this.netToolStrip.Size = new System.Drawing.Size(635, 25);
			this.netToolStrip.TabIndex = 4;
			this.netToolStrip.Text = "toolStrip1";
			// 
			// addHostBtn
			// 
			this.addHostBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.addHostBtn.Image = ((System.Drawing.Image)(resources.GetObject("addHostBtn.Image")));
			this.addHostBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addHostBtn.Name = "addHostBtn";
			this.addHostBtn.Size = new System.Drawing.Size(55, 22);
			this.addHostBtn.Text = "Add Host";
			this.addHostBtn.Click += new System.EventHandler(this.AddHostBtnClick);
			// 
			// discoverBtn
			// 
			this.discoverBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.discoverBtn.Enabled = false;
			this.discoverBtn.Image = ((System.Drawing.Image)(resources.GetObject("discoverBtn.Image")));
			this.discoverBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.discoverBtn.Name = "discoverBtn";
			this.discoverBtn.Size = new System.Drawing.Size(81, 22);
			this.discoverBtn.Text = "Discover Hosts";
			// 
			// reverifyHostsBtn
			// 
			this.reverifyHostsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.reverifyHostsBtn.Image = ((System.Drawing.Image)(resources.GetObject("reverifyHostsBtn.Image")));
			this.reverifyHostsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.reverifyHostsBtn.Name = "reverifyHostsBtn";
			this.reverifyHostsBtn.Size = new System.Drawing.Size(114, 22);
			this.reverifyHostsBtn.Text = "Recheck Known Hosts";
			this.reverifyHostsBtn.Click += new System.EventHandler(this.ReverifyHostsBtnClick);
			// 
			// sharesTab
			// 
			this.sharesTab.Controls.Add(this.sharesList);
			this.sharesTab.Controls.Add(this.sharesToolStrip);
			this.sharesTab.Location = new System.Drawing.Point(4, 24);
			this.sharesTab.Name = "sharesTab";
			this.sharesTab.Size = new System.Drawing.Size(635, 401);
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
			this.sharesList.FullRowSelect = true;
			this.sharesList.GridLines = true;
			this.sharesList.Location = new System.Drawing.Point(0, 25);
			this.sharesList.Name = "sharesList";
			this.sharesList.Size = new System.Drawing.Size(635, 376);
			this.sharesList.TabIndex = 2;
			this.sharesList.UseCompatibleStateImageBehavior = false;
			this.sharesList.View = System.Windows.Forms.View.Details;
			this.sharesList.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.SharesListQueryContinueDrag);
			this.sharesList.DoubleClick += new System.EventHandler(this.SharesListDoubleClick);
			this.sharesList.DragDrop += new System.Windows.Forms.DragEventHandler(this.SharesListDragDrop);
			this.sharesList.DragEnter += new System.Windows.Forms.DragEventHandler(this.SharesListDragEnter);
			// 
			// sharesToolStrip
			// 
			this.sharesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.sharesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.refreshSharesBtn});
			this.sharesToolStrip.Location = new System.Drawing.Point(0, 0);
			this.sharesToolStrip.Name = "sharesToolStrip";
			this.sharesToolStrip.Size = new System.Drawing.Size(635, 25);
			this.sharesToolStrip.TabIndex = 1;
			this.sharesToolStrip.Text = "toolStrip2";
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
			this.xfersTab.Controls.Add(this.xfersToolStrip);
			this.xfersTab.Location = new System.Drawing.Point(4, 24);
			this.xfersTab.Name = "xfersTab";
			this.xfersTab.Size = new System.Drawing.Size(635, 401);
			this.xfersTab.TabIndex = 2;
			this.xfersTab.Text = "Xfers";
			this.xfersTab.UseVisualStyleBackColor = true;
			// 
			// xfersList
			// 
			this.xfersList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									hostHdr,
									fileHdr,
									percHdr,
									progressHdr,
									dirctionHdr,
									statusHdr,
									speedHdr});
			this.xfersList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xfersList.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.xfersList.FullRowSelect = true;
			this.xfersList.GridLines = true;
			this.xfersList.Location = new System.Drawing.Point(0, 25);
			this.xfersList.Name = "xfersList";
			this.xfersList.Size = new System.Drawing.Size(635, 376);
			this.xfersList.TabIndex = 1;
			this.xfersList.UseCompatibleStateImageBehavior = false;
			this.xfersList.View = System.Windows.Forms.View.Details;
			// 
			// xfersToolStrip
			// 
			this.xfersToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.xfersToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.clearCompletedXfersButton});
			this.xfersToolStrip.Location = new System.Drawing.Point(0, 0);
			this.xfersToolStrip.Name = "xfersToolStrip";
			this.xfersToolStrip.Size = new System.Drawing.Size(635, 25);
			this.xfersToolStrip.TabIndex = 0;
			this.xfersToolStrip.Text = "toolStrip3";
			// 
			// clearCompletedXfersButton
			// 
			this.clearCompletedXfersButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.clearCompletedXfersButton.Image = ((System.Drawing.Image)(resources.GetObject("clearCompletedXfersButton.Image")));
			this.clearCompletedXfersButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.clearCompletedXfersButton.Name = "clearCompletedXfersButton";
			this.clearCompletedXfersButton.Size = new System.Drawing.Size(93, 22);
			this.clearCompletedXfersButton.Text = "Clear Completed";
			this.clearCompletedXfersButton.Click += new System.EventHandler(this.ClearCompletedXfersButtonClick);
			// 
			// settingsTab
			// 
			this.settingsTab.Controls.Add(this.browseDefaultDirButton);
			this.settingsTab.Controls.Add(this.defaultDownloadDirectoryBox);
			this.settingsTab.Controls.Add(this.label1);
			this.settingsTab.Location = new System.Drawing.Point(4, 24);
			this.settingsTab.Name = "settingsTab";
			this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
			this.settingsTab.Size = new System.Drawing.Size(635, 401);
			this.settingsTab.TabIndex = 3;
			this.settingsTab.Text = "Settings";
			this.settingsTab.UseVisualStyleBackColor = true;
			// 
			// browseDefaultDirButton
			// 
			this.browseDefaultDirButton.Location = new System.Drawing.Point(346, 28);
			this.browseDefaultDirButton.Name = "browseDefaultDirButton";
			this.browseDefaultDirButton.Size = new System.Drawing.Size(75, 23);
			this.browseDefaultDirButton.TabIndex = 2;
			this.browseDefaultDirButton.Text = "Browse";
			this.browseDefaultDirButton.UseVisualStyleBackColor = true;
			this.browseDefaultDirButton.Click += new System.EventHandler(this.BrowseDefaultDirButtonClick);
			// 
			// defaultDownloadDirectoryBox
			// 
			this.defaultDownloadDirectoryBox.Enabled = false;
			this.defaultDownloadDirectoryBox.Location = new System.Drawing.Point(9, 28);
			this.defaultDownloadDirectoryBox.Name = "defaultDownloadDirectoryBox";
			this.defaultDownloadDirectoryBox.Size = new System.Drawing.Size(331, 23);
			this.defaultDownloadDirectoryBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(412, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Default Download Directory";
			// 
			// updateTimer
			// 
			this.updateTimer.Enabled = true;
			this.updateTimer.Interval = 250;
			this.updateTimer.Tick += new System.EventHandler(this.UpdateTimerTick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(643, 451);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusStrip1);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.MinimumSize = new System.Drawing.Size(450, 450);
			this.Name = "MainForm";
			this.Text = "Lanline";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.hostsPage.ResumeLayout(false);
			this.hostsPage.PerformLayout();
			this.hostContextMenu.ResumeLayout(false);
			this.netToolStrip.ResumeLayout(false);
			this.netToolStrip.PerformLayout();
			this.sharesTab.ResumeLayout(false);
			this.sharesTab.PerformLayout();
			this.sharesToolStrip.ResumeLayout(false);
			this.sharesToolStrip.PerformLayout();
			this.xfersTab.ResumeLayout(false);
			this.xfersTab.PerformLayout();
			this.xfersToolStrip.ResumeLayout(false);
			this.xfersToolStrip.PerformLayout();
			this.settingsTab.ResumeLayout(false);
			this.settingsTab.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox defaultDownloadDirectoryBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button browseDefaultDirButton;
		private System.Windows.Forms.TabPage settingsTab;
		private System.Windows.Forms.Timer updateTimer;
		private System.Windows.Forms.ToolStripMenuItem forgetHostToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem browseFilesToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip hostContextMenu;
		private System.Windows.Forms.ToolStripButton addHostBtn;
		private System.Windows.Forms.ToolStripButton reverifyHostsBtn;
		private System.Windows.Forms.ToolStrip netToolStrip;
		private System.Windows.Forms.ToolStripButton clearCompletedXfersButton;
		private System.Windows.Forms.ToolStrip xfersToolStrip;
		private System.Windows.Forms.ToolStrip sharesToolStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.ToolStripButton refreshSharesBtn;
		private System.Windows.Forms.ListView xfersList;
		private System.Windows.Forms.TabPage xfersTab;
		private System.Windows.Forms.ListView sharesList;
		private System.Windows.Forms.TabPage sharesTab;
		private System.Windows.Forms.TabPage hostsPage;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ToolStripButton discoverBtn;
		private System.Windows.Forms.ListView hostsList;
		private System.Windows.Forms.StatusStrip statusStrip1;
	}
}
