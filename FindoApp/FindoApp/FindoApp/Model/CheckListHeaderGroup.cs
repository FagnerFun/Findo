using FindoApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindoApp.Model
{
    public class CheckListHeaderGroup: List<CheckList>
    {
        public CheckListHeaderGroup()
        {

        }

        public CheckListHeaderGroup(List<CheckList> list, string title)
        {
            Title = title;
            AddRange(list);
        }

        public string Title { get; set; }
    }
}
