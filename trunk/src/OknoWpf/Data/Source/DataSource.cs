using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OknoWpf.Data {
    interface IDataSource<T> where T : new() {
        DataStorage<T> Read();
        void Write(DataStorage<T> item);
    }
}
