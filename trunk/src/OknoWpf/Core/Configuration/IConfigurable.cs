using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OknoWpf.Core;

namespace OknoWpf.Data {
    public interface IConfigurable {
        void Configure(ConfigurationData data);
    }
}
