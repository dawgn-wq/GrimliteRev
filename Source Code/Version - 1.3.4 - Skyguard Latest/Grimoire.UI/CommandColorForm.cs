using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Properties;

namespace Grimoire.UI;

public class CommandColorForm : DarkForm
{
	public static CommandColorForm Instance = new CommandColorForm();

	private IContainer components;

	private ColorDialog colorDialog1;

	private TrackBar trackBar1;

	private DarkButton btnReloadColors;

	private DarkComboBox comboBox1;

	private DarkButton btnSetColor;

	private DarkCheckBox checkBox1;

	private DarkButton btnSave;

	private DarkButton btnRandomColors;

	private DarkButton btnRefresh;

	private DarkTextBox txtRGB;

	public CommandColorForm()
	{
		InitializeComponent();
	}

	private void btnLabelColor_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			string key = comboBox1.SelectedItem.ToString().Replace("Cmd", "") + "Color";
			Config.Instance.SetValue(key, colorDialog1.Color.ToArgb().ToString("X"));
			Dictionary<string, Color> currentColors = BotManager.Instance.CurrentColors;
			if (currentColors.ContainsKey(key))
			{
				currentColors[key] = colorDialog1.Color;
			}
		}
	}

	private void CommandColorForm_Load(object sender, EventArgs e)
	{
		Type type = typeof(IBotCommand);
		IEnumerable<Type> enumerable = from p in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly s) => s.GetTypes())
			where type.IsAssignableFrom(p) && !p.IsInterface
			select p;
		Type[] array = (enumerable as Type[]) ?? enumerable.ToArray();
		comboBox1.Items.Clear();
		comboBox1.Items.Add("Index");
		comboBox1.Items.Add("Variable");
		comboBox1.Items.Add("ExtendedVariable");
		Type[] array2 = array;
		foreach (Type type2 in array2)
		{
			string[] array3 = type2.ToString().Split('.');
			comboBox1.Items.Add(array3[array3.Count() - 1]);
		}
		string value = Config.Instance.GetValue<string>("font");
		if (value != null)
		{
			Font = new Font(value, 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
		}
		trackBar1.Value = int.Parse(Config.Instance.GetValue<string>("lstCommandsFontSize") ?? "60");
	}

	private void CommandColorForm_Shown(object sender, EventArgs e)
	{
		Root.SetDRM(Instance, !Root.Instance.enableCapture);
	}

	private int GetColor(Control ctr)
	{
		string key = ctr.Name.ToString().Remove(0, 3);
		string text = SystemColors.WindowText.ToArgb().ToString("X");
		try
		{
			return int.Parse(Config.Instance.GetValue<string>(key) ?? text, NumberStyles.HexNumber);
		}
		catch
		{
		}
		return int.Parse(SystemColors.WindowText.ToString());
	}

	private int GetColor(string ctr)
	{
		string text = SystemColors.WindowText.ToArgb().ToString("X");
		try
		{
			return int.Parse(Config.Instance.GetValue<string>(ctr) ?? text, NumberStyles.HexNumber);
		}
		catch
		{
		}
		return int.Parse(SystemColors.WindowText.ToString());
	}

	private bool GetChecked(CheckBox ctr)
	{
		string text = ctr.Name.ToString().Remove(0, 3);
		return bool.Parse(Config.Instance.GetValue<string>(text + "Centered") ?? "false");
	}

	private bool GetChecked(string ctr)
	{
		return bool.Parse(Config.Instance.GetValue<string>(ctr + "Centered") ?? "false");
	}

	private void CommandColorForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing)
		{
			e.Cancel = true;
			Hide();
		}
	}

	private void chkLabelCentered_CheckedChanged(object sender, EventArgs e)
	{
		string key = "";
		switch (((CheckBox)sender).Name.Replace("chk", ""))
		{
		case "Label":
			key = "LabelCentered";
			break;
		case "Kill":
			key = "KillCentered";
			break;
		case "Index":
			key = "IndexCentered";
			break;
		}
		Config.Instance.SetValue(key, ((CheckBox)sender).Checked.ToString());
	}

	private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
	{
		if (e.Index <= -1)
		{
			return;
		}
		int ındex = e.Index;
		try
		{
			e.DrawBackground();
			SolidBrush brush = new SolidBrush(BotManager.Instance.GetCurrentColor(comboBox1.Items[ındex].ToString().Replace("Cmd", "") + "Color"));
			e.Graphics.DrawString(comboBox1.Items[ındex].ToString(), Font, brush, e.Bounds);
		}
		catch
		{
		}
	}

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			checkBox1.Checked = GetChecked(comboBox1.Items[comboBox1.SelectedIndex].ToString().Replace("Cmd", ""));
		}
		catch
		{
		}
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		string key = comboBox1.SelectedItem.ToString().Replace("Cmd", "") + "Centered";
		Config.Instance.SetValue(key, ((CheckBox)sender).Checked.ToString());
		Dictionary<string, bool> currentCentered = BotManager.Instance.CurrentCentered;
		if (currentCentered.ContainsKey(key))
		{
			currentCentered[key] = ((CheckBox)sender).Checked;
		}
	}

	private void trackBar1_Scroll(object sender, EventArgs e)
	{
		try
		{
			BotManager.Instance.lstCommands.ItemHeight = trackBar1.Value / 4;
			BotManager.Instance.lstCommands.Font = new Font(BotManager.Instance.lstCommands.Font.FontFamily, (float)BotManager.Instance.lstCommands.ItemHeight - 6.5f, FontStyle.Regular);
		}
		catch
		{
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Config.Instance.SetValue("CommandsSize", trackBar1.Value.ToString());
	}

	private void btnRandomColors_Click(object sender, EventArgs e)
	{
		Type type = typeof(IBotCommand);
		IEnumerable<Type> enumerable = from p in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly s) => s.GetTypes())
			where type.IsAssignableFrom(p) && !p.IsInterface
			select p;
		Type[] array = (enumerable as Type[]) ?? enumerable.ToArray();
		comboBox1.Items.Clear();
		Random random = new Random();
		int result = 255;
		int result2 = 255;
		int result3 = 255;
		if (string.IsNullOrEmpty(txtRGB.Text))
		{
			txtRGB.Text = "255, 255, 255";
		}
		try
		{
			if (txtRGB.Text.Contains("#"))
			{
				Color color = ColorTranslator.FromHtml(txtRGB.Text);
				result = Convert.ToInt16(color.R);
				result2 = Convert.ToInt16(color.G);
				result3 = Convert.ToInt16(color.B);
			}
			else if (txtRGB.Text.Contains(","))
			{
				try
				{
					string[] array2 = txtRGB.Text.Split(',');
					int.TryParse(array2[0], out result);
					int.TryParse(array2[1], out result2);
					int.TryParse(array2[2], out result3);
				}
				catch
				{
				}
			}
		}
		catch (Exception)
		{
		}
		Type[] array3 = array;
		foreach (Type type2 in array3)
		{
			string[] array4 = type2.ToString().Split('.');
			string text = array4[array4.Count() - 1];
			string key = text.Replace("Cmd", "") + "Color";
			Config.Instance.SetValue(key, Extensions.generatePastelHex(random, result, result2, result3));
		}
	}

	private void btnRefresh_Click(object sender, EventArgs e)
	{
		Type type = typeof(IBotCommand);
		IEnumerable<Type> enumerable = from p in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly s) => s.GetTypes())
			where type.IsAssignableFrom(p) && !p.IsInterface
			select p;
		Type[] array = (enumerable as Type[]) ?? enumerable.ToArray();
		comboBox1.Items.Clear();
		comboBox1.Items.Add("Index");
		comboBox1.Items.Add("Variable");
		comboBox1.Items.Add("ExtendedVariable");
		Type[] array2 = array;
		foreach (Type type2 in array2)
		{
			string[] array3 = type2.ToString().Split('.');
			comboBox1.Items.Add(array3[array3.Count() - 1]);
		}
	}

	private void btnReloadColors_Click(object sender, EventArgs e)
	{
		BotManager.Instance.CurrentCentered.Clear();
		BotManager.Instance.CurrentColors.Clear();
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
		this.colorDialog1 = new System.Windows.Forms.ColorDialog();
		this.comboBox1 = new DarkUI.Controls.DarkComboBox();
		this.btnSetColor = new DarkUI.Controls.DarkButton();
		this.checkBox1 = new DarkUI.Controls.DarkCheckBox();
		this.trackBar1 = new System.Windows.Forms.TrackBar();
		this.btnSave = new DarkUI.Controls.DarkButton();
		this.btnRandomColors = new DarkUI.Controls.DarkButton();
		this.btnRefresh = new DarkUI.Controls.DarkButton();
		this.txtRGB = new DarkUI.Controls.DarkTextBox();
		this.btnReloadColors = new DarkUI.Controls.DarkButton();
		((System.ComponentModel.ISupportInitialize)this.trackBar1).BeginInit();
		base.SuspendLayout();
		this.colorDialog1.AnyColor = true;
		this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.comboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(20, 14);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(186, 21);
		this.comboBox1.TabIndex = 3;
		this.comboBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(comboBox1_DrawItem);
		this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
		this.btnSetColor.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnSetColor.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnSetColor.BackColorUseGeneric = false;
		this.btnSetColor.Checked = false;
		this.btnSetColor.Location = new System.Drawing.Point(20, 41);
		this.btnSetColor.Name = "btnSetColor";
		this.btnSetColor.Size = new System.Drawing.Size(209, 21);
		this.btnSetColor.TabIndex = 4;
		this.btnSetColor.Text = "Set Color of Selected";
		this.btnSetColor.Click += new System.EventHandler(btnLabelColor_Click);
		this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(54, 70);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(144, 17);
		this.checkBox1.TabIndex = 5;
		this.checkBox1.Text = "Set Selected to Centered";
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.trackBar1.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.trackBar1.Cursor = System.Windows.Forms.Cursors.SizeWE;
		this.trackBar1.Location = new System.Drawing.Point(20, 98);
		this.trackBar1.Maximum = 100;
		this.trackBar1.Minimum = 20;
		this.trackBar1.Name = "trackBar1";
		this.trackBar1.Size = new System.Drawing.Size(210, 45);
		this.trackBar1.TabIndex = 6;
		this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
		this.trackBar1.Value = 60;
		this.trackBar1.Scroll += new System.EventHandler(trackBar1_Scroll);
		this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnSave.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnSave.BackColorUseGeneric = false;
		this.btnSave.Checked = false;
		this.btnSave.Location = new System.Drawing.Point(20, 137);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(209, 23);
		this.btnSave.TabIndex = 7;
		this.btnSave.Text = "Save Size";
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.btnRandomColors.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnRandomColors.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnRandomColors.BackColorUseGeneric = false;
		this.btnRandomColors.Checked = false;
		this.btnRandomColors.Location = new System.Drawing.Point(20, 166);
		this.btnRandomColors.Name = "btnRandomColors";
		this.btnRandomColors.Size = new System.Drawing.Size(209, 23);
		this.btnRandomColors.TabIndex = 7;
		this.btnRandomColors.Text = "Random Colors based on RGB";
		this.btnRandomColors.Click += new System.EventHandler(btnRandomColors_Click);
		this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnRefresh.BackColorUseGeneric = false;
		this.btnRefresh.Checked = false;
		this.btnRefresh.Location = new System.Drawing.Point(209, 14);
		this.btnRefresh.Name = "btnRefresh";
		this.btnRefresh.Size = new System.Drawing.Size(20, 21);
		this.btnRefresh.TabIndex = 8;
		this.btnRefresh.Text = "R";
		this.btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
		this.txtRGB.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtRGB.Location = new System.Drawing.Point(20, 194);
		this.txtRGB.Name = "txtRGB";
		this.txtRGB.Size = new System.Drawing.Size(209, 20);
		this.txtRGB.TabIndex = 9;
		this.txtRGB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.btnReloadColors.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnReloadColors.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnReloadColors.BackColorUseGeneric = false;
		this.btnReloadColors.Checked = false;
		this.btnReloadColors.Location = new System.Drawing.Point(21, 220);
		this.btnReloadColors.Name = "btnReloadColors";
		this.btnReloadColors.Size = new System.Drawing.Size(209, 23);
		this.btnReloadColors.TabIndex = 7;
		this.btnReloadColors.Text = "Reload Colors";
		this.btnReloadColors.Click += new System.EventHandler(btnReloadColors_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		base.ClientSize = new System.Drawing.Size(245, 251);
		base.Controls.Add(this.txtRGB);
		base.Controls.Add(this.btnRefresh);
		base.Controls.Add(this.btnReloadColors);
		base.Controls.Add(this.btnRandomColors);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.trackBar1);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.btnSetColor);
		base.Controls.Add(this.comboBox1);
		base.Icon = Grimoire.Properties.Resources.GrimoireIcon;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "CommandColorForm";
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Colors";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(CommandColorForm_FormClosing);
		base.Load += new System.EventHandler(CommandColorForm_Load);
		base.Shown += new System.EventHandler(CommandColorForm_Shown);
		((System.ComponentModel.ISupportInitialize)this.trackBar1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
