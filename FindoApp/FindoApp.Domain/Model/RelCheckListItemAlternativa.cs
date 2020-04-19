using System;
using System.Collections.Generic;
using System.Text;

namespace FindoApp.Domain.Model
{
    public class RelCheckListItemAlternativa
    {
        public Guid IdCheckListItemAlternativa { get; set; }
        public virtual CheckListItemAlternativa Alternativa { get; set; }
        public Guid IdCheckListItem { get; set; }
        public virtual CheckListItem Item { get; set; }
    }
}
