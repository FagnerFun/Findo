using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.CheckList.Domain.Model
{
    public class CheckListItemAlternativa
    {
        public Guid IdCheckListItemAlternativa { get; set; }
        public string Texto { get; set; }

        public int Ordem { get; set; }

        public virtual ICollection<RelCheckListItemAlternativa> AlternativaItem { get; set; }
    }
}
