using ControleEstoque.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ControleEstoque.WEB.Controllers
{
    public class ContaController : Controller
    {

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var admin = UsuarioModel.ValidarUsuario(login.Usuario, login.Senha);

            if (admin)
            {
                FormsAuthentication.SetAuthCookie(login.Usuario, login.LembrarMe);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login Inválido.");
            }

            return View(login);

        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult logOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}