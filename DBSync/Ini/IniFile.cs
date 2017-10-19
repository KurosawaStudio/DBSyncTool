using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DBSync.Ini
{
    public class IniFile
    {
        public List<IniSection> Sections { get; set; }=new List<IniSection>();

        public IniSection this[int i] => Sections[i];

        public IniSection this[string n]
        {
            get
            {
                IniSection v = null;
                foreach (var sect in Sections)
                {
                    if (sect.Name == n)
                    {
                        v = sect;
                        break;
                    }
                }
                return v;
            }
            set
            {
                IniSection v = null;
                foreach (var sect in Sections)
                {
                    if (sect.Name == n)
                    {
                        v = sect;
                        break;
                    }
                }
                if (v != null)
                {
                    v = value;
                }
                else
                {
                    Sections.Add(new IniSection() {Name = n});

                }
            }
        }

        public static IniFile FromIni(string[] iniContent)
        {
            IniFile ini=new IniFile();
            string[] lines = iniContent;

            Regex sectline=new Regex("^[[][a-zA-Z0-9]{1,30}[]]$");
            Regex kvline=new Regex("^.*[=].*$");
            IniSection sect = null;
            foreach (var line in lines)
            {
                // REMOVE EMPTY LINES OR COMMENT LINES
                if (string.IsNullOrEmpty(line.Trim()) || line.StartsWith(";") || line.StartsWith("#")) continue;

                string iniLine = line.Trim();

                if (sectline.IsMatch(iniLine))
                {
                    if (sect != null)
                    {
                        ini.Sections.Add(sect);
                    }
                    sect = new IniSection {Name = iniLine.Trim('[').Trim(']')};
                }
                else if (kvline.IsMatch(iniLine))
                {
                    if (sect == null)
                    {
                        sect=new IniSection(){Name="NoName"};
                    }
                    sect.Values.Add(new IniKeyValuePair(iniLine));
                }
                
            }

            if (sect != null && (!ini.Sections.Contains(sect)))
            {
                ini.Sections.Add(sect);
            }

            return ini;
        }

        public string ToIni()
        {
            string ini = "";
            foreach (var sect in Sections)
            {
                ini += $"[{sect.Name}]\r\n";
                foreach (var kp in sect.Values)
                {
                    ini += $"{kp.Key}={kp.Value.Replace("\r", "\\r").Replace("\n", "\\n")}\r\n";
                }
                ini += "\r\n";
            }
            return ini;
        }
    }
}
