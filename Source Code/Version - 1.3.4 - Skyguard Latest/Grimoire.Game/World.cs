using System;
using System.Collections.Generic;
using System.Linq;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using Newtonsoft.Json;

namespace Grimoire.Game
{
    public static class World
    {
        // =================================================================
        // TEMİZLENEN ÖZELLİKLER (POSTSHARP KALINTILARINDAN ARINDIRILMIŞ)
        // =================================================================
        public static List<Monster> Monsters { get; set; }
        public static bool VisibleDropUI { get; set; }
        public static bool uiLock { get; set; }
        public static bool VisibleWorld { get; set; }
        public static int ActiveCutscene { get; set; }
        public static bool FPSBox { get; set; }
        public static bool mapLoadInProgress { get; set; }

        // =================================================================
        // STANDART DEĞİŞKENLER VE ÖZELLİKLER
        // =================================================================
        public static List<Map> Maps;
        public static Map CurrentMap;
        public static List<ShopInfo> Shops;
        public static ShopInfo CurrentShop;
        public static List<InventoryItem> Drops;
        public static Server CurrentServer;
        public static List<Quest> Quests;
        public static Dictionary<string, PlayerInfo> _players;
        public static List<ShopInfo> LoadedShops;
        public static DropStack DropStack;
        private static readonly Dictionary<LockActions, string> LockedActions;

        public static List<PlayerInfo> Players => _players.Values.ToList();
        public static List<Monster> VisibleMonsters => Flash.Call<List<Monster>>("GetVisibleMonstersInCell", new string[0]);
        public static List<Monster> AvailableMonsters => Flash.Call<List<Monster>>("GetMonstersInCell", new string[0]);
        public static bool IsMapLoading => !Flash.Call<bool>("MapLoadComplete", new string[0]);
        public static List<string> PlayersInMap => Flash.Call<List<string>>("PlayersInMap", new string[0]);
        public static List<InventoryItem> ItemTree => Flash.Call<List<InventoryItem>>("GetItemTree", new string[0]);
        public static string[] Cells => Flash.Call<string[]>("GetCells", new string[0]);
        public static List<string> Pads => Flash.Call<List<string>>("GetPads", new string[0]);
        public static int RoomId => Map.ID;
        public static int RoomNumber => int.Parse(Map.Instance);
        public static List<Avatar> AvatarsInMap => Flash.Call<List<Avatar>>("AvatarsInMap", new string[0]);
        public static List<Avatar> AvatarsInMyCell => AvatarsInMap.FindAll((Avatar a) => a.Cell == Player.Cell);
        public static List<Monster> MonstersInMap => Flash.Call<List<Monster>>("MonstersInMap", new string[0]);
        public static bool VisibleConnScreen => Flash.Call<bool>("InConnStage", new string[0]);
        public static List<Aura> PlayerAuras { get; set; }
        public static List<Aura> EnemyAuras { get; set; }
        public static List<Cell> GetMapCells => Flash.Call<List<Cell>>("GetMapCells", new string[0]);
        public static List<Cell> AllCellInfo => Flash.Call<List<Cell>>("GetAllPads", new object[1] { true });

        // =================================================================
        // OLAYLAR (EVENTS)
        // =================================================================
        public static event Action<InventoryItem> ItemDropped;
        public static event Action<ShopInfo> ShopLoaded;
        public static event Action<Aura> auraCountdown;

        // =================================================================
        // STANDART METOTLAR
        // =================================================================
        public static void RefreshDictionary()
        {
            _players = JsonConvert.DeserializeObject<Dictionary<string, PlayerInfo>>(Flash.Call("Players", new object[0]));
        }

        public static void OnItemDropped(InventoryItem drop)
        {
            Action<InventoryItem> itemDropped = World.ItemDropped;
            itemDropped?.Invoke(drop);

            if (Drops.Find((InventoryItem d) => d.Id == drop.Id) == null)
            {
                Drops.Add(drop);
            }
            int num = Player.Bank.SavedItems.FirstOrDefault((InventoryItem it) => it.Name.Equals(drop.Name, StringComparison.OrdinalIgnoreCase))?.Quantity ?? (Player.Inventory.Items.Find((InventoryItem x) => x.Name.Equals(drop.Name)) ?? new InventoryItem()).Quantity;
            LogForm.Instance.AppendDrops($"[Item Drop] ({num}) {drop.Name} x {drop.Quantity} at {DateTime.Now:hh:mm:ss tt}.\r\n");
        }

