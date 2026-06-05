using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Grimoire.Botting;
using Newtonsoft.Json;

namespace Grimoire.UI;

public class NodeEditor : Form
{
	public class TypedCommand
	{
		public string Name;

		public Type CommandType;

		public override string ToString()
		{
			return Name;
		}
	}

	public Type lb_item;

	public static NodeEditor Instance = new NodeEditor();

	private IContainer components;

	private SplitContainer splitContainer1;

	private ListBox listBox1;

	private Panel panel1;

	public NodeEditor()
	{
		InitializeComponent();
	}

	private void listBox1_MouseDown(object sender, MouseEventArgs e)
	{
		lb_item = null;
		if (listBox1.Items.Count != 0)
		{
			int index = listBox1.IndexFromPoint(e.X, e.Y);
			TypedCommand typedCommand = listBox1.Items[index] as TypedCommand;
			DragDropEffects dragDropEffects = DoDragDrop(typedCommand.CommandType, DragDropEffects.All);
		}
	}

	private void listBox1_DragLeave(object sender, EventArgs e)
	{
		MessageBox.Show(JsonConvert.SerializeObject(listBox1.SelectedItem));
	}

	private void Form1_DragDrop(object sender, DragEventArgs e)
	{
		lb_item = null;
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		IEnumerable<Type> enumerable = from p in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly s) => s.GetTypes())
			where typeof(IBotCommand).IsAssignableFrom(p) && p.GetProperties().Length != 0
			select p;
		foreach (Type item in enumerable)
		{
			TypedCommand typedCommand = new TypedCommand();
			typedCommand.Name = item.Name;
			typedCommand.CommandType = item;
			listBox1.Items.Add(typedCommand);
		}
	}

	private void splitContainer1_Panel2_DragDrop(object sender, DragEventArgs e)
	{
		if (lb_item == null)
		{
			return;
		}
		CommandNode commandNode = new CommandNode();
		Type type = lb_item;
		PropertyInfo[] properties = type.GetProperties();
		List<PropertyInfo> list = properties.ToList();
		int num = 13;
		foreach (PropertyInfo item in list)
		{
			Label label = new Label();
			label.Location = new Point(13, num);
			label.Text = item.Name;
			TextBox textBox = new TextBox();
			textBox.Location = new Point(73, num);
			textBox.Text = "text for " + item.Name + " here";
			num += 30;
			commandNode.Controls.Add(textBox);
			commandNode.Controls.Add(label);
		}
		num += 13;
		commandNode.Size = new Size(200, num);
		commandNode.activeControl = panel1;
		commandNode.previousPosition = new Point(e.X, e.Y);
		panel1.Controls.Add(commandNode);
		lb_item = null;
	}

	private void splitContainer1_Panel2_DragEnter(object sender, DragEventArgs e)
	{
		e.Effect = DragDropEffects.All;
		lb_item = ((TypedCommand)listBox1.SelectedItem).CommandType;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Grimoire.UI.NodeEditor));
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.panel1 = new System.Windows.Forms.Panel();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.listBox1);
		this.splitContainer1.Panel2.Controls.Add(this.panel1);
		this.splitContainer1.Panel2.DragDrop += new System.Windows.Forms.DragEventHandler(splitContainer1_Panel2_DragDrop);
		this.splitContainer1.Panel2.DragEnter += new System.Windows.Forms.DragEventHandler(splitContainer1_Panel2_DragEnter);
		this.splitContainer1.Size = new System.Drawing.Size(800, 450);
		this.splitContainer1.SplitterDistance = 209;
		this.splitContainer1.TabIndex = 0;
		this.listBox1.DisplayMember = "Name";
		this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listBox1.FormattingEnabled = true;
		this.listBox1.Location = new System.Drawing.Point(0, 0);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(209, 450);
		this.listBox1.TabIndex = 0;
		this.listBox1.DragLeave += new System.EventHandler(listBox1_DragLeave);
		this.listBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(listBox1_MouseDown);
		this.panel1.AllowDrop = true;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(587, 450);
		this.panel1.TabIndex = 0;
		this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(splitContainer1_Panel2_DragDrop);
		this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(splitContainer1_Panel2_DragEnter);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 450);
		base.Controls.Add(this.splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "NodeEditor";
		this.Text = "Command Node Editor";
		base.Load += new System.EventHandler(Form1_Load);
		base.DragDrop += new System.Windows.Forms.DragEventHandler(Form1_DragDrop);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
