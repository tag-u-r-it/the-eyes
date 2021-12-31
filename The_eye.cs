using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace the_eyes
{
    class The_eye
    {
        private async void Refresh_eye(Canvas can, Line line)
        {
            while (true)
            {
                await Task.Delay(30);
                var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
                line.X2 = pointerPosition.X - Window.Current.Bounds.X;
                line.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
            }
        }
        public void Init_eye(Canvas can)
        {
            Ellipse eye = new Ellipse();
            eye.Width = 100;
            eye.Height = 100;
            eye.Fill = new SolidColorBrush(Windows.UI.Colors.WhiteSmoke);
            can.Children.Add(eye);
            Canvas.SetLeft(eye, 100);
            Canvas.SetTop(eye, 100);

            Ellipse iris = new Ellipse();
            iris.Width = 20;
            iris.Height = 20;
            iris.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
            can.Children.Add(iris);
            Canvas.SetLeft(iris, eye.ActualOffset.X);
            Canvas.SetTop(iris, eye.ActualOffset.Y);

            Line line = new Line();
            line.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            line.StrokeThickness = 1;
            line.X1 = eye.ActualOffset.X + eye.Height / 2;
            line.Y1 = eye.ActualOffset.Y + eye.Height / 2;
            can.Children.Add(line);
            Canvas.SetLeft(line, 0);
            Canvas.SetTop(line, 0);

            Refresh_eye(can, line);
        }
    }
}
