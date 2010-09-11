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
using System.IO;

namespace Lanline
{
	public delegate void MessageLoggedDelegate(string message);
	
	public class Logging
	{
		public static event MessageLoggedDelegate OnMessageLogged;
		private static object loggingLock = new object();
		
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
		
		public static void LogExceptionToFile(Exception exc, string title) {
			lock(loggingLock) {
				using(FileStream fs = new FileStream("lanline-exception.txt", FileMode.Append)) {
					fs.WriteUTF8("== " + title + " ==\r\n");
					fs.WriteUTF8(exc.ToString());	
					fs.WriteUTF8("\r\n\r\n");
				}
			}
		}
	}
}
