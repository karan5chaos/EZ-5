using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Test_App;

public class settings : Form
{
	private string xmlFilePath = "c:/EZ-5/Users/" + Environment.UserName + "/dataxml/Data.XML";

	private string xmlFilePathendc = "c:/EZ-5/Users/" + Environment.UserName + "/dataxml/Data.XML.dc";

	private IContainer components = null;

	private TextBox textBox1;

	private Label label1;

	private Button button2;

	private ErrorProvider errorProvider1;

	private GroupBox groupBox1;

	private ListBox listBox1;

	private Button button1;

	private OpenFileDialog openFileDialog1;

	public settings()
	{
		InitializeComponent();
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void button2_Click(object sender, EventArgs e)
	{
		try
		{
			if (textBox1.Text != "" && textBox1.Text != null && (!textBox1.Text.Contains(".bat") || !textBox1.Text.Contains(".BAT")) && (Directory.Exists(textBox1.Text) || File.Exists(textBox1.Text)))
			{
				XDocument xDocument = XDocument.Load(xmlFilePath);
				XElement xElement = xDocument.Element("process");
				xElement.Add(new XElement("subproc", new XElement("process_path", textBox1.Text)));
				xDocument.Save(xmlFilePath);
				listBox1.Items.Add(textBox1.Text);
				return;
			}
			if (textBox1.Text.Contains(".BAT") || !textBox1.Text.Contains(".bat"))
			{
				errorProvider1.Clear();
				errorProvider1.SetError(textBox1, ".bat files are not allowed.");
			}
			errorProvider1.Clear();
			errorProvider1.SetError(textBox1, "Invalid File Path Entered.");
		}
		catch (Exception ex)
		{
			errorProvider1.Clear();
			errorProvider1.SetError(textBox1, ex.Message);
			Err_log err_log = new Err_log();
			err_log.writelog(ex.Message, DateTime.Now.ToString(), ex.StackTrace);
		}
	}

	private void textBox1_Enter(object sender, EventArgs e)
	{
		errorProvider1.Clear();
	}

	private void settings_Load(object sender, EventArgs e)
	{
	}

	private void Encrypt(string inputFilePath, string outputfilePath)
	{
		string password = "MAKV2SPBNI99212";
		using (Aes aes = Aes.Create())
		{
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
		File.Delete(inputFilePath);
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
		using (FileStream stream = new FileStream(inputFilePath, FileMode.Open))
		{
			using CryptoStream cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read);
			using FileStream fileStream = new FileStream(outputfilePath, FileMode.Create);
			int num;
			while ((num = cryptoStream.ReadByte()) != -1)
			{
				fileStream.WriteByte((byte)num);
			}
		}
		File.Delete(inputFilePath);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
		textBox1.Text = openFileDialog1.FileName;
	}

	private void button1_Click_1(object sender, EventArgs e)
	{
		openFileDialog1.ShowDialog();
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
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.button2 = new System.Windows.Forms.Button();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.button1 = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
		this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.textBox1.Location = new System.Drawing.Point(74, 10);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(391, 21);
		this.textBox1.TabIndex = 0;
		this.textBox1.Enter += new System.EventHandler(textBox1_Enter);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 14);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(54, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Add Path :";
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Location = new System.Drawing.Point(544, 9);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 3;
		this.button2.Text = "Save";
		this.button2.UseVisualStyleBackColor = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.errorProvider1.ContainerControl = this;
		this.button1.Location = new System.Drawing.Point(496, 9);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(42, 23);
		this.button1.TabIndex = 4;
		this.button1.Text = "...";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click_1);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.listBox1);
		this.groupBox1.Location = new System.Drawing.Point(12, 38);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(607, 117);
		this.groupBox1.TabIndex = 5;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Last Added";
		this.listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.listBox1.FormattingEnabled = true;
		this.listBox1.Location = new System.Drawing.Point(6, 19);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(595, 95);
		this.listBox1.TabIndex = 0;
		this.openFileDialog1.FileName = "openFileDialog1";
		this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog1_FileOk);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(631, 164);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.textBox1);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MinimizeBox = false;
		base.Name = "settings";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Path Edit";
		base.Load += new System.EventHandler(settings_Load);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
