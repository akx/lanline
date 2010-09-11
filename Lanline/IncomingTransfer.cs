/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 22:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Lanline
{
	/// <summary>
	/// Description of IncomingTransfer.
	/// </summary>
	public class IncomingTransfer: Transfer
	{
		BackgroundWorker worker;
		string tempFileName;
		
		public IncomingTransfer(Host h, string vpath, string localPath)
		{
			direction = TransferDirection.INCOMING;
			this.remoteHost = h;
			this.hostName = h.Ip;
			this.file1 = vpath;
			this.file2 = localPath;
			tempFileName = file2 + ".ll-" + (DateTime.Now.Ticks % 9999).ToString();
			string dirName = Path.GetDirectoryName(localPath);
			if(!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);
		}
		
		public void Start() {
			lock(this) {
				if(status == TransferStatus.Idle) {
					
					status = TransferStatus.Busy;
					if(worker == null) {
						worker = new BackgroundWorker();
						worker.WorkerReportsProgress = true;
						worker.WorkerSupportsCancellation = true;
						worker.DoWork += new DoWorkEventHandler(worker_DoWork);
						worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e) { 
							worker = null;
						};
						worker.RunWorkerAsync();
					}
				}
			}
			//worker_DoWork(null, null);
		}

		void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			Uri u = remoteHost.GetURI("/f/" + file1);
			WebRequest req = WebRequest.Create(u);
			req.Timeout = 3500;
			WebResponse resp = req.GetResponse();
			
			long total = resp.ContentLength;
			long received = 0;
			Stream inStream = resp.GetResponseStream();
			byte[] buffer = new byte[262144];
			using(FileStream outStream = new FileStream(tempFileName, FileMode.CreateNew)) {
				while(true) {
					if(worker.CancellationPending) {
						break;
					}
					int read = inStream.Read(buffer, 0, buffer.Length);
					if(read == 0) break;
					received += read;
					//System.Diagnostics.Debug.Print("Received {0} bytes", received);
					outStream.Write(buffer, 0, read);
					SetProgress((received / (float)total) * 100);
				}
			}
			if(worker.CancellationPending) {
				if(File.Exists(tempFileName)) {
					Logging.Log("Deleting temporary file {0} when canceling transfer", tempFileName);
					File.Delete(tempFileName);
				}
				status = TransferStatus.Canceled;
			} else {
				File.Move(tempFileName, file2);
				SetIsComplete();
			}
		}
		
		
		public override void Cancel()
		{
			if(worker != null) worker.CancelAsync();
		}
		
		
	}
}
