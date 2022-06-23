using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Test_App;

public class devlog : Form
{
	private IContainer components = null;

	private TextBox textBox1;

	private Label label1;

	private GroupBox groupBox1;

	private TextBox textBox2;

	private Label label2;

	public devlog()
	{
		InitializeComponent();
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
		if (textBox1.Text == "piprani" && textBox2.Text == "dev$login")
		{
			Dev_console dev_console = new Dev_console();
			dev_console.ShowDialog(this);
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
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.textBox1.Location = new System.Drawing.Point(136, 24);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(179, 20);
		this.textBox1.TabIndex = 0;
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(30, 27);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(89, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Enter Username :";
		this.groupBox1.Controls.Add(this.textBox2);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(336, 96);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "LogIn";
		this.textBox2.Location = new System.Drawing.Point(136, 58);
		this.textBox2.Name = "textBox2";
		this.textBox2.PasswordChar = '*';
		this.textBox2.Size = new System.Drawing.Size(179, 20);
		this.textBox2.TabIndex = 2;
		this.textBox2.TextChanged += new System.EventHandler(textBox2_TextChanged);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(9, 61);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(110, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "Enter dev_password :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(364, 128);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "devlog";
		this.Text = "devlog";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
