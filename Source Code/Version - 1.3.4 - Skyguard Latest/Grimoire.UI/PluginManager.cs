using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Tools.Plugins;
using Properties;

namespace Grimoire.UI;

public class PluginManager : DarkForm
{
	private IContainer components;

	public DarkGroupBox gbLoaded;

	public DarkButton btnUnload;

	public DarkTextBox txtDesc;

	public DarkLabel lblAuthor;

	public ListBox lstLoaded;

	public DarkGroupBox gbLoad;

	public DarkButton btnBrowse;

	public DarkButton btnLoad;

	private TreeView treePlugins;

	public DarkTextBox txtPlugin;

	private string path = Application.StartupPath + "\\Plugins";

	private string Plugintext;

	public static PluginManager Instance { get; } = new PluginManager();

	public string LastError { get; private set; }

	public PluginManager()
	{
		InitializeComponent();
	}

	private void PluginManager_Load(object sender, EventArgs e)
	{
		lstLoaded.DisplayMember = "Name";
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		UpdateTree();
	}

	private void AddTreeNodes(TreeNode node, string path)
	{
		foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
		{
			string add = Path.GetFileName(item);
			if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
			{
				node.Nodes.Add(add).Nodes.Add("Loading...");
			}
		}
		foreach (string item2 in Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly))
		{
			string add2 = Path.GetFileName(item2);
			if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
			{
				node.Nodes.Add(add2);
			}
		}
	}

	private void AddTreeNodes(TreeView tree, string path)
	{
		foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
		{
			string add = Path.GetFileName(item);
			if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
			{
				tree.Nodes.Add(add).Nodes.Add("Loading...");
			}
		}
		foreach (string item2 in Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly))
		{
			string add2 = Path.GetFileName(item2);
			if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
			{
				tree.Nodes.Add(add2);
			}
		}
	}

	private void UpdateTree()
	{
		treePlugins.Nodes.Clear();
		AddTreeNodes(treePlugins, path);
	}

	private void treePlugins_AfterSelect(object sender, TreeViewEventArgs e)
	{
		string dllFilePath;
		if (File.Exists(dllFilePath = Path.Combine(path, e.Node.FullPath)))
		{
			GrimoirePlugin grimoirePlugin = new GrimoirePlugin(dllFilePath);
			if (grimoirePlugin.Load())
			{
				txtPlugin.Clear();
				lstLoaded.Items.Clear();
				ListBox.ObjectCollection ıtems = lstLoaded.Items;
				object[] array = GrimoirePlugin.LoadedPlugins.ToArray();
				object[] items = array;
				ıtems.AddRange(items);
				lstLoaded.SelectedItem = grimoirePlugin;
			}
			else
			{
				DarkMessageBox.Show(new Form
				{
					TopMost = true,
					StartPosition = FormStartPosition.CenterScreen
				}, grimoirePlugin.LastError, "Grimoire", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void treePlugins_AfterExpand(object sender, TreeViewEventArgs e)
	{
		string text;
		if (Directory.Exists(text = Path.Combine(path, e.Node.FullPath)))
		{
			AddTreeNodes(e.Node, text);
			if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "Loading...")
			{
				e.Node.Nodes.RemoveAt(0);
			}
		}
	}

	private void btnBrowse_Click(object sender, EventArgs e)
	{
		using OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = "Load Grimoire plugin";
		openFileDialog.Filter = "Dynamic Link Library|*.dll";
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			txtPlugin.Text = openFileDialog.SafeFileName;
			Plugintext = openFileDialog.FileName;
		}
	}

	public bool LoadPlugins(string path)
	{
		GrimoirePlugin grimoirePlugin = null;
		if (File.Exists(path))
		{
			grimoirePlugin = new GrimoirePlugin(path);
			if (grimoirePlugin.Load())
			{
				GrimoirePlugin.LoadedPlugins.Add(grimoirePlugin);
				return true;
			}
		}
		LastError = grimoirePlugin?.LastError;
		return false;
	}

	private void btnLoad_Click(object sender, EventArgs e)
	{
		string plugintext;
		if (File.Exists(plugintext = Plugintext))
		{
			GrimoirePlugin grimoirePlugin = new GrimoirePlugin(plugintext);
			if (grimoirePlugin.Load())
			{
				txtPlugin.Clear();
				lstLoaded.Items.Clear();
				ListBox.ObjectCollection ıtems = lstLoaded.Items;
				object[] array = GrimoirePlugin.LoadedPlugins.ToArray();
				object[] items = array;
				ıtems.AddRange(items);
				lstLoaded.SelectedItem = grimoirePlugin;
			}
			else
			{
				DarkMessageBox.Show(new Form
				{
					TopMost = true,
					StartPosition = FormStartPosition.CenterScreen
				}, grimoirePlugin.LastError, "Error Detected!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void btnUnload_Click(object sender, EventArgs e)
	{
		int selectedIndex;
		if ((selectedIndex = lstLoaded.SelectedIndex) > -1)
		{
			GrimoirePlugin grimoirePlugin = GrimoirePlugin.LoadedPlugins[selectedIndex];
			if (grimoirePlugin.Unload())
			{
				lstLoaded.Items.RemoveAt(selectedIndex);
				lblAuthor.Text = "Plugin created by:";
				txtDesc.Clear();
			}
			else
			{
				DarkMessageBox.Show(new Form
				{
					TopMost = true,
					StartPosition = FormStartPosition.CenterScreen
				}, grimoirePlugin.LastError, "Error Detected!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	public bool LoadRange(string[] paths)
	{
		return paths.All(LoadPlugins);
	}

	private void lstLoaded_SelectedIndexChanged(object sender, EventArgs e)
	{
		int selectedIndex;
		if ((selectedIndex = lstLoaded.SelectedIndex) > -1)
		{
			GrimoirePlugin grimoirePlugin = GrimoirePlugin.LoadedPlugins[selectedIndex];
			lblAuthor.Text = "Plugin created by: " + grimoirePlugin.Author;
			txtDesc.Text = grimoirePlugin.Description;
		}
	}

	private void PluginManager_Shown(object sender, EventArgs e)
	{
		Root.SetDRM(Instance, !Root.Instance.enableCapture);
	}

	private void PluginManager_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing)
		{
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
		this.gbLoaded = new DarkUI.Controls.DarkGroupBox();
		this.btnUnload = new DarkUI.Controls.DarkButton();
		this.txtDesc = new DarkUI.Controls.DarkTextBox();
		this.lblAuthor = new DarkUI.Controls.DarkLabel();
		this.lstLoaded = new System.Windows.Forms.ListBox();
		this.gbLoad = new DarkUI.Controls.DarkGroupBox();
		this.btnBrowse = new DarkUI.Controls.DarkButton();
		this.btnLoad = new DarkUI.Controls.DarkButton();
		this.txtPlugin = new DarkUI.Controls.DarkTextBox();
		this.treePlugins = new System.Windows.Forms.TreeView();
		this.gbLoaded.SuspendLayout();
		this.gbLoad.SuspendLayout();
		base.SuspendLayout();
		this.gbLoaded.Controls.Add(this.btnUnload);
		this.gbLoaded.Controls.Add(this.txtDesc);
		this.gbLoaded.Controls.Add(this.lblAuthor);
		this.gbLoaded.Controls.Add(this.lstLoaded);
		this.gbLoaded.Location = new System.Drawing.Point(12, 213);
		this.gbLoaded.Name = "gbLoaded";
		this.gbLoaded.Size = new System.Drawing.Size(292, 260);
		this.gbLoaded.TabIndex = 12;
		this.gbLoaded.TabStop = false;
		this.gbLoaded.Text = "Loaded plugins";
		this.btnUnload.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnUnload.BackColorUseGeneric = false;
		this.btnUnload.Checked = false;
		this.btnUnload.Location = new System.Drawing.Point(148, 231);
		this.btnUnload.Name = "btnUnload";
		this.btnUnload.Size = new System.Drawing.Size(135, 23);
		this.btnUnload.TabIndex = 3;
		this.btnUnload.Text = "Unload selected plugin";
		this.btnUnload.Click += new System.EventHandler(btnUnload_Click);
		this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.txtDesc.Location = new System.Drawing.Point(6, 112);
		this.txtDesc.Multiline = true;
		this.txtDesc.Name = "txtDesc";
		this.txtDesc.ReadOnly = true;
		this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.txtDesc.Size = new System.Drawing.Size(277, 113);
		this.txtDesc.TabIndex = 2;
		this.lblAuthor.AutoSize = true;
		this.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.lblAuthor.Location = new System.Drawing.Point(3, 96);
		this.lblAuthor.Name = "lblAuthor";
		this.lblAuthor.Size = new System.Drawing.Size(92, 13);
		this.lblAuthor.TabIndex = 1;
		this.lblAuthor.Text = "Plugin created by:";
		this.lstLoaded.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		this.lstLoaded.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.lstLoaded.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.lstLoaded.FormattingEnabled = true;
		this.lstLoaded.Location = new System.Drawing.Point(6, 19);
		this.lstLoaded.Name = "lstLoaded";
		this.lstLoaded.Size = new System.Drawing.Size(277, 67);
		this.lstLoaded.TabIndex = 0;
		this.lstLoaded.SelectedIndexChanged += new System.EventHandler(lstLoaded_SelectedIndexChanged);
		this.gbLoad.Controls.Add(this.btnBrowse);
		this.gbLoad.Controls.Add(this.btnLoad);
		this.gbLoad.Controls.Add(this.txtPlugin);
		this.gbLoad.Location = new System.Drawing.Point(12, 12);
		this.gbLoad.Name = "gbLoad";
		this.gbLoad.Size = new System.Drawing.Size(292, 51);
		this.gbLoad.TabIndex = 11;
		this.gbLoad.TabStop = false;
		this.gbLoad.Text = "Load plugin";
		this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnBrowse.BackColorUseGeneric = false;
		this.btnBrowse.Checked = false;
		this.btnBrowse.Location = new System.Drawing.Point(200, 19);
		this.btnBrowse.Name = "btnBrowse";
		this.btnBrowse.Size = new System.Drawing.Size(25, 20);
		this.btnBrowse.TabIndex = 7;
		this.btnBrowse.Text = "...";
		this.btnBrowse.Click += new System.EventHandler(btnBrowse_Click);
		this.btnLoad.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnLoad.BackColorUseGeneric = false;
		this.btnLoad.Checked = false;
		this.btnLoad.Location = new System.Drawing.Point(231, 19);
		this.btnLoad.Name = "btnLoad";
		this.btnLoad.Size = new System.Drawing.Size(55, 20);
		this.btnLoad.TabIndex = 8;
		this.btnLoad.Text = "Load";
		this.btnLoad.Click += new System.EventHandler(btnLoad_Click);
		this.txtPlugin.Location = new System.Drawing.Point(6, 19);
		this.txtPlugin.Name = "txtPlugin";
		this.txtPlugin.Size = new System.Drawing.Size(188, 20);
		this.txtPlugin.TabIndex = 4;
		this.treePlugins.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		this.treePlugins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.treePlugins.ForeColor = System.Drawing.Color.Gainsboro;
		this.treePlugins.HotTracking = true;
		this.treePlugins.Location = new System.Drawing.Point(12, 70);
		this.treePlugins.Name = "treePlugins";
		this.treePlugins.Size = new System.Drawing.Size(292, 136);
		this.treePlugins.TabIndex = 13;
		this.treePlugins.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(treePlugins_AfterExpand);
		this.treePlugins.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treePlugins_AfterSelect);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		base.ClientSize = new System.Drawing.Size(316, 485);
		base.Controls.Add(this.treePlugins);
		base.Controls.Add(this.gbLoaded);
		base.Controls.Add(this.gbLoad);
		base.Icon = Grimoire.Properties.Resources.GrimoireIcon;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "PluginManager";
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Plugin Manager";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(PluginManager_FormClosing);
		base.Shown += new System.EventHandler(PluginManager_Shown);
		base.Load += new System.EventHandler(PluginManager_Load);
		this.gbLoaded.ResumeLayout(false);
		this.gbLoaded.PerformLayout();
		this.gbLoad.ResumeLayout(false);
		this.gbLoad.PerformLayout();
		base.ResumeLayout(false);
	}
}
