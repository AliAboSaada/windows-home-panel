using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OknoWpf.Wpf {
    public class AppDispatcher {
        public static void Run(Action action) {
            App.WindowDispatcher.Invoke(action);
        }
    }
}
