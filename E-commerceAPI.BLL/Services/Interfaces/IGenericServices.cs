using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Interfaces
{
    public interface IGenericServices<TRequest,TResponse,TEntity>
    {
        int Create (TRequest request);
        int Update(int Id ,TRequest request);
        int Delete(int id);

        IEnumerable<TResponse> GetAll(bool onlyActive =false);
        TResponse? GetById(int id);
        bool ToggleStatus(int Id);

    }
}
