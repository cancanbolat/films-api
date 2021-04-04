using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Models
{
    public class Cast : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
