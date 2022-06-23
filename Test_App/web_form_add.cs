using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Test_App;

public class web_form_add : Form
{
	private string xmlFilePath = "c:/EZ-5/users/" + Environment.UserName + "/dataxml/Data.XML";

	private IContainer components = null;

	private Button button1;

	private TextBox textBox1;

	private Label label2;

	private GroupBox groupBox2;

	private ListBox listBox1;

	private OpenFileDialog openFileDialog1;

	private SaveFileDialog saveFileDialog1;

	private GroupBox groupBox3;

	private ToolTip toolTip1;

	private ErrorProvider errorProvider1;

	private HelpProvider helpProvider1;

	private GroupBox groupBox1;

	private Button button4;

	private Button button3;

	private Button button2;

	private RadioButton opbuttn;

	private RadioButton ffxbuttn;

	private RadioButton chromebuttn;

	public web_form_add()
	{
		InitializeComponent();
	}

	private void web_form_add_Load(object sender, EventArgs e)
	{
		helpProvider1.SetHelpString(textBox1, "Enter your Name ");
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (textBox1.Text != null && textBox1.Text != "")
		{
			if (chromebuttn.Checked)
			{
				addwebpath("Chrome.exe", textBox1.Text);
			}
			else if (ffxbuttn.Checked)
			{
				addwebpath("firefox.exe", textBox1.Text);
			}
			else if (opbuttn.Checked)
			{
				addwebpath("opera.exe", textBox1.Text);
			}
		}
		else
		{
			if (textBox1.Text != null && textBox1.Text != "")
			{
				errorProvider1.Clear();
				errorProvider1.SetError(textBox1, "URL Field Left Blank");
			}
			errorProvider1.Clear();
			errorProvider1.SetError(textBox1, "Invalid URL");
		}
	}

	public void UrlIsValid(string url)
	{
	}

	private void addwebpath(string web_browser, string url)
	{
		XDocument xDocument = XDocument.Load(xmlFilePath);
		XElement xElement = xDocument.Element("process");
		xElement.Add(new XElement("subproc", new XElement("process_path", web_browser, new XAttribute("link", url))));
		xDocument.Save(xmlFilePath);
		listBox1.Items.Add(web_browser + "- " + url);
	}

	private void importWebLinksListToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DialogResult dialogResult = openFileDialog1.ShowDialog();
	}

	private void exportWebLinksListToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DialogResult dialogResult = saveFileDialog1.ShowDialog();
	}

	private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
	}

	private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void button2_Click(object sender, EventArgs e)
	{
		chromebuttn.Checked = true;
	}

	private void button3_Click(object sender, EventArgs e)
	{
		ffxbuttn.Checked = true;
	}

	private void button4_Click(object sender, EventArgs e)
	{
		opbuttn.Checked = true;
	}

	private void button5_Click(object sender, EventArgs e)
	{
	}

	private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
	{
	}

	private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test_App.web_form_add));
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.helpProvider1 = new System.Windows.Forms.HelpProvider();
		this.chromebuttn = new System.Windows.Forms.RadioButton();
		this.ffxbuttn = new System.Windows.Forms.RadioButton();
		this.opbuttn = new System.Windows.Forms.RadioButton();
		this.button2 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
		this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.textBox1.Location = new System.Drawing.Point(108, 27);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(154, 21);
		this.textBox1.TabIndex = 0;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Location = new System.Drawing.Point(268, 25);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 1;
		this.button1.Text = "Add";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(25, 30);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(77, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Enter Address :";
		this.groupBox2.Controls.Add(this.listBox1);
		this.groupBox2.Location = new System.Drawing.Point(379, 11);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(205, 177);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Last Added";
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.listBox1.FormattingEnabled = true;
		this.listBox1.Location = new System.Drawing.Point(7, 20);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(192, 145);
		this.listBox1.TabIndex = 0;
		this.listBox1.TabStop = false;
		this.openFileDialog1.FileName = "openFileDialog1";
		this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog1_FileOk);
		this.saveFileDialog1.DefaultExt = "XML";
		this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(saveFileDialog1_FileOk);
		this.groupBox3.Controls.Add(this.textBox1);
		this.groupBox3.Controls.Add(this.button1);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Location = new System.Drawing.Point(12, 125);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(361, 63);
		this.groupBox3.TabIndex = 4;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Enter Web URL";
		this.errorProvider1.ContainerControl = this;
		this.chromebuttn.AutoSize = true;
		this.chromebuttn.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Highlight;
		this.chromebuttn.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.chromebuttn.Location = new System.Drawing.Point(6, 72);
		this.chromebuttn.Name = "chromebuttn";
		this.chromebuttn.Size = new System.Drawing.Size(96, 17);
		this.chromebuttn.TabIndex = 4;
		this.chromebuttn.Text = "Google Chrome";
		this.chromebuttn.UseVisualStyleBackColor = true;
		this.ffxbuttn.AutoSize = true;
		this.ffxbuttn.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Highlight;
		this.ffxbuttn.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.ffxbuttn.Location = new System.Drawing.Point(145, 72);
		this.ffxbuttn.Name = "ffxbuttn";
		this.ffxbuttn.Size = new System.Drawing.Size(92, 17);
		this.ffxbuttn.TabIndex = 5;
		this.ffxbuttn.Text = "Mozilla Firefox";
		this.ffxbuttn.UseVisualStyleBackColor = true;
		this.opbuttn.AutoSize = true;
		this.opbuttn.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.opbuttn.Location = new System.Drawing.Point(295, 72);
		this.opbuttn.Name = "opbuttn";
		this.opbuttn.Size = new System.Drawing.Size(53, 17);
		this.opbuttn.TabIndex = 6;
		this.opbuttn.Text = "Opera";
		this.opbuttn.UseVisualStyleBackColor = true;
		this.button2.BackgroundImage = (System.Drawing.Image)resources.GetObject("button2.BackgroundImage");
		this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
		this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Location = new System.Drawing.Point(28, 19);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(48, 47);
		this.button2.TabIndex = 7;
		this.button2.TabStop = false;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button3.BackgroundImage = (System.Drawing.Image)resources.GetObject("button3.BackgroundImage");
		this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
		this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
		this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button3.Location = new System.Drawing.Point(164, 19);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(48, 47);
		this.button3.TabIndex = 8;
		this.button3.TabStop = false;
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.button4.BackgroundImage = (System.Drawing.Image)resources.GetObject("button4.BackgroundImage");
		this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
		this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
		this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button4.Location = new System.Drawing.Point(295, 19);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(48, 47);
		this.button4.TabIndex = 9;
		this.button4.TabStop = false;
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.groupBox1.Controls.Add(this.button4);
		this.groupBox1.Controls.Add(this.button3);
		this.groupBox1.Controls.Add(this.button2);
		this.groupBox1.Controls.Add(this.opbuttn);
		this.groupBox1.Controls.Add(this.ffxbuttn);
		this.groupBox1.Controls.Add(this.chromebuttn);
		this.groupBox1.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(12, 11);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(361, 108);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Select Web Browser";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(592, 198);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "web_form_add";
		this.helpProvider1.SetShowHelp(this, true);
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Add Web Path";
		base.Load += new System.EventHandler(web_form_add_Load);
		this.groupBox2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
