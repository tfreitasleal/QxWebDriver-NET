using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
/*using JSONArray = org.json.simple.JSONArray;
using JSONObject = org.json.simple.JSONObject;
using JSONParser = org.json.simple.parser.JSONParser;
using ParseException = org.json.simple.parser.ParseException;*/

namespace Qooxdoo.WebDriver.Log
{
    public class LogEntry
    {
        public LogEntry(string json)
        {
            //JSONParser parser = new JSONParser();

            try
            {
                JObject jObject = JObject.Parse(json);
                clazz = (string) jObject.GetValue("clazz");
                level = (string) jObject.GetValue("level");
                time = (string) jObject.GetValue("time");
                JArray jArray = (JArray) jObject.GetValue("items");
                using (IEnumerator<object> itr = jArray.GetEnumerator())
                {
                    while (itr.MoveNext())
                    {
                        items.Add(itr.Current.ToString());
                    }
                }

                /*object obj = parser.parse(json);
                JSONObject jsonEntry = (JSONObject) obj;
                clazz = (string) jsonEntry.get("clazz");
                level = (string) jsonEntry.get("level");
                time = (string) jsonEntry.get("time");

                JSONArray jsonItems = (JSONArray) jsonEntry.get("items");
                using (IEnumerator<object> itr = jsonItems.GetEnumerator())
                {
                    while (itr.MoveNext())
                    {
                        items.Add(itr.Current.ToString());
                    }
                }*/
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }

        public string clazz;
        public string level;
        public IList<string> items = new List<string>();
        public string time;

        public override string ToString()
        {
            if (string.ReferenceEquals(clazz, null))
            {
                return time + " " + level + ": " + items;
            }

            return time + " " + level + ": " + clazz + " " + items;
        }
    }
}