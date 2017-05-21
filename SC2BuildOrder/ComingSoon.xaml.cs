using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Shell;

namespace SC2BuildOrder
{
    public partial class ComingSoon : PhoneApplicationPage
    {
        public ComingSoon()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // turn on antilock screen
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            Random rand = new Random();
            int n = rand.Next(1, 48);
            // while (n == 13 || n == 45) { n = rand.Next(1, 51); }
            
            String bgpath = "/Images/Backgrounds/bg" + n + ".jpg/";
            BitmapImage bitmapImage = new BitmapImage(new Uri(bgpath, UriKind.Relative));
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmapImage;
            LayoutRoot.Background = imageBrush;
        }
    }
}