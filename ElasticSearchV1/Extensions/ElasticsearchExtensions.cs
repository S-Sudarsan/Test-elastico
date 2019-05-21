using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearchV1.Entity;
using Nest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElasticSearchV1.Extensions
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);

            var client = new ElasticClient(settings);
            client.CreateIndex(defaultIndex, i => new CreateIndexDescriptor(defaultIndex).Mappings(ms => ms.Map<Product>(m => m.AutoMap())));

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
