using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MicroNetCore.Rest.Services
{
    public interface IRestService<TModel>
        where TModel : class, new()
    {
        Task<Entity> FindAsync(Expression<Func<TModel, bool>> predicate = null);
        Task<Entity> FindPageAsync(int pageIndex, int pageSize, Expression<Func<TModel, bool>> predicate = null);

        Task<Entity> GetAsync(long id);
        Task<long> PostAsync(TModel model);
        Task PutAsync(long id, TModel model);
        Task DeleteAsync(long id);
    }
}