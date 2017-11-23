using System.Collections.Generic;
using System.Data;

namespace Qooxdoo.WebDriverDemo
{
    public static class SystemProperties
    {
        private static Dictionary<string, string> _properties;
        private static string _default;

        public static Dictionary<string, string> Instance
        {
            get
            {
                if (_properties == null)
                    _properties = new Dictionary<string, string>();

                return _properties;
            }
        }

        public static void SetProperty(string key, string value)
        {
            Instance[key] = value;
        }

        public static string GetProperty(string key, string defaultValue)
        {
            _default = defaultValue;
            return GetPropertyCore(key);
        }

        public static string GetProperty(string key)
        {
            _default = null;
            return GetPropertyCore(key);
        }

        private static string GetPropertyCore(string key)
        {
            if (!Instance.ContainsKey(key))
            {
                if (_default != null)
                    SetProperty(key, _default);
                else
                    throw new MissingPrimaryKeyException(key);
            }

            return _properties[key];
        }
    }
}