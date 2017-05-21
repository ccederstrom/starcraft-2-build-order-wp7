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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Shell;

namespace SC2BuildOrder
{
    public partial class BuildOrder : PhoneApplicationPage
    {
        private int selectedIndex;
        private String mRace;
        private int index_id;
        private String buildname;
        public ObservableCollection<Build_Order> builds { get; private set; }
        public ObservableCollection<Object_SC> units { get; private set; }
        public ObservableCollection<Object_SC> current_build_order { get; private set; }
        public BuildOrder()
        {
            InitializeComponent();
            builds = new ObservableCollection<Build_Order>();
            units = new ObservableCollection<Object_SC>();
            current_build_order = new ObservableCollection<Object_SC>();
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
            LayoutRoot.Background = imageBrush;
            //Current Build Panel
            bgpath = "/Images/Backgrounds/bg_translucent.png/";
            bitmapImage = new BitmapImage(new Uri(bgpath, UriKind.Relative));
            imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmapImage;
            currentbuild_panel.Background = imageBrush;
            //Query Database
            mRace = NavigationContext.QueryString["race"];
            buildname = NavigationContext.QueryString["buildname"];
            index_id =Int16.Parse(NavigationContext.QueryString["index_id"]);
            PageTitle.Text = buildname;
            DB_Helper.connect();
            builds = DB_Helper.get_Build_Order_By_Index_Id(index_id);
            for (int i = 0; i < builds.Count; i++)
            {
                current_build_order.Add(DB_Helper.get_Object_SC_by_Obj_Id(builds.ElementAt<Build_Order>(i).Obj_Id).ElementAt<Object_SC>(0));
            }
            build.ItemsSource=current_build_order;
            if (build.Items.Any())
            {
                selectedIndex = 0;
                build.SelectedIndex = selectedIndex;
                Object_SC item = (Object_SC)build.SelectedItem;
                //items.Background = new SolidColorBrush(Colors.Orange); 
                Debug.WriteLine("Initial selected index is "+build.SelectedIndex);
                Debug.WriteLine("selected name is " + item.Name);

                Uri uri = new Uri(item.Imagesource, UriKind.Relative);
                ImageSource imgSource = new BitmapImage(uri);
                current_icon.Source = imgSource;
                current_name.Text = item.Name;
                current_mineral.Text = item.Mineral+"";
                current_gas.Text = item.Gas+"";
                current_food.Text = item.Food+"";
                current_time.Text = item.Time+"";
            }
        }
        private void NextStep_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (build.Items.Any())
            {
                if (build.Items.Count > selectedIndex + 1)
                {
                    selectedIndex++;
                    build.SelectedIndex = selectedIndex;
                    Object_SC item = (Object_SC)build.SelectedItem;
                    //items.Background = new SolidColorBrush(Colors.Orange); 
                    Debug.WriteLine("Initial selected index is " + build.SelectedIndex);
                    Debug.WriteLine("selected name is " + item.Name);

                    Uri uri = new Uri(item.Imagesource, UriKind.Relative);
                    ImageSource imgSource = new BitmapImage(uri);
                    current_icon.Source = imgSource;
                    current_name.Text = item.Name;
                    current_mineral.Text = item.Mineral + "";
                    current_gas.Text = item.Gas + "";
                    current_food.Text = item.Food + "";
                    current_time.Text = item.Time + "";
                }
                else
                {
                    MessageBox.Show("Done");
                }
            }
        }
    }
}