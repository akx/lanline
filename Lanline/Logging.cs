/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 17:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Lanline
{
	public delegate void MessageLoggedDelegate(string message);
	
	public class Logging
	{
		public static event MessageLoggedDelegate OnMessageLogged;
		
		
		public static void Log(string fmt, params object[] args) {
			string msg = String.Format(fmt, args);
			System.Diagnostics.Debug.Print(msg);
			if(OnMessageLogged != null) {
				OnMessageLogged(msg);
			}
		}
		
		public static void Debug(string fmt, params object[] args) {
			string msg = String.Format(fmt, args);
			System.Diagnostics.Debug.Print("Debug: " + msg);
		}
	}
}
