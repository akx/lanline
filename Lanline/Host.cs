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
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace Lanline
{
	/// <summary>
	/// Description of Host.
	/// </summary>
	public class Host
	{
		string ip;
		uint port = 0;
		bool verified = false;
		string clientIdentification;
		string clientVersion = "(unknown)";
		string name = "(unknown)";
		int nFiles = -1;
		string[] fileList;
		bool hasFileList;
		
		public string[] FileList {
			get { return fileList; }
		}
		
		public bool HasFileList {
			get { return hasFileList; }
		}
		
		
		public string Ip {
			get { return ip; }
		}
		
		public uint Port {
			get { return port; }
		}
		
		public string ClientVersion {
			get { return clientVersion; }
		}
		
		public string Name {
			get { return name; }
		}
		
		public int NFiles {
			get { return nFiles; }
		}
		
		public bool Verified {
			get { return verified; }
		}
		
		
		public Host(string ip, uint port)
		{
			this.ip = ip;
			this.port = port;
			Unverify();
		}
		
		void Unverify() {
			verified = false;
			clientIdentification = "";
			clientVersion = "?";
			nFiles = -1;
			name = "?";
			hasFileList = false;
			fileList = null;
		}
		
		public Uri GetURI(string path) {
			if(!path.StartsWith("/")) path = "/" + path;
			return new Uri("http://"+ip+":"+port.ToString(CultureInfo.InvariantCulture)+path);
		}
		
		public bool Verify() {
			Unverify();
			WebRequest req = WebRequest.Create(GetURI("info"));
			req.Timeout = 3500;
			WebResponse resp;
			try {
				resp = req.GetResponse();
			} catch(Exception) {
				return false;
			}
			TextReader tr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
			clientIdentification = tr.ReadToEnd();
			ParseClientIdentification();
			return verified;
		}
		
		void ParseClientIdentification() {
			foreach(string lline in clientIdentification.Split('\n')) {
				string line = lline.Trim();
				int colonIndex = line.IndexOf(':');
				if(colonIndex > -1) {
					string key = line.Substring(0, colonIndex).ToLower();
					string value = line.Substring(colonIndex + 1);
					switch(key) {
						case "version":
							clientVersion = value;
							verified = true;
							break;
						case "name":
							name = value;
							break;
						case "files":
							nFiles = Convert.ToInt32(value);
							break;
					}
				}
			}
		}
		
		public int RequestFileList() {
			hasFileList = false;
			WebRequest req = WebRequest.Create(GetURI("filelist"));
			req.Timeout = 3500;
			WebResponse resp = req.GetResponse();
			TextReader tr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
			fileList = null;
			List<string> tempFileList = new List<string>();
			while(true) {
				string line = tr.ReadLine();
				if(line == null) break;
				tempFileList.Add(line);
			}
			fileList = tempFileList.ToArray();
			hasFileList = true;
			return (hasFileList ? fileList.Length : -1);
		}
		
		public void InvalidateFileList() {
			fileList = null;
			hasFileList = false;
		}
		
	}
}
