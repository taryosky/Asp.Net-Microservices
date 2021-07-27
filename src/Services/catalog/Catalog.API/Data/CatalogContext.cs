using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Catalog.API.Entities;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = mongoClient.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(config.GetSection("DatabaseSettings:CollectionName").Value);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
