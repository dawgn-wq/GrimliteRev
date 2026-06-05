using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Networking;
using Properties;

namespace Grimoire.UI;

public class PacketLogger : DarkForm
{
	private IContainer components;

	public DarkTextBox txtPackets;

	public DarkButton btnStart;

	private DarkButton btnStop;

	private DarkButton btnCopy;

	private TableLayoutPanel tableLayoutPanel1;

	private DarkButton btnClear;

	public static PacketLogger Instance { get; } = new PacketLogger();

	private PacketLogger()
	{
		InitializeComponent();
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		txtPackets.Clear();
	}

	private void btnCopy_Click(object sender, EventArgs e)
	{
		if (txtPackets.Text.Length > 0)
		{
			Clipboard.SetText(txtPackets.Text);
		}
	}

	private void btnStop_Click(object sender, EventArgs e)
	{
		Proxy.Instance._catchXtPackets = false;
		btnStart.Enabled = true;
	}

	private void btnStart_Click(object sender, EventArgs e)
	{
		Proxy.Instance._catchXtPackets = true;
		btnStart.Enabled = false;
	}

	private void PacketLogger_Shown(object sender, EventArgs e)
	{
		Root.SetDRM(Instance, !Root.Instance.enableCapture);
	}

	private void PacketLogger_FormClosing(object sender, FormClosingEventArgs e)
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
		this.txtPackets = new DarkUI.Controls.DarkTextBox();
		this.btnStart = new DarkUI.Controls.DarkButton();
		this.btnStop = new DarkUI.Controls.DarkButton();
		this.btnCopy = new DarkUI.Controls.DarkButton();
		this.btnClear = new DarkUI.Controls.DarkButton();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.tableLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.txtPackets.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtPackets.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.txtPackets.Location = new System.Drawing.Point(15, 12);
		this.txtPackets.MaxLength = int.MaxValue;
		this.txtPackets.Multiline = true;
		this.txtPackets.Name = "txtPackets";
		this.txtPackets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtPackets.Size = new System.Drawing.Size(416, 244);
		this.txtPackets.TabIndex = 15;
		this.btnStart.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnStart.BackColorUseGeneric = false;
		this.btnStart.Checked = false;
		this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnStart.Location = new System.Drawing.Point(318, 3);
		this.btnStart.Name = "btnStart";
		this.btnStart.Size = new System.Drawing.Size(101, 24);
		this.btnStart.TabIndex = 16;
		this.btnStart.Text = "Start";
		this.btnStart.Click += new System.EventHandler(btnStart_Click);
		this.btnStop.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnStop.BackColorUseGeneric = false;
		this.btnStop.Checked = false;
		this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnStop.Location = new System.Drawing.Point(213, 3);
		this.btnStop.Name = "btnStop";
		this.btnStop.Size = new System.Drawing.Size(99, 24);
		this.btnStop.TabIndex = 17;
		this.btnStop.Text = "Stop";
		this.btnStop.Click += new System.EventHandler(btnStop_Click);
		this.btnCopy.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnCopy.BackColorUseGeneric = false;
		this.btnCopy.Checked = false;
		this.btnCopy.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnCopy.Location = new System.Drawing.Point(108, 3);
		this.btnCopy.Name = "btnCopy";
		this.btnCopy.Size = new System.Drawing.Size(99, 24);
		this.btnCopy.TabIndex = 18;
		this.btnCopy.Text = "Copy";
		this.btnCopy.Click += new System.EventHandler(btnCopy_Click);
		this.btnClear.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnClear.BackColorUseGeneric = false;
		this.btnClear.Checked = false;
		this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnClear.Location = new System.Drawing.Point(3, 3);
		this.btnClear.Name = "btnClear";
		this.btnClear.Size = new System.Drawing.Size(99, 24);
		this.btnClear.TabIndex = 19;
		this.btnClear.Text = "Clear";
		this.btnClear.Click += new System.EventHandler(btnClear_Click);
		this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tableLayoutPanel1.ColumnCount = 4;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.tableLayoutPanel1.Controls.Add(this.btnClear, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnStart, 3, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnStop, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnCopy, 1, 0);
		this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 259);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(422, 30);
		this.tableLayoutPanel1.TabIndex = 20;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		base.ClientSize = new System.Drawing.Size(451, 291);
		base.Controls.Add(this.tableLayoutPanel1);
		base.Controls.Add(this.txtPackets);
		base.Icon = Grimoire.Properties.Resources.GrimoireIcon;
		base.Name = "PacketLogger";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Packet Sniffer";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(PacketLogger_FormClosing);
		base.Shown += new System.EventHandler(PacketLogger_Shown);
		this.tableLayoutPanel1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
