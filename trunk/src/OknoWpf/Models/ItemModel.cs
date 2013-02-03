using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;
using System.Xml.Serialization;
using OknoWpf.Models;

namespace OknoWpf {
    [Serializable]
    public class ItemModel : NotifyPropertyChanged {
        private string name;
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
                fire("Name");
            }
        }
        private string description;
        public string Description {
            get {
                return description;
            }
            set {
                description = value;
                fire("Description");
            }
        }

        private double itemHeight = 50;
        [XmlIgnore]
        public double ItemHeight {
            get {
                return itemHeight;
            }
            set {
                itemHeight = value;
                fire("ItemHeight");
            }
        }

        private ImageSource imageSource;
        [XmlIgnore]
        public ImageSource ImageSource {
            get {
                return imageSource;
            }
            set {
                imageSource = value;
                fire("ImageSource");
            }
        }

        private Brush itemForeground = Brushes.WhiteSmoke;
        [XmlIgnore]
        public Brush ItemForeground {
            get {
                return itemForeground;
            }
            set {
                itemForeground = value;
                fire("ItemForeground");
            }
        }
        private ItemModelMode mode = ItemModelMode.View;
        [XmlIgnore]
        public ItemModelMode Mode { get { return mode; } set { mode = value; fire("Mode", "IsEditMode", "IsViewMode"); } }
        [XmlIgnore]
        public bool IsEditMode { get { return mode == ItemModelMode.Edit; } }
        [XmlIgnore]
        public bool IsViewMode { get { return mode == ItemModelMode.View; } }

        public ItemModel() {
        }
    }
}
