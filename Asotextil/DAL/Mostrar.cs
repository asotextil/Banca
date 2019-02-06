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
    public class Mostrar : MongoContext
    {
        private static Mostrar instance = null;
        
        public static Mostrar Instance { get => instance ?? new Mostrar(); }

        public void Empezar()
        {
            var document = DB.GetCollection<Carro>("Carro").AsQueryable<Carro>().ToList();
            foreach (var item in document)
            {
                System.Diagnostics.Debug.WriteLine(item.Nombre);
            }
        }
    }
}
