using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Properties;

namespace Grimoire.UI;

public class UserFriendlyCommandEditor : DarkForm
{
	private IContainer components;

	private DarkButton btnOK;

	private DarkListBox listContainer1;

	private ToolStripContainer toolStripContainer1;

	private SplitContainer splitContainer1;

	private DarkButton btnRawCommand;

	private DarkButton btnCancel;

	public static UserFriendlyCommandEditor Instance = new UserFriendlyCommandEditor();

	private static readonly JsonSerializerSettings _questSerializerSettings = new JsonSerializerSettings
	{
		DefaultValueHandling = DefaultValueHandling.Ignore,
		NullValueHandling = NullValueHandling.Include,
		TypeNameHandling = TypeNameHandling.All
	};

	private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
	{
		DefaultValueHandling = DefaultValueHandling.Include,
		NullValueHandling = NullValueHandling.Include,
		TypeNameHandling = TypeNameHandling.All
	};

	private List<StatementCommand> statementCommands;

	private static object cmdObj { get; set; }

	private static UserFriendlyCommandEditor commandEditor { get; set; }

	public static string cmd { get; set; }

	private UserFriendlyCommandEditor()
	{
		InitializeComponent();
		statementCommands = JsonConvert.DeserializeObject<List<StatementCommand>>(Resources.statementcmds, _serializerSettings);
	}

	private void UserFriendlyCommandEditor_Shown(object sender, EventArgs e)
	{
		Root.SetDRM(Instance, !Root.Instance.enableCapture);
	}

	private void UserFriendlyCommandEditor_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void RawCommandEditor_Load(object sender, EventArgs e)
	{
	}

	private void txtCmd_KeyDown(object sender, KeyEventArgs e)
	{
		switch (e.KeyCode)
		{
		case Keys.Return:
			btnOK.PerformClick();
			break;
		case Keys.Escape:
			btnCancel.PerformClick();
			break;
		}
	}

