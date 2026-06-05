using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.UI;

public class CommandNode : ResizableUserControl
{
	public Point previousPosition;

	private IContainer components;

	private Button btnRemove;

	public Control activeControl { get; set; }

	public CommandNode()
	{
		InitializeComponent();
		previousPosition = base.Location;
	}

	private void CommandNode_MouseDown(object sender, MouseEventArgs e)
	{
		activeControl = sender as Control;
		previousPosition = e.Location;
		Cursor = Cursors.Hand;
	}

	private void CommandNode_MouseMove(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			if (activeControl.Location.X == Math.Min(Math.Max(activeControl.Right + (e.X - previousPosition.X), 0), activeControl.Parent.Width - activeControl.Width) && activeControl.Location.Y == Math.Min(Math.Max(activeControl.Bottom + (e.Y - previousPosition.Y), 0), activeControl.Parent.Height - activeControl.Height))
			{
				int num = Math.Min(Math.Max(activeControl.Right + (e.X - previousPosition.X), 0), activeControl.Parent.Width - activeControl.Width);
				int num2 = Math.Min(Math.Max(activeControl.Bottom + (e.Y - previousPosition.Y), 0), activeControl.Parent.Height - activeControl.Height);
				activeControl.Location = new Point(num, num2);
			}
			else
			{
				int num3 = Math.Min(Math.Max(activeControl.Left + (e.X - previousPosition.X), 0), activeControl.Parent.Width - activeControl.Width);
				int num4 = Math.Min(Math.Max(activeControl.Top + (e.Y - previousPosition.Y), 0), activeControl.Parent.Height - activeControl.Height);
				activeControl.Location = new Point(num3, num4);
			}
		}
	}

	private void CommandNode_KeyDown(object sender, KeyEventArgs e)
	{
		Keys keyCode = e.KeyCode;
		if (keyCode == Keys.Delete)
		{
			Dispose();
		}
	}

	private void btnRemove_Click(object sender, EventArgs e)
	{
		Dispose();
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
		this.btnRemove = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRemove.Location = new System.Drawing.Point(208, -1);
		this.btnRemove.Name = "btnRemove";
		this.btnRemove.Size = new System.Drawing.Size(23, 23);
		this.btnRemove.TabIndex = 0;
		this.btnRemove.Text = "X";
		this.btnRemove.UseVisualStyleBackColor = true;
		this.btnRemove.Click += new System.EventHandler(btnRemove_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		base.Controls.Add(this.btnRemove);
		base.Name = "CommandNode";
		base.Size = new System.Drawing.Size(230, 209);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(CommandNode_KeyDown);
		base.MouseDown += new System.Windows.Forms.MouseEventHandler(CommandNode_MouseDown);
		base.MouseMove += new System.Windows.Forms.MouseEventHandler(CommandNode_MouseMove);
		base.ResumeLayout(false);
	}
}
