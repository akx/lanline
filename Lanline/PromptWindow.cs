/*
 * Created by SharpDevelop.
 * User: Aarni
 * Date: 11.9.2010
 * Time: 15:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lanline
{
	/// <summary>
	/// Description of PromptWindow.
	/// </summary>
	public partial class PromptWindow : Form
	{
		public PromptWindow()
		{
			InitializeComponent();
		}
		
		public static String Prompt(string title, string label, string value) {
			PromptWindow pw = new PromptWindow();
			pw.label.Text = label;
			pw.Text = title;
			pw.textBox1.Text = value;
			if(pw.ShowDialog() == DialogResult.OK) {
				return pw.textBox1.Text;
			}
			return null;
		}
	}
}
