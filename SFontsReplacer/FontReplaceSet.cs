using FSLib.Windows.Controls;
using FSLib.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SystemFontReplacer
{
	public class FontReplaceSet : FunctionalForm
	{
		private IContainer components;

		private GradientBanner gradientBanner1;

		private Label label1;

		private Label label2;

		private FileSelector fontFile;

		private ComboBox fontCombo;

		private Label label3;

		private Button btnOK;

		private Button btnCancel;

		private Label fontWarning;

		private Dictionary<string, string> list;

		private int _mode;

		internal TaskInfo Task
		{
			get;
			private set;
		}

		public int Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
				this.fontFile.Enabled = (this._mode == 0);
			}
		}

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FontReplaceSet));
			this.label1 = new Label();
			this.label2 = new Label();
			this.fontCombo = new ComboBox();
			this.label3 = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.fontWarning = new Label();
			this.fontFile = new FileSelector();
			this.gradientBanner1 = new GradientBanner();
			base.SuspendLayout();
			this.label1.AccessibleDescription = null;
			this.label1.AccessibleName = null;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Font = null;
			this.label1.Name = "label1";
			this.label2.AccessibleDescription = null;
			this.label2.AccessibleName = null;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Font = null;
			this.label2.Name = "label2";
			this.fontCombo.AccessibleDescription = null;
			this.fontCombo.AccessibleName = null;
			componentResourceManager.ApplyResources(this.fontCombo, "fontCombo");
			this.fontCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.fontCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.fontCombo.BackgroundImage = null;
			this.fontCombo.Font = null;
			this.fontCombo.FormattingEnabled = true;
			this.fontCombo.Name = "fontCombo";
			this.label3.AccessibleDescription = null;
			this.label3.AccessibleName = null;
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Font = null;
			this.label3.Name = "label3";
			this.btnOK.AccessibleDescription = null;
			this.btnOK.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackgroundImage = null;
			this.btnOK.Font = null;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnCancel.AccessibleDescription = null;
			this.btnCancel.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackgroundImage = null;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.Font = null;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.fontWarning.AccessibleDescription = null;
			this.fontWarning.AccessibleName = null;
			componentResourceManager.ApplyResources(this.fontWarning, "fontWarning");
			this.fontWarning.Font = null;
			this.fontWarning.ForeColor = Color.Red;
			this.fontWarning.Name = "fontWarning";
			this.fontFile.AccessibleDescription = null;
			this.fontFile.AccessibleName = null;
			componentResourceManager.ApplyResources(this.fontFile, "fontFile");
			this.fontFile.BackgroundImage = null;
			this.fontFile.EnableFileDrop = false;
			this.fontFile.EnableUserType = false;
			this.fontFile.EnablueMulitChoose = false;
			this.fontFile.FileMode = FileSelector.Mode.Open;
			this.fontFile.Font = null;
			this.fontFile.Name = "fontFile";
			this.gradientBanner1.AccessibleDescription = null;
			this.gradientBanner1.AccessibleName = null;
			componentResourceManager.ApplyResources(this.gradientBanner1, "gradientBanner1");
			this.gradientBanner1.BackgroundImage = null;
			this.gradientBanner1.BottomBorderColor = SystemColors.WindowFrame;
			this.gradientBanner1.BriefText = null;
			this.gradientBanner1.BriefTextColor = Color.White;
			this.gradientBanner1.DisplayPadding = 5f;
			this.gradientBanner1.EndColor = Color.White;
			this.gradientBanner1.Font = null;
			this.gradientBanner1.HeadImage = null;
			this.gradientBanner1.Name = "gradientBanner1";
			this.gradientBanner1.StartColor = Color.RoyalBlue;
			this.gradientBanner1.TextAlign = GradientBanner.TextAlignType.MiddleLeft;
			this.gradientBanner1.TextColor = Color.White;
			this.gradientBanner1.TextFont = new Font("微软雅黑", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			base.AcceptButton = this.btnOK;
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.fontWarning);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.fontCombo);
			base.Controls.Add(this.fontFile);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.gradientBanner1);
			this.Font = null;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = null;
			base.Name = "FontReplaceSet";
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FontReplaceSet()
		{
			this.InitializeComponent();
			this.fontFile.FileSelectChanged += new EventHandler(this.fontFile_FileSelectChanged);
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.fontCombo.Text = this.fontCombo.Text.ToLower();
			if (string.IsNullOrEmpty(this.fontCombo.Text) || !this.list.ContainsKey(this.fontCombo.Text))
			{
				FunctionalForm.Infomation(SR.NoFontSelected);
				return;
			}
			if (this.Mode == 0 && !File.Exists(this.fontFile.SelectedFile))
			{
				FunctionalForm.Infomation(SR.NoNewFileSelected);
				return;
			}
			if (this.Mode == 1)
			{
				this.Task = new TaskInfo(this.list[this.fontCombo.Text], this.fontCombo.Text, "", false);
			}
			else
			{
				this.Task = new TaskInfo(this.list[this.fontCombo.Text], this.fontCombo.Text, this.fontFile.SelectedFile, true);
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void fontFile_FileSelectChanged(object sender, EventArgs e)
		{
			string selectedFile = this.fontFile.SelectedFile;
			if (string.IsNullOrEmpty(selectedFile))
			{
				this.fontCombo.Text = "";
				this.fontWarning.Visible = false;
				this.fontCombo.SelectedIndex = -1;
				return;
			}
			string text = Path.GetFileName(selectedFile).ToLower();
			if (this.list.ContainsKey(text))
			{
				this.fontCombo.Text = text;
				this.fontWarning.Visible = false;
				return;
			}
			this.fontCombo.Text = "";
			this.fontWarning.Visible = true;
			this.fontCombo.SelectedIndex = -1;
		}

		public void InitializeList(Dictionary<string, string> list)
		{
			this.list = list;
			foreach (string current in list.Keys)
			{
				this.fontCombo.Items.Add(current);
			}
		}

		internal void Clear()
		{
			this.fontFile.Clear();
		}
	}
}
