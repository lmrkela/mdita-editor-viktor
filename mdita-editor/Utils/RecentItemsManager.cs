using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace mDitaEditor.Utils
{
	public static class RecentItemsManager
	{
		public static readonly string REGISTRY_KEY = "Software\\mDitaEditor\\MRU";

        private static readonly List<string> _recentItems = new List<string>();

        static RecentItemsManager()
        {
            var regKey = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, false);
            if (regKey != null)
            {
                var values = regKey.GetValueNames();
                Array.Sort(values, (string a, string b) =>
                {
                    return b?.CompareTo(a) ?? 0;
                });
                _recentItems.AddRange(values.Select(value => regKey.GetValue(value, null)).OfType<string>());
            }
        }

        public static List<string> RecentProjects => new List<string>(_recentItems);

	    public static bool Add(string path)
	    {
	        if (path == null)
	        {
	            return false;
	        }

	        using (var regKey = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY))
	        {
	            if (regKey == null)
	            {
	                return false;
	            }

	            if (_recentItems.Contains(path))
                {
                    Remove(path);
                }
	            while(true)
	            {
	                var name = DateTime.Now.Ticks.ToString();
	                var value = regKey.GetValue(name);
	                if (value == null)
	                {
	                    regKey.SetValue(name, path);
	                    _recentItems.Insert(0, path);
	                    return true;
	                }
	            }
	        }
	    }

	    public static bool Remove(string path)
	    {
	        if (path == null)
	        {
	            return false;
	        }
            if (!_recentItems.Contains(path))
            {
                return false;
            }

	        using (var regKey = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, true))
	        {
	            if (regKey == null)
	            {
	                return false;
	            }

	            foreach (var name in regKey.GetValueNames())
	            {
	                if (regKey.GetValue(name) as string == path)
	                {
	                    regKey.DeleteValue(name);
	                    _recentItems.Remove(path);
	                    return true;
	                }
	            }
	        }

	        return false;
        }

	    public static void Clear()
        {
            Registry.CurrentUser.DeleteSubKey(REGISTRY_KEY);
            _recentItems.Clear();
        }
	}
}
