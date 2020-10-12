using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyParserLib.TreeParts
{
    public class Namespace
    {
        public List<DataType> DataTypes { get; set; }
        public string Name { get; set; }

        public Namespace(string name)
        {
            Name = name;
            DataTypes = new List<DataType>();
        }
    }
}
