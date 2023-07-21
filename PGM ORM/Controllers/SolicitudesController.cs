using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PGM_ORM.Models;

namespace PGM_ORM.Controllers
{
    public class SolicitudesController : Controller
    {
        private readonly OrmcrudContext _context;

        public SolicitudesController(OrmcrudContext context)
        {
            _context = context;
        }

        // GET: Solicitudes
        public async Task<IActionResult> Index()
        {
            //Datos de la sesión
            int? Id_usuario = HttpContext.Session.GetInt32("Id_usuario");
            int? Id_departamento = HttpContext.Session.GetInt32("Id_departamento");
            var Nombre_usuario = HttpContext.Session.GetString("Nombre_usuario");

            //Comprobar si el usuario corresponde al departamento que gestiona solicitudes
            if(Id_departamento != 2)
            {
                return RedirectToAction("Error", "Acesso");
            }

            var ormcrudContext = _context.Solicitudes.Include(s => s.SolicitudUsuarioNavigation);
            return View(await ormcrudContext.ToListAsync());
        }

        // GET: Solicitudes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Solicitudes == null)
            {
                return NotFound();
            }

            var solicitude = await _context.Solicitudes
                .Include(s => s.SolicitudUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitude == null)
            {
                return NotFound();
            }

            return View(solicitude);
        }

        // GET: Solicitudes/Create
        public IActionResult Create()
        {
            ViewData["SolicitudUsuario"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Solicitudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSolicitud,NombreSolicitud,ApellidosSolicitud,RunSolicitud,TelefonoSolicitud,CorreoSolicitud,FechaSolicitud,DetalleSolicitud,Servicio,SolicitudUsuario")] Solicitude solicitude)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitude);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SolicitudUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", solicitude.SolicitudUsuario);
            return View(solicitude);
        }

        // GET: Solicitudes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Solicitudes == null)
            {
                return NotFound();
            }

            var solicitude = await _context.Solicitudes.FindAsync(id);
            if (solicitude == null)
            {
                return NotFound();
            }
            ViewData["SolicitudUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", solicitude.SolicitudUsuario);
            return View(solicitude);
        }

        // POST: Solicitudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSolicitud,NombreSolicitud,ApellidosSolicitud,RunSolicitud,TelefonoSolicitud,CorreoSolicitud,FechaSolicitud,DetalleSolicitud,Servicio,SolicitudUsuario")] Solicitude solicitude)
        {
            if (id != solicitude.IdSolicitud)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitude);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudeExists(solicitude.IdSolicitud))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SolicitudUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", solicitude.SolicitudUsuario);
            return View(solicitude);
        }

        // GET: Solicitudes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Solicitudes == null)
            {
                return NotFound();
            }

            var solicitude = await _context.Solicitudes
                .Include(s => s.SolicitudUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitude == null)
            {
                return NotFound();
            }

            return View(solicitude);
        }

        // POST: Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Solicitudes == null)
            {
                return Problem("Entity set 'OrmcrudContext.Solicitudes'  is null.");
            }
            var solicitude = await _context.Solicitudes.FindAsync(id);
            if (solicitude != null)
            {
                _context.Solicitudes.Remove(solicitude);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudeExists(int id)
        {
          return (_context.Solicitudes?.Any(e => e.IdSolicitud == id)).GetValueOrDefault();
        }
    }
}
