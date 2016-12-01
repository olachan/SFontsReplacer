using FSLib.Windows;
using FSLib.Windows.Forms;
using FSLib.Windows.Win32.UnsafeNativeMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SystemFontReplacer.Properties;

namespace SystemFontReplacer
{
	internal class RunReplaceTask : PerPixelAlphaForm
	{
		private BackgroundWorker bgw = new BackgroundWorker
		{
			WorkerReportsProgress = false,
			WorkerSupportsCancellation = false
		};

		private Dictionary<string, string> replaceFonts;

		private int succCount;

		private int failCount;

		private string fontsDic;

		private string windowPath;

		private string workfolder;

		private string backupFolder;

		private string logFile;

		private StringBuilder sb = new StringBuilder(20480);

		public bool ForceKillAllNonSysProcess
		{
			get;
			set;
		}

		public int ParentProcessID
		{
			get;
			set;
		}

		private Dictionary<string, string> SystemFonts
		{
			get;
			set;
		}

		public RunReplaceTask()
		{
			base.StartPosition = FormStartPosition.CenterScreen;
			base.ShowInTaskbar = false;
			base.TopMost = true;
			base.FormFadeIn += delegate(object s, EventArgs e)
			{
				this.bgw.RunWorkerAsync();
			};
			base.Shown += delegate(object x, EventArgs y)
			{
				base.FormBitmap = Resources.Spalsh;
			};
			this.bgw.DoWork += new DoWorkEventHandler(this.bgw_DoWork);
			this.bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
		}

		private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.WriteLogFile();
			ProcessStartInfo startInfo = new ProcessStartInfo("explorer.exe");
			Process.Start(startInfo);
			if (e.Error != null)
			{
				FunctionalForm.Infomation(e.Error.ToString());
			}
			string text = string.Format(SR.Log_RunTaskFinished, this.succCount, this.failCount);
			if (this.failCount > 0)
			{
				text = text + Environment.NewLine + SR.Log_FailedNote;
			}
			if (this.succCount > 0)
			{
				text = text + Environment.NewLine + SR.AutoReboot;
				if (FunctionalForm.Question(text, true))
				{
					RunReplaceTask.Reboot();
				}
			}
			else
			{
				FunctionalForm.Infomation(text);
			}
			Process.Start(this.logFile);
			base.Close();
		}

		private void bgw_DoWork(object sender, DoWorkEventArgs e)
		{
			Program.SetCulture();
			this.workfolder = Path.Combine(Application.StartupPath, "working");
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
			this.windowPath = Path.GetDirectoryName(folderPath);
			this.sb.AppendLine(string.Format(SR.LogFileHeader, Application.ProductVersion));
			this.sb.AppendLine(string.Format(SR.LogFileOperationStartTime, DateTime.Now));
			this.sb.AppendLine("-----------------------------------------------------------------------------------");
			this.KillProcess();
			this.DeleteFontCache();
			this.CreateBackupFolder();
			this.ScanSystemFonts();
			this.ScanTask();
			this.sb.AppendLine();
			this.sb.AppendLine(SR.LogTaskBegin);
			foreach (string current in this.replaceFonts.Keys)
			{
				string text = Path.Combine(this.fontsDic, current);
				if (string.IsNullOrEmpty(this.replaceFonts[current]))
				{
					this.sb.AppendLine(string.Format(SR.Log_RunningDeleteTask, current));
					if (this.TakeOwner(text) && this.RunDelete(text, true))
					{
						this.succCount++;
					}
					else
					{
						this.failCount++;
					}
				}
				else
				{
					this.sb.AppendLine(string.Format(SR.Log_RunningReplaceTask, current));
					if (this.TakeOwner(text) && this.RunReplace(text, this.replaceFonts[current]))
					{
						this.succCount++;
					}
					else
					{
						this.failCount++;
					}
				}
				this.sb.AppendLine();
			}
			this.sb.AppendLine();
			this.sb.AppendLine();
			this.sb.AppendLine("-----------------------------------------------------------------------------------");
			this.sb.AppendLine(string.Format(SR.Log_RunTaskFinished, this.succCount, this.failCount));
			if (this.failCount > 0)
			{
				this.sb.AppendLine(SR.Log_FailedNote);
			}
		}

		private void CreateBackupFolder()
		{
			this.sb.Append(SR.Log_CreateBackupFolder);
			string text = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
			this.backupFolder = Path.Combine(Application.StartupPath, string.Format("backup{0}{1}", Path.DirectorySeparatorChar, text));
			if (!Directory.Exists(this.backupFolder))
			{
				Directory.CreateDirectory(this.backupFolder);
			}
			this.sb.AppendLine(string.Format(SR.Log_CreateBackupFolderFinished, text));
		}

