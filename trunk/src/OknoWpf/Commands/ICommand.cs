using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OknoWpf.Logic.Commands {
    public interface ICommand {
        void Execute(string command, bool isAccepted);
    }
}
