using System;

namespace DBSync.Ini
{
    //Ini Key Value Pair
    public class IniKeyValuePair
    {
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
        public IniKeyValuePair()
        {
            
        }

        public IniKeyValuePair(string iniPairLine)
        {
            string[] p = iniPairLine.Split('=');
            string key = p[0];
            string value = iniPairLine.Substring(iniPairLine.IndexOf('=') + 1);
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            Key = key.Trim();
            Value = ParseValue(value.Trim());
        }

        public IniKeyValuePair(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            Key = key.Trim();
            Value = ParseValue(value.Trim());
        }

        private string ParseValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            else
            {
                string v = value;
                v=v.Replace("\\r", "\r").Replace("\\n","\n");
                return v;
            }
        }
    }
}
