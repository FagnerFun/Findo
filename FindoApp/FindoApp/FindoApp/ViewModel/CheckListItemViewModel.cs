using System.Collections.ObjectModel;
using System.Linq;
using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model;
using Prism.Navigation;

namespace FindoApp.ViewModel
{
    public class CheckListItemViewModel : ViewModelBase
    {
        public ObservableCollection<CheckListItem> Items { get; }

        private ICheckListService _checkListService;
        public CheckListItemViewModel(INavigationService navigationService, ICheckListService checkListService) : base(navigationService)
        {
            _checkListService = checkListService;
            Items = new ObservableCollection<CheckListItem>();
        }
        
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            CheckList chk = parameters.GetValue<CheckList>("checklist");

            ListName = chk.Title;

            chk.Groups.SelectMany(x => x.Itens)
                      .OrderBy(x => x.Order)
                      .ToList()
                      .ForEach(x => Items.Add(x));
        }

        #region propertys
        
        string listName = "";
        public string ListName
        {
            get { return listName; }
            set { listName = value; RaisePropertyChanged(); }
        }
        
        #endregion
    }
}
