using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using KAStore.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Reposetories.Classes
{
    public class GenericRepsitory <T>: IGenericReposetories <T> where T : BaseModel
    {

        private readonly ApplicationDbContext _context;
        public GenericRepsitory(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool withTrackin = false)
        {
           if(withTrackin)
                return _context.Set<T>().ToList();

            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T? GetById(int id)=> _context.Set<T>().Find(id);
        

        public int Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
    }
}
