
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
            The_eye eye_left = new The_eye();
            eye_left.Init_eye(Main_canvas, 300, 250, false);
            The_eye eye_right = new The_eye();
            eye_right.Init_eye(Main_canvas, 500, 250, false);
        }
    }
}
