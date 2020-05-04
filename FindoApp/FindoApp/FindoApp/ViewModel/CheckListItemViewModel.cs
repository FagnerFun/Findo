using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model;
using FindoApp.Model;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FindoApp.ViewModel
{
    public class CheckListItemViewModel : ViewModelBase
    {
        public ObservableCollection<CheckListItemModel> Items { get; }

        private ICheckListService _checkListService;
        public CheckListItemViewModel(INavigationService navigationService, ICheckListService checkListService) : base(navigationService)
        {
            _checkListService = checkListService;
            Items = new ObservableCollection<CheckListItemModel>();
        }
        
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            CheckList chk = parameters.GetValue<CheckList>("checklist");

            ListName = chk.Title;

            Items.Clear();
            chk.Groups.FirstOrDefault().Itens.OrderBy(x => x.Order).ForEach(x => Items.Add(new CheckListItemModel(x)));
        }

        protected override void TitleCommandEvent(object e)
        {
            CheckListItemModel item = e as CheckListItemModel;
            item.Expanded = true;
        }

        #region properties

        string listName = "";
        public string ListName
        {
            get { return listName; }
            set { listName = value; RaisePropertyChanged(); }
        }
        
        #endregion
    }
}
