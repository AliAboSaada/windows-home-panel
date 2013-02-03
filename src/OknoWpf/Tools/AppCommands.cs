using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace OknoWpf {
    public class AppCommands {

        public AppCommands() {
        }

        public ICommand OpenMainWindow { get; set; }
    }
}