        public static void OnShopLoaded(ShopInfo shopInfo)
        {
            World.ShopLoaded?.Invoke(shopInfo);
            LoadedShops.Add(shopInfo);
        }

        public static bool IsActionAvailable(LockActions action)
        {
            return Flash.Call<bool>("IsActionAvailable", new string[1] { LockedActions[action] });
        }

        public static void SetSpawnPoint()
        {
            Flash.Call("SetSpawnPoint");
        }

        public static bool IsMonsterAvailable(string name)
        {
            return Flash.Call<bool>("IsMonsterAvailable", new string[1] { name });
        }

        public static int MonsterHealth(string name)
        {
            return Flash.Call<int>("MonsterHealth", new string[1] { name });
        }

        public static void ReloadCurrentMap()
        {
            Flash.Call("reloadCurrentMap");
        }

        public static void GameMessage(string msg)
        {
            Flash.Call("GameMessage", msg);
        }

        public static string ServerTime()
        {
            return Flash.Call<string>("GetServerTime", new string[0]);
        }

        public static string ServerName()
        {
            return Flash.Call<string>("GetServerName", new string[0]);
        }

        public static void SendClientPacket(string packet, string type = "str")
        {
            Flash.Call("sendClientPacket", packet, type);
        }

        public static void SendPacket(string packet, string type = "String")
        {
            Flash.CallGameFunction("sfc.send" + type, packet);
        }

        public static List<Avatar> AvatarsInCell(string cell)
        {
            string targetedCell = ((cell != "Wait" && cell != "Blank") ? cell : "Enter");
            return AvatarsInMap.FindAll((Avatar a) => a.Cell == targetedCell);
        }

        public static List<Monster> MonstersInCell(string cell)
        {
            return Monsters?.FindAll((Monster mon) => mon.Cell == cell) ?? new List<Monster>();
        }

        public static List<Monster> VisibleMonster(string cell)
        {
            return Monsters?.FindAll((Monster m) => m.Cell == cell && m.Alive) ?? new List<Monster>();
        }

        public static bool IsGameLoaded()
        {
            return Flash.Call<bool>("InGame", new string[0]);
        }

        public static void GamePopup(string text, bool red = false)
        {
            Flash.Call("GamePopup", text, red);
        }

        public static Cell CellInfo(string cell)
        {
            return Flash.Call<Cell>("GetCellInfo", new string[1] { cell });
        }

        public static void ScanMap()
        {
            Flash.Call("GetAllPads", false);
        }

        // =================================================================
        // CONSTRUCTOR (YAPICI METOT)
        // =================================================================
        static World()
        {
            Maps = new List<Map>();
            CurrentMap = new Map();
            Shops = new List<ShopInfo>();
            CurrentShop = new ShopInfo();
            Drops = new List<InventoryItem>();
            CurrentServer = new Server();
            Quests = new List<Quest>();
            PlayerAuras = new List<Aura>();
            EnemyAuras = new List<Aura>();
            LoadedShops = new List<ShopInfo>();
            DropStack = new DropStack();
            Monsters = new List<Monster>();

            LockedActions = new Dictionary<LockActions, string>(14)
            {
                { LockActions.LoadShop, "loadShop" },
                { LockActions.LoadEnhShop, "loadEnhShop" },
                { LockActions.LoadHairShop, "loadHairShop" },
                { LockActions.EquipItem, "equipItem" },
                { LockActions.UnequipItem, "unequipItem" },
                { LockActions.BuyItem, "buyItem" },
                { LockActions.SellItem, "sellItem" },
                { LockActions.GetMapItem, "getMapItem" },
                { LockActions.TryQuestComplete, "tryQuestComplete" },
                { LockActions.AcceptQuest, "acceptQuest" },
                { LockActions.DoIA, "doIA" },
                { LockActions.Rest, "rest" },
                { LockActions.Who, "who" },
                { LockActions.Transfer, "tfer" }
            };
        }
    }
}