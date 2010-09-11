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
		
		public IncomingTransfer(Host h, string vpath, string localPath)
		{
			direction = TransferDirection.INCOMING;
			this.remoteHost = h;
			this.hostName = h.Ip;
			this.file1 = vpath;
			this.file2 = localPath;
			string dirName = Path.GetDirectoryName(localPath);
			if(!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);
		}
		
		public void Start() {
			/*
			worker = new BackgroundWorker();
			worker.WorkerReportsProgress = true;
			worker.WorkerSupportsCancellation = true;
			worker.DoWork += new DoWorkEventHandler(worker_DoWork);
			worker.RunWorkerAsync();
			*/
			worker_DoWork(null, null);
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
			using(FileStream outStream = new FileStream(file2, FileMode.CreateNew)) {
				while(true) {
					int read = inStream.Read(buffer, 0, buffer.Length);
					if(read == 0) break;
					received += read;
					System.Diagnostics.Debug.Print("Received {0} bytes", received);
					outStream.Write(buffer, 0, read);
					SetProgress((read / (float)received) * 100);
				}
			}
			SetIsComplete();
		}
		
		
		
		
	}
}
