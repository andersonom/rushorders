using MongoDB.Driver;

namespace RushOrders.Data.Context
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }

    }
}