using Microsoft.AspNetCore.Mvc;
using SistemasPlenarioMVC.Data;
using SistemasPlenarioMVC.Models;

namespace SistemasPlenarioMVC.Controllers
{
    public class TelefonoController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TelefonoController(ApplicationDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(int id)
        {
            List<Telefono> lisTel = _db.Telefonos.Where(x => x.PersonaId == id).ToList();
            Persona p = _db.Personas.FirstOrDefault(x => x.PersonaId == id);
            ViewData["nombre"] = p.Nombre;
            ViewData["perId"] = id;
            return View(lisTel);
        }
        public IActionResult Agregar(int id)
        {
            List<Telefono> lisTel = new List<Telefono>();
            if (ExistePersona(id))
            {
                lisTel = _db.Telefonos.Where(x => x.PersonaId == id).ToList();
            }
            ViewData["list"] = lisTel;
            ViewData["perId"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(Telefono tel)
        {
            if (ModelState.IsValid)
            {
                _db.Telefonos.Add(tel);
                _db.SaveChanges();
            }
            return RedirectToAction("Index","Persona");
        }
        public IActionResult Modificar(int id)
        {
            Telefono tel = new Telefono();
            if (_db.Telefonos.Find(id) != null)
            {
                tel = _db.Telefonos.Where(x => x.TelefonoId == id).FirstOrDefault();
            }
            return View(tel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Modificar(Telefono tel)
        {
            if (ModelState.IsValid)
            {
                _db.Telefonos.Update(tel);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Persona");
        }
        public IActionResult Borrar(int id)
        {
            var aux = _db.Telefonos.Find(id);
            if ( aux != null)
            {
                _db.Telefonos.Remove(_db.Telefonos.Where(x => x.TelefonoId == id).FirstOrDefault());
                _db.SaveChanges();
            }
            ViewData["perId"] = aux.PersonaId;
            return RedirectToAction("Index", "Persona");
        }
        private bool ExistePersona(int id)
        {
            return _db.Personas.Any(x => x.PersonaId == id);
        }
    }
}
