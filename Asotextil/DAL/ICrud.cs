using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface ICrud<T>
    {
        Task<IdentityResult> Create(T model);
        Task<IdentityResult> Edit(T model);
        Task<IdentityResult> Delete(T model);
        Task<List<T>> Listar(T model);
    }
}
