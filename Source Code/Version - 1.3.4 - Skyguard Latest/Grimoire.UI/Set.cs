using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Game;
using Grimoire.Game.Data;
using Newtonsoft.Json;
using Properties;

namespace Grimoire.UI;

public class Set : DarkForm
{
	private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
	{
		DefaultValueHandling = DefaultValueHandling.Include,
		TypeNameHandling = TypeNameHandling.All
	};

	private string path = Application.StartupPath + "\\Sets";

	public static Set Instance = new Set();

	private IContainer components;

	private SaveFileDialog saveFileDialog1;

	private OpenFileDialog openFileDialog1;

	private DarkButton btnSave;

	private DarkListBox listBox1;

	private DarkComboBox comboBox1;

	private DarkButton btnAdd;

	private DarkButton btnRefresh;

	private DarkButton btnLoad;

	private DarkButton btnClear;

	public Set()
	{
		InitializeComponent();
	}

	private void ApplyConfig(SetItem config)
	{
		listBox1.Items.Clear();
		List<ISetInterface> set = config.Set;
		if (set != null && set.Count > 0)
		{
			ListBox.ObjectCollection ıtems = listBox1.Items;
			object[] array = config.Set.ToArray();
			object[] items = array;
			ıtems.AddRange(items);
		}
	}

	private SetItem GenerateConfig()
	{
		return new SetItem
		{
			Set = listBox1.Items.Cast<ISetInterface>().ToList()
		};
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		if (comboBox1.SelectedItem != null)
		{
			string name = ((InventoryItem)comboBox1.SelectedItem).Name;
			string text = ((InventoryItem)comboBox1.SelectedItem).Category;
			if (InventoryItem.Weapons.Contains(text))
			{
				text = "Weapon";
			}
			AddSetItem(new Item
			{
				Name = name,
				Type = text
			});
		}
	}

	private void Set_Load(object sender, EventArgs e)
	{
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
		foreach (string item2 in Directory.EnumerateFiles(path, "*.gset", SearchOption.TopDirectoryOnly))
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
		foreach (string item2 in Directory.EnumerateFiles(path, "*.gset", SearchOption.TopDirectoryOnly))
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
	}

