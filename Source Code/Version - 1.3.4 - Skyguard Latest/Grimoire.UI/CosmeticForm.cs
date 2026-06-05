using System;
using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Tools;
using Grimoire.Utils;
using Properties;

namespace Grimoire.UI;

public class CosmeticForm : DarkForm
{
	private IContainer components;

	private TableLayoutPanel tableLayoutPanel1;

	private LinkLabel linkHelm;

	private LinkLabel linkArmor;

	private LinkLabel linkWeapon;

	private LinkLabel linkPet;

	private LinkLabel linkCape;

	private LinkLabel linkClass;

	private TableLayoutPanel tableLayoutPanel2;

	private TableLayoutPanel tableLayoutPanel3;

	private DarkComboBox cbPlayer;

	private DarkButton btnGrabCosm;

	private DarkListBox lbItems;

	private DarkButton btnCopyAll;

	private DarkButton btnEquipSelected;

	private DarkButton btnClear;

	private DarkButton btnRefresh;

	private DarkLabel label1;

	private DarkLabel label2;

	private DarkLabel label3;

	private DarkLabel label4;

	private DarkLabel label5;

	private DarkLabel label6;

	private DarkTextBox txtArmor1;

	private DarkTextBox txtHelm1;

	private DarkTextBox txtWeapon1;

	private DarkTextBox txtClass1;

	private DarkTextBox txtCape1;

	private DarkTextBox txtPet1;

	private DarkButton btnHelmSet;

	private DarkButton btnArmorSet;

	private DarkButton btnClassSet;

	private DarkButton btnWeaponSet;

	private DarkButton btnPetSet;

	private DarkButton btnCapeSet;

	private DarkTextBox txtHelm2;

	private DarkTextBox txtArmor2;

	private DarkTextBox txtClass2;

	private DarkTextBox txtWeapon2;

	private DarkTextBox txtPet2;

	private DarkTextBox txtCape2;

	private DarkButton btnSaveSet;

	private DarkButton btnLoadSet;

	private DarkTextBox txtOff2;

	private DarkTextBox txtOff1;

	private DarkButton btnSetOffhand;

	private DarkLabel label7;

	private DarkButton btnRemove;

	private LinkLabel linkGround;

	private DarkLabel darkLabel1;

	private DarkButton btnGroundSet;

	private DarkTextBox txtGround2;

	private DarkTextBox txtGround1;

	public static CosmeticForm Instance { get; } = new CosmeticForm();

	public CosmeticForm()
	{
		InitializeComponent();
		lbItems.SelectionMode = SelectionMode.MultiExtended;
	}

	private void CosmeticForm_Shown(object sender, EventArgs e)
	{
		Root.SetDRM(Instance, !Root.Instance.enableCapture);
	}

