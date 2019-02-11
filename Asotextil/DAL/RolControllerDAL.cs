using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA;
using MongoDB.Bson;

namespace DAL
{
    public class RolControllerDAL : MongoContext, ICrud<Role>
    {
        private static RolControllerDAL instance = null;

        public static RolControllerDAL Instance { get => instance ?? new RolControllerDAL(); }
        public async Task<IdentityResult> Create(Role model) 
        {
            var result = IdentityResult.Success;
            var session = MongoCliente.StartSession();
            var roles = DB.GetCollection<Role>("Role");
            session.StartTransaction();
            try
            {
                await roles.InsertOneAsync(model);
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                result = IdentityResult.Failed(new string[] { e.Message });
            }
            return result;
        }

        public async Task<IdentityResult> Delete(Role model)
        {
            var result = IdentityResult.Success;
            var session = MongoCliente.StartSession();
            var roles = DB.GetCollection<Role>("Role");
            var filtro = Builders<Role>.Filter.Eq("Nombre", model.Nombre);
            session.StartTransaction();
            try
            {
                await roles.DeleteOneAsync(filtro);
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                result = IdentityResult.Failed(new string[] { e.Message });
            }
            return result;
        }

        public async Task<IdentityResult> Edit(Role model)
        {
            var result = IdentityResult.Success;
            var session = MongoCliente.StartSession();
            var roles = DB.GetCollection<Role>("Role");
            var filtro = Builders<Role>.Filter.Eq("_id", model.Id);
            var update = Builders<Role>.Update.Set("Nombre", model.Nombre);
            session.StartTransaction();
            try
            {
                await roles.UpdateOneAsync(filtro,update);
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                result = IdentityResult.Failed(new string[] { e.Message });
            }
            return result;
        }

        public async Task<IdentityResult> Edit(Role model, string name)
        {
            var roles = DB.GetCollection<Role>("Role");
            var filtro = Builders<Role>.Filter.Eq("Nombre", name);
            var i = await roles.FindAsync<Role>(filtro);
            model.Id = i.First().Id;
            return await Edit(model);
        }

        public async Task<IdentityResult> RolToUser(List<Role> rols, string cedula)
        {
            var result = IdentityResult.Success;
            var user = DB.GetCollection<Afiliado>("Afiliado");
            var filtro = Builders<Afiliado>.Filter.Eq("Cedula", cedula);
            var roles = (from ro in rols select new Roles { Nombre = ro.Nombre }).ToList();
            var session = MongoCliente.StartSession();
            var update = Builders<Afiliado>.Update.Set("Roles", roles);
            session.StartTransaction();
            try
            {
                await user.UpdateOneAsync(filtro, update);
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                result = IdentityResult.Failed(new string[] { e.Message });
            }
            return result;
        }

        [Obsolete]
        public Task<List<Role>> Listar(Role model)
        {
            throw new NotImplementedException();
        }
    }
}
