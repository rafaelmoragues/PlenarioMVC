using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasPlenarioMVC.Data;
using SistemasPlenarioMVC.Models;

namespace SistemasPlenarioMVC.Controllers
{
    public class PersonaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PersonaController(ApplicationDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            List<Persona> listPer = _db.Personas.ToList();
            return View(listPer);
        }
        public IActionResult Agregar(int? id) {
            Persona per = new Persona();
            if (_db.Personas.Find(id) != null)
            {
                per = _db.Personas.Where(x => x.PersonaId == id).FirstOrDefault();
            }
            return View(per); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(Persona per)
        {
            if (ModelState.IsValid)
            {
                if (ExistePersona(per.PersonaId))
                {
                    _db.Personas.Update(per);
                    _db.SaveChanges();                    
                }
                else
                {
                    _db.Personas.Add(per);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Borrar(int id)
        {
            
            if (ExistePersona(id))
            {
                _db.Personas.Remove(_db.Personas.Where(x => x.PersonaId == id).FirstOrDefault());
                _db.SaveChanges();
            }
                return RedirectToAction("Index");
        }
        public IActionResult Buscar(string nombre)
        {
            Persona per = new Persona();
            per = _db.Personas.Where(x => x.Nombre == nombre).Include(y =>y.Telefonos).FirstOrDefault();

            return View("MostrarPersona", per);
        }
        private bool ExistePersona(int id)
        {
            return _db.Personas.Any(x => x.PersonaId == id);
        }
    }
}
