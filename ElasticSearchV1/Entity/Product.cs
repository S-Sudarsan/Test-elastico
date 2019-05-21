using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearchV1.Entity
{
    public class Product
    {
        public int Id { get; set; }
        [Text(Name="name")]
        public string Name { get; set; }
        [Text(Name="description")]
        public string Description { get; set; }
        [Text(Name = "tag")]
        public string[] Tags { get; set; }
    }
}
