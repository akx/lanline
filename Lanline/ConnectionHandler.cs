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

namespace Lanline
{
	/// <summary>
	/// Description of ConnectionHandler.
	/// </summary>
	public class ConnectionHandler: BackgroundWorker
	{
		TcpClient client;
		NetworkStream stream;
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
			string path = parts[1];
			stream.WriteSimpleHTTPResponse(200, "text/plain", "Requested: " + path);
			client.Close();
		}
	}
}
