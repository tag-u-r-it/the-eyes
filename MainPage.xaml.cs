using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            ApplicationView.PreferredLaunchViewSize = new Windows.Foundation.Size(600, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }
        private void Set_eyes(object sender, RoutedEventArgs e)
        {
            The_eye eye_left = new The_eye();
            The_eye eye_right = new The_eye();
            eye_left.Init_eye(Canvas_main, Canvas_main.ActualWidth / 4, Canvas_main.ActualHeight / 2, 0, false);
            eye_right.Init_eye(Canvas_main, Canvas_main.ActualWidth / 2, Canvas_main.ActualHeight / 2, 1, false);

            //eye_left.Update_position_on_canvas(Canvas_main, eye_left, Canvas_main.ActualWidth / 4, Canvas_main.ActualHeight / 2);
        }
    }
}
