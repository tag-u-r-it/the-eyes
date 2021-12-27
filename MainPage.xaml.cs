﻿
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace the_eyes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            line.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            line.X1 = left_eye.ActualOffset.X + left_eye.Width/2;
            line.Y1 = left_eye.ActualOffset.Y + left_eye.Height/2;

            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            line.X2 = pointerPosition.X - Window.Current.Bounds.X;
            line.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
            
        }
        private async void refresh_sight(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                await Task.Delay(30);
                var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
                line.X2 = pointerPosition.X - Window.Current.Bounds.X;
                line.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
                if (line.X2 < left_eye.ActualOffset.X-15 + left_eye.Width && line.X2 > left_eye.ActualOffset.X)
                {
                    Canvas.SetLeft(left_iris, line.X2);
                }
                if (line.Y2 < left_eye.ActualOffset.Y-15 + left_eye.Height && line.Y2 > left_eye.ActualOffset.Y)
                {
                    Canvas.SetTop(left_iris, line.Y2);
                }
                Main_grid.UpdateLayout();
            }
        }
    }
}
