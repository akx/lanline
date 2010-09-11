/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 18:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Lanline
{
	/// <summary>
	/// Description of XferManager.
	/// </summary>
	public class XferManager
	{
		private static XferManager _instance;

		public static XferManager Instance {
			get { if(_instance == null) {
					_instance = new XferManager();
				}
				return _instance;
			}
		}
		
		List<Transfer> transfers;
		
		public XferManager()
		{
			transfers = new List<Transfer>();
		}
		
		public void Track(Transfer tr) {
			lock(transfers) {
				if(!transfers.Contains(tr)) {
					transfers.Add(tr);
					StatusManager.Instance.RaiseFlag(StatusFlag.XfersChanged);
				}
			}
		}
		
		public void ClearCompleted() {
			lock(transfers) {
				for(int i = 0; i < transfers.Count; i++) {
					if(transfers[i].Status == TransferStatus.Completed) {
						transfers.RemoveAt(i);
						i--;
					}
				}
			}
			StatusManager.Instance.RaiseFlag(StatusFlag.XfersChanged);
		}
		
		public IEnumerable<Transfer> EnumerateTransfers() {
			for(int i = 0; i < transfers.Count; i++) yield return transfers[i];
		}
		
		public int GetRunningDownloadCount() {
			int count = 0;
			lock(transfers) {
				foreach(Transfer xfer in transfers) {
					if(xfer.Direction == TransferDirection.In && xfer.Status == TransferStatus.Busy) count ++;
				}
			}
			return count;
		}
		
		public void GetRunningCounts(out int dlCount, out int ulCount) {
			dlCount = 0;
			ulCount = 0;
			lock(transfers) {
				foreach(Transfer xfer in transfers) {
					if(xfer.Direction == TransferDirection.In && xfer.Status == TransferStatus.Busy) dlCount ++;
					if(xfer.Direction == TransferDirection.Out && xfer.Status == TransferStatus.Busy) ulCount ++;
				}
			}
		}
		
		public void StartQueuedConnections() {
			int quota = 5 - GetRunningDownloadCount();
			
			Logging.Debug("Available download slots: {0}", quota);
			if(quota <= 0) return;
			foreach(Transfer xfer in transfers) {
				if(xfer.Direction == TransferDirection.In && xfer.Status == TransferStatus.Idle) {
					//Logging.Debug("XferManager starting idle download {0}", xfer);
					(xfer as IncomingTransfer).Start();
					StatusManager.Instance.RaiseFlag(StatusFlag.XfersChanged);
					quota --;
					if(quota <= 0) break;
				}
			}
		}
	}
}
