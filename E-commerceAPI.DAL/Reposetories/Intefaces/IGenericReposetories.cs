using E_commerceAPI.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Reposetories.Intefaces
{
    public interface IGenericReposetories<T> where T : BaseModel
    {
        int Add(T entity);
        T? GetById(int id);
        IEnumerable<T> GetAll(bool withTrackin=false);
        int Update(T entity);
        int Remove(T entity);
    }
}
