using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OknoWpf.ViewModels;
using System.Text.RegularExpressions;
using OknoWpf.Data;

namespace OknoWpf.Logic.Commands.Types {
    public class ProgramCommand :ICommand {
        private Context context;
        private Regex uriRegex = new Regex("[A-z]+\\.[A-z]+"); 

        public ProgramCommand(Context context) {
            this.context = context;
        }

        public void Execute(string command, bool isAccepted) {
            command = command.Trim();
            if (command == "exit") {
                DispatchApp(() => {
                    try {
                        App.Current.Shutdown();
                    } catch (Exception ex) {
                        Debug.Print(ex.ToString());
                    }
                });
                
            } else if (command == "last run") {

            } else if (command == "save all") {
                context.SaveAll();
            } else if ((Uri.IsWellFormedUriString(command, UriKind.Absolute) || uriRegex.IsMatch(command)) && !command.EndsWith(".exe")) {
                if (isAccepted) {
                    ProcessRunner.Run("chrome", String.Format("{0} --incognito --disable-extensions", command));
                }
            }
        }

        private void DispatchApp(Action action) {
            App.Current.Dispatcher.Invoke(action);
        }
    }
}
