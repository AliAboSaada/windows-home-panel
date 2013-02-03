using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OknoWpf.Wpf;
using OknoWpf.Core;

namespace Tests {
    [TestClass]
    public class SlideAnimationTest {
        private SlideAnimation slideAnimation;

        [TestInitialize]
        public void Initialize() {
            slideAnimation = new SlideAnimation();
            slideAnimation.Finished += new Action(() => {
                Assert.IsTrue(slideAnimation.CurrentPosition == slideAnimation.EndPosition);
            });
        }
        [TestMethod]
        public void SlideDownTest() {
            SlideTest(isSlideDown: true);
        }
        [TestMethod]
        public void SlideUpTest() {
            SlideTest(isSlideDown: false);
        }
        private void SlideTest(bool isSlideDown) {
            slideAnimation.StartPosition = (isSlideDown ? 100 : 1430);
            slideAnimation.EndPosition = (isSlideDown ? 1430 : 100);

            slideAnimation.StartAnimation();

            try {
                Synchronization.Wait(() =>
                    slideAnimation.IsWorking, TimeSpan.FromSeconds(500));
            } catch (TimeoutException ex) { 
                throw new Exception(String.Format("Timeout. Current position: 0", slideAnimation.CurrentPosition));
            }
        }
    }
}
