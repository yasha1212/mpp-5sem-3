using AssemblyParserLib;
using AssemblyParserLib.TreeParts;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssemblyBrowser
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Model model;
        private ICommand openFileCommand;

        public ObservableCollection<AssemblyTree> AssemblyTree
        {
            get => model.AssemblyTree;
            set
            {
                model.AssemblyTree = value;
                OnPropertyChanged("AssemblyTree");
            }
        }

        public string FileName
        {
            get => model.FileName;
            set
            {
                model.FileName = value;

                var parser = new AssemblyParser();
                AssemblyTree = new ObservableCollection<AssemblyTree>
                {
                    parser.ParseFromFile(model.FileName)
                };
                OnPropertyChanged("FileName");
            }
        }

        public ICommand OpenFileCommand
        {
            get
            {
                return openFileCommand ?? new OpenFileCommand(obj => 
                {
                    var openFileDialog = new OpenFileDialog();

                    FileName = openFileDialog.ShowDialog() == true ? openFileDialog.FileName : FileName;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel(Model model)
        {
            this.model = model;
        }

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
