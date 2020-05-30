using FindoApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindoApp.Domain.Interface.Service
{
    public interface ICheckListService
    {
        Task<List<CheckList>> GetCheckLists();
    }
}
