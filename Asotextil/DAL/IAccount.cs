using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA;
using Microsoft.AspNet.Identity;

namespace DAL
{
    interface IAccount
    {
        Task<IdentityResult> Registrar(Afiliado afiliado);
    }
}
