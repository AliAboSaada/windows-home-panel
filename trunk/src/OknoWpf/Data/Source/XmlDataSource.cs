using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace OknoWpf.Data {
    public class XmlDataSource<T> : IDataSource<T> where T: new() {
        private String filePath;

        public void Configure(String filePath) {
            this.filePath = filePath;
        }

        public DataStorage<T> Read() {
            if (!File.Exists(filePath)) {
                if (ReturnNewIfEmpty)
                    return new DataStorage<T>(new T());
                else
                    return null;
            }

            var serializer = new XmlSerializer(typeof(T));
            using (var fs = new FileStream(filePath, FileMode.Open)){
                T item = (T)serializer.Deserialize(fs);

                DataStorage<T> storage = new DataStorage<T>(item);
                return storage;
            }            
        }

        public void Write(DataStorage<T> item) {
            var serializer = new XmlSerializer(typeof(T));
            using (var fs = new FileStream(filePath, FileMode.Create)) {
                serializer.Serialize(fs, item.Item);
            }  
        }

        public bool ReturnNewIfEmpty { get; set; }
    }
}
