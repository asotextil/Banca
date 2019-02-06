using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace BLL
{
    interface ICrud<T>
    {
        Task<IdentityResult> Create(T model);
        Task<IdentityResult> Edit(T model);
        Task<IdentityResult> Delete(T model);
        Task<List<T>> Listar(T model);
    }
}
