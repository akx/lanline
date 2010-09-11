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
	
	public class Transfer
	{
		protected TransferDirection direction;
		protected string file1;
		protected string file2;
		protected string host;
		protected int progress;
		protected bool hasCompleted;
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
		
		public string Host {
			get { return host; }
		}
		
		public int Progress {
			get { return progress; }
		}
		
		public bool HasCompleted {
			get { return hasCompleted; }
		}
		
		
		public Transfer()
		{
			startedOn = DateTime.Now;
			progress = 0;
			hasCompleted = false;
			host = file1 = file2 = "???";
			
		}
		
		public void SetProgress(int progress) {
			if(progress < 0) progress = 0;
			if(progress > 100) progress = 100;
			this.progress = progress;
		}
		
		public void SetIsComplete() {
			if(!hasCompleted) {
				SetProgress(100);
				hasCompleted = true;
				completedOn = DateTime.Now;
			}
		}
	}
}
