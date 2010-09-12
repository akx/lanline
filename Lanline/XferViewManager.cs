/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 12.9.2010
 * Time: 10:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Lanline
{
	/// <summary>
	/// Description of XferViewManager.
	/// </summary>
	public class XferViewManager
	{
		Dictionary<Transfer, ListViewItem> listItemMap;
		Dictionary<Transfer, int> xferSortKeys;
		List<ListViewItem> listItemOrder;
		ListView attachedListView;
		
		public XferViewManager()
		{
			listItemMap = new Dictionary<Transfer, ListViewItem>();
			listItemOrder = new List<ListViewItem>();
			xferSortKeys = new Dictionary<Transfer, int>();
		}
		
		public void AttachListView(ListView lv) {
			if(lv != null) throw new Exception("Already attached");
			attachedListView = lv;
		}
		
		public void Add(Transfer xfer) {
			ListViewItem lvi = new ListViewItem(new string[]{
				xfer.HostName,
				xfer.File1,
				"-",
				"-",
				xfer.Direction.ToString(),
				"-",
				"-"
			});
			lvi.Tag = xfer;
			listItemMap[xfer] = lvi;
		}
		
		public void UpdateSingleItem(Transfer xfer) {
			int progBars = (int)Math.Ceiling((xfer.Status == TransferStatus.Completed ? 100 : xfer.Progress) / 100.0 * 20);
			ListViewItem lvi = listItemMap[xfer];
			lvi.SubItems[2].Text = xfer.GetProgString();
			lvi.SubItems[3].Text = (xfer.Status == TransferStatus.Busy ? "|".Repeat(progBars).PadRight(20, '.') : "");
			lvi.SubItems[5].Text = xfer.Status.ToString();
			lvi.SubItems[6].Text = Math.Round(xfer.GetAverageSpeed(), 1) + "K/s";
			if(xfer.Status == TransferStatus.Busy) {
				lvi.ForeColor = Color.FromArgb(255, 0, 0, (int)(xfer.Progress * 2));
				xferSortKeys[xfer] = -(int)xfer.Progress;
			}
			else if(xfer.Status == TransferStatus.Completed) {
				lvi.ForeColor = Color.Green;
				xferSortKeys[xfer] = 50000;
			}
			else {
				lvi.ForeColor = Color.Gray;
				xferSortKeys[xfer] = 5000;
			}
			if(xfer.Status == TransferStatus.Error) {
				lvi.ForeColor = Color.Red;
				xferSortKeys[xfer] = 1000;
			}
		}
		
		public void Remove(Transfer xfer) {
			ListViewItem lvi = listItemMap[xfer];
			listItemOrder.Remove(lvi);
			listItemMap.Remove(xfer);
		}
		
		public void Clear() {
			listItemMap.Clear();
			listItemOrder.Clear();
		}
		
		void GetXferItemSortKey(ListViewItem lvi) {
			
		}
		
		public void Update() {
			listItemOrder.Sort(delegate(ListViewItem a, ListViewItem b) {
			    int ska = xferSortKeys[a.Tag as Transfer];
			    int skb = xferSortKeys[b.Tag as Transfer];
	        });
			foreach(ListViewItem lvi in listItemOrder) {
				
			}
		}
	}
}
