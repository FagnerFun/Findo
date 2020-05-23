using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model;
using FindoApp.Model;
using FindoApp.Model.interfaces;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FindoApp.ViewModel
{
    public class CheckListItemViewModel : ViewModelBase
    {
        public ObservableCollection<CheckListItemModel> Items { get; }

        private ICheckListService _checkListService;
        private IMessageBoxService _messageBoxService;
        private INavigationService _navigationService;

        public ICommand SelectedItemComboCommand { get; set; }
        public ICommand SelectedItemMultiSelectionCommand { get; set; }
        public ICommand DoneCommand { get; set; }

        private CheckListItemAlternativaModel oldItem = null;

        public CheckListItemViewModel(INavigationService navigationService, ICheckListService checkListService, IMessageBoxService messageBoxService) : base(navigationService)
        {
            _checkListService = checkListService;
            _messageBoxService = messageBoxService;
            _navigationService = navigationService;

            Items = new ObservableCollection<CheckListItemModel>();

            SelectedItemComboCommand = new Command<object>((o) => SelectedItemComboExecute(o));
            SelectedItemMultiSelectionCommand = new Command<object>((o) => SelectedItemMultiExecute(o));
            DoneCommand = new Command(() => DoneExecute());

        }
        
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            CheckList chk = parameters.GetValue<CheckList>("checklist");

            ListName = chk.Title;

            Items.Clear();
            chk.Groups.FirstOrDefault().Itens.OrderBy(x => x.Order).ForEach(x => Items.Add(new CheckListItemModel(x)));
        }

        public override void TitleCommandEvent(object e)
        {
            if (e == null) return;

            CheckListItemModel item = e as CheckListItemModel;
            item.Expanded = !item.Expanded;
        }

        private void SelectedItemComboExecute(object e)
        {
            if (e == null) return;

            CheckListItemAlternativaModel item = e as CheckListItemAlternativaModel;
            if(oldItem != null && oldItem.IdCheckListItemAlternativa != item.IdCheckListItemAlternativa)
            {
                oldItem.Selected = !oldItem.Selected;
            }
            oldItem = item;
            item.Selected = !item.Selected;
        }

        private void SelectedItemMultiExecute(object e)
        {
            if (e == null) return;

            CheckListItemAlternativaModel item = e as CheckListItemAlternativaModel;
            item.Selected = !item.Selected;
        }

        private async void DoneExecute()
        {
            string answer = await _messageBoxService.ShowActionSheet("Done?", "Cancel", null,  new string[] { "Save", "Save and done", "Discard" });

            if(answer != "Cancel" || string.IsNullOrEmpty(answer))
                await _navigationService.GoBackAsync();
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
