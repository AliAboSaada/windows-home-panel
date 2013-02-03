using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using OknoWpf.ViewModels;
using OknoWpf.Logic.Commands;

namespace OknoWpf.Models {
    public abstract class ModuleBase {
        public string Name { get; set; }
        public DataAgent DataAgent { get; protected set; }

        public ObservableCollection<ItemModel> Items { get { return DataAgent.Items; } }

        public List<ICommand> Commands { get; set; }

        public ModuleBase() {
        }
    }
}
