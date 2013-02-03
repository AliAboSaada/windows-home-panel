using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace OknoWpf {
    class EventCommand : ICommand {
        public Action Action { get; set; }

        public EventCommand(Action action) {
            Action = action;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter) {
            if (Action != null) {
                Action();
            }
        }
    }
}
