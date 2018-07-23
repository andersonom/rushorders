using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.ObjectModel;

namespace RushOrders.Data.Context
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }

    }
}