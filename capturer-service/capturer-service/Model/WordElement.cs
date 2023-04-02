using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace capturerservice.Model
{
    public class WordElement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Word { get; set; }
        public DateTime AddedDate { get; set; }
        public string Synonym { get; set; }
        public List<string> Definitions { get; set; }
    }
}
