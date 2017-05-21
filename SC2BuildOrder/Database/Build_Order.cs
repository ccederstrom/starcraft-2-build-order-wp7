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
    public class Build_Order : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public Build_Order() { }
        public Build_Order(int index_id, int order, int obj_id, String when, String note)
        {
            _Index_Id = index_id;
            _Order = order;
            _Obj_Id = obj_id;
            _When = when;
            _Note = note;
        }

        //ID : ID of Index
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
        //ID : ID of Index
        // Define ID: private field, public property and database column.
        private int _Index_Id;

        [Column]
        public int Index_Id
        {
            get
            {
                return _Index_Id;
            }
            set
            {
                if (_Index_Id != value)
                {
                    NotifyPropertyChanging("Index_Id");
                    _Index_Id = value;
                    NotifyPropertyChanged("Index_Id");
                }
            }
        }
        // Define item name: private field, public property and database column.
        private int _Order;

        [Column]
        public int Order
        {
            get
            {
                return _Order;
            }
            set
            {
                if (_Order != value)
                {
                    NotifyPropertyChanging("Order");
                    _Order = value;
                    NotifyPropertyChanged("Order");
                }
            }
        }
        //ID of Obj
        // Define item name: private field, public property and database column.
        private int _Obj_Id;

        [Column]
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
        private String _When;

        [Column]
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

        [Column]
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
        public class DataContext_Build_Order : DataContext
        {
            // Specify the connection string as a static, used in main page and app.xaml.
            public static string DBConnectionString = "Data Source=isostore:/DB_Build_Order.sdf";

            // Pass the connection string to the base class.
            public DataContext_Build_Order(string connectionString)
                : base(connectionString)
            { }

            // Specify a single table for the to-do items.
            public Table<Build_Order> Build_Order_Table;
        }

    }
}
