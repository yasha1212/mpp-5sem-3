using AssemblyParserLib.TreeParts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class Model
    {
        public string FileName { get; set; }

        public ObservableCollection<AssemblyTree> AssemblyTree { get; set; }
    }
}
