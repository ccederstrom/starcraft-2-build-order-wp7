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
using System.ComponentModel;
using Microsoft.Phone.Shell;

namespace SC2BuildOrder
{
    public partial class BuiltInOrder : PhoneApplicationPage
    {
        private int selectedIndex;
        private String mRace;
        private int index_id;
        private String buildname;
        public ObservableCollection<Build_Order> builds { get; private set; }
        public ObservableCollection<Object_SC> units { get; private set; }
        public ObservableCollection<unitforBuildOrder> current_build_order { get; private set; }
        public BuiltInOrder()
        {
            InitializeComponent();
            builds = new ObservableCollection<Build_Order>();
            units = new ObservableCollection<Object_SC>();
            current_build_order = new ObservableCollection<unitforBuildOrder>();
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
            bghandler.Background = imageBrush;
            //Current Build Panel
            bgpath = "/Images/Backgrounds/bg_translucent_darker.png/";
            bitmapImage = new BitmapImage(new Uri(bgpath, UriKind.Relative));
            imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmapImage;
            LayoutRoot.Background = imageBrush;
            //Query Database
            mRace = NavigationContext.QueryString["race"];
            buildname = NavigationContext.QueryString["buildname"];
            index_id = Int16.Parse(NavigationContext.QueryString["index_id"]);
            PageTitle.Text = buildname;
            DB_Helper.connect();
            builds = DB_Helper.get_Build_Order_By_Index_Id(index_id);
            for (int i = 0; i < builds.Count; i++)
            {
                Build_Order current_step = builds.ElementAt<Build_Order>(i);
                Object_SC unit = DB_Helper.get_Object_SC_by_Obj_Id(current_step.Obj_Id).ElementAt<Object_SC>(0);

                current_build_order.Add(new unitforBuildOrder(unit.Name,unit.Food,unit.Mineral,unit.Gas,unit.Time,unit.Imagesource,"@"+current_step.When,current_step.Note));
            }
            build.ItemsSource = current_build_order;
            Before.Text = NavigationContext.QueryString["note"];
            if (Before.Text == "") Before_Button.Visibility = Visibility.Collapsed;
            After.Text = NavigationContext.QueryString["notefooter"];
            if (After.Text == "") After_Button.Visibility = Visibility.Collapsed;

        }

        private void Before_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Before.Visibility == Visibility.Visible)
                Before.Visibility = Visibility.Collapsed;
            else Before.Visibility = Visibility.Visible;
        }
        private void After_Button_Click(object sender, RoutedEventArgs e)
        {
            if (After.Visibility == Visibility.Visible)
                After.Visibility = Visibility.Collapsed;
            else After.Visibility = Visibility.Visible;
        }
        private void Before_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Before.Visibility = Visibility.Collapsed;
        }
        private void After_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            After.Visibility = Visibility.Collapsed;
        }
    }

    // we need ObjectSC info from ObjectSC class and "when", "note" from Build_Order class.
    // we cannot bind 2 lists to 1 listbox
    // so I create this class to contain all infomation we need
    public class unitforBuildOrder : Object_SC
    {    
        public unitforBuildOrder(String name, int food, int mineral, int gas, int time, String path, String when, String note)
        {
            Name = name;
            Food = food;
            Mineral = mineral;
            Gas = gas;
            Time = time;
            Imagesource = path;
            _When = when;
            _Note = note;
        }

        // Define item name: private field, public property and database column.
        private String _When;

        public String When
        {
            get
            {
                return _When;
            }
            set
            {
                if (_When != value)
                {
                    NotifyPropertyChanging("When");
                    _When = value;
                    NotifyPropertyChanged("When");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private String _Note;

        public String Note
        {
            get
            {
                return _Note;
            }
            set
            {
                if (_Note != value)
                {
                    NotifyPropertyChanging("Note");
                    _Note = value;
                    NotifyPropertyChanged("Note");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}