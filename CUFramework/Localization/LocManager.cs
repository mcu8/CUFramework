using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUFramework.Localization
{
    // Kinda UDK inspired
    class LocManager
    {
        private Dictionary<string, string> CachedLocale;
        private Locale LocaleCode;
        private static Dictionary<string, LocManager> CachedManagers = null;

        public enum Locale
        {
            INT, JPN, DEU, FRA, ESM, ESN, ITA, KOR, CHT, RUS, POL, HUN, CZE, SLO
        }

        public static LocManager GetLocaleFor(string name, Locale localeCode)
        {
            if (CachedManagers == null)
                CachedManagers = new Dictionary<string, LocManager>();
            var code = $"{name}.{localeCode}";
            if (!CachedManagers.ContainsKey(code))
            {
                CachedManagers.Add(code, new LocManager(name, localeCode));
            }
            return CachedManagers[code];
        }

        public LocManager(string name, Locale localeCode = Locale.INT)
        {
            CachedLocale = new Dictionary<string, string>();
            LocaleCode = localeCode;
            string data;
            try
            {
                data = Properties.Resources.ResourceManager.GetString($"{name}.{localeCode}");
            }
            catch (Exception e)
            {
                #if DEBUG
                Debug.WriteLine(e.Message);
                #endif
                data = null;
            }

            if (data == null)
            {
                #if DEBUG
                Debug.WriteLine($"[WARNING] Locale {localeCode} undefined!");
                #endif
                var loc = $"Localization/{localeCode}/{name}.{localeCode}";
                //Debug.WriteLine(Path.GetFullPath(loc));
                if (File.Exists(loc))
                {
                    data = File.ReadAllText(loc);
                    //Debug.WriteLine(data);
                }
            }

            foreach (var line in data.Split('\n'))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed)) continue;
                var isComment = trimmed.StartsWith("#") || trimmed.StartsWith(";");
                if (!isComment)
                {
                    var kv = line.Split('=');
                    if (kv.Length > 1)
                    {
                        var key = kv[0].Trim();
                        var value = string.Join("=", new List<string>(kv).GetRange(1, kv.Count() - 1)).Trim();
                        if (CachedLocale.ContainsKey(key))
                        {
                            #if DEBUG
                            Debug.WriteLine($"[WARNING] Locale {localeCode} already contains {key} defined!");
                            #endif
                            CachedLocale[key.ToLower()] = value;
                        }
                        else
                        {
                            CachedLocale.Add(key.ToLower(), value);
                        }
                    }
                }
            }
        }

        public string GetLocalizedString(string key, params string[] pars)
        {
            if (CachedLocale.ContainsKey(key))
            {
                return string.Format(CachedLocale[key.ToLower()], pars);
            }
            #if DEBUG
            Debug.WriteLine($"[WARNING] Locale {LocaleCode} doesn't have {key} defined!");
            #endif
            return $"?{LocaleCode}.{key}?";
        }
    }
}
