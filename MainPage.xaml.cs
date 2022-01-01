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
            eye_left.Init_eye(Canvas_main, "left", false);
            eye_right.Init_eye(Canvas_main, "right", false);
        }
    }
}
