using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MongoContext
    {
        private MongoClient mongoCliente;

        public MongoClient MongoCliente
        {
            get { return mongoCliente; }
            set { mongoCliente = value; }
        }

        private IMongoDatabase db;

        public IMongoDatabase DB
        {
            get { return db; }
            set { db = value; }
        }
        
        
        public MongoContext()        //constructor   
        {
            // Leyendo credenciales desde app.config file   
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"]; //asotextildb  
            var MongoconnectionString = ConfigurationManager.AppSettings["MongoconnectionString"]; //mongodb://demouser:Fidelitas123@localhost:27017/asotextildb 
            // usando connection string
            mongoCliente = new MongoClient(MongoconnectionString);
            // Obteniendo la base de datos
            db = mongoCliente.GetDatabase(MongoDatabaseName);
        }
    }
}
