using FindoApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FindoApp.Model
{
    public class CheckListItemModel: CheckListItem, INotifyPropertyChanged
    {
        private bool _expanded;

        public string Title { get; set; }

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if(_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                    OnPropertyChanged("StateIcon");
                }
            }
        }

        public string StateIcon 
        {
            get => _expanded ?  "collapsed.png" : "expanded.png";
        }

        public CheckListItemModel(CheckListItem item, bool expanded = false)
        {
            Title = item.Description.ToUpper();
            Expanded = expanded;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
