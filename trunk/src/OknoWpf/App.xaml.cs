using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using OknoWpf.Data;
using OknoWpf.Core;
using System.IO;
using System.Reflection;
using OknoWpf.ViewModels;
using System.Windows.Threading;
using OknoWpf.Models;
using OknoWpf.Modules;
//using OknoWpf.Systems;

namespace OknoWpf {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private static IWindsorContainer container = new WindsorContainer();
        private static VariablesStorage variablesStorage;

        public static Dispatcher WindowDispatcher { get; private set; }

        public static void Started(Dispatcher windowDispatcher) {
            WindowDispatcher = windowDispatcher;

            LoadModules();
            InitializeContext();
            InitializeHotKeys();
            InitializeVariables();
            ConfigureClasses();
            InitializeServices();
        }

        private static void InitializeContext() {
            Context c = container.Resolve<Context>();

            ModuleBase dvTasks = container.Resolve<TasksModule>();
            c.Modules = new[]{
                dvTasks
            };
            c.SetCurrentModule(dvTasks);
        }

        private static void InitializeHotKeys() {
            //KeyboardManager.DisableSystemKeys();
        }

        private static void InitializeServices() {
            foreach (IService service in GetImplementations<IService>()) {
                service.Start();
            }
        }

        private static void InitializeVariables() {
            variablesStorage = container.Resolve<VariablesStorage>();
            variablesStorage["CurrentDir"] = Environment.CurrentDirectory;
        }

        private static void ConfigureClasses() {
            XmlDataSource<ConfigurationData> source = new XmlDataSource<ConfigurationData>();
            source.ReturnNewIfEmpty = true;
            source.Configure(GetConfigurationPath());

            DataStorage<ConfigurationData> data = new DataStorage<ConfigurationData>(new ConfigurationData());
            //DataStorage<ConfigurationData> data = source.Read();

            //ConfigurationData dataConf = data.Item.GetConfiguration("Data");
            data.Item.Key = "Okno";
            ConfigurationData dataConf = data.Item.AddConfiguration("Data", "");
            dataConf.AddConfiguration("FilePath", "%CurrentDir%\\data.db");
            //source.Write(data);

            IEnumerable<IConfigurable> configurables = GetImplementations<IConfigurable>();
            foreach (IConfigurable c in configurables){
                c.Configure(dataConf);
            }
        }

        private static IEnumerable<T> GetImplementations<T>() {
            Type lookedType = typeof(T);
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes()) {
                if (lookedType.IsAssignableFrom(t) && t.FullName != lookedType.FullName)
                    yield return (T)container.Resolve(t);
            } 
        }

        private static string GetConfigurationPath() {
            return Path.Combine(Environment.CurrentDirectory, "conf.xml");
        }

        private static void LoadModules() {
            container.Install(new ContainerInstaller());
        }

        public static T Resolve<T>() {
            return container.Resolve<T>();
        }

        protected override void OnExit(ExitEventArgs e) {
            //KeyboardManager.EnableSystemKeys();
            base.OnExit(e);
        }
    }
}
