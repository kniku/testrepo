using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;
using RCS.Common.Tools;
using RCS.Classes;
using DCS_Rpc;
using DCS_Rpc.generated;
using System.Collections;
using System.Linq;


namespace RCS
{
	public partial class Main_class : Form
	{
		public class PMCSParameter
		{
			public bool showMinimized;
			public bool showMaximized;
			public bool withoutLogin;
			public string autoLogin;
			public string autoPwd;
			public string preferredLanguage;
		}

		void logParamError(string Arg)
		{
			AppGlobal.log.LogError("unknown/invalid program argument found: {0}", Arg);
		}

		void processArguments(String[] Args)
		{
			foreach (string Arg in Args)
			{
				string[] key_value = Arg.Split(':');
				switch (key_value[0].ToLower())
				{
					case "/show":
						if (key_value.Count() > 1)
						{
							switch (key_value[1].ToLower())
							{
								case "max":
									AppGlobal.ProgramParameter.showMaximized = true;
									break;
								case "min":
									AppGlobal.ProgramParameter.showMinimized = true;
									break;
								default:
									logParamError(Arg);
									break;
							}
						}
						break;
					case "/no_login":
						AppGlobal.ProgramParameter.withoutLogin = true;
						break;
					case "/help":
						MessageBox.Show("usage: RCS.exe [options]" + Environment.NewLine + Environment.NewLine +
							"Valid options are:" + Environment.NewLine +
							"/help\t\t... this help" + Environment.NewLine +
							"/language:LANG\t... use LANG as GUI language" + Environment.NewLine +
							"/login:USER\t... auto login (user)" + Environment.NewLine +
							"/no_login\t\t... run without login prompt" + Environment.NewLine +
							"/pwd:PASS\t... auto login (password)" + Environment.NewLine +
							"/show:min|max\t... run minimized or maximized", "RCS usage", MessageBoxButtons.OK, MessageBoxIcon.Information);
						Main_class_FormClosing(this, null);
						break;
					case "/language":
						if (key_value.Count() > 1)
						{
							AppGlobal.ProgramParameter.preferredLanguage = key_value[1];
						}
						else
						{
							logParamError(Arg);
						}
						break;
					case "/login":
						if (key_value.Count() > 1)
						{
							AppGlobal.ProgramParameter.autoLogin = key_value[1];
						}
						else
						{
							logParamError(Arg);
						}
						break;
					case "/pwd":
						if (key_value.Count() > 1)
						{
							AppGlobal.ProgramParameter.autoPwd = key_value[1];
						}
						else
						{
							logParamError(Arg);
						}
						break;
					default:
						logParamError(Arg);
						break;
				}
			}
		}
	}
}