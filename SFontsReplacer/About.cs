using FSLib;
using FSLib.Windows.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
using SystemFontReplacer.Properties;

namespace SystemFontReplacer
{
	internal class About : PerPixelAlphaForm
	{
		public About()
		{
			base.TopMost = true;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Click += new EventHandler(this.About_Click);
		}

		private void About_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			Bitmap spalsh = Resources.Spalsh;
			Graphics graphics = Graphics.FromImage(spalsh);
			SolidBrush brush = new SolidBrush(Color.White);
			Font font = new Font("Verdana", 12f, GraphicsUnit.Pixel);
			string versionInfo = SR.VersionInfo;
			graphics.DrawString(versionInfo.FormatWith(new object[]
			{
				Application.ProductVersion
			}), font, brush, new PointF(10f, 130f));
			graphics.Flush();
			graphics.Dispose();
			base.FormBitmap = spalsh;
		}
	}
}
