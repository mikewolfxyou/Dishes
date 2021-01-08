using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;

namespace DishesApi.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private DatabaseConfiguration Configuration { get; }
        private readonly ILogger _logger;

        public DatabaseFactory(IConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            var configSection = configuration.GetSection("DataAccess:Merch");

            Configuration = new DatabaseConfiguration
            {
                Host = configSection.GetValue<string>("Host"),
                Port = configSection.GetValue<int>("Port"),
                Username = configSection.GetValue<string>("Username"),
                Password = configSection.GetValue<string>("Password"),
                Database = configSection.GetValue<string>("Database")
            };
        }

        public IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(GetConnectionString());
            try
            {
                return client.GetDatabase(Configuration.Database);
            }
            catch (Exception e)
            {
                _logger.Error("Exception by GetDatabase: " + e.Message);
                
                throw;
            }
        }

        private string GetConnectionString()
        {
            return "mongodb://" 
                   + Configuration.Username + ":" 
                   + Configuration.Password + "@"
                   + Configuration.Host + ":" 
                   + Configuration.Port + "/dishes";
        }
    }
}