using ExpressPaymentTest.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressPaymentTest.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _dataContext;

        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            _dataContext.SaveChanges();
            return entity;

        }

        public async Task<T> Get(int Id)
        {
          return await _dataContext.Set<T>().FindAsync(Id);  
        }

        public async Task<IEnumerable<T>> GetAll()
        {
              return _dataContext.Set<T>().ToList();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
