using CrudNetCore5.Data;
using CrudNetCore5.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudNetCore5.Controllers
{
    public class LibrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }
        //http get index
        public IActionResult Index()
        {
            IEnumerable<Libro> listLibros = _context.Libro;
            return View(listLibros);
        }
        //http get create
        public IActionResult Create()
        {
            return View();
        }
        //http post create
        [HttpPost]
        [ValidateAntiForgeryToken] //protección para los formularios para que un bot mande Muchos registros
        public IActionResult Create(Libro libro)
        {
            if (ModelState.IsValid) //crear registro de libro
            {
                _context.Libro.Add(libro);
                _context.SaveChanges();

                TempData["mensaje"] = "El libro se ha creado correctamente.";
                return RedirectToAction("Index");
            }
            return View();
        }
        //http get Edit
        public IActionResult Edit(int? id)
        {
            //si No entra aquí el id está seteado y sí existe el id
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Obtener el libro
            var libro = _context.Libro.Find(id);

            //si No se encuentra un libro con ese id
            if (libro == null)
            {
                return NotFound();
            }

            //si existe retorna el libro
            return View(libro);
        }
        //http post edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Libro libro)
        {
            if (ModelState.IsValid) //crear registro de libro
            {
                _context.Libro.Update(libro);
                _context.SaveChanges();

                TempData["mensaje"] = "El libro se ha actualizado correctamente.";
                return RedirectToAction("Index");
            }
            return View();
        }
        //http get Delete
        public IActionResult Delete(int? id)
        {
            //si No entra aquí el id está seteado y sí existe el id
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Obtener el libro
            var libro = _context.Libro.Find(id);

            //si No se encuentra un libro con ese id
            if (libro == null)
            {
                return NotFound();
            }

            //si existe retorna el libro
            return View(libro);
        }
        //http post delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLibro(int? id) //recibe un entero que puede ser Nulo
        {
            //Obtener el libro por id
            var libro = _context.Libro.Find(id);

            if (libro == null)
            {
                return NotFound();
            }
            
            _context.Libro.Remove(libro);
            _context.SaveChanges();

            TempData["mensaje"] = "El libro se ha eliminado correctamente.";
            return RedirectToAction("Index");

        }
    }
}
