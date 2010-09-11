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
		In = 0,
		Out = 1
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
		protected long bytesDone;
		protected long bytesTotal;
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
			status = TransferStatus.Idle;
			hostName = file1 = file2 = "???";
			remoteHost = null;
		}
		
		public void SetProgress(float progress) {
			if(progress < 0) progress = 0;
			if(progress > 100) progress = 100;
			StatusManager.Instance.RaiseFlag(StatusFlag.XfersChanged);
			this.progress = progress;
		}
		
		public void SetProgressAndBytes(long bytesDone, long bytesTotal) {
			this.bytesDone = bytesDone;
			this.bytesTotal = bytesTotal;
			
			SetProgress(((bytesDone / 1024) / (float)(bytesTotal / 1024)) * 100);
		}
		
		public TimeSpan GetDuration() {
			return ((status == TransferStatus.Completed ? completedOn : DateTime.Now) - (startedOn));
		}
		
		public float GetAverageSpeed() {
			return (bytesTotal > 0 && bytesDone > 0) ? (float)(bytesDone / GetDuration().TotalSeconds / 1024.0f) : 0;
		}
		
		public void SetIsComplete() {
			if(status == TransferStatus.Busy) {
				SetProgress(100);
				if(bytesTotal > 0) bytesDone = bytesTotal;
				status = TransferStatus.Completed;
				completedOn = DateTime.Now;
				StatusManager.Instance.RaiseFlag(StatusFlag.XfersChanged);
			}
		}
		
		public virtual void Cancel() {
			StatusManager.Instance.RaiseFlag(StatusFlag.XfersChanged);
		}
	
		public string GetProgString() {
			if(status == TransferStatus.Completed) return "Comp";
			if(status == TransferStatus.Idle) return "";
			return Math.Round(Progress, 2).ToString() + "%";
		}
		
		public virtual void CancelWithError() {
			status = TransferStatus.Error;
			StatusManager.Instance.RaiseFlag(StatusFlag.XfersChanged);
		}
	}
}
