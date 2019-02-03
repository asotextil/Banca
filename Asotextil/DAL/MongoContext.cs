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
        private static MongoContext instance = null;
        

        public static MongoContext GetInstance()
        {
            if (instance == null)
                instance = new MongoContext();
            return instance;
        }

        public IMongoDatabase db;
        private MongoClient client;
        public MongoContext()        //constructor   
        {
            // Leyendo credenciales desde app.config file   
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"]; //asotextildb  
            var MongoconnectionString = ConfigurationManager.AppSettings["MongoconnectionString"]; //mongodb://demouser:Fidelitas123@localhost:27017/asotextildb 
            // usando connection string
            client = new MongoClient(MongoconnectionString);
            // Obteniendo la base de datos
            db = client.GetDatabase(MongoDatabaseName);
        }
    }
}
