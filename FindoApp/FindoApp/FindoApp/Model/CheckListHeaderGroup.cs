using FindoApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindoApp.Model
{
    public class CheckListHeaderGroup: List<CheckList>
    {
        public string Title { get; set; }
    }
}
