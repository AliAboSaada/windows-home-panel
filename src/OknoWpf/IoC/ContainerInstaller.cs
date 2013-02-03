using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Core.Logging;
using OknoWpf.ViewModels;
using OknoWpf.Data;
using OknoWpf.Core;
using OknoWpf.Logic.Commands;
using OknoWpf.Logic.Commands.Types;
using OknoWpf.Logic;
using OknoWpf.Modules;

namespace OknoWpf {
    class ContainerInstaller :IWindsorInstaller {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store) {
            container.Register(
                Component.For<CommandExecutor>(),
                Component.For<AppCommands>(),
                Component.For<TextChangedTrigger>(),

                Component.For<TasksModule>(),

                Component.For<ProgramService>(),

                Component.For<AppValueProvider>(),
                Component.For<VariablesStorage>(),

                Component.For<Context>(),

                Component.For<XmlDataSource<ItemsDb>>(),
                Component.For<DataStorage<ItemsDb>>(),

                Component.For<ProgramCommand>(),
                Component.For<StartProcessCommand>(),
                Component.For<TasksCommand>(),

                Component.For<MainViewModel>());
        }
    }
}
