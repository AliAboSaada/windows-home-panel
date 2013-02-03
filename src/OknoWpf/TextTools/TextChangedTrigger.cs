using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Castle.Core.Logging;

namespace OknoWpf {
    public class TextChangedTrigger {
        private String text;
        private Timer timer;

        public ILogger Logger { get; set; }

        public event Action<String> TextChanged;

        public TextChangedTrigger() {
            timer = new Timer(500);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e) {
            if (TextChanged != null) {
                TextChanged(text);
            }
            timer.Stop();
        }

        public void registerChanged(string text) {
            this.text = text;
            startTimer();
        }

        private void startTimer() {
            timer.Stop();
            timer.Start();
        }
    }
}
