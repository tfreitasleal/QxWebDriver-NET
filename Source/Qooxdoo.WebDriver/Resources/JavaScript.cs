using System;
using System.Collections.Generic;
using System.IO;

/* ************************************************************************

   qxwebdriver-java

   http://github.com/qooxdoo/qxwebdriver-java

   Copyright:
     2012-2013 1&1 Internet AG, Germany, http://www.1und1.de

   License:
     LGPL: http://www.gnu.org/licenses/lgpl.html
     EPL: http://www.eclipse.org/org/documents/epl-v10.php
     See the license.txt file in the project's top-level directory for details.

   Authors:
     * Daniel Wagner (danielwagner)

************************************************************************ */

namespace Qooxdoo.WebDriver.Resources
{
    public sealed class JavaScript
    {
        public static readonly JavaScript Instance = new JavaScript("INSTANCE", InnerEnum.Instance);

        private static readonly IList<JavaScript> ValueList = new List<JavaScript>();

        static JavaScript()
        {
            ValueList.Add(Instance);
        }

        public enum InnerEnum
        {
            Instance
        }

        private readonly string _nameValue;
        private readonly int _ordinalValue;
        private readonly InnerEnum _innerEnumValue;
        private static int _nextOrdinal = 0;

        private JavaScript(string name, InnerEnum innerEnum)
        {
            _nameValue = name;
            _ordinalValue = _nextOrdinal++;
            _innerEnumValue = innerEnum;
        }
        internal Dictionary<string, string> Resources = new Dictionary<string, string>();
        protected internal string Suffix = "-min";
        protected internal string FileExtension = ".js";

        public string GetValue(string resourceId)
        {
            if (!Resources.ContainsKey(resourceId))
            {
                string resourcePath = GetResourcePath(resourceId);
                AddResourceFromPath(resourceId, resourcePath);
            }

            return Resources[resourceId];
        }

        public void AddResource(string resourceId, string resourcePath)
        {
            if (!Resources.ContainsKey(resourceId))
            {
                AddResourceFromPath(resourceId, resourcePath);
            }
        }

        protected internal void AddResourceFromPath(string resourceId, string resourcePath)
        {
            string resource = ReadResource(resourcePath);
            resource = manipulateResource(resource);
            Resources[resourceId] = resource;
        }

        protected internal string GetResourcePath(string resourceId)
        {
            resourceId = "javascript." + resourceId;
            resourceId = "/" + resourceId.Replace(".", "/") + Suffix + FileExtension;
            return resourceId;
        }

        protected internal string ReadResource(string resourcePath)
        {
            System.IO.Stream @in = this.GetType().getResourceAsStream(resourcePath);
            System.IO.StreamReader br = new System.IO.StreamReader(@in);

            string text = "";
            string line;

            try
            {
                while (!string.ReferenceEquals((line = br.ReadLine()), null))
                {
                    text += line;
                }
                br.Close();
            }
            catch (IOException e)
            {
                throw new Exception("Couldn't read resource file.", e);
            }

            return text;
        }

        protected internal string manipulateResource(string resource)
        {
            Pattern pattern = Pattern.compile("function\\(\\)\\{(.*?)\\};$", Pattern.MULTILINE);
            Matcher matcher = pattern.matcher(resource);
            if (matcher.find())
            {
                resource = matcher.group(1);
            }

            return resource;
        }

        public static IList<JavaScript> Values()
        {
            return ValueList;
        }

        public InnerEnum InnerEnumValue()
        {
            return _innerEnumValue;
        }

        public int Ordinal()
        {
            return _ordinalValue;
        }

        public override string ToString()
        {
            return _nameValue;
        }

        public static JavaScript ValueOf(string name)
        {
            foreach (JavaScript enumInstance in JavaScript.Values())
            {
                if (enumInstance._nameValue == name)
                {
                    return enumInstance;
                }
            }
            throw new System.ArgumentException(name);
        }
    }

}