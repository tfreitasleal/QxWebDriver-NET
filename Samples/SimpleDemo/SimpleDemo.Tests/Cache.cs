using System.Collections.Generic;
using System.Data;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;

namespace SimpleDemo.Tests
{
    public static class Cache
    {
        private static object _default;

        private static readonly Dictionary<string, object> Properties = new Dictionary<string, object>();

        #region Clear & Destroy

        public static void Clear()
        {
            Properties.Clear();
        }

        public static void Destroy(string key)
        {
            if (!Properties.ContainsKey(key))
                throw new MissingPrimaryKeyException(key);

            Properties.Remove(key);
        }

        #endregion

        #region Set

        public static void Set(string key, object value)
        {
            Properties[key] = value;
        }

        public static void SetWidget(string key, IWidget value)
        {
            Properties[key] = value;
        }

        public static void SetWebElement(string key, IWebElement value)
        {
            Properties[key] = value;
        }

        public static void SetString(string key, string value)
        {
            Properties[key] = value;
        }

        #endregion

        #region Get

        public static object Get(string key)
        {
            if (!Properties.ContainsKey(key))
                throw new MissingPrimaryKeyException(key);

            return Properties[key];
        }

        public static IWidget GetWidget(string key)
        {
            if (!Properties.ContainsKey(key))
                throw new MissingPrimaryKeyException(key);

            return Properties[key] as IWidget;
        }

        public static IWebElement GetWebElement(string key)
        {
            if (!Properties.ContainsKey(key))
                throw new MissingPrimaryKeyException(key);

            return Properties[key] as IWebElement;
        }

        public static string GetString(string key, string defaultValue)
        {
            _default = defaultValue;
            return GetCore(key);
        }

        public static string GetString(string key)
        {
            _default = null;
            return GetCore(key);
        }

        private static string GetCore(string key)
        {
            if (!Properties.ContainsKey(key))
            {
                if (_default != null)
                    Set(key, _default.ToString());
                else
                    throw new MissingPrimaryKeyException(key);
            }

            return Properties[key] as string;
        }

        #endregion
    }
}