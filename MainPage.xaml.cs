
using System;
using System.Drawing;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

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

            //line.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            //line_max.Stroke = new SolidColorBrush(Windows.UI.Colors.Green);
            line.X1 = left_eye.ActualOffset.X + left_eye.Width/2;
            line.Y1 = left_eye.ActualOffset.Y + left_eye.Height/2;            
            line_max.X1 = left_eye.ActualOffset.X + left_eye.Width / 2;
            line_max.Y1 = left_eye.ActualOffset.Y + left_eye.Height / 2;

            //line2.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            //line_max2.Stroke = new SolidColorBrush(Windows.UI.Colors.Green);
            line2.X1 = right_eye.ActualOffset.X + right_eye.Width / 2;
            line2.Y1 = right_eye.ActualOffset.Y + right_eye.Height / 2;
            line_max2.X1 = right_eye.ActualOffset.X + right_eye.Width / 2;
            line_max2.Y1 = right_eye.ActualOffset.Y + right_eye.Height / 2;

            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            line.X2 = pointerPosition.X - Window.Current.Bounds.X;
            line.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
            
            The_eye eye = new The_eye();
            eye.Init_eye(Main_canvas);

        }

        static bool IsInside(double circle_x, double circle_y, double rad, double x, double y)
        {
            if ((x - circle_x) * (x - circle_x) + (y - circle_y) * (y - circle_y) <= rad * rad)
                return true;
            else
                return false;
        }

        private static double GetAngleOfLineBetweenTwoPoints(PointF p1, PointF p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;
            return Math.Atan2(yDiff, xDiff) * (180 / Math.PI);
        }
        private async void Refresh_sight(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                await Task.Delay(30);
                var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;

                //left eye
                line.X2 = pointerPosition.X - Window.Current.Bounds.X;
                line.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;

                double circle_x = left_eye.ActualOffset.X + left_eye.Width / 2;
                double circle_y = left_eye.ActualOffset.Y + left_eye.Height / 2;
                double x = pointerPosition.X - Window.Current.Bounds.X;
                double y = pointerPosition.Y - Window.Current.Bounds.Y;
                double rad = left_eye.Width / 2;
                if (IsInside(circle_x, circle_y, rad, x, y))
                {
                    line_max.X2 = pointerPosition.X - Window.Current.Bounds.X;
                    line_max.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
                    Canvas.SetLeft(left_iris, line.X2);
                    Canvas.SetTop(left_iris, line.Y2);
                }
                else
                {
                    PointF p1 = new PointF((float)line.X1, (float)line.Y1);
                    PointF p2 = new PointF((float)line.X2, (float)line.Y2);
                    double angle = GetAngleOfLineBetweenTwoPoints(p1, p2);
                    angle = angle - 90;
                    double x1 = line.X1 + rad * Math.Sin(-angle * Math.PI / 180f);
                    double y1 = line.Y1 + rad * Math.Cos(-angle * Math.PI / 180f);
                    line_max.X2 = x1;
                    line_max.Y2 = y1;
                    Canvas.SetLeft(left_iris, line_max.X2 - left_iris.ActualWidth / 2);
                    Canvas.SetTop(left_iris, line_max.Y2 - left_iris.ActualWidth / 2);
                }

                //right eye
                line2.X2 = pointerPosition.X - Window.Current.Bounds.X;
                line2.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;

                circle_x = right_eye.ActualOffset.X + right_eye.Width / 2;
                circle_y = right_eye.ActualOffset.Y + right_eye.Height / 2;
                x = pointerPosition.X - Window.Current.Bounds.X;
                y = pointerPosition.Y - Window.Current.Bounds.Y;
                rad = right_eye.Width / 2;
                if (IsInside(circle_x, circle_y, rad, x, y))
                {
                    line_max2.X2 = pointerPosition.X - Window.Current.Bounds.X;
                    line_max2.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
                    Canvas.SetLeft(right_iris, line2.X2);
                    Canvas.SetTop(right_iris, line2.Y2);
                }
                else
                {
                    PointF p1 = new PointF((float)line2.X1, (float)line2.Y1);
                    PointF p2 = new PointF((float)line2.X2, (float)line2.Y2);
                    double angle = GetAngleOfLineBetweenTwoPoints(p1, p2);
                    angle = angle - 90;
                    double x1 = line2.X1 + rad * Math.Sin(-angle * Math.PI / 180f);
                    double y1 = line2.Y1 + rad * Math.Cos(-angle * Math.PI / 180f);
                    line_max2.X2 = x1;
                    line_max2.Y2 = y1;
                    Canvas.SetLeft(right_iris, line_max2.X2 - right_iris.ActualWidth / 2);
                    Canvas.SetTop(right_iris, line_max2.Y2 - right_iris.ActualWidth / 2);
                }
                Main_canvas.UpdateLayout();
            }
        }
    }
}