		private void WriteLogFile()
		{
			string arg = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
			this.logFile = Path.Combine(Application.StartupPath, string.Format("logs{0}{1}.log", Path.DirectorySeparatorChar, arg));
			string directoryName = Path.GetDirectoryName(this.logFile);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			File.WriteAllText(this.logFile, this.sb.ToString());
		}

		private bool RunReplace(string fileName, string newFile)
		{
			this.BackupOriginalFont(fileName);
			if (!this.RunDelete(fileName, false))
			{
				return false;
			}
			this.sb.Append(SR.Log_CopyNewFile);
			try
			{
				File.Copy(newFile, fileName);
				File.Delete(newFile);
				this.sb.AppendLine(SR.Log_FileCopySucc);
			}
			catch (Exception ex)
			{
				this.sb.Append(string.Format(SR.Log_FileCopyFailed, ex.Message));
				string text = fileName + ".bak";
				if (File.Exists(text))
				{
					File.Move(text, fileName);
					this.sb.AppendLine(SR.Log_FileRollback);
				}
				else
				{
					this.sb.AppendLine(SR.Log_FileNotRollback);
				}
				return false;
			}
			return true;
		}

		private bool RunDelete(string fileName, bool backup)
		{
			if (backup)
			{
				this.BackupOriginalFont(fileName);
			}
			this.sb.AppendLine(SR.Log_Deleting);
			string text = fileName + ".bak";
			if (File.Exists(text))
			{
				if (!this.TakeOwner(text))
				{
					return false;
				}
				try
				{
					this.sb.Append(string.Format(SR.Log_DeleteTempFile, Path.GetFileName(text)));
					File.Delete(text);
					this.sb.AppendLine(SR.Log_Successful);
				}
				catch (Exception ex)
				{
					this.sb.AppendLine(string.Format(SR.Log_Fail, ex.Message));
					bool result = false;
					return result;
				}
			}
			try
			{
				this.sb.Append(SR.Log_MoveFile);
				File.Move(fileName, text);
				this.sb.AppendLine(SR.Log_Successful);
			}
			catch (Exception ex2)
			{
				this.sb.AppendLine(string.Format(SR.Log_Fail, ex2.Message));
				bool result = false;
				return result;
			}
			return true;
		}

		private void BackupOriginalFont(string fileName)
		{
			this.sb.Append(SR.Log_BackupFile);
			string destFileName = Path.Combine(this.backupFolder, Path.GetFileName(fileName));
			File.Copy(fileName, destFileName, true);
			this.sb.AppendLine(SR.Log_Successful);
		}

		private void ScanTask()
		{
			this.sb.AppendLine(SR.Log_AddingTask);
			this.replaceFonts = new Dictionary<string, string>();
			string[] files = Directory.GetFiles(this.workfolder);
			Array.ForEach<string>(files, delegate(string s)
			{
				string fileName = Path.GetFileName(s);
				string a = Path.GetExtension(s).ToLower();
				bool flag = a == ".del";
				if (flag)
				{
					this.replaceFonts.Add(fileName, "");
					this.sb.AppendLine(string.Format(SR.Log_AddingDeleteTask, fileName));
					File.Delete(s);
					return;
				}
				this.replaceFonts.Add(fileName, s);
				this.sb.AppendLine(string.Format(SR.Log_AddReplaceTask, fileName));
			});
		}

		private void KillProcess()
		{
			this.KillParentProcess();
			if (this.ForceKillAllNonSysProcess)
			{
				this.KillNonSystemProcesses();
				return;
			}
			this.KillExplorer();
		}

		private void KillParentProcess()
		{
			this.sb.AppendLine(SR.WaitingParentProcessExit);
			try
			{
				Process processById = Process.GetProcessById(this.ParentProcessID);
				if (processById != null)
				{
					if (!processById.HasExited)
					{
						processById.WaitForExit(10000);
					}
					if (!processById.HasExited)
					{
						this.sb.AppendLine(SR.Log_KillingProcess);
						processById.Kill();
					}
					else
					{
						this.sb.AppendLine(SR.Log_ParentProcessExited);
					}
				}
			}
			catch (Exception)
			{
				this.sb.AppendLine(SR.Log_ParentProcessExited);
			}
		}

