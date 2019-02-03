using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA
{
    public class Carro
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Nombre")]
        public String Nombre { get; set; }
    }
}
