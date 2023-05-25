using Microsoft.AspNetCore.Mvc;
using ProyectoRom.Models;
using System.Diagnostics;
using ProyectoRom.Models.data;
using ProyectoRom.Models.Repository;


namespace ProyectoRom.Controllers
{
    public class AsignaturaController : Controller
    {
        private readonly IAsignaturaRepository _asignaturaRepository;

        public AsignaturaController(IAsignaturaRepository asignaturaRepository)
        {
            _asignaturaRepository = asignaturaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GuardarAsignatura()
        {
            Asignatura asignatura = new Asignatura();
            //Asignatura asignatura = new Asignatura();
            //libro.asignatura = asignatura;
            //var asignaturas = await _asignaturaRepository.GetAsignatura();

            //ViewBag.asignaturas = asignaturas;
            return View(asignatura);
        }

        public async Task<IActionResult> EditarAsignatura(int id)
        {

            var asignatura = await _asignaturaRepository.GetAsignaturaById(id);
            return View(asignatura);
        }

        [HttpGet]
        public async Task<IActionResult> listarAsignaturas ()
        {
            List<Asignatura>_lista=await _asignaturaRepository.GetAsignatura();
            return View(_lista);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarAsignaturas( Asignatura asig)
        {
            bool _resultado = await _asignaturaRepository.CreateAsignaturaAsync(asig);
            if (_resultado)
                //return StatusCode(StatusCodes.Status200OK, new {valor=_resultado,msg="ok"});
                //return View(_resultado);

                return RedirectToAction("listarAsignaturas");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpPost]
        public async Task<IActionResult> EditarAsignaturas(Asignatura asig)
        {
            bool _resultado = await _asignaturaRepository.UpdateAsignaturaAsync(asig);
            if (_resultado)
                //return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
                return RedirectToAction("listarAsignaturas");

            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });

        }
    }
}