		private void KillNonSystemProcesses()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
			this.KillExplorer();
			int id = Process.GetCurrentProcess().Id;
			this.sb.AppendLine(SR.Log_KillingNonSystemProcesses);
			Process[] processes = Process.GetProcesses();
			Process[] array = processes;
			int i = 0;
			while (i < array.Length)
			{
				Process process = array[i];
				string text = string.Empty;
				try
				{
					if (process.Id == id || process.HasExited)
					{
						goto IL_CF;
					}
					text = process.MainModule.FileName;
				}
				catch (Exception)
				{
					goto IL_CF;
				}
				goto IL_6D;
				IL_CF:
				i++;
				continue;
				IL_6D:
				if (text.IndexOf(folderPath, StringComparison.OrdinalIgnoreCase) == -1)
				{
					try
					{
						this.sb.Append(string.Format(SR.Log_KillingProcess, process.ProcessName));
						process.Kill();
						this.sb.AppendLine(SR.Log_Successful);
					}
					catch (Exception ex)
					{
						this.sb.AppendLine(string.Format(SR.Log_Fail, ex.Message));
					}
					goto IL_CF;
				}
				goto IL_CF;
			}
			this.sb.AppendLine(SR.Log_KillingProcessFinished);
		}

		private void KillExplorer()
		{
			this.sb.Append(SR.Log_KillingExplorer);
			Process[] processesByName = Process.GetProcessesByName("explorer");
			if (processesByName != null && processesByName.Length > 0)
			{
				Array.ForEach<Process>(processesByName, delegate(Process s)
				{
					API.TerminateProcess(s.Handle, 1u);
				});
			}
			this.sb.AppendLine(SR.Log_Successful);
		}

		private void DeleteFontCache()
		{
			this.sb.Append(SR.Log_KillingFontCache);
			string path = Path.Combine(Environment.SystemDirectory, "FntCache.dat");
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			this.sb.AppendLine(SR.Log_Successful);
		}

		private void ScanSystemFonts()
		{
			this.sb.Append(SR.Log_ScaningSysFonts);
			this.SystemFonts = new Dictionary<string, string>();
			this.fontsDic = Path.Combine(this.windowPath, "Fonts");
			string[] files = Directory.GetFiles(this.fontsDic);
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
			this.sb.AppendLine(SR.Log_Successful);
		}

		private bool TakeOwner(string filepath)
		{
			if (OS.OSMajorVersion <= 5)
			{
				return true;
			}
			this.sb.Append(string.Format(SR.Log_TakeOwner, Path.GetFileName(filepath)));
			try
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				processStartInfo.FileName = "takeown";
				processStartInfo.Arguments = string.Format("/f \"{0}\" /a", filepath);
				processStartInfo.UseShellExecute = false;
				processStartInfo.CreateNoWindow = true;
				processStartInfo.RedirectStandardOutput = true;
				processStartInfo.RedirectStandardInput = true;
				processStartInfo.RedirectStandardError = true;
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				Process.Start(processStartInfo).WaitForExit();
				processStartInfo.FileName = "icacls";
				processStartInfo.Arguments = string.Format("\"{0}\" /grant everyone:F", filepath);
				Process.Start(processStartInfo).WaitForExit();
				processStartInfo = new ProcessStartInfo();
				processStartInfo.FileName = "takeown";
				processStartInfo.Arguments = string.Format("/f \"{0}\" /a", Path.GetDirectoryName(filepath));
				processStartInfo.UseShellExecute = false;
				processStartInfo.CreateNoWindow = true;
				processStartInfo.RedirectStandardOutput = true;
				processStartInfo.RedirectStandardInput = true;
				processStartInfo.RedirectStandardError = true;
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				Process.Start(processStartInfo).WaitForExit();
				processStartInfo.FileName = "icacls";
				processStartInfo.Arguments = string.Format("\"{0}\" /grant everyone:F", Path.GetDirectoryName(filepath));
				Process.Start(processStartInfo).WaitForExit();
			}
			catch (Exception ex)
			{
				this.sb.AppendLine(string.Format(SR.Log_Fail, ex.Message));
				return false;
			}
			this.sb.AppendLine(SR.Log_Successful);
			return true;
		}

		internal static void MCBuilder()
		{
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = "mcbuilder.exe",
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardOutput = true,
					RedirectStandardInput = true,
					RedirectStandardError = true,
					WindowStyle = ProcessWindowStyle.Hidden
				}).WaitForExit();
			}
			catch (Exception)
			{
			}
		}

		internal static void Reboot()
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "shutdown.exe",
				Arguments = "-t 0 -r -f",
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				RedirectStandardInput = true,
				RedirectStandardError = true,
				WindowStyle = ProcessWindowStyle.Hidden
			}).WaitForExit();
		}
	}
}
