using films.api.Models;
using films.api.Services.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Services
{
    public class GenericService<TEntity> : IMainService<TEntity, string>
        where TEntity: BaseModel
    {
        private readonly IMongoCollection<TEntity> _collection;

        public GenericService(IMongoDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            _collection = db.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        

        public TEntity GetById(string id) => _collection.Find(x => x.Id == id).FirstOrDefault();
        
        public List<TEntity> GetAll() => _collection.Find(e => true).ToList(); 
        
        public void Add(TEntity entity) => _collection.InsertOne(entity);

        public void Delete(TEntity entity) => _collection.DeleteOne(x => x.Id == entity.Id);

        public void Update(string id, TEntity entity) => _collection.ReplaceOne(x => x.Id == id, entity);
    }

}
