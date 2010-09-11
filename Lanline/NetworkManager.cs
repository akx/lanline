/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 15:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Net;

namespace Lanline
{
	/// <summary>
	/// Description of NetworkManager.
	/// </summary>
	public class NetworkManager
	{
		public const int LANLINE_PORT = 1900;
		
		List<Host> knownHosts;
		
		private static NetworkManager _instance;

		public static NetworkManager Instance {
			get { return (_instance = _instance ?? new NetworkManager());
			}
		}
		
		NetworkManager()
		{
			knownHosts = new List<Host>();
		}
		
		public Host AddHost(string ip, uint port, bool verify) {
			Host h = new Host(ip, port);
			if(verify) {
				h.Verify();
				if(!h.Verified) return null;
			}
			knownHosts.Add(h);
			StatusManager.Instance.RaiseFlag(StatusFlag.NetworkChanged);
			return h;
		}
		
		public IEnumerable<Host> EnumerateKnownHosts() {
			foreach(Host h in knownHosts) yield return h;
		}
		
		public int NHosts { get { return knownHosts.Count; } }
		
		
		public void RemoveKnownHost(Host h) {
			if(knownHosts.Contains(h)) {
				knownHosts.Remove(h);
				StatusManager.Instance.RaiseFlag(StatusFlag.NetworkChanged);
			}
		}
		
		public bool HostIPIsKnown(string ip) {
			foreach(Host h in knownHosts) if(h.Ip == ip) return true;
			return false;
		}
		
		
	}
}
