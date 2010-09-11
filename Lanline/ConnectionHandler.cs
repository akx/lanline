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
			// Pretend we're interested in HTTP headers
			while(true) {
				string line = sr.ReadLine();
				Logging.Log("  Header: {0}", line.Trim());
				if(line.Trim() == "") break;
			}
			
			try {
				InnerHandleHTTPRequest(parts[1]);
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

			} else if(path.StartsWith("/f/")) {
				ShareFileInfo sfi = ShareManager.Instance.ResolveVPath(path.Substring(3));
				if(sfi == null) {
					stream.WriteSimpleHTTPResponse(404, "text/plain", "Not found!");
				} else {
					XferManager.Instance.BeginOutgoingTransfer(this, sfi);
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
