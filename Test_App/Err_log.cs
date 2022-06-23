using System;
using System.IO;

namespace Test_App;

internal class Err_log
{
	private string xmlFilePath = "c:/EZ-5/users/" + Environment.UserName + "/log";

	public void writelog(string err_msg, string datetime, string stk_trace)
	{
		string path = xmlFilePath + "/ez5_" + DateTime.Now.Day + "-" + DateTime.Now.Month + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".log";
		if (Directory.Exists(xmlFilePath))
		{
			FileStream fileStream = File.Create(path);
			fileStream.Close();
			File.AppendAllText(path, datetime + " : " + err_msg + "(" + stk_trace + ")" + Environment.NewLine);
		}
		else
		{
			Directory.CreateDirectory(xmlFilePath);
			File.AppendAllText(path, datetime + " : " + err_msg + "(" + stk_trace + ")" + Environment.NewLine);
		}
	}
}
