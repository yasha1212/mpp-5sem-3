using AssemblyParserLib.TreeParts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AssemblyParserLib.TreeParts
{
    public class AssemblyTree
    {
        public ObservableCollection<Namespace> Namespaces { get; set; }

        public AssemblyTree()
        {
            Namespaces = new ObservableCollection<Namespace>();
        }
    }
}