	public static string Show(object obj)
	{
		cmdObj = obj;
		JsonSerializerSettings settings = ((obj.GetType() == typeof(CmdCompleteQuest) || obj.GetType() == typeof(CmdAcceptQuest)) ? _questSerializerSettings : _serializerSettings);
		JObject content = JObject.Parse(JsonConvert.SerializeObject(obj, settings));
		using (commandEditor = new UserFriendlyCommandEditor())
		{
			int num = 13;
			int num2 = 0;
			string[] array = new string[4] { "Tag", "Description1", "Description2", "$type" };
			Dictionary<string, KeyValuePair<DarkLabel, DarkTextBox>> dictionary = new Dictionary<string, KeyValuePair<DarkLabel, DarkTextBox>>();
			foreach (KeyValuePair<string, JToken> item in content)
			{
				if (string.IsNullOrEmpty(item.Key) || Array.IndexOf(array, item.Key) != -1 || !(commandEditor.statementCommands.Find((StatementCommand s) => s.GetType() == content.GetType())?.Text != item.Key))
				{
					continue;
				}
				string text = item.Key;
				string text2 = item.Value.ToString();
				switch (item.Key)
				{
				case "Value1":
					text = commandEditor.statementCommands.Find((StatementCommand s) => s.GetType() == obj.GetType()).Description1;
					text2 = ((text2 == text) ? "" : text2);
					break;
				case "Value2":
					text = commandEditor.statementCommands.Find((StatementCommand s) => s.GetType() == obj.GetType()).Description2;
					text2 = ((text2 == text) ? "" : text2);
					break;
				case "Quest":
					text2 = JsonConvert.DeserializeObject<Quest>(item.Value.ToString()).Id + " (use Raw Editor)";
					break;
				}
				dictionary.Add(item.Key, new KeyValuePair<DarkLabel, DarkTextBox>(new DarkLabel
				{
					Name = $"lbl{item.Key}{num2}",
					Text = text,
					Size = new Size(90, 20),
					Location = new Point(25, num + 2),
					Anchor = (AnchorStyles.Top | AnchorStyles.Left)
				}, new DarkTextBox
				{
					Name = $"tb{item.Key}{num2}",
					Text = text2,
					Size = new Size(160, 20),
					Location = new Point(125, num),
					Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right)
				}));
				commandEditor.Controls.Add(dictionary[item.Key].Key);
				commandEditor.Controls.Add(dictionary[item.Key].Value);
				num2++;
				num += 30;
			}
			commandEditor.Size = new Size(commandEditor.Size.Width, commandEditor.Size.Height + num - 13);
			DialogResult dialogResult = commandEditor.ShowDialog();
			bool flag = dialogResult == DialogResult.OK;
			bool flag2 = dialogResult == DialogResult.Abort;
			if (flag)
			{
				foreach (KeyValuePair<string, JToken> item2 in content)
				{
					if (dictionary.ContainsKey(item2.Key))
					{
						content[item2.Key] = dictionary[item2.Key].Value.Text;
					}
				}
				string result = JsonConvert.SerializeObject(content, Formatting.Indented, _serializerSettings);
				BotManager.Instance.LastIndexedSearch = 0;
				return result;
			}
			if (flag2)
			{
				return RawCommandEditor.Show(JsonConvert.SerializeObject(cmdObj, Formatting.Indented, settings));
			}
			return null;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Grimoire.UI.UserFriendlyCommandEditor));
		this.btnOK = new DarkUI.Controls.DarkButton();
		this.btnCancel = new DarkUI.Controls.DarkButton();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.btnRawCommand = new DarkUI.Controls.DarkButton();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		base.SuspendLayout();
		this.btnOK.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnOK.BackColorUseGeneric = false;
		this.btnOK.Checked = false;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnOK.Location = new System.Drawing.Point(0, 0);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(137, 23);
		this.btnOK.TabIndex = 0;
		this.btnOK.Text = "OK";
		this.btnCancel.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnCancel.BackColorUseGeneric = false;
		this.btnCancel.Checked = false;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnCancel.Location = new System.Drawing.Point(0, 0);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(141, 23);
		this.btnCancel.TabIndex = 1;
		this.btnCancel.Text = "Cancel";
		this.splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.splitContainer1.Location = new System.Drawing.Point(12, 46);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
		this.splitContainer1.Panel2.Controls.Add(this.btnOK);
		this.splitContainer1.Size = new System.Drawing.Size(282, 23);
		this.splitContainer1.SplitterDistance = 141;
		this.splitContainer1.TabIndex = 2;
		this.btnRawCommand.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnRawCommand.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnRawCommand.BackColorUseGeneric = false;
		this.btnRawCommand.Checked = false;
		this.btnRawCommand.DialogResult = System.Windows.Forms.DialogResult.Abort;
		this.btnRawCommand.Location = new System.Drawing.Point(12, 17);
		this.btnRawCommand.Name = "btnRawCommand";
		this.btnRawCommand.Size = new System.Drawing.Size(282, 23);
		this.btnRawCommand.TabIndex = 3;
		this.btnRawCommand.Text = "Raw Command Editor";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		base.ClientSize = new System.Drawing.Size(308, 81);
		base.Controls.Add(this.btnRawCommand);
		base.Controls.Add(this.splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MinimizeBox = false;
		base.Name = "UserFriendlyCommandEditor";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Command Editor";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(UserFriendlyCommandEditor_FormClosing);
		base.Load += new System.EventHandler(RawCommandEditor_Load);
		base.Shown += new System.EventHandler(UserFriendlyCommandEditor_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(UserFriendlyCommandEditor_KeyDown);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void UserFriendlyCommandEditor_KeyDown(object sender, KeyEventArgs e)
	{
		switch (e.KeyCode)
		{
		case Keys.Return:
			btnOK.PerformClick();
			break;
		case Keys.Escape:
			btnCancel.PerformClick();
			break;
		}
	}
}
