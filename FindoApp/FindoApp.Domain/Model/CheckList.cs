using System;
using System.Collections.Generic;

namespace FindoApp.Domain.Model
{
    public class CheckList
    {
        public Guid IdCheckList { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<GroupCheckList> Groups { get; set; }
    }
}
