using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DarkUI.Controls;
using DarkUI.Forms;
using Properties;

namespace Grimoire.UI;

public class DPSForm : DarkForm
{
	public Dictionary<string, int> Damage = new Dictionary<string, int>();

	public Dictionary<string, int> DamagePerSecond = new Dictionary<string, int>();

	public static DPSForm Instance = new DPSForm();

	private IContainer components;

	public Chart chart1;

	private Timer timer1;

	private DarkLabel darkLabel1;

	public DPSForm()
	{
		InitializeComponent();
		timer1.Start();
	}

	private void DPSForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing)
		{
			e.Cancel = true;
			Hide();
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (DamagePerSecond.Count == 0)
		{
			return;
		}
		int[] array = DamagePerSecond.Values.ToArray();
		darkLabel1.Text = int.Parse(string.Concat(array)).ToString();
		chart1.Series.Clear();
		foreach (KeyValuePair<string, int> item in DamagePerSecond)
		{
			chart1.Series.Add(item.Key);
			chart1.Series[item.Key].Points.Add(item.Value);
			chart1.Series[item.Key].ChartType = SeriesChartType.Column;
		}
		DamagePerSecond.Clear();
	}

	private void DPSForm_Load(object sender, EventArgs e)
	{
		chart1.Series.Clear();
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
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint item = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2.0, 301.0);
		this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.darkLabel1 = new DarkUI.Controls.DarkLabel();
		((System.ComponentModel.ISupportInitialize)this.chart1).BeginInit();
		base.SuspendLayout();
		this.chart1.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
		this.chart1.BackSecondaryColor = System.Drawing.Color.Transparent;
		this.chart1.BorderlineColor = System.Drawing.Color.Empty;
		chartArea.Name = "ChartArea1";
		this.chart1.ChartAreas.Add(chartArea);
		legend.Name = "Legend1";
		this.chart1.Legends.Add(legend);
		this.chart1.Location = new System.Drawing.Point(12, 12);
		this.chart1.Name = "chart1";
		this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
		series.ChartArea = "ChartArea1";
		series.Legend = "Legend1";
		series.Name = "Series1";
		series.Points.Add(item);
		this.chart1.Series.Add(series);
		this.chart1.Size = new System.Drawing.Size(517, 300);
		this.chart1.TabIndex = 0;
		this.chart1.Text = "chart1";
		this.timer1.Interval = 5000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.darkLabel1.AutoSize = true;
		this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.darkLabel1.Location = new System.Drawing.Point(55, 319);
		this.darkLabel1.Name = "darkLabel1";
		this.darkLabel1.Size = new System.Drawing.Size(57, 13);
		this.darkLabel1.TabIndex = 1;
		this.darkLabel1.Text = "Your DPS:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(541, 356);
		base.Controls.Add(this.darkLabel1);
		base.Controls.Add(this.chart1);
		base.Icon = Grimoire.Properties.Resources.GrimoireIcon;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(210, 173);
		base.Name = "DPSForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "DPS Form";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(DPSForm_FormClosing);
		base.Load += new System.EventHandler(DPSForm_Load);
		((System.ComponentModel.ISupportInitialize)this.chart1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
