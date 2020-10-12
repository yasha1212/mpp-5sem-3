using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyParserLib.TreeParts
{
    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public Property(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
