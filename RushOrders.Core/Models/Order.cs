using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RushOrders.Core.Models
{
    public class Order
    {

        public ObjectId Id { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
    }
}
