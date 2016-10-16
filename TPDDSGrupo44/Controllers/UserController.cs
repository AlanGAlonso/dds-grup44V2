using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //POST: User
        [HttpPost]
        public ActionResult Index(FormCollection usuario)
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
    }
}