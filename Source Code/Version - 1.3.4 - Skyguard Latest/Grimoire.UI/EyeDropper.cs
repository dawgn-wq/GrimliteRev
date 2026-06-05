using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Properties;
using Unity3.Eyedropper;

namespace Grimoire.UI;

public class EyeDropper : DarkForm
{
	private IContainer components;

	private RichTextBox richTextBox1;

	public Unity3.Eyedropper.EyeDropper eyeDropper1;

	private DarkTextBox textBox1;

	public static EyeDropper Instance { get; } = new EyeDropper();

	public EyeDropper()
	{
		InitializeComponent();
	}

	private void EyeDropper_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing)
		{
			e.Cancel = true;
			Hide();
		}
	}

	private void EyeDropper_Shown(object sender, EventArgs e)
	{
		Root.SetDRM(Instance, !Root.Instance.enableCapture);
	}

	private void eyeDropper1_ScreenCaptured(Bitmap capturedPixels, Color capturedColor)
	{
		string text = string.Format(Environment.NewLine + "#{0} / {1}, {2}, {3} / #{4}", eyeDropper1.SelectedColor.ToArgb().ToString("X2"), eyeDropper1.SelectedColor.R, eyeDropper1.SelectedColor.G, eyeDropper1.SelectedColor.B, eyeDropper1.SelectedColor.ToArgb().ToString("X2").Substring(2));
		textBox1.BackColor = eyeDropper1.SelectedColor;
		textBox1.ForeColor = (((double)textBox1.BackColor.GetBrightness() > 0.7) ? Color.Black : Color.White);
		textBox1.Text = text;
	}

	private void eyeDropper1_EndScreenCapture(object sender, EventArgs e)
	{
		string text = string.Format("#{0} / {1}, {2}, {3} / #{4}\n", eyeDropper1.SelectedColor.ToArgb().ToString("X2"), eyeDropper1.SelectedColor.R, eyeDropper1.SelectedColor.G, eyeDropper1.SelectedColor.B, eyeDropper1.SelectedColor.ToArgb().ToString("X2").Substring(2));
		richTextBox1.AppendText(text, eyeDropper1.SelectedColor);
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
		this.eyeDropper1 = new Unity3.Eyedropper.EyeDropper();
		this.richTextBox1 = new System.Windows.Forms.RichTextBox();
		this.textBox1 = new DarkUI.Controls.DarkTextBox();
		base.SuspendLayout();
		this.eyeDropper1.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.eyeDropper1.Location = new System.Drawing.Point(10, 8);
		this.eyeDropper1.MaximumSize = new System.Drawing.Size(22, 22);
		this.eyeDropper1.MinimumSize = new System.Drawing.Size(22, 22);
		this.eyeDropper1.Name = "eyeDropper1";
		this.eyeDropper1.PixelPreviewSize = new System.Drawing.Size(150, 150);
		this.eyeDropper1.PixelPreviewZoom = 15f;
		this.eyeDropper1.PreviewLocation = new System.Drawing.Point(-120, 20);
		this.eyeDropper1.PreviewPositionStyle = Unity3.Eyedropper.EyeDropper.ePreviewPositionStyle.BottomLeft;
		this.eyeDropper1.SelectedColor = System.Drawing.Color.Empty;
		this.eyeDropper1.ShowColorPreview = false;
		this.eyeDropper1.Size = new System.Drawing.Size(22, 22);
		this.eyeDropper1.TabIndex = 0;
		this.eyeDropper1.Text = "eyeDropper1";
		this.eyeDropper1.ScreenCaptured += new Unity3.Eyedropper.EyeDropper.ScreenCapturedArgs(eyeDropper1_ScreenCaptured);
		this.eyeDropper1.EndScreenCapture += new System.EventHandler(eyeDropper1_EndScreenCapture);
		this.richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		this.richTextBox1.Location = new System.Drawing.Point(0, 34);
		this.richTextBox1.Name = "richTextBox1";
		this.richTextBox1.Size = new System.Drawing.Size(235, 104);
		this.richTextBox1.TabIndex = 1;
		this.richTextBox1.Text = "";
		this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.textBox1.Location = new System.Drawing.Point(43, -3);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(192, 37);
		this.textBox1.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		base.ClientSize = new System.Drawing.Size(235, 138);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.richTextBox1);
		base.Controls.Add(this.eyeDropper1);
		base.Icon = Grimoire.Properties.Resources.GrimoireIcon;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "EyeDropper";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = " Eye Dropper";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(EyeDropper_FormClosing);
		base.Shown += new System.EventHandler(EyeDropper_Shown);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
