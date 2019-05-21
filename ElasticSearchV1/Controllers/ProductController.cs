using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ElasticSearchV1.Entity;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ElasticSearchV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IElasticClient _elasticClient;

        public ProductController(IElasticClient client)
        {
            _elasticClient = client;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var response = await _elasticClient.IndexDocumentAsync<Product>(product);

            return Ok(response.Result);
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find(string term)
        {
            try
            {
                var result = _elasticClient.Search<Product>(q => q
                .AllTypes()
                .From(0)
                .Size(10)
                .Query(p => p
                .Match(m => m
                .Field(f => f.Name)
                .Query(term))));

                return Ok(result.Documents);
            }
            catch (Exception Ex)
            {
                throw;
            }
            
        }
    }
}