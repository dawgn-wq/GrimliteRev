using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.Botting
{
    public class BotClientConfig
    {
        public string file { get; set; }

        public Dictionary<string, string> Contents { get; set; } = new Dictionary<string, string>();

        public static BotClientConfig Instance { get; set; } = Load(Application.StartupPath + "\\BotClientConfig.cfg");

        public string Get(string key)
        {
            if (!Contents.TryGetValue(key, out var value))
            {
                return null;
            }
            return value;
        }

        public void Set(string key, string value)
        {
            Contents[key] = value;
        }

        public void Save()
        {
            File.WriteAllLines(file, Contents.Select((KeyValuePair<string, string> kvp) => kvp.Key + "=" + kvp.Value));
        }

        public static BotClientConfig Load(string path)
        {
            // DOSYA OLUŞTURMA VE KİLİT SORUNU DÜZELTİLDİ:
            if (!File.Exists(path))
            {
                File.Create(path).Close(); // Dosyayı oluştur ve hemen kilidini serbest bırak
            }

            var config = new BotClientConfig { file = path };

            // BOŞ DOSYA OKUMA HATALARI DÜZELTİLDİ:
            foreach (string line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains("=")) continue;
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    config.Contents[parts[0]] = parts[1];
                }
            }

            return config;
        }

        public T GetValue<T>(string key)
        {
            object obj;
            try
            {
                obj = Instance.Get(key);
            }
            catch
            {
                obj = null;
            }
            if (typeof(T) == typeof(int))
            {
                obj = ((obj == null) ? (-1) : int.Parse(obj.ToString()));
            }
            else if (typeof(T) == typeof(bool))
            {
                obj = obj != null && bool.Parse(obj.ToString());
            }
            else if (typeof(T) == typeof(string))
            {
                obj = obj?.ToString();
            }
            return (T)obj;
        }

        public void SetValue(string key, string value)
        {
            Instance.Set(key, value);
            Instance.Save();
        }
    }
}