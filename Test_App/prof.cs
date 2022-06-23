using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Test_App;

public class prof : Form
{
	private string xmlFilePath = "c:/EZ-5/Users/" + Environment.UserName + "/dataxml";

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button button1;

	private TextBox textBox1;

	private Label label1;

	public prof()
	{
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (!Directory.Exists(xmlFilePath + "/" + textBox1.Text))
		{
			Directory.CreateDirectory(xmlFilePath + "/" + textBox1.Text);
		}
		else
		{
			File.Create(xmlFilePath + "/Data.XML");
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.button1);
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(279, 57);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Add Profile";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 28);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Name :";
		this.textBox1.Location = new System.Drawing.Point(53, 25);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(147, 20);
		this.textBox1.TabIndex = 1;
		this.button1.Location = new System.Drawing.Point(206, 23);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(64, 23);
		this.button1.TabIndex = 2;
		this.button1.Text = "Add";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(302, 83);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "prof";
		this.Text = "Profile";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
