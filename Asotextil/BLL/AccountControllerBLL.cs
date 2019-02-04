using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DATA;

namespace BLL
{
    public class AccountControllerBLL : IAccount
    {
        private static AccountControllerBLL instance = null;

        public static AccountControllerBLL GetInstance()
        {
            if (instance == null)
                instance = new AccountControllerBLL();
            return instance;
        }

        public async Task<bool> Registrar(Afiliado afiliado)
        {
            try
            {
                return await AccountControllerDAL.GetInstance().Registrar(afiliado);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
