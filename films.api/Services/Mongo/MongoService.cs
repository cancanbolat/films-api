using films.api.Models.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Services.Mongo
{
    public class MongoService
    {
        private readonly IMongoCollection<Films> _films;
        public MongoService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            _films = db.GetCollection<Films>("Films");
        }

        public void Add(Films entity) => _films.InsertOne(entity);

        public void Delete(Films entity) => _films.DeleteOne(f => f.Id == entity.Id);

        public List<Films> GetAll() => _films.Find(film => true).ToList();

        public Films GetById(string id) => _films.Find<Films>(f => f.Id == id).FirstOrDefault();

        public void Update(string id, Films entity) => _films.ReplaceOne(f => f.Id == id, entity);
    }
}
