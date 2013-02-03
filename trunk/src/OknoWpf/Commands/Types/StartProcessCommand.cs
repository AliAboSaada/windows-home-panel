using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace OknoWpf.Logic.Commands {
    public class StartProcessCommand : ICommand{
        private Regex regex = new Regex("([A-z0-9 ]+)\\.exe");

        public void Execute(string command, bool isAccepted) {
            if (!isAccepted)
                return;

            Match m = regex.Match(command);

            if (m.Length > 0) {
                StartProcess(m.Groups[1].Value);
            }
        }

        private void StartProcess(string processName) {
            try {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(processName);
                p.Start();
            } catch (Exception ex) {
                throw new CommandException("Process not found.");
            }
        }
    }
}
