using FindoApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindoApp.Domain.Interface.Service
{
    public interface ICheckListService
    {
        List<CheckList> GetCheckLists();
    }
}
