using Azure.AI.InkRecognizer;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InkRecognizerWishList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InkViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var language = "de-DE";
            this.viewModel = new InkViewModel(
                language,
                new InkRecognizer("766a8a28ed424794ab59f638f9dc82ce", "https://inkrecognizer-demo.cognitiveservices.azure.com", language));

            this.DataContext = this.viewModel;
        }

        private void inkCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Touch | CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Mouse;
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var uiStrokes = inkCanvas.InkPresenter.StrokeContainer.GetStrokes();

            this.viewModel.ClearStrokes();

            foreach (var stroke in uiStrokes)
            {
                var points = stroke.GetInkPoints().Select(p => new InkPoint((float)p.Position.X, (float)p.Position.Y)).ToList();
                this.viewModel.AddStroke(stroke.Id, points);
            }
            
            await this.viewModel.RecognizeCommand.ExecuteAsync();
        }

    }
}
