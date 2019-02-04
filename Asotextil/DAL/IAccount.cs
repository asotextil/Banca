using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA;

namespace DAL
{
    interface IAccount
    {
        Task<Boolean> Registrar(Afiliado afiliado);
    }
}
