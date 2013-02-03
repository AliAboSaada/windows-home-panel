using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OknoWpf.Logic.Commands {
    public class CommandException : Exception {
        public CommandException(String message) :base(message){
        }
    }
}
