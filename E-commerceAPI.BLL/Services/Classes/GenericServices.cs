using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class GenericServices<TRequest, TResponse, TEntity> : IGenericServices<TRequest, TResponse, TEntity>
        where TEntity : BaseModel

    {
        private readonly IGenericReposetories<TEntity> _genericReposetories;

        public GenericServices(IGenericReposetories<TEntity>genericReposetories)
        {
            _genericReposetories = genericReposetories;
        }
        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
           return _genericReposetories.Add(entity);
        }

        public int Delete(int id)
        {
           var entity = _genericReposetories.GetById(id);
            if (entity == null)
            {
                throw new ArgumentException("Entity not found");
            }
            return _genericReposetories.Remove(entity);
        }

        public IEnumerable<TResponse> GetAll(bool onlyActive = false)
        {
            var entities = _genericReposetories.GetAll();
            if (onlyActive)
            {
                entities = entities.Where(e => e.Status == Status.Active);
            }
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(int id)
        {
           var entity=  _genericReposetories.GetById(id);
            return entity  is null? default: entity.Adapt<TResponse>();
        }

        public bool ToggleStatus(int Id)
        {
            var entity = _genericReposetories.GetById(Id);
            if (entity is null)
            {
                return false;
            }
            entity.Status = entity.Status == Status.Active ? Status.InActive : Status.Active;
            _genericReposetories.Update(entity);
            return true;


        }

        public int Update(int Id, TRequest request)
        {
            var entity = _genericReposetories.GetById(Id);
            if (entity == null)
            {
                return 0;
            }
            var updateEntity = request.Adapt(entity);
            return _genericReposetories.Update(updateEntity);
        }
    }
}
