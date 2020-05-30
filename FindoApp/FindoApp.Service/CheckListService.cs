using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model;
using FindoApp.Domain.Model.Enum;
using FindoApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindoApp.Service
{
    public class CheckListService : ICheckListService
    {
        private IFindo _findoApi;

        public CheckListService(IFindo findoApi)
        {
            _findoApi = findoApi;
        }

        public async Task<List<CheckList>> GetCheckLists()
        {
            return await _findoApi.GetCheckListAsync();
        }
    }
}
