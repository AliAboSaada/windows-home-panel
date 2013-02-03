using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OknoWpf.ViewModels;
using OknoWpf.Models;

namespace OknoWpf.Data {
    public class Context {
        public event Action CurrentModuleChanged;

        public ModuleBase CurrentModule { get; private set; }
        public ModuleBase[] Modules { get; set; }        

        public void SetCurrentModule(ModuleBase module) {
            CurrentModule = module;

            if (CurrentModuleChanged != null) {
                CurrentModuleChanged();
            }
        }

        public void SaveAll() {
            foreach (ModuleBase m in Modules) {
                m.DataAgent.Save();
            }
        }
    }
}
