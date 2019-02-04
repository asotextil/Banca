using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA;

namespace DAL
{
    public class AccountControllerDAL : MongoContext, IAccount
    {
        private static AccountControllerDAL instance = null;
        
        public static AccountControllerDAL GetInstance()
        {
            if (instance == null)
                instance = new AccountControllerDAL();
            return instance;
        }
        public async Task<bool> Registrar(Afiliado afiliado)
        {
            var session = MongoCliente.StartSession();
            var afiliados = DB.GetCollection<Afiliado>("Afiliado");
            session.StartTransaction();
            try
            {
                await afiliados.InsertOneAsync(afiliado);
                session.CommitTransaction();
            }
            catch
            {
                session.AbortTransaction();
                throw;
            }
            return true;
        }
    }
}
