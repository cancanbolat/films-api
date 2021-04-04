using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Services
{
    public interface IMainService<T, TKey>
        where TKey: IEquatable<TKey>
    {
        List<T> GetAll();
        T GetById(TKey id);
        void Add(T entity);
        void Delete(T entity);
        void Update(TKey id, T entity);
    }
}
