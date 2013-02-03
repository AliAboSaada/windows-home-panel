using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace OknoWpf {
    public class NotifyPropertyChanged : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void fire(params String[] names) {
            if (PropertyChanged != null) {
                foreach (String n in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(n));
            }
        }
    }
}
