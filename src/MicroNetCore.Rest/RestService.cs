using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MicroNetCore.Rest
{
    public sealed class RestService<TModel> : IRestService<TModel>
        where TModel : class, new()
    {
        public Task<IEnumerable<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<long> PostAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(long id, TModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}