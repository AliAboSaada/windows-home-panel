using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using OknoWpf.ViewModels;
using System.Windows;
using OknoWpf.Data;
using OknoWpf.Models;

namespace OknoWpf {
    class MainViewModel : NotifyPropertyChanged {

        private String commandText;
        private CommandExecutor commandExecutor;
        private Context context;

        public ModuleBase[] Modules { get { return context.Modules; } }
        public ModuleBase CurrentModule { get { return context.CurrentModule; } }

        public MainViewModel(CommandExecutor executor, Context context) {
            this.commandExecutor = executor;
            this.context = context;

            context.CurrentModuleChanged += CurrenModuleChanged;
        }

        public ObservableCollection<ItemModel> Items { get { return CurrentModule.Items; } }


        private void CurrenModuleChanged() {
            fire("CurrentModule");
        }

        public String ViewName {
            get {
                return CurrentModule.Name;
            }
        }

        public String CommandText {
            get {
                return commandText;
            }
            set {
                commandText = value;
                fire("CommandText");

                commandExecutor.Execute(commandText, false);
            }
        }

        public void AcceptCommand() {
            ExecuteCommand(CommandText);
        }

        public void ExecuteCommand(string commandText) {
            commandExecutor.Execute(commandText, true);
        }
    }
}
