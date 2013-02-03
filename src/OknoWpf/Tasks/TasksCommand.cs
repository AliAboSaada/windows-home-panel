using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using OknoWpf.ViewModels;
using OknoWpf.Wpf;
using OknoWpf.Modules;

namespace OknoWpf.Logic.Commands.Types {
    public class TasksCommand : ICommand{
        private Regex regex = new Regex("add ([A-z0-9 ]+)", RegexOptions.IgnoreCase);
        private DataAgent data;

        public TasksCommand(DataAgent data) {
            this.data = data;
        }

        public void Execute(string command, bool isAccepted) {
            if (isAccepted) {
                Match m = regex.Match(command);
                if (m.Groups.Count > 0) {
                    String taskName = m.Groups[1].Value;

                    if (taskName.Length > 0) {
                        AppDispatcher.Run(() => {
                            data.Items.Add(new ItemModel {
                                Name = taskName
                            });
                        });
                        data.Save();
                    }
                }
            }
        }
    }
}
