using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OknoWpf.Core.Diagnostics {
    public class TimeCounter {
        private static DateTime? firstDate;
        private static DateTime lastDate;

        public static void Report(string message = "") {
            if (firstDate == null) {
                firstDate = DateTime.Now;
                Debug.Print(message + " " + firstDate.Value.ToString("MM:ss:fff"));
            } else {
                DateTime nlastDate = DateTime.Now;
                if (lastDate == DateTime.MinValue) {
                    lastDate = firstDate.Value;
                }
                Debug.Print(message + " " + nlastDate.ToString("MM:ss:fff") + " td:" + nlastDate.Subtract(firstDate.Value).TotalMilliseconds + "ms" + " d:" + nlastDate.Subtract(lastDate).TotalMilliseconds + "ms");
                lastDate = nlastDate;
            }
        }
    }
}
