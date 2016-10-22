using System;
using System.Web.Mvc;
using TPDDSGrupo44.Models;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index ()
        {
            if (BaseViewModel.usuario != null)
            {
                return View(BaseViewModel.usuario);
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection usuario)
        {
            if (BaseViewModel.usuario != null)
            {
                BaseViewModel.usuario.actualizarDatos(usuario["nombre"], usuario["contraseniaActual"], usuario["contraseniaNueva"], usuario["contraseniaNueva2"], Convert.ToInt32(usuario["dni"]), usuario["email"]);
                return View(BaseViewModel.usuario);
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        // GET: User
        [HttpPost]
        public ActionResult Register(FormCollection usuario)
        {
            bool registrado = Usuario.registrarse(usuario["nombre"], usuario["password"], usuario["password2"], usuario["email"], Convert.ToInt32(usuario["dni"]));
            if (registrado)
            {
                return RedirectToAction("LogIn");
            } 
            return View();
        }


        // GET: User
        public ActionResult LogIn()
        {
            return View();
        }

        //POST: User
        [HttpPost]
        public ActionResult LogIn(FormCollection usuario)
        {
            bool login = Usuario.autenticarse(usuario["nombre"], usuario["password"]);

            if (login)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            } else
            {
                return View();
            }

        }


        // GET: LogOut
        public ActionResult LogOut()
        {
            Usuario.salir();

            return RedirectToAction("Index", "Home");
        }
    }
}