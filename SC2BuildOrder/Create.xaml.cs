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
using Coding4Fun.Phone.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Shell;
//using Coding4Fun.Phone.Controls;

namespace SC2BuildOrder
{
    public partial class Create : PhoneApplicationPage
    {
        private string name;
        private string desc =" ";
        private string mRace;
        private string mAuthor;
        public ObservableCollection<ItemViewModel> BuildOrder { get; private set; }
        public ObservableCollection<ItemViewModel> Units { get; private set; }
        public ObservableCollection<ItemViewModel> Buildings { get; private set; }
        public ObservableCollection<ItemViewModel> Upgrades { get; private set; }
        public ObservableCollection<Object_SC> Object_SC_Table;

        // Constructor
        public Create()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            BuildOrder = new ObservableCollection<ItemViewModel>();
            this.Units = new ObservableCollection<ItemViewModel>();
            this.Buildings = new ObservableCollection<ItemViewModel>();
            this.Upgrades = new ObservableCollection<ItemViewModel>();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // turn on antilock screen
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            //Populate the lists
            mRace = NavigationContext.QueryString["race"];
            LoadData(mRace);
            unit_list.ItemsSource = Units; 
            buildings_list.ItemsSource = Buildings;
            upgrades_list.ItemsSource = Upgrades; 
            build_order.ItemsSource = BuildOrder;
            //Set BackGround 
            String bgpath = "";
            if (mRace == "Zerg")
            {
                bgpath = "/Images/Backgrounds/bg-mutalisk-top.jpg/";
            }
            else if (mRace == "Protoss")
            {
                bgpath = "/Images/Backgrounds/bg-mothership-top.jpg/";
            }
            else if (mRace == "Terran")
            {
                bgpath = "/Images/Backgrounds/bg-siege-tank-top.jpg.jpg/";
            }
            BitmapImage bitmapImage = new BitmapImage(new Uri(bgpath, UriKind.Relative));
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmapImage;
            LayoutRoot.Background = imageBrush;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        private void unit_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemViewModel item = unit_list.SelectedItem as ItemViewModel;
            BuildOrder.Add(item);
        }

        private void buildings_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemViewModel item = buildings_list.SelectedItem as ItemViewModel;
            BuildOrder.Add(item);
        }

        private void upgrades_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemViewModel item = upgrades_list.SelectedItem as ItemViewModel;
            BuildOrder.Add(item);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            InputPrompt input_name = new InputPrompt();
            input_name.Completed += input_name_Completed;
            input_name.Title = "Save";
            input_name.Message = "Please enter a name";
            input_name.Show();
        }
        void input_name_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            name = e.Result;
            DB_Helper.connect();
            ObservableCollection<Index> test = DB_Helper.get_Index_By_Name(name);
            if (test.Count == 0)
            {
                mAuthor = "Custom";
                DB_Helper.createIndex(name, desc,mRace,mAuthor,"","");
                ObservableCollection<Index> result = DB_Helper.get_Index_By_Name(name);
                int index_id = result[0].Id;

                for (int i = 0; i < BuildOrder.Count; i++)
                {
                    DB_Helper.insertBuild(index_id, i, BuildOrder[i].ObjId,"","");
                }
                NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                MessageBox.Show("Name is used. Please choose other name.");
            }
         }

        public void LoadData(String mRace)
        {
            DB_Helper.connect();
            //Load Units
            Object_SC_Table = DB_Helper.get_Object_SC_by_Type_and_Race(mRace, DB_Helper.UNIT);
            foreach (Object_SC obj in Object_SC_Table)
            {
                Units.Add(new ItemViewModel() { ObjId = obj.Obj_Id, Name = obj.Name, Mineral = obj.Mineral, Vespene = obj.Gas, Supply = obj.Food, BuildTime = obj.Time, Path= obj.Imagesource});
            }
            //Load Buildings
            Object_SC_Table.Clear();
            Object_SC_Table = DB_Helper.get_Object_SC_by_Type_and_Race(mRace, DB_Helper.BUILDING);
            foreach (Object_SC obj in Object_SC_Table)
            {
                this.Buildings.Add(new ItemViewModel() { ObjId = obj.Obj_Id, Name = obj.Name, Mineral = obj.Mineral, Vespene = obj.Gas, Supply = obj.Food, BuildTime = obj.Time, Path = obj.Imagesource});
            }
            //Load Upgardes
            Object_SC_Table.Clear();
            Object_SC_Table = DB_Helper.get_Object_SC_by_Type_and_Race(mRace, DB_Helper.UPGRADE);
            foreach (Object_SC obj in Object_SC_Table)
            {
                this.Upgrades.Add(new ItemViewModel() { ObjId = obj.Obj_Id, Name = obj.Name, Mineral = obj.Mineral, Vespene = obj.Gas, Supply = obj.Food, BuildTime = obj.Time, Path = obj.Imagesource });
            }
        }
    }
}