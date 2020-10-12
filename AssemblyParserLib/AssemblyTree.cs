using AssemblyParserLib.TreeParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyParserLib
{
    public class AssemblyTree
    {
        public List<Namespace> Namespaces { get; set; }

        public AssemblyTree()
        {
            Namespaces = new List<Namespace>();
        }
    }
}
