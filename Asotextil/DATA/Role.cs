using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA
{
   
    public class Role
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Nombre")]
        public string Nombre { get; set; }

        [BsonConstructor]
        public Role()
        {

        }
        
    }
}