	private void lnkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (Player.IsLoggedIn)
		{
			cbPlayer.Items.Clear();
			World.RefreshDictionary();
			ComboBox.ObjectCollection ıtems = cbPlayer.Items;
			object[] items = World.Players.ToArray();
			ıtems.AddRange(items);
			cbPlayer.SelectedItem = ((cbPlayer.Items.Count > 0) ? cbPlayer.Items[0] : null);
		}
	}

	private void btnGrabCosm_Click(object sender, EventArgs e)
	{
		if (cbPlayer.SelectedIndex <= -1)
		{
			return;
		}
		try
		{
			try
			{
				if (lbItems.Items[lbItems.Items.Count - 1].ToString() != " ")
				{
					lbItems.Items.Add(" ");
				}
			}
			catch
			{
			}
			ListBox.ObjectCollection ıtems = lbItems.Items;
			object[] items = CosmeticEquipment.Get(((PlayerInfo)cbPlayer.SelectedItem).EntID).ToArray();
			ıtems.AddRange(items);
			lbItems.Items.Add(" ");
		}
		catch
		{
		}
	}

	private void btnCopyAll_Click(object sender, EventArgs e)
	{
		lbItems.Items.Cast<CosmeticEquipment>().ForEach(delegate(CosmeticEquipment x)
		{
			x.Equip();
		});
	}

	private void btnEquipSelected_Click(object sender, EventArgs e)
	{
		try
		{
			lbItems.SelectedItems.Cast<CosmeticEquipment>().ForEach(delegate(CosmeticEquipment x)
			{
				x.Equip();
			});
		}
		catch
		{
		}
	}

	private void lnkGrabTarget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		lbItems.Items.Clear();
		ListBox.ObjectCollection ıtems = lbItems.Items;
		object[] items = CosmeticEquipment.Get(Flash.Instance.GetGameObject("world.myAvatar.target.uid", 0)).ToArray();
		ıtems.AddRange(items);
	}

	private void CosmeticForm_FormClosing(object sender, FormClosingEventArgs e)
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

	private void btnRefresh_Click(object sender, EventArgs e)
	{
		if (Player.IsLoggedIn)
		{
			cbPlayer.Items.Clear();
			World.RefreshDictionary();
			ComboBox.ObjectCollection ıtems = cbPlayer.Items;
			object[] items = World.Players.ToArray();
			ıtems.AddRange(items);
			cbPlayer.SelectedItem = ((cbPlayer.Items.Count > 0) ? cbPlayer.Items[0] : null);
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		lbItems.Items.Clear();
		TextBox[] array = new TextBox[16]
		{
			txtHelm1, txtHelm2, txtArmor1, txtArmor2, txtClass1, txtClass2, txtWeapon1, txtWeapon2, txtPet1, txtPet2,
			txtCape1, txtCape2, txtOff1, txtOff2, txtGround1, txtGround2
		};
		TextBox[] array2 = array;
		foreach (TextBox textBox in array2)
		{
			textBox.Text = "";
		}
	}

	private void linkItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (cbPlayer.SelectedIndex <= -1)
		{
			return;
		}
		try
		{
			string text = ((LinkLabel)sender).Text.Split(' ')[1].Replace("Wep", "Weapon");
			object[] array = CosmeticEquipment.Get(((PlayerInfo)cbPlayer.SelectedItem).EntID).ToArray();
			object[] array2 = array;
			for (int i = 0; array2.Length > i; i++)
			{
				if (array2[i].ToString().StartsWith(text))
				{
					lbItems.Items.Add(array2[i]);
					string[] array3 = array2[i].ToString().Replace(text + ": ", "").Split(';');
					if (text == "Cape" && txtCape1.Text != (txtCape1.Text.NullIfEmpty() ?? array3[0]))
					{
						txtCape1.Text = array3[0];
						txtCape2.Text = array3[1];
					}
					else if (text == "Class" && txtClass1.Text != (txtClass1.Text.NullIfEmpty() ?? array3[0]))
					{
						txtClass1.Text = array3[0];
						txtClass2.Text = array3[1];
					}
					else if (text == "Pet" && txtPet2.Text != (txtPet1.Text.NullIfEmpty() ?? array3[0]))
					{
						txtPet1.Text = array3[0];
						txtPet2.Text = array3[1];
					}
					else if (text == "Weapon" && txtWeapon1.Text != (txtWeapon1.Text.NullIfEmpty() ?? array3[0]))
					{
						txtWeapon1.Text = array3[0];
						txtWeapon2.Text = array3[1];
					}
					else if (text == "Helm" && txtHelm1.Text != (txtHelm1.Text.NullIfEmpty() ?? array3[0]))
					{
						txtHelm1.Text = array3[0];
						txtHelm2.Text = array3[1];
					}
					else if (text == "Armor" && txtArmor1.Text != (txtArmor1.Text.NullIfEmpty() ?? array3[0]))
					{
						txtArmor1.Text = array3[0];
						txtArmor2.Text = array3[1];
					}
					else if (text == "Ground" && txtGround1.Text != (txtGround1.Text.NullIfEmpty() ?? array3[0]))
					{
						txtGround1.Text = array3[0];
						txtGround2.Text = array3[1];
					}
				}
			}
		}
		catch
		{
		}
	}

	private void lbItems_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Delete && lbItems.SelectedItem != null)
		{
			int selectedIndex = lbItems.SelectedIndex;
			if (selectedIndex > -1)
			{
				for (int num = lbItems.SelectedIndices.Count - 1; num >= 0; num--)
				{
					int index = lbItems.SelectedIndices[num];
					lbItems.Items.RemoveAt(index);
				}
				lbItems.EndUpdate();
			}
		}
		if (e.KeyCode != Keys.Return || lbItems.SelectedIndex <= -1)
		{
			return;
		}
		ListBox.SelectedObjectCollection selectedItems = lbItems.SelectedItems;
		for (int i = 0; selectedItems.Count > i; i++)
		{
			string text = selectedItems[i].ToString().Split(':')[0];
			if (!(selectedItems[i].ToString() == " "))
			{
				string[] array = selectedItems[i].ToString().Replace(text + ": ", "").Split(';');
				switch (text)
				{
				case "Cape":
					txtCape1.Text = array[0];
					txtCape2.Text = array[1];
					break;
				case "Pet":
					txtPet1.Text = array[0];
					txtPet2.Text = array[1];
					break;
				case "Class":
					txtClass1.Text = array[0];
					txtClass2.Text = array[1];
					break;
				case "Helm":
					txtHelm1.Text = array[0];
					txtHelm2.Text = array[1];
					break;
				case "Armor":
					txtArmor1.Text = array[0];
					txtArmor2.Text = array[1];
					break;
				case "Ground":
					txtGround1.Text = array[0];
					txtGround2.Text = array[1];
					break;
				default:
					txtWeapon1.Text = array[0];
					txtWeapon2.Text = array[1];
					break;
				}
			}
		}
	}

	private void btnSet_Click(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		string text;
		string text2;
		string str;
		switch (button.Name.Replace("btn", "").Replace("Set", ""))
		{
		case "Cape":
			text = "ba";
			text2 = txtCape1.Text;
			str = txtCape2.Text;
			break;
		case "Class":
			text = "ar";
			text2 = txtClass1.Text;
			str = txtClass2.Text;
			break;
		case "Pet":
			text = "pe";
			text2 = txtPet1.Text;
			str = txtPet2.Text;
			break;
		case "Helm":
			text = "he";
			text2 = txtHelm1.Text;
			str = txtHelm2.Text;
			break;
		case "Armor":
			text = "co";
			text2 = txtArmor1.Text;
			str = txtArmor2.Text;
			break;
		case "Offhand":
			text = "Off";
			text2 = txtOff1.Text;
			str = txtOff2.Text;
			break;
		case "Ground":
			text = "mi";
			text2 = txtGround1.Text;
			str = txtGround2.Text;
			break;
		default:
			text = "Weapon";
			text2 = txtWeapon1.Text;
			str = txtWeapon2.Text;
			break;
		}
		dynamic val = new ExpandoObject();
		val.sFile = text2;
		val.sLink = str.ReplaceLink();
		val.sType = text;
		Flash.Call("SetEquip", new object[2] { text, val });
	}

	private void btnSaveSet_Click(object sender, EventArgs e)
	{
		string[] contents = new string[24]
		{
			"Helmet:",
			"he file:" + (txtHelm1.Text ?? "None"),
			"he link:" + (txtHelm2.Text ?? "None"),
			"\r\nArmor:",
			"co file:" + (txtArmor1.Text ?? "None"),
			"co link:" + (txtArmor2.Text ?? "None"),
			"\r\nClass:",
			"ar file:" + (txtClass1.Text ?? "None"),
			"ar link:" + (txtClass2.Text ?? "None") + "\r\n",
			"\r\nWeapon:",
			"Weapon file:" + (txtWeapon1.Text ?? "None"),
			"Weapon link:" + (txtWeapon2.Text ?? "None") + "\r\n",
			"\r\nPet:",
			"pe file:" + (txtPet1.Text ?? "None"),
			"pe link:" + (txtPet2.Text ?? "None"),
			"\r\nCape:",
			"ba file:" + (txtCape1.Text ?? "None"),
			"ba link:" + (txtCape2.Text ?? "None"),
			"\r\nOff:",
			"off file:" + (txtOff1.Text ?? "None"),
			"off link:" + (txtOff2.Text ?? "None"),
			"\r\nGround:",
			"mi file:" + (txtGround1.Text ?? "None"),
			"mi link:" + (txtGround2.Text ?? "None")
		};
		SaveFileDialog saveFileDialog = new SaveFileDialog
		{
			InitialDirectory = Application.StartupPath + "\\Sets",
			Filter = "Grimoire sets|*.gset"
		};
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			File.WriteAllLines(saveFileDialog.FileName, contents);
		}
	}

	private void btnLoadSet_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog
		{
			InitialDirectory = Application.StartupPath + "\\Sets",
			Filter = "Grimoire sets|*.gset"
		};
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			string[] array = (from empty in File.ReadAllLines(openFileDialog.FileName)
				where empty.Trim() != string.Empty
				select empty).ToArray();
			try
			{
				txtHelm1.Text = array[1].Split(':')[1];
				txtHelm2.Text = array[2].Split(':')[1];
				txtArmor1.Text = array[4].Split(':')[1];
				txtArmor2.Text = array[5].Split(':')[1];
				txtClass1.Text = array[7].Split(':')[1];
				txtClass2.Text = array[8].Split(':')[1];
				txtWeapon1.Text = array[10].Split(':')[1];
				txtWeapon2.Text = array[11].Split(':')[1];
				txtPet1.Text = array[13].Split(':')[1];
				txtPet2.Text = array[14].Split(':')[1];
				txtCape1.Text = array[16].Split(':')[1];
				txtCape2.Text = array[17].Split(':')[1];
				txtOff1.Text = array[19].Split(':')[1];
				txtOff2.Text = array[20].Split(':')[1];
				txtGround1.Text = array[22].Split(':')[1];
				txtGround2.Text = array[23].Split(':')[1];
			}
			catch
			{
			}
		}
	}

	private void lbItems_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (!(lbItems.SelectedItem.ToString() == " "))
		{
			string text = lbItems.SelectedItem.ToString().Split(':')[0];
			string[] array = lbItems.SelectedItem.ToString().Replace(text + ": ", "").Split(';');
			switch (text)
			{
			case "Cape":
				txtCape1.Text = array[0];
				txtCape2.Text = array[1];
				break;
			case "Pet":
				txtPet1.Text = array[0];
				txtPet2.Text = array[1];
				break;
			case "Class":
				txtClass1.Text = array[0];
				txtClass2.Text = array[1];
				break;
			case "Helm":
				txtHelm1.Text = array[0];
				txtHelm2.Text = array[1];
				break;
			case "Armor":
				txtArmor1.Text = array[0];
				txtArmor2.Text = array[1];
				break;
			case "Ground":
				txtGround1.Text = array[0];
				txtGround2.Text = array[1];
				break;
			default:
				txtWeapon1.Text = array[0];
				txtWeapon2.Text = array[1];
				break;
			}
		}
	}

	private void btnRemove_Click(object sender, EventArgs e)
	{
		try
		{
			ListBox.SelectedIndexCollection selectedIndices = lbItems.SelectedIndices;
			for (int num = selectedIndices.Count - 1; num >= 0; num--)
			{
				int index = selectedIndices[num];
				lbItems.Items.RemoveAt(index);
			}
		}
		catch
		{
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
		this.components = new System.ComponentModel.Container();
		this.cbPlayer = new DarkUI.Controls.DarkComboBox();
		this.btnGrabCosm = new DarkUI.Controls.DarkButton();
		this.lbItems = new DarkUI.Controls.DarkListBox(this.components);
		this.btnCopyAll = new DarkUI.Controls.DarkButton();
		this.btnEquipSelected = new DarkUI.Controls.DarkButton();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.btnClear = new DarkUI.Controls.DarkButton();
		this.btnRemove = new DarkUI.Controls.DarkButton();
		this.linkHelm = new System.Windows.Forms.LinkLabel();
		this.linkArmor = new System.Windows.Forms.LinkLabel();
		this.linkWeapon = new System.Windows.Forms.LinkLabel();
		this.linkPet = new System.Windows.Forms.LinkLabel();
		this.linkCape = new System.Windows.Forms.LinkLabel();
		this.btnRefresh = new DarkUI.Controls.DarkButton();
		this.linkClass = new System.Windows.Forms.LinkLabel();
		this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.linkGround = new System.Windows.Forms.LinkLabel();
		this.label1 = new DarkUI.Controls.DarkLabel();
		this.label2 = new DarkUI.Controls.DarkLabel();
		this.label3 = new DarkUI.Controls.DarkLabel();
		this.label4 = new DarkUI.Controls.DarkLabel();
		this.label5 = new DarkUI.Controls.DarkLabel();
		this.label6 = new DarkUI.Controls.DarkLabel();
		this.txtArmor1 = new DarkUI.Controls.DarkTextBox();
		this.txtHelm1 = new DarkUI.Controls.DarkTextBox();
		this.txtWeapon1 = new DarkUI.Controls.DarkTextBox();
		this.txtClass1 = new DarkUI.Controls.DarkTextBox();
		this.txtCape1 = new DarkUI.Controls.DarkTextBox();
		this.txtPet1 = new DarkUI.Controls.DarkTextBox();
		this.btnHelmSet = new DarkUI.Controls.DarkButton();
		this.btnArmorSet = new DarkUI.Controls.DarkButton();
		this.btnClassSet = new DarkUI.Controls.DarkButton();
		this.btnWeaponSet = new DarkUI.Controls.DarkButton();
		this.btnPetSet = new DarkUI.Controls.DarkButton();
		this.btnCapeSet = new DarkUI.Controls.DarkButton();
		this.txtHelm2 = new DarkUI.Controls.DarkTextBox();
		this.txtArmor2 = new DarkUI.Controls.DarkTextBox();
		this.txtClass2 = new DarkUI.Controls.DarkTextBox();
		this.txtWeapon2 = new DarkUI.Controls.DarkTextBox();
		this.txtPet2 = new DarkUI.Controls.DarkTextBox();
		this.txtCape2 = new DarkUI.Controls.DarkTextBox();
		this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
		this.darkLabel1 = new DarkUI.Controls.DarkLabel();
		this.txtOff2 = new DarkUI.Controls.DarkTextBox();
		this.txtOff1 = new DarkUI.Controls.DarkTextBox();
		this.btnSetOffhand = new DarkUI.Controls.DarkButton();
		this.label7 = new DarkUI.Controls.DarkLabel();
		this.btnGroundSet = new DarkUI.Controls.DarkButton();
		this.txtGround2 = new DarkUI.Controls.DarkTextBox();
		this.txtGround1 = new DarkUI.Controls.DarkTextBox();
		this.btnSaveSet = new DarkUI.Controls.DarkButton();
		this.btnLoadSet = new DarkUI.Controls.DarkButton();
		this.tableLayoutPanel1.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		this.tableLayoutPanel3.SuspendLayout();
		base.SuspendLayout();
		this.cbPlayer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cbPlayer.FormattingEnabled = true;
		this.cbPlayer.Location = new System.Drawing.Point(42, 14);
		this.cbPlayer.Name = "cbPlayer";
		this.cbPlayer.Size = new System.Drawing.Size(266, 21);
		this.cbPlayer.TabIndex = 0;
		this.btnGrabCosm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGrabCosm.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnGrabCosm.BackColorUseGeneric = false;
		this.btnGrabCosm.Checked = false;
		this.btnGrabCosm.Location = new System.Drawing.Point(310, 14);
		this.btnGrabCosm.Name = "btnGrabCosm";
		this.btnGrabCosm.Size = new System.Drawing.Size(94, 21);
		this.btnGrabCosm.TabIndex = 1;
		this.btnGrabCosm.Text = "Grab";
		this.btnGrabCosm.Click += new System.EventHandler(btnGrabCosm_Click);
		this.lbItems.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lbItems.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		this.lbItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.lbItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.lbItems.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.lbItems.FormattingEnabled = true;
		this.lbItems.ItemHeight = 18;
		this.lbItems.Location = new System.Drawing.Point(12, 266);
		this.lbItems.Name = "lbItems";
		this.lbItems.Size = new System.Drawing.Size(512, 182);
		this.lbItems.TabIndex = 2;
		this.lbItems.KeyDown += new System.Windows.Forms.KeyEventHandler(lbItems_KeyDown);
		this.lbItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(lbItems_MouseDoubleClick);
		this.btnCopyAll.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnCopyAll.BackColorUseGeneric = false;
		this.btnCopyAll.Checked = false;
		this.btnCopyAll.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnCopyAll.Location = new System.Drawing.Point(3, 3);
		this.btnCopyAll.Name = "btnCopyAll";
		this.btnCopyAll.Size = new System.Drawing.Size(122, 23);
		this.btnCopyAll.TabIndex = 4;
		this.btnCopyAll.Text = "Equip All";
		this.btnCopyAll.Click += new System.EventHandler(btnCopyAll_Click);
		this.btnEquipSelected.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnEquipSelected.BackColorUseGeneric = false;
		this.btnEquipSelected.Checked = false;
		this.btnEquipSelected.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnEquipSelected.Location = new System.Drawing.Point(131, 3);
		this.btnEquipSelected.Name = "btnEquipSelected";
		this.btnEquipSelected.Size = new System.Drawing.Size(122, 23);
		this.btnEquipSelected.TabIndex = 5;
		this.btnEquipSelected.Text = "Equip Selected";
		this.btnEquipSelected.Click += new System.EventHandler(btnEquipSelected_Click);
		this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tableLayoutPanel1.ColumnCount = 4;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.tableLayoutPanel1.Controls.Add(this.btnCopyAll, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnEquipSelected, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnClear, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnRemove, 3, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 448);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(512, 29);
		this.tableLayoutPanel1.TabIndex = 7;
		this.btnClear.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnClear.BackColorUseGeneric = false;
		this.btnClear.Checked = false;
		this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnClear.Location = new System.Drawing.Point(259, 3);
		this.btnClear.Name = "btnClear";
		this.btnClear.Size = new System.Drawing.Size(122, 23);
		this.btnClear.TabIndex = 6;
		this.btnClear.Text = "Clear All";
		this.btnClear.Click += new System.EventHandler(btnClear_Click);
		this.btnRemove.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnRemove.BackColorUseGeneric = false;
		this.btnRemove.Checked = false;
		this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnRemove.Location = new System.Drawing.Point(387, 3);
		this.btnRemove.Name = "btnRemove";
		this.btnRemove.Size = new System.Drawing.Size(122, 23);
		this.btnRemove.TabIndex = 6;
		this.btnRemove.Text = "Remove";
		this.btnRemove.Click += new System.EventHandler(btnRemove_Click);
		this.linkHelm.AutoSize = true;
		this.linkHelm.Dock = System.Windows.Forms.DockStyle.Fill;
		this.linkHelm.LinkColor = System.Drawing.Color.Gainsboro;
		this.linkHelm.Location = new System.Drawing.Point(3, 0);
		this.linkHelm.Name = "linkHelm";
		this.linkHelm.Size = new System.Drawing.Size(66, 14);
		this.linkHelm.TabIndex = 8;
		this.linkHelm.TabStop = true;
		this.linkHelm.Text = "Grab Helm";
		this.linkHelm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.linkHelm.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkItems_LinkClicked);
		this.linkArmor.AutoSize = true;
		this.linkArmor.Dock = System.Windows.Forms.DockStyle.Fill;
		this.linkArmor.LinkColor = System.Drawing.Color.Gainsboro;
		this.linkArmor.Location = new System.Drawing.Point(75, 0);
		this.linkArmor.Name = "linkArmor";
		this.linkArmor.Size = new System.Drawing.Size(66, 14);
		this.linkArmor.TabIndex = 8;
		this.linkArmor.TabStop = true;
		this.linkArmor.Text = "Grab Armor";
		this.linkArmor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.linkArmor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkItems_LinkClicked);
		this.linkWeapon.AutoSize = true;
		this.linkWeapon.Dock = System.Windows.Forms.DockStyle.Fill;
		this.linkWeapon.LinkColor = System.Drawing.Color.Gainsboro;
		this.linkWeapon.Location = new System.Drawing.Point(219, 0);
		this.linkWeapon.Name = "linkWeapon";
		this.linkWeapon.Size = new System.Drawing.Size(66, 14);
		this.linkWeapon.TabIndex = 8;
		this.linkWeapon.TabStop = true;
		this.linkWeapon.Text = "Grab Wep";
		this.linkWeapon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.linkWeapon.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkItems_LinkClicked);
		this.linkPet.AutoSize = true;
		this.linkPet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.linkPet.LinkColor = System.Drawing.Color.Gainsboro;
		this.linkPet.Location = new System.Drawing.Point(291, 0);
		this.linkPet.Name = "linkPet";
		this.linkPet.Size = new System.Drawing.Size(66, 14);
		this.linkPet.TabIndex = 8;
		this.linkPet.TabStop = true;
		this.linkPet.Text = "Grab Pet";
		this.linkPet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.linkPet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkItems_LinkClicked);
		this.linkCape.AutoSize = true;
		this.linkCape.Dock = System.Windows.Forms.DockStyle.Fill;
		this.linkCape.LinkColor = System.Drawing.Color.Gainsboro;
		this.linkCape.Location = new System.Drawing.Point(363, 0);
		this.linkCape.Name = "linkCape";
		this.linkCape.Size = new System.Drawing.Size(66, 14);
		this.linkCape.TabIndex = 8;
		this.linkCape.TabStop = true;
		this.linkCape.Text = "Grab Cape";
		this.linkCape.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.linkCape.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkItems_LinkClicked);
		this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnRefresh.BackColorUseGeneric = false;
		this.btnRefresh.Checked = false;
		this.btnRefresh.Location = new System.Drawing.Point(12, 14);
		this.btnRefresh.Name = "btnRefresh";
		this.btnRefresh.Size = new System.Drawing.Size(28, 21);
		this.btnRefresh.TabIndex = 9;
		this.btnRefresh.Text = "⟳";
		this.btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
		this.linkClass.AutoSize = true;
		this.linkClass.Dock = System.Windows.Forms.DockStyle.Fill;
		this.linkClass.LinkColor = System.Drawing.Color.Gainsboro;
		this.linkClass.Location = new System.Drawing.Point(147, 0);
		this.linkClass.Name = "linkClass";
		this.linkClass.Size = new System.Drawing.Size(66, 14);
		this.linkClass.TabIndex = 8;
		this.linkClass.TabStop = true;
		this.linkClass.Text = "Grab Class";
		this.linkClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.linkClass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkItems_LinkClicked);
		this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tableLayoutPanel2.ColumnCount = 7;
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
		this.tableLayoutPanel2.Controls.Add(this.linkHelm, 0, 0);
		this.tableLayoutPanel2.Controls.Add(this.linkArmor, 1, 0);
		this.tableLayoutPanel2.Controls.Add(this.linkCape, 5, 0);
		this.tableLayoutPanel2.Controls.Add(this.linkClass, 2, 0);
		this.tableLayoutPanel2.Controls.Add(this.linkPet, 4, 0);
		this.tableLayoutPanel2.Controls.Add(this.linkWeapon, 3, 0);
		this.tableLayoutPanel2.Controls.Add(this.linkGround, 6, 0);
		this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 41);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.RowCount = 1;
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel2.Size = new System.Drawing.Size(508, 14);
		this.tableLayoutPanel2.TabIndex = 10;
		this.linkGround.AutoSize = true;
		this.linkGround.Dock = System.Windows.Forms.DockStyle.Fill;
		this.linkGround.LinkColor = System.Drawing.Color.Gainsboro;
		this.linkGround.Location = new System.Drawing.Point(435, 0);
		this.linkGround.Name = "linkGround";
		this.linkGround.Size = new System.Drawing.Size(70, 14);
		this.linkGround.TabIndex = 9;
		this.linkGround.TabStop = true;
		this.linkGround.Text = "Grab Ground";
		this.linkGround.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.linkGround.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkItems_LinkClicked);
		this.label1.AutoSize = true;
		this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.label1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.label1.Location = new System.Drawing.Point(3, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(52, 24);
		this.label1.TabIndex = 11;
		this.label1.Text = "Helm";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label2.AutoSize = true;
		this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.label2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.label2.Location = new System.Drawing.Point(3, 24);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(52, 24);
		this.label2.TabIndex = 11;
		this.label2.Text = "Armor";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label3.AutoSize = true;
		this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.label3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.label3.Location = new System.Drawing.Point(3, 48);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(52, 24);
		this.label3.TabIndex = 11;
		this.label3.Text = "Class";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label4.AutoSize = true;
		this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.label4.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.label4.Location = new System.Drawing.Point(3, 72);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(52, 24);
		this.label4.TabIndex = 11;
		this.label4.Text = "Weapon";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label5.AutoSize = true;
		this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.label5.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.label5.Location = new System.Drawing.Point(3, 120);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(52, 24);
		this.label5.TabIndex = 11;
		this.label5.Text = "Pet";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label6.AutoSize = true;
		this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.label6.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.label6.Location = new System.Drawing.Point(3, 144);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(52, 24);
		this.label6.TabIndex = 11;
		this.label6.Text = "Cape";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtArmor1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtArmor1.Location = new System.Drawing.Point(60, 26);
		this.txtArmor1.Margin = new System.Windows.Forms.Padding(2);
		this.txtArmor1.Name = "txtArmor1";
		this.txtArmor1.Size = new System.Drawing.Size(238, 20);
		this.txtArmor1.TabIndex = 12;
		this.txtHelm1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtHelm1.Location = new System.Drawing.Point(60, 2);
		this.txtHelm1.Margin = new System.Windows.Forms.Padding(2);
		this.txtHelm1.Name = "txtHelm1";
		this.txtHelm1.Size = new System.Drawing.Size(238, 20);
		this.txtHelm1.TabIndex = 12;
		this.txtWeapon1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtWeapon1.Location = new System.Drawing.Point(60, 74);
		this.txtWeapon1.Margin = new System.Windows.Forms.Padding(2);
		this.txtWeapon1.Name = "txtWeapon1";
		this.txtWeapon1.Size = new System.Drawing.Size(238, 20);
		this.txtWeapon1.TabIndex = 12;
		this.txtClass1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtClass1.Location = new System.Drawing.Point(60, 50);
		this.txtClass1.Margin = new System.Windows.Forms.Padding(2);
		this.txtClass1.Name = "txtClass1";
		this.txtClass1.Size = new System.Drawing.Size(238, 20);
		this.txtClass1.TabIndex = 12;
		this.txtCape1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtCape1.Location = new System.Drawing.Point(60, 146);
		this.txtCape1.Margin = new System.Windows.Forms.Padding(2);
		this.txtCape1.Name = "txtCape1";
		this.txtCape1.Size = new System.Drawing.Size(238, 20);
		this.txtCape1.TabIndex = 12;
		this.txtPet1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtPet1.Location = new System.Drawing.Point(60, 122);
		this.txtPet1.Margin = new System.Windows.Forms.Padding(2);
		this.txtPet1.Name = "txtPet1";
		this.txtPet1.Size = new System.Drawing.Size(238, 20);
		this.txtPet1.TabIndex = 12;
		this.btnHelmSet.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnHelmSet.BackColorUseGeneric = false;
		this.btnHelmSet.Checked = false;
		this.btnHelmSet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnHelmSet.Location = new System.Drawing.Point(449, 1);
		this.btnHelmSet.Margin = new System.Windows.Forms.Padding(1);
		this.btnHelmSet.Name = "btnHelmSet";
		this.btnHelmSet.Size = new System.Drawing.Size(62, 22);
		this.btnHelmSet.TabIndex = 1;
		this.btnHelmSet.Text = "Set";
		this.btnHelmSet.Click += new System.EventHandler(btnSet_Click);
		this.btnArmorSet.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnArmorSet.BackColorUseGeneric = false;
		this.btnArmorSet.Checked = false;
		this.btnArmorSet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnArmorSet.Location = new System.Drawing.Point(449, 25);
		this.btnArmorSet.Margin = new System.Windows.Forms.Padding(1);
		this.btnArmorSet.Name = "btnArmorSet";
		this.btnArmorSet.Size = new System.Drawing.Size(62, 22);
		this.btnArmorSet.TabIndex = 1;
		this.btnArmorSet.Text = "Set";
		this.btnArmorSet.Click += new System.EventHandler(btnSet_Click);
		this.btnClassSet.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnClassSet.BackColorUseGeneric = false;
		this.btnClassSet.Checked = false;
		this.btnClassSet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnClassSet.Location = new System.Drawing.Point(449, 49);
		this.btnClassSet.Margin = new System.Windows.Forms.Padding(1);
		this.btnClassSet.Name = "btnClassSet";
		this.btnClassSet.Size = new System.Drawing.Size(62, 22);
		this.btnClassSet.TabIndex = 1;
		this.btnClassSet.Text = "Set";
		this.btnClassSet.Click += new System.EventHandler(btnSet_Click);
		this.btnWeaponSet.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnWeaponSet.BackColorUseGeneric = false;
		this.btnWeaponSet.Checked = false;
		this.btnWeaponSet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnWeaponSet.Location = new System.Drawing.Point(449, 73);
		this.btnWeaponSet.Margin = new System.Windows.Forms.Padding(1);
		this.btnWeaponSet.Name = "btnWeaponSet";
		this.btnWeaponSet.Size = new System.Drawing.Size(62, 22);
		this.btnWeaponSet.TabIndex = 1;
		this.btnWeaponSet.Text = "Set";
		this.btnWeaponSet.Click += new System.EventHandler(btnSet_Click);
		this.btnPetSet.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnPetSet.BackColorUseGeneric = false;
		this.btnPetSet.Checked = false;
		this.btnPetSet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnPetSet.Location = new System.Drawing.Point(449, 121);
		this.btnPetSet.Margin = new System.Windows.Forms.Padding(1);
		this.btnPetSet.Name = "btnPetSet";
		this.btnPetSet.Size = new System.Drawing.Size(62, 22);
		this.btnPetSet.TabIndex = 1;
		this.btnPetSet.Text = "Set";
		this.btnPetSet.Click += new System.EventHandler(btnSet_Click);
		this.btnCapeSet.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnCapeSet.BackColorUseGeneric = false;
		this.btnCapeSet.Checked = false;
		this.btnCapeSet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnCapeSet.Location = new System.Drawing.Point(449, 145);
		this.btnCapeSet.Margin = new System.Windows.Forms.Padding(1);
		this.btnCapeSet.Name = "btnCapeSet";
		this.btnCapeSet.Size = new System.Drawing.Size(62, 22);
		this.btnCapeSet.TabIndex = 1;
		this.btnCapeSet.Text = "Set";
		this.btnCapeSet.Click += new System.EventHandler(btnSet_Click);
		this.txtHelm2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtHelm2.Location = new System.Drawing.Point(302, 2);
		this.txtHelm2.Margin = new System.Windows.Forms.Padding(2);
		this.txtHelm2.Name = "txtHelm2";
		this.txtHelm2.Size = new System.Drawing.Size(144, 20);
		this.txtHelm2.TabIndex = 13;
		this.txtArmor2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtArmor2.Location = new System.Drawing.Point(302, 26);
		this.txtArmor2.Margin = new System.Windows.Forms.Padding(2);
		this.txtArmor2.Name = "txtArmor2";
		this.txtArmor2.Size = new System.Drawing.Size(144, 20);
		this.txtArmor2.TabIndex = 13;
		this.txtClass2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtClass2.Location = new System.Drawing.Point(302, 50);
		this.txtClass2.Margin = new System.Windows.Forms.Padding(2);
		this.txtClass2.Name = "txtClass2";
		this.txtClass2.Size = new System.Drawing.Size(144, 20);
		this.txtClass2.TabIndex = 13;
		this.txtWeapon2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtWeapon2.Location = new System.Drawing.Point(302, 74);
		this.txtWeapon2.Margin = new System.Windows.Forms.Padding(2);
		this.txtWeapon2.Name = "txtWeapon2";
		this.txtWeapon2.Size = new System.Drawing.Size(144, 20);
		this.txtWeapon2.TabIndex = 13;
		this.txtPet2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtPet2.Location = new System.Drawing.Point(302, 122);
		this.txtPet2.Margin = new System.Windows.Forms.Padding(2);
		this.txtPet2.Name = "txtPet2";
		this.txtPet2.Size = new System.Drawing.Size(144, 20);
		this.txtPet2.TabIndex = 13;
		this.txtCape2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtCape2.Location = new System.Drawing.Point(302, 146);
		this.txtCape2.Margin = new System.Windows.Forms.Padding(2);
		this.txtCape2.Name = "txtCape2";
		this.txtCape2.Size = new System.Drawing.Size(144, 20);
		this.txtCape2.TabIndex = 13;
		this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tableLayoutPanel3.ColumnCount = 4;
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58f));
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.06897f));
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.93103f));
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63f));
		this.tableLayoutPanel3.Controls.Add(this.darkLabel1, 0, 7);
		this.tableLayoutPanel3.Controls.Add(this.btnCapeSet, 3, 6);
		this.tableLayoutPanel3.Controls.Add(this.label6, 0, 6);
		this.tableLayoutPanel3.Controls.Add(this.txtCape1, 1, 6);
		this.tableLayoutPanel3.Controls.Add(this.label5, 0, 5);
		this.tableLayoutPanel3.Controls.Add(this.txtPet1, 1, 5);
		this.tableLayoutPanel3.Controls.Add(this.txtCape2, 2, 6);
		this.tableLayoutPanel3.Controls.Add(this.label3, 0, 2);
		this.tableLayoutPanel3.Controls.Add(this.txtClass1, 1, 2);
		this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
		this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel3.Controls.Add(this.btnPetSet, 3, 5);
		this.tableLayoutPanel3.Controls.Add(this.txtPet2, 2, 5);
		this.tableLayoutPanel3.Controls.Add(this.txtClass2, 2, 2);
		this.tableLayoutPanel3.Controls.Add(this.txtArmor1, 1, 1);
		this.tableLayoutPanel3.Controls.Add(this.txtHelm1, 1, 0);
		this.tableLayoutPanel3.Controls.Add(this.btnClassSet, 3, 2);
		this.tableLayoutPanel3.Controls.Add(this.txtArmor2, 2, 1);
		this.tableLayoutPanel3.Controls.Add(this.btnArmorSet, 3, 1);
		this.tableLayoutPanel3.Controls.Add(this.btnHelmSet, 3, 0);
		this.tableLayoutPanel3.Controls.Add(this.txtHelm2, 2, 0);
		this.tableLayoutPanel3.Controls.Add(this.label4, 0, 3);
		this.tableLayoutPanel3.Controls.Add(this.txtWeapon1, 1, 3);
		this.tableLayoutPanel3.Controls.Add(this.txtWeapon2, 2, 3);
		this.tableLayoutPanel3.Controls.Add(this.btnWeaponSet, 3, 3);
		this.tableLayoutPanel3.Controls.Add(this.txtOff2, 2, 4);
		this.tableLayoutPanel3.Controls.Add(this.txtOff1, 1, 4);
		this.tableLayoutPanel3.Controls.Add(this.btnSetOffhand, 3, 4);
		this.tableLayoutPanel3.Controls.Add(this.label7, 0, 4);
		this.tableLayoutPanel3.Controls.Add(this.btnGroundSet, 3, 7);
		this.tableLayoutPanel3.Controls.Add(this.txtGround2, 2, 7);
		this.tableLayoutPanel3.Controls.Add(this.txtGround1, 1, 7);
		this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 67);
		this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(1);
		this.tableLayoutPanel3.Name = "tableLayoutPanel3";
		this.tableLayoutPanel3.RowCount = 8;
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.tableLayoutPanel3.Size = new System.Drawing.Size(512, 193);
		this.tableLayoutPanel3.TabIndex = 14;
		this.darkLabel1.AutoSize = true;
		this.darkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.darkLabel1.Location = new System.Drawing.Point(3, 168);
		this.darkLabel1.Name = "darkLabel1";
		this.darkLabel1.Size = new System.Drawing.Size(52, 25);
		this.darkLabel1.TabIndex = 17;
		this.darkLabel1.Text = "Ground";
		this.darkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtOff2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtOff2.Location = new System.Drawing.Point(302, 98);
		this.txtOff2.Margin = new System.Windows.Forms.Padding(2);
		this.txtOff2.Name = "txtOff2";
		this.txtOff2.Size = new System.Drawing.Size(144, 20);
		this.txtOff2.TabIndex = 13;
		this.txtOff1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtOff1.Location = new System.Drawing.Point(60, 98);
		this.txtOff1.Margin = new System.Windows.Forms.Padding(2);
		this.txtOff1.Name = "txtOff1";
		this.txtOff1.Size = new System.Drawing.Size(238, 20);
		this.txtOff1.TabIndex = 12;
		this.btnSetOffhand.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnSetOffhand.BackColorUseGeneric = false;
		this.btnSetOffhand.Checked = false;
		this.btnSetOffhand.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnSetOffhand.Location = new System.Drawing.Point(449, 97);
		this.btnSetOffhand.Margin = new System.Windows.Forms.Padding(1);
		this.btnSetOffhand.Name = "btnSetOffhand";
		this.btnSetOffhand.Size = new System.Drawing.Size(62, 22);
		this.btnSetOffhand.TabIndex = 1;
		this.btnSetOffhand.Text = "Set";
		this.btnSetOffhand.Click += new System.EventHandler(btnSet_Click);
		this.label7.AutoSize = true;
		this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.label7.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.label7.Location = new System.Drawing.Point(3, 96);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(52, 24);
		this.label7.TabIndex = 11;
		this.label7.Text = "Offhand";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnGroundSet.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnGroundSet.BackColorUseGeneric = false;
		this.btnGroundSet.Checked = false;
		this.btnGroundSet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnGroundSet.Location = new System.Drawing.Point(449, 169);
		this.btnGroundSet.Margin = new System.Windows.Forms.Padding(1);
		this.btnGroundSet.Name = "btnGroundSet";
		this.btnGroundSet.Size = new System.Drawing.Size(62, 23);
		this.btnGroundSet.TabIndex = 14;
		this.btnGroundSet.Text = "Set";
		this.btnGroundSet.Click += new System.EventHandler(btnSet_Click);
		this.txtGround2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtGround2.Location = new System.Drawing.Point(302, 170);
		this.txtGround2.Margin = new System.Windows.Forms.Padding(2);
		this.txtGround2.Name = "txtGround2";
		this.txtGround2.Size = new System.Drawing.Size(144, 20);
		this.txtGround2.TabIndex = 15;
		this.txtGround1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtGround1.Location = new System.Drawing.Point(60, 170);
		this.txtGround1.Margin = new System.Windows.Forms.Padding(2);
		this.txtGround1.Name = "txtGround1";
		this.txtGround1.Size = new System.Drawing.Size(238, 20);
		this.txtGround1.TabIndex = 16;
		this.btnSaveSet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSaveSet.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnSaveSet.BackColorUseGeneric = false;
		this.btnSaveSet.Checked = false;
		this.btnSaveSet.Location = new System.Drawing.Point(405, 14);
		this.btnSaveSet.Name = "btnSaveSet";
		this.btnSaveSet.Size = new System.Drawing.Size(59, 21);
		this.btnSaveSet.TabIndex = 15;
		this.btnSaveSet.Text = "Save";
		this.btnSaveSet.Click += new System.EventHandler(btnSaveSet_Click);
		this.btnLoadSet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnLoadSet.BackColor = System.Drawing.Color.FromArgb(45, 51, 66);
		this.btnLoadSet.BackColorUseGeneric = false;
		this.btnLoadSet.Checked = false;
		this.btnLoadSet.Location = new System.Drawing.Point(465, 14);
		this.btnLoadSet.Name = "btnLoadSet";
		this.btnLoadSet.Size = new System.Drawing.Size(59, 21);
		this.btnLoadSet.TabIndex = 15;
		this.btnLoadSet.Text = "Load";
		this.btnLoadSet.Click += new System.EventHandler(btnLoadSet_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(28, 32, 40);
		base.ClientSize = new System.Drawing.Size(536, 484);
		base.Controls.Add(this.btnLoadSet);
		base.Controls.Add(this.btnSaveSet);
		base.Controls.Add(this.tableLayoutPanel3);
		base.Controls.Add(this.tableLayoutPanel2);
		base.Controls.Add(this.btnRefresh);
		base.Controls.Add(this.tableLayoutPanel1);
		base.Controls.Add(this.lbItems);
		base.Controls.Add(this.btnGrabCosm);
		base.Controls.Add(this.cbPlayer);
		base.Icon = Grimoire.Properties.Resources.GrimoireIcon;
		base.Name = "CosmeticForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "SWF Cosmetics";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(CosmeticForm_FormClosing);
		base.Shown += new System.EventHandler(CosmeticForm_Shown);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel2.ResumeLayout(false);
		this.tableLayoutPanel2.PerformLayout();
		this.tableLayoutPanel3.ResumeLayout(false);
		this.tableLayoutPanel3.PerformLayout();
		base.ResumeLayout(false);
	}
}
