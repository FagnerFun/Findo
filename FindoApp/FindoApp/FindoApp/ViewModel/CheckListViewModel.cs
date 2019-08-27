using FindoApp.Domain.Model;
using FindoApp.Model;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FindoApp.ViewModel
{
    public class CheckListViewModel : ViewModelBase
    {
        public ObservableCollection<CheckListHeaderGroup> Items { get; }

        public CheckListViewModel(INavigationService navigationService) : base(navigationService)
        {
            Items = new ObservableCollection<CheckListHeaderGroup>();


            List<CheckList> checklits = new List<CheckList>();

            checklits.Add(new CheckList { IdCheckList = Guid.NewGuid(), Title = "A Lista 123" });
            checklits.Add(new CheckList { IdCheckList = Guid.NewGuid(), Title = "A Lista 456" });
            checklits.Add(new CheckList { IdCheckList = Guid.NewGuid(), Title = "Uma Lista Qualquer" });
            checklits.Add(new CheckList { IdCheckList = Guid.NewGuid(), Title = "Minha Listagem" });
            checklits.Add(new CheckList { IdCheckList = Guid.NewGuid(), Title = "Minha Listagem teste" });
            checklits.Add(new CheckList { IdCheckList = Guid.NewGuid(), Title = "Lista de pedidos" });
            checklits.Add(new CheckList { IdCheckList = Guid.NewGuid(), Title = "Lista de produtos" });

            var dictonary = checklits.GroupBy(c => c.Title[0]).ToDictionary(x => x.Key, x => x.ToList()).OrderBy(x => x.Key);

            foreach (var item in dictonary)
            {
                var groupItem = new CheckListHeaderGroup { Title = item.Key.ToString() };
                groupItem.AddRange(item.Value);
                Items.Add(groupItem);
            }
        }
    }
}
