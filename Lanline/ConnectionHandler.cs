/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 16:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lanline
{
	/// <summary>
	/// Description of ConnectionHandler.
	/// </summary>
	public class ConnectionHandler: BackgroundWorker
	{
		TcpClient client;
		NetworkStream stream;
		
		public TcpClient Client {
			get { return client; }
		}
		
		public NetworkStream Stream {
			get { return stream; }
		}
		
		
		public ConnectionHandler(TcpClient client)
		{
			this.client = client;
			this.stream = client.GetStream();
			this.DoWork += new DoWorkEventHandler(ConnectionHandler_DoWork);
		}

		void ConnectionHandler_DoWork(object sender, DoWorkEventArgs e)
		{
			StreamReader sr = new StreamReader(stream, Encoding.ASCII);
			string httpVerbLine = sr.ReadLine();
			string[] parts = httpVerbLine.Split(new char[]{' '}, 3);
			string verb = parts[0].ToLower();
			if(verb != "get") {
				stream.WriteUTF8("HTTP/1.0 400 Error\r\nContent-type:text/plain\r\n\r\nNot supported");
				return;
			}
			string path = Uri.UnescapeDataString(parts[1]);// new Uri("http://localhost" + parts[1], true).AbsolutePath;
			//string path = Uri.UnescapeDataString(parts[1]);
			// Pretend we're interested in HTTP headers
			Logging.Log("Path: " + path);
			while(true) {
				string line = sr.ReadLine();
				Logging.Log("  Header: {0}", line.Trim());
				if(line.Trim() == "") break;
			}
			
			try {
				InnerHandleHTTPRequest(path);
			} catch(Exception exc) {
				Logging.Log("Exception while handling HTTP request: {0}", exc.ToString());
			}
			client.Close();
		}
		
		void InnerHandleHTTPRequest(string path) {
			

			if(path == "/filelist") {
				string[] fileList = ShareManager.Instance.GetFullVFileList();
				byte[] buf = Encoding.UTF8.GetBytes(String.Join("\r\n", fileList));
				
				stream.WriteHTTPResponseHeader(200, "text/plain", buf.Length);
        		client.WriteStreamAsHTTPContent(new MemoryStream(buf));

			} else if(path == "/info") {
				string name = "(unnamed)";
				try {
					name = System.Environment.MachineName;
				} catch(Exception) {
					
				}
				string version = Application.ProductVersion;
				string resp = String.Format("version:{0}\nfiles:{1}\nname:{2}", version, ShareManager.Instance.TotalFiles, name);
				stream.WriteSimpleHTTPResponse(200, "text/plain", resp);
				
				string remoteIp = (client.Client.RemoteEndPoint as IPEndPoint).Address.ToString();
				if(remoteIp != "127.0.0.1" && !NetworkManager.Instance.HostIPIsKnown(remoteIp)) {
					Host h = NetworkManager.Instance.AddHost(remoteIp, NetworkManager.LANLINE_PORT, false);
					h.Name = "<downloader>";
				}
				
			} else if(path.StartsWith("/f/")) {
				ShareFileInfo sfi = ShareManager.Instance.ResolveVPath(path.Substring(3));
				if(sfi == null) {
					stream.WriteSimpleHTTPResponse(404, "text/plain", "Not found!");
				} else {
					OutgoingTransfer transfer = new OutgoingTransfer(this, sfi);
					XferManager.Instance.Track(transfer);
					byte[] buffer = new byte[0xfa00];
					float prog = 0;
					int writtenTotal = 0;
					int throttleCounter = 0;
					int bytesPerMsec = 1048576;
					FileInfo fi = new FileInfo(sfi.absoluteFsPath);
					stream.WriteHTTPResponseHeader(200, "application/octet-stream", fi.Length);
					Socket sock = client.Client;
					sock.Send(new byte[]{13, 10});
					using(FileStream sourceStream = new FileStream(sfi.absoluteFsPath, FileMode.Open)) {
						while(true) {
							int bufSize = sourceStream.Read(buffer, 0, buffer.Length);
	        				if (bufSize == 0) break;
	        				writtenTotal += sock.Send(buffer, bufSize, SocketFlags.None);
	        				transfer.SetProgress(writtenTotal / (float) fi.Length * 100);
	        				throttleCounter += bufSize;
	        				while(throttleCounter >= bytesPerMsec) {
	        					Thread.Sleep(1);
	        					throttleCounter -= bytesPerMsec;
	        				}
						}
					}
					transfer.SetIsComplete();
					/*
					stream.WriteHTTPResponseHeader(200, "application/octet-stream", sfi.size);
					using(FileStream fs = new FileStream(sfi.absoluteFsPath, FileMode.Open)) {
						client.WriteStreamAsHTTPContent(
					}*/
				}
			}
			else {
				stream.WriteSimpleHTTPResponse(200, "text/plain", "Requested: " + path);
			}
		}
	}
}
