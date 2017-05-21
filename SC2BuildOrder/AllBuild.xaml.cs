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
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Shell;

namespace SC2BuildOrder
{
    public partial class AllBuild : PhoneApplicationPage
    {
        private String mRace;
        public AllBuild()
        {
            InitializeComponent();

            DataContext = App.ViewModel;
            //this.Loaded += new RoutedEventHandler(Page_Loaded);
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // turn on antilock screen
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            //Set BackGround 
            Random rand = new Random();
            int n = rand.Next(1, 48);
            // while (n == 6 || n == 13 || n == 45) { n = rand.Next(1, 51); }
            String bgpath = "/Images/Backgrounds/bg" + n + ".jpg/";
            BitmapImage bitmapImage = new BitmapImage(new Uri(bgpath, UriKind.Relative));
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmapImage;
            imageBrush.Stretch = Stretch.UniformToFill;
            imageBrush.AlignmentX = 0;
            imageBrush.AlignmentY = 0;
            bghandler.Background = imageBrush;
            //Query Database
            mRace = NavigationContext.QueryString["race"];
            Debug.WriteLine("Race is "+mRace);
            DB_Helper.connect();
            built_in.ItemsSource = DB_Helper.get_Index_Built_In(mRace);
            custom.ItemsSource = DB_Helper.get_Index_By_Author("Custom",mRace);
        }
        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (!App.ViewModel.IsDataLoaded)
        //    {
        //        App.ViewModel.LoadData();
        //    }
        //}

        private void built_in_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Index item = built_in.SelectedItem as Index;
            //openBuildOrder(item.Id,item.Name);
            if (item != null)
            {
                String path = "/BuiltInOrder.xaml?index_id=" + item.Id + "&race=" + mRace + "&buildname=" + item.Name + "&note=" + item.Note + "&notefooter=" + item.Note_Footer;
                NavigationService.Navigate(new Uri(path, UriKind.RelativeOrAbsolute));
            }
        }

        private void custom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Index item = custom.SelectedItem as Index;
            if(item!=null)
                openBuildOrder(item.Id,item.Name);
        }

        private void openBuildOrder(int index_id, String buildname)
        {
            String path = "/BuildOrder.xaml?index_id=" + index_id + "&race=" + mRace + "&buildname=" + buildname;
            NavigationService.Navigate(new Uri(path, UriKind.RelativeOrAbsolute));
        }
    }
}