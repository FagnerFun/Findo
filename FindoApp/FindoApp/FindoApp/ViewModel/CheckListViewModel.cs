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
using Xamarin.Forms.Internals;

namespace FindoApp.ViewModel
{
    public class CheckListViewModel : ViewModelBase
    {
        public ObservableCollection<CheckListHeaderGroup> Items { get; set; }
        public ICommand ItemClickCommand { get; set; }
        public ICommand SearchListCommand { get; set; }


        private ICheckListService _checkListService;
        public CheckListViewModel(INavigationService navigationService, ICheckListService checkListService) : base(navigationService)
        {
            _checkListService = checkListService;

            Items = new ObservableCollection<CheckListHeaderGroup>();

            ItemClickCommand = new Command<CheckList>(async (item) => await ItemClickCommandExecute(item));
            SearchListCommand = new Command<string>((s) => SearchListCommandExecute(s));

            LoadDataAsync();
        }

        async Task LoadDataAsync()
        {
            List<CheckList> checklits = await _checkListService.GetCheckLists();

            checklits.GroupBy(c => c.Title[0])
                     .ToDictionary(x => x.Key, x => x.ToList())
                     .OrderBy(x => x.Key)
                     .Select(x => new CheckListHeaderGroup(x.Value, x.Key.ToString()))
                     .ForEach(x => Items.Add(x));

        }

        private Task ItemClickCommandExecute(CheckList item)
        {
            var parameters = new NavigationParameters();
            parameters.Add("checklist", item);

            return NavigationService.NavigateAsync("CheckListItemPage", parameters);
        }


        private void SearchListCommandExecute(string search)
        {
            if (string.IsNullOrEmpty(search)) return;
            
            Items = new ObservableCollection<CheckListHeaderGroup>(Items.Where(x => x.Title.Contains(search)));
        }
    }
}
