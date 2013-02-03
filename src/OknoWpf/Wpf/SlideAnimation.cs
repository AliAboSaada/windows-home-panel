using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;

namespace OknoWpf.Wpf {
    public class SlideAnimation {
        private CallbackTimer timer = new CallbackTimer();

        private bool wasSlowDown;

        public double CurrentPosition { get; private set; }
        public double StartPosition { get; set; }
        public double EndPosition { get; set; }
        public double StepValue { get; private set; }
        public Dispatcher Dispatcher { get; set; }
        public int FramesPerSecond { get; set; }
        public double SlideSpeed { get; set; }

        public bool IsWorking { get; private set; }

        public event Action<double> PositionChanged;
        public event Action Finished;
        
        public SlideAnimation() {
            SlideSpeed = 7.0;
            FramesPerSecond = 50;

            timer.WorkingCondition = TimerWorkingCondition;
            timer.Elapsed += TimerElapsed;
            timer.Interval = 1000 / FramesPerSecond;
            timer.Stopped += TimerStopped;
        }

        public void StartAnimation() {
            if (IsWorking) {
                return;
            }
            wasSlowDown = false;
            CurrentPosition = StartPosition;
            CalculateStepValue();
            CalculateOrientation();

            if (StepValue == 0) {
                StopAnimation();
            } else {
                timer.Start();
                IsWorking = true;
            }
        }

        private void CalculateOrientation() {
            LeftToRight = StartPosition < EndPosition;
        }

        public void StopAnimation() {
            CurrentPosition = EndPosition;
            timer.Stop();
        }

        private void TimerStopped() {
            if (Dispatcher != null) {
                Dispatcher.Invoke(new Action(() => {
                    if (Finished != null) {
                        Finished();
                    }
                }));
            } else {
                if (Finished != null) {
                    Finished();
                }
            }
            IsWorking = false;
        }

        private void CalculateStepValue() {
            StepValue = (EndPosition - StartPosition) / FramesPerSecond * SlideSpeed;
        }

        private void TimerElapsed() {
            if (IsFinishingSlide() && !wasSlowDown) {
                SlowDown();
            }

            if (Math.Abs(CurrentPosition - EndPosition) < Math.Abs(StepValue))
                CurrentPosition = EndPosition;
            else
                CurrentPosition += StepValue;

            if (Dispatcher != null) {
                Dispatcher.Invoke(new Action(() => {
                    if (PositionChanged != null) {
                        PositionChanged(CurrentPosition);
                    }
                }));
            } else {
                if (PositionChanged != null) {
                    PositionChanged(CurrentPosition);
                }
            }
        }

        private void SlowDown() {
            StepValue /= 3;
            wasSlowDown = true;
        }

        private bool IsFinishingSlide() {
            double diff = Math.Abs(EndPosition - CurrentPosition);
            return diff < 40;
        }

        private bool TimerWorkingCondition() {
            return CurrentPosition != EndPosition;
        }

        public bool LeftToRight {
            get { return StepValue > 0; }
            private set {
                if (value) {
                    if (StepValue < 0)
                        StepValue *= -1;
                } else {
                    if (StepValue > 0)
                        StepValue *= -1;
                }
            }
        }
    }
}
