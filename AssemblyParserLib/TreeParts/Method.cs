using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyParserLib.TreeParts
{
    public class Method
    {
        public string Signature { get; set; }

        public Method(string signature)
        {
            Signature = signature;
        }
    }
}
