using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Toy.Models.MongoTest
{
    public partial class MgExample
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime RowDatetime { get; set; }
        public bool IsDelete { get; set; }
    }
}