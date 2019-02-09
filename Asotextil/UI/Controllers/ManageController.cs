using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL;
using DATA;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UI.Models;

namespace UI.Controllers
{
   // [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager rolemanager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = rolemanager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //
        // GET: /Manage/Role
        public ActionResult Roles()
        {
            var roles = RoleManager.Roles.ToList();
            return View(roles.Select( r => new RolesViewModel { Id = r.Id, Name = r.Name}));
        }

        //
        // GET: /Manage/CreateRole
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = IdentityResult.Success;
                if (RoleManager.RoleExists(model.Name))
                    result = IdentityResult.Failed(new string[] { "El Rol ya Existe" });
                else
                {
                    var role = new Role { Nombre = model.Name };
                    result = await RoleManager.CreateAsync(new IdentityRole(model.Name));
                    if (result.Succeeded)
                    {
                        result = await RolControllerBLL.Instance.Create(role);
                        if (result.Succeeded)
                            return RedirectToAction("Roles");
                    }
                }
                AddErrors(result);
            }
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }
        
        public async Task<ActionResult> EditRole(string id)
        {
            var rolesViewModel = await RoleManager.FindByIdAsync(id);
            if (rolesViewModel == null)
            {
                return HttpNotFound();
            }
            RolesViewModel rol = new RolesViewModel { Id = rolesViewModel.Id, Name = rolesViewModel.Name };
            return View(rol);
        }

        // POST: RolesViewModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRole([Bind(Include = "Id,Name")] RolesViewModel rolesViewModel)
        {
            if (ModelState.IsValid)
            {
                var rol = await RoleManager.FindByIdAsync(rolesViewModel.Id);
                string name = rol.Name;
                rol.Name = rolesViewModel.Name;
                var result = IdentityResult.Success;
                result = await RoleManager.UpdateAsync(rol);
                if (result.Succeeded)
                {
                    result = await RolControllerBLL.Instance.Edit(new Role { Nombre = rolesViewModel.Name }, name);
                    if (result.Succeeded)
                        return RedirectToAction("Roles");
                }
                AddErrors(result);
            }
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(rolesViewModel);
        }

        // GET: RolesViewModels/Delete/5
        public async Task<ActionResult> DeleteRole(string id)
        {
            var rolesViewModel = await RoleManager.FindByIdAsync(id);
            if (rolesViewModel == null)
            {
                return HttpNotFound();
            }
            RolesViewModel rol = new RolesViewModel { Id = rolesViewModel.Id, Name = rolesViewModel.Name };
            return View(rol);
        }

        // POST: RolesViewModels/Delete/5
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleConfirmed(string id)
        {
            var rol = await RoleManager.FindByIdAsync(id);
            string name = rol.Name;
            var result = IdentityResult.Success;
            result = await RoleManager.DeleteAsync(rol);
            if (result.Succeeded)
            {
                result = await RolControllerBLL.Instance.Delete(new Role { Nombre = name });
                if (result.Succeeded)
                    return RedirectToAction("Roles");
            }
            AddErrors(result);
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(rol);
        }
        

        //
        // GET: /Manage/RoleToUser
        public ActionResult AddRolToUser()
        {
            List<SelectListItem> users = (
                    from u in UserManager.Users.ToList()
                    select new SelectListItem { Text = u.Cedula, Value = u.Cedula }
                ).ToList();
            MultiRolSelect model = new MultiRolSelect { MultiRolSelectId = new List<string>(), SelectedRolesList = new List<RolesViewModel>() };
            ViewBag.RolesList = GetRolList();
            ViewBag.Users = users;
            return View(model);
        }

        // POST: RolesToUsers/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRolToUser(MultiRolSelect model, string user )
        {
            // Verification  
            if (ModelState.IsValid)
            {
                // Initialization.  
                List<RolesViewModel> rolList = (
                    from r in RoleManager.Roles.ToList()
                    select new RolesViewModel { Id = r.Id, Name = r.Name }
                ).ToList();
                model.SelectedRolesList = rolList.Where(p => model.MultiRolSelectId.Contains(p.Id)).Select(q => q).ToList();
                string id = UserManager.Users.First(u => u.Cedula.Equals(user)).Id;
                var result = IdentityResult.Success;
                result = await UserManager.AddToRolesAsync(id, model.SelectedRolesList.Select(r => r.Name).ToArray());
                if (result.Succeeded)
                {
                    //result = await RolControllerBLL.Instance.Delete(new Role { Nombre = name });
                    if (result.Succeeded)
                        return RedirectToAction("Roles");
                }
                AddErrors(result);
            }
            List<SelectListItem> users = (
                from u in UserManager.Users.ToList()
                select new SelectListItem { Text = u.Cedula, Value = u.Cedula }
            ).ToList();
            ViewBag.Users = users;
            // Loading drop down lists. 
            ViewBag.RolesList = this.GetRolList();
            return View(model);
        }

        #region Get country method.  

        /// <summary>  
        /// Get country method.  
        /// </summary>  
        /// <returns>Return country for drop down list.</returns>  
        private IEnumerable<SelectListItem> GetRolList()
        { 
            var list = RoleManager.Roles.ToList()
                .Select(p =>
                    new SelectListItem {
                        Value = p.Id,
                        Text = p.Name
                    });
            SelectList lstobj = new SelectList(list, "Value", "Text");
            // info.  
            return lstobj;
        }

        #endregion

     
        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Su contraseña se ha cambiado."
                : message == ManageMessageId.SetPasswordSuccess ? "Su contraseña se ha establecido."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Su proveedor de autenticación de dos factores se ha establecido."
                : message == ManageMessageId.Error ? "Se ha producido un error."
                : message == ManageMessageId.AddPhoneSuccess ? "Se ha agregado su número de teléfono."
                : message == ManageMessageId.RemovePhoneSuccess ? "Se ha quitado su número de teléfono."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generar el token y enviarlo
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Su código de seguridad es: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Enviar un SMS a través del proveedor de SMS para verificar el número de teléfono
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // Si llegamos a este punto, es que se ha producido un error, volvemos a mostrar el formulario
            ModelState.AddModelError("", "No se ha podido comprobar el teléfono");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // Si llegamos a este punto, es que se ha producido un error, volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "Se ha quitado el inicio de sesión externo."
                : message == ManageMessageId.Error ? "Se ha producido un error."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Solicitar la redirección al proveedor de inicio de sesión externo para vincular un inicio de sesión para el usuario actual
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Asistentes
        // Se usan para protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}