using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyParserLib.TreeParts
{
    public class Method
    {
        public string Signature { get; set; }
        public string Name { get; set; }

        public Method(string name, string signature)
        {
            Name = name;
            Signature = signature;
        }
    }
}
