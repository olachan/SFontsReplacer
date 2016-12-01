//using FSLib.App.SimpleUpdater;
using FSLib.Windows.Forms;
using FSLib.Windows.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace SystemFontReplacer
{
	public class Config : FunctionalForm
	{
		private class CultureInfoWrapper
		{
			public CultureInfo CultureInfo
			{
				get;
				set;
			}

			public CultureInfoWrapper(CultureInfo cultureInfo)
			{
				this.CultureInfo = cultureInfo;
			}

			public override string ToString()
			{
				return this.CultureInfo.DisplayName;
			}
		}

		private IContainer components;

		private LinkLabel lnkUninstall;

		private ComboBox langList;

		private Label label1;

		private LinkLabel lnkCheckUpdate;

		private bool _supressClosing;

		private bool inRefreshing;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Config));
			this.lnkUninstall = new LinkLabel();
			this.langList = new ComboBox();
			this.label1 = new Label();
			this.lnkCheckUpdate = new LinkLabel();
			base.SuspendLayout();
			this.lnkUninstall.AccessibleDescription = null;
			this.lnkUninstall.AccessibleName = null;
			componentResourceManager.ApplyResources(this.lnkUninstall, "lnkUninstall");
			this.lnkUninstall.Font = null;
			this.lnkUninstall.Name = "lnkUninstall";
			this.lnkUninstall.TabStop = true;
			this.langList.AccessibleDescription = null;
			this.langList.AccessibleName = null;
			componentResourceManager.ApplyResources(this.langList, "langList");
			this.langList.BackgroundImage = null;
			this.langList.DropDownStyle = ComboBoxStyle.DropDownList;
			this.langList.Font = null;
			this.langList.FormattingEnabled = true;
			this.langList.Name = "langList";
			this.label1.AccessibleDescription = null;
			this.label1.AccessibleName = null;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Font = null;
			this.label1.Name = "label1";
			this.lnkCheckUpdate.AccessibleDescription = null;
			this.lnkCheckUpdate.AccessibleName = null;
			componentResourceManager.ApplyResources(this.lnkCheckUpdate, "lnkCheckUpdate");
			this.lnkCheckUpdate.Font = null;
			this.lnkCheckUpdate.Name = "lnkCheckUpdate";
			this.lnkCheckUpdate.TabStop = true;
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.Controls.Add(this.lnkCheckUpdate);
			base.Controls.Add(this.lnkUninstall);
			base.Controls.Add(this.langList);
			base.Controls.Add(this.label1);
			this.Font = null;
			base.FormBorderStyle = FormBorderStyle.None;
			base.Icon = null;
			base.Name = "Config";
			base.ShowInTaskbar = false;
			base.TopMost = true;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public Config()
		{
			this.InitializeComponent();
			this.InitLanguage();
			base.Deactivate += delegate(object s, EventArgs e)
			{
				if (!this._supressClosing)
				{
					base.Close();
				}
			};
			this.lnkUninstall.Click += delegate(object s, EventArgs e)
			{
				this.Uninstall();
			};
			this.lnkCheckUpdate.Click += delegate(object s, EventArgs e)
			{
				//Updater.CheckUpdateSimple();
				base.Close();
			};
		}

		private void Uninstall()
		{
			this._supressClosing = true;
			if (!FunctionalForm.Question(SR.ConfirmToDeleteSettings, true))
			{
				this._supressClosing = false;
				return;
			}
			Program.CFG.Uninstall();
			FunctionalForm.Infomation(SR.UninstallSuccessfully);
			Environment.Exit(0);
		}

		private void InitLanguage()
		{
			List<CultureInfo> localCultureList = LocalizationHelper.GetLocalCultureList();
			this.langList.Items.Add(SR.AutoSelect);
			foreach (CultureInfo current in localCultureList)
			{
				this.langList.Items.Add(new Config.CultureInfoWrapper(current));
				if (Thread.CurrentThread.CurrentCulture.LCID == current.LCID)
				{
					this.langList.SelectedIndex = this.langList.Items.Count - 1;
				}
			}
			if (this.langList.Items.Count > 0 && this.langList.SelectedIndex == -1)
			{
				this.langList.SelectedIndex = 0;
			}
			this.langList.SelectedIndexChanged += new EventHandler(this.langList_SelectedIndexChanged);
		}

		private void langList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.inRefreshing)
			{
				return;
			}
			this.inRefreshing = true;
			object selectedItem = this.langList.SelectedItem;
			if (selectedItem is string)
			{
				LocalizationHelper.ApplyLangResourceToOpenedForms(CultureInfo.InvariantCulture.LCID);
			}
			else
			{
				Config.CultureInfoWrapper cultureInfoWrapper = selectedItem as Config.CultureInfoWrapper;
				LocalizationHelper.ApplyLangResourceToOpenedForms(cultureInfoWrapper.CultureInfo.LCID);
			}
			Program.CFG.SetConfig("Language", Thread.CurrentThread.CurrentUICulture.LCID);
			for (int i = 0; i < this.langList.Items.Count; i++)
			{
				if (this.langList.Items[i] is string)
				{
					this.langList.Items[i] = SR.AutoSelect;
				}
				else
				{
					this.langList.Items[i] = this.langList.Items[i];
				}
			}
			this.inRefreshing = false;
		}
	}
}
