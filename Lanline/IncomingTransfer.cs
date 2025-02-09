﻿/*
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
		static Random rng = new Random();
		BackgroundWorker worker;
		string tempFileName;
		
		
		public IncomingTransfer(Host h, string vpath, string localPath)
		{
			direction = TransferDirection.In;
			this.remoteHost = h;
			this.hostName = h.Ip;
			this.file1 = vpath;
			this.file2 = localPath;
			tempFileName = file2 + ".ll-" + (rng.Next(0, 9999)).ToString();
			
		}
		
		public void Start() {
			lock(this) {
				if(status == TransferStatus.Idle) {
					
					status = TransferStatus.Busy;
					if(worker == null) {
						startedOn = DateTime.Now;
						worker = new BackgroundWorker();
						worker.WorkerReportsProgress = true;
						worker.WorkerSupportsCancellation = true;
						worker.DoWork += new DoWorkEventHandler(worker_DoWork);
						worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e) { 
							if(e.Error != null) {
								status = TransferStatus.Error;
								Logging.LogExceptionToFile(e.Error, "Incoming Transfer " + file2);
							}
							this.worker = null;
						};
						worker.RunWorkerAsync();
					}
				}
			}
		}

		void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			try {
				worker_DoWorkInner();
			} catch(Exception exc) {
				status = TransferStatus.Error;
				Logging.LogExceptionToFile(exc, "IncomingTransfer Wrapper");
				DeleteTemporary();
			}
			StatusManager.Instance.RaiseFlag(StatusFlag.XfersChanged);
			
		}
		void worker_DoWorkInner() {
			string dirName = Path.GetDirectoryName(file2);
			if(!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);
			
			Logging.Log("Started download of {0}", this.file2);
			Uri u = remoteHost.GetURI("/f/" + file1);
			WebRequest req = WebRequest.Create(u);
			req.Timeout = 90000;
			WebResponse resp = req.GetResponse();
			
			long total = resp.ContentLength;
			long received = 0;
			Stream inStream = resp.GetResponseStream();
			byte[] buffer = new byte[1024 * 1024];
			Logging.Debug("Starting download of {0}, expecting {1} bytes.", tempFileName, total);
			using(FileStream outStream = new FileStream(tempFileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read)) {
				while(true) {
					if(worker.CancellationPending) {
						break;
					}
					int read = inStream.Read(buffer, 0, buffer.Length);
					received += read;
					if(read == 0) break;
					
					//System.Diagnostics.Debug.Print("Received {0} bytes", received);
					outStream.Write(buffer, 0, read);
					SetProgressAndBytes(received, total);
					//SetProgress((received / (float)total) * 100);
				}
				outStream.Close();
			}
			if(received < total) {
				Logging.Log("Only got {0} bytes of {1} expected, error. :(", received, total);
				DeleteTemporary();
				status = TransferStatus.Error;
				return;
			}
			Logging.Debug("Finished download of {0}.", tempFileName);
			if(worker.CancellationPending || status == TransferStatus.Canceled || status == TransferStatus.Error) {
				DeleteTemporary();
				status = TransferStatus.Canceled;
			} else {
				File.Move(tempFileName, file2);
				SetIsComplete();
			}
		}
		
		void DeleteTemporary() {
			if(!String.IsNullOrEmpty(tempFileName) && File.Exists(tempFileName)) {
				Logging.Log("Deleting temporary file {0} when canceling transfer", tempFileName);
				File.Delete(tempFileName);
			}
		}
		
		public override void Cancel()
		{
			if(worker != null && status == TransferStatus.Busy) worker.CancelAsync();
		}
		
		
	}
}
