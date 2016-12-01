using FSLib.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SystemFontReplacer.Properties;

namespace SystemFontReplacer
{
	public class Loading : PerPixelAlphaForm
	{
		private BackgroundWorker bgw = new BackgroundWorker
		{
			WorkerReportsProgress = true,
			WorkerSupportsCancellation = false
		};

		private string systemRoot;

		public Dictionary<string, string> SystemFonts
		{
			get;
			set;
		}

		public Loading()
		{
			base.StartPosition = FormStartPosition.CenterScreen;
			base.ShowInTaskbar = false;
			base.TopMost = true;
			base.FormFadeIn += delegate(object s, EventArgs e)
			{
				this.bgw.RunWorkerAsync();
			};
			base.Shown += new EventHandler(this.Loading_Shown);
			this.bgw.DoWork += new DoWorkEventHandler(this.bgw_DoWork);
			this.bgw.ProgressChanged += new ProgressChangedEventHandler(this.bgw_ProgressChanged);
			this.bgw.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs e)
			{
				base.Close();
			};
		}

		private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			Bitmap spalsh = Resources.Spalsh;
			Graphics graphics = Graphics.FromImage(spalsh);
			graphics.DrawString(string.Format("{0}", e.UserState), new Font("Vernada", 12f, GraphicsUnit.Pixel), new SolidBrush(Color.White), new PointF(5f, 167f));
			graphics.Flush();
			graphics.Dispose();
			base.FormBitmap = spalsh;
		}

		private void Loading_Shown(object sender, EventArgs e)
		{
			Bitmap spalsh = Resources.Spalsh;
			Graphics graphics = Graphics.FromImage(spalsh);
			graphics.DrawString("loading...", new Font("Vernada", 12f, GraphicsUnit.Pixel), new SolidBrush(Color.White), new PointF(5f, 167f));
			graphics.Flush();
			graphics.Dispose();
			base.FormBitmap = spalsh;
		}

		private void bgw_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.Sleep(2000);
			this.systemRoot = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System));
			this.bgw.ReportProgress(0, SR.ScanSystemFonts);
			this.ScanSystemFonts();
			this.bgw.ReportProgress(0, SR.ThanksForUsing);
			Thread.Sleep(1000);
		}

		private void ScanSystemFonts()
		{
			this.SystemFonts = new Dictionary<string, string>();
			string[] files = Directory.GetFiles(Path.Combine(this.systemRoot, "Fonts"));
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				string value = Path.GetExtension(text).ToLower();
				if (".ttf.ttc.otf.otc".IndexOf(value) != -1)
				{
					this.SystemFonts.Add(Path.GetFileName(text).ToLower(), text);
				}
			}
		}
	}
}
