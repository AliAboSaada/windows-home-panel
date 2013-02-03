using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OknoWpf.Models;
using OknoWpf.ViewModels;
using OknoWpf.Data;
using OknoWpf.Core;
using OknoWpf.Logic.Commands.Types;
using OknoWpf.Tasks;
using System.IO;

namespace OknoWpf.Modules {
    public class TasksModule : ModuleBase, IConfigurable, IService{
        private AppValueProvider appValue;

        public TasksModule(AppValueProvider appValue) {
            this.appValue = appValue;
            DataAgent = new DataAgent(appValue);
            Name = "Tasks";
            Commands = new List<Logic.Commands.ICommand>(new[]{
                new TasksCommand(DataAgent)
            });
        }

        public void Configure(ConfigurationData data) {
            ConfigurationData d = data.GetConfiguration("Tasks");
            if (d == null) {
                d = new ConfigurationData();
                d.AddConfiguration(TasksConsts.DataFilePathVariable, Path.Combine(AppVariables.CurrentDir, "tasks.db"));
            }

            DataAgent.Configure(d);
        }

        public void Start() {
            DataAgent.Start();
        }

        public void Stop() {
            DataAgent.Stop();
        }
    }
}
