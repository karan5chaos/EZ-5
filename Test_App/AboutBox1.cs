using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Test_App;

internal class AboutBox1 : Form
{
	private IContainer components = null;

	private Label label1;

	private Label label2;

	private TextBox textBox1;

	public string AssemblyTitle
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), inherit: false);
			if (customAttributes.Length != 0)
			{
				AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
				if (assemblyTitleAttribute.Title != "")
				{
					return assemblyTitleAttribute.Title;
				}
			}
			return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
		}
	}

	public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

	public string AssemblyDescription
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
		}
	}

	public string AssemblyProduct
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyProductAttribute)customAttributes[0]).Product;
		}
	}

	public string AssemblyCopyright
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
		}
	}

	public string AssemblyCompany
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCompanyAttribute)customAttributes[0]).Company;
		}
	}

	public AboutBox1()
	{
		InitializeComponent();
		Text = $"About EZ-5";
	}

	private void AboutBox1_Load(object sender, EventArgs e)
	{
		label1.Focus();
		textBox1.SelectionLength = 0;
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test_App.AboutBox1));
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(7, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(62, 25);
		this.label1.TabIndex = 0;
		this.label1.Text = "EZ-5";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(9, 34);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(95, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Version : 3.0 (Final)";
		this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.textBox1.Location = new System.Drawing.Point(12, 59);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(381, 159);
		this.textBox1.TabIndex = 2;
		this.textBox1.TabStop = false;
		this.textBox1.Text = resources.GetString("textBox1.Text");
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(403, 227);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "AboutBox1";
		base.Padding = new System.Windows.Forms.Padding(9);
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "About";
		base.Load += new System.EventHandler(AboutBox1_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