	private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		string text;
		if (File.Exists(text = Path.Combine(path, e.Node.FullPath)))
		{
			TryDeserialize(File.ReadAllText(text), out var config);
			ApplyConfig(config);
		}
	}

	private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
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

	private void Set_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing)
		{
			e.Cancel = true;
			Hide();
		}
	}

	private void btnRefresh_Click(object sender, EventArgs e)
	{
		if (!Player.IsLoggedIn || Player.Inventory.Items == null)
		{
			return;
		}
		if (comboBox1.Items.Count > 0)
		{
			comboBox1.Items.Clear();
		}
		comboBox1.Items.Add("");
		comboBox1.Items.Add("[Weapons]");
		comboBox1.Items.Add("");
		foreach (InventoryItem ıtem in Player.Inventory.Items)
		{
			if (ıtem.IsWeapon)
			{
				comboBox1.Items.Add(ıtem);
			}
		}
		comboBox1.Items.Add("");
		comboBox1.Items.Add("[Classes]");
		comboBox1.Items.Add("");
		foreach (InventoryItem ıtem2 in Player.Inventory.Items)
		{
			if (ıtem2.Category == "Class")
			{
				comboBox1.Items.Add(ıtem2);
			}
		}
		comboBox1.Items.Add("");
		comboBox1.Items.Add("[Armors]");
		comboBox1.Items.Add("");
		foreach (InventoryItem ıtem3 in Player.Inventory.Items)
		{
			if (ıtem3.Category == "Armor")
			{
				comboBox1.Items.Add(ıtem3);
			}
		}
		comboBox1.Items.Add("");
		comboBox1.Items.Add("[Helmets]");
		comboBox1.Items.Add("");
		foreach (InventoryItem ıtem4 in Player.Inventory.Items)
		{
			if (ıtem4.Category == "Helm")
			{
				comboBox1.Items.Add(ıtem4);
			}
		}
		comboBox1.Items.Add("");
		comboBox1.Items.Add("[Capes]");
		comboBox1.Items.Add("");
		foreach (InventoryItem ıtem5 in Player.Inventory.Items)
		{
			if (ıtem5.Category == "Cape")
			{
				comboBox1.Items.Add(ıtem5);
			}
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		saveFileDialog1.Title = "Save Set";
		if (!Directory.Exists(Application.StartupPath + "\\Sets"))
		{
			Directory.CreateDirectory(Application.StartupPath + "\\Sets");
		}
		saveFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "Sets");
		saveFileDialog1.DefaultExt = ".gset";
		saveFileDialog1.Filter = "Grimoire sets|*.gset";
		saveFileDialog1.CheckFileExists = false;
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			SetItem value = GenerateConfig();
			try
			{
				File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _serializerSettings));
			}
			catch (Exception ex)
			{
				MessageBox.Show("Unable to save bot: " + ex.Message);
			}
		}
	}

	private bool TryDeserialize(string json, out SetItem config)
	{
		try
		{
			config = JsonConvert.DeserializeObject<SetItem>(json);
			return true;
		}
		catch
		{
		}
		config = null;
		return false;
	}

	private void btnLoad_Click(object sender, EventArgs e)
	{
		openFileDialog1.Title = "Load Set";
		if (!Directory.Exists(Application.StartupPath + "\\Sets"))
		{
			Directory.CreateDirectory(Application.StartupPath + "\\Sets");
		}
		openFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "Sets");
		openFileDialog1.DefaultExt = ".gset";
		openFileDialog1.Filter = "Grimoire sets|*.gset";
		if (openFileDialog1.ShowDialog() == DialogResult.OK && TryDeserialize(File.ReadAllText(openFileDialog1.FileName), out var config))
		{
			ApplyConfig(config);
		}
	}

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (comboBox1.SelectedItem.ToString() == "" || comboBox1.SelectedItem.ToString().Contains("["))
		{
			comboBox1.SelectedItem = comboBox1.SelectedIndex++;
		}
	}

	private void AddSetItem(ISetInterface cmd)
	{
		listBox1.Items.Add(cmd);
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		listBox1.Items.Clear();
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
		this.btnSave = new DarkUI.Controls.DarkButton();
		this.listBox1 = new DarkUI.Controls.DarkListBox(this.components);
		this.comboBox1 = new DarkUI.Controls.DarkComboBox();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.btnAdd = new DarkUI.Controls.DarkButton();
		this.btnRefresh = new DarkUI.Controls.DarkButton();
		this.btnLoad = new DarkUI.Controls.DarkButton();
		this.btnClear = new DarkUI.Controls.DarkButton();
		base.SuspendLayout();
		this.btnSave.Checked = false;
		this.btnSave.Location = new System.Drawing.Point(10, 355);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(112, 23);
		this.btnSave.TabIndex = 0;
		this.btnSave.Text = "Save";
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.listBox1.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.listBox1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.listBox1.FormattingEnabled = true;
		this.listBox1.ItemHeight = 18;
		this.listBox1.Location = new System.Drawing.Point(10, 65);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(352, 110);
		this.listBox1.TabIndex = 1;
		this.comboBox1.DisplayMember = "Name";
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(58, 13);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(206, 21);
		this.comboBox1.TabIndex = 3;
		this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
		this.openFileDialog1.FileName = "openFileDialog1";
		this.btnAdd.Checked = false;
		this.btnAdd.Location = new System.Drawing.Point(270, 11);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(98, 23);
		this.btnAdd.TabIndex = 5;
		this.btnAdd.Text = "Add";
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.btnRefresh.Checked = false;
		this.btnRefresh.Location = new System.Drawing.Point(12, 12);
		this.btnRefresh.Name = "btnRefresh";
		this.btnRefresh.Size = new System.Drawing.Size(40, 23);
		this.btnRefresh.TabIndex = 6;
		this.btnRefresh.Text = "R";
		this.btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
		this.btnLoad.Checked = false;
		this.btnLoad.Location = new System.Drawing.Point(250, 355);
		this.btnLoad.Name = "btnLoad";
		this.btnLoad.Size = new System.Drawing.Size(112, 23);
		this.btnLoad.TabIndex = 0;
		this.btnLoad.Text = "Load";
		this.btnLoad.Click += new System.EventHandler(btnLoad_Click);
		this.btnClear.Checked = false;
		this.btnClear.Location = new System.Drawing.Point(270, 36);
		this.btnClear.Name = "btnClear";
		this.btnClear.Size = new System.Drawing.Size(98, 23);
		this.btnClear.TabIndex = 5;
		this.btnClear.Text = "Clear";
		this.btnClear.Click += new System.EventHandler(btnClear_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		base.ClientSize = new System.Drawing.Size(374, 387);
		base.Controls.Add(this.btnRefresh);
		base.Controls.Add(this.btnClear);
		base.Controls.Add(this.btnAdd);
		base.Controls.Add(this.comboBox1);
		base.Controls.Add(this.listBox1);
		base.Controls.Add(this.btnLoad);
		base.Controls.Add(this.btnSave);
		base.Icon = Grimoire.Properties.Resources.GrimoireIcon;
		base.Name = "Set";
		this.Text = "Set";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Set_FormClosing);
		base.Load += new System.EventHandler(Set_Load);
		base.ResumeLayout(false);
	}
}
