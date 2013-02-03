using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OknoWpf.Data;
using OknoWpf.Logic.Commands.Types;
using OknoWpf.Logic.Commands;

namespace OknoWpf.Logic {
    public class ProgramService : IService{
        private ProgramCommand programCommand;
        private StartProcessCommand startProcessCommand;
        private CommandExecutor executor;

        public ProgramService(CommandExecutor executor, ProgramCommand programCommand, StartProcessCommand startProcessCommand) {
            this.programCommand = programCommand;
            this.startProcessCommand = startProcessCommand;
            this.executor = executor;
        }

        public void Start() {
            executor.RegisterCommands(programCommand, startProcessCommand);
        }

        public void Stop() {
        }
    }
}
