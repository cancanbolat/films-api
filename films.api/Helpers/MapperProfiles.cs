using AutoMapper;
using films.api.DTO;
using films.api.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Helpers
{
    public class MapperProfiles: Profile
    {
        public MapperProfiles()
        {
            CreateMap<Films, FilmsDTO>();
        }
    }
}
