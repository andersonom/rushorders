using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace RushOrders.Data.Context
{
    public class MongoContext : IMongoContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }
        public IMongoDatabase Database { get; }


        public MongoContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
            IsSSL = Convert.ToBoolean(configuration.GetSection("MongoConnection:IsSSL").Value);
            DatabaseName = configuration.GetSection("MongoConnection:Database").Value;

            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }

                var mongoClient = new MongoClient(settings);

                Database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting to the Mongo server.", ex);
            }
        }
    }
}
