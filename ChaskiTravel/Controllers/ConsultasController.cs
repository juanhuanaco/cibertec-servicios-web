using ChaskiTravel.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ChaskiTravel.Controllers
{
    [Authorize]
    public class ConsultasController : Controller
    {
        consultaDAO filtroFecha = new consultaDAO();
        [Authorize(Roles = "Administrador")]
        public IActionResult ConsultarPedido(string fecha = "")
        {
            SqlParameter[] parametros = new SqlParameter[]
              {
                new SqlParameter("@fe",fecha),
              };

      
            return View(filtroFecha.listadoConsulta("usp_consultar_pedido_fecha", parametros));


        }
    }
}
