using FindoApp.Domain.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindoApp.Domain.Model
{
    public class CheckListItem
    {
        public CheckListItem()
        {
            IsRequired = true;
        }

        public Guid IdCheckListItem { get; set; }

        public Guid IdGroupCheckList { get; set; }

        public string Descricao { get; set; }

        public int Ordem { get; set; }

        public bool IsRequired { get; set; }

        public enAnswerType AnswerType { get; set; }

        //public virtual ICollection<RelCheckListItemAlternativa> ItemOption { get; set; }

        //public virtual ICollection<CheckListItemAlternativa> Options { get; set; }

        //public virtual CheckListItemAnswer Answer { get; set; }

        public virtual GroupCheckList Group { get; set; }
    }
}
