using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace OknoWpf.Wpf {
    public class CallbackTimer {
        private Timer timer = new Timer();
        private Object synch = new Object();
        public event Action Elapsed;
        public Func<bool> WorkingCondition { get; set; }
        public event Action Stopped;

        public int Interval { get; set; }

        public CallbackTimer() {
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        public void Start() {
            timer.Interval = Interval;
            timer.Start();
        }

        public void Stop() {
            timer.Stop();

            if (Stopped != null) {
                Stopped();
            }
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e) {
            lock (synch) {
                if (Elapsed != null) {
                    Elapsed();
                }
                if (WorkingCondition != null) {
                    if (!WorkingCondition()) {
                        Stop();
                    }
                }
            }
        }
    }
}
