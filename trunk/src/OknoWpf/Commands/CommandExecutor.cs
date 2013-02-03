using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OknoWpf.Logic.Commands;
using System.Threading.Tasks;
using OknoWpf.Data;
using OknoWpf.Commands;

namespace OknoWpf {
    public class CommandExecutor {
        private List<ICommand> commands = new List<ICommand>();
        private Context context;

        public CommandExecutor(Context context) {
            this.context = context;
        }

        public void RegisterCommands(params ICommand[] commands){

            foreach (ICommand c in commands) {
                this.commands.Add(c);
            }
        }

        public void Execute(string command, bool isAccepted) {
            Task.Factory.StartNew(() => {
                CommandExecuteArgs args = new CommandExecuteArgs();

                ExecuteCommands(command, isAccepted, context.CurrentModule.Commands, args);
                ExecuteCommands(command, isAccepted, commands, args);
            });
        }

        private void ExecuteCommands(string command, bool isAccepted, List<ICommand> list, CommandExecuteArgs args) {
            foreach (ICommand c in list) {
                c.Execute(command, isAccepted);

                if (args.Handled)
                    return;
            }
        }
    }
}
