//using FSLib.App.SimpleUpdater;
using FSLib.Windows.Config;
using FSLib.Windows.Dialogs;
using FSLib.Windows.Forms;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SystemFontReplacer
{
	internal static class Program
	{
		internal static readonly RegistryConfigStorage CFG = new RegistryConfigStorage(false, "FishGarden", "SystemFontReplacer", true);

		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Program.SetCulture();
			ThreadException.SettingUpForm();
			if (Program.CFG.GetConfig("FirstRun", true))
			{
				FunctionalForm.Infomation(SR.NotGreenWareWarning);
				Program.CFG.SetConfig("FirstRun", false);
			}
			if (args != null && args.Length == 3)
			{
				if (args[0] == "-replace")
				{
					RunReplaceTask mainForm = new RunReplaceTask
					{
						ForceKillAllNonSysProcess = (args[1] == "1"),
						ParentProcessID = int.Parse(args[2])
					};
					Application.Run(mainForm);
					return;
				}
			}
			else
			{
				MainForm mainForm2 = new MainForm();
				string text = Application.StartupPath + "\\source";
				if (Directory.Exists(text))
				{
					mainForm2.SourceDic = text;
				}
				Application.Run(mainForm2);
			}
		}

		internal static void SetCulture()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Program.CFG.GetConfig("Language", CultureInfo.InvariantCulture.LCID)));
		}

		internal static void CheckUpdate(bool forceCheck)
		{
			string name = "lastupdate";
			DateTime dateTime = Program.CFG.GetDateTime(name);
			if (!forceCheck && dateTime.Day == DateTime.Now.Day)
			{
				return;
			}
			Program.CFG.SetDateTime(name, DateTime.Now);
			//Updater.CheckUpdateSimple();
		}
	}
}
