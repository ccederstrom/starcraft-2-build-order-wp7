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
    public class Index : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public Index(){}
        public Index(string name, string desc, string race, string author, string note, string notefooter)
        {
            _Name = name;
            _Description = desc;
            _Race = race;
            _Author = author;
            _Note = note;
            _Note_Footer = notefooter;
        }
        // Define ID: private field, public property and database column.
        private int _Id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    NotifyPropertyChanging("Id");
                    _Id = value;
                    NotifyPropertyChanged("Id");
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
        private string _Description;

        [Column]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (_Description != value)
                {
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private string _Race;

        [Column]
        public string Race
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
        private string _Author;

        [Column]
        public string Author
        {
            get
            {
                return _Author;
            }
            set
            {
                if (_Author != value)
                {
                    NotifyPropertyChanging("Author");
                    _Author = value;
                    NotifyPropertyChanged("Author");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private string _Note;

        [Column]
        public string Note
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
        // Define item name: private field, public property and database column.
        private string _Note_Footer;

        [Column]
        public string Note_Footer
        {
            get
            {
                return _Note_Footer;
            }
            set
            {
                if (_Note_Footer != value)
                {
                    NotifyPropertyChanging("Note_Footer");
                    _Note_Footer = value;
                    NotifyPropertyChanged("Note_Footer");
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
        public class DataContext_Index : DataContext
        {
            // Specify the connection string as a static, used in main page and app.xaml.
            public static string DBConnectionString = "Data Source=isostore:/DB_Index.sdf";

            // Pass the connection string to the base class.
            public DataContext_Index(string connectionString)
                : base(connectionString)
            { }

            public Table<Index> Index_Table;
        }

    }
}
