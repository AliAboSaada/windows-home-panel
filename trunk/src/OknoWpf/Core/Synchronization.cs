using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OknoWpf.Core {
    public class Synchronization {
        public static void Wait(Func<bool> condition, TimeSpan timeout) {
            DateTime dtStart = DateTime.Now;

            while (condition()) {
                Thread.Sleep(20);

                if (DateTime.Now.Subtract(dtStart) > timeout) {
                    throw new TimeoutException();
                }
            }
        }
    }
}
