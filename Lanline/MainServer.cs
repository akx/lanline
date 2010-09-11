/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 16:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lanline
{
	/// <summary>
	/// Description of MainServer.
	/// </summary>
	public class MainServer
	{
		private static MainServer _instance;

		public static MainServer Instance {
			get { if(_instance == null) {
					_instance = new MainServer();
				}
				return _instance;
			}
		}
		
		TcpListener listener;
		BackgroundWorker listenerWorker;
		
		public MainServer()
		{
			
		}
		
		public void Start() {
			if(listener != null) {
				Logging.Log("Server has already been started, will not restart.");
				return;
			}
			listener = new TcpListener(IPAddress.Any, 1900);
			listener.Start(); 
			listenerWorker = new BackgroundWorker();
			listenerWorker.DoWork += new DoWorkEventHandler(listenerWorker_DoWork);
			listenerWorker.RunWorkerAsync();
			Logging.Log("Server started.");
		}

		void listenerWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			while(true) {
				if(listener.Pending()) {
					// Fire-and-forget a connection handler thread.
					TcpClient client = listener.AcceptTcpClient();
					ConnectionHandler ch = new ConnectionHandler(client);
					ch.RunWorkerAsync();
				} else {
					Thread.Sleep(10);
				}
			}
		}
	}
}
