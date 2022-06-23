using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Test_App;

public class reminders_form : Form
{
	private string xmlFilePath = "c:/EZ-5/Users/" + Environment.UserName + "/reminds/Reminds.XML";

	private string xmlFilePathdc = "c:/EZ-5/Users/" + Environment.UserName + "/reminds/Reminds.XML.dc";

	private IContainer components = null;

	private Timer timer1;

	private Button button1;

	private DateTimePicker dateTimePicker1;

	private Button button2;

	private DateTimePicker dateTimePicker2;

	private Label label1;

	private Timer timer2;

	public reminders_form()
	{
		InitializeComponent();
	}

	private void reminders_form_Load(object sender, EventArgs e)
	{
		if (!Directory.Exists("c:/EZ-5/users/" + Environment.UserName + "/reminds"))
		{
			Directory.CreateDirectory("c:/EZ-5/users/" + Environment.UserName + "/reminds");
			using XmlWriter xmlWriter = XmlWriter.Create(xmlFilePath);
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("Reminder");
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			return;
		}
		if (File.Exists(xmlFilePath))
		{
			timer2.Start();
			timer1.Start();
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		dateTimePicker1.Enabled = true;
		dateTimePicker2.Enabled = true;
	}

	private void addremid()
	{
		XDocument xDocument = XDocument.Load(xmlFilePath);
		XElement xElement = xDocument.Element("Reminder");
		xElement.Add(new XElement("remind", new XAttribute("date", dateTimePicker1.Text), new XAttribute("time", dateTimePicker2.Text)));
		xDocument.Save(xmlFilePath);
	}

	private void button2_Click(object sender, EventArgs e)
	{
		addremid();
	}

	private void getreminds()
	{
		XDocument xDocument = XDocument.Load(xmlFilePath);
		IEnumerable<XElement> enumerable = xDocument.Descendants("remind");
		List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
		foreach (XElement item in enumerable)
		{
			if (item.HasAttributes && !list.Contains(new KeyValuePair<string, string>(item.Attribute("time").Value, item.Attribute("date").Value)))
			{
				list.Add(new KeyValuePair<string, string>(item.Attribute("time").Value, item.Attribute("date").Value));
			}
		}
		foreach (KeyValuePair<string, string> item2 in list)
		{
			if (item2.Key == label1.Text)
			{
				MessageBox.Show("reminder :" + item2.Key + item2.Value);
			}
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		getreminds();
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
		label1.Text = DateTime.Now.ToString("HH:mm:ss");
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
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.button1 = new System.Windows.Forms.Button();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.button2 = new System.Windows.Forms.Button();
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.button1.Location = new System.Drawing.Point(12, 12);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 0;
		this.button1.Text = "Add New";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.dateTimePicker1.Enabled = false;
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dateTimePicker1.Location = new System.Drawing.Point(103, 15);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
		this.dateTimePicker1.TabIndex = 1;
		this.button2.Location = new System.Drawing.Point(420, 12);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(56, 23);
		this.button2.TabIndex = 4;
		this.button2.Text = "add";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.dateTimePicker2.Enabled = false;
		this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
		this.dateTimePicker2.Location = new System.Drawing.Point(309, 15);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.ShowUpDown = true;
		this.dateTimePicker2.Size = new System.Drawing.Size(96, 20);
		this.dateTimePicker2.TabIndex = 5;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(434, 90);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(35, 13);
		this.label1.TabIndex = 6;
		this.label1.Text = "label1";
		this.timer2.Tick += new System.EventHandler(timer2_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(658, 274);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.dateTimePicker2);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.dateTimePicker1);
		base.Controls.Add(this.button1);
		base.Name = "reminders_form";
		this.Text = "reminders_form";
		base.Load += new System.EventHandler(reminders_form_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
