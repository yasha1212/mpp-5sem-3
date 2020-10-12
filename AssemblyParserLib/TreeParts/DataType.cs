using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyParserLib.TreeParts
{
    public class DataType
    {
        public List<Method> Methods { get; set; }
        public List<Property> Properties { get; set; }
        public List<Field> Fields { get; set; }
        public string Name { get; set; }

        public DataType(string name)
        {
            Name = name;
            Methods = new List<Method>();
            Properties = new List<Property>();
            Fields = new List<Field>();
        }
    }
}
