using films.api.Models.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Services.Mongo
{
    public class FilmService : GenericService<Films>
    {
        public FilmService(IMongoDatabaseSettings settings) : base(settings)
        {
        }

    }
}
