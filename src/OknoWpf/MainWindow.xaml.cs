using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnManaged;
using OknoWpf.Wpf;
using System.Diagnostics;
using OknoWpf.Core.Diagnostics;
using OknoWpf.Models;
//using OknoWpf.Systems;

namespace OknoWpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private AppCommands appCommands;
        private TextChangedTrigger commandTextTrigger;
        private MainViewModel viewModel;
        private SlideAnimation slideAnimation;
        private bool isSlideDown;

        public MainWindow() {

            TimeCounter.Report("Start application");

            InitializeApp();
            InitializeLocals();
            InitializeComponent();
            
            TimeCounter.Report("Initialized components");
            
            InitializeCommands();
            InitializeHotKeys();
            InitializePosition();

            DataContext = viewModel = App.Resolve<MainViewModel>();

            TimeCounter.Report("Initialization finished");
        }

        private void InitializeApp() {
            App.Started(Dispatcher);
            TimeCounter.Report("Initialized app");
        }

        private MainViewModel GetViewModel() {
            return viewModel;
        }

        private void InitializeLocals() {
            slideAnimation = new SlideAnimation {
                Dispatcher = Dispatcher
            };
            appCommands = App.Resolve<AppCommands>();
            commandTextTrigger = App.Resolve<TextChangedTrigger>();

            slideAnimation.PositionChanged += new Action<double>(fadeAnimation_PositionChanged);
            slideAnimation.Finished += new Action(slideAnimation_Finished);
            commandTextTrigger.TextChanged += new Action<string>(commandTextTrigger_TextChanged);
            
            TimeCounter.Report("Initialized locals");
        }

        void slideAnimation_Finished() {
            isSlideDown = slideAnimation.LeftToRight;

            if (this.Width == wideViewWidth && !isWideView) {
                this.Width = shortViewWidth;
            }
        }

        void fadeAnimation_PositionChanged(double position) {
            this.Left = position;
        }

        void commandTextTrigger_TextChanged(string text) {
            GetViewModel().CommandText = text;
        }

        private void InitializePosition() {
            this.Left = SystemParameters.VirtualScreenWidth - this.Width;
            this.Top = 0;
            this.Height = SystemParameters.VirtualScreenHeight;
        }

        private void InitializeCommands() {
            appCommands.OpenMainWindow = new EventCommand(Show);
        }

        private void InitializeHotKeys() {
            //KeyboardManager.WindowsKeyPressed += new Action(KeyboardManager_WindowsKeyPressed);
            new HotKey(Key.None, KeyModifier.Ctrl, OpenMainWindow, true);

            TimeCounter.Report("Initialized hot keys");
        }

        void KeyboardManager_WindowsKeyPressed() {
            this.Activate();
        }

        private void OpenMainWindow(HotKey key) {
            this.Activate();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                GetViewModel().CommandText = FilterTextBox.Text;
                GetViewModel().AcceptCommand();
            } else if (isSlideDown) {
                SlideUpWindow();
            } else if (!isSlideDown && (e.Key == Key.LeftCtrl || e.Key == Key.Escape)) {
                SlideDownWindow();
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e) {
            commandTextTrigger.registerChanged(FilterTextBox.Text);
        }

        private void Window_LostFocus(object sender, RoutedEventArgs e) {
            
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e) {
            slideAnimation.StopAnimation();
        }

        private void Window_Activated(object sender, EventArgs e) {
            SlideUpWindow();
            this.Focus();
            FilterTextBox.Focus();
        }

        private void SlideUpWindow() {
            SlideUpWindow(SystemParameters.VirtualScreenWidth - this.Width);
        }

        private void SlideUpWindow(double endPosition) {
            if (slideAnimation.IsWorking) {
                slideAnimation.StopAnimation();
            }
            slideAnimation.StartPosition = this.Left;
            slideAnimation.EndPosition = endPosition;

            slideAnimation.StartAnimation();
        }

        private void SlideDownWindow() {
            if (slideAnimation.IsWorking) {
                slideAnimation.StopAnimation();
            }

            slideAnimation.StartPosition = this.Left;
            slideAnimation.EndPosition = SystemParameters.VirtualScreenWidth - 5;

            slideAnimation.StartAnimation();
        }

        private void Window_Deactivated(object sender, EventArgs e) {
            SlideDownWindow();
        }

        private void LastRunButton_Click(object sender, RoutedEventArgs e) {
            GetViewModel().ExecuteCommand("last run");
        }

        private void listView1_DragLeave(object sender, DragEventArgs e) {
            
        }

        private bool isWideView;
        private int shortViewWidth = 250;
        private int wideViewWidth = 700;

        private void grid_MouseUp(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Released) {
                if (isWideView) {
                    Left = SystemParameters.VirtualScreenWidth - wideViewWidth;
                    SlideUpWindow(SystemParameters.VirtualScreenWidth - shortViewWidth);
                } else {
                    Width = wideViewWidth;
                    Left = SystemParameters.VirtualScreenWidth - shortViewWidth;
                    SlideUpWindow();
                }
                isWideView = !isWideView;
            }
        }
        private ItemModel itemModelMouseOver;

        private void StackPanel_MouseMove(object sender, MouseEventArgs e) {
            Label txt = (Label)e.Source;
            if (e.LeftButton == MouseButtonState.Pressed) {
               
                DragDrop.DoDragDrop(txt, txt.Tag, DragDropEffects.Move);
            }
            itemModelMouseOver = (ItemModel)txt.Tag;
        }

        private void listView1_Drop(object sender, DragEventArgs e) {
            ItemModel model = (ItemModel)e.Data.GetData(typeof(ItemModel));

            if (model != null && itemModelMouseOver != null) {
                e.Handled = true;
                GetViewModel().Items.Remove(model);

                int idx = GetViewModel().Items.IndexOf(itemModelMouseOver);
                if (idx >= 0) {
                    GetViewModel().Items.Insert(idx, model);
                }
            }
        }
        
        private void listView1_PreviewDragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetData(typeof(ItemModel)) != null) {
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
        }
        
        private void Label_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Delete) {
                
                ItemModel item = (ItemModel)listView1.SelectedItem;

                if (item != null) {
                    int idx = GetViewModel().Items.IndexOf(item);
                    int nextIdx = 0;

                    if (idx < GetViewModel().Items.Count) {
                        nextIdx = idx;
                    } else if (GetViewModel().Items.Count == 1) {
                        nextIdx = -1;
                    }
                    GetViewModel().Items.Remove(item);

                    if (nextIdx >= 0)
                        listView1.SelectedIndex = nextIdx;
                }

            } else if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl)) {
                GetViewModel().ExecuteCommand("save all");
            }
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            ItemModel item = (ItemModel)((Label)e.Source).Tag;
            item.Mode = ItemModelMode.Edit;
        }

        private void txtItem_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                TextBox txt = (TextBox)e.Source;
                ItemModel item = (ItemModel)txt.Tag;
                item.Name = txt.Text;
                item.Mode = ItemModelMode.View;
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                if (e.ButtonState == MouseButtonState.Released) {
                    Debug.Write("sasa");
                }
            }
        }
    }
}
