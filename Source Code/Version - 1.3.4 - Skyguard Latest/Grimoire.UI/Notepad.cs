using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using DarkUI.Forms;
using Properties;

namespace Grimoire.UI;

public class Notepad : DarkForm
{
	private string font = ConfigurationManager.AppSettings.Get("Font");

	private float? fontSize = float.Parse(ConfigurationManager.AppSettings.Get("FontSize") ?? "8.25", CultureInfo.InvariantCulture.NumberFormat);

	public static Notepad Instance = new Notepad();

	private IContainer components;

	private RichTextBox richTextBox1;

	private Panel panel1;

	public Notepad()
	{
		InitializeComponent();
	}

	private void Notepad_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.FormOwnerClosing)
		{
			if (base.WindowState == FormWindowState.Minimized)
			{
				base.WindowState = FormWindowState.Normal;
			}
			e.Cancel = true;
			Hide();
		}
	}

	private void Notepad_Load(object sender, EventArgs e)
	{
		if (font != null && fontSize.HasValue)
		{
			Font = new Font(font, fontSize.Value, FontStyle.Regular, GraphicsUnit.Point, 0);
		}
		richTextBox1.ContextMenuStrip = Context();
	}

	private void Notepad_Shown(object sender, EventArgs e)
	{
		Root.SetDRM(Instance, !Root.Instance.enableCapture);
	}

	private ContextMenuStrip Context()
	{
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
		{
			Text = "Font"
		};
		toolStripMenuItem.Click += delegate
		{
			FontDialog fontDialog = new FontDialog();
			fontDialog.ShowDialog();
			if (richTextBox1.SelectedText == null)
			{
				richTextBox1.Font = fontDialog.Font;
			}
			else
			{
				richTextBox1.SelectionFont = fontDialog.Font;
			}
		};
		ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem
		{
			Text = "Color"
		};
		toolStripMenuItem2.Click += delegate
		{
			ColorDialog colorDialog = new ColorDialog();
			colorDialog.ShowDialog();
			if (richTextBox1.SelectedText == null)
			{
				richTextBox1.ForeColor = colorDialog.Color;
			}
			else
			{
				richTextBox1.SelectionColor = colorDialog.Color;
			}
		};
		contextMenuStrip.Items.Add(toolStripMenuItem);
		contextMenuStrip.Items.Add(toolStripMenuItem2);
		return contextMenuStrip;
	}

	private void Notepad_DragOver(object sender, DragEventArgs e)
	{
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
		{
			e.Effect = DragDropEffects.Link;
		}
		else
		{
			e.Effect = DragDropEffects.None;
		}
	}

	private void Notepad_DragDrop(object sender, DragEventArgs e)
	{
		if (!e.Data.GetDataPresent(DataFormats.FileDrop))
		{
			return;
		}
		string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
		string[] array2 = array;
		foreach (string path in array2)
		{
			if (File.Exists(path))
			{
				using TextReader textReader = new StreamReader(path);
				RichTextBox richTextBox = richTextBox1;
				richTextBox.Text = richTextBox.Text + "\r\n" + textReader.ReadToEnd() + "\r\n";
			}
		}
	}

	private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
	{
		if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.S)
		{
			string contents = richTextBox1.Text;
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				Filter = "txt files (*.txt)|*.txt|gbot files (*.gbot)|*.gbot|All files (*.*)|*.*",
				FilterIndex = 1,
				RestoreDirectory = true
			};
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				File.WriteAllText(saveFileDialog.FileName, contents);
			}
		}
		if (e.KeyCode == Keys.Back && richTextBox1.Text.Length < 1)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
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
		this.richTextBox1 = new System.Windows.Forms.RichTextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.richTextBox1.AllowDrop = true;
		this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.richTextBox1.ForeColor = System.Drawing.Color.Gainsboro;
		this.richTextBox1.Location = new System.Drawing.Point(0, 0);
		this.richTextBox1.Name = "richTextBox1";
		this.richTextBox1.Size = new System.Drawing.Size(480, 227);
		this.richTextBox1.TabIndex = 1;
		this.richTextBox1.Text = "";
		this.richTextBox1.DragDrop += new System.Windows.Forms.DragEventHandler(Notepad_DragDrop);
		this.richTextBox1.DragOver += new System.Windows.Forms.DragEventHandler(Notepad_DragOver);
		this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(richTextBox1_KeyDown);
		this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel1.Controls.Add(this.richTextBox1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(480, 227);
		this.panel1.TabIndex = 4;
		this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(Notepad_DragDrop);
		this.panel1.DragOver += new System.Windows.Forms.DragEventHandler(Notepad_DragOver);
		this.AllowDrop = true;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(480, 227);
		base.Controls.Add(this.panel1);
		base.Icon = Grimoire.Properties.Resources.GrimoireIcon;
		base.Name = "Notepad";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Notepad";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Notepad_FormClosing);
		base.Load += new System.EventHandler(Notepad_Load);
		base.Shown += new System.EventHandler(Notepad_Shown);
		base.DragDrop += new System.Windows.Forms.DragEventHandler(Notepad_DragDrop);
		base.DragOver += new System.Windows.Forms.DragEventHandler(Notepad_DragOver);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
