using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.CheckList.Domain.Model
{
    public class GroupCheckList
    {
        public Guid IdGroupCheckList { get; set; }

        public Guid IdCheckList { get; set; }

        public string Titulo { get; set; }

        public int Ordem { get; set; }

        public virtual CheckList CheckList { get; set; }

        public virtual ICollection<CheckListItem> Itens { get; set; }
    }
}
