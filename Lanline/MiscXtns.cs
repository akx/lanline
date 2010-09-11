/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 18:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Lanline
{
	/// <summary>
	/// Description of MiscXtns.
	/// </summary>
	public static class MiscXtns
	{
		public static string Repeat(this string s, int times) {
			string outS = "";
			while(times -- > 0) outS += s;
			return outS;
		}
	}
}
