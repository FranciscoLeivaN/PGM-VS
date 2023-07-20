using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGM_ORM.Models;
using System.Security.Cryptography;
using System.Text;

namespace PGM_ORM.Controllers
{
    public class AcessoController : Controller
    {
        private readonly OrmcrudContext _context;

        public AcessoController(OrmcrudContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validate(string? Clave, string? Correo)
        {
            var correo = Correo;
            var clave = Clave;

            //Condicional para ver si clave o correo están vacías o nulas. 
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                TempData["Error"] = "Usuario o contraseña Invalida, intenta de nuevo.";
                return RedirectToAction("Index","Acesso");
            }

            //Búsqueda de usuario utilizando el correo ingresado desde el formulario. 
            var v_usuario = _context.Usuarios.FirstOrDefault(m => m.Correo == correo);

            if (v_usuario == null)
            {
                //Como no se encontró un usuario con ese correo se envia mensaje de error.
                TempData["Error"] = "Correo electrónico no existe.";
                return RedirectToAction("Index", "Acesso");
            }

            //Se obtiene la clave del usuario encontrado
            var v_clave = v_usuario.Clave;

            //Se codifica la clave en formato SHA256

            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(clave);
            byte[] hash = sha256.ComputeHash(inputBytes);
            string hashedPassword = Convert.ToBase64String(hash);

            //Comprobar contraseñas de formulario y modelo.
            if (hashedPassword != v_clave)
            {
                //Como las contraseñas no coinciden se envia al login nuevamente con mensaje de error.
                TempData["Error"] = "La contraseña no es valida.";
                return RedirectToAction("Index", "Acesso");
                
            }

            //Falta agregar datos de la sesión
            HttpContext.Session.SetInt32("Id_usuario", v_usuario.Id);
            HttpContext.Session.SetString("Nombre_usuario",v_usuario.Nombre);
            HttpContext.Session.SetInt32("Id_departamento", v_usuario.UsuariosDepartamento);

            return RedirectToAction("Index", "Home");


        }
    }
}
