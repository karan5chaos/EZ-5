using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Test_App.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
public sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("1")]
	public string Setting
	{
		get
		{
			return (string)this["Setting"];
		}
		set
		{
			this["Setting"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool Autostart
	{
		get
		{
			return (bool)this["Autostart"];
		}
		set
		{
			this["Autostart"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("01/13/2016 19:13:00")]
	public DateTime last_logon
	{
		get
		{
			return (DateTime)this["last_logon"];
		}
		set
		{
			this["last_logon"] = value;
		}
	}
}
