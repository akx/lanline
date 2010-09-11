/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 15:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Lanline
{
	/// <summary>
	/// Description of Host.
	/// </summary>
	public class Host
	{
		string ip;
		uint port;
		
		
		public Host(string ip, uint port)
		{
			this.ip = ip;
			this.port = port;
		}
		
	}
}
