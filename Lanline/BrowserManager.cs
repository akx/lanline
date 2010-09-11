/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 20:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Lanline
{
	/// <summary>
	/// Description of BrowserManager.
	/// </summary>
	public class BrowserManager
	{
		private static BrowserManager _instance;

		public static BrowserManager Instance {
			get { return (_instance = _instance ?? new BrowserManager());
			}
		}
		
		private Dictionary<Host, BrowserWindow> windows;
		
		public BrowserManager()
		{
			windows = new Dictionary<Host, BrowserWindow>();
		}
		
		public BrowserWindow Open(Host h) {
			BrowserWindow w;
			if(windows.ContainsKey(h)) {
				w = windows[h];
			} else {
				w = new BrowserWindow(h);
				windows[h] = w;
				w.Closed += delegate(object sender, EventArgs e) { 
					BrowserWindow bw = (sender as BrowserWindow);
					windows.Remove(bw.Host);
				};
			}
			w.Show();
			w.BringToFront();
			return w;
		}
	}
}
