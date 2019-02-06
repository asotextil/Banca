using DAL;
using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace BLL
{
    public class RolControllerBLL : ICrud<Role>
    {
        private static RolControllerBLL instance = null;
        public static RolControllerBLL Instance { get => instance ?? new RolControllerBLL(); }
        public async Task<IdentityResult> Create(Role model)
        {
            return await RolControllerDAL.Instance.Create(model);
        }
        
        public async Task<IdentityResult> Delete(Role model)
        {
            return await RolControllerDAL.Instance.Delete(model);
        }

        public async Task<IdentityResult> Edit(Role model)
        {
            return await RolControllerDAL.Instance.Edit(model);
        }

        public async Task<IdentityResult> Edit(Role model, string name)
        {
            return await RolControllerDAL.Instance.Edit(model, name);
        }

        [Obsolete]
        public Task<List<Role>> Listar(Role model)
        {
            throw new NotImplementedException();
        }
    }
}
