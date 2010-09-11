/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 16:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace Lanline
{
	public static class StreamXtns
	{
		public static void WriteUTF8(this Stream stream, string str) {
			byte[] buf = Encoding.UTF8.GetBytes(str);
			stream.Write(buf, 0, buf.Length);
		}
		
		public static void WriteHTTPResponseHeader(this Stream stream, int code, string contentType, int contentLength) {
			stream.WriteUTF8(string.Format("HTTP/1.0 {0} {1}\r\n", code, HttpStatusCode.GetName(typeof(HttpStatusCode), code) ?? "UNKNOWN"));
			stream.WriteUTF8("Content-Type: " + contentType + "\r\n");
			stream.WriteUTF8("Content-Length: " + contentLength.ToString(CultureInfo.InvariantCulture) + "\r\n");
		}
		
		public static void WriteSimpleHTTPResponse(this Stream stream, int code, string contentType, string content) {
			byte[] buf = Encoding.UTF8.GetBytes(content);
			stream.WriteHTTPResponseHeader(code, contentType, buf.Length);
			stream.Write(new byte[]{13, 10}, 0, 2);
			stream.Write(buf, 0, buf.Length);
		}
	}
}
