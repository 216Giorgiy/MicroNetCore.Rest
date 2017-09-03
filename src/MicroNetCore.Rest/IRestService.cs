using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MicroNetCore.Rest
{
    public interface IRestService<TModel>
        where TModel : class, new()
    {
        Task<IEnumerable<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate = null);

        Task<TModel> GetAsync(long id);

        Task<long> PostAsync(TModel model);

        Task PutAsync(long id, TModel model);

        Task DeleteAsync(long id);
    }
}