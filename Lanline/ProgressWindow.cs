/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 16:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Lanline
{
	/// <summary>
	/// Description of ProgressWindow.
	/// </summary>
	public partial class ProgressWindow : Form
	{
		BackgroundWorker worker;
		public ProgressWindow(string title, BackgroundWorker worker)
		{
			InitializeComponent();
			this.worker = worker;
			Text = label1.Text = title;
			progressBar1.Minimum = 0;
			progressBar1.Maximum = 100;
			CancelBtn.Enabled = worker.WorkerSupportsCancellation;
			progressBar1.Style = (worker.WorkerReportsProgress ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee);
			worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
			worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
		}

		void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Close();
		}

		void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if(e.UserState != null) {
				label1.Text = e.UserState.ToString();
			}
			progressBar1.Value = e.ProgressPercentage;
		}
		
		void CancelBtnClick(object sender, EventArgs e)
		{
			worker.CancelAsync();
		}
	}
}
