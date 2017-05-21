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
using Microsoft.Phone.Shell;

namespace SC2BuildOrder
{
    public partial class CounterUnits : PhoneApplicationPage
    {
        public IdleDetectionMode UserIdleDetectionMode;
        public ObservableCollection<ItemViewModel> Units { get; private set; }
        public ObservableCollection<Object_SC> Object_SC_Table;
        private string mRace;

        public CounterUnits()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
            this.Units = new ObservableCollection<ItemViewModel>();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // will let your app run under the lock screen if the user locks it.
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled; 
            // turn on antilock screen
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            mRace = NavigationContext.QueryString["race"];
            LoadData(mRace);
            unit_list.ItemsSource = Units;

        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // will let your app run under the lock screen if the user locks it.
            //PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Enabled; 
            // turn off antilock screen
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
        }


        public void LoadData(String mRace)
        {
            Units.Clear();
            DB_Helper.connect();
            //Load Units
            Object_SC_Table = DB_Helper.get_Object_SC_by_Type_and_Race(mRace, DB_Helper.UNIT);
            foreach (Object_SC obj in Object_SC_Table)
            {
                Units.Add(new ItemViewModel() { ObjId = obj.Obj_Id, Name = obj.Name, Path = obj.Imagesource, Strong1=obj.Strong1, Strong2=obj.Strong2, Strong3=obj.Strong3, Weak1=obj.Weak1, Weak2=obj.Weak2,Weak3=obj.Weak3 });
            }

        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/InfoPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}