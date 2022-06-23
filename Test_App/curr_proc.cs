using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Test_App;

public class curr_proc : Form
{
	private string xmlFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\dataxml\\Data.XML";

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label2;

	private Label label1;

	private Button button1;

	private DataGridView dataGridView1;

	private DataGridViewTextBoxColumn Column1;

	private DataGridViewTextBoxColumn Column2;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem setPathToolStripMenuItem;

	public curr_proc()
	{
		InitializeComponent();
	}

	private void curr_proc_Load(object sender, EventArgs e)
	{
	}

	public void view_data()
	{
		try
		{
			XDocument xDocument = XDocument.Load(xmlFilePath);
			IEnumerable<XElement> enumerable = xDocument.Descendants("subproc").Descendants("process_path");
			foreach (XElement item in enumerable)
			{
				if (item.HasAttributes)
				{
					dataGridView1.Rows.Add(item.FirstAttribute.Value, item.Value);
				}
				else
				{
					dataGridView1.Rows.Add(item.Value);
				}
			}
			label1.Text = "Total paths : ";
			int num = dataGridView1.Rows.Count - 1;
			label2.Text = num.ToString();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		dataGridView1.ReadOnly = true;
		dataGridView1.Rows.Clear();
		dataGridView1.Refresh();
		Process[] processes = Process.GetProcesses();
		Process[] array = processes;
		foreach (Process process in array)
		{
			try
			{
				if (!string.IsNullOrEmpty(process.MainWindowTitle))
				{
					dataGridView1.Rows.Add(process.MainModule.FileName.ToString(), process.MainWindowTitle.ToString());
				}
			}
			catch
			{
			}
		}
		int num = dataGridView1.Rows.Count - 1;
		label2.Text = num.ToString();
	}

	private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
	{
	}

	private void setPathToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Clipboard.SetText(dataGridView1.SelectedCells[0].Value.ToString());
		XDocument xDocument = XDocument.Load(xmlFilePath);
		XElement xElement = xDocument.Element("process");
		xElement.Add(new XElement("subproc", new XElement("process_path", Clipboard.GetText())));
		xDocument.Save(xmlFilePath);
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.setPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.button1);
		this.groupBox1.Controls.Add(this.dataGridView1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(334, 415);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Processes";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(126, 384);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(0, 13);
		this.label2.TabIndex = 3;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 384);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(107, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Total Apps Running :";
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.button1.Location = new System.Drawing.Point(203, 377);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(119, 26);
		this.button1.TabIndex = 1;
		this.button1.Text = "Load / Refresh Tasks";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.Column1, this.Column2);
		this.dataGridView1.Location = new System.Drawing.Point(16, 30);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.Size = new System.Drawing.Size(300, 328);
		this.dataGridView1.TabIndex = 0;
		this.Column1.FillWeight = 98.47716f;
		this.Column1.HeaderText = "Path";
		this.Column1.Name = "Column1";
		this.Column2.FillWeight = 101.5228f;
		this.Column2.HeaderText = "Process";
		this.Column2.Name = "Column2";
		this.Column2.ReadOnly = true;
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.setPathToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
		this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(contextMenuStrip1_Opening);
		this.setPathToolStripMenuItem.Name = "setPathToolStripMenuItem";
		this.setPathToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
		this.setPathToolStripMenuItem.Text = "Set Path";
		this.setPathToolStripMenuItem.Click += new System.EventHandler(setPathToolStripMenuItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(358, 439);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "curr_proc";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Current running processes";
		base.Load += new System.EventHandler(curr_proc_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
