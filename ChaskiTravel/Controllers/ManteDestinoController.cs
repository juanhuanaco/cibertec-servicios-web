using ChaskiTravel.DAO;
using ChaskiTravel.Models;
using ChaskiTravel.Models.DI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaskiTravel.Controllers
{
    [Authorize]
    public class ManteDestinoController : Controller
    {
        IHotel _hotel;
        IDestino _destino;
        ICategoria _categoria;
        ITour _tour;
        public ManteDestinoController()
        {

            _hotel = new hotelDAO();
            _destino = new destinoDAO();
            _categoria = new categoriaDAO();
            _tour = new tourDAO();
        }
        //  [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {

            //enviar lista de hoteles
            ViewBag.categoriaLis = new SelectList(_categoria.listado(), "IdCategoria", "NombreCategoria");
            ViewBag.hotelLis = new SelectList(_hotel.listado(), "idHotel", "nomHotel");
            ViewBag.toursList = new SelectList(_tour.listado(), "idTour", "descripcionTour");
            ViewBag.destinos = _destino.listado();
            return View(new Destino());

        }
        [HttpPost]
        public IActionResult Create(Destino d)
        {

            ViewBag.mensaje = _destino.agregar(d);
            ViewBag.categoriaLis = new SelectList(_categoria.listado(), "IdCategoria", "NombreCategoria", d.IdCategoria);

            ViewBag.hotelLis = new SelectList(_hotel.listado(), "idHotel", "nomHotel", d.idHotel);
            ViewBag.toursList = new SelectList(_tour.listado(), "idTour", "descripcionTour", d.idTour);
            ViewBag.destinos = _destino.listado();
            return View(d);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(int id)
        {
            Destino d = _destino.buscar(id);

            //  if (t == null) return RedirectToAction("Index");
            ViewBag.categoriaLis = new SelectList(_categoria.listado(), "IdCategoria", "NombreCategoria", d.IdCategoria);

            ViewBag.hotelLis = new SelectList(_hotel.listado(), "idHotel", "nomHotel", d.idHotel);
            ViewBag.toursList = new SelectList(_tour.listado(), "idTour", "descripcionTour", d.idTour);



            return View(d);
        }
        [HttpPost]
        public IActionResult Edit(Destino d)
        {

            ViewBag.mensajeEditar = _destino.actualizar(d);
            ViewBag.categoriaLis = new SelectList(_categoria.listado(), "IdCategoria", "NombreCategoria", d.IdCategoria);
            ViewBag.hotelLis = new SelectList(_hotel.listado(), "idHotel", "nomHotel", d.idHotel);
            ViewBag.toursList = new SelectList(_tour.listado(), "idTour", "descripcionTour", d.idTour);

            return View(d);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
             _destino.eliminar(id);


            return RedirectToAction("Create", "ManteDestino");

        }
    }


}
