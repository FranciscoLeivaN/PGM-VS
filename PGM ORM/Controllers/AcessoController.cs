using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGM_ORM.Models;

namespace PGM_ORM.Controllers
{
    public class AcessoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validate(string Clave, string Correo)
        {
            var correo = Correo;
            var clave = Clave;

            if (correo == "" || clave == "")
            {
               
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
