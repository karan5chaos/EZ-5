using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Test_App.Properties;

namespace Test_App;

public class Form1 : Form
{
	private string monitorpath = "c:/EZ-5/Users/" + Environment.UserName + "/dataxml";

	private string xmlFilePath = "c:/EZ-5/Users/" + Environment.UserName + "/dataxml/Data.XML";

	private string xmlFilePathdc = "c:/EZ-5/Users/" + Environment.UserName + "/dataxml/Data.XML.dc";

	private string path = "";

	private static int chromex = 0;

	private static int ffx = 0;

	private static int operax = 0;

	private static int proc_time;

	private static int web_time;

	private static int startup_time;

	private int totalproc = 0;

	private int totalweb = 0;

	private IContainer components = null;

	private Timer timer1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem setPathToolStripMenuItem;

	private SaveFileDialog saveFileDialog1;

	private OpenFileDialog openFileDialog1;

	private NotifyIcon notifyIcon1;

	private ImageList imageList1;

	private ToolTip toolTip1;

	private FileSystemWatcher fileSystemWatcher1;

	private Timer timer2;

	private ImageList onandoff;

	private Panel panel2;

	private ListView listView1;

	private BackgroundWorker backgroundWorker1;

	private BackgroundWorker backgroundWorker2;

	private BackgroundWorker backgroundWorker3;

	private BackgroundWorker backgroundWorker4;

	private BackgroundWorker backgroundWorker5;

	private BackgroundWorker backgroundWorker6;

	private TextBox textBox2;

	private ColorDialog colorDialog1;

	private Timer timer3;

	private MenuStrip menuStrip1;

	private ToolStripMenuItem fileToolStripMenuItem;

	private ToolStripMenuItem addProcessToolStripMenuItem;

	private ToolStripMenuItem addWebLinkToolStripMenuItem;

	private ToolStripMenuItem importExportToolStripMenuItem;

	private ToolStripMenuItem importLinksToolStripMenuItem;

	private ToolStripMenuItem exportLinksToolStripMenuItem;

	private ToolStripMenuItem aboutToolStripMenuItem;

	private ToolStripMenuItem actstas;

	private ToolStripMenuItem runAppsToolStripMenuItem;

	private ToolStripMenuItem manageToolStripMenuItem;

	private GroupBox groupBox5;

	private HelpProvider helpProvider1;

	private GroupBox groupBox2;

	private ListBox listBox1;

	private Timer data_refresh;

	private ToolStripMenuItem toolStripMenuItem1;

	private ToolStripMenuItem resetToolStripMenuItem;

	private ColumnHeader columnHeader1;

	public Form1()
	{
		InitializeComponent();
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		execution_auto();
		autostart_determination();
		stopwatch.Stop();
		startup_time = Convert.ToInt32(stopwatch.ElapsedMilliseconds);
		refresh();
		populate_statistics();
	}

