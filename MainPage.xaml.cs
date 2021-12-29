
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

            line.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            line.X1 = left_eye.ActualOffset.X + left_eye.Width/2;
            line.Y1 = left_eye.ActualOffset.Y + left_eye.Height/2;
            line_max.Stroke = new SolidColorBrush(Windows.UI.Colors.Green);
            line_max.X1 = left_eye.ActualOffset.X + left_eye.Width / 2;
            line_max.Y1 = left_eye.ActualOffset.Y + left_eye.Height / 2;

            //line2.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            line2.X1 = right_eye.ActualOffset.X + right_eye.Width / 2;
            line2.Y1 = right_eye.ActualOffset.Y + right_eye.Height / 2;

            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            line.X2 = pointerPosition.X - Window.Current.Bounds.X;
            line.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;

        }

        static bool isInside(double circle_x, double circle_y,
                                  double rad, double x, double y)
        {
            if ((x - circle_x) * (x - circle_x) + (y - circle_y) * (y - circle_y) <= rad * rad)
                return true;
            else
                return false;
        }

        private async void refresh_sight(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                await Task.Delay(30);
                var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;

                //left eye
                line.X2 = pointerPosition.X - Window.Current.Bounds.X;
                line.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
                left_iris.CenterPoint = new System.Numerics.Vector3((float)line.X2, (float)line.Y2, 0.0f);
                //if (line.X2 < left_eye.ActualOffset.X-15 + left_eye.Width && line.X2 > left_eye.ActualOffset.X)
                //{
                //    Canvas.SetLeft(left_iris, line.X2);
                //}
                //if (line.Y2 < left_eye.ActualOffset.Y-15 + left_eye.Height && line.Y2 > left_eye.ActualOffset.Y)
                //{
                //    Canvas.SetTop(left_iris, line.Y2);
                //}
                double circle_x = left_eye.ActualOffset.X + left_eye.Width / 2;
                double circle_y = left_eye.ActualOffset.Y + left_eye.Height / 2;
                double x = pointerPosition.X - Window.Current.Bounds.X;
                double y = pointerPosition.Y - Window.Current.Bounds.Y;
                double rad = left_eye.Width / 2;
                if (isInside(circle_x, circle_y, rad, x, y))
                {
                    line_max.X2 = pointerPosition.X - Window.Current.Bounds.X;
                    line_max.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
                    Canvas.SetLeft(left_iris, line.X2);
                    Canvas.SetTop(left_iris, line.Y2);
                }
                else
                {

                    debug.Text = (line.X2 - line.X1).ToString();
                }

                //right eye
                line2.X2 = pointerPosition.X - Window.Current.Bounds.X;
                line2.Y2 = pointerPosition.Y - Window.Current.Bounds.Y;
                if (line2.X2 < right_eye.ActualOffset.X - 15 + right_eye.Width && line2.X2 > right_eye.ActualOffset.X)
                {
                    Canvas.SetLeft(right_iris, line2.X2);
                }
                if (line2.Y2 < right_eye.ActualOffset.Y - 15 + right_eye.Height && line2.Y2 > right_eye.ActualOffset.Y)
                {
                    Canvas.SetTop(right_iris, line2.Y2);
                }
                Main_canvas.UpdateLayout();
            }
        }
    }
}
