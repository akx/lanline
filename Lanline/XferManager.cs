/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 18:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Lanline
{
	/// <summary>
	/// Description of XferManager.
	/// </summary>
	public class XferManager
	{
		private static XferManager _instance;

		public static XferManager Instance {
			get { if(_instance == null) {
					_instance = new XferManager();
				}
				return _instance;
			}
		}
		
		public XferManager()
		{
		}
		
		public void BeginOutgoingTransfer(ConnectionHandler conn, ShareFileInfo sfi) {
			
		}
	}
}
