using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Services
{
    public interface IEntity
    {

    }

    public interface IEntity<out TKey> : IEntity 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
    }
}
