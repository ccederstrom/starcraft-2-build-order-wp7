using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace SC2BuildOrder
{
    [Table]
    public class Object_SC : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public Object_SC(string name,String race, int type,int food, int mineral, int gas, int time, String path, String strong1, String strong2, String strong3, String weak1, String weak2, String weak3)
        {
            _Name = name;
            _Race = race;
            _Type = type;
            _Food = food;
            _Mineral = mineral;
            _Gas = gas;
            _Time = time;
            _Imagesource = path;
            _Strong1 = strong1;
            _Strong2 = strong2;
            _Strong3 = strong3;
            _Weak1 = weak1;
            _Weak2 = weak2;
            _Weak3 = weak3;
        }
        public Object_SC() { }
        // Define ID: private field, public property and database column.
        private int _Obj_Id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Obj_Id
        {
            get
            {
                return _Obj_Id;
            }
            set
            {
                if (_Obj_Id != value)
                {
                    NotifyPropertyChanging("Obj_Id");
                    _Obj_Id = value;
                    NotifyPropertyChanged("Obj_Id");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private string _Name;

        [Column]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private int _Type;

        [Column]
        public int Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type != value)
                {
                    NotifyPropertyChanging("Type");
                    _Type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private String _Race;

        [Column]
        public String Race
        {
            get
            {
                return _Race;
            }
            set
            {
                if (_Race != value)
                {
                    NotifyPropertyChanging("Race");
                    _Race = value;
                    NotifyPropertyChanged("Race");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private int _Food;

        [Column]
        public int Food
        {
            get
            {
                return _Food;
            }
            set
            {
                if (_Food != value)
                {
                    NotifyPropertyChanging("Food");
                    _Food = value;
                    NotifyPropertyChanged("Food");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private int _Mineral;

        [Column]
        public int Mineral
        {
            get
            {
                return _Mineral;
            }
            set
            {
                if (_Mineral != value)
                {
                    NotifyPropertyChanging("Mineral");
                    _Mineral = value;
                    NotifyPropertyChanged("Mineral");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private int _Gas;

        [Column]
        public int Gas
        {
            get
            {
                return _Gas;
            }
            set
            {
                if (_Gas != value)
                {
                    NotifyPropertyChanging("Gas");
                    _Gas = value;
                    NotifyPropertyChanged("Gas");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private int _Time;

        [Column]
        public int Time
        {
            get
            {
                return _Time;
            }
            set
            {
                if (_Time != value)
                {
                    NotifyPropertyChanging("Time");
                    _Time = value;
                    NotifyPropertyChanged("Time");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private String _Imagesource;

        [Column]
        public String Imagesource
        {
            get
            {
                return _Imagesource;
            }
            set
            {
                if (_Imagesource != value)
                {
                    NotifyPropertyChanging("Imagesource");
                    _Imagesource = value;
                    NotifyPropertyChanged("Imagesource");
                }
            }
        }
        
        // Define item name: private field, public property and database column.
        private String _Strong1;

        [Column]
        public String Strong1
        {
            get
            {
                return _Strong1;
            }
            set
            {
                if (_Strong1 != value)
                {
                    NotifyPropertyChanging("Strong1");
                    _Strong1 = value;
                    NotifyPropertyChanged("Strong1");
                }
            }
        }

         // Define item name: private field, public property and database column.
        private String _Strong2;

        [Column]
        public String Strong2
        {
            get
            {
                return _Strong2;
            }
            set
            {
                if (_Strong2 != value)
                {
                    NotifyPropertyChanging("Strong2");
                    _Strong2 = value;
                    NotifyPropertyChanged("Strong2");
                }
            }
        }

         // Define item name: private field, public property and database column.
        private String _Strong3;

        [Column]
        public String Strong3
        {
            get
            {
                return _Strong3;
            }
            set
            {
                if (_Strong3 != value)
                {
                    NotifyPropertyChanging("Strong3");
                    _Strong3 = value;
                    NotifyPropertyChanged("Strong3");
                }
            }
        }

         // Define item name: private field, public property and database column.
        private String _Weak1;

        [Column]
        public String Weak1
        {
            get
            {
                return _Weak1;
            }
            set
            {
                if (_Weak1 != value)
                {
                    NotifyPropertyChanging("Weak1");
                    _Weak1 = value;
                    NotifyPropertyChanged("Weak1");
                }
            }
        }

         // Define item name: private field, public property and database column.
        private String _Weak2;

        [Column]
        public String Weak2
        {
            get
            {
                return _Weak2;
            }
            set
            {
                if (_Weak2 != value)
                {
                    NotifyPropertyChanging("Weak2");
                    _Weak2 = value;
                    NotifyPropertyChanged("Weak2");
                }
            }
        }
         // Define item name: private field, public property and database column.
        private String _Weak3;

        [Column]
        public String Weak3
        {
            get
            {
                return _Weak3;
            }
            set
            {
                if (_Weak3 != value)
                {
                    NotifyPropertyChanging("Weak3");
                    _Weak3 = value;
                    NotifyPropertyChanged("Weak3");
                }
            }
        }


        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

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
        public class DataContext_Object_SC : DataContext
        {
            // Specify the connection string as a static, used in main page and app.xaml.
            public static string DBConnectionString = "Data Source=isostore:/DB_Object_SC.sdf";

            // Pass the connection string to the base class.
            public DataContext_Object_SC(string connectionString)
                : base(connectionString)
            { }

            // Specify a single table for the to-do items.
            public Table<Object_SC> Object_SC_Table;
        }

    }
}
