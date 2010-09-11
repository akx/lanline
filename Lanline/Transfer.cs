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
		BUSY = 0,
		COMPLETED = 1,
		CANCELED = 2,
		ERROR = 3
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
			get { return (status == TransferStatus.COMPLETED); }
		}
		
		
		public Transfer()
		{
			startedOn = DateTime.Now;
			progress = 0;
			status = TransferStatus.BUSY;
			hostName = file1 = file2 = "???";
			remoteHost = null;
		}
		
		public void SetProgress(float progress) {
			if(progress < 0) progress = 0;
			if(progress > 100) progress = 100;
			this.progress = progress;
		}
		
		public void SetIsComplete() {
			if(status == TransferStatus.BUSY) {
				SetProgress(100);
				status = TransferStatus.COMPLETED;
				completedOn = DateTime.Now;
			}
		}
		
		public void Cancel() {
			
		}
	}
}
