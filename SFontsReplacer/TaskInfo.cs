using System;

namespace SystemFontReplacer
{
	internal class TaskInfo
	{
		public string FilePath
		{
			get;
			set;
		}

		public string FileKey
		{
			get;
			set;
		}

		public string NewFile
		{
			get;
			set;
		}

		public bool ActionType
		{
			get;
			set;
		}

		public TaskInfo(string filePath, string fileKey, string newFile, bool actionType)
		{
			this.FilePath = filePath;
			this.FileKey = fileKey;
			this.NewFile = newFile;
			this.ActionType = actionType;
		}
	}
}
