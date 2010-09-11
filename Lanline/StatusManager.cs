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
		NetworkChanged,
		XfersChanged,
		SharesChanged
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
			//Logging.Debug("Raised flag {0}.", flag.ToString());
		}
		
		public bool GetAndLowerFlag(StatusFlag flag) {
			if(flags.ContainsKey(flag) && flags[flag]) {
				//Logging.Debug("Lowered flag {0}.", flag.ToString());
				flags[flag] = false;
				return true;
			}
			return false;
		}
	}
}
