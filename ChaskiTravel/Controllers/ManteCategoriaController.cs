using ChaskiTravel.DAO;
using ChaskiTravel.Models;
using ChaskiTravel.Models.DI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChaskiTravel.Controllers
{
    [Authorize]
    public class ManteCategoriaController : Controller
    {


        ICategoria _categoria;
        public ManteCategoriaController()
        {
            _categoria = new categoriaDAO();
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            //enviar lista de categoria
            ViewBag.categorias = _categoria.listado();
            return View(new Categoria());
        }

        [HttpPost]
        public IActionResult Create(Categoria c)
        {

            ViewBag.mensaje = _categoria.agregar(c);
            ViewBag.categorias = _categoria.listado();
            return View(c);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(int id)
        {
            Categoria c = _categoria.buscar(id);

            //  if (t == null) return RedirectToAction("Index");


            return View(c);
        }
        [HttpPost]
        public IActionResult Edit(Categoria c)
        {

            ViewBag.mensaje = _categoria.actualizar(c);
            return View(c);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
            _categoria.eliminar(id);


            return RedirectToAction("Create", "ManteCategoria");

        }




    }
}
