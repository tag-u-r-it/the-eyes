using System;
using System.Drawing;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace the_eyes
{
    class The_eye
    {
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
        private PointF Iris_coord = new PointF();
        private void Set_iris_coord(PointF point)
        {
            Iris_coord = point;
        }
        private async void Update_position_on_canvas(Canvas can, Ellipse eye, Ellipse iris, Line line, Line line_max, int id)
        {
            while(true)
            {
                await Task.Delay(60);
                if (id == 0) Canvas.SetLeft(eye, can.ActualWidth / 4 + eye.ActualWidth/2);
                if (id == 1) Canvas.SetLeft(eye, can.ActualWidth / 2 + eye.ActualWidth/2);
                Canvas.SetTop(eye, can.ActualHeight / 2);

                Canvas.SetLeft(iris, Iris_coord.X);
                Canvas.SetTop(iris, Iris_coord.Y);

                line.X1 = eye.ActualOffset.X + eye.Height / 2;
                line.Y1 = eye.ActualOffset.Y + eye.Height / 2;

                line_max.X1 = eye.ActualOffset.X + eye.Height / 2;
                line_max.Y1 = eye.ActualOffset.Y + eye.Height / 2;
            }
        }
        private async void Refresh_eye(Canvas can, Ellipse eye, Ellipse iris, Line line, Line line_max)
        {
            while (true)
            {
                await Task.Delay(30);
                var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
                line.X2 = pointerPosition.X - Window.Current.Bounds.X;
                line.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;

                double circle_x = eye.ActualOffset.X + eye.Width / 2;
                double circle_y = eye.ActualOffset.Y + eye.Height / 2;
                double x = pointerPosition.X - Window.Current.Bounds.X;
                double y = pointerPosition.Y - Window.Current.Bounds.Y;
                double rad = eye.Width / 2;
                if (IsInside(circle_x, circle_y, rad, x, y))
                {
                    line_max.X2 = pointerPosition.X - Window.Current.Bounds.X;
                    line_max.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
                    Canvas.SetLeft(iris, line.X2);
                    Canvas.SetTop(iris, line.Y2);
                }
                else
                {
                    PointF p1 = new PointF((float)line.X1, (float)line.Y1);
                    PointF p2 = new PointF((float)line.X2, (float)line.Y2);
                    double angle = GetAngleOfLineBetweenTwoPoints(p1, p2);
                    angle -= 90;
                    double x1 = line.X1 + rad * Math.Sin(-angle * Math.PI / 180f);
                    double y1 = line.Y1 + rad * Math.Cos(-angle * Math.PI / 180f);
                    line_max.X2 = x1;
                    line_max.Y2 = y1;
                    Canvas.SetLeft(iris, line_max.X2 - iris.ActualWidth / 2);
                    Canvas.SetTop(iris, line_max.Y2 - iris.ActualWidth / 2);
                }
                PointF iris_point = new PointF((float)iris.ActualOffset.X, (float)iris.ActualOffset.Y);
                Set_iris_coord(iris_point);
                can.UpdateLayout();
            }
        }
        public void Init_eye(Canvas can, double pos_x, double pos_y, int id, bool debug)
        {
            Ellipse eye = new Ellipse();
            eye.Width = 100;
            eye.Height = 100;
            eye.Fill = new SolidColorBrush(Windows.UI.Colors.WhiteSmoke);
            can.Children.Add(eye);
            Canvas.SetLeft(eye, pos_x);
            Canvas.SetTop(eye, pos_y);

            Ellipse iris = new Ellipse();
            iris.Width = 20;
            iris.Height = 20;
            iris.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
            can.Children.Add(iris);
            Canvas.SetLeft(iris, eye.ActualOffset.X);
            Canvas.SetTop(iris, eye.ActualOffset.Y);

            Line line = new Line();
            if (debug) line.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            line.StrokeThickness = 1;
            line.X1 = eye.ActualOffset.X + eye.Height / 2;
            line.Y1 = eye.ActualOffset.Y + eye.Height / 2;
            can.Children.Add(line);
            Canvas.SetLeft(line, 0);
            Canvas.SetTop(line, 0);

            Line line_max = new Line();
            if (debug) line_max.Stroke = new SolidColorBrush(Windows.UI.Colors.Green);
            line_max.StrokeThickness = 1;
            line_max.X1 = eye.ActualOffset.X + eye.Height / 2;
            line_max.Y1 = eye.ActualOffset.Y + eye.Height / 2;
            can.Children.Add(line_max);
            Canvas.SetLeft(line_max, 0);
            Canvas.SetTop(line_max, 0);

            Refresh_eye(can, eye, iris, line, line_max);
            Update_position_on_canvas(can, eye, iris, line, line_max, id);
        }
    }
}
