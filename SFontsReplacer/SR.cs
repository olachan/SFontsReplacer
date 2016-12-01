using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SystemFontReplacer
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class SR
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(SR.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("SystemFontReplacer.SR", typeof(SR).Assembly);
					SR.resourceMan = resourceManager;
				}
				return SR.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		internal static string AutoReboot
		{
			get
			{
				return SR.ResourceManager.GetString("AutoReboot", SR.resourceCulture);
			}
		}

		internal static string AutoSelect
		{
			get
			{
				return SR.ResourceManager.GetString("AutoSelect", SR.resourceCulture);
			}
		}

		internal static string ConfirmToDeleteSettings
		{
			get
			{
				return SR.ResourceManager.GetString("ConfirmToDeleteSettings", SR.resourceCulture);
			}
		}

		internal static string Copying
		{
			get
			{
				return SR.ResourceManager.GetString("Copying", SR.resourceCulture);
			}
		}

		internal static string DeleteFont
		{
			get
			{
				return SR.ResourceManager.GetString("DeleteFont", SR.resourceCulture);
			}
		}

		internal static string ExplorerWillBeKilled
		{
			get
			{
				return SR.ResourceManager.GetString("ExplorerWillBeKilled", SR.resourceCulture);
			}
		}

		internal static string FontNotInstalled
		{
			get
			{
				return SR.ResourceManager.GetString("FontNotInstalled", SR.resourceCulture);
			}
		}

		internal static string KillWarning
		{
			get
			{
				return SR.ResourceManager.GetString("KillWarning", SR.resourceCulture);
			}
		}

		internal static string Log_AddingDeleteTask
		{
			get
			{
				return SR.ResourceManager.GetString("Log_AddingDeleteTask", SR.resourceCulture);
			}
		}

		internal static string Log_AddingTask
		{
			get
			{
				return SR.ResourceManager.GetString("Log_AddingTask", SR.resourceCulture);
			}
		}

		internal static string Log_AddReplaceTask
		{
			get
			{
				return SR.ResourceManager.GetString("Log_AddReplaceTask", SR.resourceCulture);
			}
		}

		internal static string Log_BackupFile
		{
			get
			{
				return SR.ResourceManager.GetString("Log_BackupFile", SR.resourceCulture);
			}
		}

		internal static string Log_CopyNewFile
		{
			get
			{
				return SR.ResourceManager.GetString("Log_CopyNewFile", SR.resourceCulture);
			}
		}

		internal static string Log_CreateBackupFolder
		{
			get
			{
				return SR.ResourceManager.GetString("Log_CreateBackupFolder", SR.resourceCulture);
			}
		}

		internal static string Log_CreateBackupFolderFinished
		{
			get
			{
				return SR.ResourceManager.GetString("Log_CreateBackupFolderFinished", SR.resourceCulture);
			}
		}

		internal static string Log_DeleteTempFile
		{
			get
			{
				return SR.ResourceManager.GetString("Log_DeleteTempFile", SR.resourceCulture);
			}
		}

		internal static string Log_Deleting
		{
			get
			{
				return SR.ResourceManager.GetString("Log_Deleting", SR.resourceCulture);
			}
		}

		internal static string Log_Fail
		{
			get
			{
				return SR.ResourceManager.GetString("Log_Fail", SR.resourceCulture);
			}
		}

		internal static string Log_FailedNote
		{
			get
			{
				return SR.ResourceManager.GetString("Log_FailedNote", SR.resourceCulture);
			}
		}

		internal static string Log_FileCopyFailed
		{
			get
			{
				return SR.ResourceManager.GetString("Log_FileCopyFailed", SR.resourceCulture);
			}
		}

		internal static string Log_FileCopySucc
		{
			get
			{
				return SR.ResourceManager.GetString("Log_FileCopySucc", SR.resourceCulture);
			}
		}

		internal static string Log_FileNotRollback
		{
			get
			{
				return SR.ResourceManager.GetString("Log_FileNotRollback", SR.resourceCulture);
			}
		}

		internal static string Log_FileRollback
		{
			get
			{
				return SR.ResourceManager.GetString("Log_FileRollback", SR.resourceCulture);
			}
		}

		internal static string Log_KillingExplorer
		{
			get
			{
				return SR.ResourceManager.GetString("Log_KillingExplorer", SR.resourceCulture);
			}
		}

		internal static string Log_KillingFontCache
		{
			get
			{
				return SR.ResourceManager.GetString("Log_KillingFontCache", SR.resourceCulture);
			}
		}

		internal static string Log_KillingNonSystemProcesses
		{
			get
			{
				return SR.ResourceManager.GetString("Log_KillingNonSystemProcesses", SR.resourceCulture);
			}
		}

		internal static string Log_KillingProcess
		{
			get
			{
				return SR.ResourceManager.GetString("Log_KillingProcess", SR.resourceCulture);
			}
		}

		internal static string Log_KillingProcessFinished
		{
			get
			{
				return SR.ResourceManager.GetString("Log_KillingProcessFinished", SR.resourceCulture);
			}
		}

		internal static string Log_MoveFile
		{
			get
			{
				return SR.ResourceManager.GetString("Log_MoveFile", SR.resourceCulture);
			}
		}

		internal static string Log_ParentProcessExited
		{
			get
			{
				return SR.ResourceManager.GetString("Log_ParentProcessExited", SR.resourceCulture);
			}
		}

		internal static string Log_RunningDeleteTask
		{
			get
			{
				return SR.ResourceManager.GetString("Log_RunningDeleteTask", SR.resourceCulture);
			}
		}

		internal static string Log_RunningReplaceTask
		{
			get
			{
				return SR.ResourceManager.GetString("Log_RunningReplaceTask", SR.resourceCulture);
			}
		}

		internal static string Log_RunTaskFinished
		{
			get
			{
				return SR.ResourceManager.GetString("Log_RunTaskFinished", SR.resourceCulture);
			}
		}

		internal static string Log_ScaningSysFonts
		{
			get
			{
				return SR.ResourceManager.GetString("Log_ScaningSysFonts", SR.resourceCulture);
			}
		}

		internal static string Log_Successful
		{
			get
			{
				return SR.ResourceManager.GetString("Log_Successful", SR.resourceCulture);
			}
		}

		internal static string Log_TakeOwner
		{
			get
			{
				return SR.ResourceManager.GetString("Log_TakeOwner", SR.resourceCulture);
			}
		}

		internal static string LogFileHeader
		{
			get
			{
				return SR.ResourceManager.GetString("LogFileHeader", SR.resourceCulture);
			}
		}

		internal static string LogFileOperationStartTime
		{
			get
			{
				return SR.ResourceManager.GetString("LogFileOperationStartTime", SR.resourceCulture);
			}
		}

		internal static string LogTaskBegin
		{
			get
			{
				return SR.ResourceManager.GetString("LogTaskBegin", SR.resourceCulture);
			}
		}

		internal static string NoBackup
		{
			get
			{
				return SR.ResourceManager.GetString("NoBackup", SR.resourceCulture);
			}
		}

		internal static string NoFontSelected
		{
			get
			{
				return SR.ResourceManager.GetString("NoFontSelected", SR.resourceCulture);
			}
		}

		internal static string NoNewFileSelected
		{
			get
			{
				return SR.ResourceManager.GetString("NoNewFileSelected", SR.resourceCulture);
			}
		}

		internal static string NotGreenWareWarning
		{
			get
			{
				return SR.ResourceManager.GetString("NotGreenWareWarning", SR.resourceCulture);
			}
		}

		internal static string OperationAlreadyAdded
		{
			get
			{
				return SR.ResourceManager.GetString("OperationAlreadyAdded", SR.resourceCulture);
			}
		}

		internal static string OperationFailed
		{
			get
			{
				return SR.ResourceManager.GetString("OperationFailed", SR.resourceCulture);
			}
		}

		internal static string Ready
		{
			get
			{
				return SR.ResourceManager.GetString("Ready", SR.resourceCulture);
			}
		}

		internal static string ScanSystemFonts
		{
			get
			{
				return SR.ResourceManager.GetString("ScanSystemFonts", SR.resourceCulture);
			}
		}

		internal static string SureToDeleteBackup
		{
			get
			{
				return SR.ResourceManager.GetString("SureToDeleteBackup", SR.resourceCulture);
			}
		}

		internal static string ThanksForUsing
		{
			get
			{
				return SR.ResourceManager.GetString("ThanksForUsing", SR.resourceCulture);
			}
		}

		internal static string UninstallSuccessfully
		{
			get
			{
				return SR.ResourceManager.GetString("UninstallSuccessfully", SR.resourceCulture);
			}
		}

		internal static string VersionInfo
		{
			get
			{
				return SR.ResourceManager.GetString("VersionInfo", SR.resourceCulture);
			}
		}

		internal static string WaitingParentProcessExit
		{
			get
			{
				return SR.ResourceManager.GetString("WaitingParentProcessExit", SR.resourceCulture);
			}
		}

		internal static string Win7Detected
		{
			get
			{
				return SR.ResourceManager.GetString("Win7Detected", SR.resourceCulture);
			}
		}

		internal SR()
		{
		}
	}
}
