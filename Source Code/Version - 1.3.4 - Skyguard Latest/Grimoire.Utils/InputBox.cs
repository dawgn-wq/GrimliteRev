using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;

namespace Grimoire.Utils;

public class InputBox
{
	private static int extraHeight = 20;

	private static int labelHeight = 20;

	public static DialogResult InputDialog(ref string input, string prompt, string title = "Title", int width = 250, int height = 85, int eHeight = 20, int lHeight = 20)
	{
		extraHeight = eHeight;
		labelHeight = lHeight;
		return ShowInputDialogBox(ref input, prompt, title, width, height);
	}

	public static DialogResult ShowInputDialogBox(ref string input, string prompt, string title = "Title", int width = 250, int height = 85)
	{
		Size clientSize = new Size(width, height);
		DarkForm darkForm = new DarkForm();
		darkForm.MaximizeBox = false;
		darkForm.MinimizeBox = false;
		darkForm.TopMost = true;
		darkForm.StartPosition = FormStartPosition.CenterScreen;
		darkForm.FormBorderStyle = FormBorderStyle.FixedDialog;
		darkForm.ClientSize = clientSize;
		darkForm.Text = title;
		RichTextBox richTextBox = new RichTextBox();
		richTextBox.ForeColor = Color.White;
		richTextBox.Text = prompt;
		richTextBox.Location = new Point(5, 5);
		richTextBox.Multiline = true;
		richTextBox.BorderStyle = BorderStyle.None;
		richTextBox.Size = new Size(clientSize.Width - 10, labelHeight);
		richTextBox.ReadOnly = true;
		richTextBox.ForeColor = Color.White;
		richTextBox.BackColor = Color.FromArgb(28, 32, 40);
		richTextBox.TabIndex = 2;
		darkForm.Controls.Add(richTextBox);
		DarkTextBox darkTextBox = new DarkTextBox();
		darkTextBox.BorderStyle = BorderStyle.FixedSingle;
		darkTextBox.Size = new Size(clientSize.Width - 10, 23);
		darkTextBox.Location = new Point(5, richTextBox.Location.Y + extraHeight);
		darkTextBox.Text = "";
		darkTextBox.TabIndex = 1;
		darkForm.Controls.Add(darkTextBox);
		DarkButton darkButton = new DarkButton();
		darkButton.DialogResult = DialogResult.OK;
		darkButton.BackColor = Color.FromArgb(45, 51, 66);
		darkButton.Name = "okButton";
		darkButton.Size = new Size(75, 23);
		darkButton.Text = "OK";
		darkButton.Location = new Point(clientSize.Width - 80 - 80, clientSize.Height - 30);
		darkForm.Controls.Add(darkButton);
		DarkButton darkButton2 = new DarkButton();
		darkButton2.BackColor = Color.FromArgb(45, 51, 66);
		darkButton2.DialogResult = DialogResult.Cancel;
		darkButton2.Name = "cancelButton";
		darkButton2.Size = new Size(75, 23);
		darkButton2.Text = "Cancel";
		darkButton2.Location = new Point(clientSize.Width - 80, clientSize.Height - 30);
		darkForm.Controls.Add(darkButton2);
		darkForm.AcceptButton = darkButton;
		darkForm.CancelButton = darkButton2;
		DialogResult result = darkForm.ShowDialog();
		input = darkTextBox.Text;
		return result;
	}
}
