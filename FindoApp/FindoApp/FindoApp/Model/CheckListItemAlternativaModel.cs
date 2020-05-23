using FindoApp.Domain.Model;
using System.ComponentModel;

namespace FindoApp.Model
{
    public class CheckListItemAlternativaModel: CheckListItemAlternativa, INotifyPropertyChanged
    {
        public CheckListItemAlternativaModel(CheckListItemAlternativa alternative)
        {
            Title = alternative.Texto;
            IdCheckListItemAlternativa = alternative.IdCheckListItemAlternativa;
        }


        public string Title { get; set; }

        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("ShowIcon");
                    OnPropertyChanged("TextColor");
                }
            }
        }

        public string TextColor
        {
            get => Selected ? "#FEDB03" : "#AFAFAF";
        }

        public bool ShowIcon
        {
            get => _selected ? true : false;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
