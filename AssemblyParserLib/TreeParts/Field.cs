using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace AssemblyParserLib.TreeParts
{
    public class Field
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Signature { get; set; }

        public Field(string name, string type)
        {
            Name = name;
            Type = type;
            Signature = String.Format("{0} {1}", type, name);
        }
    }
}
