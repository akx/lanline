/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 23:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Lanline
{
	public enum StatusFlag {
		NETWORK_CHANGED = 1,
		XFERS_CHANGED = 2
	};
	
	
	public class StatusManager
	{
		private static StatusManager _instance;

		public static StatusManager Instance {
			get { return (_instance = _instance ?? new StatusManager());
			}
		}
		
		Dictionary<StatusFlag, bool> flags;
		
		StatusManager()
		{
			flags = new Dictionary<StatusFlag, bool>();
		}
		
		public void RaiseFlag(StatusFlag flag) {
			flags[flag] = true;
		}
		
		public bool GetAndLowerFlag(StatusFlag flag) {
			if(flags.ContainsKey(flag)) {
				bool val = flags[flag];
				flags[flag] = false;
				return val;
			}
			return false;
		}
	}
}
