using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA;
using Microsoft.AspNet.Identity;

namespace DAL
{
    public class AccountControllerDAL : MongoContext, IAccount
    {
        private static AccountControllerDAL instance = null;

        public static AccountControllerDAL Instance { get => instance ?? new AccountControllerDAL(); }

        public async Task<IdentityResult> Registrar(Afiliado afiliado)
        {
            var result = IdentityResult.Success;
            var session = MongoCliente.StartSession();
            var afiliados = DB.GetCollection<Afiliado>("Afiliado");
            session.StartTransaction();
            try
            {
                await afiliados.InsertOneAsync(afiliado);
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                result = IdentityResult.Failed(new string[] { e.Message });
            }
            return result;
        }
    }
}
