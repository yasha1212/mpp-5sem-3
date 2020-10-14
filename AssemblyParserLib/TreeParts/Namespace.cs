using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AssemblyParserLib.TreeParts
{
    public class Namespace
    {
        public ObservableCollection<DataType> DataTypes { get; set; }
        public string Name { get; set; }

        public Namespace(string name)
        {
            Name = name;
            DataTypes = new ObservableCollection<DataType>();
        }
    }
}
