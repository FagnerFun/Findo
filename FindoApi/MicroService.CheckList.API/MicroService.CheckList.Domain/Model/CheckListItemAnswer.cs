using System;

namespace MicroService.CheckList.Domain.Model
{
    public class CheckListItemAnswer
    {
        public Guid IdCheckListItemAnswer { get; set; }

        public Guid IdEvaluation { get; set; }

        public Guid IdCheckListItem { get; set; }

        public Guid? IdCheckListItemAlternativa { get; set; }

        public string Value { get; set; }

        public virtual CheckListItemAlternativa CheckListItemAlternativa { get; set; }
    }
}
