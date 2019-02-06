using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DATA;
using Microsoft.AspNet.Identity;

namespace BLL
{
    public class AccountControllerBLL : IAccount
    {
        private static AccountControllerBLL instance = null;
        public static AccountControllerBLL Instance { get => instance ?? new AccountControllerBLL(); }

        public async Task<IdentityResult> Registrar(Afiliado afiliado)
        {
            return await AccountControllerDAL.Instance.Registrar(afiliado);
        }
    }
}
