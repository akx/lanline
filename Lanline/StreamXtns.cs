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
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lanline
{
	public static class StreamXtns
	{
		public static void WriteUTF8(this Stream stream, string str) {
			byte[] buf = Encoding.UTF8.GetBytes(str);
			stream.Write(buf, 0, buf.Length);
			/*int pieceSize = 2048;
			for(int i = 0; i < buf.Length; i += pieceSize) {
				Logging.Log("Writing @ offset {0}", i);
				stream.Write(buf, i, Math.Min(buf.Length, i + pieceSize));
			}*/
		}
		
		public static void WriteHTTPResponseHeader(this Stream stream, int code, string contentType, int contentLength) {
			stream.WriteUTF8(string.Format("HTTP/1.0 {0} {1}\r\n", code, HttpStatusCode.GetName(typeof(HttpStatusCode), code) ?? "UNKNOWN"));
			stream.WriteUTF8("Content-Type: " + contentType + "\r\n");
			if(contentLength > 0) stream.WriteUTF8("Content-Length: " + contentLength.ToString(CultureInfo.InvariantCulture) + "\r\n");
		}
		
		public static void WriteStreamAsHTTPContent(this TcpClient client, Stream sourceStream) {
			SocketError success = SocketError.Success;
        	byte[] buffer = new byte[0xfa00];
			client.Client.Send(new byte[]{13, 10});
			while (true)
   			{
    			Logging.Log("Reading @ offset {0}", sourceStream.Position);
        		int size = sourceStream.Read(buffer, 0, buffer.Length);
        		if (size == 0) break;
        		Logging.Log("Size read: {0}", size);
        		int wrote = client.Client.Send(buffer, size, SocketFlags.None);
        		Logging.Log("Size written: {0}", wrote);
        		Thread.Sleep(10);
    		}
			
		}
		
		public static void WriteSimpleHTTPResponse(this Stream stream, int code, string contentType, string content) {
			byte[] buf = Encoding.UTF8.GetBytes(content);
			stream.WriteHTTPResponseHeader(code, contentType, buf.Length);
			stream.Write(new byte[]{13, 10}, 0, 2);
			stream.Write(buf, 0, buf.Length);
		}
	}
}
