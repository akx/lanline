/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 18:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Lanline
{
	public enum TransferDirection {
		INCOMING = 0,
		OUTGOING = 1
	};
	
	public enum TransferStatus {
		Idle = 0,
		Busy,
		Completed,
		Canceled,
		Error
	};
	
	public class Transfer
	{
		protected TransferDirection direction;
		protected string file1;
		protected string file2;
		protected string hostName;
		protected Host remoteHost;
		protected float progress;
		protected TransferStatus status;
		protected DateTime startedOn;
		protected DateTime completedOn;
		
		public TransferStatus Status {
			get { return status; }
		}
		
		
		public TransferDirection Direction {
			get { return direction; }
		}
		
		public string File1 {
			get { return file1; }
		}
		
		public string File2 {
			get { return file2; }
		}
		
		public string HostName {
			get { return hostName; }
		}
		
		public Host RemoteHost {
			get { return remoteHost; }
		}
		
		
		public float Progress {
			get { return progress; }
		}
		
		public bool HasCompleted {
			get { return (status == TransferStatus.Completed); }
		}
		
		
		public Transfer()
		{
			startedOn = DateTime.Now;
			progress = 0;
			status = TransferStatus.Busy;
			hostName = file1 = file2 = "???";
			remoteHost = null;
		}
		
		public void SetProgress(float progress) {
			if(progress < 0) progress = 0;
			if(progress > 100) progress = 100;
			if(Math.Abs(progress - this.progress) > 3) StatusManager.Instance.RaiseFlag(StatusFlag.XFERS_CHANGED);
			this.progress = progress;
		}
		
		public void SetIsComplete() {
			if(status == TransferStatus.Busy) {
				SetProgress(100);
				status = TransferStatus.Completed;
				completedOn = DateTime.Now;
				StatusManager.Instance.RaiseFlag(StatusFlag.XFERS_CHANGED);
			}
		}
		
		public virtual void Cancel() {
			StatusManager.Instance.RaiseFlag(StatusFlag.XFERS_CHANGED);
		}
	}
}
