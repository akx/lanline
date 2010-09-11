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
				if(!transfers.Contains(tr)) transfers.Add(tr);
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
			StatusManager.Instance.RaiseFlag(StatusFlag.XFERS_CHANGED);
		}
		
		public IEnumerable<Transfer> EnumerateTransfers() {
			foreach(Transfer xfer in transfers) yield return xfer;
		}
		
		public int GetRunningDownloadCount() {
			int count = 0;
			foreach(Transfer xfer in transfers) {
				if(xfer.Direction == TransferDirection.INCOMING && xfer.Status == TransferStatus.Busy) count ++;
			}
			return count;
		}
		
		public void StartQueuedConnections() {
			int quota = 5 - GetRunningDownloadCount();
			foreach(Transfer xfer in transfers) {
				if(xfer.Direction == TransferDirection.INCOMING && xfer.Status == TransferStatus.Idle) {
					(xfer as IncomingTransfer).Start();
				}
			}
		}
	}
}
