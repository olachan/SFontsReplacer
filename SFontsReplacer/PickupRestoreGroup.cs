using FSLib;
using FSLib.Windows.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SystemFontReplacer.Properties;

namespace SystemFontReplacer
{
	public class PickupRestoreGroup : FunctionalForm
	{
		private IContainer components;

		private Label label1;

		private ListBox lstBackupList;

		private Label label2;

		private TextBox txtContents;

		private Button btnDelete;

		private Button btnCancel;

		private Button btnOK;

		public string[] Files
		{
			get;
			private set;
		}

		public PickupRestoreGroup()
		{
			this.InitializeComponent();
			base.Load += new EventHandler(this.PickupRestoreGroup_Load);
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			this.lstBackupList.SelectedIndexChanged += delegate(object s, EventArgs e)
			{
				this.EnsureEnableAndContents();
			};
			this.lstBackupList.DrawItem += new DrawItemEventHandler(this.lstBackupList_DrawItem);
			this.lstBackupList.MeasureItem += new MeasureItemEventHandler(this.lstBackupList_MeasureItem);
			this.lstBackupList.DrawMode = DrawMode.OwnerDrawVariable;
			this.btnDelete.Enabled = false;
			this.btnOK.Enabled = false;
		}

		private void lstBackupList_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = 20;
		}

		private void lstBackupList_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index == -1)
			{
				return;
			}
			e.DrawBackground();
			Graphics graphics = e.Graphics;
			graphics.DrawImage(Resources.Saved, 3 + e.Bounds.Left, 2 + e.Bounds.Top, 16, 16);
			SolidBrush brush = new SolidBrush(e.ForeColor);
			graphics.DrawString(this.lstBackupList.Items[e.Index].ToString(), e.Font, brush, new PointF((float)(22 + e.Bounds.Left), (float)(4 + e.Bounds.Top)));
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.lstBackupList.SelectedIndex == -1)
			{
				return;
			}
			if (!FunctionalForm.Question(SR.SureToDeleteBackup, true))
			{
				return;
			}
			string text = Path.Combine(Application.StartupPath, "backup");
			text = Path.Combine(text, this.lstBackupList.SelectedItem.ToString());
			Directory.Delete(text, true);
			this.lstBackupList.Items.RemoveAt(this.lstBackupList.SelectedIndex);
		}

		private void PickupRestoreGroup_Load(object sender, EventArgs e)
		{
			string path = Path.Combine(Application.StartupPath, "backup");
			if (!Directory.Exists(path))
			{
				FunctionalForm.Infomation(SR.NoBackup);
				base.DialogResult = DialogResult.Cancel;
				base.Close();
				return;
			}
			string[] directories = Directory.GetDirectories(path);
			string[] array = directories;
			for (int i = 0; i < array.Length; i++)
			{
				string path2 = array[i];
				if (Directory.GetFiles(path2).Length == 0)
				{
					Directory.Delete(path2);
				}
				else
				{
					this.lstBackupList.Items.Add(Path.GetFileName(path2));
				}
			}
		}

		private void EnsureEnableAndContents()
		{
			this.btnOK.Enabled = (this.btnDelete.Enabled = (this.lstBackupList.SelectedIndex != -1));
			if (this.lstBackupList.SelectedIndex == -1)
			{
				this.txtContents.Text = string.Empty;
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			string text = Path.Combine(Application.StartupPath, "backup");
			text = Path.Combine(text, this.lstBackupList.SelectedItem.ToString());
			this.Files = Directory.GetFiles(text);
			string[] files = this.Files;
			for (int i = 0; i < files.Length; i++)
			{
				string text2 = files[i];
				FileInfo fileInfo = new FileInfo(text2);
				stringBuilder.AppendLine(Path.GetFileName(text2) + "  (" + fileInfo.Length.ToSizeDescription() + ")");
			}
			this.txtContents.Text = stringBuilder.ToString();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PickupRestoreGroup));
			this.label1 = new Label();
			this.lstBackupList = new ListBox();
			this.label2 = new Label();
			this.txtContents = new TextBox();
			this.btnDelete = new Button();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			base.SuspendLayout();
			this.label1.AccessibleDescription = null;
			this.label1.AccessibleName = null;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Font = null;
			this.label1.Name = "label1";
			this.lstBackupList.AccessibleDescription = null;
			this.lstBackupList.AccessibleName = null;
			componentResourceManager.ApplyResources(this.lstBackupList, "lstBackupList");
			this.lstBackupList.BackgroundImage = null;
			this.lstBackupList.Font = null;
			this.lstBackupList.FormattingEnabled = true;
			this.lstBackupList.Name = "lstBackupList";
			this.label2.AccessibleDescription = null;
			this.label2.AccessibleName = null;
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Font = null;
			this.label2.Name = "label2";
			this.txtContents.AccessibleDescription = null;
			this.txtContents.AccessibleName = null;
			componentResourceManager.ApplyResources(this.txtContents, "txtContents");
			this.txtContents.BackgroundImage = null;
			this.txtContents.Font = null;
			this.txtContents.Name = "txtContents";
			this.txtContents.ReadOnly = true;
			this.btnDelete.AccessibleDescription = null;
			this.btnDelete.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnDelete, "btnDelete");
			this.btnDelete.BackgroundImage = null;
			this.btnDelete.Font = null;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnCancel.AccessibleDescription = null;
			this.btnCancel.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackgroundImage = null;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.Font = null;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnOK.AccessibleDescription = null;
			this.btnOK.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackgroundImage = null;
			this.btnOK.DialogResult = DialogResult.OK;
			this.btnOK.Font = null;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOK;
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.CancelButton = this.btnCancel;
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnDelete);
			base.Controls.Add(this.txtContents);
			base.Controls.Add(this.lstBackupList);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			this.Font = null;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = null;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PickupRestoreGroup";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
