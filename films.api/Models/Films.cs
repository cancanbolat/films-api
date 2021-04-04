using films.api.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Models.Mongo
{
    public class Films : BaseModel
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public double Imdb { get; set; }
    }
}
