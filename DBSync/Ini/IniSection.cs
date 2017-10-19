using System.Collections.Generic;

namespace DBSync.Ini
{
    public class IniSection
    {
        public string Name { get; set; }
        public List<IniKeyValuePair> Values { get; set; }=new List<IniKeyValuePair>();

        public string this[int i] => Values[i].Value;

        public string this[string n]
        {
            get
            {
                IniKeyValuePair v = null;
                foreach (var valuePair in Values)
                {
                    if (valuePair.Key == n)
                    {
                        v = valuePair;
                        break;
                    }
                }
                return v?.Value;
            }
            set
            {
                IniKeyValuePair v = null;
                foreach (var valuePair in Values)
                {
                    if (valuePair.Key == n)
                    {
                        v = valuePair;
                        break;
                    }
                }
                if (v != null)
                {
                    v.Value = value;
                }
                else
                {
                    Values.Add(new IniKeyValuePair(n,value));
                    
                }
            }
        }
    }
}
