using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace OknoWpf.Data {
    [Serializable]
    public class ItemsDb {
        public ObservableCollection<ItemModel> Items { get; set; }
    }
}
