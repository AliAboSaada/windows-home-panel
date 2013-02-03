using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OknoWpf.Data {
    public class DataStorage<T> {
        private T item;

        public DataStorage(T item){
            this.item = item;    
        }
        public T Item { get { return item; } }
    }
}
