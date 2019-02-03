using DATA;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace DAL
{
    public class Mostrar
    {
        private static Mostrar instance = null;


        public static Mostrar GetInstance()
        {
            if (instance == null)
                instance = new Mostrar();
            return instance;
        }

        public void Empezar()
        {
            MongoContext.GetInstance();
            var document = MongoContext.GetInstance().db.GetCollection<Carro>("Carro").AsQueryable<Carro>().ToList();
            foreach (var item in document)
            {
                System.Diagnostics.Debug.WriteLine(item.Nombre);
            }
        }
    }
}
