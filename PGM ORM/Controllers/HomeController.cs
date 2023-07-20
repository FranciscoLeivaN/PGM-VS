using Microsoft.AspNetCore.Mvc;
using PGM_ORM.Models;
using System.Diagnostics;

namespace PGM_ORM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int? Id_usuario = HttpContext.Session.GetInt32("Id_usuario");
            int? Id_departamento = HttpContext.Session.GetInt32("Id_departamento");
            var Nombre_usuario = HttpContext.Session.GetString("Nombre_usuario");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}