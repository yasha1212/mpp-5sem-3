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
    public class Model : INotifyPropertyChanged
    {
        private string fileName;
        private ObservableCollection<AssemblyTree> assemblyTree;

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        public ObservableCollection<AssemblyTree> AssemblyTree
        {
            get => assemblyTree;
            set
            {
                assemblyTree = value;
                OnPropertyChanged("AssemblyTree");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
