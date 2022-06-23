using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace Test_App;

public class Dev_console : Form
{
	private string xmlFilePath = "c:/EZ-5/users/" + Environment.UserName + "/log";

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox textBox1;

	private Button button1;

	private OpenFileDialog openFileDialog1;

	private TextBox textBox2;

	private GroupBox groupBox2;

	private Label label1;

	private ListBox listBox1;

	private Label label2;

	private Label label4;

	private Label label3;

	private Label label6;

	private Label label5;

	private Label label7;

	private Button button2;

	private Label label8;

	private FileSystemWatcher fileSystemWatcher1;

	private Label label10;

	private Label label9;

	private Label label11;

	private TextBox textBox3;

	private Label label12;

	public Dev_console()
	{
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		openFileDialog1.ShowDialog();
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

	private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
		try
		{
			textBox1.Text = openFileDialog1.FileName;
			string text = File.ReadAllText(openFileDialog1.FileName);
			textBox3.Text = text;
			Decrypt(openFileDialog1.FileName, openFileDialog1.FileName + ".tmp");
			string text2 = File.ReadAllText(openFileDialog1.FileName + ".tmp");
			textBox2.Text = text2;
			File.Delete(openFileDialog1.FileName + ".tmp");
		}
		catch (XmlException ex)
		{
			File.Delete(openFileDialog1.FileName + ".tmp");
			MessageBox.Show("Please select a valid list file", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
		catch (Exception ex2)
		{
			File.Delete(openFileDialog1.FileName + ".tmp");
			MessageBox.Show(ex2.Message.ToString());
			Err_log err_log2 = new Err_log();
			err_log2.writelog(ex2.Message, DateTime.Now.ToString(), ex2.StackTrace);
		}
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void Dev_console_Load(object sender, EventArgs e)
	{
		if (Directory.Exists(xmlFilePath))
		{
			fileSystemWatcher1.Path = xmlFilePath;
			listBox1.Items.Clear();
			string[] files = Directory.GetFiles(xmlFilePath, "*.log");
			string[] array = files;
			foreach (string item in array)
			{
				listBox1.Items.Add(item);
			}
			label2.Text = listBox1.Items.Count.ToString();
			label4.Text = Directory.GetLastAccessTime(xmlFilePath).ToString();
			label6.Text = Directory.GetLastWriteTime(xmlFilePath).ToString();
			label8.Text = Directory.GetAccessControl(xmlFilePath).ToString();
			label10.Text = Directory.GetParent(xmlFilePath).ToString();
		}
	}

	private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void button2_Click(object sender, EventArgs e)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(xmlFilePath);
		FileInfo[] files = directoryInfo.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			fileInfo.Delete();
		}
		listBox1.Items.Clear();
	}

	private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
	{
		label2.Text = listBox1.Items.Count.ToString();
		label4.Text = Directory.GetLastAccessTime(xmlFilePath).ToString();
		label6.Text = Directory.GetLastWriteTime(xmlFilePath).ToString();
	}

	private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
	{
		label2.Text = listBox1.Items.Count.ToString();
		label4.Text = Directory.GetLastAccessTime(xmlFilePath).ToString();
		label6.Text = Directory.GetLastWriteTime(xmlFilePath).ToString();
	}

	private void label11_Click(object sender, EventArgs e)
	{
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.button2 = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.label1 = new System.Windows.Forms.Label();
		this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.fileSystemWatcher1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.textBox3);
		this.groupBox1.Controls.Add(this.textBox2);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(352, 376);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Decryption tool";
		this.textBox2.BackColor = System.Drawing.Color.Black;
		this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox2.ForeColor = System.Drawing.Color.Gainsboro;
		this.textBox2.Location = new System.Drawing.Point(6, 223);
		this.textBox2.Multiline = true;
		this.textBox2.Name = "textBox2";
		this.textBox2.ReadOnly = true;
		this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.textBox2.Size = new System.Drawing.Size(340, 147);
		this.textBox2.TabIndex = 0;
		this.textBox1.Location = new System.Drawing.Point(12, 399);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(242, 20);
		this.textBox1.TabIndex = 1;
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.button1.Location = new System.Drawing.Point(272, 397);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(92, 23);
		this.button1.TabIndex = 2;
		this.button1.Text = "Choose a file";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.openFileDialog1.FileName = "openFileDialog1";
		this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog1_FileOk);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.button2);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.listBox1);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Location = new System.Drawing.Point(384, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(343, 376);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Log Status";
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(103, 282);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(13, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "--";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(29, 282);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(70, 13);
		this.label9.TabIndex = 11;
		this.label9.Text = "Root Parent :";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(103, 252);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(13, 13);
		this.label8.TabIndex = 10;
		this.label8.Text = "--";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(15, 252);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(87, 13);
		this.label7.TabIndex = 9;
		this.label7.Text = "Access Control : ";
		this.button2.Location = new System.Drawing.Point(19, 337);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(97, 23);
		this.button2.TabIndex = 8;
		this.button2.Text = "Clear all Logs";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(103, 223);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(13, 13);
		this.label6.TabIndex = 7;
		this.label6.Text = "--";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(2, 223);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(100, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Last Creation time : ";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(103, 194);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(13, 13);
		this.label4.TabIndex = 5;
		this.label4.Text = "--";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 194);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(96, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "Last Access time : ";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(154, 27);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(13, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "--";
		this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.listBox1.FormattingEnabled = true;
		this.listBox1.Location = new System.Drawing.Point(19, 57);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(303, 108);
		this.listBox1.TabIndex = 2;
		this.listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(16, 28);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(110, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Total Logs Generated";
		this.fileSystemWatcher1.EnableRaisingEvents = true;
		this.fileSystemWatcher1.SynchronizingObject = this;
		this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(fileSystemWatcher1_Created);
		this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(fileSystemWatcher1_Deleted);
		this.textBox3.BackColor = System.Drawing.Color.White;
		this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.textBox3.ForeColor = System.Drawing.Color.Black;
		this.textBox3.Location = new System.Drawing.Point(6, 43);
		this.textBox3.Multiline = true;
		this.textBox3.Name = "textBox3";
		this.textBox3.ReadOnly = true;
		this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.textBox3.Size = new System.Drawing.Size(340, 147);
		this.textBox3.TabIndex = 1;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(6, 27);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(81, 13);
		this.label11.TabIndex = 2;
		this.label11.Text = "Encrypted Data";
		this.label11.Click += new System.EventHandler(label11_Click);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(6, 207);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(82, 13);
		this.label12.TabIndex = 3;
		this.label12.Text = "Decrypted Data";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(752, 432);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "Dev_console";
		this.Text = "Dev_console";
		base.Load += new System.EventHandler(Dev_console_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.fileSystemWatcher1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
