using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AssemblyParserLib.TreeParts
{
    public class DataType
    {
        public ObservableCollection<Method> Methods { get; set; }
        public ObservableCollection<Property> Properties { get; set; }
        public ObservableCollection<Field> Fields { get; set; }
        public string Name { get; set; }

        public DataType(string name)
        {
            Name = name;
            Methods = new ObservableCollection<Method>();
            Properties = new ObservableCollection<Property>();
            Fields = new ObservableCollection<Field>();
        }
    }
}
