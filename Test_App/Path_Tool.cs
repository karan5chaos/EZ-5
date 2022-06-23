using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Test_App;

public class Path_Tool : Form
{
	private string xmlFilePath = "c:/EZ-5/users/" + Environment.UserName + "/dataxml/Data.XML";

	private IContainer components = null;

	private ContextMenuStrip webMenuStrip1;

	private ToolStripMenuItem web_delMenuItem;

	private ContextMenuStrip processMenuStrip2;

	private ToolStripMenuItem proc_delMenuItem1;

	private ListBox process_xml;

	private ListBox web_xml;

	private Button button2;

	private ToolTip toolTip1;

	private Button button4;

	private FileSystemWatcher fileSystemWatcher1;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private GroupBox groupBox1;

	private Label label6;

	private Label label5;

	private Label label2;

	private Label label1;

	private Button button6;

	private Timer timer1;

	private Label label3;

	private Label label4;

	private Button button5;

	private Button button1;

	private Button button3;

	public Path_Tool()
	{
		InitializeComponent();
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void button2_Click(object sender, EventArgs e)
	{
	}

	private void Path_Tool_Load(object sender, EventArgs e)
	{
		fileSystemWatcher1.Path = "c:/EZ-5/users/" + Environment.UserName + "/dataxml";
		view_data();
	}

	public void view_data()
	{
		web_xml.Items.Clear();
		process_xml.Items.Clear();
		int num = 0;
		int num2 = 0;
		try
		{
			XDocument xDocument = XDocument.Load(xmlFilePath);
			IEnumerable<XElement> enumerable = xDocument.Descendants("subproc").Descendants("process_path");
			foreach (XElement item in enumerable)
			{
				if (item.HasAttributes)
				{
					web_xml.Items.Add(item.FirstAttribute.Value);
					num2++;
				}
				else
				{
					process_xml.Items.Add(item.Value);
					num++;
				}
			}
			label5.Text = num.ToString();
			label6.Text = num2.ToString();
		}
		catch (Exception ex)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	private void process_xml_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			XElement xElement = XElement.Load(xmlFilePath);
			XElement xElement2 = (from a in xElement.Descendants("subproc")
				where a.Element("process_path").HasAttributes && a.Element("process_path").FirstAttribute.Value == web_xml.SelectedItem.ToString()
				select a).FirstOrDefault();
			xElement2.Remove();
			xElement.Save(xmlFilePath);
			label3.Text = web_xml.SelectedItem.ToString();
			web_xml.Items.Remove(web_xml.SelectedItem);
		}
		catch (NullReferenceException)
		{
			toolTip1.Show("Please select a path to delete", process_xml, 6);
		}
		catch (Exception ex2)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex2.Message, DateTime.Now.ToString(), ex2.StackTrace);
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
	}

	private void toolStripMenuItem1_Click(object sender, EventArgs e)
	{
		try
		{
			XElement xElement = XElement.Load(xmlFilePath);
			XElement xElement2 = (from a in xElement.Descendants("subproc")
				where a.Element("process_path").Value == process_xml.SelectedItem.ToString()
				select a).First();
			xElement2.Remove();
			xElement.Save(xmlFilePath);
			label3.Text = process_xml.SelectedItem.ToString();
			process_xml.Items.Remove(process_xml.SelectedItem);
		}
		catch (NullReferenceException)
		{
		}
		catch (Exception ex2)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex2.Message, DateTime.Now.ToString(), ex2.StackTrace);
		}
	}

	private void button2_Click_1(object sender, EventArgs e)
	{
		deleteToolStripMenuItem_Click(null, null);
	}

	private void button3_Click(object sender, EventArgs e)
	{
	}

	private void button4_Click(object sender, EventArgs e)
	{
		web_form_add web_form_add2 = new web_form_add();
		web_form_add2.ShowDialog(this);
	}

	private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
	{
		try
		{
			view_data();
		}
		catch (Exception)
		{
		}
	}

	private void tabPage1_Click(object sender, EventArgs e)
	{
	}

	private void label4_Click(object sender, EventArgs e)
	{
	}

	private void button5_Click(object sender, EventArgs e)
	{
	}

	private void button6_Click(object sender, EventArgs e)
	{
		try
		{
			XElement xElement = XElement.Load(xmlFilePath);
			XElement xElement2 = (from a in xElement.Descendants("subproc")
				where a.Element("process_path").HasAttributes
				select a).FirstOrDefault();
			xElement2.Remove();
			xElement.Save(xmlFilePath);
			label3.Text = web_xml.SelectedItem.ToString();
		}
		catch (Exception ex)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
	}

	private void button3_Click_1(object sender, EventArgs e)
	{
		settings settings2 = new settings();
		settings2.ShowDialog(this);
	}

	private void button1_Click_1(object sender, EventArgs e)
	{
		toolStripMenuItem1_Click(null, null);
	}

	private void button5_Click_1(object sender, EventArgs e)
	{
		try
		{
			XElement xElement = XElement.Load(xmlFilePath);
			XElement xElement2 = xElement.Descendants("subproc").FirstOrDefault();
			xElement2.Remove();
			xElement.Save(xmlFilePath);
			label3.Text = process_xml.SelectedItem.ToString();
		}
		catch (Exception ex)
		{
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test_App.Path_Tool));
		this.webMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.web_delMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.processMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.proc_delMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.process_xml = new System.Windows.Forms.ListBox();
		this.button4 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.web_xml = new System.Windows.Forms.ListBox();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.button6 = new System.Windows.Forms.Button();
		this.button5 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.webMenuStrip1.SuspendLayout();
		this.processMenuStrip2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.fileSystemWatcher1).BeginInit();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.webMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.web_delMenuItem });
		this.webMenuStrip1.Name = "contextMenuStrip1";
		this.webMenuStrip1.Size = new System.Drawing.Size(209, 26);
		this.web_delMenuItem.Name = "web_delMenuItem";
		this.web_delMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete | System.Windows.Forms.Keys.Alt;
		this.web_delMenuItem.Size = new System.Drawing.Size(208, 22);
		this.web_delMenuItem.Text = "Delete Web Path";
		this.web_delMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
		this.processMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.proc_delMenuItem1 });
		this.processMenuStrip2.Name = "contextMenuStrip1";
		this.processMenuStrip2.Size = new System.Drawing.Size(229, 26);
		this.proc_delMenuItem1.Name = "proc_delMenuItem1";
		this.proc_delMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Delete | System.Windows.Forms.Keys.Control;
		this.proc_delMenuItem1.Size = new System.Drawing.Size(228, 22);
		this.proc_delMenuItem1.Text = "Delete Process Path";
		this.proc_delMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
		this.process_xml.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.process_xml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.process_xml.ContextMenuStrip = this.processMenuStrip2;
		this.process_xml.FormattingEnabled = true;
		this.process_xml.HorizontalScrollbar = true;
		this.process_xml.Location = new System.Drawing.Point(3, 6);
		this.process_xml.Name = "process_xml";
		this.process_xml.Size = new System.Drawing.Size(417, 80);
		this.process_xml.TabIndex = 1;
		this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button4.Image = (System.Drawing.Image)resources.GetObject("button4.Image");
		this.button4.Location = new System.Drawing.Point(424, 6);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(30, 23);
		this.button4.TabIndex = 4;
		this.toolTip1.SetToolTip(this.button4, "Add New Web Adress");
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Image = (System.Drawing.Image)resources.GetObject("button2.Image");
		this.button2.Location = new System.Drawing.Point(424, 35);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(30, 23);
		this.button2.TabIndex = 3;
		this.toolTip1.SetToolTip(this.button2, "Deleted selected entry");
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click_1);
		this.web_xml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.web_xml.ContextMenuStrip = this.webMenuStrip1;
		this.web_xml.FormattingEnabled = true;
		this.web_xml.HorizontalScrollbar = true;
		this.web_xml.Location = new System.Drawing.Point(3, 6);
		this.web_xml.Name = "web_xml";
		this.web_xml.Size = new System.Drawing.Size(417, 80);
		this.web_xml.TabIndex = 1;
		this.button6.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.button6.BackColor = System.Drawing.Color.IndianRed;
		this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button6.Image = (System.Drawing.Image)resources.GetObject("button6.Image");
		this.button6.Location = new System.Drawing.Point(424, 64);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(30, 23);
		this.button6.TabIndex = 5;
		this.toolTip1.SetToolTip(this.button6, "Delete Sequentially");
		this.button6.UseVisualStyleBackColor = false;
		this.button6.Click += new System.EventHandler(button6_Click);
		this.button5.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.button5.BackColor = System.Drawing.Color.IndianRed;
		this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button5.Image = (System.Drawing.Image)resources.GetObject("button5.Image");
		this.button5.Location = new System.Drawing.Point(424, 64);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(30, 23);
		this.button5.TabIndex = 7;
		this.toolTip1.SetToolTip(this.button5, "Delete Sequentially");
		this.button5.UseVisualStyleBackColor = false;
		this.button5.Click += new System.EventHandler(button5_Click_1);
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Image = (System.Drawing.Image)resources.GetObject("button1.Image");
		this.button1.Location = new System.Drawing.Point(424, 35);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(30, 23);
		this.button1.TabIndex = 5;
		this.toolTip1.SetToolTip(this.button1, "Deleted selected entry");
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click_1);
		this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button3.Image = (System.Drawing.Image)resources.GetObject("button3.Image");
		this.button3.Location = new System.Drawing.Point(424, 6);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(30, 23);
		this.button3.TabIndex = 6;
		this.toolTip1.SetToolTip(this.button3, "Add New Process");
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click_1);
		this.fileSystemWatcher1.EnableRaisingEvents = true;
		this.fileSystemWatcher1.SynchronizingObject = this;
		this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(fileSystemWatcher1_Changed);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(2, 3);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(468, 121);
		this.tabControl1.TabIndex = 8;
		this.tabPage1.Controls.Add(this.button5);
		this.tabPage1.Controls.Add(this.button1);
		this.tabPage1.Controls.Add(this.button3);
		this.tabPage1.Controls.Add(this.process_xml);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(460, 95);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "System Paths";
		this.tabPage1.Click += new System.EventHandler(tabPage1_Click);
		this.tabPage2.Controls.Add(this.button6);
		this.tabPage2.Controls.Add(this.button2);
		this.tabPage2.Controls.Add(this.web_xml);
		this.tabPage2.Controls.Add(this.button4);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(460, 95);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Web Paths";
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(6, 130);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(460, 82);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Status";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(96, 56);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(13, 13);
		this.label3.TabIndex = 8;
		this.label3.Text = "--";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(14, 57);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(72, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Last Deleted :";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(412, 22);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(15, 13);
		this.label6.TabIndex = 5;
		this.label6.Text = "--";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(124, 22);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(15, 13);
		this.label5.TabIndex = 4;
		this.label5.Text = "--";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(302, 22);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(90, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Total Web Paths : ";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(14, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(100, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Total System Paths :";
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(474, 218);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.tabControl1);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Path_Tool";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Path Viewer";
		base.Load += new System.EventHandler(Path_Tool_Load);
		this.webMenuStrip1.ResumeLayout(false);
		this.processMenuStrip2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.fileSystemWatcher1).EndInit();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
