using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.Sample.Data
{
    public sealed class Repository<TModel> : IRepository<TModel>
        where TModel : class, IModel, new()
    {
        public async Task<IEnumerable<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate = null)
        {
            return new List<TModel>();
        }

        public async Task<TModel> GetAsync(long id)
        {
            return new TModel();
        }

        public async Task<long> PostAsync(TModel model)
        {
            return 1;
        }

        public async Task PutAsync(long id, TModel model)
        {
        }

        public async Task DeleteAsync(long id)
        {
        }
    }
}