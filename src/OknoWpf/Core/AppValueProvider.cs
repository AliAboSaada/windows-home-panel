using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OknoWpf.Core;

namespace OknoWpf.Data {
    public class AppValueProvider {
        private VariablesStorage variablesStorage;

        public AppValueProvider(VariablesStorage variablesStorage) {
            this.variablesStorage = variablesStorage;
        }

        public String ResolveValue(String value) {
            String result = value;

            foreach (String key in variablesStorage.Keys) {
                result = result.Replace("%" + key + "%", variablesStorage[key]);
            }
            return result;
        }
    }
}

