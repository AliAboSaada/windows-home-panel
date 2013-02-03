using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using OknoWpf.Data;
using OknoWpf.Core;

namespace OknoWpf.ViewModels {
    public class DataAgent {
        private DataStorage<ItemsDb> storage;
        private XmlDataSource<ItemsDb> source;
        private AppValueProvider appValueProvider;

        public DataAgent(AppValueProvider valueProvider) {
            this.source = new XmlDataSource<ItemsDb>();
            this.appValueProvider = valueProvider;
        }

        public string Name { get; set; }

        public void Start() {
            storage = source.Read();

            if (storage.Item.Items == null) {
                storage.Item.Items = new ObservableCollection<ItemModel>();
            }
        }

        public void Stop() {
            source.Write(storage);
        }

        public void Configure(ConfigurationData configuration) {
            source.ReturnNewIfEmpty = true;
            source.Configure(appValueProvider.ResolveValue(configuration.GetValue("FilePath")));
        }

        public ObservableCollection<ItemModel> Items { get { return storage.Item.Items; } set { storage.Item.Items = value; } }

        public void Save() {
            source.Write(storage);
        }
    }
}
