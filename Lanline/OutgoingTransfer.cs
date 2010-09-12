/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 18:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Lanline
{
	/// <summary>
	/// Description of OutgoingTransfer.
	/// </summary>
	public class OutgoingTransfer: Transfer
	{
		private ConnectionHandler ch;
		private ShareFileInfo sfi;
		public OutgoingTransfer(ConnectionHandler ch, ShareFileInfo sfi): base()
		{
			direction = TransferDirection.Out;
			
			this.ch = ch;
			this.sfi = sfi;
			this.hostName = ch.Client.Client.RemoteEndPoint.ToString();
			this.file1 = sfi.relativeVPath;
			this.file2 = "(remote)";
		}
		
		public void RunTransferInThisThread() {
			byte[] buffer = new byte[524288];
			//float prog = 0;
			int writtenTotal = 0;
			//int throttleCounter = 0;
			//int bytesPerMsec = 1048576;
			FileInfo fi = new FileInfo(sfi.absoluteFsPath);
			ch.Stream.WriteHTTPResponseHeader(200, "application/octet-stream", fi.Length);
			Socket sock = ch.Client.Client;
			sock.Send(new byte[]{13, 10});
			status = TransferStatus.Busy;
			using(FileStream sourceStream = new FileStream(sfi.absoluteFsPath, FileMode.Open, FileAccess.Read)) {
				while(true) {
					int bufSize = sourceStream.Read(buffer, 0, buffer.Length);
    				if (bufSize == 0) break;
    				writtenTotal += sock.Send(buffer, bufSize, SocketFlags.None);
    				SetProgressAndBytes(writtenTotal, fi.Length);
    				/*throttleCounter += bufSize;
    				while(throttleCounter >= bytesPerMsec) {
    					Thread.Sleep(1);
    					throttleCounter -= bytesPerMsec;
    				}*/
				}
			}
			SetIsComplete();
		}
	}
}
