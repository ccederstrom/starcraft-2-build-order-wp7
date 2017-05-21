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
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Phone.Shell;

namespace SC2BuildOrder
{
    public partial class MainMenu : PhoneApplicationPage
    {
        String mTo;
        String mRace;
        Boolean onRaceChoser=false;
        public MainMenu()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // turn on antilock screen
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            RaceDialog.Visibility = Visibility.Collapsed;
            PageTitle.Visibility = Visibility.Visible;
            Random rand = new Random();
            int n = rand.Next(1,48);
            // while (n == 13||n==45) { n = rand.Next(1, 48); }
            Debug.WriteLine("n is "+n);
            if (n==6)
            {
                PageTitle.Text = " ";
            }
            else
            {
                PageTitle.Text = "SC2 Build Orders";
            }
            String bgpath = "/Images/Backgrounds/bg" + n + ".jpg/";
            BitmapImage bitmapImage = new BitmapImage(new Uri(bgpath, UriKind.Relative));
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmapImage;
            imageBrush.Stretch = Stretch.UniformToFill;
            imageBrush.AlignmentX = 0;
            imageBrush.AlignmentY = 0;
            LayoutRoot.Background = imageBrush;
            
        }
        private void Create_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //RaceDialog.Visibility = Visibility.Visible;
            //PageTitle.Visibility = Visibility.Collapsed;
            //mTo = "Create";
            //onRaceChoser = true;
                        
            NavigationService.Navigate(new Uri("/ComingSoon.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowBuildOrder_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            RaceDialog.Visibility = Visibility.Visible;
            PageTitle.Visibility = Visibility.Collapsed;
            mTo = "AllBuild";
            onRaceChoser = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            RaceDialog.Visibility = Visibility.Collapsed;
            PageTitle.Visibility = Visibility.Visible;
            onRaceChoser = false;
        }

        private void Terran_Click(object sender, RoutedEventArgs e)
        {
            mRace = "Terran";
            gotoPage();
        }

        private void Zerg_Click(object sender, RoutedEventArgs e)
        {
            mRace = "Zerg";
            gotoPage();
        }

        private void Protoss_Click(object sender, RoutedEventArgs e)
        {
            mRace = "Protoss";
            gotoPage();
        }
        private void gotoPage()
        {
            if (mTo != "Surprise")
            {
                String path = "/" + mTo + ".xaml?race=" + mRace;
                NavigationService.Navigate(new Uri(path, UriKind.RelativeOrAbsolute));
            }
            else 
            {
                DB_Helper.connect();
                ObservableCollection<Index> Built_in = new ObservableCollection<Index>();
                ObservableCollection<Index> Custom = new ObservableCollection<Index>();
                ObservableCollection<Index> All = new ObservableCollection<Index>();
                Random mRand = new Random();

                Built_in = DB_Helper.get_Index_Built_In(mRace);
                Custom = DB_Helper.get_Index_By_Author("Custom", mRace);
                All = Built_in;
                foreach (Index build in Custom){
                    All.Add(build);
                }
                if(All.Count==0)
                {
                    MessageBox.Show("There is no build order for this race");
                    RaceDialog.Visibility = Visibility.Collapsed;
                    PageTitle.Visibility = Visibility.Visible;
                    onRaceChoser = false;
                }
                else
                {
                    int chosen = mRand.Next(All.Count);
                    Index chosenBuild = All[chosen];
                    String path = "/BuiltInOrder.xaml?index_id=" + chosenBuild.Id + "&race=" + mRace + "&buildname=" + chosenBuild.Name + "&note=" + chosenBuild.Note + "&notefooter=" + chosenBuild.Note_Footer;
                    NavigationService.Navigate(new Uri(path, UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void Help_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/InfoPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CounterUnits_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            RaceDialog.Visibility = Visibility.Visible;
            PageTitle.Visibility = Visibility.Collapsed;
            mTo = "CounterUnits";
            onRaceChoser = true;
        }

        private void Surprise_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            RaceDialog.Visibility = Visibility.Visible;
            PageTitle.Visibility = Visibility.Collapsed;
            mTo = "Surprise";
            onRaceChoser = true;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (onRaceChoser == true)
            {
                e.Cancel = true;  //Cancels the default behavior.
                RaceDialog.Visibility = Visibility.Collapsed;
                PageTitle.Visibility = Visibility.Visible;
                onRaceChoser = false;
            }
        }
    }
}