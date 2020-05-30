using FindoApp.Domain.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindoApp.Service.Interface
{
    [Headers("Content-Type: application/json")]
    public interface IFindo
    {
        [Get("/api/CheckList")]
        Task<List<CheckList>> GetCheckListAsync();
    }
}
