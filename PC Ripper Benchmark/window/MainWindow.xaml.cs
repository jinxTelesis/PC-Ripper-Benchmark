﻿using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.util;
using System.Windows;
using System.Windows.Media;
using static PC_Ripper_Benchmark.function.RipperTypes;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Author(s): Anthony Jaghab, David Hartglass, (c) all rights reserved.
    /// </summary>

    public partial class MainWindow : Window {

        #region Instance member(s), and enum(s).        

        private RipperSettings rs;

        #endregion

        #region Constructor(s) and method(s).

        /// <summary>
        /// Default constructor for the <see cref="MainWindow"/>.
        /// </summary>

        public MainWindow() {
            InitializeComponent();

            this.rs = new RipperSettings();
            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));

            this.tabComponents.ItemContainerStyle = s;
            this.tabComponents.SelectedIndex = 0;
        }

        private void ShowTabWindow(Tab theTab) {

            switch (theTab) {
                case Tab.WELCOME: {
                    this.tabComponents.SelectedIndex = (int)Tab.WELCOME;
                    return;
                }

                case Tab.CPU: {
                    this.tabComponents.SelectedIndex = (int)Tab.CPU;
                    return;
                }

                case Tab.DISK: {
                    this.tabComponents.SelectedIndex = (int)Tab.DISK;
                    return;
                }

                case Tab.RAM: {
                    this.tabComponents.SelectedIndex = (int)Tab.RAM;
                    return;
                }

                case Tab.GPU: {
                    this.tabComponents.SelectedIndex = (int)Tab.GPU;
                    return;
                }

                case Tab.SETTINGS: {
                    this.tabComponents.SelectedIndex = (int)Tab.SETTINGS;
                    return;
                }

                case Tab.RUNNING_TEST: {
                    this.tabComponents.SelectedIndex = (int)Tab.RUNNING_TEST;
                    return;
                }
            }
        }

        #endregion

        #region Event(s) and event handler(s).

        private void BtnTemp_Click(object sender, RoutedEventArgs e) {
            new QuestionaireWindow().Show();
        }

        private void BtnCPU_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            this.btnCPU.Foreground = Brushes.Salmon;
        }

        private void BtnCPU_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            this.btnCPU.Foreground = Brushes.Black;
        }

        private void BtnCPU_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.CPU);
        }

        private void GrdCPUTests_SizeChanged(object sender, SizeChangedEventArgs e) {
            //double widthDifference = System.Math.Abs(grdCPUTests.ActualWidth - e.NewSize.Width - e.PreviousSize.Width);

            //this.imgSuccessorship.Margin = new Thickness(this.imgSuccessorship.Margin.Left + widthDifference,
            //    this.imgSuccessorship.Margin.Top, this.imgSuccessorship.Margin.Right,
            //    this.imgSuccessorship.Margin.Bottom);

            //this.imgBoolean.Margin = new Thickness(this.imgBoolean.Margin.Left + widthDifference,
            //    this.imgBoolean.Margin.Top, this.imgBoolean.Margin.Right,
            //    this.imgBoolean.Margin.Bottom);

            //this.imgQueue.Margin = new Thickness(this.imgQueue.Margin.Left + widthDifference,
            //    this.imgQueue.Margin.Top, this.imgQueue.Margin.Right,
            //    this.imgQueue.Margin.Bottom);

            //this.imgLinkedList.Margin = new Thickness(this.imgLinkedList.Margin.Left + widthDifference,
            //    this.imgLinkedList.Margin.Top, this.imgLinkedList.Margin.Right,
            //    this.imgLinkedList.Margin.Bottom);

            //this.imgTree.Margin = new Thickness(this.imgTree.Margin.Left + widthDifference,
            //    this.imgTree.Margin.Top, this.imgTree.Margin.Right,
            //    this.imgTree.Margin.Bottom);

        }

        #endregion

        private void BtnRunTest_Click(object sender, RoutedEventArgs e) {
            CPUFunctions cpu = new CPUFunctions(ref this.rs);
            CPUResults results = new CPUResults(this.rs);

            ThreadType threadType;
            if (this.radSingle.IsChecked == true) {
                threadType = ThreadType.Single;
            } else if (this.radMultithread.IsChecked == true) {
                threadType = ThreadType.Multithreaded;
            } else {
                MessageBox.Show("Please select a type of test you'd like to perform.", 
                    "RipperUnknownTestException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            // SingleUI is going to cause problems because it is
            // threaded and it doesn't call constructor yet.
            // before we use the contents of that object.
            // we need to wait for it.

            this.txtResults.Document.Blocks.Clear();

            try {
                results = cpu.RunCPUBenchmark(threadType);
            } catch (RipperThreadException ex) {
                MessageBox.Show($"Oh no. A Ripper thread exception occured.. {ex.ToString()}");
            }

            this.txtResults.AppendText($"Successfully ran the CPU test! Below is the " +
                $"results of the test.\n\n" +
                $"{results.Description}\n\n" +
                $"\n\n");

            this.txtRunningTest.Text = "Results for the CPU test:";


            ShowTabWindow(Tab.RUNNING_TEST);
        }

    }
}
