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
        public CheckListItemModel(CheckListItem item, bool expanded = false)
        {
            Title = item.Description.ToUpper();
            Expanded = expanded;

            base.Answer = item.Answer;
            base.AnswerType = item.AnswerType;

            item.Options?.OrderBy(x => x.Ordem).ForEach(x => AlternativesModel.Add(new CheckListItemAlternativaModel(x)));
            
        }

        #region properties

        public List<CheckListItemAlternativaModel> AlternativesModel { get; set; } = new List<CheckListItemAlternativaModel>();

        public string Title { get; set; }

        private bool _expanded;

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
        
        public int Height
        {
            get
            {
                var count = !AlternativesModel.Any() || AlternativesModel.Count == 0 ? 1 : AlternativesModel.Count ;
                return 60 * count;
            }
        }
        
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
