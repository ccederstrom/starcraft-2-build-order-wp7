using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SC2BuildOrder
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _name;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged("LineOne");
                }
            }
        }

        private int _ObjId;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int ObjId
        {
            get
            {
                return _ObjId;
            }
            set
            {
                if (value != _ObjId)
                {
                    _ObjId = value;
                    NotifyPropertyChanged("LineOne");
                }
            }
        }

        private int _mineral;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int Mineral
        {
            get
            {
                return _mineral;
            }
            set
            {
                if (value != _mineral)
                {
                    _mineral = value;
                    NotifyPropertyChanged("LineTwo");
                }
            }
        }

        private int _vespene;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int Vespene
        {
            get
            {
                return _vespene;
            }
            set
            {
                if (value != _vespene)
                {
                    _vespene = value;
                    NotifyPropertyChanged("LineThree");
                }
            }
        }


        private int _supply;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int Supply
        {
            get
            {
                return _supply;
            }
            set
            {
                if (value != _supply)
                {
                    _supply = value;
                    NotifyPropertyChanged("LineThree");
                }
            }
        }


        private int _buildTime;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int BuildTime
        {
            get
            {
                return _buildTime;
            }
            set
            {
                if (value != _buildTime)
                {
                    _buildTime = value;
                    NotifyPropertyChanged("LineThree");
                }
            }
        }

        private string _path;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                if (value != _path)
                {
                    _path = value;
                    NotifyPropertyChanged("Path");
                }
            }
        }
        private string _strong1;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Strong1
        {
            get
            {
                return _strong1;
            }
            set
            {
                if (value != _strong1)
                {
                    _strong1 = value;
                    NotifyPropertyChanged("Strong1");
                }
            }
        }

        private string _strong2;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Strong2
        {
            get
            {
                return _strong2;
            }
            set
            {
                if (value != _strong2)
                {
                    _strong2 = value;
                    NotifyPropertyChanged("Strong2");
                }
            }
        }

        private string _strong3;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Strong3
        {
            get
            {
                return _strong3;
            }
            set
            {
                if (value != _strong3)
                {
                    _strong3 = value;
                    NotifyPropertyChanged("Strong3");
                }
            }
        }

        private string _weak1;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Weak1
        {
            get
            {
                return _weak1;
            }
            set
            {
                if (value != _weak1)
                {
                    _weak1 = value;
                    NotifyPropertyChanged("Weak1");
                }
            }
        }

        private string _weak2;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Weak2
        {
            get
            {
                return _weak2;
            }
            set
            {
                if (value != _weak2)
                {
                    _weak2 = value;
                    NotifyPropertyChanged("Weak2");
                }
            }
        }

        private string _weak3;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Weak3
        {
            get
            {
                return _weak3;
            }
            set
            {
                if (value != _weak3)
                {
                    _weak3 = value;
                    NotifyPropertyChanged("Weak3");
                }
            }
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