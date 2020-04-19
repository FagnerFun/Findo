using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model;
using FindoApp.Model;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindoApp.ViewModel
{
    public class CheckListViewModel : ViewModelBase
    {
        public ObservableCollection<CheckListHeaderGroup> Items { get; }
        public ICommand ItemClickCommand { get; set; }


        private ICheckListService _checkListService;
        public CheckListViewModel(INavigationService navigationService, ICheckListService checkListService) : base(navigationService)
        {
            _checkListService = checkListService;

            Items = new ObservableCollection<CheckListHeaderGroup>();

            List<CheckList> checklits = _checkListService.GetCheckLists();

            var orderedList = checklits.GroupBy(c => c.Title[0])
                                       .ToDictionary(x => x.Key, x => x.ToList())
                                       .OrderBy(x => x.Key)
                                       .Select(x => new CheckListHeaderGroup(x.Value, x.Key.ToString()));

            Items = new ObservableCollection<CheckListHeaderGroup>(orderedList);
            ItemClickCommand = new Command<CheckList>( async (item) => await ItemClickCommandExecute(item) );
        }

        private Task ItemClickCommandExecute(CheckList item)
        {
            var parameters = new NavigationParameters();
            parameters.Add("checklist", item);

            return NavigationService.NavigateAsync("CheckListItemPage", parameters);
        }

    }
}
