using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoRom.Models;
using ProyectoRom.Models.data;
using ProyectoRom.Models.Repository;
using System.Diagnostics;

namespace ProyectoRom.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroRepository _libroRpository;
        private readonly IAsignaturaRepository _asignaturaRepository;

        public LibroController(ILibroRepository libroRpository,IAsignaturaRepository asignaturaRepository)
        {
            _libroRpository = libroRpository;
            _asignaturaRepository = asignaturaRepository;

        }


        public IActionResult Index()
        {
            return View();
        }



  
        public async Task<IActionResult> BuscarLibros(string criterio)
        {
            if (string.IsNullOrEmpty(criterio))
            {
                // Si el criterio está vacío, redirecciona a la página principal u otra acción apropiada
                return RedirectToAction("listarLibros");
            }

            List<Libro> listaLibros = await _libroRpository.BuscarLibros(criterio);
            return View(listaLibros);
        }

        public async Task<IActionResult> GuardarLibros()
        {
            Libro libro= new Libro();
            Asignatura asignatura= new Asignatura();
            libro.asignatura= asignatura;
            var asignaturas = await _asignaturaRepository.GetAsignatura();
            ViewBag.asignaturas = asignaturas;
            return View(libro);
        }

        public async Task<IActionResult> EditarLibros(int id)
        {
       
            var libro = await _libroRpository.GetLibroById(id);
            ViewBag.asignaturas =await _asignaturaRepository.GetAsignatura();
            return View(libro);
        }

        [HttpGet]
        public async Task<IActionResult> listarLibros()
        {
            List<Libro> _lista = await _libroRpository.GetLibro();
            return View(_lista);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarLibro( Libro lib)
        {
            bool _resultado = await _libroRpository.CreateLibroAsync(lib);
           if(_resultado)
                //return StatusCode(StatusCodes.Status200OK, new {valor=_resultado,msg="ok"});
                //return View(_resultado);

                return RedirectToAction("listarLibros");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });

        }


        [HttpPost]
        public async Task<IActionResult> EditarLibro( Libro lib)
        {
            bool _resultado = await _libroRpository.UpdateLibroAsync(lib);
            if (_resultado)
                //return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
                return RedirectToAction("listarLibros");

            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });

        }

        
        public async Task<IActionResult> EliminarLibros(int lib)
        {
            bool _resultado = await _libroRpository.DeleteLibroAsync(lib);
            if (_resultado)
                //return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
                return RedirectToAction("listarLibros");

            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });

        }


    }
}
