using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA
{
    public class Afiliado
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }
        
        [BsonElement("Contraseña")]
        public string Password { get; set; }

        [BsonElement("Cedula")]
        public String Cedula { get; set; }

        [BsonElement("Nombre")]
        public String Nombre { get; set; }

        [BsonElement("Primer_Apellido")]
        public String Primer_Apellido { get; set; }

        [BsonElement("Segundo_Apellido")]
        public String Segundo_Apellido { get; set; }

        [BsonElement("FechaNacimiento")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime FechaNacimiento { get; set; }

        [BsonElement("FechaIngreso")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime FechaIngreso { get; set; }

        [BsonElement("Puesto")]
        public String Puesto { get; set; }

        [BsonElement("Salario")]
        public Double? Salario { get; set; }

        [BsonElement("FechaSalida")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime? FechaSalida { get; set; }

        [BsonElement("Estado")]
        [BsonDefaultValue(true)]
        public Boolean Estado { get; set; }
    }
}
