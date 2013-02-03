using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OknoWpf.Core {
    [Serializable]
    public class ConfigurationData {
        public String Key { get; set; }
        public String Value { get; set; }

        public List<ConfigurationData> Configurations { get; set; } 

        public String GetValue(String key) {
            foreach (ConfigurationData d in Configurations) {
                if (d.Key == key) {
                    return d.Value;
                }
            }
            return null;
        }

        public ConfigurationData() {
            Configurations = new List<ConfigurationData>();
        }

        public ConfigurationData AddConfiguration(string key, string value) {
            ConfigurationData data = null;
            Configurations.Add(data = new ConfigurationData {
                Key = key,
                Value = value
            });
            return data;
        }

        public ConfigurationData GetConfiguration(string key) {
            foreach (ConfigurationData d in Configurations) {
                if (d.Key == key) {
                    return d;
                }
            }
            return null;
        }
    }
}
