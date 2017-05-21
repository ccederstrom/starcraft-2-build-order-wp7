using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace SC2BuildOrder
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Units = new ObservableCollection<ItemViewModel>();
            this.Buildings = new ObservableCollection<ItemViewModel>();
            this.Upgrades = new ObservableCollection<ItemViewModel>();
        }
        private ObservableCollection<Object_SC> _Object_SC_Table;
        public ObservableCollection<Object_SC> Object_SC_Table
        {
            get
            {
                return _Object_SC_Table;
            }
            set
            {
                if (_Object_SC_Table != value)
                {
                    _Object_SC_Table = value;
                    NotifyPropertyChanged("Table");
                }
            }
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Units { get; private set; }
        public ObservableCollection<ItemViewModel> Buildings { get; private set; }
        public ObservableCollection<ItemViewModel> Upgrades { get; private set; }
        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData(String mRace)
        {
            // Sample data; replace with real data
            //this.Items.Add(new ItemViewModel() { Name = "runtime one", Mineral = 50, Vespene = 0, Supply = 2, BuildTime = 30 });
            //this.Items.Add(new ItemViewModel() { Name = "runtime one", Mineral = 50, Vespene = 0, Supply = 2, BuildTime = 30 });
            //this.Items.Add(new ItemViewModel() { Name = "runtime one", Mineral = 50, Vespene = 0, Supply = 2, BuildTime = 30 });
            //this.Items.Add(new ItemViewModel() { Name = "runtime one", Mineral = 50, Vespene = 0, Supply = 2, BuildTime = 30 });
            //this.Items.Add(new ItemViewModel() { Name = "runtime one", Mineral = 50, Vespene = 0, Supply = 2, BuildTime = 30 });
            //this.Items.Add(new ItemViewModel() { Name = "runtime one", Mineral = 50, Vespene = 0, Supply = 2, BuildTime = 30 });

            //Object_SC_Table = DB_Helper.get_Object_SC_by_Type(DB_Helper.UNIT);
            //// List<Pushpin> pushpin = new List<Pushpin>();
            //for (int i = 0; i < Object_SC_Table.Count; i++)
            //{
            //    this.Items.Add(new ItemViewModel() { Name = Object_SC_Table[i].Name, Mineral = Object_SC_Table[i].Mineral, Vespene = Object_SC_Table[i].Gas, Supply = Object_SC_Table[i].Food, BuildTime = Object_SC_Table[i].Time });
                
            //}

            DB_Helper.connect();
            //Load Units
            Object_SC_Table = DB_Helper.get_Object_SC_by_Type_and_Race(mRace, DB_Helper.UNIT);
            foreach(Object_SC obj in Object_SC_Table){
                this.Units.Add(new ItemViewModel() { ObjId= obj.Obj_Id,Name = obj.Name, Mineral = obj.Mineral, Vespene = obj.Gas, Supply = obj.Food, BuildTime = obj.Time });
            }
            //Load Buildings
            Object_SC_Table.Clear();
            Object_SC_Table = DB_Helper.get_Object_SC_by_Type_and_Race(mRace, DB_Helper.BUILDING);
            foreach (Object_SC obj in Object_SC_Table)
            {
                this.Buildings.Add(new ItemViewModel() { ObjId = obj.Obj_Id, Name = obj.Name, Mineral = obj.Mineral, Vespene = obj.Gas, Supply = obj.Food, BuildTime = obj.Time });
            }
            //Load Upgardes
            Object_SC_Table.Clear();
            Object_SC_Table = DB_Helper.get_Object_SC_by_Type_and_Race(mRace, DB_Helper.UPGRADE);
            foreach (Object_SC obj in Object_SC_Table)
            {
                this.Upgrades.Add(new ItemViewModel() { ObjId = obj.Obj_Id, Name = obj.Name, Mineral = obj.Mineral, Vespene = obj.Gas, Supply = obj.Food, BuildTime = obj.Time });
            }
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}