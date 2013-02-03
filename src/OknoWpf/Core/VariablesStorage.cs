using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OknoWpf.Core {
    public class VariablesStorage {
        private Dictionary<String, String> dict = new Dictionary<string, string>();

        public String this[String key] {
            get {
                return dict[key];
            }
            set {
                dict.Add(key, value);
            }
        }

        public IEnumerable<String> Keys { get { return dict.Keys; } }
    }
}