	private void populate_statistics()
	{
		int num = startup_time + proc_time + web_time;
		listBox1.Items.Clear();
		listBox1.Items.Add("GET_PROCESS_TIME");
		listBox1.Items.Add("    > startup_time : " + startup_time + " Msecs.");
	}

	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			Settings.Default.Save();
			notifyIcon1.Dispose();
		}
		catch (Exception ex)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
		import_data(openFileDialog1.FileName);
		refresh();
	}

	public void chkdir()
	{
		notifyIcon1.BalloonTipText = "Setup Completed.";
		notifyIcon1.BalloonTipTitle = "Setup Status";
		notifyIcon1.Visible = true;
		notifyIcon1.ShowBalloonTip(3);
	}

	public void import_data(string path)
	{
		try
		{
			Decrypt(path, path + ".tmp");
			XDocument xDocument = XDocument.Load(path + ".tmp");
			IEnumerable<XElement> enumerable = xDocument.Descendants("subproc").Descendants("process_path");
			int num = 0;
			int num2 = 0;
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			List<string> list2 = new List<string>();
			foreach (XElement item in enumerable)
			{
				if (item.HasAttributes && !list.Contains(new KeyValuePair<string, string>(item.Value, item.FirstAttribute.Value)))
				{
					list.Add(new KeyValuePair<string, string>(item.Value, item.FirstAttribute.Value));
				}
				else if (!item.HasAttributes)
				{
					list2.Add(item.Value);
				}
			}
			XDocument xDocument2 = XDocument.Load(xmlFilePath);
			XElement xElement = xDocument2.Element("process");
			foreach (KeyValuePair<string, string> item2 in list)
			{
				xElement.Add(new XElement("subproc", new XElement("process_path", item2.Key, new XAttribute("link", item2.Value))));
				num++;
			}
			for (int i = 0; i < list2.Count; i++)
			{
				xElement.Add(new XElement("subproc", new XElement("process_path", list2[i].ToString())));
				num2++;
			}
			xDocument2.Save(xmlFilePath);
			File.Delete(path + ".tmp");
			int num3 = num + num2;
			notifyIcon1.ShowBalloonTip(3000, "Import Task Finished", "Data Imported : \n \nLinks : " + num + " \nProcesses : " + num2 + " \nTotal Data Imported : " + num3, ToolTipIcon.Info);
		}
		catch (XmlException ex)
		{
			File.Delete(path + ".tmp");
			MessageBox.Show("Please select a valid list file", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
		catch (Exception ex2)
		{
			File.Delete(path + ".tmp");
			MessageBox.Show(ex2.Message.ToString());
			Err_log err_log2 = new Err_log();
			err_log2.writelog(ex2.Message, DateTime.Now.ToString(), ex2.StackTrace);
		}
	}

	public void export_data()
	{
		int num = 0;
		int num2 = 0;
		try
		{
			XDocument xDocument = XDocument.Load(xmlFilePath);
			IEnumerable<XElement> enumerable = xDocument.Descendants("subproc").Descendants("process_path");
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			List<string> list2 = new List<string>();
			foreach (XElement item in enumerable)
			{
				if (item.HasAttributes && !list.Contains(new KeyValuePair<string, string>(item.Value, item.FirstAttribute.Value)))
				{
					list.Add(new KeyValuePair<string, string>(item.Value, item.FirstAttribute.Value));
				}
				else if (!item.HasAttributes)
				{
					list2.Add(item.Value);
				}
			}
			XDocument xDocument2 = new XDocument(new XElement("process"));
			XElement xElement = xDocument2.Element("process");
			foreach (KeyValuePair<string, string> item2 in list)
			{
				xElement.Add(new XElement("subproc", new XElement("process_path", item2.Key, new XAttribute("link", item2.Value))));
				num++;
			}
			for (int i = 0; i < list2.Count; i++)
			{
				xElement.Add(new XElement("subproc", new XElement("process_path", list2[i].ToString())));
				num2++;
			}
			path = saveFileDialog1.FileName + ".dc";
			xDocument2.Save(saveFileDialog1.FileName);
			Encrypt(saveFileDialog1.FileName, saveFileDialog1.FileName + ".dc");
			File.Delete(saveFileDialog1.FileName);
			int num3 = num + num2;
			notifyIcon1.ShowBalloonTip(3000, "Export Task Finished", "Data Exported : \n \nLinks : " + num + " \nProcesses : " + num2 + " \nTotal Data Exported : " + num3, ToolTipIcon.Info);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
		export_data();
		refresh();
	}

	private void Form1_DragEnter(object sender, DragEventArgs e)
	{
		Form1 form = new Form1();
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
		{
			e.Effect = DragDropEffects.Copy;
		}
	}

	private void Form1_DragDrop(object sender, DragEventArgs e)
	{
		string[] array = (string[])e.Data.GetData(DataFormats.FileDrop, autoConvert: false);
		try
		{
			XDocument xDocument = XDocument.Load(xmlFilePath);
			XElement xElement = xDocument.Element("process");
			string[] array2 = array;
			foreach (string content in array2)
			{
				if (Path.GetExtension(content).Equals(".dc", StringComparison.CurrentCulture))
				{
					if (MessageBox.Show("Do you want to import data from this list file ?", "Import Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
					{
					}
				}
				else
				{
					xElement.Add(new XElement("subproc", new XElement("process_path", content)));
				}
			}
			xDocument.Save(xmlFilePath);
			listView1.Items.Clear();
			refresh();
		}
		catch (Exception ex)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	private void Form1_DragLeave(object sender, EventArgs e)
	{
	}

	private void currentProcessToolStripMenuItem_Click(object sender, EventArgs e)
	{
		curr_proc curr_proc2 = new curr_proc();
		curr_proc2.ShowDialog(this);
	}

	public void webstw(string atts, string attfs)
	{
		if (atts.Contains("chrome") || atts.Contains("Chrome"))
		{
			ListViewItem listViewItem = new ListViewItem();
			listViewItem.Tag = "chrome";
			listViewItem.Text = attfs;
			listView1.Items.Add(listViewItem);
			chromex++;
		}
		else if (atts.Contains("firefox") || atts.Contains("Firefox"))
		{
			ListViewItem listViewItem2 = new ListViewItem();
			listViewItem2.Tag = "fire";
			listViewItem2.Text = attfs;
			listView1.Items.Add(listViewItem2);
			ffx++;
		}
		else if (atts.Contains("opera"))
		{
			ListViewItem listViewItem3 = new ListViewItem();
			listViewItem3.Tag = "opera";
			listViewItem3.Text = attfs;
			listView1.Items.Add(listViewItem3);
			operax++;
		}
	}

	public void get_procs()
	{
		try
		{
			listView1.Items.Clear();
			XDocument xDocument = XDocument.Load(xmlFilePath);
			IEnumerable<XElement> enumerable = xDocument.Descendants("subproc").Descendants("process_path");
			Stopwatch stopwatch = Stopwatch.StartNew();
			foreach (XElement item in enumerable)
			{
				if (Settings.Default.Autostart)
				{
					Process.Start(item.Value);
					totalproc++;
				}
				else if (!Settings.Default.Autostart)
				{
					listView1.Items.Add(item.Value);
					totalproc++;
				}
			}
			proc_time = Convert.ToInt32(stopwatch.ElapsedMilliseconds);
		}
		catch (Exception ex)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	public void get_webs()
	{
		try
		{
			listView1.Items.Clear();
			XDocument xDocument = XDocument.Load(xmlFilePath);
			IEnumerable<XElement> enumerable = xDocument.Descendants("subproc").Descendants("process_path");
			Stopwatch stopwatch = Stopwatch.StartNew();
			foreach (XElement item in enumerable)
			{
				if (item.HasAttributes)
				{
					if (Settings.Default.Autostart)
					{
						Process.Start(item.Value, item.FirstAttribute.Value);
						totalweb++;
					}
					else if (!Settings.Default.Autostart)
					{
						webstw(item.Value, item.FirstAttribute.Value);
						totalweb++;
					}
				}
			}
			stopwatch.Stop();
			web_time = Convert.ToInt32(stopwatch.ElapsedMilliseconds);
		}
		catch (Exception ex)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	public void app_start()
	{
		runAppsToolStripMenuItem_Click(null, null);
	}

	private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
	{
		try
		{
			refresh();
		}
		catch
		{
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		listView1.View = View.Tile;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		listView1.View = View.LargeIcon;
	}

	private void listView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
	{
		if (listView1.SelectedItems[0].Tag == "chrome")
		{
			Process.Start("chrome.exe", listView1.SelectedItems[0].SubItems[0].Text);
		}
		else if (listView1.SelectedItems[0].Tag == "fire")
		{
			Process.Start("firefox.exe", listView1.SelectedItems[0].SubItems[0].Text);
		}
		else if (listView1.SelectedItems[0].Tag == "opera")
		{
			Process.Start("opera.exe", listView1.SelectedItems[0].SubItems[0].Text);
		}
		else
		{
			Process.Start(listView1.SelectedItems[0].SubItems[0].Text);
		}
	}

	private void textBox1_Click_1(object sender, EventArgs e)
	{
	}

	private void refresh()
	{
		listView1.Items.Clear();
		try
		{
			XDocument xDocument = XDocument.Load(xmlFilePath);
			IEnumerable<XElement> source = xDocument.Descendants("subproc").Descendants("process_path");
			foreach (XElement item in source.Distinct())
			{
				if (item.HasAttributes)
				{
					webstw(item.Value, item.FirstAttribute.Value);
				}
				else
				{
					listView1.Items.Add(item.Value);
				}
			}
		}
		catch (Exception ex)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	private void Encrypt(string inputFilePath, string outputfilePath)
	{
		string password = "MAKV2SPBNI99212";
		using Aes aes = Aes.Create();
		Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
		{
			73, 118, 97, 110, 32, 77, 101, 100, 118, 101,
			100, 101, 118
		});
		aes.Key = rfc2898DeriveBytes.GetBytes(32);
		aes.IV = rfc2898DeriveBytes.GetBytes(16);
		using FileStream stream = new FileStream(outputfilePath, FileMode.Create);
		using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);
		using FileStream fileStream = new FileStream(inputFilePath, FileMode.Open);
		int num;
		while ((num = fileStream.ReadByte()) != -1)
		{
			cryptoStream.WriteByte((byte)num);
		}
	}

	private void Decrypt(string inputFilePath, string outputfilePath)
	{
		string password = "MAKV2SPBNI99212";
		using Aes aes = Aes.Create();
		Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
		{
			73, 118, 97, 110, 32, 77, 101, 100, 118, 101,
			100, 101, 118
		});
		aes.Key = rfc2898DeriveBytes.GetBytes(32);
		aes.IV = rfc2898DeriveBytes.GetBytes(16);
		using FileStream stream = new FileStream(inputFilePath, FileMode.Open);
		using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read);
		using FileStream fileStream = new FileStream(outputfilePath, FileMode.Create);
		int num;
		while ((num = cryptoStream.ReadByte()) != -1)
		{
			fileStream.WriteByte((byte)num);
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		listView1.View = View.List;
	}

	private void button8_Click(object sender, EventArgs e)
	{
	}

	private void button5_Click_1(object sender, EventArgs e)
	{
		AboutBox1 aboutBox = new AboutBox1();
		aboutBox.ShowDialog(this);
	}

	private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
	{
	}

	private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
	{
	}

	private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
	{
	}

	private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
	{
	}

	private void button10_Click(object sender, EventArgs e)
	{
		devlog devlog2 = new devlog();
		devlog2.ShowDialog(this);
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
		if (textBox2.Text == "Devlog")
		{
			textBox2.Clear();
			devlog devlog2 = new devlog();
			devlog2.ShowDialog(this);
		}
	}

	private void button10_Click_1(object sender, EventArgs e)
	{
	}

	private void button11_Click(object sender, EventArgs e)
	{
	}

	private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
	{
	}

	private void timer3_Tick(object sender, EventArgs e)
	{
	}

	private void addProcessToolStripMenuItem_Click(object sender, EventArgs e)
	{
		settings settings2 = new settings();
		settings2.ShowDialog(this);
	}

	private void addWebLinkToolStripMenuItem_Click(object sender, EventArgs e)
	{
		web_form_add web_form_add2 = new web_form_add();
		web_form_add2.ShowDialog(this);
	}

	private void importLinksToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DialogResult dialogResult = openFileDialog1.ShowDialog();
	}

	private void exportLinksToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DialogResult dialogResult = saveFileDialog1.ShowDialog();
	}

	private void toolStripMenuItem2_Click(object sender, EventArgs e)
	{
	}

	private void actstas_Click(object sender, EventArgs e)
	{
		if (!Settings.Default.Autostart)
		{
			actstas.BackColor = Color.LawnGreen;
			Settings.Default.Autostart = true;
			actstas.Text = "Auto";
			runAppsToolStripMenuItem.Enabled = false;
			runAppsToolStripMenuItem.Visible = false;
		}
		else if (Settings.Default.Autostart)
		{
			actstas.BackColor = Color.Red;
			Settings.Default.Autostart = false;
			actstas.Text = "Manual";
			runAppsToolStripMenuItem.Enabled = true;
			runAppsToolStripMenuItem.Visible = true;
		}
	}

	private void runAppsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			listView1.Items.Clear();
			XDocument xDocument = XDocument.Load(xmlFilePath);
			IEnumerable<XElement> enumerable = xDocument.Descendants("subproc").Descendants("process_path");
			foreach (XElement item in enumerable)
			{
				if (item.HasAttributes)
				{
					Process.Start(item.Value, item.FirstAttribute.Value);
					webstw(item.Value, item.FirstAttribute.Value);
				}
				else
				{
					Process.Start(item.Value);
					listView1.Items.Add(item.Value);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	private void manageToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void button6_Click(object sender, EventArgs e)
	{
	}

	private void addSchduledProcessToolStripMenuItem_Click(object sender, EventArgs e)
	{
		reminders_form reminders_form2 = new reminders_form();
		reminders_form2.ShowDialog(this);
	}

	private void chart1_Click(object sender, EventArgs e)
	{
	}

	private void groupBox4_Enter(object sender, EventArgs e)
	{
	}

	private void button1_Click_1(object sender, EventArgs e)
	{
	}

	private void procxc_Click(object sender, EventArgs e)
	{
	}

	private void helpToolStripMenuItem_Click(object sender, EventArgs e)
	{
		helpProvider1.SetShowHelp(this, value: true);
	}

	private void groupBox5_Enter(object sender, EventArgs e)
	{
	}

	private void data_refresh_Tick(object sender, EventArgs e)
	{
	}

	private void listView1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void execution_auto()
	{
		if (!Directory.Exists("c:/EZ-5/users/" + Environment.UserName + "/dataxml"))
		{
			Directory.CreateDirectory("c:/EZ-5/users/" + Environment.UserName + "/dataxml");
			using (XmlWriter xmlWriter = XmlWriter.Create(xmlFilePath))
			{
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("process");
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndDocument();
			}
			fileSystemWatcher1.Path = monitorpath;
			notifyIcon1.ShowBalloonTip(1000, "Setup completed", "Files and settings loaded", ToolTipIcon.Info);
		}
		else if (File.Exists(xmlFilePath))
		{
			fileSystemWatcher1.Path = monitorpath;
			if (Settings.Default.Autostart)
			{
				app_start();
			}
		}
	}

	private void autostart_determination()
	{
		if (Settings.Default.Autostart)
		{
			actstas.BackColor = Color.LawnGreen;
			actstas.Text = "Auto";
			runAppsToolStripMenuItem.Enabled = false;
			runAppsToolStripMenuItem.Visible = false;
		}
		else
		{
			actstas.BackColor = Color.Crimson;
			actstas.Text = "Manual";
			runAppsToolStripMenuItem.Enabled = true;
			runAppsToolStripMenuItem.Visible = true;
		}
	}

	private void toolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Path_Tool path_Tool = new Path_Tool();
		path_Tool.ShowDialog(this);
	}

	private void resetToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("Reset deletes your list and settings. Continue ?", "Reset Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			try
			{
				listView1.Items.Clear();
				Directory.Delete("c:\\EZ-5", recursive: true);
				listView1.Items.Clear();
				menuStrip1.Enabled = false;
				notifyIcon1.ShowBalloonTip(1000, "Reset Successful", "please restart the application", ToolTipIcon.Info);
			}
			catch (Exception)
			{
				menuStrip1.Enabled = false;
				notifyIcon1.ShowBalloonTip(1000, "Reset Successful", "please restart the application", ToolTipIcon.Info);
			}
		}
	}

	private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
	{
	}

	private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
	{
	}

	private void addNewProfileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		prof prof2 = new prof();
		prof2.ShowDialog(this);
	}

	private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
	{
		AboutBox1 aboutBox = new AboutBox1();
		aboutBox.ShowDialog(this);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test_App.Form1));
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.setPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.onandoff = new System.Windows.Forms.ImageList(this.components);
		this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.listView1 = new System.Windows.Forms.ListView();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
		this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
		this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
		this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
		this.backgroundWorker5 = new System.ComponentModel.BackgroundWorker();
		this.backgroundWorker6 = new System.ComponentModel.BackgroundWorker();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.colorDialog1 = new System.Windows.Forms.ColorDialog();
		this.timer3 = new System.Windows.Forms.Timer(this.components);
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.addProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.addWebLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.importExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.importLinksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.exportLinksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.actstas = new System.Windows.Forms.ToolStripMenuItem();
		this.runAppsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.helpProvider1 = new System.Windows.Forms.HelpProvider();
		this.data_refresh = new System.Windows.Forms.Timer(this.components);
		this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.fileSystemWatcher1).BeginInit();
		this.panel2.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.menuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.setPathToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
		this.setPathToolStripMenuItem.Name = "setPathToolStripMenuItem";
		this.setPathToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
		this.setPathToolStripMenuItem.Text = "Set Path";
		this.timer1.Interval = 5000;
		this.saveFileDialog1.AddExtension = false;
		this.saveFileDialog1.Title = "Save your current list";
		this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(saveFileDialog1_FileOk);
		this.openFileDialog1.Filter = "List Files|*.dc";
		this.openFileDialog1.Title = "Select the list to Import";
		this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog1_FileOk);
		this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
		this.notifyIcon1.Icon = (System.Drawing.Icon)resources.GetObject("notifyIcon1.Icon");
		this.notifyIcon1.Text = "EZ-5 UI";
		this.notifyIcon1.Visible = true;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Explorer.png");
		this.imageList1.Images.SetKeyName(1, "pdf.png");
		this.imageList1.Images.SetKeyName(2, "log.png");
		this.imageList1.Images.SetKeyName(3, "txt.png");
		this.imageList1.Images.SetKeyName(4, "doc.png");
		this.imageList1.Images.SetKeyName(5, "xml.png");
		this.imageList1.Images.SetKeyName(6, "zip.png");
		this.imageList1.Images.SetKeyName(7, "docx.png");
		this.imageList1.Images.SetKeyName(8, "xlsx.png");
		this.imageList1.Images.SetKeyName(9, "7z.png");
		this.imageList1.Images.SetKeyName(10, "jar.png");
		this.imageList1.Images.SetKeyName(11, "pptx.png");
		this.imageList1.Images.SetKeyName(12, "lnk.png");
		this.imageList1.Images.SetKeyName(13, "chrome.png");
		this.imageList1.Images.SetKeyName(14, "firefox.png");
		this.imageList1.Images.SetKeyName(15, "opera.png");
		this.onandoff.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("onandoff.ImageStream");
		this.onandoff.TransparentColor = System.Drawing.Color.Transparent;
		this.onandoff.Images.SetKeyName(0, "on.png");
		this.onandoff.Images.SetKeyName(1, "off.png");
		this.fileSystemWatcher1.EnableRaisingEvents = true;
		this.fileSystemWatcher1.SynchronizingObject = this;
		this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(fileSystemWatcher1_Changed);
		this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(fileSystemWatcher1_Deleted);
		this.listView1.BackColor = System.Drawing.SystemColors.ControlLight;
		this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.columnHeader1 });
		this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listView1.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.listView1.FullRowSelect = true;
		this.listView1.GridLines = true;
		this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.listView1.Location = new System.Drawing.Point(3, 17);
		this.listView1.Name = "listView1";
		this.listView1.Size = new System.Drawing.Size(601, 210);
		this.listView1.TabIndex = 8;
		this.listView1.UseCompatibleStateImageBehavior = false;
		this.listView1.View = System.Windows.Forms.View.Details;
		this.listView1.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
		this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(listView1_MouseDoubleClick_1);
		this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.groupBox5);
		this.panel2.Location = new System.Drawing.Point(2, 36);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(865, 241);
		this.panel2.TabIndex = 21;
		this.groupBox2.Controls.Add(this.listBox1);
		this.groupBox2.Location = new System.Drawing.Point(3, 8);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(237, 230);
		this.groupBox2.TabIndex = 14;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Statistics";
		this.listBox1.FormattingEnabled = true;
		this.listBox1.Location = new System.Drawing.Point(7, 20);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(224, 199);
		this.listBox1.TabIndex = 0;
		this.groupBox5.Controls.Add(this.listView1);
		this.groupBox5.Location = new System.Drawing.Point(246, 8);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(607, 230);
		this.groupBox5.TabIndex = 13;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Paths/Links";
		this.groupBox5.Enter += new System.EventHandler(groupBox5_Enter);
		this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
		this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker2_DoWork);
		this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker3_DoWork);
		this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker4_DoWork);
		this.backgroundWorker6.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker6_DoWork);
		this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.textBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.textBox2.Location = new System.Drawing.Point(0, 283);
		this.textBox2.Name = "textBox2";
		this.textBox2.PasswordChar = '*';
		this.textBox2.Size = new System.Drawing.Size(867, 14);
		this.textBox2.TabIndex = 23;
		this.textBox2.TextChanged += new System.EventHandler(textBox2_TextChanged);
		this.timer3.Tick += new System.EventHandler(timer3_Tick);
		this.menuStrip1.Font = new System.Drawing.Font("Calibri", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.fileToolStripMenuItem, this.manageToolStripMenuItem, this.importExportToolStripMenuItem, this.aboutToolStripMenuItem, this.actstas, this.runAppsToolStripMenuItem });
		this.menuStrip1.Location = new System.Drawing.Point(0, 0);
		this.menuStrip1.Name = "menuStrip1";
		this.menuStrip1.Size = new System.Drawing.Size(867, 24);
		this.menuStrip1.TabIndex = 26;
		this.menuStrip1.Text = "menuStrip1";
		this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.addProcessToolStripMenuItem, this.addWebLinkToolStripMenuItem });
		this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
		this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
		this.fileToolStripMenuItem.Text = "Add";
		this.addProcessToolStripMenuItem.Name = "addProcessToolStripMenuItem";
		this.addProcessToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.A | System.Windows.Forms.Keys.Control;
		this.addProcessToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
		this.addProcessToolStripMenuItem.Text = "Add Process";
		this.addProcessToolStripMenuItem.Click += new System.EventHandler(addProcessToolStripMenuItem_Click);
		this.addWebLinkToolStripMenuItem.Name = "addWebLinkToolStripMenuItem";
		this.addWebLinkToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.W | System.Windows.Forms.Keys.Control;
		this.addWebLinkToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
		this.addWebLinkToolStripMenuItem.Text = "Add Web Link";
		this.addWebLinkToolStripMenuItem.Click += new System.EventHandler(addWebLinkToolStripMenuItem_Click);
		this.manageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.toolStripMenuItem1, this.resetToolStripMenuItem });
		this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
		this.manageToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
		this.manageToolStripMenuItem.Text = "Manage";
		this.manageToolStripMenuItem.Click += new System.EventHandler(manageToolStripMenuItem_Click);
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Q | System.Windows.Forms.Keys.Control;
		this.toolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
		this.toolStripMenuItem1.Text = "View Paths";
		this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
		this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
		this.resetToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.R | System.Windows.Forms.Keys.Control;
		this.resetToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
		this.resetToolStripMenuItem.Text = "Reset";
		this.resetToolStripMenuItem.Click += new System.EventHandler(resetToolStripMenuItem_Click);
		this.importExportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.importLinksToolStripMenuItem, this.exportLinksToolStripMenuItem });
		this.importExportToolStripMenuItem.Name = "importExportToolStripMenuItem";
		this.importExportToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
		this.importExportToolStripMenuItem.Text = "Import/Export";
		this.importLinksToolStripMenuItem.Name = "importLinksToolStripMenuItem";
		this.importLinksToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.I | System.Windows.Forms.Keys.Control;
		this.importLinksToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.importLinksToolStripMenuItem.Text = "Import Links";
		this.importLinksToolStripMenuItem.Click += new System.EventHandler(importLinksToolStripMenuItem_Click);
		this.exportLinksToolStripMenuItem.Name = "exportLinksToolStripMenuItem";
		this.exportLinksToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.E | System.Windows.Forms.Keys.Control;
		this.exportLinksToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.exportLinksToolStripMenuItem.Text = "Export Links";
		this.exportLinksToolStripMenuItem.Click += new System.EventHandler(exportLinksToolStripMenuItem_Click);
		this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
		this.aboutToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
		this.aboutToolStripMenuItem.Text = "About";
		this.aboutToolStripMenuItem.Click += new System.EventHandler(aboutToolStripMenuItem_Click);
		this.actstas.Name = "actstas";
		this.actstas.ShortcutKeys = System.Windows.Forms.Keys.M | System.Windows.Forms.Keys.Control;
		this.actstas.Size = new System.Drawing.Size(87, 20);
		this.actstas.Text = "Active Status";
		this.actstas.Click += new System.EventHandler(actstas_Click);
		this.runAppsToolStripMenuItem.Font = new System.Drawing.Font("Calibri", 9f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.runAppsToolStripMenuItem.Name = "runAppsToolStripMenuItem";
		this.runAppsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control;
		this.runAppsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
		this.runAppsToolStripMenuItem.Text = "Run Apps";
		this.runAppsToolStripMenuItem.Click += new System.EventHandler(runAppsToolStripMenuItem_Click);
		this.data_refresh.Interval = 10000;
		this.data_refresh.Tick += new System.EventHandler(data_refresh_Tick);
		this.columnHeader1.Text = "List";
		this.columnHeader1.Width = 590;
		this.AllowDrop = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(867, 297);
		base.Controls.Add(this.menuStrip1);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.panel2);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.HelpButton = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MainMenuStrip = this.menuStrip1;
		base.MaximizeBox = false;
		base.Name = "Form1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "EZ5";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form1_FormClosing);
		base.Load += new System.EventHandler(Form1_Load);
		base.DragDrop += new System.Windows.Forms.DragEventHandler(Form1_DragDrop);
		base.DragEnter += new System.Windows.Forms.DragEventHandler(Form1_DragEnter);
		base.DragLeave += new System.EventHandler(Form1_DragLeave);
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.fileSystemWatcher1).EndInit();
		this.panel2.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox5.ResumeLayout(false);
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
