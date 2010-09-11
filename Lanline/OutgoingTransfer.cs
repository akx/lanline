/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 18:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Lanline
{
	/// <summary>
	/// Description of OutgoingTransfer.
	/// </summary>
	public class OutgoingTransfer: Transfer
	{
		private ConnectionHandler ch;
		public OutgoingTransfer(ConnectionHandler ch, ShareFileInfo sfi): base()
		{
			direction = TransferDirection.OUTGOING;
			this.ch = ch;
			this.host = ch.Client.Client.RemoteEndPoint.ToString();
			this.file1 = sfi.relativeVPath;
			this.file2 = "(remote)";
		}
	}
}
