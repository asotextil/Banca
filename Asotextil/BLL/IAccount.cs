using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA;

namespace BLL
{
    interface IAccount
    {
        Task<Boolean> Registrar(Afiliado afiliado);
    }
}
