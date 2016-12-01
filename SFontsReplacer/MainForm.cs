using FSLib.Windows;
using FSLib.Windows.Components;
using FSLib.Windows.Controls;
using FSLib.Windows.Forms;
using FSLib.Windows.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SystemFontReplacer
{
	public class MainForm : FunctionalForm, ILocalizable
	{
		private IContainer components;

		private GradientBanner gradientBanner1;

		private ListView taskList;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private Button btnAdd;

		private Button btnAddDel;

		private Button btnRun;

		private Button btnDeleteTask;

		private ToolTip toolTip1;

		private CheckBox chkKillAll;

		private FileDropHelper fileDropHelper1;

		private Button btnRestore;

		private LinkLabel lnkAbout;

		private LinkLabel lnkConfig;

		private FontReplaceSet fps = new FontReplaceSet();

		private Dictionary<string, string> fonts;

		private Action<ListViewItem, string> _updateDelegate;

		public string SourceDic
		{
			get;
			set;
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			this.taskList = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.btnAdd = new Button();
			this.btnAddDel = new Button();
			this.btnRun = new Button();
			this.gradientBanner1 = new GradientBanner();
			this.btnDeleteTask = new Button();
			this.toolTip1 = new ToolTip(this.components);
			this.chkKillAll = new CheckBox();
			this.btnRestore = new Button();
			this.lnkAbout = new LinkLabel();
			this.lnkConfig = new LinkLabel();
			this.fileDropHelper1 = new FileDropHelper();
			base.SuspendLayout();
			this.taskList.AccessibleDescription = null;
			this.taskList.AccessibleName = null;
			componentResourceManager.ApplyResources(this.taskList, "taskList");
			this.taskList.BackgroundImage = null;
			this.taskList.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3
			});
			this.taskList.Font = null;
			this.taskList.FullRowSelect = true;
			this.taskList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.taskList.Name = "taskList";
			this.toolTip1.SetToolTip(this.taskList, componentResourceManager.GetString("taskList.ToolTip"));
			this.taskList.UseCompatibleStateImageBehavior = false;
			this.taskList.View = View.Details;
			componentResourceManager.ApplyResources(this.columnHeader1, "columnHeader1");
			componentResourceManager.ApplyResources(this.columnHeader2, "columnHeader2");
			componentResourceManager.ApplyResources(this.columnHeader3, "columnHeader3");
			this.btnAdd.AccessibleDescription = null;
			this.btnAdd.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.BackgroundImage = null;
			this.btnAdd.Font = null;
			this.btnAdd.Name = "btnAdd";
			this.toolTip1.SetToolTip(this.btnAdd, componentResourceManager.GetString("btnAdd.ToolTip"));
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAddDel.AccessibleDescription = null;
			this.btnAddDel.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnAddDel, "btnAddDel");
			this.btnAddDel.BackgroundImage = null;
			this.btnAddDel.Font = null;
			this.btnAddDel.Name = "btnAddDel";
			this.toolTip1.SetToolTip(this.btnAddDel, componentResourceManager.GetString("btnAddDel.ToolTip"));
			this.btnAddDel.UseVisualStyleBackColor = true;
			this.btnRun.AccessibleDescription = null;
			this.btnRun.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnRun, "btnRun");
			this.btnRun.BackgroundImage = null;
			this.btnRun.Font = null;
			this.btnRun.Name = "btnRun";
			this.toolTip1.SetToolTip(this.btnRun, componentResourceManager.GetString("btnRun.ToolTip"));
			this.btnRun.UseVisualStyleBackColor = true;
			this.gradientBanner1.AccessibleDescription = null;
			this.gradientBanner1.AccessibleName = null;
			componentResourceManager.ApplyResources(this.gradientBanner1, "gradientBanner1");
			this.gradientBanner1.BackgroundImage = null;
			this.gradientBanner1.BottomBorderColor = Color.RoyalBlue;
			this.gradientBanner1.BriefText = null;
			this.gradientBanner1.BriefTextColor = Color.Magenta;
			this.gradientBanner1.DisplayPadding = 5f;
			this.gradientBanner1.EndColor = Color.White;
			this.gradientBanner1.Font = null;
			this.gradientBanner1.HeadImage = (Image)componentResourceManager.GetObject("gradientBanner1.HeadImage");
			this.gradientBanner1.Name = "gradientBanner1";
			this.gradientBanner1.ShowBottomBorder = true;
			this.gradientBanner1.StartColor = Color.RoyalBlue;
			this.gradientBanner1.TextAlign = GradientBanner.TextAlignType.MiddleLeft;
			this.gradientBanner1.TextColor = Color.White;
			this.gradientBanner1.TextFont = new Font("微软雅黑", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.toolTip1.SetToolTip(this.gradientBanner1, componentResourceManager.GetString("gradientBanner1.ToolTip"));
			this.btnDeleteTask.AccessibleDescription = null;
			this.btnDeleteTask.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnDeleteTask, "btnDeleteTask");
			this.btnDeleteTask.BackgroundImage = null;
			this.btnDeleteTask.Font = null;
			this.btnDeleteTask.Name = "btnDeleteTask";
			this.toolTip1.SetToolTip(this.btnDeleteTask, componentResourceManager.GetString("btnDeleteTask.ToolTip"));
			this.btnDeleteTask.UseVisualStyleBackColor = true;
			this.chkKillAll.AccessibleDescription = null;
			this.chkKillAll.AccessibleName = null;
			componentResourceManager.ApplyResources(this.chkKillAll, "chkKillAll");
			this.chkKillAll.BackgroundImage = null;
			this.chkKillAll.Checked = true;
			this.chkKillAll.CheckState = CheckState.Checked;
			this.chkKillAll.Font = null;
			this.chkKillAll.Name = "chkKillAll";
			this.toolTip1.SetToolTip(this.chkKillAll, componentResourceManager.GetString("chkKillAll.ToolTip"));
			this.chkKillAll.UseVisualStyleBackColor = true;
			this.btnRestore.AccessibleDescription = null;
			this.btnRestore.AccessibleName = null;
			componentResourceManager.ApplyResources(this.btnRestore, "btnRestore");
			this.btnRestore.BackgroundImage = null;
			this.btnRestore.Font = null;
			this.btnRestore.Name = "btnRestore";
			this.toolTip1.SetToolTip(this.btnRestore, componentResourceManager.GetString("btnRestore.ToolTip"));
			this.btnRestore.UseVisualStyleBackColor = true;
			this.lnkAbout.AccessibleDescription = null;
			this.lnkAbout.AccessibleName = null;
			componentResourceManager.ApplyResources(this.lnkAbout, "lnkAbout");
			this.lnkAbout.Font = null;
			this.lnkAbout.Name = "lnkAbout";
			this.lnkAbout.TabStop = true;
			this.toolTip1.SetToolTip(this.lnkAbout, componentResourceManager.GetString("lnkAbout.ToolTip"));
			this.lnkConfig.AccessibleDescription = null;
			this.lnkConfig.AccessibleName = null;
			componentResourceManager.ApplyResources(this.lnkConfig, "lnkConfig");
			this.lnkConfig.Font = null;
			this.lnkConfig.Name = "lnkConfig";
			this.lnkConfig.TabStop = true;
			this.toolTip1.SetToolTip(this.lnkConfig, componentResourceManager.GetString("lnkConfig.ToolTip"));
			this.fileDropHelper1.AttachedControl = this.taskList;
			this.fileDropHelper1.DropTypeEnable = FileDropHelper.DropType.File;
			this.fileDropHelper1.Enabled = true;
			this.fileDropHelper1.FileTypeFilter = "ttf|ttc|otf";
			this.fileDropHelper1.LinkType = DragDropEffects.Link;
			base.AccessibleDescription = null;
			base.AccessibleName = null;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = null;
			base.Controls.Add(this.lnkConfig);
			base.Controls.Add(this.lnkAbout);
			base.Controls.Add(this.btnRestore);
			base.Controls.Add(this.chkKillAll);
			base.Controls.Add(this.btnRun);
			base.Controls.Add(this.btnDeleteTask);
			base.Controls.Add(this.btnAddDel);
			base.Controls.Add(this.btnAdd);
			base.Controls.Add(this.taskList);
			base.Controls.Add(this.gradientBanner1);
			this.Font = null;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "MainForm";
			this.toolTip1.SetToolTip(this, componentResourceManager.GetString("$this.ToolTip"));
			base.Load += new EventHandler(this.MainForm_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public MainForm()
		{
			this.InitializeComponent();
			this.taskList.SmallImageList = LogView.ImgList;
			this.btnRun.Enabled = (this.btnDeleteTask.Enabled = false);
			this.fps.Enabled = true;
			this.chkKillAll.Checked = Program.CFG.GetConfig("KillAllProcesses", true);
			this.taskList.SelectedIndexChanged += delegate(object s, EventArgs e)
			{
				this.btnDeleteTask.Enabled = (this.taskList.SelectedIndices.Count > 0);
			};
			this.btnAddDel.Click += delegate(object s, EventArgs e)
			{
				this.AddTask(1);
			};
			this.btnAdd.Click += delegate(object s, EventArgs e)
			{
				this.AddTask(0);
			};
			this.btnDeleteTask.Click += delegate(object s, EventArgs e)
			{
				foreach (ListViewItem listViewItem in this.taskList.SelectedItems)
				{
					listViewItem.Remove();
				}
				this.btnRun.Enabled = (this.taskList.Items.Count > 0);
			};
			this.btnRun.Click += delegate(object s, EventArgs e)
			{
				this.RunWork();
			};
			this.lnkAbout.Click += delegate(object s, EventArgs e)
			{
				new About().ShowDialog();
			};
			this.chkKillAll.CheckedChanged += delegate(object s, EventArgs e)
			{
				Program.CFG.SetConfig("KillAllProcesses", this.chkKillAll.Checked);
			};
			this.fileDropHelper1.PathDroped += delegate(object s, FileDropHelper.PathDropedEventArgs e)
			{
				this.AddTaskAuto(e.Path);
			};
			this.btnRestore.Click += delegate(object s, EventArgs e)
			{
				this.RestoreBackup();
			};
			ControlHelper.AddShieldToButton(this.btnRun);
			this.lnkConfig.Click += delegate(object s, EventArgs e)
			{
				new Config
				{
					Location = Control.MousePosition
				}.Show();
			};
		}

		private void RestoreBackup()
		{
			PickupRestoreGroup pickupRestoreGroup = new PickupRestoreGroup();
			if (pickupRestoreGroup.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}
			this.taskList.Items.Clear();
			string[] files = pickupRestoreGroup.Files;
			for (int i = 0; i < files.Length; i++)
			{
				string path = files[i];
				this.AddTaskAuto(path);
			}
		}

		private void AddTaskAuto(string path)
		{
			string fileName = Path.GetFileName(path);
			string text = fileName.ToLower();
			if (!this.fonts.ContainsKey(text))
			{
				FunctionalForm.Infomation(string.Format(SR.FontNotInstalled, fileName));
				return;
			}
			this.AddTask(new TaskInfo(this.fonts[text], text, path, true));
		}

		public void ChangeUILanguage()
		{
			this.fps = new FontReplaceSet();
			this.fps.InitializeList(this.fonts);
		}

		private void AddTask(int mode)
		{
			this.fps.Mode = mode;
			this.fps.Clear();
			if (this.fps.ShowDialog() == DialogResult.OK)
			{
				this.AddTask(this.fps.Task);
			}
		}

		private void AddTask(TaskInfo t)
		{
			foreach (ListViewItem listViewItem in this.taskList.Items)
			{
				TaskInfo taskInfo = listViewItem.Tag as TaskInfo;
				if (taskInfo.FileKey == t.FileKey)
				{
					FunctionalForm.Infomation(SR.OperationAlreadyAdded);
					this.taskList.SelectedIndices.Clear();
					listViewItem.Selected = true;
					listViewItem.EnsureVisible();
					return;
				}
			}
			ListViewItem listViewItem2 = LogView.CreateItemStatic(t.ActionType ? LogView.RowType.Add : LogView.RowType.Remove, t.FileKey);
			listViewItem2.SubItems.Add(t.ActionType ? t.NewFile : SR.DeleteFont);
			listViewItem2.SubItems.Add("");
			listViewItem2.Tag = t;
			this.taskList.Items.Add(listViewItem2);
			listViewItem2.EnsureVisible();
			this.btnRun.Enabled = (this.taskList.Items.Count > 0);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Loading loading = new Loading();
			loading.ShowDialog();
			this.fps.InitializeList(loading.SystemFonts);
			this.fonts = loading.SystemFonts;
			if (Environment.OSVersion.Version.Major == 6)
			{
				FunctionalForm.Infomation(SR.Win7Detected);
			}
			base.Shown += new EventHandler(this.MainForm_Shown);
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.SourceDic))
			{
				this.CheckAutoLoad();
			}
			Program.CheckUpdate(false);
		}

		private void RunWork()
		{
			string content = string.Empty;
			if (this.chkKillAll.Checked)
			{
				content = SR.KillWarning;
			}
			else
			{
				content = SR.ExplorerWillBeKilled;
			}
			if (!FunctionalForm.Question(content, true))
			{
				return;
			}
			string text = Path.Combine(Application.StartupPath, "working");
			Directory.CreateDirectory(text);
			foreach (ListViewItem listViewItem in this.taskList.Items)
			{
				TaskInfo taskInfo = listViewItem.Tag as TaskInfo;
				this.SetStateInfo(listViewItem, SR.Copying);
				Application.DoEvents();
				if (taskInfo.ActionType)
				{
					File.Copy(taskInfo.NewFile, Path.Combine(text, Path.GetFileName(taskInfo.FilePath)), true);
				}
				else
				{
					File.WriteAllText(Path.Combine(text, Path.GetFileName(taskInfo.FilePath) + ".del"), "");
				}
				Application.DoEvents();
				this.SetStateInfo(listViewItem, SR.Ready);
			}
			string arguments = "-replace " + (this.chkKillAll.Checked ? "1" : "0") + " " + Process.GetCurrentProcess().Id.ToString();
			ProcessStartInfo processStartInfo = new ProcessStartInfo(Application.ExecutablePath, arguments);
			if (OS.OSMajorVersion > 5)
			{
				processStartInfo.Verb = "runas";
			}
			try
			{
				Process.Start(processStartInfo);
			}
			catch (Win32Exception ex)
			{
				FunctionalForm.Infomation(string.Format(SR.OperationFailed, ex.Message));
				return;
			}
			base.Close();
		}

		private void SetStateInfo(ListViewItem item, string message)
		{
			if (this._updateDelegate == null)
			{
				this._updateDelegate = new Action<ListViewItem, string>(this.SetStateInfo);
			}
			if (base.InvokeRequired)
			{
				base.Invoke(this._updateDelegate, new object[]
				{
					item,
					message
				});
				return;
			}
			item.SubItems[2].Text = message;
		}

		private void CheckAutoLoad()
		{
			string[] files = Directory.GetFiles(this.SourceDic);
			string str = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "\\Fonts\\";
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				string value = Path.GetExtension(text).ToLower();
				if (".ttf.ttc.otf.otc".IndexOf(value) != -1)
				{
					string fileName = Path.GetFileName(text);
					string text2 = str + fileName;
					if (File.Exists(text2))
					{
						TaskInfo taskInfo = new TaskInfo(text2, fileName.ToLower(), text, true);
						ListViewItem listViewItem = LogView.CreateItemStatic(taskInfo.ActionType ? LogView.RowType.Add : LogView.RowType.Remove, taskInfo.FileKey);
						listViewItem.SubItems.Add(taskInfo.ActionType ? taskInfo.NewFile : "删除这个字体");
						listViewItem.SubItems.Add("");
						listViewItem.Tag = taskInfo;
						this.taskList.Items.Add(listViewItem);
						listViewItem.EnsureVisible();
					}
				}
			}
			this.btnRun.Enabled = (this.taskList.Items.Count > 0);
		}
	}
}
