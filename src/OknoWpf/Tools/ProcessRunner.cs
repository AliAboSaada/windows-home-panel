using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OknoWpf.Logic {
    public class ProcessRunner {
        public static void Run(String processName, String arguments) {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo {
                FileName = processName,
                Arguments =arguments
            };
            p.Start();
        }
    }
}
