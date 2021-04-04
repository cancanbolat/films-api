using films.api.Models;
using films.api.Services.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Services
{
    public class CastService : GenericService<Cast>
    {
        public CastService(IMongoDatabaseSettings settings) : base(settings)
        {
        }
    }
}
