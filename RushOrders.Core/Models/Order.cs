using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RushOrders.Core.Models
{
    [NotMapped]
    public class Order
    {
        public Order()
        {
            CreationDate = DateTime.UtcNow;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
    }
}
