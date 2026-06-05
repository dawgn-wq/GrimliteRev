using System;
using System.Collections.Generic;
using System.Linq;
using Grimoire.Botting;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Game
{
    public static class Player
    {
        public enum State
        {
            Dead,
            Idle,
            InCombat
        }

        // =================================================================
        // TEMİZLENEN ÖZELLİKLER (POSTSHARP KALINTILARINDAN ARINDIRILMIŞ)
        // =================================================================
        public static int ActivationFlag { get; set; }
        public static bool HasDeathAd { get; set; }
        public static bool targetOnSelf { get; set; }
        public static int walkSpeed { get; set; }
        public static int levelCap { get; set; }

        // =================================================================
        // STANDART ÖZELLİKLER (FLASH ÇAĞRILARI)
        // =================================================================
        public static int UserID => Flash.Call<int>("UserID", new object[0]);

        public static int CharUserID { get; set; }

        public static Bank Bank { get; }

        public static Inventory Inventory { get; }

        public static TempInventory TempInventory { get; }

        public static House House { get; }

        public static List<Faction> Factions => JsonConvert.DeserializeObject<List<Faction>>(Flash.Call("GetFactions", new object[0]));

        public static Quests Quests { get; }

        public static string Username { get; set; }

        private static string Password => Flash.Call<string>("Password", new string[0]);

        public static bool IsLoggedIn { get; set; }

        public static string Cell => Flash.Call<string>("Cell", new string[0]);

        public static string Pad => Flash.Call<string>("Pad", new string[0]);

        public static State CurrentState => (State)Flash.Call<int>("State", new string[0]);

        public static int Health => Flash.Call<int>("Health", new string[0]);

        public static int HealthMax => Flash.Call<int>("HealthMax", new string[0]);

        public static bool IsAlive => Health > 0;

        public static int Mana => Flash.Call<int>("Mana", new string[0]);

        public static int ManaMax => Flash.Call<int>("ManaMax", new string[0]);

        public static string Map => Grimoire.Game.Data.Map.Area.Split('-')[0];

        public static int Level => Flash.Call<int>("Level", new string[0]);

        public static int Gold => Flash.Call<int>("Gold", new string[0]);

        public static bool HasTarget => Flash.Call<bool>("HasTarget", new string[0]);

        public static int AllSkillsAvailable => Flash.Call<int>("AllSkillsAvailable", new string[0]);

        public static bool IsAfk => Flash.Call<bool>("IsAfk", new string[0]);

        public static float[] Position => Flash.Call<float[]>("Position", new string[0]);

        public static bool IsMember => Flash.Call<bool>("IsMember", new string[0]);

        public static string Class => Flash.Call<string>("Class", new string[0]);

        // =================================================================
        // STANDART METOTLAR (BOT İŞLEVLERİ)
        // =================================================================
        public static int SkillAvailable(string index)
        {
            return Flash.Call<int>("SkillAvailable", new string[1] { index });
        }

        public static bool IsSkillAvailable(string skillIndex)
        {
            return Flash.Call<bool>("CheckSkillAvailability", new string[1] { skillIndex });
        }

        public static void ToggleMute()
        {
            Flash.Call("MuteToggle");
        }

        public static void ChangeAccessLevel(string level)
        {
            Flash.Call("ChangeAccessLevel", level);
        }

        public static void WalkToPoint(string x, string y)
        {
            Flash.Call("WalkToPoint", x, y);
        }

        public static void CancelAutoAttack()
        {
            Flash.Call("CancelAutoAttack");
        }

        public static void CancelTarget()
        {
            Flash.Call("CancelTarget");
        }

        public static void CancelTargetSelf()
        {
            Flash.Call("CancelTargetSelf");
        }

        public static void AttackMonster(string name)
        {
            if (!BotUtilities.ShouldUseSkill)
            {
                CancelAutoAttack();
                return;
            }
            Flash.Call("AttackMonster", name);
        }

        public static void SetSpawnPoint()
        {
            Flash.Call("SetSpawnPoint");
        }

        public static async void MoveToCell(string cell = "Enter", string pad = "Spawn")
        {
            Flash.Call("Jump", (cell != "Wait" && cell != "Blank") ? cell : "Enter", pad);
        }

        public static void Rest()
        {
            Flash.Call("Rest");
        }

        public static void JoinMap(string map, string cell, string pad)
        {
            Flash.Call("Join", map, cell, pad);
        }

        public static void Equip(string id)
        {
            Flash.Call("Equip", id);
        }

        public static void Equip(int id)
        {
            Flash.Call("Equip", id.ToString());
        }

        public static void EquipPotion(int id, string desc, string file, string name)
        {
            Flash.Call("EquipPotion", id.ToString(), desc, file, name);
        }

        public static void GoToPlayer(string name)
        {
            Flash.Call("GoTo", name);
        }

        public static bool HasActiveBoost(string name)
        {
            return Flash.Call<bool>("HasActiveBoost", new string[1] { name });
        }

        public static void UseBoost(string id)
        {
            Flash.Call("UseBoost", id);
        }

        public static void UseBoost(int id)
        {
            Flash.Call("UseBoost", id.ToString());
        }

        public static void UseSkill(string index)
        {
            if (!BotUtilities.ShouldUseSkill)
            {
                CancelAutoAttack();
                return;
            }
            Flash.Call("UseSkill", index);
        }

        public static void GetMapItem(string id)
        {
            Flash.Call("GetMapItem", id);
        }

        public static void GetMapItem(int id)
        {
            Flash.Call("GetMapItem", id.ToString());
        }

        public static void Logout()
        {
            Flash.Call("Logout");
        }

        public static void ShowServers()
        {
            Flash.Call("ShowServers");
        }

        public static void LoadMap(string name)
        {
            Flash.Call("LoadMap", name);
        }

        public static void AcceptQuest(int id)
        {
            Flash.Call("Accept", id.ToString());
        }

        public static void CompleteQuest(int id, string itemid = null)
        {
            Flash.Call("Complete", id.ToString(), itemid);
        }

        public static bool HasTargetByID(int monID)
        {
            return Flash.Call<bool>("HasTargetByID", new object[1] { new int[1] { monID } });
        }

        public static void AFKPostpone()
        {
            Flash.Call("afkPostpone");
        }

        public static void EnableChat()
        {
            Flash.Call("EnableChat");
        }

        public static bool HasDrop(string item)
        {
            return World.DropStack.FirstOrDefault((InventoryItem i) => i.Name.Equals(item, StringComparison.OrdinalIgnoreCase)) != null;
        }

        public static void AcceptDrop(int itemID)
        {
            Flash.Call("AcceptDrop", itemID);
        }

        public static bool HasLoadedAvatar()
        {
            return Flash.Call<bool>("HasLoadedAvatar", new string[0]);
        }

        public static bool isAuraActive(string target, string aura)
        {
            return Flash.Call<bool>("isAuraActive", new string[2] { target, aura });
        }

        public static bool isAuraWithStrValActive(string target, string aura, string value)
        {
            return Flash.Call<bool>("isAuraWithStrValActive", new string[3] { target, aura, value });
        }

        public static bool auraComparison(string target, string comparison, string aura, int value)
        {
            return Flash.Call<bool>("auraComparison", new object[4] { target, comparison, aura, value });
        }

        public static void cancelAfk()
        {
            Flash.Call("cancelAfk");
        }

        public static void Unequip(string itemID)
        {
            Flash.Call("Unequip", itemID);
        }

        public static void UnequipPotion(string itemName)
        {
            Flash.Call("UnequipPotion", itemName);
        }

        // =================================================================
        // CONSTRUCTOR (YAPICI METOT)
        // =================================================================
        static Player()
        {
            Bank = new Bank();
            Inventory = new Inventory();
            TempInventory = new TempInventory();
            House = new House();
            Quests = new Quests();
        }
    }
}