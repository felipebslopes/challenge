using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeApi.Dtos
{
    public class RepositoriesDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public Owner owner { get; set; }
        public string description { get; set; }
        public string created_at { get; set; }
        public string language { get; set; }

    }

    public class Owner
    {
        public string avatar_url { get; set; }

    }

}
